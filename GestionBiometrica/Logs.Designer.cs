namespace GestionBiometrica
{
    partial class Logs
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
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            btnFiltrar = new Button();
            dgvLogs = new DataGridView();
            colFecha = new DataGridViewTextBoxColumn();
            colHora = new DataGridViewTextBoxColumn();
            colOperacion = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colDetalles = new DataGridViewTextBoxColumn();
            btnVolver = new Button();
            lblTotalRegistros = new Label();
            panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // panelPrincipal
            // 
            panelPrincipal.BackColor = Color.White;
            panelPrincipal.Controls.Add(lblTitulo);
            panelPrincipal.Controls.Add(lblFechaInicio);
            panelPrincipal.Controls.Add(dtpFechaInicio);
            panelPrincipal.Controls.Add(lblFechaFin);
            panelPrincipal.Controls.Add(dtpFechaFin);
            panelPrincipal.Controls.Add(btnFiltrar);
            panelPrincipal.Controls.Add(dgvLogs);
            panelPrincipal.Controls.Add(btnVolver);
            panelPrincipal.Controls.Add(lblTotalRegistros);
            panelPrincipal.Location = new Point(23, 23);
            panelPrincipal.Margin = new Padding(4, 3, 4, 3);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Padding = new Padding(23, 23, 23, 23);
            panelPrincipal.Size = new Size(887, 623);
            panelPrincipal.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitulo.Location = new Point(23, 23);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(467, 46);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registro de Actividades";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblFechaInicio.Location = new Point(23, 92);
            lblFechaInicio.Margin = new Padding(4, 0, 4, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(74, 15);
            lblFechaInicio.TabIndex = 1;
            lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Font = new Font("Segoe UI", 9F);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(117, 90);
            dtpFechaInicio.Margin = new Padding(4, 3, 4, 3);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(139, 23);
            dtpFechaInicio.TabIndex = 2;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblFechaFin.Location = new Point(280, 92);
            lblFechaFin.Margin = new Padding(4, 0, 4, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(60, 15);
            lblFechaFin.TabIndex = 3;
            lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Font = new Font("Segoe UI", 9F);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(362, 90);
            dtpFechaFin.Margin = new Padding(4, 3, 4, 3);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(139, 23);
            dtpFechaFin.TabIndex = 4;
            // 
            // btnFiltrar
            // 
            btnFiltrar.BackColor = Color.FromArgb(0, 122, 204);
            btnFiltrar.Cursor = Cursors.Hand;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Location = new Point(525, 89);
            btnFiltrar.Margin = new Padding(4, 3, 4, 3);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(117, 29);
            btnFiltrar.TabIndex = 5;
            btnFiltrar.Text = "FILTRAR";
            btnFiltrar.UseVisualStyleBackColor = false;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.ColumnHeadersHeight = 30;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] { colFecha, colHora, colOperacion, colEstado, colDetalles });
            dgvLogs.Location = new Point(23, 138);
            dgvLogs.Margin = new Padding(4, 3, 4, 3);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(840, 404);
            dgvLogs.TabIndex = 6;
            // 
            // colFecha
            // 
            colFecha.HeaderText = "Fecha";
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // colHora
            // 
            colHora.HeaderText = "Hora";
            colHora.Name = "colHora";
            colHora.ReadOnly = true;
            // 
            // colOperacion
            // 
            colOperacion.HeaderText = "Operación";
            colOperacion.Name = "colOperacion";
            colOperacion.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colDetalles
            // 
            colDetalles.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetalles.HeaderText = "Detalles";
            colDetalles.Name = "colDetalles";
            colDetalles.ReadOnly = true;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(64, 64, 64);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(770, 565);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(93, 35);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "VOLVER";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click_1;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI", 9F);
            lblTotalRegistros.Location = new Point(23, 565);
            lblTotalRegistros.Margin = new Padding(4, 0, 4, 0);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(92, 15);
            lblTotalRegistros.TabIndex = 8;
            lblTotalRegistros.Text = "Total registros: 0";
            // 
            // Logs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(933, 669);
            Controls.Add(panelPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Logs";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Seguridad - Registro de Actividades";
            panelPrincipal.ResumeLayout(false);
            panelPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalles;
    }
}