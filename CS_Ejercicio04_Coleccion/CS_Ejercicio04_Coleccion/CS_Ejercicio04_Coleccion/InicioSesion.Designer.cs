namespace CS_Ejercicio04_Coleccion {
    partial class FormInicioSesion {
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
            this.btnAceptar = new System.Windows.Forms.PictureBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.panelNuevoUsu = new System.Windows.Forms.Panel();
            this.wrong3 = new System.Windows.Forms.PictureBox();
            this.wrong2 = new System.Windows.Forms.PictureBox();
            this.wrong1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newPassRepeat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.newPass = new System.Windows.Forms.TextBox();
            this.newNick = new System.Windows.Forms.TextBox();
            this.inicioSesion = new System.Windows.Forms.Label();
            this.registrarse = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).BeginInit();
            this.panelLogin.SuspendLayout();
            this.panelNuevoUsu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wrong3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wrong2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wrong1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgCerrar
            // 
            this.imgCerrar.BackColor = System.Drawing.Color.Transparent;
            this.imgCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCerrar.Location = new System.Drawing.Point(250, 2);
            this.imgCerrar.Name = "imgCerrar";
            this.imgCerrar.Size = new System.Drawing.Size(30, 30);
            this.imgCerrar.TabIndex = 4;
            this.imgCerrar.TabStop = false;
            this.imgCerrar.Click += new System.EventHandler(this.imgCerrar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Transparent;
            this.btnAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAceptar.Location = new System.Drawing.Point(92, 214);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(97, 35);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.TabStop = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Transparent;
            this.panelLogin.Controls.Add(this.label2);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.txtClave);
            this.panelLogin.Controls.Add(this.txtUsuario);
            this.panelLogin.Location = new System.Drawing.Point(33, 61);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(207, 122);
            this.panelLogin.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nick";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(59, 97);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 12;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(59, 44);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 23);
            this.txtUsuario.TabIndex = 11;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            // 
            // panelNuevoUsu
            // 
            this.panelNuevoUsu.BackColor = System.Drawing.Color.Transparent;
            this.panelNuevoUsu.Controls.Add(this.wrong3);
            this.panelNuevoUsu.Controls.Add(this.wrong2);
            this.panelNuevoUsu.Controls.Add(this.wrong1);
            this.panelNuevoUsu.Controls.Add(this.label5);
            this.panelNuevoUsu.Controls.Add(this.newPassRepeat);
            this.panelNuevoUsu.Controls.Add(this.label3);
            this.panelNuevoUsu.Controls.Add(this.label4);
            this.panelNuevoUsu.Controls.Add(this.newPass);
            this.panelNuevoUsu.Controls.Add(this.newNick);
            this.panelNuevoUsu.Location = new System.Drawing.Point(12, 61);
            this.panelNuevoUsu.Name = "panelNuevoUsu";
            this.panelNuevoUsu.Size = new System.Drawing.Size(236, 147);
            this.panelNuevoUsu.TabIndex = 13;
            // 
            // wrong3
            // 
            this.wrong3.BackColor = System.Drawing.Color.Transparent;
            this.wrong3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wrong3.Location = new System.Drawing.Point(185, 125);
            this.wrong3.Name = "wrong3";
            this.wrong3.Size = new System.Drawing.Size(20, 20);
            this.wrong3.TabIndex = 23;
            this.wrong3.TabStop = false;
            // 
            // wrong2
            // 
            this.wrong2.BackColor = System.Drawing.Color.Transparent;
            this.wrong2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wrong2.Location = new System.Drawing.Point(185, 76);
            this.wrong2.Name = "wrong2";
            this.wrong2.Size = new System.Drawing.Size(20, 20);
            this.wrong2.TabIndex = 22;
            this.wrong2.TabStop = false;
            // 
            // wrong1
            // 
            this.wrong1.BackColor = System.Drawing.Color.Transparent;
            this.wrong1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wrong1.Location = new System.Drawing.Point(185, 23);
            this.wrong1.Name = "wrong1";
            this.wrong1.Size = new System.Drawing.Size(20, 20);
            this.wrong1.TabIndex = 21;
            this.wrong1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(80, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Repita Contraseña";
            // 
            // newPassRepeat
            // 
            this.newPassRepeat.Location = new System.Drawing.Point(80, 125);
            this.newPassRepeat.Name = "newPassRepeat";
            this.newPassRepeat.PasswordChar = '*';
            this.newPassRepeat.Size = new System.Drawing.Size(100, 20);
            this.newPassRepeat.TabIndex = 19;
            this.newPassRepeat.TextChanged += new System.EventHandler(this.newUsu_TextChange);
            this.newPassRepeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newUsu_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nueva Contraseña";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(80, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Nuevo Nick";
            // 
            // newPass
            // 
            this.newPass.Location = new System.Drawing.Point(80, 76);
            this.newPass.Name = "newPass";
            this.newPass.PasswordChar = '*';
            this.newPass.Size = new System.Drawing.Size(100, 20);
            this.newPass.TabIndex = 16;
            this.newPass.TextChanged += new System.EventHandler(this.newUsu_TextChange);
            this.newPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newUsu_KeyPress);
            // 
            // newNick
            // 
            this.newNick.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newNick.Location = new System.Drawing.Point(80, 23);
            this.newNick.Name = "newNick";
            this.newNick.Size = new System.Drawing.Size(100, 23);
            this.newNick.TabIndex = 15;
            this.newNick.TextChanged += new System.EventHandler(this.newUsu_TextChange);
            this.newNick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newUsu_KeyPress);
            // 
            // inicioSesion
            // 
            this.inicioSesion.AutoSize = true;
            this.inicioSesion.BackColor = System.Drawing.Color.Transparent;
            this.inicioSesion.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inicioSesion.Location = new System.Drawing.Point(42, 32);
            this.inicioSesion.Name = "inicioSesion";
            this.inicioSesion.Size = new System.Drawing.Size(88, 17);
            this.inicioSesion.TabIndex = 14;
            this.inicioSesion.Text = "Iniciar Sesión";
            this.inicioSesion.Click += new System.EventHandler(this.inicioSesion_Click);
            // 
            // registrarse
            // 
            this.registrarse.AutoSize = true;
            this.registrarse.BackColor = System.Drawing.Color.Transparent;
            this.registrarse.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrarse.Location = new System.Drawing.Point(169, 32);
            this.registrarse.Name = "registrarse";
            this.registrarse.Size = new System.Drawing.Size(71, 17);
            this.registrarse.TabIndex = 15;
            this.registrarse.Text = "Registrarse";
            this.registrarse.Click += new System.EventHandler(this.registrarse_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // FormInicioSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panelNuevoUsu);
            this.Controls.Add(this.registrarse);
            this.Controls.Add(this.inicioSesion);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.imgCerrar);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormInicioSesion";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.InicioSesion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelNuevoUsu.ResumeLayout(false);
            this.panelNuevoUsu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wrong3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wrong2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wrong1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox imgCerrar;
        private System.Windows.Forms.PictureBox btnAceptar;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Panel panelNuevoUsu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox newPassRepeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox newPass;
        private System.Windows.Forms.TextBox newNick;
        private System.Windows.Forms.PictureBox wrong3;
        private System.Windows.Forms.PictureBox wrong2;
        private System.Windows.Forms.PictureBox wrong1;
        private System.Windows.Forms.Label inicioSesion;
        private System.Windows.Forms.Label registrarse;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
    }
}

