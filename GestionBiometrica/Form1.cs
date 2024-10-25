using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GestionBiometrica
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Socket> clients = new Dictionary<string, Socket>();
        private bool serverRunning = false;
        private Thread serverThread;
        private Socket listener;
        private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
        private System.Windows.Forms.Timer uiUpdateTimer;

        public Form1()
        {
            InitializeComponent();
            StartServer();
            ConfigurarBotones();
        }

        private void ConfigurarBotones()
        {
            // Configurar los eventos de los nuevos botones
            btnEditar.Click += new EventHandler(btnEditar_Click);
            btnEliminar.Click += new EventHandler(btnEliminar_Click);

            // Mantener la configuración del botón de registro
            btnRegistrar.Click += new EventHandler(btnRegistrar_Click);
            btnVolver.Click += new EventHandler(btnVolver_Click);
        }

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, ingresa un nombre de usuario y contraseña.", "Campos Requeridos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userData = new UserCredentials
            {
                Username = username,
                Password = password
            };
            string jsonData = JsonConvert.SerializeObject(userData);

            EnviarDatosServidor(jsonData);
        }

        private void EnviarDatosServidor(string jsonData)
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

                txtUsuario.Clear();
                txtPassword.Clear();
                txtUsuario.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartServer()
        {
            if (!serverRunning)
            {
                serverThread = new Thread(RunServer);
                serverThread.Start();
                serverRunning = true;
            }
        }

        private void RunServer()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Any;
                int port = 55000;
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(ipAddress, port));
                listener.Listen(10);

                while (serverRunning)
                {
                    try
                    {
                        Socket handler = listener.Accept();
                        Thread clientThread = new Thread(() => HandleClient(handler));
                        clientThread.Start();
                    }
                    catch (SocketException)
                    {
                        if (!serverRunning) break;
                    }
                }
            }
            catch (SocketException)
            {
                // Manejo silencioso del error
            }
        }

        private void HandleClient(Socket handler)
        {
            string clientName = $"Dispositivo_{Guid.NewGuid().ToString().Substring(0, 4)}";

            lock (clients)
            {
                clients[clientName] = handler;
            }

            byte[] bytes = new byte[1024];
            while (serverRunning)
            {
                try
                {
                    int bytesRec = handler.Receive(bytes);
                    if (bytesRec == 0) break;

                    string data = Encoding.ASCII.GetString(bytes, 0, bytesRec).Trim();
                    if (data.ToLower() == "exit") break;
                }
                catch (SocketException)
                {
                    break;
                }
            }

            try
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch { }

            lock (clients)
            {
                clients.Remove(clientName);
            }
        }

        private void StopServer()
        {
            serverRunning = false;
            try
            {
                listener?.Close();
                foreach (var client in clients.Values)
                {
                    try
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                    }
                    catch { }
                }
                clients.Clear();
            }
            catch { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopServer();
        }

        private void btnVolver_Click(object? sender, EventArgs e)
        {
            Menu formRegistro = new Menu();
            formRegistro.Show();
            this.Hide();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnEditar_Click(object? sender, EventArgs e)
        {
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
        }

        private class UserCredentials
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
    }
}