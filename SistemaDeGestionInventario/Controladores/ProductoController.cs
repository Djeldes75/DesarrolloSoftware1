using InventarioApp.AccesoDatos;
using InventarioApp.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventarioApp.Controladores
{
    /// <summary>
    /// Controlador que gestiona las operaciones CRUD y consultas adicionales
    /// para la entidad <see cref="Producto"/> en la base de datos.
    /// </summary>
    public class ProductoController
    {
        /// <summary>
        /// Crea (INSERT) un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="producto">
        /// Objeto de tipo <see cref="Producto"/> que contiene la información
        /// del nuevo producto a guardar.
        /// </param>
        public static void Agregar(Producto producto)
        {
            // Se obtiene una conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Se define la consulta SQL para insertar un nuevo producto.
                string sql = @"INSERT INTO Productos 
                               (Nombre, IDCategoria, Precio, Existencia, IDProveedor)
                               VALUES 
                               (@Nombre, @IDCategoria, @Precio, @Existencia, @IDProveedor)";

                // Se crea el comando SQL y se asocia a la conexión.
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los valores de los parámetros a partir del objeto producto.
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@IDCategoria", producto.IDCategoria);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Existencia", producto.Existencia);
                    cmd.Parameters.AddWithValue("@IDProveedor", producto.IDProveedor);

                    // Se ejecuta la consulta sin esperar resultado (INSERT).
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lee (SELECT) todos los productos de la base de datos.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="Producto"/> que representan todos los registros
        /// encontrados en la tabla Productos.
        /// </returns>
        public static List<Producto> ListarTodo()
        {
            List<Producto> lista = new List<Producto>();

            // Se abre la conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL que obtiene las columnas que corresponden al modelo Producto.
                string sql = "SELECT CodigoProducto, Nombre, IDCategoria, Precio, Existencia, IDProveedor FROM Productos";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se ejecuta la consulta y se obtiene un DataReader.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se recorren los registros obtenidos.
                        while (reader.Read())
                        {
                            // Se crea un objeto Producto y se asignan sus propiedades.
                            var prod = new Producto
                            {
                                CodigoProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                IDCategoria = reader.GetInt32(2),
                                Precio = reader.GetDecimal(3),
                                Existencia = reader.GetInt32(4),
                                IDProveedor = reader.GetInt32(5)
                            };
                            // Se agrega el objeto a la lista.
                            lista.Add(prod);
                        }
                    }
                }
            }
            // Se retorna la lista completa de productos.
            return lista;
        }

        /// <summary>
        /// Actualiza (UPDATE) la información de un producto existente en la base de datos.
        /// </summary>
        /// <param name="producto">
        /// Objeto <see cref="Producto"/> que contiene los nuevos valores para actualizar
        /// el registro correspondiente.
        /// </param>
        public static void Editar(Producto producto)
        {
            // Se obtiene la conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para actualizar la información del producto.
                string sql = @"UPDATE Productos
                               SET Nombre = @Nombre,
                                   IDCategoria = @IDCategoria,
                                   Precio = @Precio,
                                   Existencia = @Existencia,
                                   IDProveedor = @IDProveedor
                               WHERE CodigoProducto = @CodigoProducto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los valores a los parámetros del comando.
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@IDCategoria", producto.IDCategoria);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Existencia", producto.Existencia);
                    cmd.Parameters.AddWithValue("@IDProveedor", producto.IDProveedor);
                    cmd.Parameters.AddWithValue("@CodigoProducto", producto.CodigoProducto);

                    // Se ejecuta la consulta de actualización.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina (DELETE) un producto de la base de datos mediante su Código de Producto.
        /// </summary>
        /// <param name="codigoProducto">
        /// Código único que identifica al producto a eliminar.
        /// </param>
        /// <returns>
        /// <c>true</c> si se eliminó al menos un registro; de lo contrario, <c>false</c>.
        /// </returns>
        public static bool Eliminar(int codigoProducto)
        {
            try
            {
                // Se abre la conexión a la base de datos.
                using (SqlConnection conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();
                    // Consulta SQL para eliminar el producto basándose en su código.
                    string sql = "DELETE FROM Productos WHERE CodigoProducto = @CodigoProducto";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Se establece el parámetro con el código del producto.
                        cmd.Parameters.AddWithValue("@CodigoProducto", codigoProducto);
                        // Se ejecuta la consulta y se obtiene el número de filas afectadas.
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        // Se retorna true si se eliminó al menos un registro.
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error se retorna false (opcional: se puede registrar el error).
                return false;
            }
        }

        /// <summary>
        /// Consulta los productos filtrados por la Categoría especificada.
        /// </summary>
        /// <param name="idCategoria">
        /// ID de la categoría que se usará como filtro en la consulta.
        /// </param>
        /// <returns>
        /// Lista de <see cref="Producto"/> que pertenecen a la categoría solicitada.
        /// </returns>
        public static List<Producto> ListarPorCategoria(int idCategoria)
        {
            List<Producto> lista = new List<Producto>();

            // Se abre la conexión y se prepara la consulta filtrada por categoría.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT CodigoProducto, Nombre, IDCategoria, Precio, Existencia, IDProveedor
                               FROM Productos
                               WHERE IDCategoria = @IDCategoria";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asigna el valor del parámetro para la categoría.
                    cmd.Parameters.AddWithValue("@IDCategoria", idCategoria);
                    // Se ejecuta la consulta y se obtienen los registros.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Producto
                            {
                                CodigoProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                IDCategoria = reader.GetInt32(2),
                                Precio = reader.GetDecimal(3),
                                Existencia = reader.GetInt32(4),
                                IDProveedor = reader.GetInt32(5)
                            };
                            lista.Add(prod);
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Consulta los productos filtrados por el Proveedor especificado.
        /// </summary>
        /// <param name="idProveedor">
        /// ID del proveedor que se usará como filtro.
        /// </param>
        /// <returns>
        /// Lista de <see cref="Producto"/> asociados al proveedor indicado.
        /// </returns>
        public static List<Producto> ListarPorProveedor(int idProveedor)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT CodigoProducto, Nombre, IDCategoria, Precio, Existencia, IDProveedor
                               FROM Productos
                               WHERE IDProveedor = @IDProveedor";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IDProveedor", idProveedor);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Producto
                            {
                                CodigoProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                IDCategoria = reader.GetInt32(2),
                                Precio = reader.GetDecimal(3),
                                Existencia = reader.GetInt32(4),
                                IDProveedor = reader.GetInt32(5)
                            };
                            lista.Add(prod);
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Retorna un listado de productos que tienen un nivel de Existencia menor a 10.
        /// </summary>
        /// <returns>
        /// Lista de <see cref="Producto"/> con stock bajo (Existencia menor a 10).
        /// </returns>
        public static List<Producto> ReporteBajoStock()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT CodigoProducto, Nombre, IDCategoria, Precio, Existencia, IDProveedor
                               FROM Productos
                               WHERE Existencia < 10";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Producto
                            {
                                CodigoProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                IDCategoria = reader.GetInt32(2),
                                Precio = reader.GetDecimal(3),
                                Existencia = reader.GetInt32(4),
                                IDProveedor = reader.GetInt32(5)
                            };
                            lista.Add(prod);
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Realiza una búsqueda de productos por coincidencia parcial
        /// en el nombre, usando LIKE en la consulta SQL.
        /// </summary>
        /// <param name="texto">
        /// Texto a buscar dentro del nombre de los productos.
        /// </param>
        /// <returns>
        /// Lista de <see cref="Producto"/> que coinciden con el criterio de búsqueda.
        /// </returns>
        public static List<Producto> BuscarPorNombre(string texto)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT CodigoProducto, Nombre, IDCategoria, Precio, Existencia, IDProveedor
                               FROM Productos
                               WHERE Nombre LIKE @texto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se utiliza el operador LIKE para buscar coincidencias parciales.
                    cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Producto
                            {
                                CodigoProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                IDCategoria = reader.GetInt32(2),
                                Precio = reader.GetDecimal(3),
                                Existencia = reader.GetInt32(4),
                                IDProveedor = reader.GetInt32(5)
                            };
                            lista.Add(prod);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
