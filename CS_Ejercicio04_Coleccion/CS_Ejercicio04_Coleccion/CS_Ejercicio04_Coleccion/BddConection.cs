using System.Data.SqlClient;

namespace CS_Ejercicio04_Coleccion {
    class BddConection {

        public static SqlConnection newConnection() {
            //SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=..\..\..\..\bdd\Coleccion.mdf;Integrated Security=True;Connect Timeout=30");
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\xCristina_S\Desktop\WorkSpace\cSharp\CS_Ejercicio04_Coleccion\bdd\Coleccion.mdf;Integrated Security=True;Connect Timeout=30");
            
            connection.Open();
            return connection;
        }

        public static void closeConnection(SqlConnection connection) {
            connection.Close();
        }
    }
}
