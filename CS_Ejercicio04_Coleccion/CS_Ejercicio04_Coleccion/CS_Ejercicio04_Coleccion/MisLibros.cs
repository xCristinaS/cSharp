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
    public partial class MisLibros : Form {

        private string usuario; // para saber que usuario inicio sesión. 
        private ImageList listaImg = new ImageList();

        public MisLibros(string usuario) {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void MisLibros_Load(object sender, EventArgs e) {
            lvLibros.View = View.LargeIcon;
            lvLibros.LargeImageList = listaImg;
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
            cargarLibros(comboGeneros.SelectedItem.ToString());
        }

        private void cargarLibros(string genero) {
            string titulo, imagen; int cont = 0; 
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}') and titulo in (select titulo from libroUsu where nick = '{1}')", genero, usuario);
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            listaImg.Images.Clear(); lvLibros.Items.Clear();
            while (datos.Read()) {
                ListViewItem item = new ListViewItem(); 
                titulo = datos.GetString(0);
                imagen = datos.GetString(1);
                listaImg.Images.Add(Image.FromFile(Constantes.RUTA_RECURSOS+imagen+Constantes.EXT_JPG));
                item.ImageIndex = cont;
                item.Text = titulo;
                lvLibros.Items.Add(item);
                cont++;
            }
            
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
