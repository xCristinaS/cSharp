using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace ExamenT1CristinaSola {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            CenterToScreen();
            consultarDatosBdd();
        }

        private void consultarDatosBdd() {
            SqlConnection conexion = BDDConnection.newConexion();
            string select = "select count(*) from Padre";
            SqlCommand orden = new SqlCommand(select, conexion);
            int i = (int) orden.ExecuteScalar();
            if (i == 0)
                importarDesdeFichero();
            BDDConnection.closeConnection(conexion);
        }

        private void importarDesdeFichero() {
            importarNinios();
            importarPadres();
            importarCae();
            importarPadreNinio();
        }

        private void importarPadres() {
            StreamReader lector = new StreamReader(@"..//../Recursos//progenitor.txt"); string linea;
            SqlConnection conexion = BDDConnection.newConexion();
            while ((linea = lector.ReadLine()) != null)
                insertarPadre(linea, conexion);
            BDDConnection.closeConnection(conexion);
        }

        private void importarNinios() {
            StreamReader lector = new StreamReader(@"..//../Recursos//niño.txt"); string linea;
            SqlConnection conexion = BDDConnection.newConexion();
            while ((linea = lector.ReadLine()) != null)
                insertarNinios(linea, conexion);
            BDDConnection.closeConnection(conexion);
        }

        private void importarPadreNinio() {
            StreamReader lector = new StreamReader(@"..//../Recursos//espadre.txt"); string linea;
            SqlConnection conexion = BDDConnection.newConexion();
            while ((linea = lector.ReadLine()) != null)
                insertarPadreNinio(linea, conexion);
            BDDConnection.closeConnection(conexion);
        }

        private void importarCae() {
            StreamReader lector = new StreamReader(@"..//../Recursos//cae.txt"); string linea;
            SqlConnection conexion = BDDConnection.newConexion();
            while ((linea = lector.ReadLine()) != null)
                insertarCae(linea, conexion);
            BDDConnection.closeConnection(conexion);
        }

        private void insertarPadre(string linea, SqlConnection conexion) {
            String[] campos = linea.Split('|');
            string insert = string.Format("insert into Padre Values ('{0}', '{1}')", campos[0], campos[1]);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
        }

        private void insertarNinios(string linea, SqlConnection conexion) {
            String[] campos = linea.Split('|');
            string insert = string.Format("insert into ninio Values ('{0}', '{1}', '{2}')", campos[0], campos[1], campos[2]);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
        }

        private void insertarPadreNinio(string linea, SqlConnection conexion) {
            String[] campos = linea.Split('|');
            string insert = string.Format("insert into padre_ninio Values ('{0}', '{1}')", campos[0], campos[1]);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
        }

        private void insertarCae(string linea, SqlConnection conexion) {
            String[] campos = linea.Split('|');
            string insert = string.Format("insert into cae Values ('{0}', '{1}', '{2}')", campos[0], campos[1], campos[2]);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
        }

        private void iniciarSesion_Click(object sender, EventArgs e) {
            string usuario = tbUsuario.Text;
            if (!usuario.Equals("")) {
                if (!usuario.Equals(Constantes.RATONCITO)) {
                    if (comprobarPadreEnBdd(usuario))
                        abrirVistaPadre(usuario);
                    else
                        MessageBox.Show("Ese id de usuario no se encuentra en la base de datos.", "Error");
                } else
                    abrirVistaRatoncito();
            }
        }

        private bool comprobarPadreEnBdd(string usuario) {
            bool existe = false;
            SqlConnection conexion = BDDConnection.newConexion();
            string consulta = string.Format("select count(*) from Padre where idPadre = '{0}'", usuario);
            SqlCommand orden = new SqlCommand(consulta, conexion);
            int i = (int)orden.ExecuteScalar();
            if (i == 1)
                existe = true;
            return existe;
        }

        private void abrirVistaPadre(string usuario) {
            this.Hide();
            FormPadre form = new FormPadre(usuario);
            form.FormClosed += (s, args) => this.Show();
            form.Show();
        }

        private void abrirVistaRatoncito() {
            this.Hide();
            FormRaton form = new FormRaton();
            form.FormClosed += (s, args) => this.Show();
            form.Show();
        }
    }
}
