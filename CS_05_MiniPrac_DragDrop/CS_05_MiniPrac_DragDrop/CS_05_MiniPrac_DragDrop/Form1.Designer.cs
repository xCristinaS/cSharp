namespace CS_05_MiniPrac_DragDrop {
    partial class Form1 {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(162, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.textBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            this.textBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(34, 112);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.textBox3.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            this.textBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(14, 154);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 3;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(152, 154);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 95);
            this.listBox2.TabIndex = 4;
            this.listBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.listBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(327, 152);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(121, 97);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.itemDrag);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(500, 152);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(121, 97);
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDrop);
            this.listView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 371);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
    }
}

