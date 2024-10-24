namespace GestionBiometrica
{
    partial class Menu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnRegistro = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // Panel Principal
            this.panelPrincipal.BackColor = System.Drawing.Color.White;
            this.panelPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPrincipal.Size = new System.Drawing.Size(500, 600);
            this.panelPrincipal.Location = new System.Drawing.Point(150, 20);

            // PictureBox para logo
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.Location = new System.Drawing.Point(190, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            /*this.pictureBox1.Image = Properties.Resources.safe_icon; */// Necesitarás agregar un icono

            // Título
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(130, 170);
            this.lblTitulo.Size = new System.Drawing.Size(250, 45);
            this.lblTitulo.Text = "Sistema de Seguridad";
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

            // Descripción
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDescripcion.Location = new System.Drawing.Point(100, 230);
            this.lblDescripcion.Size = new System.Drawing.Size(300, 40);
            this.lblDescripcion.Text = "Gestión de usuarios y monitoreo de accesos";
            this.lblDescripcion.ForeColor = System.Drawing.Color.Gray;
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Botón Registro
            this.btnRegistro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegistro.ForeColor = System.Drawing.Color.White;
            this.btnRegistro.Location = new System.Drawing.Point(100, 300);
            this.btnRegistro.Size = new System.Drawing.Size(300, 60);
            this.btnRegistro.Text = "REGISTRO DE USUARIOS";
            this.btnRegistro.UseVisualStyleBackColor = false;
            this.btnRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistro.Click += new System.EventHandler(this.btnRegistro_Click);

            // Botón Logs
            this.btnLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogs.ForeColor = System.Drawing.Color.White;
            this.btnLogs.Location = new System.Drawing.Point(100, 380);
            this.btnLogs.Size = new System.Drawing.Size(300, 60);
            this.btnLogs.Text = "REGISTROS DE ACTIVIDAD";
            this.btnLogs.UseVisualStyleBackColor = false;
            this.btnLogs.Cursor = System.Windows.Forms.Cursors.Hand;
             this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            // Agregar controles al panel
            this.panelPrincipal.Controls.Add(this.pictureBox1);
            this.panelPrincipal.Controls.Add(this.lblTitulo);
            this.panelPrincipal.Controls.Add(this.lblDescripcion);
            this.panelPrincipal.Controls.Add(this.btnRegistro);
            this.panelPrincipal.Controls.Add(this.btnLogs);

            // Agregar panel al formulario
            this.Controls.Add(this.panelPrincipal);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Seguridad - Menú Principal";

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Button btnRegistro;
        private System.Windows.Forms.Button btnLogs;
    }
}