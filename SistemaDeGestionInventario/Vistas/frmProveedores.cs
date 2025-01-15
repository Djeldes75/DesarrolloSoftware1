using InventarioApp.Controladores;
using InventarioApp.Modelos;
using System;
using System.Windows.Forms;

namespace InventarioApp.Vistas
{
    /// <summary>
    /// Formulario para la administración (CRUD) de Proveedores.
    /// Permite listar, agregar, editar, eliminar y refrescar
    /// la información de proveedores.
    /// </summary>
    public partial class frmProveedores : Form
    {
        /// <summary>
        /// Constructor principal del formulario.
        /// Inicializa los componentes de la interfaz (InitializeComponent).
        /// </summary>
        public frmProveedores()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se ejecuta cuando el formulario se carga.
        /// Llama al método CargarProveedores para mostrar la lista inicial.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void frmProveedores_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        /// <summary>
        /// Carga y muestra en el DataGridView la lista de todos los proveedores
        /// obtenida desde la capa de control.
        /// </summary>
        private void CargarProveedores()
        {
            // Obtiene la lista de proveedores desde el controlador.
            var proveedores = ProveedorController.ListarTodo();

            // Desactivar la autogeneración de columnas para tener un control total.
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.Columns.Clear();

            // 1. Columna para IDProveedor.
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                Name = "IDProveedor",               // Nombre interno (debe coincidir con la propiedad del modelo).
                DataPropertyName = "IDProveedor",   // Propiedad del objeto Proveedor.
                HeaderText = "ID",                  // Texto de encabezado mostrado.
                ReadOnly = true                     // Generalmente el ID no es editable.
            };
            dgvProveedores.Columns.Add(colId);

            // 2. Columna para NombreEmpresa.
            DataGridViewTextBoxColumn colNombreEmpresa = new DataGridViewTextBoxColumn
            {
                Name = "NombreEmpresa",
                DataPropertyName = "NombreEmpresa",
                HeaderText = "Empresa"
            };
            dgvProveedores.Columns.Add(colNombreEmpresa);

            // 3. Columna para Contacto.
            DataGridViewTextBoxColumn colContacto = new DataGridViewTextBoxColumn
            {
                Name = "Contacto",
                DataPropertyName = "Contacto",
                HeaderText = "Contacto"
            };
            dgvProveedores.Columns.Add(colContacto);

            // 4. Columna para Direccion.
            DataGridViewTextBoxColumn colDireccion = new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                DataPropertyName = "Direccion",
                HeaderText = "Dirección"
            };
            dgvProveedores.Columns.Add(colDireccion);

            // 5. Columna para Telefono.
            DataGridViewTextBoxColumn colTelefono = new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono"
            };
            dgvProveedores.Columns.Add(colTelefono);

            // Se asigna la lista de proveedores al DataSource del DataGridView.
            dgvProveedores.DataSource = proveedores;

            // (Opcional) Imprime en consola los nombres y propiedades de las columnas para depuración.
            foreach (DataGridViewColumn col in dgvProveedores.Columns)
            {
                Console.WriteLine($"Columna -> Name: {col.Name}, DataPropertyName: {col.DataPropertyName}");
            }
        }

        /// <summary>
        /// Limpia el contenido de los TextBox para poder ingresar nuevos datos o refrescar la vista.
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombreEmpresa.Clear();
            txtContacto.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();

            // Se enfoca el TextBox de NombreEmpresa para facilitar la entrada de nuevos datos.
            txtNombreEmpresa.Focus();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Agregar".
        /// Crea un nuevo objeto Proveedor con los valores de los TextBox y llama al controlador para almacenarlo en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia de Proveedor y asigna los valores obtenidos de los TextBox (se usa Trim para eliminar espacios).
            Proveedor prov = new Proveedor
            {
                NombreEmpresa = txtNombreEmpresa.Text.Trim(),
                Contacto = txtContacto.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };

            // Llama al método Agregar del ProveedorController para insertar el nuevo registro.
            ProveedorController.Agregar(prov);

            MessageBox.Show("Proveedor agregado correctamente.");

            // Recarga el listado de proveedores y limpia los campos.
            CargarProveedores();
            LimpiarCampos();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Editar".
        /// Verifica que haya una fila seleccionada en el DataGridView, crea un objeto Proveedor
        /// con la información editada y llama al controlador para actualizar el registro en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verifica que se haya seleccionado alguna fila en el DataGridView.
            if (dgvProveedores.CurrentRow != null)
            {
                // Se obtiene el ID del proveedor de la fila seleccionada.
                int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IDProveedor"].Value);

                // Se crea un objeto Proveedor con los datos actualizados.
                Proveedor prov = new Proveedor
                {
                    IDProveedor = id,
                    NombreEmpresa = txtNombreEmpresa.Text.Trim(),
                    Contacto = txtContacto.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                // Llama al método Editar del controlador para actualizar la información.
                ProveedorController.Editar(prov);

                MessageBox.Show("Proveedor editado correctamente.");

                // Recarga el listado y limpia los campos.
                CargarProveedores();
                LimpiarCampos();
            }
            else
            {
                // Si no hay una fila seleccionada, se muestra un mensaje al usuario.
                MessageBox.Show("Selecciona un proveedor para editar.");
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Eliminar".
        /// Verifica que haya un proveedor seleccionado en el DataGridView, obtiene su ID y llama al controlador para eliminarlo de la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica que se haya seleccionado una fila.
            if (dgvProveedores.CurrentRow != null)
            {
                // Solicita confirmación al usuario para eliminar el registro.
                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este proveedor?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // Se obtiene el ID del proveedor de la celda "IDProveedor".
                        int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IDProveedor"].Value);

                        // Llama al método Eliminar del controlador para borrar el registro.
                        bool resultado = ProveedorController.Eliminar(id);

                        if (resultado)
                        {
                            MessageBox.Show("Proveedor eliminado correctamente.",
                                "Operación exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el proveedor.",
                                "Operación fallida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }

                        // Recarga la lista de proveedores tras la eliminación.
                        CargarProveedores();
                        // Si se dispone de un método para limpiar campos, se puede llamar aquí.
                        // LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        // En caso de error, se muestra un mensaje con la descripción.
                        MessageBox.Show($"Ocurrió un error al eliminar el proveedor: {ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Si no se selecciona ningún proveedor, se notifica al usuario.
                MessageBox.Show("Selecciona un proveedor para eliminar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Refrescar".
        /// Vuelve a cargar la lista de proveedores y limpia los campos de texto.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarProveedores();
            LimpiarCampos();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en una celda del DataGridView.
        /// Carga los datos del proveedor seleccionado en los TextBox para su visualización o edición.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Datos del evento, incluyendo el índice de la fila seleccionada.</param>
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se verifica que el índice de la fila sea válido y que la fila actual no sea nula.
            if (e.RowIndex >= 0 && dgvProveedores.CurrentRow != null)
            {
                // Se cargan los valores de cada columna en el TextBox correspondiente.
                txtNombreEmpresa.Text = dgvProveedores.Rows[e.RowIndex].Cells["NombreEmpresa"].Value.ToString();
                txtContacto.Text = dgvProveedores.Rows[e.RowIndex].Cells["Contacto"].Value.ToString();
                txtDireccion.Text = dgvProveedores.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
                txtTelefono.Text = dgvProveedores.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
            }
        }
    }
}
