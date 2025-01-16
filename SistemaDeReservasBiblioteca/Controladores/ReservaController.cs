using SistemaDeReservasBiblioteca.AccesoDatos;
using SistemaDeReservasBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SistemaDeReservasBiblioteca.Controladores
{
    public class ReservaController
    {
        // Agregar una nueva reserva.
        public void AgregarReserva(Reserva reserva)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "INSERT INTO Reservas (IdUsuario, ISBNLibro, FechaReserva, FechaRetorno) " +
                               "VALUES (@IdUsuario, @ISBNLibro, @FechaReserva, @FechaRetorno)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
                    cmd.Parameters.AddWithValue("@ISBNLibro", reserva.ISBNLibro);
                    cmd.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);

                    if (reserva.FechaRetorno.HasValue)
                        cmd.Parameters.AddWithValue("@FechaRetorno", reserva.FechaRetorno.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaRetorno", DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Editar una reserva existente.
        public void EditarReserva(Reserva reserva)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "UPDATE Reservas SET IdUsuario = @IdUsuario, ISBNLibro = @ISBNLibro, " +
                               "FechaReserva = @FechaReserva, FechaRetorno = @FechaRetorno " +
                               "WHERE IdReserva = @IdReserva";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                    cmd.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
                    cmd.Parameters.AddWithValue("@ISBNLibro", reserva.ISBNLibro);
                    cmd.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);

                    if (reserva.FechaRetorno.HasValue)
                        cmd.Parameters.AddWithValue("@FechaRetorno", reserva.FechaRetorno.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaRetorno", DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar una reserva por Id.
        public void EliminarReserva(int idReserva)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "DELETE FROM Reservas WHERE IdReserva = @IdReserva";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Obtener todas las reservas.
        public List<Reserva> ObtenerTodasReservas()
        {
            List<Reserva> reservas = new List<Reserva>();
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "SELECT * FROM Reservas";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                IdReserva = Convert.ToInt32(dr["IdReserva"]),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                ISBNLibro = dr["ISBNLibro"].ToString(),
                                FechaReserva = Convert.ToDateTime(dr["FechaReserva"]),
                                FechaRetorno = dr["FechaRetorno"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["FechaRetorno"]) : null
                            };
                            reservas.Add(reserva);
                        }
                    }
                }
            }
            return reservas;
        }
    }
}
