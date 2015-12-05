using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CS_Ejercicio03_FichaDePersonajes {
    class Album {
        private LinkedList<Personaje> lista;
        private int posicion;

        public Album() {
            posicion = 0;
            lista = new LinkedList<Personaje>();
        }
        public void agregarPersonaje(Personaje p) {
            lista.AddLast(p);
        }
        public Personaje personajeActual() {
            Personaje p = null;
            if (!vacio())
                p = lista.ElementAt(posicion);

            return p;
        }
        public bool vacio() {
            bool r = false;
            if (lista.Count == 0)
                r = true;
            return r;
        }
        public int cuantosPjHay() {
            return lista.Count;
        }
        public void importarPjs() {
            StreamReader lector; string linea; Personaje p;
            if (File.Exists("..\\..\\datosPersonajes.txt")) {
                lector = new StreamReader("..\\..\\datosPersonajes.txt");
                while((linea = lector.ReadLine()) != null) // leo el fichero linea a linea
                    if ((p = Personaje.montarPersonaje(linea)) != null) // si el personaje que monto de la linea no es nulo
                        lista.AddLast(p); // lo agrego a la lista de personajes del álbum.
                
                lector.Close();
            } 
        }
        public void exportarPjs() {
            StreamWriter escritor = new StreamWriter("..\\..\\datosPersonajes.txt");
            foreach (Personaje p in lista) 
                escritor.WriteLine(p.escribirPersonaje());
            
            escritor.Close();
        }
        public Personaje mostrarSiguientePj() {
            Personaje p = null;
            if (posicion + 1 < lista.Count) {
                posicion++;
                p = lista.ElementAt(posicion);
            }
            return p;
        }
        public Personaje mostrarAnteriorPj() {
            Personaje p = null;
            if (posicion - 1 >= 0) {
                posicion--;
                p = lista.ElementAt(posicion);
            }
            return p;
        }
        public bool hayAnterior() {
            bool r = true;
            if (posicion == 0) 
                r = false;
            return r;
        }
        public bool haySiguiente() {
            bool r = true;
            if (posicion == lista.Count - 1)
                r = false;
            return r;
        }
        public void eliminarPj() {
            lista.Remove(lista.ElementAt(posicion));
            if (posicion > 0)
                posicion--;
        }
        public bool personajesMismoNombre(string nombreP) {
            bool r = false;
            foreach (Personaje p in lista)
                if (p.getNombreP().Equals(nombreP))
                    r = true;
            return r;
        }
        public void importarDesde(string ruta) {
            StreamReader lector; string linea; Personaje p; bool importar = true;
            if (File.Exists(ruta)) {
                lector = new StreamReader(ruta);
                while ((linea = lector.ReadLine()) != null) {
                    if ((p = Personaje.montarPersonaje(linea)) != null) {
                        for (int i = 0; i < lista.Count; i++) // para no importar personajes con el mismo nombre.
                            if (lista.ElementAt(i).getNombreP().Equals(p.getNombreP()))
                                importar = false;
                        
                        if (importar)
                            lista.AddLast(p);
                    }
                }
                lector.Close();
            }
        }
        public void exportarA(string ruta) {
            StreamWriter escritor = new StreamWriter(ruta);
            foreach (Personaje p in lista)
                escritor.WriteLine(p.escribirPersonaje());

            escritor.Close();
        }
    }
}
