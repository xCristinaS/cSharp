using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_04_MiniPrac_ProgresBar
{
    public partial class Form1 : Form
    {
        // Maxvalue de progresbar indica como se divide el tamaño total de la barra. 
        // si es 10 aumenta dando saltos. 
        public Form1()
        {
            InitializeComponent();
        }

        private void clic(object sender, EventArgs e)
        {
            if (sender.Equals(btnAbajo))
            {
                if (barra.Value > barra.Minimum)
                    barra.Value--;
                
            }
            else if (sender.Equals(btnArriba))
            {
                if (barra.Value < barra.Maximum)
                    barra.Value++;
            } 
        }

        private void clicBarra(object sender, MouseEventArgs e)
        {
            // Aumenta al hacer clic en la mitad superior de la barra y disminuye en el inferior.
            /*
                        int mitad = barra.Width / 2;

                        if (e.Location.X > mitad && barra.Value < barra.Maximum)
                            barra.Value++;
                        else if (e.Location.X < mitad && barra.Value > barra.Minimum)
                            barra.Value--;
            */
            // Al pulsar en "lleno" disminuyo y al pulsar en vacío aumento. 
            /*
                        if (e.Location.X > barra.Value)
                            barra.Value++;
                        else if (e.Location.X < barra.Value)
                            barra.Value--;
            */
            // Aumento la barra hasta la posición donde hice clic. 
            // barra.Value = e.Location.X;

            // Aumento la barra hasta la posición donde hice clic para cualquier barra. 
            barra.Value = (int) Math.Ceiling((decimal)(e.Location.X * barra.Maximum) / barra.Width);
        }
    }
}
