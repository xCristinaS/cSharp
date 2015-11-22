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
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            SqlConnection conexion = BddConection.newConnection();
            string usuario = txtUsuario.Text; string clave = txtClave.Text;
            string select = string.Format("select count(*) from usuario where nick = '{0}' and pass = '{1}';", usuario, clave);
            SqlCommand orden = new SqlCommand(select, conexion);

            int resultado = (int)orden.ExecuteScalar();
            if (resultado == 1)
                MessageBox.Show("logueado.");
            else
                MessageBox.Show("NO logueado.");

            BddConection.closeConnection(conexion);
        }
    }
}
