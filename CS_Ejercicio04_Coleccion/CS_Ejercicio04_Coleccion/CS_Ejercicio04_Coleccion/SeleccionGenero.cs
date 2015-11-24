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
    public partial class SeleccionGenero : Form {

        private string usuario; // para saber que usuario inicio sesión. 

        public SeleccionGenero(string usuario) {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void SeleccionGenero_Load(object sender, EventArgs e) {
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
            cargarLibros(comboGeneros.SelectedText);
        }

        private void cargarLibros(string genero) {
            string select = "select distinct genero from LibroGenero;";
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            while (datos.Read())
                comboGeneros.Items.Add(datos.GetString(0));

            datos.Close();
            BddConection.closeConnection(conexion);
        }
    }
}
