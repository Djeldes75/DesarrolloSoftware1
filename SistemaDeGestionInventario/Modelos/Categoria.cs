namespace InventarioApp.Modelos
{
    /// <summary>
    /// Representa la entidad de Categoría dentro del sistema de inventario.
    /// Contiene información básica como el ID, el nombre y la descripción.
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Identificador único de la categoría (clave primaria).
        /// </summary>
        public int IDCategoria { get; set; }

        /// <summary>
        /// Nombre de la categoría, por ejemplo: "Electrónica", "Ropa", etc.
        /// </summary>
        public string NombreCategoria { get; set; }

        /// <summary>
        /// Descripción opcional para detallar características
        /// o alcance de la categoría.
        /// </summary>
        public string Descripcion { get; set; }
    }
}
