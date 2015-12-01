namespace CS_Ejercicio04_Coleccion {
    partial class DetallesLibro {
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
            this.imgCerrar = new System.Windows.Forms.PictureBox();
            this.portada = new System.Windows.Forms.PictureBox();
            this.genero = new System.Windows.Forms.Label();
            this.titulo = new System.Windows.Forms.Label();
            this.autor = new System.Windows.Forms.Label();
            this.sipnosis = new System.Windows.Forms.Label();
            this.panelSinopsis = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portada)).BeginInit();
            this.panelSinopsis.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.Color.Transparent;
            this.imgCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgCerrar.Location = new System.Drawing.Point(720, 2);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(30, 30);
            this.imgCerrar.TabIndex = 5;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // portada
            // 
            this.portada.BackColor = System.Drawing.Color.Transparent;
            this.portada.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.portada.Location = new System.Drawing.Point(12, 12);
            this.portada.Name = "portada";
            this.portada.Size = new System.Drawing.Size(273, 420);
            this.portada.TabIndex = 8;
            this.portada.TabStop = false;
            // 
            // genero
            // 
            this.genero.AutoSize = true;
            this.genero.BackColor = System.Drawing.Color.Transparent;
            this.genero.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genero.Location = new System.Drawing.Point(373, 112);
            this.genero.Name = "genero";
            this.genero.Size = new System.Drawing.Size(30, 17);
            this.genero.TabIndex = 10;
            this.genero.Text = "labl";
            // 
            // titulo
            // 
            this.titulo.AutoSize = true;
            this.titulo.BackColor = System.Drawing.Color.Transparent;
            this.titulo.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titulo.Location = new System.Drawing.Point(301, 32);
            this.titulo.MaximumSize = new System.Drawing.Size(430, 56);
            this.titulo.MinimumSize = new System.Drawing.Size(430, 56);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(430, 56);
            this.titulo.TabIndex = 11;
            this.titulo.Text = "label2";
            // 
            // autor
            // 
            this.autor.AutoSize = true;
            this.autor.BackColor = System.Drawing.Color.Transparent;
            this.autor.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autor.Location = new System.Drawing.Point(357, 95);
            this.autor.Name = "autor";
            this.autor.Size = new System.Drawing.Size(43, 17);
            this.autor.TabIndex = 12;
            this.autor.Text = "label3";
            // 
            // sipnosis
            // 
            this.sipnosis.AutoSize = true;
            this.sipnosis.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sipnosis.Location = new System.Drawing.Point(3, 9);
            this.sipnosis.MaximumSize = new System.Drawing.Size(410, 0);
            this.sipnosis.MinimumSize = new System.Drawing.Size(3, 3);
            this.sipnosis.Name = "sipnosis";
            this.sipnosis.Size = new System.Drawing.Size(36, 15);
            this.sipnosis.TabIndex = 13;
            this.sipnosis.Text = "label1";
            // 
            // panelSinopsis
            // 
            this.panelSinopsis.BackColor = System.Drawing.Color.Transparent;
            this.panelSinopsis.Controls.Add(this.sipnosis);
            this.panelSinopsis.Location = new System.Drawing.Point(300, 141);
            this.panelSinopsis.MaximumSize = new System.Drawing.Size(439, 275);
            this.panelSinopsis.Name = "panelSinopsis";
            this.panelSinopsis.Size = new System.Drawing.Size(439, 275);
            this.panelSinopsis.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Género/s:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(305, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Autor:";
            // 
            // DetallesLibro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(753, 444);
            this.Controls.Add(this.panelSinopsis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.autor);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.genero);
            this.Controls.Add(this.portada);
            this.Controls.Add(this.imgCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(750, 750);
            this.Name = "DetallesLibro";
            this.Text = "DetallesLibro";
            this.Load += new System.EventHandler(this.DetallesLibro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portada)).EndInit();
            this.panelSinopsis.ResumeLayout(false);
            this.panelSinopsis.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.PictureBox portada;
        private System.Windows.Forms.Label genero;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label autor;
        private System.Windows.Forms.Label sipnosis;
        private System.Windows.Forms.Panel panelSinopsis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}