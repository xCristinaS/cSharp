using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                while((linea = lector.ReadLine()) != null) 
                    lista.AddLast(Personaje.montarPersonaje(linea));
                
                lector.Close();
            } else
                File.Create("..\\..\\datosPersonajes.txt");
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
    }
}
