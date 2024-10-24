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
    public partial class EliminarUsuario : Form
    {
        private bool usuarioEncontrado = false;

        public EliminarUsuario()
        {
            InitializeComponent();
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
                    senderSocket.Send(msg);

                    byte[] bytes = new byte[1024];
                    int bytesRec = senderSocket.Receive(bytes);
                    string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    var result = JsonConvert.DeserializeObject<dynamic>(response);

                    if (result.status == "success")
                    {
                        usuarioEncontrado = true;
                        btnEliminar.Enabled = true;
                        MessageBox.Show("Usuario encontrado. Puede proceder a eliminarlo.", "Usuario Encontrado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnEliminar.Enabled = false;
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

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!usuarioEncontrado)
            {
                MessageBox.Show("Primero debe buscar un usuario", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar este usuario?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
                return;

            try
            {
                using (Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ipAddress = IPAddress.Parse("192.168.1.101");
                    await senderSocket.ConnectAsync(new IPEndPoint(ipAddress, 55000));

                    var deleteData = new
                    {
                        action = "eliminarUsuario",
                        nombre = txtNombre.Text
                    };

                    string jsonData = JsonConvert.SerializeObject(deleteData);
                    byte[] msg = Encoding.ASCII.GetBytes(jsonData + "\n");
                    await senderSocket.SendAsync(new ArraySegment<byte>(msg), SocketFlags.None);

                    byte[] bytes = new byte[1024];
                    ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
                    int bytesRec = await senderSocket.ReceiveAsync(buffer, SocketFlags.None);
                    string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    var result = JsonConvert.DeserializeObject<dynamic>(response);

                    if (result.status == "success")
                    {
                        MessageBox.Show("Usuario eliminado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar usuario", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    senderSocket.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            btnEliminar.Enabled = false;
            usuarioEncontrado = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 formPrincipal = new Form1();
            formPrincipal.Show();
            this.Close();
        }
    }
}
