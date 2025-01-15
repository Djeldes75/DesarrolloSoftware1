using InventarioApp.Controladores;
using InventarioApp.Modelos;
using System;
using System.IO;
using System.Windows.Forms;

namespace InventarioApp.Vistas
{
    /// <summary>
    /// Formulario para la administración de Productos (CRUD).
    /// Permite también filtrar por Categoría, Proveedor, 
    /// mostrar productos con bajo stock, buscar por nombre 
    /// y exportar datos a CSV.
    /// </summary>
    public partial class frmProductos : Form
    {
        /// <summary>
        /// Constructor principal del formulario.
        /// Inicializa los componentes de la interfaz.
        /// </summary>
        public frmProductos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara cuando el formulario carga.
        /// Inicializa los combos y carga la lista de productos.
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void frmProductos_Load(object sender, EventArgs e)
        {
            // Se cargan los ComboBox con datos de Categorías y Proveedores.
            CargarCombos();
            // Se carga la lista completa de productos en el DataGridView.
            CargarProductos();
        }

        /// <summary>
        /// Carga las listas de Categorías y Proveedores en sus respectivos ComboBox.
        /// </summary>
        private void CargarCombos()
        {
            // Cargar combo de Categorías
            var listaCat = CategoriaController.ListarTodo();
            cmbCategorias.DataSource = listaCat;
            cmbCategorias.DisplayMember = "NombreCategoria";
            cmbCategorias.ValueMember = "IDCategoria";

            // Cargar combo de Proveedores
            var listaProv = ProveedorController.ListarTodo();
            cmbProveedores.DataSource = listaProv;
            cmbProveedores.DisplayMember = "NombreEmpresa";
            cmbProveedores.ValueMember = "IDProveedor";

            // Se pueden agregar más combos si se desean otros filtros
        }

        /// <summary>
        /// Carga todos los productos desde el controlador y los muestra en el DataGridView.
        /// Se configuran manualmente las columnas para tener un control total sobre su apariencia.
        /// </summary>
        private void CargarProductos()
        {
            // Obtiene la lista completa de productos.
            var productos = ProductoController.ListarTodo();

            // Desactivar la autogeneración de columnas para configurar columnas manualmente.
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            // 1. Columna para CodigoProducto
            DataGridViewTextBoxColumn colCodigo = new DataGridViewTextBoxColumn
            {
                Name = "CodigoProducto",              // Nombre interno que coincide con la propiedad del modelo.
                DataPropertyName = "CodigoProducto",  // Nombre de la propiedad del objeto Producto.
                HeaderText = "Código",                // Título que se muestra en el encabezado.
                ReadOnly = true                       // Por lo general el código no es editable.
            };
            dgvProductos.Columns.Add(colCodigo);

            // 2. Columna para Nombre
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre"
            };
            dgvProductos.Columns.Add(colNombre);

            // 3. Columna para IDCategoria
            DataGridViewTextBoxColumn colCategoria = new DataGridViewTextBoxColumn
            {
                Name = "IDCategoria",
                DataPropertyName = "IDCategoria",
                HeaderText = "Categoría"
            };
            dgvProductos.Columns.Add(colCategoria);

            // 4. Columna para Precio
            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                DataPropertyName = "Precio",
                HeaderText = "Precio"
            };
            dgvProductos.Columns.Add(colPrecio);

            // 5. Columna para Existencia
            DataGridViewTextBoxColumn colExistencia = new DataGridViewTextBoxColumn
            {
                Name = "Existencia",
                DataPropertyName = "Existencia",
                HeaderText = "Existencia"
            };
            dgvProductos.Columns.Add(colExistencia);

            // 6. Columna para IDProveedor
            DataGridViewTextBoxColumn colProveedor = new DataGridViewTextBoxColumn
            {
                Name = "IDProveedor",
                DataPropertyName = "IDProveedor",
                HeaderText = "Proveedor"
            };
            dgvProductos.Columns.Add(colProveedor);

            // Se asigna la lista de productos al DataSource del DataGridView.
            dgvProductos.DataSource = productos;

            // (Opcional) Imprime en consola los nombres de las columnas para depuración.
            foreach (DataGridViewColumn col in dgvProductos.Columns)
            {
                Console.WriteLine($"Columna -> Name: {col.Name}, DataPropertyName: {col.DataPropertyName}");
            }
        }

        /// <summary>
        /// Limpia los TextBox y restablece el valor seleccionado en los ComboBox.
        /// </summary>
        private void LimpiarCampos()
        {
            // Limpia los campos de texto.
            txtNombreProducto.Clear();
            txtPrecio.Clear();
            txtExistencia.Clear();
            txtBuscar.Clear();

            // Si hay ítems en los ComboBox, se selecciona el primero por defecto.
            if (cmbCategorias.Items.Count > 0)
                cmbCategorias.SelectedIndex = 0;

            if (cmbProveedores.Items.Count > 0)
                cmbProveedores.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Agregar".
        /// Crea un nuevo objeto Producto con los datos capturados en la interfaz
        /// y llama al controlador para guardarlo en la base de datos.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Verifica que los ComboBox tengan un valor seleccionado.
            if (cmbCategorias.SelectedValue == null || cmbProveedores.SelectedValue == null)
            {
                MessageBox.Show("Falta seleccionar categoría o proveedor.");
                return;
            }

            // Crea un nuevo objeto Producto y asigna los valores de los controles.
            Producto p = new Producto
            {
                Nombre = txtNombreProducto.Text.Trim(),
                IDCategoria = Convert.ToInt32(cmbCategorias.SelectedValue),
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Existencia = Convert.ToInt32(txtExistencia.Text),
                IDProveedor = Convert.ToInt32(cmbProveedores.SelectedValue)
            };

            // Llama al método Agregar del controlador para insertar el producto.
            ProductoController.Agregar(p);

            MessageBox.Show("Producto agregado correctamente.");

            // Refresca el DataGridView y limpia los controles.
            CargarProductos();
            LimpiarCampos();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Editar".
        /// Verifica que se haya seleccionado un producto, actualiza el objeto Producto
        /// con los nuevos valores y llama al controlador para actualizar el registro.
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verifica que exista una fila seleccionada en el DataGridView.
            if (dgvProductos.CurrentRow != null)
            {
                // Obtiene el Código del producto seleccionado.
                int codigo = Convert.ToInt32(dgvProductos.CurrentRow.Cells["CodigoProducto"].Value);

                // Crea un objeto Producto con los valores actualizados.
                Producto p = new Producto
                {
                    CodigoProducto = codigo,
                    Nombre = txtNombreProducto.Text.Trim(),
                    IDCategoria = Convert.ToInt32(cmbCategorias.SelectedValue),
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Existencia = Convert.ToInt32(txtExistencia.Text),
                    IDProveedor = Convert.ToInt32(cmbProveedores.SelectedValue)
                };

                // Llama al método Editar del controlador para actualizar el producto.
                ProductoController.Editar(p);
                MessageBox.Show("Producto editado correctamente.");

                // Refresca el listado y limpia los campos.
                CargarProductos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Selecciona un producto de la lista para editar.");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Eliminar".
        /// Toma el producto seleccionado y lo elimina de la base de datos mediante el controlador.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica que se haya seleccionado un producto.
            if (dgvProductos.CurrentRow != null)
            {
                // Solicita confirmación al usuario.
                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // Se asume que la columna se llama "CodigoProducto".
                        int codigo = Convert.ToInt32(dgvProductos.CurrentRow.Cells["CodigoProducto"].Value);

                        // Llama al controlador para eliminar el producto.
                        bool resultado = ProductoController.Eliminar(codigo);

                        if (resultado)
                        {
                            MessageBox.Show("Producto eliminado correctamente.",
                                "Operación exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el producto.",
                                "Operación fallida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }

                        // Refresca el listado de productos.
                        CargarProductos();
                        // Opcionalmente, se puede limpiar los campos.
                        // LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al eliminar el producto: {ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un producto para eliminar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Refrescar".
        /// Recarga la lista de productos y limpia los controles de entrada.
        /// </summary>
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            LimpiarCampos();
        }

        /// <summary>
        /// Evento que se activa al hacer clic en una celda del DataGridView.
        /// Pasa los datos del producto seleccionado a los controles para su visualización o edición.
        /// </summary>
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se verifica que la fila seleccionada sea válida.
            if (e.RowIndex >= 0 && dgvProductos.CurrentRow != null)
            {
                // Se asignan los valores de la fila a los TextBox correspondientes.
                txtNombreProducto.Text = dgvProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtPrecio.Text = dgvProductos.Rows[e.RowIndex].Cells["Precio"].Value.ToString();
                txtExistencia.Text = dgvProductos.Rows[e.RowIndex].Cells["Existencia"].Value.ToString();

                // Se actualizan los ComboBox según los valores de IDCategoria e IDProveedor.
                int idCat = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["IDCategoria"].Value);
                cmbCategorias.SelectedValue = idCat;

                int idProv = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["IDProveedor"].Value);
                cmbProveedores.SelectedValue = idProv;
            }
        }

        /// <summary>
        /// Filtra los productos según la categoría seleccionada en el ComboBox "cmbCategorias"
        /// y actualiza el DataGridView con los resultados.
        /// </summary>
        private void btnFiltrarPorCategoria_Click(object sender, EventArgs e)
        {
            if (cmbCategorias.SelectedValue != null)
            {
                int idCat = Convert.ToInt32(cmbCategorias.SelectedValue);
                var productosFiltrados = ProductoController.ListarPorCategoria(idCat);
                dgvProductos.DataSource = productosFiltrados;
            }
        }

        /// <summary>
        /// Filtra los productos según el proveedor seleccionado en el ComboBox "cmbProveedores"
        /// y actualiza el DataGridView con los resultados.
        /// </summary>
        private void btnFiltrarPorProveedor_Click(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedValue != null)
            {
                int idProv = Convert.ToInt32(cmbProveedores.SelectedValue);
                var productosFiltrados = ProductoController.ListarPorProveedor(idProv);
                dgvProductos.DataSource = productosFiltrados;
            }
        }

        /// <summary>
        /// Muestra en el DataGridView el reporte de productos con stock bajo (Existencia menor a 10).
        /// </summary>
        private void btnBajoStock_Click(object sender, EventArgs e)
        {
            var listaBajoStock = ProductoController.ReporteBajoStock();
            dgvProductos.DataSource = listaBajoStock;
        }

        /// <summary>
        /// Realiza una búsqueda de productos por nombre utilizando el texto de "txtBuscar".
        /// Muestra los resultados en el DataGridView.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            var resultados = ProductoController.BuscarPorNombre(texto);
            dgvProductos.DataSource = resultados;
        }

        /// <summary>
        /// Exporta los productos mostrados en el DataGridView a un archivo CSV.
        /// Solicita al usuario la ubicación y nombre del archivo mediante un SaveFileDialog.
        /// </summary>
        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            // Verifica que haya datos en el DataGridView.
            if (dgvProductos.Rows.Count > 0)
            {
                // Configura el SaveFileDialog para archivos CSV.
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = "Productos.csv"
                };

                // Si el usuario elige una ubicación y da OK...
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Se crea un StreamWriter para el archivo CSV.
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        // Se escribe la cabecera del CSV.
                        sw.WriteLine("CodigoProducto,Nombre,IDCategoria,Precio,Existencia,IDProveedor");
                        // Se recorren todas las filas del DataGridView.
                        foreach (DataGridViewRow row in dgvProductos.Rows)
                        {
                            // Se omite la fila de nueva entrada.
                            if (!row.IsNewRow)
                            {
                                string cod = row.Cells["CodigoProducto"].Value.ToString();
                                string nom = row.Cells["Nombre"].Value.ToString();
                                string cat = row.Cells["IDCategoria"].Value.ToString();
                                string pre = row.Cells["Precio"].Value.ToString();
                                string exi = row.Cells["Existencia"].Value.ToString();
                                string prov = row.Cells["IDProveedor"].Value.ToString();

                                // Se escribe la línea en el archivo CSV.
                                sw.WriteLine($"{cod},{nom},{cat},{pre},{exi},{prov}");
                            }
                        }
                    }
                    MessageBox.Show("Archivo CSV exportado correctamente.");
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.");
            }
        }
    }
}
