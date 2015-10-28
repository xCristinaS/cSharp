using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CS_Ejercicio02_BotHuyePersona
{
    class Persona
    {
        int velocidad = 0, monstruoMuyLejos = 400, monstruoCerca = 200;
        Point posicion;
        Size tamanio;
        Image imagen;

        public Persona(Size tamanioForm)
        {
            Random rnd = new Random();
            int tamanioP = rnd.Next(Constantes.TAMANIO_MIN_PERSONA, Constantes.TAMANIO_MAX_PERSONA);
            posicion = new Point(rnd.Next(Constantes.LIMITES_CAMPO, tamanioForm.Width - tamanioP), rnd.Next(Constantes.LIMITES_CAMPO, tamanioForm.Height - tamanioP));
            velocidad = rnd.Next(Constantes.MAX_VELOCIDAD);
            tamanio = new Size(tamanioP, tamanioP);

            switch(rnd.Next(Constantes.MIN_IMG_DISP, Constantes.MAX_IMG_DISP)){
                case 1:
                    imagen = Properties.Resources.p1;
                    break;
                case 2:
                    imagen = Properties.Resources.p2;
                    break;
                case 3:
                    imagen = Properties.Resources.p3;
                    break;
                case 4:
                    imagen = Properties.Resources.p4;
                    break;
                case 5:
                    imagen = Properties.Resources.p5;
                    break;
                case 6:
                    imagen = Properties.Resources.p6;
                    break;
                case 7:
                    imagen = Properties.Resources.p7;
                    break;
                case 8:
                    imagen = Properties.Resources.p8;
                    break;
                case 9:
                    imagen = Properties.Resources.p9;
                    break;
                case 10:
                    imagen = Properties.Resources.p10;
                    break;
                case 11:
                    imagen = Properties.Resources.p11;
                    break;
                case 12:
                    imagen = Properties.Resources.p12;
                    break;
            }
        }
        private int mirar(Point posicionMonstruo)
        {
            int r;
            r = (int) Math.Pow(Math.Pow(posicion.X - posicionMonstruo.X, 2) + Math.Pow(posicion.Y - posicionMonstruo.Y, 2), 0.5);
            return r;
        }
        public void huir(Point posicionMonstruo, Size campo)
        {
            int distanciaConMonstruo = mirar(posicionMonstruo), i;
            int monstruoX = posicionMonstruo.X, personaX = posicion.X;
            int monstruoY = posicionMonstruo.Y, personaY = posicion.Y;
            int maxAlCampo = campo.Height - tamanio.Height - Constantes.LIMITES_CAMPO;
            int maxAnCampo = campo.Width - tamanio.Width - Constantes.LIMITES_CAMPO;

            if (distanciaConMonstruo <= monstruoMuyLejos)
            {
                if (distanciaConMonstruo > monstruoCerca)
                {
                    if (monstruoX > personaX && personaX - 1 > Constantes.LIMITES_CAMPO)
                        posicion.X -= 1;
                    if (monstruoX < personaX && personaX + 1 < maxAnCampo)
                        posicion.X += 1;
                    if (monstruoY > personaY && personaY - 1 > Constantes.LIMITES_CAMPO)
                        posicion.Y -= 1;
                    if (monstruoY < personaY && personaY + 1 < maxAlCampo)
                        posicion.Y += 1;
                }
                else if (distanciaConMonstruo <= monstruoCerca)
                {
                    if (monstruoX > personaX && personaX - 1 > Constantes.LIMITES_CAMPO)
                        for (i = 0; i <= velocidad; i++)
                            posicion.X -= 1;
                    if (monstruoX < personaX && personaX + 1 < maxAnCampo)
                        for (i = 0; i <= velocidad; i++)
                            posicion.X += 1;
                    if (monstruoY > personaY && personaY - 1 > Constantes.LIMITES_CAMPO)
                        for (i = 0; i <= velocidad; i++)
                            posicion.Y -= 1;
                    if (monstruoY < personaY && personaY + 1 < maxAlCampo)
                        for (i = 0; i <= velocidad; i++)
                            posicion.Y += 1;
                }
            }
        }
        public void crecer()
        {
            tamanio.Width = tamanio.Width + Constantes.CRECIMIENTO_PERSONA;
            tamanio.Height = tamanio.Height + Constantes.CRECIMIENTO_PERSONA;
        }
        public void gritar()
        {
            Console.Beep();
        }
        public Point getPosicion()
        {
            return posicion;
        }
        public Size getTamanio()
        {
            return tamanio;
        }
        public Image getImagen()
        {
            return imagen;
        }
        public void setTamanio(Size nuevoTamanio)
        {
            tamanio = nuevoTamanio;
        }
    }
}
