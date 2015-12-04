using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace CS_Ejercicio04_Coleccion {
    public partial class FormInicioSesion : Form {

        public FormInicioSesion() {
            InitializeComponent();
        }

        private void InicioSesion_Load(object sender, EventArgs e) {
            CenterToScreen();
            panelNuevoUsu.Visible = false;
            wrong1.Hide();
            wrong2.Hide();
            wrong3.Hide();
            this.BackgroundImage = Image.FromFile(Constantes.FONDO_FORM);
            btnAceptar.BackgroundImage = Image.FromFile(Constantes.BOTON_ACEPTAR);
            imgCerrar.BackgroundImage = Image.FromFile(Constantes.BOTON_CERRAR);
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void login_KeyPress(object sender, KeyPressEventArgs e) {
            if (!txtUsuario.Text.Equals("") && !txtClave.Text.Equals("") && e.KeyChar == (char)Keys.Enter) 
                iniciarSesion();
        }

        private void newUsu_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) 
                validarYCrearUsuario();
        }
        
        private bool validarYCrearUsuario() {
            bool usuarioCreado = false;
            if (!newNick.Text.Equals("") && !newPass.Text.Equals("") && !newPassRepeat.Text.Equals(""))
                if (newNickValido() && passCorrectas()) {
                    registrarUsuario();
                    wrong1.Visible = false;
                    wrong2.Visible = false;
                    wrong3.Visible = false;
                    usuarioCreado = true;
                }
            return usuarioCreado;
        }

        private void validarYLoguear() {
            if (!txtUsuario.Text.Equals("") && !txtClave.Text.Equals(""))
                iniciarSesion();
        }

        private bool newNickValido() {
            bool result = true; string nuevoUsu = newNick.Text;
            if (!nuevoUsu.Equals("")) {
                SqlConnection conexion = BddConection.newConnection();
                string select = "select count(*) from usuario where nick = '" + nuevoUsu + "'";
                SqlCommand orden = new SqlCommand(select, conexion);
                SqlDataReader datos = orden.ExecuteReader();
                datos.Read();
                if (datos.GetInt32(0) != 0)
                    result = false;
            } else
                result = false;

            if (result)
                wrong1.BackgroundImage = Image.FromFile(Constantes.BIEN);
            else
                wrong1.BackgroundImage = Image.FromFile(Constantes.FALLO);

            return result;
        }

        private bool passCorrectas() {
            bool result = false;
            if (!newPass.Text.Equals(""))
                if (newPass.Text.Equals(newPassRepeat.Text))
                    result = true;

            if (result) {
                wrong2.BackgroundImage = Image.FromFile(Constantes.BIEN);
                wrong3.BackgroundImage = Image.FromFile(Constantes.BIEN);
            } else {
                wrong2.BackgroundImage = Image.FromFile(Constantes.FALLO);
                wrong3.BackgroundImage = Image.FromFile(Constantes.FALLO);
            }
            return result;
        }

        private void agregarNuevoUsuarioBdd() {
            SqlConnection conexion = BddConection.newConnection();
            string newUsu = newNick.Text, newClave = newPass.Text;
            string insert = string.Format("insert into usuario values ('{0}', '{1}');", newUsu, newClave);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
        }

        private void resetCampos() {
            txtUsuario.Text = "";
            txtClave.Text = "";
            newNick.Text = "";
            newPass.Text = "";
            newPassRepeat.Text = "";
        }

        private void newUsu_TextChange(object sender, EventArgs e) {
            if (((TextBox)sender).Equals(newNick) && !wrong1.Visible)
                wrong1.Visible = true;
            else if (!((TextBox)sender).Equals(newNick) && !wrong2.Visible) {
                wrong2.Visible = true;
                wrong3.Visible = true;
            } 
            newNickValido();
            passCorrectas();
        }

        private void registrarse_Click(object sender, EventArgs e) {
            panelLogin.Hide();
            panelNuevoUsu.Show();
            resetCampos();
        }

        private void inicioSesion_Click(object sender, EventArgs e) {
            panelLogin.Show();
            panelNuevoUsu.Hide();
            resetCampos();
        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            if (panelLogin.Visible)
                validarYLoguear();
            else {
                if (validarYCrearUsuario()) {
                    panelNuevoUsu.Visible = false;
                    panelLogin.Visible = true;
                }
            }
        }

        private void iniciarSesion() {
            SqlConnection conexion = BddConection.newConnection();
            string usuario = txtUsuario.Text; string clave = txtClave.Text;
            string select = string.Format("select count(*) from usuario where nick = '{0}' and pass = '{1}';", usuario, clave);
            SqlCommand orden = new SqlCommand(select, conexion);

            int resultado = (int)orden.ExecuteScalar();
            if (resultado == 1) {
                resetCampos();
                this.Hide();
                Libros form2 = new Libros(usuario);
                form2.FormClosed += (s, args) => this.Show();
                form2.Show();
            } else
                MessageBox.Show("El usuario y/o contraseña introducidos no son válidos.", "Error");

            BddConection.closeConnection(conexion);
        }

        private void registrarUsuario() {
            agregarNuevoUsuarioBdd();
            resetCampos();
        }
    }
}
