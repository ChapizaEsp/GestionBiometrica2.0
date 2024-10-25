using GestionBiometrica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace GestionBiometrica
{

    public partial class CajaFuerte : Form
    {
        private readonly Context context;
        private List<(int id, string nombre, string direccion)> sucursales;
        private const string ESP32_IP = "192.168.1.200";
        private const int ESP32_PORT = 80;

        public CajaFuerte()
        {
            InitializeComponent();
            context = new Context();
            CargarSucursales();
        }

        private void CargarSucursales()
        {
            try
            {
                // Obtener las sucursales de la base de datos
                sucursales = context.ObtenerSucursales();

                // Limpiar items existentes
                cbSucursal.Items.Clear();

                // Configurar el ComboBox
                var items = new List<ComboBoxItem>();
                foreach (var sucursal in sucursales)
                {
                    items.Add(new ComboBoxItem
                    {
                        Id = sucursal.id,
                        Nombre = sucursal.nombre
                    });
                }

                cbSucursal.DataSource = items;
                cbSucursal.DisplayMember = "Nombre";
                cbSucursal.ValueMember = "Id";
                cbSucursal.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las sucursales: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSucursal.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona una sucursal.",
                        "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ipCaja = txtIpCaja.Text.Trim();
                if (string.IsNullOrWhiteSpace(ipCaja))
                {
                    MessageBox.Show("Por favor, ingresa la IP de la caja fuerte.",
                        "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarFormatoIP(ipCaja))
                {
                    MessageBox.Show("Por favor, ingresa una dirección IP válida (ejemplo: 192.168.1.1)",
                        "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = (ComboBoxItem)cbSucursal.SelectedItem;
                int idSucursal = selectedItem.Id;

                if (context.GuardarCajaFuerte(ipCaja, idSucursal))
                {
                    MessageBox.Show("Caja Fuerte registrada correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la caja fuerte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clase auxiliar para los items del ComboBox
        private void LimpiarCampos()
        {
            txtIpCaja.Clear();
            cbSucursal.SelectedIndex = -1;
            txtIpCaja.Focus();
        }

        private bool ValidarFormatoIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) return false;
            string[] octetos = ip.Split('.');
            if (octetos.Length != 4) return false;
            return octetos.All(octeto =>
                byte.TryParse(octeto, out byte num) && num >= 0 && num <= 255);
        }

        private class ComboBoxItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public override string ToString()
            {
                return Nombre;
            }
        }

        private bool SendDataToServer(string jsonData)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    // Intentar conectar con timeout de 5 segundos
                    var result = client.BeginConnect(ESP32_IP, ESP32_PORT, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

                    if (!success)
                    {
                        throw new Exception("No se pudo establecer conexión con el dispositivo.");
                    }

                    client.EndConnect(result);

                    // Preparar los datos a enviar
                    byte[] data = Encoding.UTF8.GetBytes(jsonData);

                    // Obtener el stream para enviar los datos
                    using (NetworkStream stream = client.GetStream())
                    {
                        // Enviar los datos
                        stream.Write(data, 0, data.Length);
                        stream.Flush();

                        // Esperar respuesta (opcional)
                        byte[] responseBuffer = new byte[1024];
                        int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                        string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

                        // Aquí podrías procesar la respuesta si el ESP32 envía alguna
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar datos al dispositivo: {ex.Message}",
                    "Error de Comunicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Menu formMenu = new Menu();
            formMenu.Show();
            this.Hide();
        }
    }
}