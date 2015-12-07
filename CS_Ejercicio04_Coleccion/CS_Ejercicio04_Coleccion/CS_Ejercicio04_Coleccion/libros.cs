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
            // cargo los géneros en los combobox.
            string select = "select distinct genero from LibroGenero;"; // selecciono los diferentes géneros. 
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();

            while (datos.Read()) { // voy leyendo los generos y
                misGeneros.Items.Add(datos.GetString(0)); // los agrego al combo de misGeneros.
                generosTienda.Items.Add(datos.GetString(0)); // los agrego al combo de generosTienda. 
            }
            misGeneros.Items.Add(Constantes.MOSTRAR_TODOS); // después de leer todos los generos, agrego una opción que es "mostrar todos"
            generosTienda.Items.Add(Constantes.MOSTRAR_TODOS);
            misGeneros.SelectedIndex = misGeneros.Items.Count-1; // por defecto el combobox aparecerá con la opción de "mostrar todos" seleccionada.
            generosTienda.SelectedIndex = generosTienda.Items.Count - 1;
            misGeneros.DropDownHeight = misGeneros.ItemHeight * Constantes.ITEMS_COMBO_SCROLL; // le pongo scroll a la lista de géneros, para que sólo se puedan mostrar los indicados en la constante.
            generosTienda.DropDownHeight = misGeneros.ItemHeight * Constantes.ITEMS_COMBO_SCROLL;
            datos.Close();
            BddConection.closeConnection(conexion);
        }

        private void comboGeneros_SelectedIndexChanged(object sender, EventArgs e) {
            // cuando se cambia el indice de los combobox, es que hemos cambiado de genero, entonces
            ComboBox combo = (ComboBox)sender;
            string genero = combo.SelectedItem.ToString();
            if (!usandoBuscador) { // si no estoy usando el buscador
                BddConection.cerrarConexion();
                cargarLibros(genero, combo); // cargo los libros de ese genero
                cargarFondoGenero(genero, combo); // cargo el fondo de ese género
            } else
                buscador_TextChanged(null, null); // si uso el buscador, te lo explico más abajo (línea 220).
        }

        private void cargarLibros(string genero, ComboBox combo) {
            if (!genero.Equals(Constantes.MOSTRAR_TODOS)) { // si el género es diferente a "mostrar todos"
                if (combo.Equals(misGeneros)) // si el combo es "misGeneros"
                    cargarMisLibros(genero); // cargo los libros de ese género en el listView de mi colección
                else // en caso contrario
                    cargarLibrosTienda(genero); // cargo los libros de ese género en la tienda. 
            } else { // si el género es "mostrar todos"
                if (combo.Equals(misGeneros)) // y el combo es "misGeneros"
                    cargarTodosMisLibros(); // cargo todos los libros de la colección del usuario en el ListView de su colección.
                else // en caso contrario
                    cargarTodosEnTienda(); // cargo todos los libros en el ListView de la tienda. 
            }
        }

        private void cargarTodosMisLibros() { // cargo todos los libros del usuario
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from libroUsu where nick = '{0}');", usuario);
            traerLibrosDeBdd(select, misLibros, listaImgMisLibros);  // ejecutará la select y cargará los libros en la ListView "misLibros"
        }

        private void cargarTodosEnTienda() { // cargo todos los libros de la tienda.
            string select = "select titulo, imagenPortada from libro;";
            traerLibrosDeBdd(select, tienda, listaImgTienda);
        }

        private void cargarMisLibros(string genero) { // cargo todos los libros que tenga el usuario de un determinado género.
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}') and titulo in (select titulo from libroUsu where nick = '{1}')", genero, usuario);
            traerLibrosDeBdd(select, misLibros, listaImgMisLibros);
        }

        private void cargarLibrosTienda(string genero) { // cargo todos los libros de la tienda de un determinado género. 
            string select = string.Format("select titulo, imagenPortada from libro where titulo in (select titulo from LibroGenero where genero = '{0}')", genero);
            traerLibrosDeBdd(select, tienda, listaImgTienda);
        }

        private void traerLibrosDeBdd(string select, ListView lista, ImageList imagenes) { // ejecuta la select que le llega y la carga en el ListView correspondiente.
            string titulo, imagen; int cont = 0;
            SqlConnection conexion = BddConection.newConnection();
            SqlCommand orden = new SqlCommand(select, conexion);
            SqlDataReader datos = orden.ExecuteReader();
            imagenes.Images.Clear(); lista.Items.Clear();
            while (datos.Read()) { 
                ListViewItem item = new ListViewItem();
                titulo = datos.GetString(0); // cojo el título
                imagen = datos.GetString(1); // cojo la imagen
                imagenes.Images.Add(Image.FromFile(Constantes.RUTA_RECURSOS + imagen + Constantes.EXT_JPG)); // meto la imagen en la lista de imagenes
                item.ImageIndex = cont; // el índice de la imagen lo igualo al contador, para que la primera imagen insertada se corresponda con el 1er objeto y así sucesivamente. 
                item.Text = titulo; // el texto del item del listview lleva el titulo del libro
                lista.Items.Add(item); // agrego el item al listView. 
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
            // cuando selecciono un item, lo cojo de la lista de item seleccionados y lanzo el formulario de detalles de ese libro. 
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
            if (sender.Equals(tienda)) // cuando arrastro un elemento, si el elemento es un item de la tienda, deshabilito el borrar
                eliminar.AllowDrop = false;
            else // en caso contrario habilito el borrado. 
                eliminar.AllowDrop = true;
            ((ListView)sender).DoDragDrop(((ListView)sender).SelectedItems, DragDropEffects.All);
        }

        private void lvLibros_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void lvLibros_DragDrop(object sender, DragEventArgs e) {
            ListView.SelectedListViewItemCollection objetos; 
            objetos = (ListView.SelectedListViewItemCollection) e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)); // Cojo los libros que seleccione y arrasté desde la tienda
            SqlConnection conexion = BddConection.newConnection();
            cargarTodosMisLibros(); // cargo todos mis libros, para que haya una correspondencia directa con la base de datos. 
            comprarLibros(objetos, conexion); // compro los libros arrastrados (los incorporo a mi colección).
            BddConection.closeConnection(conexion);
        }

        private void comprarLibros(ListView.SelectedListViewItemCollection objSeleccionados, SqlConnection conexion) {
            bool guardar = true;
            foreach (ListViewItem it in objSeleccionados) { // voy recorriendo los objetos seleccionados, que son los que me han llegado desde la tienda. 
                guardar = true;
                foreach (ListViewItem itEnLista in misLibros.Items) // recorro mis libros, que están actualizados con la bdd porque acabo de cargarlos todos. 
                    if (it.Text.Equals(itEnLista.Text)) // si los títulos de los libros son iguales, no dejo guardarlo.
                        guardar = false;

                if (guardar) { // si llego aquí es porque no tengo ese libro en mi colección
                    agregarLibroAMiColeccion(it.Text, conexion); // agrego el libro en mi colección
                    actualizarLibrosEnLista(); // actualizo la lista de libros de mi colección
                }
            }
        }

        private void actualizarLibrosEnLista() { // para hacer que los libros que aparecen en la lista son los que tengo en la bdd. 
            if (misGeneros.SelectedIndex != generosTienda.SelectedIndex) // si los indices de la tienda son diferentes
                misGeneros.SelectedIndex = generosTienda.SelectedIndex; // hago que el índice de mis libros sea el mismo que el de la tienda, para que cuando compre uno "romántico" por ejemplo, se me muestren los que tengo de ese genero, y de además los actualiza. 
            else if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS)) // si el genero que tengo seleccionado es el de mostrar todos
                cargarTodosMisLibros(); // cargo todos mis libros
            else // si no se da ninguna de las dos opciones anteriores, cargo los libros del género seleccionado.
                cargarMisLibros(misGeneros.SelectedItem.ToString());
        }

        private void agregarLibroAMiColeccion(string libro, SqlConnection conexion) { // inserto el libro en la colección del usuario. 
            string select = string.Format("insert into libroUsu Values ('{0}', '{1}')", usuario, libro);
            SqlCommand orden = new SqlCommand(select, conexion);
            orden.ExecuteScalar();
        }

        private void eliminar_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void eliminar_DragDrop(object sender, DragEventArgs e) { // cuando llega un libro a la papelera, lo elimino. 
            ListView.SelectedListViewItemCollection objetos;
            SqlConnection conexion = BddConection.newConnection();
            objetos = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)); // cojo los objetos seleccionados que le llegan a la papelera desde mis libros.
            eliminarLibros(conexion, objetos); // los elimino.
            BddConection.closeConnection(conexion);
            actualizarLibrosEnLista();
        }
        
        private void eliminarLibros(SqlConnection conexion, ListView.SelectedListViewItemCollection objSeleccionados) {
            SqlCommand orden; string select; string titulo;
            foreach (ListViewItem it in objSeleccionados) { // cada libro que llega, lo borro de la bdd.
                titulo = it.Text;
                select = string.Format("delete from libroUsu where titulo = '{0}' and nick = '{1}';", titulo, usuario);
                orden = new SqlCommand(select, conexion);
                orden.ExecuteScalar();
            }
        }

        private void cargarFondoGenero(string genero, ComboBox combo) { // selecciono el fondo del ListView en función del género seleccionado en la lista correspondiente. 
            if (combo.Equals(misGeneros)) // si el combo es mis géneros, pongo el fondo en misLibros. 
                misLibros.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero + Constantes.EXT_PNG);
            else // en caso contrario lo pongo en la tienda. 
                tienda.BackgroundImage = Image.FromFile(Constantes.RUTA_RECURSOS + genero + Constantes.EXT_PNG);
        }

        private void buscador_TextChanged(object sender, EventArgs e) {
            string titulo = buscadorTitulo.Text, autor = buscadorAutor.Text;
            if (!titulo.Equals("") || !autor.Equals("")) { // si los textbox de titulo y autor tienen contenido
                usandoBuscador = true; // indico que estoy usando el buscador
                calcularSelectBuscador(titulo, autor); // creo la select que debo ejecutar
            } else { // en caso contrario
                usandoBuscador = false; // indico que no estoy usando el buscador y cargo los libros en funcion de los géneros seleccionados. 
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
            BddConection.abrirConexion(); // abro conexion
        }

        private void buscador_FocusLeave(object sender, EventArgs e) {
            if (!usandoBuscador) // cuando el buscador pierde el foco y he dejado de usar el buscador, cierro la conexion
                BddConection.cerrarConexion();
        }

        private void calcularSelectBuscador(string titulo, string autor) { // monto la select en función del titulo, autor y género seleccionado y la ejecuto. 
            if (generosTienda.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                BddConection.ejecutarSelectBuscador(string.Format("select titulo, imagenPortada from libro where titulo like '%{0}%' and autor like '%{1}%'", titulo, autor),tienda, listaImgTienda);
            else
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from libro l, libroGenero g where l.titulo = g.titulo and l.titulo like '%{0}%' and autor like '%{1}%' and genero = '{2}'", titulo, autor, generosTienda.SelectedItem.ToString()),tienda,listaImgTienda);

            if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS))
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from librousu u, libro l where u.titulo = l.titulo and l.titulo like '%{0}%' and autor like '%{1}%'", titulo, autor),misLibros,listaImgMisLibros);
            else
                BddConection.ejecutarSelectBuscador(string.Format("select l.titulo, imagenPortada from librousu u, libro l , libroGenero g where u.titulo = l.titulo and l.titulo = g.titulo and l.titulo like '%{0}%' and autor like '%{1}%' and genero = '{2}'", titulo, autor, misGeneros.SelectedItem.ToString()), misLibros, listaImgMisLibros);
        }

        private void verDetallesToolStripMenuItem_Click(object sender, EventArgs e) { // si hago clic en el item de ver detalles
            if (sender.Equals(contextMenuStripMisLibros.Items[0])) // si el sender es el contextMenuStrip de misLibros
                lvLibros_ItemActivate(misLibros, null); // cargo la vista detalles del item seleccionado en el listview de misLibros
            else // en caso contrario, lo cargo de la tienda
                lvLibros_ItemActivate(tienda, null);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) { // si hago clic en el item de eliminar del contextMenuStrip de mis libros
            SqlConnection conexion = BddConection.newConnection();
            eliminarLibros(conexion, misLibros.SelectedItems); // elimino los libros seleccionados en el listview de mi coleccion
            BddConection.closeConnection(conexion);
            if (misGeneros.SelectedItem.ToString().Equals(Constantes.MOSTRAR_TODOS)) // actualizo los libros de mi lista. 
                cargarTodosMisLibros();
            else
                cargarMisLibros(misGeneros.SelectedItem.ToString());
        }

        private void comprarToolStripMenuItem_Click(object sender, EventArgs e) { // si hago clic en el item de comprar del contextMenuStrip de la tienda
            SqlConnection conexion = BddConection.newConnection();
            comprarLibros(tienda.SelectedItems, conexion); // compro los libros seleccionados de la tienda. 
            BddConection.closeConnection(conexion);
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e) { // cuando hago clic con el botón derecho en un listView, muestro su correspondiente contextMenuStrip.
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
