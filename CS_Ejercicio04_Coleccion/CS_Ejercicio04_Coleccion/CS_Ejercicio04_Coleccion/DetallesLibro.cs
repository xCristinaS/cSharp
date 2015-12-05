using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CS_Ejercicio04_Coleccion {
    public partial class DetallesLibro : Form {

        private string usuario;
        private string libro;

        public DetallesLibro(string usuario, string libro) {
            InitializeComponent();
            this.usuario = usuario;
            this.libro = libro;
        }

        private void DetallesLibro_Load(object sender, EventArgs e) {
            CenterToScreen();
            cargarLibro();
            panelSinopsis.AutoScroll = true;
            imgCerrar.BackgroundImage = Image.FromFile(Constantes.BOTON_CERRAR);
            leerLibro.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + "ojo.png");
        }

        private void cargarLibro() {
            string select = string.Format("select autor from libro where titulo = '{0}'", libro);
            SqlConnection conexion = BddConection.newConnection(); bool ponComa = false;
            SqlCommand orden = new SqlCommand(select, conexion); 
            SqlDataReader datos = orden.ExecuteReader();
            titulo.Text = libro;
            if (datos.Read())
                autor.Text = datos.GetString(0);
            datos.Close();
            select = string.Format("select imagenPortada from libro where titulo = '{0}'", libro);
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                portada.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + datos.GetString(0) + Constantes.EXT_JPG);
            datos.Close();
            select = string.Format("select genero from LibroGenero where titulo = '{0}'", libro);
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            while (datos.Read()) {
                if (!ponComa) {
                    genero.Text = datos.GetString(0);
                    this.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero.Text + Constantes.DETALLES_EXT_PNG);
                    ponComa = true;
                } else
                    genero.Text += ", " + datos.GetString(0);
            }
            datos.Close();
            select = string.Format("select sipnosis from libro where titulo = '{0}'", libro);
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                sipnosis.Text = (string)datos.GetString(0);
            datos.Close();
            select = string.Format("select count(*) from librousu where titulo = '{0}'", libro);
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read()) {
                if (datos.GetInt32(0) == 0) {
                    habilitarCompra();
                    leerLibro.Visible = false;
                } else {
                    habilitarEliminacion();
                    leerLibro.Visible = true;
                }
            }
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void habilitarCompra() {
            Image img;
            img = Image.FromFile(Constantes.IMG_COMPRAR);
            img.Tag = true;
            imgComprarVender.BackgroundImage = img;
        }

        private void habilitarEliminacion() {
            Image img;
            img = Image.FromFile(Constantes.PAPELERA);
            img.Tag = false;
            imgComprarVender.BackgroundImage = img;
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void imgComprarVender_Click(object sender, EventArgs e) {
            if ((bool)imgComprarVender.BackgroundImage.Tag) {
                agregarLibroAMiColeccion();
                leerLibro.Visible = true;
            } else {
                eliminarLibroDeMiColeccion();
                leerLibro.Visible = false;
            }
        }

        private void agregarLibroAMiColeccion() {
            string select = string.Format("insert into libroUsu Values ('{0}', '{1}')", usuario, libro);
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
            habilitarEliminacion();
        }

        private void eliminarLibroDeMiColeccion() {
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden;
            string select = string.Format("delete from libroUsu where titulo = '{0}' and nick = '{1}';", libro, usuario);
            orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
            habilitarCompra();
        }

        private void leerLibro_Click(object sender, EventArgs e) {
            this.Hide();
            LeerLibro form3 = new LeerLibro(libro);
            form3.FormClosed += (s, args) => this.Show();
            form3.Show();
        }
    }
}
