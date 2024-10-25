using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionBiometrica
{
    internal class Context
    {
        private readonly string connectionString;

        public Context()
        {
            connectionString = @"Data Source=DESKTOP-I8ELGSJ\SQLEXPRESS;Initial Catalog=GestionDeSeguridad;Integrated Security=True";
        }

        public bool GuardarUsuario(string usuario, string password, int sucursalId, int cajaId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (UsuarioExiste(usuario, connection))
                    {
                        MessageBox.Show("El usuario ya existe en la base de datos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    string insertQuery = @"INSERT INTO Usuario (nombre, password, id_sucursal, id_caja, fecha_registro) 
                                         VALUES (@nombre, @password, @id_sucursal, @id_caja, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@nombre", usuario);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@id_sucursal", sucursalId);
                        cmd.Parameters.AddWithValue("@id_caja", cajaId);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en la base de datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool UsuarioExiste(string usuario, SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Usuario WHERE nombre = @nombre";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nombre", usuario);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ValidarUsuario(string usuario, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Usuario WHERE nombre = @nombre AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nombre", usuario);
                        cmd.Parameters.AddWithValue("@password", password);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool EliminarUsuario(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Usuario WHERE nombre = @nombre";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nombre", usuario);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarUsuario(string usuarioActual, string nuevoUsuario, string nuevoPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Usuario SET Usuario = @nuevoUsuario, password = @nuevoPassword " +
                                 "WHERE Usuario = @usuarioActual";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nuevoUsuario", nuevoUsuario);
                        cmd.Parameters.AddWithValue("@nuevoPassword", nuevoPassword);
                        cmd.Parameters.AddWithValue("@usuarioActual", usuarioActual);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<(int id, string nombre, string direccion)> ObtenerSucursales()
        {
            var sucursales = new List<(int id, string nombre, string direccion)>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT [id_sucursal], [nombre], [Direccion] 
                                   FROM [GestionDeSeguridad].[dbo].[Sucursales]";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sucursales.Add((
                                reader.GetInt32(0),    // id_sucursal
                                reader.GetString(1),   // nombre
                                reader.GetString(2)    // Direccion
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener sucursales: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sucursales;
        }

        public bool GuardarSucursal(string nombre, string direccion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Sucursales (nombre, Direccion) VALUES (@nombre, @direccion)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar sucursal: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<(int id, string ipCaja, int sucursalId)> ObtenerCajasPorSucursal(int sucursalId)
        {
            var cajas = new List<(int id, string ipCaja, int sucursalId)>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT [id_caja], [IP_caja], [id_sucursal]
                               FROM [GestionDeSeguridad].[dbo].[CajasFuertes]
                               WHERE [id_sucursal] = @sucursalId
                               ORDER BY [id_caja]";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@sucursalId", sucursalId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cajas.Add((
                                    reader.GetInt32(0),    // id_caja
                                    reader.GetString(1),    // IP_caja
                                    reader.GetInt32(2)     // id_sucursal
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener cajas fuertes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cajas;
        }

        public bool GuardarCajaFuerte(string ipCaja, int sucursalId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verificar si la IP ya existe
                    string checkQuery = @"SELECT COUNT(*) 
                                        FROM [GestionDeSeguridad].[dbo].[CajasFuertes] 
                                        WHERE [IP_caja] = @ipCaja";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@ipCaja", ipCaja);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe una caja fuerte con esta IP.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Verificar si la sucursal existe
                    if (!SucursalExiste(sucursalId, connection))
                    {
                        MessageBox.Show("La sucursal seleccionada no existe.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Insertar la nueva caja fuerte
                    string insertQuery = @"INSERT INTO [GestionDeSeguridad].[dbo].[CajasFuertes] 
                                        ([IP_caja], [id_sucursal]) 
                                        VALUES (@ipCaja, @sucursalId)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ipCaja", ipCaja);
                        cmd.Parameters.AddWithValue("@sucursalId", sucursalId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar caja fuerte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SucursalExiste(int sucursalId, SqlConnection connection)
        {
            string query = @"SELECT COUNT(*) 
                           FROM [GestionDeSeguridad].[dbo].[Sucursales] 
                           WHERE [id_sucursal] = @sucursalId";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@sucursalId", sucursalId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
