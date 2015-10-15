using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_02_Clase_MiniPracticaCheckBox
{
    public partial class Form1 : Form
    {
        DateTime cb1, cb2, cb3;
        byte cont = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cb1 = DateTime.Now;
            if (checkBox1.Checked)
            {
                if (cont == 2)
                {
                    if (cb2.CompareTo(cb3) < 0)
                    {
                        checkBox2.Checked = false;
                    }
                    else
                    {
                        checkBox3.Checked = false;
                    }
                }
                cont++;
            }
            else
            {
                cont--;
                checkBox3.Checked = true;
                checkBox2.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            cb2 = DateTime.Now;
            if (checkBox2.Checked)
            {
                if (cont == 2)
                {
                    if (cb1.CompareTo(cb3) < 0)
                    {
                        checkBox1.Checked = false;
                    }
                    else
                    {
                        checkBox3.Checked = false;
                    }
                }
                cont++;
            }
            else
            {
                cont--;
                checkBox1.Checked = true;
                checkBox3.Checked = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cb3 = DateTime.Now;
            if (checkBox3.Checked)
            {
                if (cont == 2)
                {
                    if (cb2.CompareTo(cb1) < 0)
                    {
                        checkBox2.Checked = false;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
                cont++;
            }
            else
            {
                cont--;
                checkBox1.Checked = true;
                checkBox2.Checked = true;
            }
        }
    }
}
