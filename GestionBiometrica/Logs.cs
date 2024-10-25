using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiometrica
{
    public partial class Logs : Form
    {
        private Socket listener;
        private bool isListening = true;
        private readonly List<LogEntry> logEntries = new List<LogEntry>();
        private readonly object lockObject = new object();
        private const int LOG_PORT = 55000;  // Puerto 55000, igual que en el ESP32

        public class LogEntry
        {
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Operacion { get; set; }
            public string Estado { get; set; }
            public string Detalles { get; set; }
        }

        public Logs()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            IniciarEscuchaLogs();

            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today;
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Menu formPrincipal = new Menu();
            formPrincipal.Show();
            this.Close();
        }
        private void ConfigurarDataGridView()
        {
            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvLogs.RowsDefaultCellStyle.BackColor = Color.White;
            dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.CellFormatting += DgvLogs_CellFormatting;
        }

        private void DgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvLogs.Columns["colEstado"].Index && e.Value != null)
            {
                string estado = e.Value.ToString().ToLower();
                switch (estado)
                {
                    case "exitoso":
                    case "success":
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "fallido":
                    case "error":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.DarkGray;
                        break;
                }
            }
        }

        private void IniciarEscuchaLogs()
        {
            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, LOG_PORT);
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Thread listenerThread = new Thread(EscucharLogs)
                {
                    IsBackground = true
                };
                listenerThread.Start();

                Console.WriteLine($"Servidor de logs iniciado en puerto {LOG_PORT}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar el servidor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EscucharLogs()
        {
            while (isListening)
            {
                try
                {
                    Socket handler = listener.Accept();
                    StringBuilder messageBuilder = new StringBuilder();
                    byte[] buffer = new byte[1024];
                    bool headersParsed = false;
                    int contentLength = 0;

                    // Leer headers HTTP
                    while (true)
                    {
                        int bytesRec = handler.Receive(buffer);
                        if (bytesRec <= 0) break;

                        string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                        messageBuilder.Append(chunk);

                        // Buscar el final de los headers HTTP
                        if (!headersParsed && chunk.Contains("\r\n\r\n"))
                        {
                            string headers = messageBuilder.ToString();
                            var match = Regex.Match(headers, @"Content-Length:\s*(\d+)", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                contentLength = int.Parse(match.Groups[1].Value);
                                headersParsed = true;
                            }

                            // Extraer el cuerpo del mensaje
                            int bodyStart = headers.IndexOf("\r\n\r\n") + 4;
                            string initialBody = headers.Substring(bodyStart);
                            messageBuilder.Clear();
                            messageBuilder.Append(initialBody);

                            if (messageBuilder.Length >= contentLength)
                                break;
                        }
                        else if (headersParsed && messageBuilder.Length >= contentLength)
                        {
                            break;
                        }
                    }

                    if (messageBuilder.Length > 0)
                    {
                        string jsonBody = messageBuilder.ToString().Trim();
                        Console.WriteLine($"Datos recibidos: {jsonBody}");
                        ProcesarMensaje(jsonBody);

                        // Enviar respuesta HTTP
                        string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 2\r\n\r\nOK";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        handler.Send(responseBytes);
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (SocketException ex)
                {
                    if (isListening)
                    {
                        Console.WriteLine($"Error en la conexión: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error general: {ex.Message}");
                }
            }
        }

        private void ProcesarMensaje(string mensaje)
        {
            try
            {
                dynamic jsonData = JsonConvert.DeserializeObject(mensaje);

                if (jsonData?.type?.ToString() == "log")
                {
                    var logEntry = new LogEntry
                    {
                        Fecha = DateTime.Now,
                        Hora = DateTime.Now.ToString("HH:mm:ss"),
                        Operacion = jsonData.message?.ToString() ?? "Sin operación",
                        Estado = "Exitoso",  // Puedes ajustar el estado según sea necesario
                        Detalles = DeterminarDetalles(jsonData.message?.ToString() ?? "")
                    };

                    lock (lockObject)
                    {
                        logEntries.Add(logEntry);
                    }

                    this.Invoke((MethodInvoker)ActualizarGrid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar mensaje: {ex.Message}\nMensaje recibido: {mensaje}");
            }
        }

        private string DeterminarDetalles(string mensaje)
        {
            if (string.IsNullOrEmpty(mensaje))
                return "Operación desconocida";

            if (mensaje.Contains("Puerta"))
                return "Estado de la puerta";
            if (mensaje.Contains("Perilla"))
                return "Estado de la perilla";
            if (mensaje.Contains("Caja"))
                return "Operación de caja fuerte";
            if (mensaje.Contains("Huella"))
                return "Registro biométrico";
            if (mensaje.Contains("Usuario"))
                return "Gestión de usuarios";
            if (mensaje.Contains("Sistema"))
                return "Operación del sistema";
            return "Operación general";
        }

        private void ActualizarGrid()
        {
            try
            {
                dgvLogs.Rows.Clear();
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                lock (lockObject)
                {
                    var registrosFiltrados = logEntries
                        .Where(log => log.Fecha >= fechaInicio && log.Fecha <= fechaFin)
                        .OrderByDescending(log => log.Fecha)
                        .ToList();

                    foreach (var log in registrosFiltrados)
                    {
                        dgvLogs.Rows.Add(
                            log.Fecha.ToShortDateString(),
                            log.Hora,
                            log.Operacion,
                            log.Estado,
                            log.Detalles
                        );
                    }
                }

                if (dgvLogs.Rows.Count > 0)
                {
                    dgvLogs.FirstDisplayedScrollingRowIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la tabla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isListening = false;
            if (listener != null)
            {
                try
                {
                    listener.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cerrar el listener: {ex.Message}");
                }
            }
            base.OnFormClosing(e);
        }



        private void Logs_Load(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            Menu formRegistro = new Menu();
            formRegistro.Show();
            this.Hide(); // Oculta el menú mientras se muestra el formulario de registro
        }
    }
}
