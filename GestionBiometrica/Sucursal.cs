using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;

using GestionBiometrica;

namespace GestionBiometrica
{
    public partial class Sucursal : Form
    {
        private Context context;

        public Sucursal()
        {
            InitializeComponent();
            context = new Context();

        }

        // Evento para el botón Registrar


        private void SendDataToServer(string jsonData)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("192.168.1.101");
                int port = 55000;

                using (Socket senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    senderSocket.Connect(new IPEndPoint(ipAddress, port));
                    byte[] msg = Encoding.ASCII.GetBytes(jsonData + "\n");
                    senderSocket.Send(msg);
                    senderSocket.Shutdown(SocketShutdown.Both);
                }

                MessageBox.Show("Usuario registrado correctamente", "Registro Exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar datos al ESP32: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para el botón Volver al Menú
        private void BtnVolver_Click(object sender, EventArgs e)
        {
            this.Close(); // Volver al menú principal (cerrar la ventana)
        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            Menu formMenu = new Menu();
            formMenu.Show();
            this.Hide();

        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            // Obtener los valores de los campos de texto
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Por favor, ingresa el nombre y la dirección de la sucursal.",
                    "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardar en la base de datos usando el contexto (context.GuardarSucursal)
            if (context.GuardarSucursal(nombre, direccion))  // Ajusta según los parámetros que necesites
            {
                // Si se guardó correctamente en la base de datos, se envían los datos al servidor o ESP32
                var sucursalData = new
                {
                    Nombre = nombre,
                    Direccion = direccion,

                };

                // Serializar los datos a JSON
                string jsonData = JsonConvert.SerializeObject(sucursalData);

                // Enviar datos al servidor o ESP32
                SendDataToServer(jsonData);

                // Limpiar los campos de texto y hacer foco en el primero
                txtNombre.Clear();
                txtDireccion.Clear();
                txtNombre.Focus();

                MessageBox.Show("Sucursal registrada correctamente y datos enviados.",
                    "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Si no se pudo guardar, mostrar un mensaje de error
                MessageBox.Show("Error al registrar la sucursal. Inténtalo de nuevo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
