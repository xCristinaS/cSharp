using eBdb.EpubReader;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CS_Ejercicio04_Coleccion {
    public partial class LeerLibro : Form {

        private string libro;
        private Epub epub;

        public LeerLibro(string libro) {
            InitializeComponent();
            this.libro = libro;
            imgCerrar.BackgroundImage = Image.FromFile(Constantes.BOTON_CERRAR);
        }

        private void LeerLibro_Load(object sender, EventArgs e) {
            imgCerrar.Location = new Point(Screen.PrimaryScreen.Bounds.Width - imgCerrar.Width-1, 1);
            navegador.Size = new Size(navegador.Parent.Size.Width - 40, navegador.Parent.Size.Height - 80);
            navegador.Left = (this.ClientSize.Width - navegador.Width) / 2;
            navegador.Top = (this.ClientSize.Height - navegador.Height) / 2;
            this.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + Constantes.MOSTRAR_TODOS + Constantes.EXT_PNG);
            cargarLibro();
        }

        private void cargarLibro() {
            // cargo el libro en el navegador para leerlo. Saco de la bdd el texto que indica qué libro es (la imagenPortada) del libro que se selecciono, porque lo 
            // tengo guardado en la carpeta de recursos con el mismo nombre. Una vez optenido, cargo el epub, y lo muestro en el navegador. 
            string select; SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden; SqlDataReader datos;
            select = string.Format("select imagenPortada from libro where titulo = '{0}'", libro);
            orden = new SqlCommand(select, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read()) 
                epub = new Epub(string.Format("{0}//{1}.epub", Constantes.RUTA_RECURSOS, datos.GetString(0)));
            datos.Close();
            BddConection.closeConnection(conexion);
            navegador.DocumentText = epub.GetContentAsHtml();
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
