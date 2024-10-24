using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiometrica
{
    public partial class EditarUsuario : Form
    {
        private bool usuarioEncontrado = false;

        public EditarUsuario()
        {
            InitializeComponent();
            ConfigurarCamposIniciales();
        }

        private void ConfigurarCamposIniciales()
        {
            // Deshabilitar campos hasta que se encuentre un usuario
            txtPassword.Enabled = false;
            txtSucursal.Enabled = false;
            txtDireccion.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ipAddress = IPAddress.Parse("192.168.1.101");
                    await Task.Factory.FromAsync(
                        senderSocket.BeginConnect(ipAddress, 55000, null, null),
                        senderSocket.EndConnect);

                    var searchData = new
                    {
                        action = "buscarUsuario",
                        usuario = txtNombre.Text
                    };

                    string jsonData = JsonConvert.SerializeObject(searchData);
                    byte[] msg = Encoding.ASCII.GetBytes(jsonData + "\n");
                    senderSocket.Send(msg); // Usando Send en lugar de SendAsync

                    byte[] bytes = new byte[1024];
                    int bytesRec = senderSocket.Receive(bytes); // Usando Receive en lugar de ReceiveAsync
                    string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    var result = JsonConvert.DeserializeObject<dynamic>(response);

                    if (result.status == "success")
                    {
                        // Llenar campos con datos del usuario
                        txtPassword.Text = result.password;
                        txtSucursal.Text = result.sucursal ?? "";
                        txtDireccion.Text = result.direccion ?? "";

                        // Habilitar campos para edición
                        txtPassword.Enabled = true;
                        txtSucursal.Enabled = true;
                        txtDireccion.Enabled = true;
                        btnGuardar.Enabled = true;
                        usuarioEncontrado = true;

                        MessageBox.Show("Usuario encontrado", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    senderSocket.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!usuarioEncontrado)
            {
                MessageBox.Show("Primero debe buscar un usuario", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtSucursal.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Todos los campos son requeridos", "Campos Incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ipAddress = IPAddress.Parse("192.168.1.101");
                    await Task.Factory.FromAsync(
                        senderSocket.BeginConnect(ipAddress, 55000, null, null),
                        senderSocket.EndConnect);

                    var updateData = new
                    {
                        action = "editarUsuario",
                        nombreOriginal = txtNombre.Text,
                        password = txtPassword.Text,
                        sucursal = txtSucursal.Text,
                        direccion = txtDireccion.Text
                    };

                    string jsonData = JsonConvert.SerializeObject(updateData);
                    byte[] msg = Encoding.ASCII.GetBytes(jsonData + "\n");
                    await senderSocket.SendAsync(new ArraySegment<byte>(msg), SocketFlags.None);

                    byte[] bytes = new byte[1024];
                    ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
                    int bytesRec = await senderSocket.ReceiveAsync(buffer, SocketFlags.None);
                    string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    var result = JsonConvert.DeserializeObject<dynamic>(response);

                    if (result.status == "success")
                    {
                        MessageBox.Show("Usuario actualizado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar usuario", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    senderSocket.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtPassword.Clear();
            txtSucursal.Clear();
            txtDireccion.Clear();
            ConfigurarCamposIniciales();
            usuarioEncontrado = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 formPrincipal = new Form1();
            formPrincipal.Show();
            this.Close(); // Esto cerrará el formulario actual
        }
    }
}
