using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExamenT1CristinaSola {
    public partial class FormPadre : Form {

        private string usuario;
        private ImageList listaImgDiente = new ImageList();

        public FormPadre(string usuario) {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void FormPadre_Load(object sender, EventArgs e) {
            CenterToScreen();
            cargarHijos();

            listViewEnBoca.AllowDrop = true;
            listViewCaidos.AllowDrop = true;

            listaImgDiente.Images.Add(Image.FromFile(Constantes.IMG_DIENTE));
            listViewEnBoca.View = View.SmallIcon;
            listViewEnBoca.SmallImageList = listaImgDiente;

            listViewCaidos.View = View.SmallIcon;
            listViewCaidos.SmallImageList = listaImgDiente;
            imgDiente.BackgroundImage = Image.FromFile(Constantes.IMG_DIENTE);
        }

        private void cargarHijos() {
            string select = String.Format("select nombre from ninio where idNinio in (select idNinio from padre_ninio where idPadre = '{0}')", usuario);
            SqlConnection conexion = BDDConnection.newConexion();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            while (datos.Read())
                comboHijos.Items.Add(datos.GetString(0));
            datos.Close();
            BDDConnection.closeConnection(conexion);
        }

        private void comboHijos_SelectedIndexChanged(object sender, EventArgs e) {
            cargarDientes(comboHijos.SelectedItem.ToString());
            actualizarTickets();
        }

        private void cargarDientes(string ninio) {
            LinkedList<String> dientesCaidos = consultarDientesCaidos(ninio);
            cargarDientesCaidos(dientesCaidos);
            cargarDientesEnBoca(dientesCaidos);
        }

        private LinkedList<String> consultarDientesCaidos(string ninio) {
            LinkedList<String> dientes = new LinkedList<string>();
            string consulta = string.Format("select idDiente from cae where idNinio = (select idNinio from ninio where nombre = '{0}')", ninio);
            SqlConnection conexion = BDDConnection.newConexion();
            SqlCommand orden = new SqlCommand(consulta, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            while (datos.Read())
                dientes.AddLast(Convert.ToString(datos.GetInt32(0)));
            return dientes;
        }

        private void cargarDientesCaidos(LinkedList<String> dientesCaidos) {
            listViewCaidos.Clear();
            foreach (string dienteCaido in dientesCaidos) {
                ListViewItem it = new ListViewItem(dienteCaido);
                it.ImageIndex = 0;
                listViewCaidos.Items.Add(it);
            }
        }

        private void cargarDientesEnBoca(LinkedList<String> dientesCaidos) {
            listViewEnBoca.Clear();
            String[] aux = dientesCaidos.ToArray<string>();
            bool agregar;
            int caido;
            for (int i = 1; i <= 32; i++) {
                agregar = true;
                for (int j = 0; j < dientesCaidos.Count; j++) {
                    caido = Convert.ToInt32(aux[j]);
                    if (caido == i)
                        agregar = false;
                }
                if (agregar) {
                    ListViewItem it = new ListViewItem(Convert.ToString(i));
                    it.ImageIndex = 0;
                    listViewEnBoca.Items.Add(it);
                }
            }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e) {
            ListView lista = (ListView) sender;
            lista.DoDragDrop(lista.SelectedItems, DragDropEffects.All);
        }

        private void listView_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void listView_DragDrop(object sender, DragEventArgs e) {
            ListView.SelectedListViewItemCollection dientes = (ListView.SelectedListViewItemCollection) e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            if (sender.Equals(listViewCaidos)) {
                agregarOEliminarDeBdd(dientes, false);
            } else {
                agregarOEliminarDeBdd(dientes, true);
            }
            cargarDientes(comboHijos.SelectedItem.ToString());
            actualizarTickets();
        }

        private void agregarOEliminarDeBdd(ListView.SelectedListViewItemCollection dientes, bool eliminar) {
            int idNinio = -1; 
            string fecha = "sin fecha", sql, selectIdNinio;
            SqlConnection conexion = BDDConnection.newConexion();
            SqlCommand orden;
            SqlDataReader datos;
            DateTime ahora = DateTime.Now; // cojo la fecha de hoy. 
            string[] aux = ahora.GetDateTimeFormats(); // me da un array con la fecha de hoy en muchos formatos. 
            fecha = aux[0]; // la primera me da la fecha con el formato dd/mm/yyyy

            selectIdNinio = string.Format("select idNinio from ninio where nombre = '{0}' and idNinio in (select idNinio from padre_ninio where idPadre = '{1}')", comboHijos.SelectedItem.ToString(), usuario);
            orden = new SqlCommand(selectIdNinio, conexion);
            datos = orden.ExecuteReader();
            if (datos.Read())
                idNinio = datos.GetInt32(0);
            datos.Close();

            foreach (ListViewItem it in dientes) {
                if (!eliminar) {
                    fecha = fechaAproximada(fecha, conexion);
                    sql = string.Format("insert into cae values ('{0}', '{1}', '{2}')", it.Text, idNinio, fecha);
                } else
                    sql = string.Format("delete from cae where idDiente = '{0}' and idNinio = '{1}'", it.Text, idNinio);
                orden = new SqlCommand(sql, conexion);
                orden.ExecuteScalar();
            }
            BDDConnection.closeConnection(conexion);
        }
        
        private void actualizarTickets() {
            string sql = string.Format("select nombre, idDiente, fecha from cae c, ninio n where c.idNinio = n.idNinio and n.idNinio in (select idNinio from padre_ninio where idPadre = '{0}')", usuario);
            SqlConnection conexion = BDDConnection.newConexion();
            SqlCommand orden = new SqlCommand(sql, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            string nombre, diente, fecha;
            ListViewItem itemAux;
            
            listViewTickets.Items.Clear();

            while (datos.Read()) {
                nombre = datos.GetString(0);
                diente = Convert.ToString(datos.GetInt32(1));
                fecha = datos.GetString(2);

                itemAux = new ListViewItem();
                itemAux.Text = nombre;

                listViewTickets.Items.Add(itemAux);
                itemAux = listViewTickets.Items[listViewTickets.Items.Count - 1];
                itemAux.SubItems.Add(new ListViewItem.ListViewSubItem(itemAux, diente));
                itemAux.SubItems.Add(new ListViewItem.ListViewSubItem(itemAux, fecha));
            }
            BDDConnection.closeConnection(conexion);
        }

        private string fechaAproximada(string fecha, SqlConnection conexion) {
            string consulta = string.Format("select count(*) from cae where fecha = '{0}' and idNinio in (select idNinio from padre_ninio where idPadre = '{1}')", fecha, usuario);
            SqlCommand orden = new SqlCommand(consulta, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            int num;
            string[] auxFecha;
            if (datos.Read()) {
                num = datos.GetInt32(0);
                if (num >= 5) {
                    DateTime ahora = DateTime.Now; // cojo la fecha de hoy. 
                    ahora = ahora.AddDays(1);
                    auxFecha = ahora.GetDateTimeFormats(); // me da un array con la fecha de hoy en muchos formatos. 
                    fecha = auxFecha[0]; // la primera me da la fecha con el formato dd/mm/yyyy
                    datos.Close();
                    fechaAproximada(fecha, conexion);
                }
            }
            datos.Close();
            return fecha;
        }
    }
}
