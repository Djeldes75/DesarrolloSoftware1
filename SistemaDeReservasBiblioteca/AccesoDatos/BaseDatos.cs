using System.Data.SqlClient;

namespace SistemaDeReservasBiblioteca.AccesoDatos
{
    public static class BaseDatos
    {
        private static string connectionString =
            @"Data Source=NEWVOID\INVENTORYDB;Initial Catalog=BibliotecaDB;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}