using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_Downloader
{
    public static class DataGridViewExtensions
    {
        // Un DataGridView bindeado directamente a un List<T>, y reasignando
        // DataSource = null / DataSource = lista en cada refresco, desincroniza el
        // CurrencyManager interno (root cause del "El índice -1 no tiene un valor").
        // Se usa un BindingSource persistente por grid: su DataSource se actualiza,
        // pero el DataGridView.DataSource se asigna una única vez.
        private static readonly ConditionalWeakTable<DataGridView, BindingSource> _bindingSources
            = new ConditionalWeakTable<DataGridView, BindingSource>();

        private static BindingSource ObtenerBindingSource(DataGridView dataGridView)
        {
            return _bindingSources.GetValue(dataGridView, grid =>
            {
                var bindingSource = new BindingSource();
                grid.DataSource = bindingSource;
                return bindingSource;
            });
        }

        // BindingSource.DataSource ignora la asignación si es la MISMA referencia
        // que ya tenía (p. ej. listas de campo mutadas in-place y reenviadas con
        // RefrescarGrid). En ese caso hay que forzar el refresco con ResetBindings.
        private static void ActualizarDataSource<T>(BindingSource bindingSource, List<T> listaDeItems)
        {
            if (ReferenceEquals(bindingSource.DataSource, listaDeItems))
                bindingSource.ResetBindings(false);
            else
                bindingSource.DataSource = listaDeItems;
        }

        public static T VerificarYRetornarSeleccion<T>(this DataGridView grid) where T : class
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid), "El DataGridView no puede ser nulo.");
            if (grid.Rows.Count == 0)
                throw new Exception("El DataGridView está vacío.");
            if (grid.SelectedRows.Count <= 0)
                throw new Exception("Debe seleccionar un elemento para continuar.");
            if (grid.SelectedRows[0].DataBoundItem == null)
                throw new Exception("No se ha vinculado ningún elemento a la fila seleccionada.");

            return grid.SelectedRows[0].DataBoundItem as T;
        }

        public static void ConfigurarGrids(this DataGridView dataGridView)
        {
            dataGridView.MultiSelect = false;
            // Deshabilitar edición en el DataGridView
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically; // Evita edición directa
            dataGridView.ReadOnly = true; // Hacer todo el DataGridView de solo lectura
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToOrderColumns = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.Gainsboro;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.ColumnHeadersHeight = 30;

            // Configurar estilos
            dataGridView.DefaultCellStyle.Font = new Font("Calibri", 9);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black; // Establecer el color del texto
            dataGridView.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 90);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 244, 244);

            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Establecer el color del texto en las cabeceras
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 95, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.RowsDefaultCellStyle.Font = new Font("Calibri", 9);
            dataGridView.RowsDefaultCellStyle.ForeColor = Color.Black; // Establecer el color del texto en las filas
        }

        public static void CargarGrid<T>(this DataGridView dataGridView, List<string> campos, List<T> listaDeItems)
        {
            var bindingSource = ObtenerBindingSource(dataGridView);

            dataGridView.Columns.Clear();
            foreach (var field in campos)
            {
                var columna = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = field,
                    HeaderText = field,
                    Name = field
                };
                dataGridView.Columns.Add(columna);
            }
            dataGridView.AutoGenerateColumns = false;

            ActualizarDataSource(bindingSource, listaDeItems);
            dataGridView.AutoResizeColumns();
        }

        /// <summary>
        /// Refresca los datos de una grilla ya configurada (mismas columnas) conservando
        /// la posición del scroll, para evitar el parpadeo y el salto al tope que produce
        /// reconstruir columnas en cada actualización (por ejemplo, en cada tick de progreso).
        /// </summary>
        public static void RefrescarGrid<T>(this DataGridView dataGridView, List<T> listaDeItems)
        {
            var bindingSource = ObtenerBindingSource(dataGridView);
            int primeraFilaVisible = dataGridView.Rows.Count > 0 ? dataGridView.FirstDisplayedScrollingRowIndex : -1;

            ActualizarDataSource(bindingSource, listaDeItems);

            if (primeraFilaVisible >= 0 && primeraFilaVisible < dataGridView.Rows.Count)
                dataGridView.FirstDisplayedScrollingRowIndex = primeraFilaVisible;
        }

        public static void CargarGrids<T>(this DataGridView dataGridView, List<KeyValuePair<string, string>> campos, List<T> listaDeItems)
        {
            var bindingSource = ObtenerBindingSource(dataGridView);

            dataGridView.Columns.Clear();
            foreach (var field in campos)
            {
                var columna = new DataGridViewTextBoxColumn
                {
                    HeaderText = field.Value,
                    DataPropertyName = field.Key,
                    Name = field.Key
                };
                dataGridView.Columns.Add(columna);
            }
            dataGridView.AutoGenerateColumns = false;

            ActualizarDataSource(bindingSource, listaDeItems);
            dataGridView.AutoResizeColumns();
        }
    }
}
