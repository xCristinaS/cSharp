using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CS_Ejercicio04_Coleccion {
    public partial class Libros : Form {

        private string usuario; // para saber que usuario inicio sesión. 
        private ImageList listaImg = new ImageList();

        public Libros(string usuario) {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void Libros_Load(object sender, EventArgs e) {
            CenterToScreen();
            misLibros.View = View.LargeIcon;
            misLibros.LargeImageList = listaImg;
            tienda.View = View.LargeIcon;
            tienda.LargeImageList = listaImg;
            listaImg.ImageSize = new Size(60, 80);
            cargarGenerosCombo();
        }

        private void cargarGenerosCombo() {
            string select = "select distinct genero from LibroGenero;";
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            while (datos.Read())
                 comboGeneros.Items.Add(datos.GetString(0));


            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void comboGeneros_SelectedIndexChanged(object sender, EventArgs e) {
            cargarMisLibros(comboGeneros.SelectedItem.ToString());
            cargarLibrosTienda(comboGeneros.SelectedItem.ToString());
        }

        private void cargarMisLibros(string genero) {
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}') and titulo in (select titulo from libroUsu where nick = '{1}')", genero, usuario);
            traerLibrosDeBdd(select, misLibros);
        }

        private void cargarLibrosTienda(string genero) {
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}')", genero);
            traerLibrosDeBdd(select, tienda);
        }

        private void traerLibrosDeBdd(string select, ListView lista) {
            string titulo, imagen; int cont = 0;
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            listaImg.Images.Clear(); lista.Items.Clear();
            while (datos.Read()) {
                ListViewItem item = new ListViewItem();
                titulo = datos.GetString(0);
                imagen = datos.GetString(1);
                listaImg.Images.Add(Image.FromFile(Constantes.RUTA_RECURSOS + imagen + Constantes.EXT_JPG));
                item.ImageIndex = cont;
                item.Text = titulo;
                lista.Items.Add(item);
                cont++;
            }
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void lvLibros_ItemActivate(object sender, EventArgs e) {
            string titulo = ((ListViewItem) ((ListView)sender).SelectedItems[0]).Text;
            this.Hide();
            DetallesLibro form3 = new DetallesLibro(usuario, titulo);
            form3.FormClosed += (s, args) => this.Show();
            form3.Show();
        }
    }
}
