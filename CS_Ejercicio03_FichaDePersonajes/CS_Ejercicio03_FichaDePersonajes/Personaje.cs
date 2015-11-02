using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Ejercicio03_FichaDePersonajes {
    class Personaje {
        private string nombreP, nombreJ, genero, raza, clase;
        private int[] atributos;
        private bool[] habilidades;
        int numTirada, habPorSeleccionar, ptosARepartirA;

        public Personaje(string nombreP, string nombreJ, string genero, string raza, string clase, int[] atributos, bool[] habilidades, int numTirada, int habPorSeleccionar, int ptosARepartirA) {
            this.nombreP = nombreP;
            this.nombreJ = nombreJ;
            this.genero = genero;
            this.raza = raza;
            this.clase = clase;
            this.atributos = atributos;
            this.habilidades = habilidades;
            this.numTirada = numTirada;
            this.habPorSeleccionar = habPorSeleccionar;
            this.ptosARepartirA = ptosARepartirA;
        }

        // Getters
        public int getNumTirada() {
            return numTirada;
        }
        public int getHabSeleccionadas() {
            return habPorSeleccionar;
        }
        public int getPtosARepartirA() {
            return ptosARepartirA;
        }
        public string getNombreP() {
            return nombreP;
        }
        public string getNombreJ() {
            return nombreJ;
        }
        public string getGenero() {
            return genero;
        }
        public string getRaza() {
            return raza;
        }
        public string getClase() {
            return clase;
        }
        public bool[] getHabilidades() {
            return habilidades;
        }
        public int[] getAtributos() {
            return atributos;
        }
        // Setters.
        public void setNumTirada(int numTirada) {
            this.numTirada = numTirada;
        }

        public void setHabSeleccionadas(int habSeleccionadas) {
            this.habPorSeleccionar = habSeleccionadas;
        }

        public void setPtosARepartirA(int ptosARepartirA) {
            this.ptosARepartirA = ptosARepartirA;
        }

        public void setNombreP(String nombreP) {
            this.nombreP = nombreP;
        }

        public void setNombreJ(string nombreJ) {
            this.nombreJ = nombreJ;
        }

        public void setGenero(string genero) {
            this.genero = genero;
        }

        public void setRaza(string raza) {
            this.raza = raza;
        }

        public void setClase(string clase) {
            this.clase = clase;
        }

        public void setHabilidades(bool[] habilidades) {
            this.habilidades = habilidades;
        }

        public void setAtributos(int[] atributos) {
            this.atributos = atributos;
        }
    }
}
