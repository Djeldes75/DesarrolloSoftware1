using SistemaDeReservasBiblioteca.AccesoDatos;
using SistemaDeReservasBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SistemaDeReservasBiblioteca.Controladores
{
    public class LibroController
    {
        // Agregar un nuevo libro.
        public void AgregarLibro(Libro libro)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "INSERT INTO Libros (ISBN, Titulo, Autor, Editorial, AnioPublicacion, Genero, NumeroCopias) " +
                               "VALUES (@ISBN, @Titulo, @Autor, @Editorial, @AnioPublicacion, @Genero, @NumeroCopias)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                    cmd.Parameters.AddWithValue("@Genero", libro.Genero);
                    cmd.Parameters.AddWithValue("@NumeroCopias", libro.NumeroCopias);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Editar un libro existente.
        public void EditarLibro(Libro libro)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Editorial = @Editorial, " +
                               "AnioPublicacion = @AnioPublicacion, Genero = @Genero, NumeroCopias = @NumeroCopias " +
                               "WHERE ISBN = @ISBN";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                    cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                    cmd.Parameters.AddWithValue("@Genero", libro.Genero);
                    cmd.Parameters.AddWithValue("@NumeroCopias", libro.NumeroCopias);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar un libro por ISBN.
        public void EliminarLibro(string isbn)
        {
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "DELETE FROM Libros WHERE ISBN = @ISBN";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Obtener todos los libros registrados.
        public List<Libro> ObtenerTodosLibros()
        {
            List<Libro> libros = new List<Libro>();
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "SELECT * FROM Libros";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Libro libro = new Libro
                            {
                                ISBN = dr["ISBN"].ToString(),
                                Titulo = dr["Titulo"].ToString(),
                                Autor = dr["Autor"].ToString(),
                                Editorial = dr["Editorial"].ToString(),
                                AnioPublicacion = Convert.ToInt32(dr["AnioPublicacion"]),
                                Genero = dr["Genero"].ToString(),
                                NumeroCopias = Convert.ToInt32(dr["NumeroCopias"])
                            };
                            libros.Add(libro);
                        }
                    }
                }
            }
            return libros;
        }

        // Buscar libros filtrando por género.
        public List<Libro> BuscarLibrosPorGenero(string genero)
        {
            List<Libro> libros = new List<Libro>();
            using (SqlConnection connection = BaseDatos.ObtenerConexion())
            {
                string query = "SELECT * FROM Libros WHERE Genero LIKE @Genero";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Genero", "%" + genero + "%");
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Libro libro = new Libro
                            {
                                ISBN = dr["ISBN"].ToString(),
                                Titulo = dr["Titulo"].ToString(),
                                Autor = dr["Autor"].ToString(),
                                Editorial = dr["Editorial"].ToString(),
                                AnioPublicacion = Convert.ToInt32(dr["AnioPublicacion"]),
                                Genero = dr["Genero"].ToString(),
                                NumeroCopias = Convert.ToInt32(dr["NumeroCopias"])
                            };
                            libros.Add(libro);
                        }
                    }
                }
            }
            return libros;
        }
    }
}
