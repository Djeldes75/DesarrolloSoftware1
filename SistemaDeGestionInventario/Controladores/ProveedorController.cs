using InventarioApp.AccesoDatos;
using InventarioApp.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventarioApp.Controladores
{
    /// <summary>
    /// Controlador que gestiona las operaciones CRUD (Crear, Leer, Actualizar y Eliminar)
    /// para la entidad Proveedor en la base de datos.
    /// </summary>
    public class ProveedorController
    {
        /// <summary>
        /// Crea (INSERT) un nuevo proveedor en la base de datos.
        /// </summary>
        /// <param name="proveedor">
        /// Objeto de tipo <see cref="Proveedor"/> que contiene
        /// la información a insertar.
        /// </param>
        public static void Agregar(Proveedor proveedor)
        {
            // Se establece la conexión con la base de datos utilizando la clase BaseDatos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Define la consulta SQL para insertar un nuevo proveedor.
                string sql = @"INSERT INTO Proveedores (NombreEmpresa, Contacto, Direccion, Telefono)
                               VALUES (@NombreEmpresa, @Contacto, @Direccion, @Telefono)";

                // Se crea un comando SQL con la consulta y la conexión.
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los valores a los parámetros de la consulta.
                    cmd.Parameters.AddWithValue("@NombreEmpresa", proveedor.NombreEmpresa);
                    cmd.Parameters.AddWithValue("@Contacto", proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);

                    // Ejecuta el comando y se inserta el registro.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lee (SELECT) todos los proveedores de la base de datos.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="Proveedor"/> que representan
        /// los registros en la tabla Proveedores.
        /// </returns>
        public static List<Proveedor> ListarTodo()
        {
            // Lista que se llenará con los proveedores obtenidos.
            List<Proveedor> lista = new List<Proveedor>();

            // Se establece la conexión a la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para seleccionar los datos de la tabla Proveedores.
                string sql = "SELECT IDProveedor, NombreEmpresa, Contacto, Direccion, Telefono FROM Proveedores";

                // Se crea el comando SQL con la consulta.
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se ejecuta la consulta y se obtiene un lector de datos.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se recorre cada registro obtenido.
                        while (reader.Read())
                        {
                            // Se crea un objeto Proveedor y se asignan sus propiedades.
                            var prov = new Proveedor
                            {
                                IDProveedor = reader.GetInt32(0),
                                NombreEmpresa = reader.GetString(1),
                                // Verifica si el valor es nulo antes de asignar, en caso de serlo asigna cadena vacía.
                                Contacto = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Direccion = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Telefono = reader.IsDBNull(4) ? "" : reader.GetString(4)
                            };
                            // Se agrega el objeto a la lista.
                            lista.Add(prov);
                        }
                    }
                }
            }

            // Se retorna la lista de proveedores.
            return lista;
        }

        /// <summary>
        /// Actualiza (UPDATE) la información de un proveedor existente
        /// en la base de datos.
        /// </summary>
        /// <param name="proveedor">
        /// Objeto <see cref="Proveedor"/> con la información editada
        /// que se aplicará en la base de datos.
        /// </param>
        public static void Editar(Proveedor proveedor)
        {
            // Se establece la conexión con la base de datos.
            using (SqlConnection conn = BaseDatos.ObtenerConexion())
            {
                conn.Open();
                // Consulta SQL para actualizar la información de un proveedor.
                string sql = @"UPDATE Proveedores
                               SET NombreEmpresa = @NombreEmpresa,
                                   Contacto = @Contacto,
                                   Direccion = @Direccion,
                                   Telefono = @Telefono
                               WHERE IDProveedor = @IDProveedor";

                // Se crea un comando SQL con la consulta y la conexión.
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Se asignan los valores correspondientes a cada parámetro.
                    cmd.Parameters.AddWithValue("@NombreEmpresa", proveedor.NombreEmpresa);
                    cmd.Parameters.AddWithValue("@Contacto", proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@IDProveedor", proveedor.IDProveedor);

                    // Se ejecuta el comando para actualizar el registro.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina (DELETE) un proveedor de la base de datos
        /// mediante su ID.
        /// </summary>
        /// <param name="idProveedor">
        /// ID único que identifica al proveedor que se desea eliminar.
        /// </param>
        /// <returns>
        /// <c>true</c> si se eliminó al menos un registro; de lo contrario, <c>false</c>.
        /// </returns>
        public static bool Eliminar(int idProveedor)
        {
            try
            {
                // Se establece la conexión con la base de datos.
                using (SqlConnection conn = BaseDatos.ObtenerConexion())
                {
                    conn.Open();
                    // Consulta SQL para eliminar un proveedor utilizando su ID.
                    string sql = "DELETE FROM Proveedores WHERE IDProveedor = @IDProveedor";

                    // Se crea un comando SQL con la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Se establece el parámetro del ID.
                        cmd.Parameters.AddWithValue("@IDProveedor", idProveedor);

                        // Se ejecuta el comando y se obtiene el número de filas afectadas.
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        // Se retorna true si se eliminó al menos un registro, de lo contrario false.
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error, se retorna false. (Aquí puedes registrar el error según convenga)
                return false;
            }
        }
    }
}
