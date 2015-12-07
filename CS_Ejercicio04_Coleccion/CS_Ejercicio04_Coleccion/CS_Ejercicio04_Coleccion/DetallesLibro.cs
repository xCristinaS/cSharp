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
            string select = string.Format("select autor from libro where titulo = '{0}'", libro); // select que me devuelve el autor del libro.
            SqlConnection conexion = BddConection.newConnection(); bool ponComa = false;
            SqlCommand orden = new SqlCommand(select, conexion); 
            SqlDataReader datos = orden.ExecuteReader(); // ejecuto select.
            titulo.Text = libro; // pongo el titulo del libro.
            if (datos.Read()) // si la select me da resultados.
                autor.Text = datos.GetString(0); // lo leo y asigno el autor. 
            datos.Close(); 
            select = string.Format("select imagenPortada from libro where titulo = '{0}'", libro); // selecciono la portada.
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                portada.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + datos.GetString(0) + Constantes.EXT_JPG); // cargo la portada. 
            datos.Close();
            select = string.Format("select genero from LibroGenero where titulo = '{0}'", libro); // selecciono el/los géneros.
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            while (datos.Read()) {
                if (!ponComa) {
                    genero.Text = datos.GetString(0); // escribo el género en el textbox. 
                    this.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero.Text + Constantes.DETALLES_EXT_PNG); // cargo el fondo del género.
                    ponComa = true;
                } else
                    genero.Text += ", " + datos.GetString(0); 
            }
            datos.Close();
            select = string.Format("select sipnosis from libro where titulo = '{0}'", libro); // selecciono la sinopsis.
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                sipnosis.Text = (string)datos.GetString(0); // cargo la sinopsis.
            datos.Close();
            select = string.Format("select count(*) from librousu where titulo = '{0}'", libro); // compruebo si el usuario tiene el libro.
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read()) {
                if (datos.GetInt32(0) == 0) { // si no lo tiene habilito que se pueda comprar.
                    habilitarCompra();
                    leerLibro.Visible = false;
                } else {
                    habilitarEliminacion(); // si lo tiene habilito la eliminación y el modo lectura. 
                    leerLibro.Visible = true;
                }
            }
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void habilitarCompra() { // cambio la imagen del picturebox por la de coprar.
            Image img;
            img = Image.FromFile(Constantes.IMG_COMPRAR);
            img.Tag = true; // indico en el tag de la imagen que el usuario no tiene el libro.
            imgComprarVender.BackgroundImage = img;
        }

        private void habilitarEliminacion() { // cambio la imagen del picturebox por la de vender.
            Image img;
            img = Image.FromFile(Constantes.PAPELERA);
            img.Tag = false; // indico en el tag de la imagen que el usuario tiene el libro.
            imgComprarVender.BackgroundImage = img;
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void imgComprarVender_Click(object sender, EventArgs e) {
            // Al hacer clic en el picturebox compruebo el tag de la imagen y
            if ((bool)imgComprarVender.BackgroundImage.Tag) { // si tiene un true, significa que el usuario no lo tenia, entonces como he hecho clic
                agregarLibroAMiColeccion(); // lo agrego a mi colección
                leerLibro.Visible = true; // y habilito el modo lectura.
            } else { // en caso contrario, es el usuario tenía el libro y ha hecho clic en borrar
                eliminarLibroDeMiColeccion();  // lo elimino de su colección
                leerLibro.Visible = false; // deshabilito el modo lectura
            }
        }

        private void agregarLibroAMiColeccion() { // agrego el libro a la colección del usuario.
            string select = string.Format("insert into libroUsu Values ('{0}', '{1}')", usuario, libro);
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
            habilitarEliminacion();
        }

        private void eliminarLibroDeMiColeccion() { // elimino el libro de la colección del usuario. 
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden;
            string select = string.Format("delete from libroUsu where titulo = '{0}' and nick = '{1}';", libro, usuario);
            orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
            habilitarCompra();
        }

        private void leerLibro_Click(object sender, EventArgs e) { // cuando hago clic en leerLibro, lanzo el formulario de lectura. 
            this.Hide();
            LeerLibro form3 = new LeerLibro(libro);
            form3.FormClosed += (s, args) => this.Show();
            form3.Show();
        }
    }
}
