using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_01_Clase_MinipracticaCombo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //String[] a = { "a", "b", "c", "d" };
            //comboBox1.Items.Add("Pepito");
            //comboBox1.Items.AddRange(a);
            //comboBox1.SelectedIndex = 5; Selecciona el index por defecto. 
            //comboBox1.SelectedItem = "amarillo";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                String[] colores = { "rosa", "amarillo", "verde", "rojo", "azul" };
                comboBox1.Items.AddRange(colores);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                String[] letras = { "a", "b", "c", "d", "e" };
                comboBox1.Items.AddRange(letras);
            }
        }
    }
}
