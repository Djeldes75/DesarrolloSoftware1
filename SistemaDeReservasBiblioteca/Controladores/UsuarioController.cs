using SistemaDeReservasBiblioteca.AccesoDatos;
using SistemaDeReservasBiblioteca.Modelos;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SistemaDeReservasBiblioteca.Controladores
{
    public class UsuarioController
    {
        // Agregar un nuevo usuario.
        public void AgregarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono) " +
                               "VALUES (@Nombre, @Apellido, @Email, @Telefono)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Editar un usuario existente.
        public void EditarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono " +
                               "WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar un usuario por Id.
        public void EliminarUsuario(int idUsuario)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Obtener todos los usuarios.
        public List<Usuario> ObtenerTodosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "SELECT * FROM Usuarios";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                IdUsuario = dr["IdUsuario"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString()
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }
    }
}
