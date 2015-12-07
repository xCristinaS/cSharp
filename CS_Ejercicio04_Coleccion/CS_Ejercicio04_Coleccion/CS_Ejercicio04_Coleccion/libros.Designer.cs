namespace CS_Ejercicio04_Coleccion {
    partial class Libros {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.misGeneros = new System.Windows.Forms.ComboBox();
            this.misLibros = new System.Windows.Forms.ListView();
            this.imgCerrar = new System.Windows.Forms.PictureBox();
            this.tienda = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.generosTienda = new System.Windows.Forms.ComboBox();
            this.eliminar = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buscadorTitulo = new System.Windows.Forms.TextBox();
            this.buscadorAutor = new System.Windows.Forms.TextBox();
            this.contextMenuStripMisLibros = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verDetallesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTienda = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verDetallesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.comprarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eliminar)).BeginInit();
            this.contextMenuStripMisLibros.SuspendLayout();
            this.contextMenuStripTienda.SuspendLayout();
            this.SuspendLayout();
            // 
            // misGeneros
            // 
            this.misGeneros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.misGeneros.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.misGeneros.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.misGeneros.FormattingEnabled = true;
            this.misGeneros.ItemHeight = 16;
            this.misGeneros.Location = new System.Drawing.Point(351, 38);
            this.misGeneros.Name = "misGeneros";
            this.misGeneros.Size = new System.Drawing.Size(121, 24);
            this.misGeneros.TabIndex = 0;
            this.misGeneros.SelectedIndexChanged += new System.EventHandler(this.comboGeneros_SelectedIndexChanged);
            // 
            // misLibros
            // 
            this.misLibros.BackgroundImageTiled = true;
            this.misLibros.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.misLibros.Location = new System.Drawing.Point(12, 68);
            this.misLibros.Name = "misLibros";
            this.misLibros.Size = new System.Drawing.Size(460, 570);
            this.misLibros.TabIndex = 2;
            this.misLibros.UseCompatibleStateImageBehavior = false;
            this.misLibros.ItemActivate += new System.EventHandler(this.lvLibros_ItemActivate);
            this.misLibros.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvLibros_ItemDrag);
            this.misLibros.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvLibros_DragDrop);
            this.misLibros.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvLibros_DragEnter);
            this.misLibros.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.Color.Transparent;
            this.imgCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgCerrar.Location = new System.Drawing.Point(1116, 3);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(30, 30);
            this.imgCerrar.TabIndex = 3;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // tienda
            // 
            this.tienda.BackgroundImageTiled = true;
            this.tienda.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tienda.Location = new System.Drawing.Point(678, 68);
            this.tienda.Name = "tienda";
            this.tienda.Size = new System.Drawing.Size(460, 570);
            this.tienda.TabIndex = 4;
            this.tienda.UseCompatibleStateImageBehavior = false;
            this.tienda.ItemActivate += new System.EventHandler(this.lvLibros_ItemActivate);
            this.tienda.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvLibros_ItemDrag);
            this.tienda.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvLibros_DragDrop);
            this.tienda.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvLibros_DragEnter);
            this.tienda.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mis Libros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(866, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 39);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tienda";
            // 
            // generosTienda
            // 
            this.generosTienda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.generosTienda.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.generosTienda.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generosTienda.FormattingEnabled = true;
            this.generosTienda.Location = new System.Drawing.Point(678, 38);
            this.generosTienda.Name = "generosTienda";
            this.generosTienda.Size = new System.Drawing.Size(121, 24);
            this.generosTienda.TabIndex = 8;
            this.generosTienda.SelectedIndexChanged += new System.EventHandler(this.comboGeneros_SelectedIndexChanged);
            // 
            // eliminar
            // 
            this.eliminar.BackColor = System.Drawing.Color.Transparent;
            this.eliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.eliminar.Location = new System.Drawing.Point(478, 462);
            this.eliminar.Name = "eliminar";
            this.eliminar.Size = new System.Drawing.Size(194, 176);
            this.eliminar.TabIndex = 11;
            this.eliminar.TabStop = false;
            this.eliminar.DragDrop += new System.Windows.Forms.DragEventHandler(this.eliminar_DragDrop);
            this.eliminar.DragEnter += new System.Windows.Forms.DragEventHandler(this.eliminar_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(505, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Buscar por título";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(505, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Buscar por autor";
            // 
            // buscadorTitulo
            // 
            this.buscadorTitulo.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscadorTitulo.Location = new System.Drawing.Point(491, 174);
            this.buscadorTitulo.Name = "buscadorTitulo";
            this.buscadorTitulo.Size = new System.Drawing.Size(170, 26);
            this.buscadorTitulo.TabIndex = 14;
            this.buscadorTitulo.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.buscadorTitulo.Enter += new System.EventHandler(this.buscador_FocusEnter);
            this.buscadorTitulo.Leave += new System.EventHandler(this.buscador_FocusLeave);
            // 
            // buscadorAutor
            // 
            this.buscadorAutor.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscadorAutor.Location = new System.Drawing.Point(491, 248);
            this.buscadorAutor.Name = "buscadorAutor";
            this.buscadorAutor.Size = new System.Drawing.Size(170, 26);
            this.buscadorAutor.TabIndex = 15;
            this.buscadorAutor.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.buscadorAutor.Enter += new System.EventHandler(this.buscador_FocusEnter);
            this.buscadorAutor.Leave += new System.EventHandler(this.buscador_FocusLeave);
            // 
            // contextMenuStripMisLibros
            // 
            this.contextMenuStripMisLibros.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verDetallesToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStripMisLibros.Name = "contextMenuStripMisLibros";
            this.contextMenuStripMisLibros.Size = new System.Drawing.Size(134, 48);
            // 
            // verDetallesToolStripMenuItem
            // 
            this.verDetallesToolStripMenuItem.Name = "verDetallesToolStripMenuItem";
            this.verDetallesToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.verDetallesToolStripMenuItem.Text = "Ver detalles";
            this.verDetallesToolStripMenuItem.Click += new System.EventHandler(this.verDetallesToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // contextMenuStripTienda
            // 
            this.contextMenuStripTienda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verDetallesToolStripMenuItem1,
            this.comprarToolStripMenuItem});
            this.contextMenuStripTienda.Name = "contextMenuStripTienda";
            this.contextMenuStripTienda.Size = new System.Drawing.Size(134, 48);
            // 
            // verDetallesToolStripMenuItem1
            // 
            this.verDetallesToolStripMenuItem1.Name = "verDetallesToolStripMenuItem1";
            this.verDetallesToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.verDetallesToolStripMenuItem1.Text = "Ver detalles";
            this.verDetallesToolStripMenuItem1.Click += new System.EventHandler(this.verDetallesToolStripMenuItem_Click);
            // 
            // comprarToolStripMenuItem
            // 
            this.comprarToolStripMenuItem.Name = "comprarToolStripMenuItem";
            this.comprarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.comprarToolStripMenuItem.Text = "Comprar";
            this.comprarToolStripMenuItem.Click += new System.EventHandler(this.comprarToolStripMenuItem_Click);
            // 
            // Libros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.buscadorAutor);
            this.Controls.Add(this.buscadorTitulo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.eliminar);
            this.Controls.Add(this.generosTienda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tienda);
            this.Controls.Add(this.imgCerrar);
            this.Controls.Add(this.misLibros);
            this.Controls.Add(this.misGeneros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Libros";
            this.Text = "SeleccionGenero";
            this.Load += new System.EventHandler(this.Libros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eliminar)).EndInit();
            this.contextMenuStripMisLibros.ResumeLayout(false);
            this.contextMenuStripTienda.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox misGeneros;
        private System.Windows.Forms.ListView misLibros;
        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.ListView tienda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox generosTienda;
        private System.Windows.Forms.PictureBox eliminar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox buscadorTitulo;
        private System.Windows.Forms.TextBox buscadorAutor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMisLibros;
        private System.Windows.Forms.ToolStripMenuItem verDetallesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTienda;
        private System.Windows.Forms.ToolStripMenuItem verDetallesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem comprarToolStripMenuItem;
    }
}