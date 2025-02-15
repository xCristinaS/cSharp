﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_05_MiniPrac_DragDrop {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            textBox2.AllowDrop = true;
            textBox3.AllowDrop = true;
            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;
            listView1.AllowDrop = true;
            listView2.AllowDrop = true;
        }

        private void mouseDown(object sender, MouseEventArgs e) {
            if (sender is TextBox)
                ((TextBox)sender).DoDragDrop(((TextBox)sender).Text, DragDropEffects.All);
            else 
                ((ListBox)sender).DoDragDrop(((ListBox)sender).SelectedItem, DragDropEffects.All);
        }

        private void dragDrop(object sender, DragEventArgs e) {
            if (sender is TextBox)
                ((TextBox)sender).Text = (string)e.Data.GetData(DataFormats.Text);
            else if (sender is ListBox)
                ((ListBox)sender).Items.Add((string)e.Data.GetData(DataFormats.Text));
            else {
                ListViewItem item = new ListViewItem();
                item.Text = (string)e.Data.GetData(DataFormats.Text);
                item.SubItems.Add("columna");
                ((ListView)sender).Items.Add(item);
            }
        }

        private void dragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void itemDrag(object sender, ItemDragEventArgs e) {
            ((ListView)sender).DoDragDrop(((ListView)sender).SelectedItems, DragDropEffects.All);
        }

        private void listView2_dragDrop(object sender, DragEventArgs e) {
            ListView.SelectedListViewItemCollection seleccionados = (ListView.SelectedListViewItemCollection) e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            ListViewItem itemlist;
            foreach (Object item in seleccionados) {
                if (item is ListViewItem) {
                    itemlist = (ListViewItem)((ListViewItem)item).Clone();
                    listView2.Items.Add(itemlist);
                }
            }

        }

        private void listView2_dragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }
    }
}
