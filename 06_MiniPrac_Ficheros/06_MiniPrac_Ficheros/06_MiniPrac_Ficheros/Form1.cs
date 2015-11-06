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

namespace _06_MiniPrac_Ficheros {
    public partial class Form1 : Form {
        bool centinela1 = false, centinela2 = false;
        string extension = ".pro", ruta = "..\\..\\";
        string[] pMagicos = { "Nigromante", "Mago" };
        string[] pMundano = { "Arquero", "Daguero", "Paladin", "Cazador", "Guerrero" };
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            string nombrefich; StreamWriter escritor;
            if (!centinela1 && comboBox1.Text.Equals("Ser Mágico")) {
                nombrefich = "serMagico";
                File.Create(ruta+nombrefich+extension);
                centinela1 = true;
            } else if (!centinela2 && comboBox1.Text.Equals("Ser Mundano")) {
                nombrefich = "serMundano";
                File.Create(ruta + nombrefich + extension);
                centinela2 = true;
            }
        }
    }
}
