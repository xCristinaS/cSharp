using System.Data.SqlClient;

namespace CS_Ejercicio04_Coleccion {
    class BddConection {

        public static SqlConnection newConnection() {
            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=Coleccion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();
            return connection;
        }

        public static void closeConnection(SqlConnection connection) {
            connection.Close();
        }
    }
}
