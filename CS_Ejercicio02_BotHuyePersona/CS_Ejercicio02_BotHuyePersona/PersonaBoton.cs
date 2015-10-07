using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio02_BotHuyePersona
{
    class PersonaBoton
    {
        Persona persona;
        Button boton;

        public PersonaBoton(Persona p, Button b)
        {
            persona = p;
            boton = b;
            boton.Enabled = true;

            igualarImagen();
            actualizar();

            boton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            boton.TabIndex = 1;
            boton.UseVisualStyleBackColor = true;
            boton.FlatAppearance.BorderSize = 0;
        }
        public PersonaBoton(Persona p)
        {
            persona = p;
            boton = new Button();
            boton.Enabled = true;

            igualarImagen();
            actualizar();

            boton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            boton.TabIndex = 1;
            boton.UseVisualStyleBackColor = true;
            boton.FlatAppearance.BorderSize = 0;
        }
        public void actualizar()
        {
            igualarPosicion();
            igualarTamanios();
        }
        public void igualarTamanios()
        {
            boton.Size = persona.getTamanio();
        }
        public void igualarImagen()
        {
            boton.BackgroundImage = persona.getImagen();
        }
        public void igualarPosicion()
        {
            boton.Location = persona.getPosicion();
        }
        public void morir()
        {
            //persona.gritar();
            persona = null;
            boton.Dispose();
        }
        public Persona getPersona()
        {
            return persona;
        }
        public Button getBoton()
        {
            return boton;
        }
    }
}
