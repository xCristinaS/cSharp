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
            // cuando pulse la tecla enter, si el campo de usuario y el de contraseña están rellenos trato de iniciar sesión. 
            if (!txtUsuario.Text.Equals("") && !txtClave.Text.Equals("") && e.KeyChar == (char)Keys.Enter) 
                iniciarSesion();
        }

        private void newUsu_KeyPress(object sender, KeyPressEventArgs e) {
            // si he pulsado el enter, trato de validar y crear el nuevo usuario. 
            if (e.KeyChar == (char)Keys.Enter) 
                validarYCrearUsuario();
        }
        
        private bool validarYCrearUsuario() {
            // si los campos nick, contraseña y repetir contraseña están rellenos, compruebo si el nuevo nick y las contraseñas son correctas y registro el usuario.
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
            // si los campos están rellenos, trato de iniciar sesión. 
            if (!txtUsuario.Text.Equals("") && !txtClave.Text.Equals(""))
                iniciarSesion();
        }

        private bool newNickValido() {
            // compruebo si en la bdd hay algún usuario con ese nick. En función de ese resultado muestro el nick como válido o no. 
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
            // si las contraseñas son iguales (newPass y newPassRepeat) son válidas. 
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
            // inserta en la bdd el nuevo usuario. 
            SqlConnection conexion = BddConection.newConnection();
            string newUsu = newNick.Text, newClave = newPass.Text;
            string insert = string.Format("insert into usuario values ('{0}', '{1}');", newUsu, newClave);
            SqlCommand orden = new SqlCommand(insert, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
        }

        private void resetCampos() {
            // reseteo los campos. 
            txtUsuario.Text = "";
            txtClave.Text = "";
            newNick.Text = "";
            newPass.Text = "";
            newPassRepeat.Text = "";
        }

        private void newUsu_TextChange(object sender, EventArgs e) {
            // cada vez que presiono una en el nick, o contraseña (ventana de nuevo usuario) compruebo la válidez de los datos. 
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
            // muestro el panel de registro. 
            panelLogin.Hide();
            panelNuevoUsu.Show();
            resetCampos();
        }

        private void inicioSesion_Click(object sender, EventArgs e) {
            // muestro el panel de login. 
            panelLogin.Show();
            panelNuevoUsu.Hide();
            resetCampos();
        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            // si estoy en el panel de login, al hacer clic en aceptar tratará de validar y loguear al usuario. Si estoy en el panel de registro tratará de crear el
            // nuevo usuario. 
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
            // si el nick y la contra coinciden con un registro de usuario de la bdd, se iniciará sesión y se cargará el siguiente formulario. En caso contrario
            // se mostrará un mensaje de error. 
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
            // agrega el nuevo usuario a la bdd y resetea los campos. 
            agregarNuevoUsuarioBdd();
            resetCampos();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e) {
            // al hacer click en el acerca de, se muestra el siguiente mensaje. 
            MessageBox.Show("Aplicación creada por Cristina Sola.", "Información");
        }
    }
}
