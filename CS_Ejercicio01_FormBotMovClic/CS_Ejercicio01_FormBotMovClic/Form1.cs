using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio01_FormBotMovClic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.Aquamarine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            MessageBox.Show("Has Ganado!!");
            this.Close();

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Tomato; 
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int maxAlt = this.Height - button1.Height;
            int maxAnch = this.Width - button1.Width;
            Random rnd = new Random();
            button1.Location = new Point(rnd.Next(maxAnch), rnd.Next(maxAlt));
        }
    }
}
