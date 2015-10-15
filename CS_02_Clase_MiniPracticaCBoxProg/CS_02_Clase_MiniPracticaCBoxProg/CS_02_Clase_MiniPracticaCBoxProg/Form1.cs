using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_02_Clase_MiniPracticaCBoxProg
{
    public partial class Form1 : Form
    {
        //LinkedList<CheckBox> lista = new LinkedList<CheckBox>();
        Random rnd = new Random();
        Point posicion;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Agregados para el progresivo.
            /*lista.AddFirst(checkBox1);
            lista.AddLast(checkBox2);
            lista.AddLast(checkBox3);
            lista.AddLast(checkBox4);
            lista.AddLast(checkBox5);*/
        }

        private void clic(object sender, EventArgs e)
        {
            // Para marcarlos progresivamente. 
            /*bool marcado = true;
            foreach (CheckBox cb in lista)
            {
                cb.Checked = marcado;
                if (cb.Equals(sender))
                    marcado = false;
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            posicion = new Point(rnd.Next(this.Size.Width - 20), rnd.Next(this.Height - 20));
            CheckBox cb = new System.Windows.Forms.CheckBox();
            cb.Size = new System.Drawing.Size(14, 15);
            cb.Location = posicion;
            
            //lista.AddLast(cb);
            this.Controls.Add(cb);
        }

        private void eliminarGenerados(object sender, EventArgs e)
        {
            /* foreach (CheckBox cb in lista)
             {
                 cb.Checked = false;
             }*/

            foreach (Object cb in this.Controls)
            {
                if (cb is CheckBox)
                    ((CheckBox)cb).Checked = false;
            }
        }

        private void clicEquis(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
