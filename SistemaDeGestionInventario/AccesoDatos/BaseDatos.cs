using System.Data.SqlClient;

namespace InventarioApp.AccesoDatos
{
    /// <summary>
    /// Clase estática que gestiona la obtención de conexiones
    /// a la base de datos para la aplicación.
    /// </summary>
    public static class BaseDatos
    {
        /// <summary>
        /// Cadena de conexión a la base de datos SQL Server.
        /// Incluye configuración de cifrado y certificación del servidor.
        /// Ajustar al nombre de servidor, instancia y parámetros requeridos.
        /// </summary>
        private static string cadenaConexion =
            @"Data Source=NEWVOID\INVENTORYDB;Initial Catalog=InventarioDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        /// <summary>
        /// Obtiene y retorna un objeto <see cref="SqlConnection"/>
        /// configurado con la cadena de conexión definida.
        /// </summary>
        /// <returns>
        /// Una conexión <see cref="SqlConnection"/> abierta y lista
        /// para usar con la base de datos especificada.
        /// </returns>
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
