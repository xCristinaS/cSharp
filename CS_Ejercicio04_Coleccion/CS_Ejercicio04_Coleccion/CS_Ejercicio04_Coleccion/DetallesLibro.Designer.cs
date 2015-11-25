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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portada)).BeginInit();
            this.SuspendLayout();
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imgCerrar.Location = new System.Drawing.Point(380, 12);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(18, 21);
            this.imgCerrar.TabIndex = 5;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // portada
            // 
            this.portada.BackColor = System.Drawing.Color.Transparent;
            this.portada.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.portada.Location = new System.Drawing.Point(24, 49);
            this.portada.Name = "portada";
            this.portada.Size = new System.Drawing.Size(153, 224);
            this.portada.TabIndex = 8;
            this.portada.TabStop = false;
            // 
            // genero
            // 
            this.genero.AutoSize = true;
            this.genero.Location = new System.Drawing.Point(226, 100);
            this.genero.Name = "genero";
            this.genero.Size = new System.Drawing.Size(35, 13);
            this.genero.TabIndex = 10;
            this.genero.Text = "label1";
            // 
            // titulo
            // 
            this.titulo.AutoSize = true;
            this.titulo.Location = new System.Drawing.Point(226, 49);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(35, 13);
            this.titulo.TabIndex = 11;
            this.titulo.Text = "label2";
            // 
            // autor
            // 
            this.autor.AutoSize = true;
            this.autor.Location = new System.Drawing.Point(226, 73);
            this.autor.Name = "autor";
            this.autor.Size = new System.Drawing.Size(35, 13);
            this.autor.TabIndex = 12;
            this.autor.Text = "label3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            // 
            // DetallesLibro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 363);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autor);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.genero);
            this.Controls.Add(this.portada);
            this.Controls.Add(this.imgCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DetallesLibro";
            this.Text = "DetallesLibro";
            this.Load += new System.EventHandler(this.DetallesLibro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.PictureBox portada;
        private System.Windows.Forms.Label genero;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label autor;
        private System.Windows.Forms.Label label1;
    }
}