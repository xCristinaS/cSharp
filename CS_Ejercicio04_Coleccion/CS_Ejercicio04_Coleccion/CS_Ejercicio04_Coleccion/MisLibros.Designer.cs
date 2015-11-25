namespace CS_Ejercicio04_Coleccion {
    partial class MisLibros {
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
            this.lvLibros = new System.Windows.Forms.ListView();
            this.imgCerrar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // comboGeneros
            // 
            this.comboGeneros.FormattingEnabled = true;
            this.comboGeneros.Location = new System.Drawing.Point(73, 12);
            this.comboGeneros.Name = "comboGeneros";
            this.comboGeneros.Size = new System.Drawing.Size(121, 21);
            this.comboGeneros.TabIndex = 0;
            this.comboGeneros.SelectedIndexChanged += new System.EventHandler(this.comboGeneros_SelectedIndexChanged);
            // 
            // lvLibros
            // 
            this.lvLibros.Location = new System.Drawing.Point(39, 68);
            this.lvLibros.MultiSelect = false;
            this.lvLibros.Name = "lvLibros";
            this.lvLibros.Size = new System.Drawing.Size(272, 130);
            this.lvLibros.TabIndex = 2;
            this.lvLibros.UseCompatibleStateImageBehavior = false;
            this.lvLibros.ItemActivate += new System.EventHandler(this.lvLibros_ItemActivate);
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imgCerrar.Location = new System.Drawing.Point(326, 12);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(18, 21);
            this.imgCerrar.TabIndex = 3;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // MisLibros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 413);
            this.Controls.Add(this.imgCerrar);
            this.Controls.Add(this.lvLibros);
            this.Controls.Add(this.comboGeneros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MisLibros";
            this.Text = "SeleccionGenero";
            this.Load += new System.EventHandler(this.MisLibros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboGeneros;
        private System.Windows.Forms.ListView lvLibros;
        private System.Windows.Forms.PictureBox imgCerrar;
    }
}