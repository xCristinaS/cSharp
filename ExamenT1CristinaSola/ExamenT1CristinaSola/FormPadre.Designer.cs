namespace ExamenT1CristinaSola {
    partial class FormPadre {
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
            this.comboHijos = new System.Windows.Forms.ComboBox();
            this.imgDiente = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewEnBoca = new System.Windows.Forms.ListView();
            this.listViewCaidos = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewTickets = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.imgDiente)).BeginInit();
            this.SuspendLayout();
            // 
            // comboHijos
            // 
            this.comboHijos.FormattingEnabled = true;
            this.comboHijos.Location = new System.Drawing.Point(725, 30);
            this.comboHijos.Name = "comboHijos";
            this.comboHijos.Size = new System.Drawing.Size(121, 21);
            this.comboHijos.TabIndex = 0;
            this.comboHijos.SelectedIndexChanged += new System.EventHandler(this.comboHijos_SelectedIndexChanged);
            // 
            // imgDiente
            // 
            this.imgDiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgDiente.Location = new System.Drawing.Point(48, 47);
            this.imgDiente.Name = "imgDiente";
            this.imgDiente.Size = new System.Drawing.Size(274, 330);
            this.imgDiente.TabIndex = 1;
            this.imgDiente.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "En boca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(328, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Caidos";
            // 
            // listViewEnBoca
            // 
            this.listViewEnBoca.Location = new System.Drawing.Point(423, 104);
            this.listViewEnBoca.Name = "listViewEnBoca";
            this.listViewEnBoca.Size = new System.Drawing.Size(480, 97);
            this.listViewEnBoca.TabIndex = 4;
            this.listViewEnBoca.UseCompatibleStateImageBehavior = false;
            this.listViewEnBoca.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.listViewEnBoca.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.listViewEnBoca.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            // 
            // listViewCaidos
            // 
            this.listViewCaidos.Location = new System.Drawing.Point(423, 271);
            this.listViewCaidos.Name = "listViewCaidos";
            this.listViewCaidos.Size = new System.Drawing.Size(480, 97);
            this.listViewCaidos.TabIndex = 5;
            this.listViewCaidos.UseCompatibleStateImageBehavior = false;
            this.listViewCaidos.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.listViewCaidos.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.listViewCaidos.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 380);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tickets";
            // 
            // listViewTickets
            // 
            this.listViewTickets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewTickets.Location = new System.Drawing.Point(48, 412);
            this.listViewTickets.Name = "listViewTickets";
            this.listViewTickets.Size = new System.Drawing.Size(855, 117);
            this.listViewTickets.TabIndex = 8;
            this.listViewTickets.UseCompatibleStateImageBehavior = false;
            this.listViewTickets.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nombre";
            this.columnHeader1.Width = 328;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Diente";
            this.columnHeader2.Width = 411;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Fecha";
            // 
            // FormPadre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(954, 553);
            this.Controls.Add(this.listViewTickets);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listViewCaidos);
            this.Controls.Add(this.listViewEnBoca);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgDiente);
            this.Controls.Add(this.comboHijos);
            this.Name = "FormPadre";
            this.Load += new System.EventHandler(this.FormPadre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgDiente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboHijos;
        private System.Windows.Forms.PictureBox imgDiente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewEnBoca;
        private System.Windows.Forms.ListView listViewCaidos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listViewTickets;
    }
}