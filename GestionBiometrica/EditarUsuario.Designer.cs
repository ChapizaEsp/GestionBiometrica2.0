namespace GestionBiometrica
{
    partial class EditarUsuario
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
            panelPrincipal = new Panel();
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnBuscar = new Button();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblSucursal = new Label();
            txtSucursal = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            btnGuardar = new Button();
            btnVolver = new Button();
            panelPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // panelPrincipal
            // 
            panelPrincipal.BackColor = Color.White;
            panelPrincipal.Controls.Add(lblTitulo);
            panelPrincipal.Controls.Add(lblNombre);
            panelPrincipal.Controls.Add(txtNombre);
            panelPrincipal.Controls.Add(btnBuscar);
            panelPrincipal.Controls.Add(lblPassword);
            panelPrincipal.Controls.Add(txtPassword);
            panelPrincipal.Controls.Add(lblSucursal);
            panelPrincipal.Controls.Add(txtSucursal);
            panelPrincipal.Controls.Add(lblDireccion);
            panelPrincipal.Controls.Add(txtDireccion);
            panelPrincipal.Controls.Add(btnGuardar);
            panelPrincipal.Controls.Add(btnVolver);
            panelPrincipal.Location = new Point(117, 23);
            panelPrincipal.Margin = new Padding(4, 3, 4, 3);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Size = new Size(700, 577);
            panelPrincipal.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitulo.Location = new Point(117, 35);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(467, 46);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Editar Usuario";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9.75F);
            lblNombre.ForeColor = Color.FromArgb(64, 64, 64);
            lblNombre.Location = new Point(58, 115);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(125, 17);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre de Usuario";
            // 
            // txtNombre
            // 
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 9.75F);
            txtNombre.Location = new Point(58, 144);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(466, 25);
            txtNombre.TabIndex = 2;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(0, 122, 204);
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(548, 144);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(93, 29);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.75F);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(58, 196);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(74, 17);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Contraseña";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 9.75F);
            txtPassword.Location = new Point(58, 225);
            txtPassword.Margin = new Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(583, 25);
            txtPassword.TabIndex = 5;
            // 
            // lblSucursal
            // 
            lblSucursal.AutoSize = true;
            lblSucursal.Font = new Font("Segoe UI", 9.75F);
            lblSucursal.ForeColor = Color.FromArgb(64, 64, 64);
            lblSucursal.Location = new Point(58, 277);
            lblSucursal.Margin = new Padding(4, 0, 4, 0);
            lblSucursal.Name = "lblSucursal";
            lblSucursal.Size = new Size(56, 17);
            lblSucursal.TabIndex = 6;
            lblSucursal.Text = "Sucursal";
            // 
            // txtSucursal
            // 
            txtSucursal.BorderStyle = BorderStyle.FixedSingle;
            txtSucursal.Font = new Font("Segoe UI", 9.75F);
            txtSucursal.Location = new Point(58, 306);
            txtSucursal.Margin = new Padding(4, 3, 4, 3);
            txtSucursal.Name = "txtSucursal";
            txtSucursal.Size = new Size(583, 25);
            txtSucursal.TabIndex = 7;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 9.75F);
            lblDireccion.ForeColor = Color.FromArgb(64, 64, 64);
            lblDireccion.Location = new Point(58, 358);
            lblDireccion.Margin = new Padding(4, 0, 4, 0);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(131, 17);
            lblDireccion.TabIndex = 8;
            lblDireccion.Text = "Dirección Caja Fuerte";
            // 
            // txtDireccion
            // 
            txtDireccion.BorderStyle = BorderStyle.FixedSingle;
            txtDireccion.Font = new Font("Segoe UI", 9.75F);
            txtDireccion.Location = new Point(58, 387);
            txtDireccion.Margin = new Padding(4, 3, 4, 3);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(583, 25);
            txtDireccion.TabIndex = 9;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(0, 150, 136);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(58, 450);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(583, 46);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "GUARDAR CAMBIOS";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(64, 64, 64);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(58, 513);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(583, 46);
            btnVolver.TabIndex = 11;
            btnVolver.Text = "VOLVER AL MENÚ";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // EditarUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(933, 635);
            Controls.Add(panelPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "EditarUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Seguridad - Editar Usuario";
            panelPrincipal.ResumeLayout(false);
            panelPrincipal.PerformLayout();
            ResumeLayout(false);
        }

        // Método para gestionar el evento del botón Volver
        //private void btnVolver_Click(object sender, EventArgs e)
        //{
        //    Form1 formPrincipal = new Form1(); // Crear una nueva instancia de Form1
        //    formPrincipal.Show();              // Mostrar el formulario principal
        //    this.Close();                      // Cerrar el formulario actual para liberar recursos
        //}

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnBuscar;
    }
}