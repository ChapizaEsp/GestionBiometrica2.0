using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiometrica
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Form1 formRegistro = new Form1();
            formRegistro.Show();
            this.Hide(); // Oculta el menú mientras se muestra el formulario de registro
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            Logs formLogs = new Logs();
            formLogs.Show();
            this.Hide(); // Oculta el menú mientras se muestra el formulario de logs
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit(); // Asegura que la aplicación se cierre completamente
        }

    }
}
