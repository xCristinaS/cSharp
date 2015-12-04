using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CS_Ejercicio04_Coleccion {
    public partial class Libros : Form {

        private string usuario; // para saber que usuario inicio sesión. 
        private ImageList listaImgMisLibros = new ImageList();
        private ImageList listaImgTienda = new ImageList();
        private bool usandoBuscador = false;

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
            misLibros.LargeImageList.ImageSize = new Size(100, 135);
            tienda.AllowDrop = true;
            tienda.View = View.LargeIcon;
            tienda.LargeImageList = listaImgTienda;
            tienda.LargeImageList.ImageSize = new Size(100, 135);
            cargarGenerosCombo();
            this.BackgroundImage = Image.FromFile(Constantes.FONDO_FORM);
            imgCerrar.BackgroundImage = Image.FromFile(Constantes.BOTON_CERRAR);
            eliminar.BackgroundImage = Image.FromFile(Constantes.PAPELERA);
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
            misGeneros.DropDownHeight = misGeneros.ItemHeight * Constantes.ITEMS_COMBO_SCROLL;
            generosTienda.DropDownHeight = misGeneros.ItemHeight * Constantes.ITEMS_COMBO_SCROLL;
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void comboGeneros_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox combo = (ComboBox)sender;
            string genero = combo.SelectedItem.ToString();
            if (!usandoBuscador) {
                BddConection.cerrarConexion();
                cargarLibros(genero, combo);
                cargarFondoGenero(genero, combo);
            } else
                buscador_TextChanged(null, null);
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
            string titulo;
            if (((ListView)sender).SelectedItems.Count > 0) {
                titulo = ((ListViewItem)((ListView)sender).SelectedItems[0]).Text;
                this.Hide();
                DetallesLibro form3 = new DetallesLibro(usuario, titulo);
                form3.FormClosed += (s, args) => this.Show();
                form3.FormClosed += (s, args) => this.cargarTodosMisLibros();
                form3.Show();
            }
        }

        private void lvLibros_ItemDrag(object sender, ItemDragEventArgs e) {
            if (sender.Equals(tienda))
                eliminar.AllowDrop = false;
            else
                eliminar.AllowDrop = true;
            ((ListView)sender).DoDragDrop(((ListView)sender).SelectedItems, DragDropEffects.All);
        }

        private void lvLibros_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void lvLibros_DragDrop(object sender, DragEventArgs e) {
            ListView lista = (ListView) sender; bool guardar = true;
            ListView.SelectedListViewItemCollection objetos; 
            objetos = (ListView.SelectedListViewItemCollection) e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));

            cargarTodosMisLibros();
            foreach (ListViewItem it in objetos) {
                guardar = true;
                foreach (ListViewItem itEnLista in lista.Items)
                    if (it.Text.Equals(itEnLista.Text))
                        guardar = false;

                if (guardar) {
                    agregarLibroAMiColeccion(it.Text); 
                }
            }
            actualizarLibrosEnLista();
        }

        private void comprarLibros() {

        }

        private void actualizarLibrosEnLista() {
            if (misGeneros.SelectedIndex != generosTienda.SelectedIndex)
                misGeneros.SelectedIndex = generosTienda.SelectedIndex;
            else if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                cargarTodosMisLibros();
            else
                cargarMisLibros(misGeneros.SelectedItem.ToString());
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
            ListView.SelectedListViewItemCollection objetos;
            SqlConnection conexion = BddConection.newConnection();
            objetos = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            eliminarLibros(conexion, objetos);
            BddConection.closeConnection(conexion);
            actualizarLibrosEnLista();
        }
        
        private void eliminarLibros(SqlConnection conexion, ListView.SelectedListViewItemCollection objSeleccionados) {
            SqlCommand orden; string select; string titulo;
            foreach (ListViewItem it in objSeleccionados) {
                titulo = it.Text;
                select = string.Format("delete from libroUsu where titulo = '{0}' and nick = '{1}';", titulo, usuario);
                orden = new SqlCommand(select, conexion);
                orden.ExecuteScalar();
            }
        }

        private void cargarFondoGenero(string genero, ComboBox combo) {
            if (combo.Equals(misGeneros))
                misLibros.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero + Constantes.EXT_PNG);
            else 
                tienda.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero + Constantes.EXT_PNG);
        }

        private void buscador_TextChanged(object sender, EventArgs e) {
            string titulo = buscadorTitulo.Text, autor = buscadorAutor.Text;
            if (!titulo.Equals("") || !autor.Equals("")) {
                usandoBuscador = true;
                calcularSelectBuscador(titulo, autor);
            } else {
                usandoBuscador = false;
                if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                    cargarTodosMisLibros();
                else
                    cargarMisLibros(misGeneros.SelectedItem.ToString());

                if (generosTienda.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                    cargarTodosEnTienda();
                else 
                    cargarLibrosTienda(generosTienda.SelectedItem.ToString());
            }     
        }

        private void buscador_FocusEnter(object sender, EventArgs e) {
            BddConection.abrirConexion();
        }

        private void buscador_FocusLeave(object sender, EventArgs e) {
            if (!usandoBuscador)
                BddConection.cerrarConexion();
        }

        private void calcularSelectBuscador(string titulo, string autor) {
            if (generosTienda.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                BddConection.ejecutarSelectBuscador(string.Format("select titulo, imagenPortada from libro where titulo like '%{0}%' and autor like '%{1}%'", titulo, autor),tienda, listaImgTienda);
            else
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from libro l, libroGenero g where l.titulo = g.titulo and l.titulo like '%{0}%' and autor like '%{1}%' and genero = '{2}'", titulo, autor, generosTienda.SelectedItem.ToString()),tienda,listaImgTienda);

            if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from librousu u, libro l where u.titulo = l.titulo and l.titulo like '%{0}%' and autor like '%{1}%'", titulo, autor),misLibros,listaImgMisLibros);
            else
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from librousu u, libro l , libroGenero g where u.titulo = l.titulo and l.titulo = g.titulo and l.titulo like '%{0}%' and autor like '%{1}%' and genero = '{2}'", titulo, autor, misGeneros.SelectedItem.ToString()), misLibros, listaImgMisLibros);
        }

        private void verDetallesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (sender.Equals(contextMenuStripMisLibros.Items[0]))
                lvLibros_ItemActivate(misLibros, null);
            else
                lvLibros_ItemActivate(tienda, null);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) {
            SqlConnection conexion = BddConection.newConnection();
            eliminarLibros(conexion, misLibros.SelectedItems);
            BddConection.closeConnection(conexion);
            if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                cargarTodosMisLibros();
            else
                cargarMisLibros(misGeneros.SelectedItem.ToString());
        }

        private void comprarToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void ListView_MouseClick(object sender, MouseEventArgs e) {
            ListView lista = (ListView) sender;
            if (e.Button == MouseButtons.Right && lista.FocusedItem.Bounds.Contains(e.Location) == true) {
                if (lista.Equals(misLibros))
                    contextMenuStripMisLibros.Show(Cursor.Position);
                else
                    contextMenuStripTienda.Show(Cursor.Position);
            }
        }

    }
}
