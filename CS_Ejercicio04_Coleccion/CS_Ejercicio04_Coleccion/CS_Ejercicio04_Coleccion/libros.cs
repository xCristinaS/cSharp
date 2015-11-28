using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CS_Ejercicio04_Coleccion {
    public partial class Libros : Form {

        private string usuario; // para saber que usuario inicio sesión. 
        private ImageList listaImgMisLibros = new ImageList();
        private ImageList listaImgTienda = new ImageList();

        public Libros(string usuario) {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void Libros_Load(object sender, EventArgs e) {
            CenterToScreen();
            eliminar.AllowDrop = true;
            misLibros.AllowDrop= true;
            misLibros.View = View.LargeIcon;
            misLibros.LargeImageList = listaImgMisLibros;
            listaImgTienda.ImageSize = new Size(85, 115);
            tienda.AllowDrop = true;
            tienda.View = View.LargeIcon;
            tienda.LargeImageList = listaImgTienda;
            listaImgMisLibros.ImageSize = new Size(85, 115);
            cargarGenerosCombo();
        }

        private void cargarGenerosCombo() {
            string select = "select distinct genero from LibroGenero;";
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            while (datos.Read()) {
                misGeneros.Items.Add(datos.GetString(0));
                generosTienda.Items.Add(datos.GetString(0));
            }
            misGeneros.Items.Add(Constantes.MOSTRAR_TODOS);
            generosTienda.Items.Add(Constantes.MOSTRAR_TODOS);
            misGeneros.SelectedIndex = misGeneros.Items.Count-1;
            generosTienda.SelectedIndex = generosTienda.Items.Count - 1;
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void comboGeneros_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox combo = (ComboBox)sender;
            string genero = combo.SelectedItem.ToString();
            cargarLibros(genero, combo);
        }

        private void cargarLibros(string genero, ComboBox combo) {
            if (!genero.Equals(Constantes.MOSTRAR_TODOS)) {
                if (combo.Equals(misGeneros))
                    cargarMisLibros(genero);
                else
                    cargarLibrosTienda(genero);
            } else {
                if (combo.Equals(misGeneros))
                    cargarTodosMisLibros();
                else
                    cargarTodosEnTienda();
            }
        }

        private void cargarTodosMisLibros() {
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from libroUsu where nick = '{0}');", usuario);
            traerLibrosDeBdd(select, misLibros, listaImgMisLibros);
        }

        private void cargarTodosEnTienda() {
            string select = "select titulo, imagenPortada from libro;";
            traerLibrosDeBdd(select, tienda, listaImgTienda);
        }

        private void cargarMisLibros(string genero) {
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}') and titulo in (select titulo from libroUsu where nick = '{1}')", genero, usuario);
            traerLibrosDeBdd(select, misLibros, listaImgMisLibros);
        }

        private void cargarLibrosTienda(string genero) {
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}')", genero);
            traerLibrosDeBdd(select, tienda, listaImgTienda);
        }

        private void traerLibrosDeBdd(string select, ListView lista, ImageList imagenes) {
            string titulo, imagen; int cont = 0;
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            imagenes.Images.Clear(); lista.Items.Clear();
            while (datos.Read()) {
                ListViewItem item = new ListViewItem();
                titulo = datos.GetString(0);
                imagen = datos.GetString(1);
                imagenes.Images.Add(Image.FromFile(Constantes.RUTA_RECURSOS + imagen + Constantes.EXT_JPG));
                item.ImageIndex = cont;
                item.Text = titulo;
                lista.Items.Add(item);
                cont++;
            }
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void lvLibros_ItemActivate(object sender, EventArgs e) {
            string titulo = ((ListViewItem) ((ListView)sender).SelectedItems[0]).Text;
            this.Hide();
            DetallesLibro form3 = new DetallesLibro(usuario, titulo);
            form3.FormClosed += (s, args) => this.Show();
            form3.Show();
        }

        private void lvLibros_ItemDrag(object sender, ItemDragEventArgs e) {
            ((ListView)sender).DoDragDrop(((ListView)sender).SelectedItems, DragDropEffects.All);
        }

        private void lvLibros_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void lvLibros_DragDrop(object sender, DragEventArgs e) {
            ListView lista = (ListView) sender; bool guardar = true; int cont = 0;
            ListView.SelectedListViewItemCollection objetos; ListViewItem item; System.Collections.IEnumerator enumerator;
            objetos = (ListView.SelectedListViewItemCollection) e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            
            foreach (ListViewItem it in objetos) {
                guardar = true;
                foreach (ListViewItem itEnLista in lista.Items)
                    if (it.Text.Equals(itEnLista.Text))
                        guardar = false;

                if (guardar) {
                    item = new ListViewItem(); // creo el nuevo item. 
                    item.Text = it.Text; // le asigno el título del libro que voy a agregar a la colección.
                    enumerator = listaImgTienda.Images.GetEnumerator(); // saco un iterador de la lista de imagenes de la tienda. 
                    enumerator.MoveNext();
                    while (cont < it.ImageIndex) { // recorro el iterador hasta encontrar la imagen de la portada asociada al título. 
                        cont++;
                        enumerator.MoveNext();
                    } 
                    listaImgMisLibros.Images.Add((Image)enumerator.Current); // agrego la imagen a la lista de imagenes de mi colección. 
                    item.ImageIndex = listaImgMisLibros.Images.Count - 1; // hago que la nueva imagen apunte a su portada. 
                    lista.Items.Add(item); // agrego el item a la lista. 
                    agregarLibroAMiColeccion(it.Text); // inserto el libro en la bdd. 
                }
            }
        }
        private void agregarLibroAMiColeccion(string libro) {
            string select = string.Format("insert into libroUsu Values ('{0}', '{1}')", usuario, libro);
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
            BddConection.closeConnection(conexion);
        }

        private void eliminar_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void eliminar_DragDrop(object sender, DragEventArgs e) {
            string titulo; string select;
            ListView.SelectedListViewItemCollection objetos;
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden;

            objetos = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            foreach (ListViewItem it in objetos) {
                titulo = it.Text;
                select = string.Format("delete from libroUsu where titulo = '{0}' and nick = '{1}';", titulo, usuario);
                orden = new SqlCommand(select, conexion);
                orden.ExecuteScalar();

                foreach (ListViewItem itemEnLista in misLibros.Items)
                    if (it.Text.Equals(itemEnLista.Text))
                        misLibros.Items.Remove(itemEnLista);
            }
            BddConection.closeConnection(conexion);
        }
    }
}
