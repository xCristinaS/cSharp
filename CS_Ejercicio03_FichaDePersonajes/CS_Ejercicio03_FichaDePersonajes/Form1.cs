using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CS_Ejercicio03_FichaDePersonajes
{
    public partial class Form1 : Form {
        private string[] personajesMagicos = { "Mago", "Nigromante" };
        string[] personajesMundanos = { "Arquero", "Daguero", "Cazador", "Guerrero", "Paladin" };
        int[] valoresAtributosAleatorios = new int[10]; private bool dadoApagado = false;
        private int numTirada = 0, ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB, habPorSelect = Constantes.HABILIDADES_SELECCIONABLES, aux = 0;
        private Random rnd = new Random(); bool modoEdicion = false, carga1 = false, carga2 = false, carga3 = false, carga4 = false;
        private Album album = new Album(); int numTiradaME = 0;

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);
        IntPtr cursor = LoadCursorFromFile(@"../../Resources/cursor.cur");

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            imgCerrar.Visible = false;
            habilitarDragDrop();
            obtenerValoresAleatorios(); // Relleno el arrays de valores de los atributos con números aleatorios.
            deshabilitarHabilidades(); // Para que no se puedan marcar si no hay personaje seleccionado. 
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            // A todos los picture box que están en el panel de atributos les cambio su .Tag a valor 0, para despúes poder 
            // jugar con los valores que se le vayan dando e incrementar o decrementar las progressbar de habilidades en 
            // función de los puntos que reparta el usuario. 
            foreach (object pbAtributo in panelAtributos.Controls) {
                if (pbAtributo is PictureBox) {
                    ((PictureBox)pbAtributo).Tag = 0;
                    if (((PictureBox)pbAtributo).Name.StartsWith("d"))  // Si la flecha es de decremento, la apago, para indicar que no se puede decrementar la barra. 
                        ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaIzqApagada;
                }
            }
            // Oculto todos los elementos para emular un menú de carga. 
            ocultarPaginaNewPersonaje();
            panelVistaPersonaje.Visible = false;
            menuSeleccion.Visible = false;
            ocultarModoEdicion();
            album.importarPjs();
            actualizarFlechasDesplazamiento();
            if (!album.vacio()) {
                imgAlbum.BackgroundImage = Properties.Resources.album;
                imgAlbum.Enabled = true;
            } else
                imgAlbum.Enabled = false;
            /*
            this.Cursor = new Cursor(cursor);
            wplayer.URL = @"songGOT.mp3";
            wplayer.controls.play();
            */
        }
        private void clicCerrar(object sender, EventArgs e) {
            this.Close();
            album.exportarPjs();
        }
        private void comboboxCambiado(object sender, EventArgs e) { // Este método es lanzado cuando cambia el comboboxRaza. 
            combClase.Items.Clear();
            // Los valores del combClase variarán en función de la raza seleccionada. Si el indice es 0, se cargarán 
            // los personajesMagicos en el combClase, en caso contrario se cargarán los personajesMundanos.
            if (combRaza.SelectedIndex == 0)
                combClase.Items.AddRange(personajesMagicos);
            else
                combClase.Items.AddRange(personajesMundanos);
            // Cuando cambio la raza, la clase se desselecciona, y por tanto debo resetear los valores de los atributos
            // de la clase que se hubiera seleccionado anteriormente y quitar la imagen del psj. 
            if (!combRaza.Text.Equals("")) {
                resetValoresAtrib();
                vaciarMochila();
                vaciarObjetosEquip();
                deshabilitarMochila();
                resetFlechasAtributos();
                limpiarHabilidadesMarcadas();
                imgPropiedades_Click(null, null);
                deshabilitarHabilidades();
                this.BackgroundImage = Properties.Resources.fondo;
            }
        } 
        private void actualizarImg(object sender, EventArgs e) {
            mostrarImgPersonaje(sender);
        }
        private void mostrarImgPersonaje(object sender) {
            String personaje;
            if (!combClase.Text.Equals("")) {
                habilitarHabilidades();
                personaje = combClase.SelectedItem.ToString();
                // Sólo doy el plus a los atributos si quien lanzó el evento fue el cambio de clase del personaje.
                // El plus de los atributos variará en función del personaje seleccionado. 
                if (sender != null && sender.Equals(combClase)) { // Hago la comprobación de si el sender es != null porque llamo a este método cuando cargo el personaje para cargar su imagen. 
                    asignarAtributosPlusPSJ(personaje);
                    cargarObjetosEquipables(personaje);
                    habilitarMochila();
                    vaciarMochila();
                    limpiarHabilidadesMarcadas();
                    resetFlechasAtributos();
                }
                // A la cadena del personaje seleccionado le quito el último carácter y lo paso a minúsculas.
                personaje = personaje.Substring(0, personaje.Length - 1);
                personaje = personaje.ToLower();

                // Si el personaje seleccionado es diferente al nigromante, que es el único personaje que 
                // no tiene género, a la cadena del personaje seleccionado le agrego una "a" en caso de que
                // el genero sea femenino y una "o" en caso de que sea masculino. 
                if (!personaje.Equals("nigromant")) {
                    if (rbtnFemenino.Checked)
                        personaje += "a";
                    if (rbtnMasculino.Checked)
                        personaje += "o";
                }
                cargarImgPersonaje(personaje);
            }
        }
        private void cargarImgPersonaje(string personaje) {
            // Evalúo la cadena personaje y establezco la imagen de fondo correspondiente al personaje seleccionado y su género. 
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
        private void asignarAtributosPlusPSJ(string personaje) { // Doy un valor extra a cada atributo en función del personaje seleccionado.
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
            darValorAtributosAleatorios(); // Cada vez que se cambia de personaje debo volver a sumar los valores aleatorios a la base de ptos por personaje.
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
            // Reestablezco los puntos a repartir, puesto que si he llegado aquí es porque se ha cambiado de personaje y
            // en ese caso, si se hizo un reparto de puntos para el personaje anterior los cambios deben ser suprimidos.
            ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB;
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
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
        private void repartirPtosAtb(object sender, EventArgs e) {
            // Si hay un personaje seleccionado y el evento lo lanzó una flecha (imagen en pictureBox) de incremento, incrementaré el atributo
            // en caso de que el evento fuera lanzado al hacer clic en una flecha de decremento, decremento el valor del atributo asociado a dicha flecha. 
            if (!combClase.Text.Equals("") || modoEdicion)
                if (ptosRepAtrib > 0 && ((PictureBox)sender).Name.StartsWith("i"))
                    incrementarAtributo(sender);
                else if (((int)((PictureBox)sender).Tag) > 0)
                    decrementarAtributo(sender);

            if (modoEdicion) 
                habilitarGuardadoME();
        }
        private void incrementarAtributo(object sender) {
            // Evalúo qué flecha de incremento fue clickeada e incremento en 1 el valor de la progressBar asociada a 
            // dicha flecha. A su vez, al .Tag asociado a la flecha de decremento le doy un +1. Lo que pretendo con esto, 
            // es que únicamente se pueda decrementar cuando previamente se haya incrementado, para nunca alterar la base 
            // que se estableció al sumar los puntos asociados a cada personaje con los valores aleatorios. 
            if (sender.Equals(incVit) && pbVitalidad.Value < pbVitalidad.Maximum) {
                pbVitalidad.Value++;
                ptosRepAtrib--;
                decVit.Tag = ((int)decVit.Tag) + 1;
                decVit.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbVitalidad.Value == pbVitalidad.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incVit.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incVit.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incCar) && pbCarisma.Value < pbCarisma.Maximum) {
                pbCarisma.Value++;
                ptosRepAtrib--;
                decCar.Tag = ((int)decCar.Tag) + 1;
                decCar.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbCarisma.Value == pbCarisma.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incCar.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incCar.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incCor) && pbCoraje.Value < pbCoraje.Maximum) {
                pbCoraje.Value++;
                ptosRepAtrib--;
                decCor.Tag = ((int)decCor.Tag) + 1;
                decCor.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbCoraje.Value == pbCoraje.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incCor.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incCor.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incDest) && pbDestreza.Value < pbDestreza.Maximum) {
                pbDestreza.Value++;
                ptosRepAtrib--;
                decDest.Tag = ((int)decDest.Tag) + 1;
                decDest.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbDestreza.Value == pbDestreza.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incDest.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incDest.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incFuer) && pbFuerza.Value < pbFuerza.Maximum) {
                pbFuerza.Value++;
                ptosRepAtrib--;
                decFuer.Tag = ((int)decFuer.Tag) + 1;
                decFuer.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbFuerza.Value == pbFuerza.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incFuer.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incFuer.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incIng) && pbIngenio.Value < pbIngenio.Maximum) {
                pbIngenio.Value++;
                ptosRepAtrib--;
                decIng.Tag = ((int)decIng.Tag) + 1;
                decIng.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbIngenio.Value == pbIngenio.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incIng.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incIng.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incIni) && pbIniciativa.Value < pbIniciativa.Maximum) {
                pbIniciativa.Value++;
                ptosRepAtrib--;
                decIni.Tag = ((int)decIni.Tag) + 1;
                decIni.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbIniciativa.Value == pbIniciativa.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incIni.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incIni.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incPerc) && pbPercepcion.Value < pbPercepcion.Maximum) {
                pbPercepcion.Value++;
                ptosRepAtrib--;
                decPerc.Tag = ((int)decPerc.Tag) + 1;
                decPerc.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbPercepcion.Value == pbPercepcion.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incPerc.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incPerc.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            } else if (sender.Equals(incRef) && pbReflejos.Value < pbReflejos.Maximum) {
                pbReflejos.Value++;
                ptosRepAtrib--;
                decRef.Tag = ((int)decRef.Tag) + 1;
                decRef.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbReflejos.Value == pbReflejos.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incRef.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incRef.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                 }
            } else if (sender.Equals(incVel) && pbVelocidad.Value < pbVelocidad.Maximum) {
                pbVelocidad.Value++;
                ptosRepAtrib--;
                decVel.Tag = ((int)decVel.Tag) + 1;
                decVel.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
                if (pbVelocidad.Value == pbVelocidad.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                    incVel.BackgroundImage = Properties.Resources.flechaDerApagada;
                    incVel.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
                }
            }
            if (ptosRepAtrib == 0) { // Cuando los puntos a repartir llegan a 0, apago las flechas de incremento. 
                foreach (object pbAtributo in panelAtributos.Controls)
                    if (pbAtributo is PictureBox)
                        if (((PictureBox)pbAtributo).Name.StartsWith("i"))
                            ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaDerApagada;
            }
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib; // Actualizo los puntos a repartir. 
        }
        private void decrementarAtributo(object sender) {
            // Evalúo que flecha de decremento fue seleccionada y decremento el valor de la progressbar asociada a dicha flecha.
            // Sólo podre decrementar el valor de la progressbar si el tag de la flecha de decremento fue previamente incrementado (esta 
            // comprobación se hace previamente, en el evento "repartirPtosAtb" que es el que se lanza al hacer clic en la flecha de decremento).
            if (sender.Equals(decVit)) {
                pbVitalidad.Value--;
                ptosRepAtrib++;
                decVit.Tag = ((int)decVit.Tag) - 1;
                incVit.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incVit.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento. 
                if ((int)decVit.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decVit.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decCar)) {
                pbCarisma.Value--;
                ptosRepAtrib++;
                decCar.Tag = ((int)decCar.Tag) - 1;
                incCar.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incCar.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decCar.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decCar.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decCor)) {
                pbCoraje.Value--;
                ptosRepAtrib++;
                decCor.Tag = ((int)decCor.Tag) - 1;
                incCor.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incCor.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decCor.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decCor.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decDest)) {
                pbDestreza.Value--;
                ptosRepAtrib++;
                decDest.Tag = ((int)decDest.Tag) - 1;
                incDest.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incDest.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decDest.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decDest.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decFuer)) {
                pbFuerza.Value--;
                ptosRepAtrib++;
                decFuer.Tag = ((int)decFuer.Tag) - 1;
                incFuer.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incFuer.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decFuer.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decFuer.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decIng)) {
                pbIngenio.Value--;
                ptosRepAtrib++;
                decIng.Tag = ((int)decIng.Tag) - 1;
                incIng.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incIng.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decIng.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decIng.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decIni)) {
                pbIniciativa.Value--;
                ptosRepAtrib++;
                decIni.Tag = ((int)decIni.Tag) - 1;
                incIni.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incIni.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decIni.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decIni.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decPerc)) {
                pbPercepcion.Value--;
                ptosRepAtrib++;
                decPerc.Tag = ((int)decPerc.Tag) - 1;
                incPerc.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incPerc.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decPerc.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decPerc.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decRef)) {
                pbReflejos.Value--;
                ptosRepAtrib++;
                decRef.Tag = ((int)decRef.Tag) - 1;
                incRef.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incRef.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decRef.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decRef.BackgroundImage = Properties.Resources.flechaIzqApagada;
            } else if (sender.Equals(decVel)) {
                pbVelocidad.Value--;
                ptosRepAtrib++;
                decVel.Tag = ((int)decVel.Tag) - 1;
                incVel.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
                incVel.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
                if ((int)decVel.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                    decVel.BackgroundImage = Properties.Resources.flechaIzqApagada;
            }
            if (ptosRepAtrib == 0) {
                foreach (object pbAtributo in panelAtributos.Controls)
                    if (pbAtributo is PictureBox)
                        if (((PictureBox)pbAtributo).Name.StartsWith("i"))  // Si la flecha es de incremento, la apago, para indicar que no se puede incrementar la barra. 
                            ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaDerApagada;
            }
            // Si he llegado hasta aquí es porque he decrementado alguna de las flechas. Si los puntos a repartir son igual a 1, significa que previamente eran 0 y por
            // tanto todas las flechas de incremento estaban apagadas, en ese caso vuelvo a habilitar todas las flechas de incremento siempre y cuando la barra asociada a ellas
            // no esté llena. 
            if (ptosRepAtrib == 1) { 
                foreach (object pbAtributo in panelAtributos.Controls)
                    if (pbAtributo is PictureBox)
                        if (((PictureBox)pbAtributo).Name.StartsWith("i") && (int)(((PictureBox)pbAtributo).Tag) != -1)
                            ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaDer;
            }
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib; // Actualizo los puntos a repartir. 
        }
        private void habilidadesCheckedChange(object sender, EventArgs e) {
            CheckBox cb = (CheckBox)sender; 
            if (cb.Checked) 
                habPorSelect--; // Si se ha seleccionado un checkbox decremento la cantidad de habilidades por seleccionar. 
            else
                habPorSelect++; // Si se ha seleccionado un checkbox incremento la cantidad de habilidades por seleccionar.

            if (habPorSelect == 0) {
                // Si ya se han seleccionado el máximo número de habilidades, deshabilito todas las que no estén marcadas. 
                foreach (object cb2 in panelHabilidades.Controls) {
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = false;
                }
            } else {
                // Si quedan habilidades por seleccionar, habilito todas las que estén desmarcadas. 
                foreach (object cb2 in panelHabilidades.Controls) {
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = true;
                }
            }
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            if (modoEdicion)
                habilitarGuardadoME();
        }
        private void habilitarHabilidades() {
            // Habilito los checkbox siempre y cuando haya un personaje seleccionado. 
            foreach (object cb2 in panelHabilidades.Controls) {
                if (cb2 is CheckBox)
                    ((CheckBox)cb2).Enabled = true;
            }
        }
        private void deshabilitarHabilidades() {
           // Deshabilito los checkbox si no hay personaje seleccionado. 
                foreach (object cb2 in panelHabilidades.Controls) {
                    if (cb2 is CheckBox)
                        ((CheckBox)cb2).Enabled = false;
                }
            }
        private void habilitarMochila() {
            imgEquipamiento.Enabled = true;
            imgEquipamiento.BackgroundImage = Properties.Resources.mochila;
        }
        private void deshabilitarMochila() {
            imgEquipamiento.Enabled = false;
            imgEquipamiento.BackgroundImage = Properties.Resources.mochilaOff;
            panelMochila.Visible = false;
            panelObjetos.Visible = false;
        }
        private void limpiarHabilidadesMarcadas() {
            // Para limpiar las habilidades marcadas al cambiar de personaje. 
            foreach (object cb2 in panelHabilidades.Controls) {
                if (cb2 is CheckBox)
                    if (((CheckBox)cb2).Checked)
                        ((CheckBox)cb2).Checked = false;
            }
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + Constantes.HABILIDADES_SELECCIONABLES;
        }
        private void tirarDado(object sender, EventArgs e) {
            String personaje; 
            // Si hay un personaje seleccionado y no se ha superado el numero de tiradas permitido: 
            if (!combClase.Text.Equals("") && numTirada < Constantes.MAX_TIRADAS || modoEdicion && numTiradaME < Constantes.MAX_TIRADAS) {
                timer2.Enabled = true;
                resetValoresAtrib(); // Pongo los atributos a 0. 
                resetFlechasAtributos();
                if (!modoEdicion)
                    personaje = combClase.SelectedItem.ToString(); // cojo el personaje seleccionado.
                else
                    personaje = album.personajeActual().getClase();
                obtenerValoresAleatorios(); // Relleno el array con valores aleatorios nuevamente. 
                asignarAtributosPlusPSJ(personaje); // Doy valor a los atributos en base al personaje, éste metodo añadirá la base aleatoria. 
                if (!modoEdicion)
                    numTirada++; // Incremento número de tirada. 
                else
                    numTiradaME++;
            }
            // Si el dado estaba habilidato y el número de tiradas llega al máximo permitido, apago el dado y cambio la imagen del dado 
            // para que aparezca deshabilitado. 
            if (!dadoApagado && (numTirada == Constantes.MAX_TIRADAS || numTiradaME == Constantes.MAX_TIRADAS)) {
                imgDado.BackgroundImage = Properties.Resources.dadoApagado;
                dadoApagado = true;
                imgDado.Enabled = false;
            }
        }
        private void obtenerValoresAleatorios() {
            // Relleno el array de valores aleatorios para los atributos. 
            for (int i = 0; i < valoresAtributosAleatorios.Length; i++)
                valoresAtributosAleatorios[i] = rnd.Next(Constantes.MIN_VALOR_ALEATORIO, Constantes.MAX_VALOR_ALEATORIO);
        }
        private void menuCarga(object sender, EventArgs e) { // Evento que lanza el timer1. Con este evento emulo una página de carga al inicio. 
            barraCarga.Value++; int num = 20;
            // Hago que la barra de carga vaya más rápido conforme avanza. 
            if (!carga1 && barraCarga.Value > 80) {
                timer1.Interval -= num/3;
                carga1 = true;
            } else if (!carga2 && barraCarga.Value > 60) {
                carga2 = true;
                timer1.Interval -= num/2;
            } else if (!carga3 && barraCarga.Value > 40) {
                carga3 = true;
                timer1.Interval -= num;
            } else if (!carga4 && barraCarga.Value > 10) {
                timer1.Interval -= (num*3);
                carga4 = true;
            }
            // Cuando la barra se completa, muestro la ficha. 
            if (barraCarga.Value == barraCarga.Maximum) {
                imgCerrar.Visible = true;
                menuSeleccion.Visible = true;
                timer1.Enabled = false;
                barraCarga.Enabled = false;
                barraCarga.Visible = false;
                lblMsgCarga.Visible = false;
                this.BackgroundImage = Properties.Resources.fondo;
            }
        }
        private void resetFlechasAtributos() {
            // Apago todas las flechas de decremento y enciendo todas las de incremento.
            // Además, les reseteo el Tag a su valor inicial y reestablezco los puntos a repartir.
            foreach (object pbAtributo in panelAtributos.Controls) {
                if (pbAtributo is PictureBox) {
                    ((PictureBox)pbAtributo).Tag = 0;
                    if (((PictureBox)pbAtributo).Name.StartsWith("d")) 
                        ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaIzqApagada;
                    else
                        ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaDer;
                }
            }
            ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB;
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
        }
        public static Image RotateImage(Image img, float rotationAngle) {
           //create an empty Bitmap image
           Bitmap bmp = new Bitmap(img.Width, img.Height);
           //turn the Bitmap into a Graphics object
           Graphics gfx = Graphics.FromImage(bmp);
           //now we set the rotation point to the center of our image
           gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
           //now rotate the image
           gfx.RotateTransform(rotationAngle);
           gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
           //set the InterpolationMode to HighQualityBicubic so to ensure a high
           //quality image once it is transformed to the specified size
           gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
           //now draw our new image onto the graphics object
           gfx.DrawImage(img, new Point(0, 0));
           //dispose of our Graphics object
           gfx.Dispose();
           //return the image
           return bmp;
        }
        private void timer2_Tick(object sender, EventArgs e) {
            if (aux < 100) {
                imgDado.BackgroundImage = RotateImage(imgDado.BackgroundImage, 15);
                aux++;
            } else {
                timer2.Enabled = false;
                aux = 0;
            }
        }
        private void comprobarSiRelleno(object sender, EventArgs e) {
            // Compruebo si los campos del panel de personaje están rellenos. Si es así, se habilitará el botón de guardado.
            // En caso contrario se deshabilita. 
            if (sender is ComboBox && ((ComboBox)sender).Equals(combRaza))
                combClase.SelectedItem = null;

            if (!combClase.Text.Equals("") && !txtNombreJugador.Text.Equals("") && !txtNombrePersonaje.Text.Equals("")) {
                imgSave.Enabled = true;
                imgSave.BackgroundImage = Properties.Resources.guardar;
            } else {
                imgSave.Enabled = false;
                imgSave.BackgroundImage = Properties.Resources.guardarOff;
            } 
        }
        private void guardarPersonaje(object sender, EventArgs e) {
            if (!album.personajesMismoNombre(txtNombrePersonaje.Text)) {
                int[] atributos = { pbVitalidad.Value, pbPercepcion.Value, pbDestreza.Value, pbFuerza.Value, pbIngenio.Value, pbCoraje.Value, pbCarisma.Value, pbIniciativa.Value, pbReflejos.Value, pbVelocidad.Value };
                bool[] habilidades = {cboxAbrCerr.Checked, cboxEsquivar.Checked, cboxSigilo.Checked, cboxDetMent.Checked, cboxPersuasion.Checked, cboxTrampasFosos.Checked, cboxOcultarse.Checked, cboxHurtar.Checked, cboxEscalar.Checked, cboxNadar.Checked, cboxEnganiar.Checked, cboxEquilibrio.Checked,
                                  cboxDisfrazarse.Checked, cboxSaltar.Checked, cboxPunteria.Checked, cboxPrimerosAux.Checked, cboxIntimidar.Checked, cboxInterrog.Checked, cboxLeerLabios.Checked};
                int[] tagsAtb = { (int)decVit.Tag, (int)decPerc.Tag, (int)decDest.Tag, (int)decFuer.Tag, (int)decIng.Tag, (int)decCor.Tag, (int)decCar.Tag, (int)decIni.Tag, (int)decRef.Tag, (int)decVel.Tag };
                string[] objetosMochila = new string[4];
                if (mObj1.BackgroundImage != null)
                    objetosMochila[0] = (string)mObj1.BackgroundImage.Tag;
                if (mObj2.BackgroundImage != null)
                    objetosMochila[1] = (string)mObj2.BackgroundImage.Tag;
                if (mObj3.BackgroundImage != null)
                    objetosMochila[2] = (string)mObj3.BackgroundImage.Tag;
                if (mObj4.BackgroundImage != null)
                    objetosMochila[3] = (string)mObj4.BackgroundImage.Tag;
                Personaje p = new Personaje(txtNombrePersonaje.Text, txtNombreJugador.Text, rbtnFemenino.Checked ? rbtnFemenino.Text : rbtnMasculino.Text, combRaza.SelectedItem.ToString(), combClase.SelectedItem.ToString(), atributos, tagsAtb, habilidades, numTirada, habPorSelect, ptosRepAtrib, objetosMochila);

                album.agregarPersonaje(p);

                if (album.cuantosPjHay() == 1) {
                    imgAlbum.BackgroundImage = Properties.Resources.album;
                    imgAlbum.Enabled = true;
                }

                ocultarPaginaNewPersonaje();
                actualizarFlechasDesplazamiento();
                numTirada = 0;
                imgDado.BackgroundImage = Properties.Resources.dado;
                imgDado.Enabled = true;
                dadoApagado = false;
                this.BackgroundImage = Properties.Resources.fondo;
                menuSeleccion.Visible = true;
            } else
                MessageBox.Show("Ya existe un personaje con ese nombre. Por favor, cambie el nombre del personaje.", "Error");
        }
        private void cargarObjetosEquipables(string personaje) {
            obj9.BackgroundImage = Properties.Resources.cuchillos;
            obj9.Tag = Properties.Resources.cuchillosOff;
            if (personaje.Equals("Guerrero")) {
                obj1.BackgroundImage = Properties.Resources.hacha;
                obj1.Tag= Properties.Resources.hachaOff;
                obj2.BackgroundImage = Properties.Resources.mazo;
                obj2.Tag = Properties.Resources.mazoOff;
                obj3.BackgroundImage = Properties.Resources.martillo;
                obj3.Tag = Properties.Resources.martilloOff;
                obj4.BackgroundImage = Properties.Resources.lanza;
                obj4.Tag = Properties.Resources.lanzaOff;
                obj5.BackgroundImage = Properties.Resources.espada2manos;
                obj5.Tag = Properties.Resources.espada2manosOff;
                obj6.BackgroundImage = Properties.Resources.armaduraP;
                obj6.Tag = Properties.Resources.armaduraPOff;
                obj7.BackgroundImage = Properties.Resources.escudo;
                obj7.Tag = Properties.Resources.escudoOff;
                obj8.BackgroundImage = Properties.Resources.espadaEd;
                obj8.Tag = Properties.Resources.espadaEdOff;
            } else if (personaje.Equals("Mago")) {
                obj1.BackgroundImage = Properties.Resources.baston;
                obj1.Tag = Properties.Resources.bastonOff;
                obj2.BackgroundImage = Properties.Resources.dagaMagica;
                obj2.Tag = Properties.Resources.dagaMagicaOff;
                obj3.BackgroundImage = Properties.Resources.dagaMagica2;
                obj3.Tag = Properties.Resources.dagaMagica2Off;
                obj4.BackgroundImage = Properties.Resources.eden1;
                obj4.Tag = Properties.Resources.eden1Off;
                obj5.BackgroundImage = Properties.Resources.eden2;
                obj5.Tag = Properties.Resources.eden2Off;
                obj6.BackgroundImage = Properties.Resources.espN1;
                obj6.Tag = Properties.Resources.espN1Off;
                obj7.BackgroundImage = Properties.Resources.espadaM;
                obj7.Tag = Properties.Resources.espadaMOff;
                obj8.BackgroundImage = Properties.Resources.armaduraL;
                obj8.Tag = Properties.Resources.armaduraLOff;
            } else if (personaje.Equals("Paladin")) {
                obj1.BackgroundImage = Properties.Resources.espada5;
                obj1.Tag = Properties.Resources.espada5Off;
                obj2.BackgroundImage = Properties.Resources.espadon;
                obj2.Tag = Properties.Resources.espadonOff;
                obj3.BackgroundImage = Properties.Resources.armaduraP;
                obj3.Tag = Properties.Resources.armaduraPOff;
                obj4.BackgroundImage = Properties.Resources.escudo;
                obj4.Tag  = Properties.Resources.escudoOff;
                obj5.BackgroundImage = Properties.Resources.espadaEd;
                obj5.Tag = Properties.Resources.espadaEdOff;
                obj6.BackgroundImage = Properties.Resources.espada6;
                obj6.Tag = Properties.Resources.espada6Off;
                obj7.BackgroundImage = Properties.Resources.espada8;
                obj7.Tag = Properties.Resources.espada8Off;
                obj8.BackgroundImage = Properties.Resources.espada7;
                obj8.Tag = Properties.Resources.espada7Off;
            } else if (personaje.Equals("Daguero")) {
                obj1.BackgroundImage = Properties.Resources.catana;
                obj1.Tag = Properties.Resources.catanaOff;
                obj2.BackgroundImage = Properties.Resources.cuchillo2;
                obj2.Tag = Properties.Resources.cuchillo2Off;
                obj3.BackgroundImage = Properties.Resources.dagaDorada;
                obj3.Tag = Properties.Resources.dagaDoradaOff;
                obj4.BackgroundImage = Properties.Resources.espadasDobles;
                obj4.Tag = Properties.Resources.espadasDoblesOff;
                obj5.BackgroundImage = Properties.Resources.daga1;
                obj5.Tag = Properties.Resources.daga1Off;
                obj6.BackgroundImage = Properties.Resources.bayoneta;
                obj6.Tag = Properties.Resources.bayonetaOff;
                obj7.BackgroundImage = Properties.Resources.daga;
                obj7.Tag = Properties.Resources.dagaOff;
                obj8.BackgroundImage = Properties.Resources.armaduraL;
                obj8.Tag = Properties.Resources.armaduraLOff;
            } else if (personaje.Equals("Nigromante")) {
                obj1.BackgroundImage = Properties.Resources.guadania;
                obj1.Tag = Properties.Resources.guadaniaOff;
                obj2.BackgroundImage = Properties.Resources.a1;
                obj2.Tag = Properties.Resources.a1Off;
                obj3.BackgroundImage = Properties.Resources.espN1;
                obj3.Tag = Properties.Resources.espN1Off;
                obj4.BackgroundImage = Properties.Resources.espN2;
                obj4.Tag = Properties.Resources.espN2Off;
                obj5.BackgroundImage = Properties.Resources.n1;
                obj5.Tag = Properties.Resources.n1Off;
                obj6.BackgroundImage = Properties.Resources.n2;
                obj6.Tag = Properties.Resources.n2Off;
                obj7.BackgroundImage = Properties.Resources.n3;
                obj7.Tag = Properties.Resources.n3Off;
                obj8.BackgroundImage = Properties.Resources.n4;
                obj8.Tag = Properties.Resources.n4Off;
            } else if (personaje.Equals("Cazador")) {
                obj1.BackgroundImage = Properties.Resources.ballesta;
                obj1.Tag = Properties.Resources.ballestaOff;
                obj2.BackgroundImage = Properties.Resources.ballesta2;
                obj2.Tag = Properties.Resources.ballesta2Off;
                obj3.BackgroundImage = Properties.Resources.espada1;
                obj3.Tag = Properties.Resources.espada1Off;
                obj4.BackgroundImage = Properties.Resources.cuchillo1;
                obj4.Tag = Properties.Resources.cuchillo1Off;
                obj5.BackgroundImage = Properties.Resources.esp1;
                obj5.Tag = Properties.Resources.esp1Off;
                obj6.BackgroundImage = Properties.Resources.esp2;
                obj6.Tag = Properties.Resources.esp2Off;
                obj7.BackgroundImage = Properties.Resources.hachaC1;
                obj7.Tag = Properties.Resources.hachaC1Off;
                obj8.BackgroundImage = Properties.Resources.armaduraL;
                obj8.Tag = Properties.Resources.armaduraLOff;
            } else if (personaje.Equals("Arquero")) {
                obj1.BackgroundImage = Properties.Resources.arco;
                obj1.Tag = Properties.Resources.arcoOff;
                obj2.BackgroundImage = Properties.Resources.arco1;
                obj2.Tag = Properties.Resources.arco1Off;
                obj3.BackgroundImage = Properties.Resources.arco2;
                obj3.Tag = Properties.Resources.arco2Off;
                obj4.BackgroundImage = Properties.Resources.arco3;
                obj4.Tag = Properties.Resources.arco3Off;
                obj5.BackgroundImage = Properties.Resources.arco4;
                obj5.Tag = Properties.Resources.arco4Off;
                obj6.BackgroundImage = Properties.Resources.arco5;
                obj6.Tag = Properties.Resources.arco5Off;
                obj7.BackgroundImage = Properties.Resources.arco6;
                obj7.Tag = Properties.Resources.arco6Off;
                obj8.BackgroundImage = Properties.Resources.armaduraL;
                obj8.Tag = Properties.Resources.armaduraLOff;
            }
        }
        private void obj_MouseDown(object sender, MouseEventArgs e) {
            PictureBox img = (PictureBox)sender; // Drag and drop. Aqui agarro el elemento. 
            img.DoDragDrop(img.Name, DragDropEffects.Copy);
        }
        private void mObj_DragEnter(object sender, DragEventArgs e) {
            if (((PictureBox)sender).BackgroundImage == null) // Si no hay una imagen ya en ese hueco de la mochila se podrá soltar la imagen.
                e.Effect = DragDropEffects.Copy; 
        }
        private void mObj_DragDrop(object sender, DragEventArgs e) {
            PictureBox img = (PictureBox)sender; bool centinela = false; string obj;
            obj = (string) e.Data.GetData(DataFormats.StringFormat);
            String prueba;
            
            // Busco entre los objetos equipables algun objeto igual al que he arrastrado a la mochila, cuando lo encuentro: 
            foreach (object o in panelObjetos.Controls) {
                if (!centinela && o is PictureBox)
                    if ((prueba = ((PictureBox)o).Name).Equals(obj)) {
                        img.BackgroundImage = ((PictureBox)o).BackgroundImage;
                        img.BackgroundImage.Tag = obj;
                        ((PictureBox)o).BackgroundImage = (Image)((PictureBox)o).Tag; // Cambio la imagen por la que guarda el tag de ese elemento, que es esa imagen pero apagada. 
                        ((PictureBox)o).Tag = img.BackgroundImage; // Guardo la imagen "encendida" en el tag. 
                        ((PictureBox)o).Enabled = false; // Deshabilito esa imagen para no poder hacer mas drag and drop. 
                        centinela = true;
                    }
            }
            if (modoEdicion)
                habilitarGuardadoME();
        }
        private void sacarObjMochila(object sender, EventArgs e) {
            // Saco el objeto de la mochila y enciendo la imagen en objetos equipables. Vuelvo a habilitar el picturebox. 
            PictureBox img = (PictureBox)sender; bool centinela = false; Image aux;
            foreach (object o in panelObjetos.Controls) {
                if (!centinela && o is PictureBox)
                    if (((PictureBox)o).Name.Equals((string)img.BackgroundImage.Tag)) {
                        aux = ((PictureBox)o).BackgroundImage; // Guardo en aux la imagen apagada. 
                        ((PictureBox)o).BackgroundImage = img.BackgroundImage; // Pongo la imagen del objeto encendida en objetos equipables. 
                        ((PictureBox)o).Tag = aux; // Vuelvo a meter la imagen apagada en el tag. 
                        ((PictureBox)o).Enabled = true; // vuelvo a habilitar el pictureBox para que me permita volver a hacer drag and drop. 
                        centinela = true;
                    }
            }
            img.BackgroundImage = null;
            img.Tag = "";// Saco el objeto de la mochila. 
            if (modoEdicion)
                habilitarGuardadoME();
        }
        private void vaciarMochila() { // Vacio la mochila cuando cambio de personaje. 
            mObj1.BackgroundImage = null;
            mObj1.Tag = "";
            mObj2.BackgroundImage = null;
            mObj2.Tag = "";
            mObj3.BackgroundImage = null;
            mObj3.Tag = "";
            mObj4.BackgroundImage = null;
            mObj4.Tag = "";
            foreach (object o in panelObjetos.Controls) {
                if (o is PictureBox)
                    ((PictureBox)o).Enabled = true;
            }
        }
        private void vaciarObjetosEquip() { // Vacio el inventario cuando cambio de raza. 
            foreach (object o in panelObjetos.Controls) {
                if (o is PictureBox) 
                    ((PictureBox)o).BackgroundImage = null;
            }
        }
        private void habilitarDragDrop() {
            mObj1.AllowDrop = true;
            mObj2.AllowDrop = true;
            mObj3.AllowDrop = true;
            mObj4.AllowDrop = true;
        }
        private void ocultarPaginaNewPersonaje() {
            limpiarPagNewPersonaje();
            panelPsj.Visible = false;
            panelAtributos.Visible = false;
            panelHabilidades.Visible = false;
            imgDado.Visible = false;
            imgSave.Visible = false;
            imgAtrasNP.Visible = false;
            imgPropiedades.Visible = false;
            imgEquipamiento.Visible = false;
            panelObjetos.Visible = false;
            panelMochila.Visible = false;
            habilitarMochila();
        }
        private void mostrarPaginaNewPersonaje() {
            limpiarPagNewPersonaje();
            panelPsj.Visible = true;
            panelAtributos.Visible = true;
            panelHabilidades.Visible = true;
            imgDado.Visible = true;
            imgSave.Visible = true;
            imgAtrasNP.Visible = true;
            imgPropiedades.Visible = true;
            imgEquipamiento.Visible = true;
            panelObjetos.Visible = false;
            panelMochila.Visible = false;
            deshabilitarMochila();
            this.BackgroundImage = Properties.Resources.fondo;
            habPorSelect = Constantes.HABILIDADES_SELECCIONABLES;
        }
        private void imgPropiedades_Click(object sender, EventArgs e) {
            panelObjetos.Visible = false;
            panelMochila.Visible = false;
            panelAtributos.Visible = true;
            panelHabilidades.Visible = true;
            if (modoEdicion) {
                if (numTiradaME < Constantes.MAX_TIRADAS) {
                    imgDado.Enabled = true;
                    imgDado.BackgroundImage = Properties.Resources.dado;
                }
            } else {
                if (numTirada < Constantes.MAX_TIRADAS) {
                    imgDado.Enabled = true;
                    imgDado.BackgroundImage = Properties.Resources.dado;
                }
            }
        }
        private void imgEquipamiento_Click(object sender, EventArgs e) {
            panelAtributos.Visible = false;
            panelHabilidades.Visible = false;
            panelMochila.Visible = true;
            panelObjetos.Visible = true;
            imgDado.Enabled = false;
            imgDado.BackgroundImage = Properties.Resources.dadoApagado;
        }
        private void volverMenuSelec(object sender, EventArgs e) {
            if (sender.Equals(imgAtrasNP)) 
                ocultarPaginaNewPersonaje();
            else if (sender.Equals(imgAtrasVP))
                panelVistaPersonaje.Visible = false;

            mostrarMenuSelec();
        }
        private void cargarPagNewPersonaje(object sender, EventArgs e) {
            menuSeleccion.Visible = false;
            mostrarPaginaNewPersonaje();
        }
        private void imgAlbum_Click(object sender, EventArgs e) {
            menuSeleccion.Visible = false;
            panelVistaPersonaje.Visible = true;

            cargarPersonajeModoVision(album.personajeActual());
        }
        private void cargarPersonajeModoVision(Personaje p) {
            string[] hab = dameNombresHab(p); string personaje; Image[] objetosMochila;
            lblNombPersMV.Text = Constantes.LBL_NOMBRE_PJ + p.getNombreP();
            lblNombJugMV.Text = Constantes.LBL_NOMBRE_JUG + p.getNombreJ();
            lblTipoPsjMV.Text = Constantes.LBL_TIPO + p.getRaza() + ", " + p.getClase();
            pbVitMV.Value = p.getAtributos()[0]; pbPercMV.Value = p.getAtributos()[1]; pbDestMV.Value = p.getAtributos()[2];
            pbFuerMV.Value = p.getAtributos()[3]; pbIngMV.Value = p.getAtributos()[4]; pbCorMV.Value = p.getAtributos()[5];
            pbCarMV.Value = p.getAtributos()[6]; pbIniMV.Value = p.getAtributos()[7]; pbRefMV.Value = p.getAtributos()[8];
            pbVelMV.Value = p.getAtributos()[9];
            hab1MV.Text = hab[0]; hab2MV.Text = hab[1]; hab3MV.Text = hab[2]; hab4MV.Text = hab[3];
            hab5MV.Text = hab[4]; hab6MV.Text = hab[5]; hab7MV.Text = hab[6]; hab8MV.Text = hab[7];
            cargarObjetosEquipables(p.getClase());
            objetosMochila = cargarObjetosEnMochila(p.getObjetosMochila());
            imgObj1MV.BackgroundImage = objetosMochila[0];
            imgObj2MV.BackgroundImage = objetosMochila[1];
            imgObj3MV.BackgroundImage = objetosMochila[2];
            imgObj4MV.BackgroundImage = objetosMochila[3];

            personaje = p.getClase();
            personaje = personaje.Substring(0, personaje.Length - 1);
            personaje = personaje.ToLower();
            if (!personaje.Equals("nigromant")) {
                if (p.getGenero().ToLower().Equals("femenino"))
                    personaje += "a";
                else
                    personaje += "o";
            }
            cargarImgPersonaje(personaje);
        }
        private string[] dameNombresHab(Personaje p) {
            string[] rs = new string[8]; int cont = 0;
            for (int i = 0; i < p.getHabilidades().Length; i++)
                if (p.getHabilidades()[i]) {
                    rs[cont] = getTextCheckboxHab(i);
                    cont++;
                }
            return rs;
        }
        private string getTextCheckboxHab(int num) {
            switch (num) {
                case 0:
                    return cboxAbrCerr.Text;
                case 1:
                    return cboxEsquivar.Text;
                case 2:
                    return cboxSigilo.Text;
                case 3:
                    return cboxDetMent.Text;
                case 4:
                    return cboxPersuasion.Text;
                case 5:
                    return cboxTrampasFosos.Text;
                case 6:
                    return cboxOcultarse.Text;
                case 7:
                    return cboxHurtar.Text;
                case 8:
                    return cboxEscalar.Text;
                case 9:
                    return cboxNadar.Text;
                case 10:
                    return cboxEnganiar.Text;
                case 11:
                    return cboxEquilibrio.Text;
                case 12:
                    return cboxDisfrazarse.Text;
                case 13:
                    return cboxSaltar.Text;
                case 14:
                    return cboxPunteria.Text;
                case 15:
                    return cboxPrimerosAux.Text;
                case 16:
                    return cboxIntimidar.Text;
                case 17:
                    return cboxInterrog.Text;
                case 18:
                    return cboxLeerLabios.Text;
                default:
                    return "";
            }
        }
        private void imgEdit_Click(object sender, EventArgs e) {
            panelVistaPersonaje.Visible = false;
            cargarModoEdicion();
        }
        private void eliminarPJ(object sender, EventArgs e) {
            DialogResult resp = MessageBox.Show("El personaje se eliminará, ¿seguro que desea continuar?", "Advertencia", MessageBoxButtons.YesNo);
            if (resp == System.Windows.Forms.DialogResult.Yes) {
                album.eliminarPj();
                actualizarFlechasDesplazamiento();
                if (album.vacio()) {
                    imgAlbum.Enabled = false;
                    imgAlbum.BackgroundImage = Properties.Resources.albumOff;
                    mostrarMenuSelec();
                    panelVistaPersonaje.Visible = false;
                } else
                    cargarPersonajeModoVision(album.personajeActual());
            }
        }
        private void pasarPaginaMV(object sender, EventArgs e) {
            Personaje p;
            if (((PictureBox)sender).Equals(imgSiguiente)) {
                if ((p = album.mostrarSiguientePj()) != null)
                    cargarPersonajeModoVision(p);
            } else {
                if ((p = album.mostrarAnteriorPj()) != null)
                    cargarPersonajeModoVision(p);
            }
            actualizarFlechasDesplazamiento();
        }
        private void actualizarFlechasDesplazamiento() {
            if (album.haySiguiente())
                imgSiguiente.Visible = true;
            else
                imgSiguiente.Visible = false;

            if (album.hayAnterior())
                imgAnterior.Visible = true;
            else
                imgAnterior.Visible = false;
        }
        private void limpiarPagNewPersonaje() {
            limpiarHabilidadesMarcadas();
            vaciarMochila();
            vaciarObjetosEquip();
            resetFlechasAtributos();
            resetValoresAtrib();
            txtNombreJugador.Text = "";
            txtNombrePersonaje.Text = "";
            combRaza.SelectedItem = null;
            deshabilitarHabilidades();
        }
        private void mostrarMenuSelec() {
            this.BackgroundImage = Properties.Resources.fondo;
            menuSeleccion.Visible = true;
        }
        private void cargarModoEdicion() {
            modoEdicion = true;
            imgSaveME.Visible = true;
            imgAtrasME.Visible = true;
            panelDatosPjME.Visible = true;
            panelAtributos.Visible = true;
            panelHabilidades.Visible = true;
            imgPropiedades.Visible = true;
            imgEquipamiento.Visible = true;
            imgDado.Visible = true;
            cargarPersonajeModoEdicion(album.personajeActual());
        }
        private void ocultarModoEdicion() {
            imgSaveME.Visible = false;
            imgAtrasME.Visible = false;
            panelDatosPjME.Visible = false;
            panelMochila.Visible = false;
            panelObjetos.Visible = false;
            panelAtributos.Visible = false;
            panelHabilidades.Visible = false;
            imgPropiedades.Visible = false;
            imgEquipamiento.Visible = false;
            imgDado.Visible = false;
            imgDado.Enabled = true;
            imgSaveME.Enabled = false;
            imgSaveME.BackgroundImage = Properties.Resources.guardarOff;
            if (numTirada == Constantes.MAX_TIRADAS) {
                imgDado.BackgroundImage = Properties.Resources.dadoApagado;
                imgDado.Enabled = false;
                dadoApagado = true;
            } else {
                imgDado.BackgroundImage = Properties.Resources.dado;
                imgDado.Enabled = true;
                dadoApagado = false;
            }
            if (modoEdicion)
                album.personajeActual().setNumTirada(numTiradaME);
            modoEdicion = false;
            numTiradaME = 0;
            vaciarMochila();
        }
        private void cargarPersonajeModoEdicion(Personaje p) {
            Image[] objetosMochila;
            lblNombrePME.Text = Constantes.LBL_NOMBRE_PJ + p.getNombreP();
            lblNombreJME.Text = Constantes.LBL_NOMBRE_JUG + p.getNombreJ();
            lblTipoME.Text = Constantes.LBL_TIPO + p.getRaza() + ", " + p.getClase();
            pbVitalidad.Value = p.getAtributos()[0]; pbPercepcion.Value = p.getAtributos()[1]; pbDestreza.Value = p.getAtributos()[2];
            pbFuerza.Value = p.getAtributos()[3]; pbIngenio.Value = p.getAtributos()[4]; pbCoraje.Value = p.getAtributos()[5];
            pbCarisma.Value = p.getAtributos()[6]; pbIniciativa.Value = p.getAtributos()[7]; pbReflejos.Value = p.getAtributos()[8];
            pbVelocidad.Value = p.getAtributos()[9];
            cargarObjetosEquipables(p.getClase());
            objetosMochila = cargarObjetosEnMochila(p.getObjetosMochila());
            mObj1.BackgroundImage = objetosMochila[0];
            mObj2.BackgroundImage = objetosMochila[1];
            mObj3.BackgroundImage = objetosMochila[2];
            mObj4.BackgroundImage = objetosMochila[3];
            cboxAbrCerr.Checked = p.getHabilidades()[0]; cboxEsquivar.Checked = p.getHabilidades()[1]; cboxSigilo.Checked = p.getHabilidades()[2]; cboxDetMent.Checked = p.getHabilidades()[3];
            cboxPersuasion.Checked = p.getHabilidades()[4]; cboxTrampasFosos.Checked = p.getHabilidades()[5]; cboxOcultarse.Checked = p.getHabilidades()[6]; cboxHurtar.Checked = p.getHabilidades()[7];
            cboxEscalar.Checked = p.getHabilidades()[8]; cboxNadar.Checked = p.getHabilidades()[9]; cboxEnganiar.Checked = p.getHabilidades()[10]; cboxEquilibrio.Checked = p.getHabilidades()[11];
            cboxDisfrazarse.Checked = p.getHabilidades()[12]; cboxSaltar.Checked = p.getHabilidades()[13]; cboxPunteria.Checked = p.getHabilidades()[14]; cboxPrimerosAux.Checked = p.getHabilidades()[15];
            cboxIntimidar.Checked = p.getHabilidades()[16]; cboxInterrog.Checked = p.getHabilidades()[17]; cboxLeerLabios.Checked = p.getHabilidades()[18];
            decVit.Tag = p.getTagsAtb()[0]; decPerc.Tag = p.getTagsAtb()[1]; decDest.Tag = p.getTagsAtb()[2]; decFuer.Tag = p.getTagsAtb()[3]; decIng.Tag = p.getTagsAtb()[4]; decCor.Tag = p.getTagsAtb()[5];
            decCar.Tag = p.getTagsAtb()[6]; decIni.Tag = p.getTagsAtb()[7]; decRef.Tag = p.getTagsAtb()[8]; decVel.Tag = p.getTagsAtb()[9];
            ptosRepAtrib = p.getPtosARepartirA();
            numTiradaME = p.getNumTirada();
            habPorSelect = p.getHabPorSeleccionar();
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            adaptarFlechasRepartoAtb();
            adaptarObjetosEquipables();
            if (habPorSelect > 0)
                habilitarHabilidades();
            if (numTiradaME == Constantes.MAX_TIRADAS) {
                imgDado.BackgroundImage = Properties.Resources.dadoApagado;
                imgDado.Enabled = false;
                dadoApagado = true;
            } else {
                imgDado.BackgroundImage = Properties.Resources.dado;
                imgDado.Enabled = true;
                dadoApagado = false;
            }
        }
        private void adaptarFlechasRepartoAtb() {
            foreach (object o in panelAtributos.Controls) {
                if (o is PictureBox)
                    if (((PictureBox)o).Name.StartsWith("d") && (int)((PictureBox)o).Tag > 0)
                        ((PictureBox)o).BackgroundImage = Properties.Resources.flechaIzq;
                    else if (((PictureBox)o).Name.StartsWith("i") && ptosRepAtrib == 0)
                        ((PictureBox)o).BackgroundImage = Properties.Resources.flechaDerApagada;

                if (o is ProgressBar)
                    if (((ProgressBar)o).Value == ((ProgressBar)o).Maximum)
                        switch (((ProgressBar)o).Name) {
                            case "pbCarisma":
                                incCar.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbCoraje":
                                incCor.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbDestreza":
                                incDest.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbFuerza":
                                incFuer.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbIngenio":
                                incIng.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbIniciativa":
                                incIni.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbPercepcion":
                                incPerc.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbReflejos":
                                incRef.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbVelocidad":
                                incVel.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                            case "pbVitalidad":
                                incVit.BackgroundImage = Properties.Resources.flechaDerApagada;
                                break;
                        }
            }
        }
        private void imgAtrasME_Click(object sender, EventArgs e) {
            ocultarModoEdicion();
            panelVistaPersonaje.Visible = true;
        }
        private void imgSaveME_Click(object sender, EventArgs e) {
            DialogResult resp = MessageBox.Show("El personaje se sobreescribirá, ¿desea continuar?", "Advertencia", MessageBoxButtons.YesNo);
            if (resp == System.Windows.Forms.DialogResult.Yes) {
                int[] atributos = { pbVitalidad.Value, pbPercepcion.Value, pbDestreza.Value, pbFuerza.Value, pbIngenio.Value, pbCoraje.Value, pbCarisma.Value, pbIniciativa.Value, pbReflejos.Value, pbVelocidad.Value };
                bool[] habilidades = {cboxAbrCerr.Checked, cboxEsquivar.Checked, cboxSigilo.Checked, cboxDetMent.Checked, cboxPersuasion.Checked, cboxTrampasFosos.Checked, cboxOcultarse.Checked, cboxHurtar.Checked, cboxEscalar.Checked, cboxNadar.Checked, cboxEnganiar.Checked, cboxEquilibrio.Checked,
                                  cboxDisfrazarse.Checked, cboxSaltar.Checked, cboxPunteria.Checked, cboxPrimerosAux.Checked, cboxIntimidar.Checked, cboxInterrog.Checked, cboxLeerLabios.Checked};
                string[] objetosMochila = new string[4];
                if (mObj1.BackgroundImage != null)
                    objetosMochila[0] = (string)mObj1.BackgroundImage.Tag;
                if (mObj2.BackgroundImage != null)
                    objetosMochila[1] = (string)mObj2.BackgroundImage.Tag;
                if (mObj3.BackgroundImage != null)
                    objetosMochila[2] = (string)mObj3.BackgroundImage.Tag;
                if (mObj4.BackgroundImage != null)
                    objetosMochila[3] = (string)mObj4.BackgroundImage.Tag;
                album.personajeActual().setAtributos(atributos);
                album.personajeActual().setHabilidades(habilidades);
                album.personajeActual().setObjetosMochila(objetosMochila);
                album.personajeActual().setPtosARepartirA(ptosRepAtrib);
                album.personajeActual().setHabPorSeleccionar(habPorSelect);
                album.personajeActual().setNumTirada(numTirada);
                cargarPersonajeModoVision(album.personajeActual());
                imgAtrasME_Click(sender, e);
            }
        }
        private void adaptarObjetosEquipables() {
            foreach (object equipado in panelMochila.Controls)
                if (equipado is PictureBox && ((PictureBox)equipado).Enabled) 
                    foreach (object o in panelObjetos.Controls) 
                        if (o is PictureBox && ((PictureBox)equipado).BackgroundImage != null && ((PictureBox)o).Name.Equals((string)((PictureBox)equipado).BackgroundImage.Tag)) {
                                ((PictureBox)o).BackgroundImage = (Image)((PictureBox)o).Tag; // Cambio la imagen por la que guarda el tag de ese elemento, que es esa imagen pero apagada. 
                                ((PictureBox)o).Tag = ((PictureBox)equipado).BackgroundImage; // Guardo la imagen "encendida" en el tag. 
                                ((PictureBox)o).Enabled = false; // Deshabilito esa imagen para no poder hacer mas drag and drop. 
                            }
        }
        private Image[] cargarObjetosEnMochila(string[] mochila) {
            Image[] items = new Image[4]; String aux;
            for (int i = 0; i < mochila.Length; i++)
                foreach (object o in panelObjetos.Controls)
                    if (o is PictureBox && ((PictureBox)o).Name.Equals(mochila[i])) {
                        items[i] = (Image)((PictureBox)o).BackgroundImage;
                        items[i].Tag = mochila[i];
                    }
            return items;
        }
        private void habilitarGuardadoME() {
            bool modificado = false;
            if (modoEdicion) {
                int[] atributos = { pbVitalidad.Value, pbPercepcion.Value, pbDestreza.Value, pbFuerza.Value, pbIngenio.Value, pbCoraje.Value, pbCarisma.Value, pbIniciativa.Value, pbReflejos.Value, pbVelocidad.Value };
                bool[] habilidades = {cboxAbrCerr.Checked, cboxEsquivar.Checked, cboxSigilo.Checked, cboxDetMent.Checked, cboxPersuasion.Checked, cboxTrampasFosos.Checked, cboxOcultarse.Checked, cboxHurtar.Checked, cboxEscalar.Checked, cboxNadar.Checked, cboxEnganiar.Checked, cboxEquilibrio.Checked,
                                  cboxDisfrazarse.Checked, cboxSaltar.Checked, cboxPunteria.Checked, cboxPrimerosAux.Checked, cboxIntimidar.Checked, cboxInterrog.Checked, cboxLeerLabios.Checked};
                string[] objetosMochila = new string[4];
                if (mObj1.BackgroundImage != null)
                    objetosMochila[0] = (string)mObj1.BackgroundImage.Tag;
                if (mObj2.BackgroundImage != null)
                    objetosMochila[1] = (string)mObj2.BackgroundImage.Tag;
                if (mObj3.BackgroundImage != null)
                    objetosMochila[2] = (string)mObj3.BackgroundImage.Tag;
                if (mObj4.BackgroundImage != null)
                    objetosMochila[3] = (string)mObj4.BackgroundImage.Tag;
                modificado = album.personajeActual().meHanModificado(habilidades, atributos, objetosMochila);
            }
            if (modificado) {
                imgSaveME.Enabled = true;
                imgSaveME.BackgroundImage = Properties.Resources.guardar;
            } else {
                imgSaveME.Enabled = false;
                imgSaveME.BackgroundImage = Properties.Resources.guardarOff;
            }
        }
        private void importarDesde_Click(object sender, EventArgs e) {
            OpenFileDialog dialogo = new OpenFileDialog();
            DialogResult resultado = dialogo.ShowDialog();
            if (resultado == DialogResult.OK) 
                if ((Regex.Match(dialogo.FileName, ".txt")).Length > 0) {
                    album.importarDesde(dialogo.FileName);
                    actualizarFlechasDesplazamiento();
                    if (!album.vacio()) {
                        imgAlbum.BackgroundImage = Properties.Resources.album;
                        imgAlbum.Enabled = true;
                    } else
                        imgAlbum.Enabled = false;
                }
        }
        private void exportarA_Click(object sender, EventArgs e) {
            SaveFileDialog dialogo = new SaveFileDialog(); string ruta;
            DialogResult resultado = dialogo.ShowDialog();
            if (resultado == DialogResult.OK) {
                ruta = dialogo.FileName;
                if ((Regex.Match(dialogo.FileName, ".txt")).Length == 0)
                    ruta += ".txt";

                album.exportarA(ruta);
            }
                
        }
    }
}
