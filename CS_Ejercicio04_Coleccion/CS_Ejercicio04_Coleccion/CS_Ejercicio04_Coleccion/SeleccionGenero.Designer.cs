namespace CS_Ejercicio04_Coleccion {
    partial class SeleccionGenero {
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
            this.listaLibros = new System.Windows.Forms.ListBox();
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
            // listaLibros
            // 
            this.listaLibros.FormattingEnabled = true;
            this.listaLibros.Location = new System.Drawing.Point(12, 60);
            this.listaLibros.Name = "listaLibros";
            this.listaLibros.Size = new System.Drawing.Size(334, 342);
            this.listaLibros.TabIndex = 1;
            // 
            // SeleccionGenero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 413);
            this.Controls.Add(this.listaLibros);
            this.Controls.Add(this.comboGeneros);
            this.Name = "SeleccionGenero";
            this.Text = "SeleccionGenero";
            this.Load += new System.EventHandler(this.SeleccionGenero_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboGeneros;
        private System.Windows.Forms.ListBox listaLibros;
    }
}