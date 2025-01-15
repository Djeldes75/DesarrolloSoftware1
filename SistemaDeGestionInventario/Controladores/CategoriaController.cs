using InventarioApp.AccesoDatos;
using InventarioApp.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventarioApp.Controladores
{
    /// <summary>
    /// Controlador que gestiona las operaciones CRUD (Crear, Leer, Actualizar y Eliminar)
    /// para la entidad <see cref="Categoria"/> en la base de datos.
    /// </summary>
    public class CategoriaController
    {
        /// <summary>
        /// Crea (INSERT) una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="categoria">
        /// Objeto de tipo <see cref="Categoria"/> que contiene la información a insertar.
        /// </param>
        public static void Agregar(Categoria categoria)
        {
            // Se abre una conexión a la base de datos utilizando la clase BaseDatos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para insertar una nueva categoría en la tabla Categorias.
                string sql = @"INSERT INTO Categorias (NombreCategoria, Descripcion)
                               VALUES (@NombreCategoria, @Descripcion)";

                // Se crea el comando SQL asociado a la conexión.
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los parámetros del comando a partir de los valores del objeto.
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    // Se ejecuta la consulta (INSERT) sin esperar resultados.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lee (SELECT) todos los registros de la tabla Categorias.
        /// </summary>
        /// <returns>
        /// Una lista de objetos <see cref="Categoria"/> que representa los registros
        /// encontrados en la base de datos.
        /// </returns>
        public static List<Categoria> ListarTodo()
        {
            // Se declara una lista para almacenar los registros obtenidos.
            List<Categoria> lista = new List<Categoria>();

            // Se establece la conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para seleccionar los campos que corresponden al modelo Categoria.
                string sql = "SELECT IDCategoria, NombreCategoria, Descripcion FROM Categorias";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se ejecuta la consulta y se obtiene un SqlDataReader.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se recorre cada registro del DataReader.
                        while (reader.Read())
                        {
                            // Se crea una instancia de Categoria y se asignan sus propiedades.
                            var cat = new Categoria
                            {
                                IDCategoria = reader.GetInt32(0),  // Lee el campo IDCategoria (índice 0)
                                NombreCategoria = reader.GetString(1), // Lee el campo NombreCategoria (índice 1)
                                // Si el campo Descripcion es nulo, se asigna una cadena vacía; de lo contrario, se asigna el valor.
                                Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2)
                            };

                            // Se agrega el objeto a la lista.
                            lista.Add(cat);
                        }
                    }
                }
            }
            // Se retorna la lista con los registros.
            return lista;
        }

        /// <summary>
        /// Actualiza (UPDATE) la información de una categoría existente en la base de datos.
        /// </summary>
        /// <param name="categoria">
        /// Objeto <see cref="Categoria"/> que contiene la información editada.
        /// </param>
        public static void Editar(Categoria categoria)
        {
            // Se obtiene una conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para actualizar la categoría según su ID.
                string sql = @"UPDATE Categorias
                               SET NombreCategoria = @NombreCategoria,
                                   Descripcion = @Descripcion
                               WHERE IDCategoria = @IDCategoria";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los parámetros con los valores del objeto categoria.
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("@IDCategoria", categoria.IDCategoria);
                    // Se ejecuta la consulta de actualización.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina (DELETE) una categoría de la base de datos mediante su ID.
        /// </summary>
        /// <param name="idCategoria">
        /// ID único que identifica la categoría a eliminar.
        /// </param>
        /// <returns>
        /// <c>true</c> si se eliminó al menos un registro; de lo contrario, <c>false</c>.
        /// </returns>
        public static bool Eliminar(int idCategoria)
        {
            try
            {
                // Se abre la conexión a la base de datos.
                using (SqlConnection conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();
                    // Consulta SQL para borrar la categoría utilizando su ID.
                    string sql = "DELETE FROM Categorias WHERE IDCategoria = @IDCategoria";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Se asigna el parámetro correspondiente.
                        cmd.Parameters.AddWithValue("@IDCategoria", idCategoria);
                        // Se ejecuta la consulta y se obtiene el número de filas afectadas.
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        // Se retorna true si se eliminó al menos un registro.
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error se retorna false.
                return false;
            }
        }
    }
}
