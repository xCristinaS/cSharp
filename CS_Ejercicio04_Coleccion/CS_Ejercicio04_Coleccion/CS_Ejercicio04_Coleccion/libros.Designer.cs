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
            this.comboGeneros = new System.Windows.Forms.ComboBox();
            this.misLibros = new System.Windows.Forms.ListView();
            this.imgCerrar = new System.Windows.Forms.PictureBox();
            this.tienda = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // comboGeneros
            // 
            this.comboGeneros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGeneros.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboGeneros.FormattingEnabled = true;
            this.comboGeneros.Location = new System.Drawing.Point(297, 27);
            this.comboGeneros.Name = "comboGeneros";
            this.comboGeneros.Size = new System.Drawing.Size(121, 24);
            this.comboGeneros.TabIndex = 0;
            this.comboGeneros.SelectedIndexChanged += new System.EventHandler(this.comboGeneros_SelectedIndexChanged);
            // 
            // misLibros
            // 
            this.misLibros.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.misLibros.Location = new System.Drawing.Point(12, 68);
            this.misLibros.Name = "misLibros";
            this.misLibros.Size = new System.Drawing.Size(299, 333);
            this.misLibros.TabIndex = 2;
            this.misLibros.UseCompatibleStateImageBehavior = false;
            this.misLibros.ItemActivate += new System.EventHandler(this.lvLibros_ItemActivate);
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imgCerrar.Location = new System.Drawing.Point(689, 12);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(18, 21);
            this.imgCerrar.TabIndex = 3;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // tienda
            // 
            this.tienda.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tienda.Location = new System.Drawing.Point(408, 68);
            this.tienda.Name = "tienda";
            this.tienda.Size = new System.Drawing.Size(299, 333);
            this.tienda.TabIndex = 4;
            this.tienda.UseCompatibleStateImageBehavior = false;
            this.tienda.ItemActivate += new System.EventHandler(this.lvLibros_ItemActivate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mis Libros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(503, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 39);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tienda";
            // 
            // Libros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 413);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tienda);
            this.Controls.Add(this.imgCerrar);
            this.Controls.Add(this.misLibros);
            this.Controls.Add(this.comboGeneros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Libros";
            this.Text = "SeleccionGenero";
            this.Load += new System.EventHandler(this.Libros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboGeneros;
        private System.Windows.Forms.ListView misLibros;
        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.ListView tienda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}