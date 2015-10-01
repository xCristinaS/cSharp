using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio02_BotHuye
{
    public partial class Form1 : Form
    {
        private static Point posRat = new Point(0,0);
        private static Point puntoBoton = new Point(0,0);
        private static int cont = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cont++;
            int xR = e.Location.X, xB = button1.Location.X, i;
            int yR = e.Location.Y, yB = button1.Location.Y;
            int minX = 15, minY = 15, maxX = this.Width - 38, maxY = this.Height - 38;
            int distanciaCorta = 200, distanciaLarga = 400;
            double distanciaEntrePuntos = (int) calcularDistancia(button1.Location, e.Location);
            label1.Text = "Distancia entre puntos: " + distanciaEntrePuntos;

            if (!posRat.Equals(e.Location) && distanciaEntrePuntos <= distanciaLarga && cont%2 == 0) {
               if (distanciaEntrePuntos > distanciaCorta)
                {
                    if (xR > xB && xB - 1 > minX) {
                        puntoBoton.X = button1.Location.X - 1;
                        puntoBoton.Y = button1.Location.Y;
                        button1.Location = puntoBoton;
                    }
                    if (xR < xB && xB + 1 < maxX)
                    {
                        puntoBoton.X = button1.Location.X + 1;
                        puntoBoton.Y = button1.Location.Y;
                        button1.Location = puntoBoton;
                    }
                    if (yR > yB && yB - 1 > minY)
                    {
                        puntoBoton.X = button1.Location.X;
                        puntoBoton.Y = button1.Location.Y - 1;
                        button1.Location = puntoBoton;
                    }
                    if (yR < yB && yB + 1 < maxY)
                    {
                        puntoBoton.X = button1.Location.X;
                        puntoBoton.Y = button1.Location.Y + 1;
                        button1.Location = puntoBoton;
                    }
                }
                else if (distanciaEntrePuntos <= distanciaCorta)
                {
                    if (xR > xB && xB - 1 > minX)
                    {
                        for (i = 0; i <= 5; i++)
                        {
                            puntoBoton.X = button1.Location.X - 1;
                            puntoBoton.Y = button1.Location.Y;
                            button1.Location = puntoBoton;
                        }
                    }
                    if (xR < xB && xB + 1 < maxX)
                    {
                        for (i = 0; i <= 5; i++)
                        {
                            puntoBoton.X = button1.Location.X + 1;
                            puntoBoton.Y = button1.Location.Y;
                            button1.Location = puntoBoton;
                        }
                    }
                    if (yR > yB && yB - 1 > minY)
                    {
                        for (i = 0; i <= 5; i++)
                        {
                            puntoBoton.X = button1.Location.X;
                            puntoBoton.Y = button1.Location.Y - 1;
                            button1.Location = puntoBoton;
                        }
                    }
                    if (yR < yB && yB + 1 < maxY)
                    {
                        for (i = 0; i <= 5; i++)
                        {
                            puntoBoton.X = button1.Location.X;
                            puntoBoton.Y = button1.Location.Y + 1;
                            button1.Location = puntoBoton;
                        }
                    }
                }
            }
            posRat = e.Location;
        }

        private double calcularDistancia(Point a, Point b)
        {
            double r = 0;
            r = Math.Pow(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2), 0.5);
            return r;
        }
    }
}
