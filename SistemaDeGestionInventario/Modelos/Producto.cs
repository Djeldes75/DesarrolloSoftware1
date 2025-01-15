namespace InventarioApp.Modelos
{
    /// <summary>
    /// Representa un producto dentro del sistema de inventario.
    /// Contiene información sobre su código, nombre, categoría, precio,
    /// existencia en stock y proveedor relacionado.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Clave primaria que identifica de forma única a cada producto.
        /// Se auto-incrementa en la base de datos.
        /// </summary>
        public int CodigoProducto { get; set; }

        /// <summary>
        /// Nombre o descripción corta del producto.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// ID de la categoría a la que pertenece el producto
        /// (relacionado con la tabla Categorías).
        /// </summary>
        public int IDCategoria { get; set; }

        /// <summary>
        /// Precio unitario o de venta del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Cantidad existente o stock disponible de este producto.
        /// </summary>
        public int Existencia { get; set; }

        /// <summary>
        /// ID del proveedor que suministra el producto
        /// (relacionado con la tabla Proveedores).
        /// </summary>
        public int IDProveedor { get; set; }
    }
}
