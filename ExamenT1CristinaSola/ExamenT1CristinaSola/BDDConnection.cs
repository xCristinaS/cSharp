using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ExamenT1CristinaSola {
    class BDDConnection {

       public static SqlConnection newConexion() {
            SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-L3F0FUM;Initial Catalog=BddExamen;Integrated Security=True;Pooling=False");
            conexion.Open();
            return conexion;
        }

        public static void closeConnection(SqlConnection conexion) {
            conexion.Close();
        }
    }
}
