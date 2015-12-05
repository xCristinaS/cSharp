using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WMPLib;

namespace CS_Ejercicio03_FichaDePersonajes {
    public partial class Form1 : Form {
        // Para mover el formulario. 
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        // Para cambiar el cursor. 
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);
        IntPtr cursor = LoadCursorFromFile(System.IO.Path.GetFullPath(@"../../Resources/cursor.cur"));
        // Para reproducir música. 
        WindowsMediaPlayer reproductor = new WindowsMediaPlayer();
        IWMPPlaylist playlist; IWMPMedia media;

        private string[] personajesMagicos = { Constantes.MAGO, Constantes.NIGROMANTE};
        string[] personajesMundanos = { Constantes.ARQUERO, Constantes.DAGUERO, Constantes.CAZADOR, Constantes.GUERRERO, Constantes.PALADIN};
        int[] valoresAtributosAleatorios = new int[10]; private bool dadoApagado = false;
        private int numTirada = 0, ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB, habPorSelect = Constantes.HABILIDADES_SELECCIONABLES, aux = 0;
        private Random rnd = new Random(); bool modoEdicion = false, carga1 = false, carga2 = false, carga3 = false, carga4 = false;
        private Album album = new Album(); int numTiradaME = 0;

        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            imgCerrar.Visible = false;
            mute.Visible = false;
            habilitarDragDrop();
            obtenerValoresAleatorios(); // Relleno el arrays de valores de los atributos con números aleatorios.
            deshabilitarHabilidades(); // Para que no se puedan marcar si no hay personaje seleccionado. 
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            resetFlechasAtributos();
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
            
            this.Cursor = new Cursor(cursor); // Cambio el cursor. 
            playlist = reproductor.playlistCollection.newPlaylist("myplaylist"); // Creo la lista.
            media = reproductor.newMedia(System.IO.Path.GetFullPath(@"../../Resources/songGOT.mp3")); // Agrego canción a media. 
            playlist.appendItem(media); // agrego canción a la lista.
            media = reproductor.newMedia(System.IO.Path.GetFullPath(@"../../Resources/songMGS2.mp3"));
            playlist.appendItem(media);
            media = reproductor.newMedia(System.IO.Path.GetFullPath(@"../../Resources/songLR.mp3"));
            playlist.appendItem(media);
            media = reproductor.newMedia(System.IO.Path.GetFullPath(@"../../Resources/songAR1.mp3"));
            playlist.appendItem(media);
            reproductor.currentPlaylist = playlist; // asigno la lista al reproductor. 
            reproductor.settings.setMode("loop", true); // para que vuelva a empezar cuando acabe.
            reproductor.controls.play(); // Doy al play! 
        }
        private void clicCerrar(object sender, EventArgs e) {
            this.Close();
            album.exportarPjs();
        }
        private void combRaza_Change(object sender, EventArgs e) {  
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
                imgPropiedades_Click(null, null);
                deshabilitarHabilidades();
                this.BackgroundImage = Properties.Resources.fondo;
            }
        } 
        private void actualizarImgPsj(object sender, EventArgs e) {
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
                    resetFlechasAtributos();
                }
                cargarImgPersonaje(identificarPersonaje(personaje)); // Cargo la imagen del personaje. 
            }
        }
        private string identificarPersonaje(string personaje) {
            personaje = personaje.Substring(0, personaje.Length - 1); // A la cadena del personaje seleccionado le quito el último carácter  
            personaje = personaje.ToLower(); // lo paso a minúsculas.
            if (!personaje.Equals("nigromant")) { // Si el personaje seleccionado es diferente al nigromante (nigromante no tiene genero)
                if (rbtnFemenino.Checked) // a la cadena personaje le agrego una "a".
                    personaje += "a";
                else // a la cadena personaje le agrego una "o".
                    personaje += "o";
            }
            return personaje;
        }
        private string identificarPersonaje(string personaje, string genero) {
            personaje = personaje.Substring(0, personaje.Length - 1); // A la cadena del personaje seleccionado le quito el último carácter  
            personaje = personaje.ToLower(); // lo paso a minúsculas.
            if (!personaje.Equals("nigromant")) { // Si el personaje seleccionado es diferente al nigromante (nigromante no tiene genero)
                if (genero.Equals("Femenino")) // a la cadena personaje le agrego una "a".
                    personaje += "a";
                else // a la cadena personaje le agrego una "o".
                    personaje += "o";
            }
            return personaje;
        }
        private void cargarImgPersonaje(string personaje) {
            this.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + personaje + Constantes.EXTENSION_PNG);
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
            pbCarisma.Value += valoresAtributosAleatorios[0]; pbCoraje.Value += valoresAtributosAleatorios[1]; pbDestreza.Value += valoresAtributosAleatorios[2]; pbFuerza.Value += valoresAtributosAleatorios[3];
            pbIngenio.Value += valoresAtributosAleatorios[4]; pbIniciativa.Value += valoresAtributosAleatorios[5]; pbPercepcion.Value += valoresAtributosAleatorios[6]; pbReflejos.Value += valoresAtributosAleatorios[7];
            pbVelocidad.Value += valoresAtributosAleatorios[8]; pbVitalidad.Value += valoresAtributosAleatorios[9];
            // Reestablezco los puntos a repartir, puesto que si he llegado aquí es porque se ha cambiado de personaje y
            // en ese caso, si se hizo un reparto de puntos para el personaje anterior los cambios deben ser suprimidos.
            ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB; lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
        }
        private void resetValoresAtrib() {
            pbCarisma.Value = 0; pbCoraje.Value = 0; pbDestreza.Value = 0; pbFuerza.Value = 0; pbIngenio.Value = 0; pbIniciativa.Value = 0;
            pbPercepcion.Value = 0; pbReflejos.Value = 0; pbVelocidad.Value = 0; pbVitalidad.Value = 0;
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
            if (sender.Equals(incVit) && pbVitalidad.Value < pbVitalidad.Maximum) 
                incrementarPb(pbVitalidad, decVit, incVit);
            else if (sender.Equals(incCar) && pbCarisma.Value < pbCarisma.Maximum)
                incrementarPb(pbCarisma, decCar, incCar);
            else if (sender.Equals(incCor) && pbCoraje.Value < pbCoraje.Maximum)
                incrementarPb(pbCoraje, decCor, incCor);
            else if (sender.Equals(incDest) && pbDestreza.Value < pbDestreza.Maximum)
                incrementarPb(pbDestreza, decDest, incDest);
            else if (sender.Equals(incFuer) && pbFuerza.Value < pbFuerza.Maximum) 
                incrementarPb(pbFuerza, decFuer, incFuer);
            else if (sender.Equals(incIng) && pbIngenio.Value < pbIngenio.Maximum) 
                incrementarPb(pbIngenio, decIng, incIng);
            else if (sender.Equals(incIni) && pbIniciativa.Value < pbIniciativa.Maximum) 
                incrementarPb(pbIniciativa, decIni, incIni);
            else if (sender.Equals(incPerc) && pbPercepcion.Value < pbPercepcion.Maximum)
                incrementarPb(pbPercepcion, decPerc, incPerc);
            else if (sender.Equals(incRef) && pbReflejos.Value < pbReflejos.Maximum) 
                incrementarPb(pbReflejos, decRef, incRef);
            else if (sender.Equals(incVel) && pbVelocidad.Value < pbVelocidad.Maximum)
                incrementarPb(pbVelocidad, decVel, incVel);
            
            if (ptosRepAtrib == 0) { // Cuando los puntos a repartir llegan a 0, apago las flechas de incremento. 
                foreach (object pbAtributo in panelAtributos.Controls)
                    if (pbAtributo is PictureBox)
                        if (((PictureBox)pbAtributo).Name.StartsWith("i"))
                            ((PictureBox)pbAtributo).BackgroundImage = Properties.Resources.flechaDerApagada;
            }
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib; // Actualizo los puntos a repartir. 
        }
        private void incrementarPb(ProgressBar barra, PictureBox btnDecremento, PictureBox btnIncremento) {
            barra.Value++;
            ptosRepAtrib--;
            btnDecremento.Tag = ((int)btnDecremento.Tag) + 1;
            btnDecremento.BackgroundImage = Properties.Resources.flechaIzq; // Activo la flecha de decremento (cambio su imagen). 
            if (barra.Value == barra.Maximum) { // Si tras incrementar la barra ha llegado al máximo, desactivo la flecha de incremento (cambio su imagen).
                btnIncremento.BackgroundImage = Properties.Resources.flechaDerApagada;
                btnIncremento.Tag = -1; // Meto un -1 para usarlo como centinela, cuando el valor del tag sea -1 significa que la barra esta llena. 
            }
        }
        private void decrementarAtributo(object sender) {
            // Evalúo que flecha de decremento fue seleccionada y decremento el valor de la progressbar asociada a dicha flecha.
            // Sólo podre decrementar el valor de la progressbar si el tag de la flecha de decremento fue previamente incrementado (esta 
            // comprobación se hace previamente, en el evento "repartirPtosAtb" que es el que se lanza al hacer clic en la flecha de decremento).
            if (sender.Equals(decVit)) 
                decrementarPb(pbVitalidad, decVit, incVit);
            else if (sender.Equals(decCar)) 
                decrementarPb(pbCarisma, decCar, incCar);
            else if (sender.Equals(decCor)) 
                decrementarPb(pbCoraje, decCor, incCor);
            else if (sender.Equals(decDest))
                decrementarPb(pbDestreza, decDest, incDest);
            else if (sender.Equals(decFuer))
                decrementarPb(pbFuerza, decFuer, incFuer);
            else if (sender.Equals(decIng)) 
                decrementarPb(pbIngenio, decIng, incIng);
            else if (sender.Equals(decIni)) 
                decrementarPb(pbIniciativa, decIni, incIni);
            else if (sender.Equals(decPerc))
                decrementarPb(pbPercepcion, decPerc, incPerc);
            else if (sender.Equals(decRef)) 
                decrementarPb(pbReflejos, decRef, incRef);
            else if (sender.Equals(decVel)) 
                decrementarPb(pbVelocidad, decVel, incVel);
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
        private void decrementarPb(ProgressBar barra, PictureBox btnDecremento, PictureBox btnIncremento) {
            barra.Value--;
            ptosRepAtrib++;
            btnDecremento.Tag = ((int)btnDecremento.Tag) - 1;
            btnIncremento.Tag = 0; // Para que el centinela (el tag) deje de indicar que la barra esta llena.
            btnIncremento.BackgroundImage = Properties.Resources.flechaDer; // Vuelvo a habilitar la flecha de incremento.
            if ((int)btnDecremento.Tag == 0) // Si el tag es 0, es porque ya he acabado con los puntos de decremento que tenía, en ese caso apago la flecha. 
                btnDecremento.BackgroundImage = Properties.Resources.flechaIzqApagada;
        }
        private void habilidadesCheckedChange(object sender, EventArgs e) {
            CheckBox cb = (CheckBox)sender; 
            if (cb.Checked) 
                habPorSelect--; // Si se ha seleccionado un checkbox decremento la cantidad de habilidades por seleccionar. 
            else
                habPorSelect++; // Si se ha seleccionado un checkbox incremento la cantidad de habilidades por seleccionar.

            if (habPorSelect == 0) { // Si ya se han seleccionado el máximo número de habilidades, deshabilito todas las que no estén marcadas. 
                foreach (object cb2 in panelHabilidades.Controls) 
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = false;
            } else { // Si quedan habilidades por seleccionar, habilito todas las que estén desmarcadas. 
                foreach (object cb2 in panelHabilidades.Controls) 
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = true;
            }
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            if (modoEdicion)
                habilitarGuardadoME();
        }
        private void habilitarHabilidades() {
            foreach (object cb2 in panelHabilidades.Controls) { // Para habilitar los checkbox cuando haya un personaje seleccionado. 
                if (cb2 is CheckBox)
                    ((CheckBox)cb2).Enabled = true;
            }
        }
        private void deshabilitarHabilidades() {
            foreach (object cb2 in panelHabilidades.Controls) { // Para deshabilitar los checkbox cuando no hay personaje seleccionado. 
                if (cb2 is CheckBox)
                        ((CheckBox)cb2).Enabled = false;
                }
            }
        private void habilitarMochila() {
            imgEquipamiento.Enabled = true; // Habilito la mochila cuando haya personaje seleccionado.
            imgEquipamiento.BackgroundImage = Properties.Resources.mochila;
        }
        private void deshabilitarMochila() {
            imgEquipamiento.Enabled = false; // Deshabilito la mochila cuando no haya personaje seleccionado.
            imgEquipamiento.BackgroundImage = Properties.Resources.mochilaOff;
            panelMochila.Visible = false;
            panelObjetos.Visible = false;
        }
        private void limpiarHabilidadesMarcadas() {
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
                timer2.Enabled = true; // Para poder girar el dado. 
                resetValoresAtrib(); // Pongo los atributos a 0. 
                resetFlechasAtributos();
                if (!modoEdicion)
                    personaje = combClase.SelectedItem.ToString(); // cojo el personaje seleccionado de la vista nuevoPersonaje.
                else
                    personaje = album.personajeActual().getClase(); // cojo el personaje seleccionado de la vista de personajes.
                obtenerValoresAleatorios(); // Relleno el array con valores aleatorios nuevamente. 
                asignarAtributosPlusPSJ(personaje); // Doy valor a los atributos en base al personaje, éste metodo añadirá la base aleatoria. 
                if (!modoEdicion) // Incremento número de tirada.
                    numTirada++; 
                else
                    numTiradaME++;
            }
            // Si el dado estaba habilidato y el número de tiradas llega al máximo permitido, apago el dado y cambio la imagen del dado 
            // para que aparezca deshabilitado. 
            if (!dadoApagado && (numTirada == Constantes.MAX_TIRADAS || numTiradaME == Constantes.MAX_TIRADAS))
                deshabilitarDado();
        }
        private void habilitarDado() { 
            imgDado.BackgroundImage = Properties.Resources.dado;
            dadoApagado = false;
            imgDado.Enabled = true;
        }
        private void deshabilitarDado() {
            imgDado.BackgroundImage = Properties.Resources.dadoApagado;
            dadoApagado = true;
            imgDado.Enabled = false;
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
            // Cuando la barra se completa, muestro el menú de selección. 
            if (barraCarga.Value == barraCarga.Maximum) {
                imgCerrar.Visible = true;
                mute.Visible = true;
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
        public static Image RotateImage(Image img, float rotationAngle) { // Método para girar imagenes. 
           Bitmap bmp = new Bitmap(img.Width, img.Height);
           Graphics gfx = Graphics.FromImage(bmp);
           gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
           gfx.RotateTransform(rotationAngle);
           gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
           gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
           gfx.DrawImage(img, new Point(0, 0));
           gfx.Dispose();
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
            if (!album.personajesMismoNombre(txtNombrePersonaje.Text)) { // Si no existe ya un personaje con el mismo nombre, guardo el personaje. 
                int[] tagsAtb = { (int)decVit.Tag, (int)decPerc.Tag, (int)decDest.Tag, (int)decFuer.Tag, (int)decIng.Tag, (int)decCor.Tag, (int)decCar.Tag, (int)decIni.Tag, (int)decRef.Tag, (int)decVel.Tag };
                // Creo un personaje recogiendo la información de la ficha newPesonaje. 
                Personaje p = new Personaje(txtNombrePersonaje.Text, txtNombreJugador.Text, rbtnFemenino.Checked ? rbtnFemenino.Text : rbtnMasculino.Text, combRaza.SelectedItem.ToString(), combClase.SelectedItem.ToString(), recogerValoresAtributos(), tagsAtb, recogerValoresHabilidades(), numTirada, habPorSelect, ptosRepAtrib, recogerObjetosMochila());
                album.agregarPersonaje(p); // agrego el personaje creado al álbum. 
                if (album.cuantosPjHay() == 1) { // Si después de guardar el número de personajes es igual a 1, habilito el modo vista de personajes. 
                    imgAlbum.BackgroundImage = Properties.Resources.album;
                    imgAlbum.Enabled = true;
                }
                ocultarPaginaNewPersonaje();
                actualizarFlechasDesplazamiento();
                numTirada = 0; habilitarDado();
                this.BackgroundImage = Properties.Resources.fondo;
                menuSeleccion.Visible = true;
            } else 
                MessageBox.Show("Ya existe un personaje con ese nombre. Por favor, cambie el nombre del personaje.", "Error");
        }
        private int[] recogerValoresAtributos() { // Recojo los valores de los atributos y los meto en un array, para pasarselos a un personaje. 
            int[] atributos = { pbVitalidad.Value, pbPercepcion.Value, pbDestreza.Value, pbFuerza.Value, pbIngenio.Value, pbCoraje.Value, pbCarisma.Value, pbIniciativa.Value, pbReflejos.Value, pbVelocidad.Value };
            return atributos;
        }
        private bool[] recogerValoresHabilidades() { // Recojo los valores de las habilidades y los almaceno en un array, para pasarselo a un personaje. 
            bool[] habilidades = {cboxAbrCerr.Checked, cboxEsquivar.Checked, cboxSigilo.Checked, cboxDetMent.Checked, cboxPersuasion.Checked, cboxTrampasFosos.Checked, cboxOcultarse.Checked, cboxHurtar.Checked, cboxEscalar.Checked, cboxNadar.Checked, cboxEnganiar.Checked, cboxEquilibrio.Checked,
                                  cboxDisfrazarse.Checked, cboxSaltar.Checked, cboxPunteria.Checked, cboxPrimerosAux.Checked, cboxIntimidar.Checked, cboxInterrog.Checked, cboxLeerLabios.Checked};
            return habilidades;
        }
        private string[] recogerObjetosMochila() { // Recojo los objetos de la mochila y los almaceno en un array, para pasarselos a un personaje. 
            string[] objetosMochila = new string[4];
            if (mObj1.BackgroundImage != null) objetosMochila[0] = (string)mObj1.BackgroundImage.Tag;
            if (mObj2.BackgroundImage != null) objetosMochila[1] = (string)mObj2.BackgroundImage.Tag;
            if (mObj3.BackgroundImage != null) objetosMochila[2] = (string)mObj3.BackgroundImage.Tag;
            if (mObj4.BackgroundImage != null) objetosMochila[3] = (string)mObj4.BackgroundImage.Tag;
            return objetosMochila;
        }
        private void cargarObjetosEquipables(string personaje) {
            // Cargo los objetos equipables según el tipo de personaje seleccionado. 
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
            
            foreach (object o in panelObjetos.Controls) { // recorro los objetos del inventario. 
                if (!centinela && o is PictureBox) // si "o" es un picturebox y el centinela no ha saltado
                    if (((PictureBox)o).Name.Equals(obj)) { // si el nombre del pictureBox es el "string" con el nombre que capturo el ratón al hacer mouseDown
                        img.BackgroundImage = ((PictureBox)o).BackgroundImage; // cojo la imagen del inventario y se la pongo a la mochila
                        img.BackgroundImage.Tag = obj; // meto el nombre del objeto del inventario del que he cogido la imagen en el tag de la imagen
                        ((PictureBox)o).BackgroundImage = (Image)((PictureBox)o).Tag;  // cambio la imagen del objeto del inventario por la de su tag, que contiene la apagada
                        ((PictureBox)o).Tag = img.BackgroundImage;  // meto en el tag la imagen encendida, que ya la tiene el hueco de la mochila. 
                        ((PictureBox)o).Enabled = false; // deshabilito el mouseDown del objeto del inventario
                        centinela = true; // hago saltar al centinela para que no busque mas
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
        private void vaciarMochila() { // Vacio la mochila cuando cambio de personaje y habilito los objetos seleccionables. 
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
            panelObjetos.Visible = false; // Oculto inventario. 
            panelMochila.Visible = false; // Oculto mochila. 
            panelAtributos.Visible = true; // Muestro atributos. 
            panelHabilidades.Visible = true; // Muestro habilidades. 
            if (modoEdicion) { // Si estoy en modo edición
                if (numTiradaME < Constantes.MAX_TIRADAS) habilitarDado(); // y me quedan tiradas habilito el dado. 
            } else // Si no estoy en modo edición
                if (numTirada < Constantes.MAX_TIRADAS) habilitarDado(); // y tengo tiradas disponibles habilito el dado. 
        }
        private void imgEquipamiento_Click(object sender, EventArgs e) {
            panelAtributos.Visible = false; // oculto los atributos. 
            panelHabilidades.Visible = false; // oculto habilidades. 
            panelMochila.Visible = true; // muestro mochila. 
            panelObjetos.Visible = true; // muestro inventario. 
            deshabilitarDado(); // deshabilito el dado. 
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
        private string[] dameNombresHab(Personaje p) { 
            string[] resp = new string[8]; int cont = 0;
            for (int i = 0; i < p.getHabilidades().Length; i++) // Recorro el array de habilidades del personaje. 
                if (p.getHabilidades()[i]) { // Si la habilidad está marcada:
                    resp[cont] = getTextCheckboxHab(i); // cojo la descripción de esa habilidad. 
                    cont++;
                }
            return resp;
        }
        private string getTextCheckboxHab(int num) {
            switch (num) { // devuelvo la descripción de la habilidad.
                case 0: return cboxAbrCerr.Text;
                case 1: return cboxEsquivar.Text;
                case 2: return cboxSigilo.Text;
                case 3: return cboxDetMent.Text;
                case 4: return cboxPersuasion.Text;
                case 5: return cboxTrampasFosos.Text;
                case 6: return cboxOcultarse.Text;
                case 7: return cboxHurtar.Text;
                case 8: return cboxEscalar.Text;
                case 9: return cboxNadar.Text;
                case 10: return cboxEnganiar.Text;
                case 11: return cboxEquilibrio.Text;
                case 12: return cboxDisfrazarse.Text;
                case 13: return cboxSaltar.Text;
                case 14: return cboxPunteria.Text;
                case 15: return cboxPrimerosAux.Text;
                case 16: return cboxIntimidar.Text;
                case 17: return cboxInterrog.Text;
                case 18: return cboxLeerLabios.Text;
                default: return "";
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
                if (album.vacio()) { // Si después de eliminar el personaje el album se queda vacio deshabilito el album (modo vista personaje) y vuelvo al menú de selección.
                    imgAlbum.Enabled = false;
                    imgAlbum.BackgroundImage = Properties.Resources.albumOff;
                    mostrarMenuSelec();
                    panelVistaPersonaje.Visible = false;
                } else // Si no está vacío vuelvo a cargar otro personaje. 
                    cargarPersonajeModoVision(album.personajeActual());
            }
        }
        private void pasarPaginaMV(object sender, EventArgs e) {
            Personaje p;
            if (((PictureBox)sender).Equals(imgSiguiente)) { // Si se pulsa en la flecha de siguiente personaje
                if ((p = album.mostrarSiguientePj()) != null) // y hay siguiente personaje
                    cargarPersonajeModoVision(p); // lo cargo en el modo visión.
            } else { // Si se pulsa en la flecha de personaje anterior
                if ((p = album.mostrarAnteriorPj()) != null) // y hay personaje anterior
                    cargarPersonajeModoVision(p); // lo cargo en el modo visión. 
            }
            actualizarFlechasDesplazamiento();
        }
        private void actualizarFlechasDesplazamiento() {
            if (album.haySiguiente()) // Si hay siguiente personaje, muestro la flecha 
                imgSiguiente.Visible = true;
            else // en caso contrario la oculto. 
                imgSiguiente.Visible = false;
            if (album.hayAnterior()) // Si hay un personaje anterior muestro la flecha
                imgAnterior.Visible = true;
            else // en caso contrario la oculto.
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
            modoEdicion = true; imgSaveME.Visible = true; imgAtrasME.Visible = true; panelDatosPjME.Visible = true;
            panelAtributos.Visible = true; panelHabilidades.Visible = true; imgPropiedades.Visible = true;
            imgEquipamiento.Visible = true; imgDado.Visible = true;
            cargarPersonajeModoEdicion(album.personajeActual());
        }
        private void ocultarModoEdicion() { // oculto todos los elementos correspondientes al modo edición. 
            imgSaveME.Visible = false; imgAtrasME.Visible = false; panelDatosPjME.Visible = false; panelMochila.Visible = false;
            panelObjetos.Visible = false; panelAtributos.Visible = false; panelHabilidades.Visible = false; imgPropiedades.Visible = false;
            imgEquipamiento.Visible = false; imgDado.Visible = false; imgDado.Enabled = true; imgSaveME.Enabled = false;
            imgSaveME.BackgroundImage = Properties.Resources.guardarOff;
            if (numTirada == Constantes.MAX_TIRADAS) // si no tengo tiradas
                deshabilitarDado(); // deshabilito el dado. 
            else // en caso contrario
                habilitarDado(); // lo habilito. 
            if (modoEdicion) // si se oculta el modo edición pero estaba previamente en él
                album.personajeActual().setNumTirada(numTiradaME); // almaceno el número de tiradas por si se han modificado. 
            modoEdicion = false; numTiradaME = 0; vaciarMochila(); 
        }
        private void cargarPersonajeModoVision(Personaje p) {
            string[] hab = dameNombresHab(p); // cargo todos los datos del personaje que se muestran en la vista de personaje. 
            cargarDatoPersonaje(p, lblNombPersMV, lblNombJugMV, lblTipoPsjMV, pbVitMV, pbPercMV, pbDestMV, pbFuerMV, pbIngMV, pbCorMV, pbCarMV, pbIniMV, pbRefMV, pbVelMV, imgObj1MV, imgObj2MV, imgObj3MV, imgObj4MV);
            hab1MV.Text = hab[0]; hab2MV.Text = hab[1]; hab3MV.Text = hab[2]; hab4MV.Text = hab[3];
            hab5MV.Text = hab[4]; hab6MV.Text = hab[5]; hab7MV.Text = hab[6]; hab8MV.Text = hab[7];
            cargarImgPersonaje(identificarPersonaje(p.getClase(), p.getGenero()));
        }
        private void cargarPersonajeModoEdicion(Personaje p) { // cargo todos los datos del personaje que se muestran en el modo edición. 
            cargarDatoPersonaje(p, lblNombrePME, lblNombreJME, lblTipoME, pbVitalidad, pbPercepcion, pbDestreza, pbFuerza, pbIngenio, pbCoraje, pbCarisma, pbIniciativa, pbReflejos, pbVelocidad, mObj1, mObj2, mObj3, mObj4);
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
            adaptarFlechasRepartoAtbModoEdicion();
            adaptarObjetosEquipables();
            if (habPorSelect > 0)
                habilitarHabilidades();
            if (numTiradaME == Constantes.MAX_TIRADAS)
                deshabilitarDado();
            else 
                habilitarDado();
        }
        private void cargarDatoPersonaje(Personaje p, Label nombreP, Label nombreJ, Label tipo, ProgressBar vitalidad, ProgressBar percepcion, ProgressBar destreza, ProgressBar fuerza, ProgressBar ingenio, ProgressBar coraje, ProgressBar carisma, ProgressBar iniciativa, ProgressBar reflejos, ProgressBar velocidad, PictureBox objeto1, PictureBox objeto2, PictureBox objeto3, PictureBox objeto4) {
            Image[] objetosMochila; // Cargo los datos del personaje comunes al modo edición y al modo visión. 
            nombreP.Text = Constantes.LBL_NOMBRE_PJ + p.getNombreP();
            nombreJ.Text = Constantes.LBL_NOMBRE_JUG + p.getNombreJ();
            tipo.Text = Constantes.LBL_TIPO + p.getRaza() + ", " + p.getClase();
            vitalidad.Value = p.getAtributos()[0]; percepcion.Value = p.getAtributos()[1]; destreza.Value = p.getAtributos()[2]; fuerza.Value = p.getAtributos()[3]; ingenio.Value = p.getAtributos()[4]; coraje.Value = p.getAtributos()[5];
            carisma.Value = p.getAtributos()[6]; iniciativa.Value = p.getAtributos()[7]; reflejos.Value = p.getAtributos()[8]; velocidad.Value = p.getAtributos()[9];
            cargarObjetosEquipables(p.getClase());
            objetosMochila = cargarObjetosEnMochila(p.getObjetosMochila());
            objeto1.BackgroundImage = objetosMochila[0]; objeto2.BackgroundImage = objetosMochila[1]; objeto3.BackgroundImage = objetosMochila[2]; objeto4.BackgroundImage = objetosMochila[3];
        }
        private void adaptarFlechasRepartoAtbModoEdicion() {
            foreach (object o in panelAtributos.Controls) { // Recorro los controles del panel de atributos. 
                if (o is PictureBox)
                    if (((PictureBox)o).Name.StartsWith("d") && (int)((PictureBox)o).Tag > 0) // si "o" es una flecha de decremento y su tag es > 0
                        ((PictureBox)o).BackgroundImage = Properties.Resources.flechaIzq; // significa que puedo decrementar, y enciendo la imagen. 
                    else if (((PictureBox)o).Name.StartsWith("i") && ptosRepAtrib == 0) // si "o" es una flecha de incremento y los puntos a repartir son 0
                        ((PictureBox)o).BackgroundImage = Properties.Resources.flechaDerApagada; // apago la flecha porque no puedo incrementar. 

                if (o is ProgressBar) 
                    if (((ProgressBar)o).Value == ((ProgressBar)o).Maximum) // si "o" es una barra y está al máximo
                        switch (((ProgressBar)o).Name) { // apago la flecha de incremento asociada a esa barra. 
                            case "pbCarisma": incCar.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbCoraje": incCor.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbDestreza": incDest.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbFuerza": incFuer.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbIngenio": incIng.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbIniciativa": incIni.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbPercepcion": incPerc.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbReflejos": incRef.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbVelocidad": incVel.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                            case "pbVitalidad": incVit.BackgroundImage = Properties.Resources.flechaDerApagada; break;
                        }
            }
        }
        private void imgAtrasME_Click(object sender, EventArgs e) { // Si salgo del modo edición vuelvo a la vista de personajes. 
            ocultarModoEdicion();
            panelVistaPersonaje.Visible = true;
        }
        private void imgSaveME_Click(object sender, EventArgs e) {
            DialogResult resp = MessageBox.Show("El personaje se sobreescribirá, ¿desea continuar?", "Advertencia", MessageBoxButtons.YesNo);
            if (resp == System.Windows.Forms.DialogResult.Yes) { // Si la respuesta es "sí" se sobreescribirá el personaje. 
                album.personajeActual().setAtributos(recogerValoresAtributos()); // recojo los valores de los atributos y se los asigno al personaje actual.
                album.personajeActual().setHabilidades(recogerValoresHabilidades()); // recojo los valores de las habilidades y se los asigno al personaje actual.
                album.personajeActual().setObjetosMochila(recogerObjetosMochila()); // recojo los objetos de la mochila y se los asigno al personaje actual.
                album.personajeActual().setPtosARepartirA(ptosRepAtrib); // sustituyo los puntos a repartir por los actuales. 
                album.personajeActual().setHabPorSeleccionar(habPorSelect); // sustituyo las habilidades por seleccionar por las actuales. 
                album.personajeActual().setNumTirada(numTirada); // sustituyo el numero de tiradas disponibles. 
                cargarPersonajeModoVision(album.personajeActual()); // cargo el personaje actual en modo visión. 
                imgAtrasME_Click(sender, e); // vuelvo al modo visión. 
            }
        }

        private void panelVistaPersonaje_Paint(object sender, PaintEventArgs e) {

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
            Image[] items = new Image[4];
            for (int i = 0; i < mochila.Length; i++) // recorro los nombres de los objetos que tiene el personaje equipado en mochila
                foreach (object o in panelObjetos.Controls) // recorro los objetos del inventario
                    if (o is PictureBox && ((PictureBox)o).Name.Equals(mochila[i])) { // si el nombre del obj del inventario es igual al que fue equipado en la mochila
                        items[i] = (Image)((PictureBox)o).BackgroundImage; // meto la imagen del objeto del inventario en el array de imagenes
                        items[i].Tag = mochila[i]; // como tag le agrego el nombre del pictureBox que contenia la imagen en el inventario. 
                    }
            return items;
        }
        private void habilitarGuardadoME() {
            bool modificado = false;
            if (modoEdicion) // Si estoy en modo edición compruebo si al personaje actual se le ha modificado algo. 
                modificado = album.personajeActual().meHanModificado(recogerValoresHabilidades(), recogerValoresAtributos(), recogerObjetosMochila());
            if (modificado) { // Si se ha modificado habilito la opción de guardar y sobreescribir el personaje. 
                imgSaveME.Enabled = true;
                imgSaveME.BackgroundImage = Properties.Resources.guardar;
            } else { // en caso contrario deshabilito la opción de guardar y sobreescribir el personaje. 
                imgSaveME.Enabled = false;
                imgSaveME.BackgroundImage = Properties.Resources.guardarOff;
            }
        }
        private void importarDesde_Click(object sender, EventArgs e) {
            OpenFileDialog dialogo = new OpenFileDialog();
            DialogResult resultado = dialogo.ShowDialog();
            if (resultado == DialogResult.OK) 
                if ((Regex.Match(dialogo.FileName, ".txt")).Length > 0) { // si el archivo seleccionado tiene extension .txt
                    album.importarDesde(dialogo.FileName); // importo
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
        private void mute_Click(object sender, EventArgs e) {
            if (reproductor.settings.mute) {
                reproductor.settings.mute = false;
                mute.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + "music.png");
            } else {
                reproductor.settings.mute = true;
                mute.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + "musicOff.png");
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e) { // Para mover el formulario. 
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
