using InventarioApp.Vistas;
using System;
using System.Windows.Forms;

namespace InventarioApp
{
    /// <summary>
    /// Formulario principal de la aplicación.
    /// Contiene un MenuStrip que permite navegar a los formularios de
    /// Categorías, Proveedores y Productos.
    /// </summary>
    public partial class frmPrincipal : Form
    {
        /// <summary>
        /// Constructor del formulario principal.
        /// Inicializa los componentes de la interfaz gráfica.
        /// </summary>
        public frmPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se ejecuta cuando el formulario principal se carga.
        /// Aquí se pueden inicializar lógicas adicionales si fuera necesario.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            // Por ahora, no se requiere lógica adicional al cargar el formulario.
            // En este lugar se podrían inicializar variables, configurar controles o cargar datos.
        }

        /// <summary>
        /// Evento del menú "Mantenimientos" -> "Categorías".
        /// Abre el formulario frmCategorias de forma modal.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se crea una instancia del formulario de Categorías.
            frmCategorias frm = new frmCategorias();
            // Se muestra el formulario de forma modal (impide interactuar con el formulario principal hasta cerrarlo).
            frm.ShowDialog();
        }

        /// <summary>
        /// Evento del menú "Mantenimientos" -> "Proveedores".
        /// Abre el formulario frmProveedores de forma modal.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se crea una instancia del formulario de Proveedores.
            frmProveedores frm = new frmProveedores();
            // Se muestra el formulario de forma modal.
            frm.ShowDialog();
        }

        /// <summary>
        /// Evento del menú "Mantenimientos" -> "Productos".
        /// Abre el formulario frmProductos de forma modal.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se crea una instancia del formulario de Productos.
            frmProductos frm = new frmProductos();
            // Se muestra el formulario de forma modal.
            frm.ShowDialog();
        }

        /// <summary>
        /// Evento del menú "Archivo" -> "Salir".
        /// Cierra la aplicación por completo.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Finaliza la ejecución de la aplicación.
            Application.Exit();
        }
    }
}
