namespace ExamenT1CristinaSola {
    partial class FormRaton {
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
            this.lbHoy = new System.Windows.Forms.ListBox();
            this.lbMañana = new System.Windows.Forms.ListBox();
            this.lbPasado = new System.Windows.Forms.ListBox();
            this.textoHoy = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Label();
            this.t = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHoy
            // 
            this.lbHoy.FormattingEnabled = true;
            this.lbHoy.Location = new System.Drawing.Point(202, 64);
            this.lbHoy.Name = "lbHoy";
            this.lbHoy.Size = new System.Drawing.Size(270, 433);
            this.lbHoy.TabIndex = 0;
            // 
            // lbMañana
            // 
            this.lbMañana.FormattingEnabled = true;
            this.lbMañana.Location = new System.Drawing.Point(513, 64);
            this.lbMañana.Name = "lbMañana";
            this.lbMañana.Size = new System.Drawing.Size(270, 433);
            this.lbMañana.TabIndex = 1;
            // 
            // lbPasado
            // 
            this.lbPasado.FormattingEnabled = true;
            this.lbPasado.Location = new System.Drawing.Point(824, 64);
            this.lbPasado.Name = "lbPasado";
            this.lbPasado.Size = new System.Drawing.Size(270, 433);
            this.lbPasado.TabIndex = 2;
            // 
            // textoHoy
            // 
            this.textoHoy.AutoSize = true;
            this.textoHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textoHoy.Location = new System.Drawing.Point(300, 32);
            this.textoHoy.Name = "textoHoy";
            this.textoHoy.Size = new System.Drawing.Size(55, 29);
            this.textoHoy.TabIndex = 3;
            this.textoHoy.Text = "Hoy";
            // 
            // text
            // 
            this.text.AutoSize = true;
            this.text.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text.Location = new System.Drawing.Point(574, 32);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(98, 29);
            this.text.TabIndex = 4;
            this.text.Text = "Mañana";
            // 
            // t
            // 
            this.t.AutoSize = true;
            this.t.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t.Location = new System.Drawing.Point(916, 32);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(95, 29);
            this.t.TabIndex = 5;
            this.t.Text = "Pasado";
            // 
            // FormRaton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 614);
            this.Controls.Add(this.t);
            this.Controls.Add(this.text);
            this.Controls.Add(this.textoHoy);
            this.Controls.Add(this.lbPasado);
            this.Controls.Add(this.lbMañana);
            this.Controls.Add(this.lbHoy);
            this.Name = "FormRaton";
            this.Text = "FormRaton";
            this.Load += new System.EventHandler(this.FormRaton_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbHoy;
        private System.Windows.Forms.ListBox lbMañana;
        private System.Windows.Forms.ListBox lbPasado;
        private System.Windows.Forms.Label textoHoy;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.Label t;
    }
}