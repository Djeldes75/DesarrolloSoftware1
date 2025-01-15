using InventarioApp.Controladores;
using InventarioApp.Modelos;
using System;
using System.Windows.Forms;

namespace InventarioApp.Vistas
{
    /// <summary>
    /// Formulario de administración de Categorías.
    /// Permite listar, agregar, editar y eliminar registros
    /// en la base de datos de categorías.
    /// </summary>
    public partial class frmCategorias : Form
    {
        /// <summary>
        /// Constructor del formulario.
        /// Inicializa los componentes de la interfaz gráfica.
        /// </summary>
        public frmCategorias()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara cuando el formulario carga.
        /// Llama al método CargarCategorias para mostrar la lista inicial.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void frmCategorias_Load(object sender, EventArgs e)
        {
            // Se carga la lista de categorías al iniciar el formulario.
            CargarCategorias();
        }

        /// <summary>
        /// Obtiene la lista de todas las categorías desde el controlador
        /// y la muestra en el DataGridView correspondiente.
        /// </summary>
        private void CargarCategorias()
        {
            // Se obtiene la lista de categorías a través del controlador.
            var categorias = CategoriaController.ListarTodo();

            // Se desactiva la autogeneración de columnas para tener control total de la presentación.
            dgvCategorias.AutoGenerateColumns = false;
            // Se limpian las columnas existentes.
            dgvCategorias.Columns.Clear();

            // Se crea manualmente la columna para el ID de la categoría.
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                Name = "IDCategoria",             // Nombre interno que coincide con la propiedad del modelo.
                DataPropertyName = "IDCategoria", // Propiedad a la que se enlaza.
                HeaderText = "ID",                // Texto que aparece en el encabezado de la columna.
                ReadOnly = true                   // El ID generalmente no es editable.
            };
            dgvCategorias.Columns.Add(colId);

            // Se crea la columna para el nombre de la categoría.
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn
            {
                Name = "NombreCategoria",
                DataPropertyName = "NombreCategoria",
                HeaderText = "Categoría"
            };
            dgvCategorias.Columns.Add(colNombre);

            // Se crea la columna para la descripción de la categoría.
            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción"
            };
            dgvCategorias.Columns.Add(colDesc);

            // Se asigna la lista de categorías obtenida al DataSource del DataGridView.
            dgvCategorias.DataSource = categorias;

            // (Opcional) Se imprime en la consola el nombre y DataPropertyName de cada columna para depurar.
            foreach (DataGridViewColumn col in dgvCategorias.Columns)
            {
                Console.WriteLine($"Columna -> Name: {col.Name} - DataPropertyName: {col.DataPropertyName}");
            }
        }

        /// <summary>
        /// Limpia los campos de texto y establece el foco en el TextBox "txtNombreCategoria".
        /// </summary>
        private void LimpiarCampos()
        {
            // Se limpian los TextBox de nombre y descripción.
            txtNombreCategoria.Clear();
            txtDescripcion.Clear();
            // Se pone el foco en el TextBox del nombre para facilitar el ingreso de datos.
            txtNombreCategoria.Focus();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Agregar".
        /// Crea un objeto de tipo Categoria con los datos ingresados 
        /// y llama al controlador para guardarlo en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Se crea un objeto Categoria y se asignan los valores ingresados en los TextBox.
            Categoria cat = new Categoria
            {
                NombreCategoria = txtNombreCategoria.Text,
                Descripcion = txtDescripcion.Text
            };

            // Se llama al controlador para agregar la nueva categoría.
            CategoriaController.Agregar(cat);

            // Se notifica al usuario que la categoría se agregó correctamente.
            MessageBox.Show("Categoría agregada correctamente.");
            // Se recarga el listado de categorías.
            CargarCategorias();
            // Se limpian los campos de entrada.
            LimpiarCampos();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Editar".
        /// Verifica que exista una fila seleccionada en el DataGridView,
        /// crea un objeto Categoria con los datos editados y actualiza el registro 
        /// en la base de datos llamando al controlador.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Se verifica que haya una fila seleccionada en el DataGridView.
            if (dgvCategorias.CurrentRow != null)
            {
                try
                {
                    // Se obtiene el ID de la categoría seleccionada.
                    int id = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["IDCategoria"].Value);

                    // Se crea un objeto Categoria con la información editada.
                    Categoria cat = new Categoria
                    {
                        IDCategoria = id,
                        NombreCategoria = txtNombreCategoria.Text,
                        Descripcion = txtDescripcion.Text
                    };

                    // Se llama al controlador para actualizar el registro en la base de datos.
                    CategoriaController.Editar(cat);

                    // Se muestra un mensaje de éxito.
                    MessageBox.Show("Categoría editada correctamente.",
                                    "Información",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Se actualiza el DataGridView y se limpian los campos.
                    CargarCategorias();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    // En caso de error, se muestra un mensaje descriptivo.
                    MessageBox.Show($"Ocurrió un error al editar la categoría: {ex.Message}",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no hay una fila seleccionada, se notifica al usuario.
                MessageBox.Show("Por favor, selecciona una categoría de la lista.",
                                "Atención",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Eliminar".
        /// Verifica que exista una categoría seleccionada, obtiene su ID
        /// y llama al controlador para eliminarla de la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Se verifica que haya una fila seleccionada.
            if (dgvCategorias.CurrentRow != null)
            {
                // Se solicita confirmación al usuario para eliminar la categoría.
                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar esta categoría?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // Se obtiene el ID de la categoría desde la celda "IDCategoria".
                        int id = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["IDCategoria"].Value);

                        // Se llama al método Eliminar del controlador, que retorna un valor booleano.
                        bool resultado = CategoriaController.Eliminar(id);

                        // Se muestra el mensaje correspondiente según el resultado.
                        if (resultado)
                        {
                            MessageBox.Show("Categoría eliminada correctamente.",
                                "Operación exitosa",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la categoría.",
                                "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Se actualiza el listado y se limpian los campos.
                        CargarCategorias();
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        // Se muestra un mensaje de error en caso de excepción.
                        MessageBox.Show($"Ocurrió un error al eliminar la categoría: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Si no se selecciona ninguna categoría, se informa al usuario.
                MessageBox.Show("Selecciona una categoría para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Evento que se dispara cuando se hace clic en una celda del DataGridView.
        /// Carga los datos de la categoría seleccionada en los TextBox para su visualización o edición.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Datos del evento, incluyendo el índice de la fila clickeada.</param>
        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se verifica que la fila seleccionada tenga un índice válido y que la fila actual no sea nula.
            if (e.RowIndex >= 0 && dgvCategorias.CurrentRow != null)
            {
                // Se asignan los valores de la fila a los TextBox correspondientes.
                txtNombreCategoria.Text = dgvCategorias.Rows[e.RowIndex].Cells["NombreCategoria"].Value.ToString();
                txtDescripcion.Text = dgvCategorias.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Refrescar".
        /// Recarga la lista de categorías y limpia los campos de texto.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            // Se refresca la lista y se limpian los TextBox.
            CargarCategorias();
            LimpiarCampos();
        }
    }
}
