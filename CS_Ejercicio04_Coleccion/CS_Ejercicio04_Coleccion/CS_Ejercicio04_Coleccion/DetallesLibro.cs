using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void cargarLibro() {
            string select = string.Format("select autor from libro where titulo = '"+libro+"'");
            SqlConnection conexion = BddConection.newConnection(); bool ponComa = false;
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            titulo.Text = libro;
            if (datos.Read())
                autor.Text = datos.GetString(0);
            datos.Close();
            select = string.Format("select imagenPortada from libro where titulo = '" + libro + "'");
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                portada.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + datos.GetString(0) + Constantes.EXT_JPG);
            datos.Close();
            select = string.Format("select genero from LibroGenero where titulo = '" + libro + "'");
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            while (datos.Read()) {
                if (!ponComa) {
                    genero.Text = datos.GetString(0);
                    ponComa = true;
                } else
                    genero.Text += ", " + datos.GetString(0);
            }
            datos.Close();
            select = string.Format("select sipnosis from libro where titulo = '" + libro + "'");
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                sipnosis.Text = (string)datos.GetString(0);
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
