namespace GestionBiometrica
{
    partial class CajaFuerte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.lblIpCaja = new System.Windows.Forms.Label();
            this.txtIpCaja = new System.Windows.Forms.TextBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblIpCaja
            // 
            this.lblIpCaja.AutoSize = true;
            this.lblIpCaja.Location = new System.Drawing.Point(30, 30);
            this.lblIpCaja.Name = "lblIpCaja";
            this.lblIpCaja.Size = new System.Drawing.Size(120, 20);
            this.lblIpCaja.Text = "IP de la Caja Fuerte";

            // 
            // txtIpCaja
            // 
            this.txtIpCaja.Location = new System.Drawing.Point(30, 60);
            this.txtIpCaja.Name = "txtIpCaja";
            this.txtIpCaja.Size = new System.Drawing.Size(250, 27);

            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(30, 110);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(70, 20);
            this.lblSucursal.Text = "Sucursal";

            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.Location = new System.Drawing.Point(30, 140);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(250, 28);

            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(30, 200);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(100, 30);
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);

            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(180, 200);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 30);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.BtnVolver_Click);

            // 
            // CajaFuerteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 280);
            this.Controls.Add(this.lblIpCaja);
            this.Controls.Add(this.txtIpCaja);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.cbSucursal);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnVolver);
            this.Name = "CajaFuerteForm";
            this.Text = "Agregar Caja Fuerte";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblIpCaja;
        private System.Windows.Forms.TextBox txtIpCaja;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnVolver;
    }



    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>




}