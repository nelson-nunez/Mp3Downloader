using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_Downloader
{
    public static class DataGridViewExtensions
    {

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

        public static void VerificarSeleccion(this DataGridView grid)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid), "La lista no puede ser nula.");
            if (grid.Rows.Count == 0)
                throw new Exception("La lista está vacía.");
            if (grid.SelectedRows.Count <= 0)
                throw new Exception("Debe seleccionar un item para continuar.");
            if (grid.SelectedRows[0].DataBoundItem == null)
                throw new Exception("No se ha vinculado ningún item a la fila seleccionada.");
        }

        public static void ConfigurarGrids(this DataGridView dataGridView, bool autogenerate = true)
        {
            dataGridView.MultiSelect = false;
            dataGridView.AutoGenerateColumns = autogenerate;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.DefaultCellStyle.Font = new Font("Calibri", 8);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 8, FontStyle.Bold);
            dataGridView.RowsDefaultCellStyle.Font = new Font("Calibri", 8);
        }

        public static void Mostrar<T>(this DataGridView dataGridView, List<T> listaDeItems)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(() => Mostrar(dataGridView, listaDeItems)));
                return;
            }

            // Asigna la lista como fuente de datos
            dataGridView.DataSource = null;
            dataGridView.DataSource = listaDeItems;
            dataGridView.AutoResizeColumns();

            //Va hasta el ultimo elemento
            if (dataGridView.Rows.Count > 0)
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
        }

        public static void CargarGrid<T>(this DataGridView dataGridView, List<string> campos, List<T> listaDeItems)
        {
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
            dataGridView.DataSource = null;
            dataGridView.DataSource = listaDeItems;
            dataGridView.AutoResizeColumns();
            
            //Va hasta el ultimo elemento
            if (dataGridView.Rows.Count > 0)
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
        }
    }
}
