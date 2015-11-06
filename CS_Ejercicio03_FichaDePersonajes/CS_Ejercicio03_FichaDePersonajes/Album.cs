using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
