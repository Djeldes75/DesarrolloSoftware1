namespace InventarioApp.Modelos
{
    /// <summary>
    /// Representa a un proveedor dentro del sistema de inventario.
    /// Contiene la información de la empresa, datos de contacto
    /// y domicilio.
    /// </summary>
    public class Proveedor
    {
        /// <summary>
        /// Identificador único del proveedor (clave primaria).
        /// </summary>
        public int IDProveedor { get; set; }

        /// <summary>
        /// Nombre de la empresa o institución que provee
        /// los productos.
        /// </summary>
        public string NombreEmpresa { get; set; }

        /// <summary>
        /// Nombre de la persona de contacto dentro de la empresa
        /// proveedora.
        /// </summary>
        public string Contacto { get; set; }

        /// <summary>
        /// Dirección física de la empresa proveedora.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Teléfono principal o de contacto de la empresa
        /// proveedora.
        /// </summary>
        public string Telefono { get; set; }
    }
}
