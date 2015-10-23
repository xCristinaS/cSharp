using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_03_MiniPrac_RB_Prog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fClic(object sender, EventArgs e)
        {
            bool marcar = true;
            foreach (Object panel in (((Control)sender).Parent.Parent).Controls)
            {
                if (panel is Panel)
                {
                    foreach (Object rb in ((Panel)panel).Controls)
                        if (rb is RadioButton)
                        {
                            ((RadioButton)rb).Checked = marcar;
                            if (rb.Equals(sender))
                                marcar = false;
                        }
                }
            }
        }

        private void dClic(object sender, EventArgs e)
        {
            bool marcar = true;
            foreach (Object panel in (((Control)sender).Parent.Parent).Controls)
            {
                if (panel is Panel)
                {
                    foreach (Object rb in ((Panel)panel).Controls)
                        if (rb is RadioButton)
                        {
                            ((RadioButton)rb).Checked = marcar;
                            if (rb.Equals(sender))
                                marcar = false;
                        }
                }
            }
        }
    }
}
