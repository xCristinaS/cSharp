using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _05_MiniPrac_Botonera {
    public partial class Form1 : Form {

        private ArrayList nombres = new ArrayList();
        private const string NUEVO = "Agregar Nuevo", VI = "Modo Visión";
        private string nombreSelec; bool modify = false;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lblNombreMostrar.Hide();
            btnModo.Hide();
            btnAnterior.Hide();
            btnPosterior.Hide();
            btnEliminar.Hide();
            btnModificar.Hide();
            btnCancelar.Hide();
        }

        private void cambiarModo(object sender, EventArgs e) {
            if (nombres.Count > 0) {
                if (!btnModo.Text.Equals(NUEVO)) {
                    modoVisionActivado();
                    btnModo.Text = NUEVO;
                } else {
                    modoEdicionActivado();
                    btnModo.Text = VI;
                }
                modify = false;
            }
        }

        private void modoVisionActivado() {
            btnAnterior.Show();
            btnPosterior.Show();
            btnEliminar.Show();
            btnModificar.Show();
            btnCancelar.Hide();
            btnNuevo.Hide();
            btnGuardar.Hide();
            lblNombreMostrar.Show();
            txtNombre.Hide();
            if (nombres.Count > 0) {
                nombreSelec = (string)nombres[0];
                lblNombreMostrar.Text = nombreSelec;
            }
        }

        private void modoEdicionActivado() {
            btnAnterior.Hide();
            btnPosterior.Hide();
            btnEliminar.Hide();
            btnModificar.Hide();
            btnCancelar.Show();
            btnNuevo.Show();
            btnGuardar.Show();
            lblNombreMostrar.Hide();
            txtNombre.Show();
            if (nombres.Count > 0)
                btnCancelar.Show();
            else
                btnCancelar.Hide();
        }

        private void nuevo(object sender, EventArgs e) {
            txtNombre.Text = "";
        }

        private void mostrarAnterior(object sender, EventArgs e) {
            int i;
            if ((i = nombres.IndexOf(nombreSelec)) > 0) {
                nombreSelec = (string)nombres[i - 1];
                lblNombreMostrar.Text = nombreSelec;
            }
        }

        private void mostrarPosterior(object sender, EventArgs e) {
            int i;
            if ((i = nombres.IndexOf(nombreSelec)) < nombres.Count - 1) {
                nombreSelec = (string)nombres[i + 1];
                lblNombreMostrar.Text = nombreSelec;
            }
        }

        private void cancelar(object sender, EventArgs e) {
            modoVisionActivado();
            modify = false;
        }
        // Pensar el modificar. 
        private void modificar(object sender, EventArgs e) {
            modoEdicionActivado();
            txtNombre.Text = nombreSelec;
            modify = true;
        }

        private void eliminar(object sender, EventArgs e) {
            if (nombres.Count > 0) {
                nombres.Remove(nombreSelec);
                if (nombres.Count > 0) {
                    nombreSelec = (string)nombres[0];
                    lblNombreMostrar.Text = nombreSelec;
                } else {
                    modoEdicionActivado();
                    btnModo.Hide();
                }
            } 
        }

        private void limpiar(object sender, EventArgs e) {
            txtNombre.Text = "";
        }

        private void guardar(object sender, EventArgs e) {
            if (nombres.Count == 0) {
                btnModo.Show();
                btnModo.Text = VI;
            }

            if (!modify)
                nombres.Add(txtNombre.Text);
            else {
                nombres[nombres.IndexOf(nombreSelec)] = txtNombre.Text;
                modoVisionActivado();
                btnModo.Text = NUEVO;
            }
            txtNombre.Text = "";
        }

    }
}
