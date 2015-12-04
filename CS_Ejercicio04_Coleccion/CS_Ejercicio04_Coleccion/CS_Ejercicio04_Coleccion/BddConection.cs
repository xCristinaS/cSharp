using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CS_Ejercicio04_Coleccion {
    class BddConection {

        private static SqlConnection conexion; private static bool conexionCerrada = true;

        public static SqlConnection newConnection() {
            //SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=..\..\..\..\bdd\Coleccion.mdf;Integrated Security=True;Connect Timeout=30");
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Cristina\Documents\GitHub\cSharp\CS_Ejercicio04_Coleccion\bdd\Coleccion.mdf;Integrated Security=True;Connect Timeout=30");
            
            connection.Open();
            return connection;
        }

        public static void closeConnection(SqlConnection connection) {
            connection.Close();
        }

        public static void abrirConexion() {
            conexion = newConnection();
            conexionCerrada = false;
        }

        public static void cerrarConexion() {
            if (!conexionCerrada) {
                conexion.Close();
                conexionCerrada = true;
            }
        }

        public static void ejecutarSelectBuscador(string select, ListView lista, ImageList imagenes) {
            string titulo, imagen; int cont = 0;
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            imagenes.Images.Clear(); lista.Items.Clear();
            while (datos.Read()) {
                ListViewItem item = new ListViewItem();
                titulo = datos.GetString(0);
                imagen = datos.GetString(1);
                imagenes.Images.Add(Image.FromFile(Constantes.RUTA_RECURSOS + imagen + Constantes.EXT_JPG));
                item.ImageIndex = cont;
                item.Text = titulo;
                lista.Items.Add(item);
                cont++;
            }
            datos.Close();
        }
    }
}
