namespace CS_Ejercicio04_Coleccion {
    partial class LeerLibro {
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
            this.navegador = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.Color.Transparent;
            this.imgCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgCerrar.Location = new System.Drawing.Point(1118, 1);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(30, 30);
            this.imgCerrar.TabIndex = 6;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // navegador
            // 
            this.navegador.Location = new System.Drawing.Point(12, 37);
            this.navegador.MinimumSize = new System.Drawing.Size(20, 20);
            this.navegador.Name = "navegador";
            this.navegador.Size = new System.Drawing.Size(1099, 574);
            this.navegador.TabIndex = 7;
            // 
            // LeerLibro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 623);
            this.Controls.Add(this.imgCerrar);
            this.Controls.Add(this.navegador);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LeerLibro";
            this.Text = "LeerLibro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LeerLibro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.WebBrowser navegador;
    }
}