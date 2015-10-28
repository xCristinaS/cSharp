using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio03_FichaDePersonajes
{
    public partial class Form1 : Form {
        private String[] personajesMagicos = { "Mago", "Nigromante" };
        String[] personajesMundanos = { "Arquero", "Daguero", "Cazador", "Guerrero", "Paladin" };
        int[] valoresAtributosAleatorios = new int[10]; private bool dadoApagado = false;
        private byte numTirada = 0, ptosRepAtrib = 15, ptosRepHab = 20, cbPorSelec = 7;
        private Random rnd = new Random();

        public Form1() {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e) {
            obtenerValoresAleatorios();
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
            lblPuntosRepartirH.Text = Constantes.PTOS_A_REP + ptosRepHab;
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + cbPorSelec;
        }

        private void clicCerrar(object sender, EventArgs e) {
            this.Close();
        }
        // Este método es lanzado cuando cambia el comboboxRaza. 
        private void comboboxCambiado(object sender, EventArgs e) {
            combClase.Items.Clear();
            if (combRaza.SelectedIndex == 0)
                combClase.Items.AddRange(personajesMagicos);
            else
                combClase.Items.AddRange(personajesMundanos);

            if (!combRaza.Text.Equals("")) {
                resetValoresAtrib();
                this.BackgroundImage = null;
            }
        }

        private void actualizarImg(object sender, EventArgs e) {
            mostrarImgPersonaje(sender);
        }

        private void mostrarImgPersonaje(object sender) {
            String personaje;
            if (!combClase.Text.Equals("")) {
                personaje = combClase.SelectedItem.ToString();
                // Sólo doy el plus a los atributos si quien lanzó el evento fue el cambio de clase del personaje.
                // El plus de los atributos variará en función del personaje seleccionado. 
                if (sender.Equals(combClase))
                    asignarAtributosPlusPSJ(personaje);
                personaje = personaje.Substring(0, personaje.Length - 1);
                personaje = personaje.ToLower();

                if (!personaje.Equals("nigromant")) {
                    if (rbtnFemenino.Checked)
                        personaje += "a";
                    if (rbtnMasculino.Checked)
                        personaje += "o";
                }

                switch (personaje) {
                    case "cazadoa": // Al elegir "cazador" arriba a cazador se le quita la "r" y si esta marcado el femenino se añade una "a".
                        this.BackgroundImage = Properties.Resources.cazadora;
                        break;
                    case "cazadoo": // Al elegir "cazador" arriba a cazador se le quita la "r" y si esta marcado el masculino se le añade una "o".
                        this.BackgroundImage = Properties.Resources.cazador;
                        break;
                    case "arquero":
                        this.BackgroundImage = Properties.Resources.arquero;
                        break;
                    case "arquera":
                        this.BackgroundImage = Properties.Resources.arquera;
                        break;
                    case "paladio": // Al elegir "paladin" arriba se le quita la "n" y si está marcado el masculino se añade una "o".
                        this.BackgroundImage = Properties.Resources.paladin;
                        break;
                    case "paladia": // Al elegir "paladin" arriba se le quita la "n" y si está marcado el femenino se añade una "a". 
                        this.BackgroundImage = Properties.Resources.paladina;
                        break;
                    case "daguero":
                        this.BackgroundImage = Properties.Resources.daguero;
                        break;
                    case "daguera":
                        this.BackgroundImage = Properties.Resources.daguera;
                        break;
                    case "guerrero":
                        this.BackgroundImage = Properties.Resources.guerrero;
                        break;
                    case "guerrera":
                        this.BackgroundImage = Properties.Resources.guerrera;
                        break;
                    case "mago":
                        this.BackgroundImage = Properties.Resources.mago;
                        break;
                    case "maga":
                        this.BackgroundImage = Properties.Resources.maga;
                        break;
                    case "nigromant":
                        this.BackgroundImage = Properties.Resources.nigromante;
                        break;
                }
            }
        }

        private void asignarAtributosPlusPSJ(String personaje) {
            switch (personaje) {
                case "Mago":
                    pbCarisma.Value = Constantes.MAGO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.MAGO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.MAGO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.MAGO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.MAGO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.MAGO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.MAGO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.MAGO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.MAGO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.MAGO_VITALIDAD_PLUS;
                    break;
                case "Guerrero":
                    pbCarisma.Value = Constantes.GUERRERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.GUERRERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.GUERRERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.GUERRERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.GUERRERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.GUERRERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.GUERRERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.GUERRERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.GUERRERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.GUERRERO_VITALIDAD_PLUS;
                    break;
                case "Paladin":
                    pbCarisma.Value = Constantes.PALADIN_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.PALADIN_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.PALADIN_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.PALADIN_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.PALADIN_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.PALADIN_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.PALADIN_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.PALADIN_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.PALADIN_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.PALADIN_VITALIDAD_PLUS;
                    break;
                case "Nigromante":
                    pbCarisma.Value = Constantes.NIGROMANTE_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.NIGROMANTE_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.NIGROMANTE_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.NIGROMANTE_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.NIGROMANTE_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.NIGROMANTE_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.NIGROMANTE_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.NIGROMANTE_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.NIGROMANTE_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.NIGROMANTE_VITALIDAD_PLUS;
                    break;
                case "Daguero":
                    pbCarisma.Value = Constantes.DAGUERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.DAGUERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.DAGUERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.DAGUERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.DAGUERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.DAGUERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.DAGUERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.DAGUERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.DAGUERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.DAGUERO_VITALIDAD_PLUS;
                    break;
                case "Arquero":
                    pbCarisma.Value = Constantes.ARQUERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.ARQUERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.ARQUERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.ARQUERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.ARQUERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.ARQUERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.ARQUERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.ARQUERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.ARQUERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.ARQUERO_VITALIDAD_PLUS;
                    break;
                case "Cazador":
                    pbCarisma.Value = Constantes.CAZADOR_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.CAZADOR_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.CAZADOR_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.CAZADOR_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.CAZADOR_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.CAZADOR_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.CAZADOR_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.CAZADOR_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.CAZADOR_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.CAZADOR_VITALIDAD_PLUS;
                    break;
            }
            darValorAtributosAleatorios();
        }

        private void darValorAtributosAleatorios() {
            pbCarisma.Value += valoresAtributosAleatorios[0];
            pbCoraje.Value += valoresAtributosAleatorios[1];
            pbDestreza.Value += valoresAtributosAleatorios[2];
            pbFuerza.Value += valoresAtributosAleatorios[3];
            pbIngenio.Value += valoresAtributosAleatorios[4];
            pbIniciativa.Value += valoresAtributosAleatorios[5];
            pbPercepcion.Value += valoresAtributosAleatorios[6];
            pbReflejos.Value += valoresAtributosAleatorios[7];
            pbVelocidad.Value += valoresAtributosAleatorios[8];
            pbVitalidad.Value += valoresAtributosAleatorios[9];
        }

        private void resetValoresAtrib() {
            pbCarisma.Value = 0;
            pbCoraje.Value = 0;
            pbDestreza.Value = 0;
            pbFuerza.Value = 0;
            pbIngenio.Value = 0;
            pbIniciativa.Value = 0;
            pbPercepcion.Value = 0;
            pbReflejos.Value = 0;
            pbVelocidad.Value = 0;
            pbVitalidad.Value = 0;
        }

        private void tirarDado(object sender, EventArgs e) {
            String personaje;
            if (!combClase.Text.Equals("") && numTirada < Constantes.MAX_TIRADAS) {
                resetValoresAtrib();
                personaje = combClase.SelectedItem.ToString();
                obtenerValoresAleatorios();
                asignarAtributosPlusPSJ(personaje);
                numTirada++;
            }

            if (!dadoApagado && numTirada == Constantes.MAX_TIRADAS) {
                imgDado.BackgroundImage = Properties.Resources.dadoApagado;
                dadoApagado = true;
            }
        }

        private void obtenerValoresAleatorios() {
            for (int i = 0; i < valoresAtributosAleatorios.Length; i++)
                valoresAtributosAleatorios[i] = rnd.Next(Constantes.MIN_VALOR_ATRIB_ALEATORIO, Constantes.MAX_VALOR_ATRIB_ALEATORIO);
        }
    }
}
