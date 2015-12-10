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

namespace ExamenT1CristinaSola {
    public partial class FormRaton : Form {
        public FormRaton() {
            InitializeComponent();
        }

        private void FormRaton_Load(object sender, EventArgs e) {
            CenterToScreen();
            cargarHoy();
            cargarManiana();
            cargarPasado();
        }

        private void cargarHoy() {
            DateTime ahora = DateTime.Now; // cojo la fecha de hoy. 
            string[] auxFecha = ahora.GetDateTimeFormats(); // me da un array con la fecha de hoy en muchos formatos. 
            string fecha = auxFecha[0]; // la primera me da la fecha con el formato dd/mm/yyyy
            string consulta = string.Format("select nombre, ciudad from ninio where idNinio in (select idNinio from cae where fecha = '{0}')", fecha); 
            SqlConnection conexion = BDDConnection.newConexion(); 
            SqlCommand orden = new SqlCommand(consulta, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            while (datos.Read()) {
                lbHoy.Items.Add(datos.GetString(0) + "         " + datos.GetString(1));
            }
            BDDConnection.closeConnection(conexion);
        }

        private void cargarManiana() {
            DateTime ahora = DateTime.Now; // cojo la fecha de hoy. 
            ahora = ahora.AddDays(1);
            string[] auxFecha = ahora.GetDateTimeFormats(); // me da un array con la fecha de hoy en muchos formatos. 
            string fecha = auxFecha[0]; // la primera me da la fecha con el formato dd/mm/yyyy
            string consulta = string.Format("select nombre, ciudad from ninio where idNinio in (select idNinio from cae where fecha = '{0}')", fecha);
            SqlConnection conexion = BDDConnection.newConexion();
            SqlCommand orden = new SqlCommand(consulta, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            while (datos.Read()) {
               lbMañana.Items.Add(datos.GetString(0) + "         " + datos.GetString(1));
            }
            BDDConnection.closeConnection(conexion);
        }

        private void cargarPasado() {

        }
    }
}
