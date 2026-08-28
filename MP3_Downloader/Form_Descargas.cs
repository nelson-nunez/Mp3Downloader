using FFMpegCore;
using Microsoft.VisualBasic;
using MP3_Downloader.Extensions;
using MP3_Downloader.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YoutubeExplode;

namespace MP3_Downloader
{
    public partial class Form_Descargas : Form
    {
        #region Vars
      
        List<Encolado> colaUrls = new List<Encolado>();
        List<DownloadedVideo> downloadscompleted = new List<DownloadedVideo>();
        YoutubeClient youtube = new YoutubeClient();
        bool IsOcupied = false;
        //PATHS
        string outputDirectory = @"";

        #endregion

        public Form_Descargas()
        {
            InitializeComponent();
            dataGridView1.ConfigurarGrids();
            dataGridView2.ConfigurarGrids();
            dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, OrdenReciente(colaUrls));
            dataGridView2.CargarGrid(new List<string> { "Titulo", "Extension", "TiempoDescarga", "Ubicacion" }, OrdenReciente(downloadscompleted));
            ActualizarTitulosGrillas();

            // Los iconos de estos botones se achican para que no dominen un botón de 140x35
            button1.Image = RedimensionarIcono(Properties.Resources.descargar, 20, 20);
            button2.Image = RedimensionarIcono(Properties.Resources.agregar, 20, 20);

            #region Directorios

            try
            {
                // Para mi PC, verifico si el directorio existe y lo preseteo
                var directorioNormal = "F:\\4.Mi música\\ZZ-NUEVAS-DESCARGAS";
                if (Directory.Exists(directorioNormal))
                {
                    outputDirectory = directorioNormal;
                    destino_descargas_txt.Text = outputDirectory;
                }
                // Configurar la ruta de FFmpeg
                string ffmpegPath = Path.Combine(Directory.GetCurrentDirectory(), "ffmpeg.exe");
                if (!File.Exists(ffmpegPath))
                {
                    string parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName)?.FullName;
                    ffmpegPath = parentDirectory != null ? Path.Combine(parentDirectory, "ffmpeg.exe") : null;
                }
                if (File.Exists(ffmpegPath))
                    GlobalFFOptions.Configure(new FFOptions { BinaryFolder = Path.GetDirectoryName(ffmpegPath) });
                else
                    throw new FileNotFoundException("No se encontró el ejecutable de FFmpeg.");                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo de configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            if (String.IsNullOrEmpty(outputDirectory))
                toolStripStatusLabel1.Text = $"Seleccione un directorio de descarga";
        }

        private static Image RedimensionarIcono(Image original, int ancho, int alto)
        {
            var redimensionado = new Bitmap(ancho, alto);
            using (var g = Graphics.FromImage(redimensionado))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, ancho, alto);
            }
            return redimensionado;
        }

        private void SetOcupado(bool ocupado)
        {
            IsOcupied = ocupado;
            button_EliminarPendiente.Enabled = !ocupado;
            button_EliminarTodos.Enabled = !ocupado;
        }

        private void ActualizarTitulosGrillas()
        {
            groupBox1.Text = $"Descargas Pendientes ({colaUrls.Count})";
            groupBox3.Text = $"Descargas Finalizadas ({downloadscompleted.Count})";
        }

        // Los últimos elementos agregados se muestran arriba de todo en los grids,
        // sin alterar el orden real de colaUrls/downloadscompleted (se usan por índice al descargar).
        private static List<T> OrdenReciente<T>(List<T> lista)
        {
            var copia = new List<T>(lista);
            copia.Reverse();
            return copia;
        }

        private void RefrescarPendientes()
        {
            dataGridView1.RefrescarGrid(OrdenReciente(colaUrls));
        }

        private void RefrescarCompletados()
        {
            dataGridView2.RefrescarGrid(OrdenReciente(downloadscompleted));
        }

        #region Buttons

        private async void Descargar_Click(object sender, EventArgs e)
        {
            if (!VerifySelectDirectory())
                return;

            if (IsOcupied)
            {
                MessageBox.Show("Ya hay una descarga en curso, espere a que finalice.", "Proceso en curso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!colaUrls.Any())
            {
                MessageBox.Show("No hay descargas pendientes en la cola.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            await DescargarVideosAsync();
        }

        private async void Encolar_Click(object sender, EventArgs e)
        {
            if (IsOcupied)
            {
                MessageBox.Show("Ya hay una descarga en curso, espere a que finalice.", "Proceso en curso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            await AgregarUrl();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsOcupied)
            {
                MessageBox.Show("No se puede cambiar el destino mientras hay una descarga en curso.", "Proceso en curso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputDirectory = folderBrowserDialog1.SelectedPath;
                destino_descargas_txt.Text = outputDirectory;
                toolStripStatusLabel1.Text = "";
            }
        }

        private void button_EliminarPendiente_Click(object sender, EventArgs e)
        {
            try
            {
                var seleccionado = dataGridView1.VerificarYRetornarSeleccion<Encolado>();
                colaUrls.Remove(seleccionado);
                RefrescarPendientes();
                ActualizarTitulosGrillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_EliminarTodos_Click(object sender, EventArgs e)
        {
            if (!colaUrls.Any())
            {
                MessageBox.Show("No hay descargas pendientes en la cola.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Eliminar las {colaUrls.Count} descargas pendientes de la cola?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
                return;

            colaUrls.Clear();
            RefrescarPendientes();
            ActualizarTitulosGrillas();
        }

        private void button_ExportarLista_Click(object sender, EventArgs e)
        {
            if (!colaUrls.Any())
            {
                MessageBox.Show("No hay descargas pendientes para exportar.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivo de texto (*.txt)|*.txt";
                dialogo.FileName = "ListaPendientes.txt";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var lineas = colaUrls.Select((item, indice) => $"{indice + 1}|{item.Nombre}|{item.Url}");
                    File.WriteAllLines(dialogo.FileName, lineas);

                    MessageBox.Show($"Se exportaron {colaUrls.Count} temas.", "Exportación completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar la lista: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_ImportarLista_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Archivo de texto (*.txt)|*.txt";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var lineas = File.ReadAllLines(dialogo.FileName);
                    int agregados = 0;
                    int duplicados = 0;

                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        int primerPipe = linea.IndexOf('|');
                        int ultimoPipe = linea.LastIndexOf('|');
                        if (primerPipe < 0 || ultimoPipe <= primerPipe)
                            continue;

                        string nombre = linea.Substring(primerPipe + 1, ultimoPipe - primerPipe - 1).Trim();
                        string url = linea.Substring(ultimoPipe + 1).Trim();

                        if (string.IsNullOrWhiteSpace(url))
                            continue;

                        if (colaUrls.Any(x => x.Url == url) || downloadscompleted.Any(x => x.Url == url))
                        {
                            duplicados++;
                            continue;
                        }

                        colaUrls.Add(new Encolado(url, nombre, "Encolado", "0"));
                        agregados++;
                    }

                    RefrescarPendientes();
                    ActualizarTitulosGrillas();

                    string mensaje = $"Se agregaron {agregados} temas a la cola.";
                    if (duplicados > 0)
                        mensaje += $" Se omitieron {duplicados} duplicados.";
                    MessageBox.Show(mensaje, "Importación completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar la lista: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Descarga

        private async Task DescargarVideosAsync()
        {
            SetOcupado(true);
            try
            {
                for (int i = 0; i < colaUrls.Count; i++)
                {
                    var itemurl = colaUrls[i];
                    try
                    {
                        itemurl.Status = "Descargando...";
                        ActualizarProgreso(itemurl.Nombre, 0, "Descargando");
                        RefrescarPendientes();

                        int ultimoPorcentaje = -1;
                        var progreso = new Progress<double>(p =>
                        {
                            int porcentaje = Math.Max(0, Math.Min(100, (int)(Math.Floor(p * 10) * 10)));
                            if (porcentaje != ultimoPorcentaje)
                            {
                                ultimoPorcentaje = porcentaje;
                                ActualizarProgreso(itemurl.Nombre, porcentaje, "Descargando");
                            }
                        });

                        var response = await youtube.DownloadMP3Async(itemurl.Url, outputDirectory, progreso);

                        // Convertir el archivo descargado a MP3 y eliminar el original
                        itemurl.Status = "Convirtiendo...";
                        ActualizarProgreso(itemurl.Nombre, 100, "Convirtiendo");
                        await YoutubeClientExtensions.ConvertDeletingToMP3Async(response.Ubicacion, outputDirectory);
                        downloadscompleted.Add(response);

                        // Actualizar estado a "Completado"
                        itemurl.Status = "Completado";
                        ActualizarProgreso(itemurl.Nombre, 100, "Completado");

                        // Eliminar de la lista de encolados
                        colaUrls.RemoveAt(i);
                        i--;
                    }
                    catch (Exception ex)
                    {
                        itemurl.Status = $"Error: {ex.Message}";
                        toolStripStatusLabel1.Text = $"Error en {itemurl.Nombre}: {ex.Message}";
                        MessageBox.Show($"Error durante la descarga de {itemurl.Nombre}: {ex.Message}", "Error de descarga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        RefrescarPendientes();
                        RefrescarCompletados();
                        ActualizarTitulosGrillas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = "Listo";
                SetOcupado(false);
            }
        }

        /// <summary>
        /// Actualiza el status strip con la descarga actual y su avance, redondeado de a 10%.
        /// </summary>
        private void ActualizarProgreso(string nombreArchivo, int porcentaje, string accion)
        {
            porcentaje = Math.Max(0, Math.Min(100, porcentaje));
            toolStripProgressBar1.Value = porcentaje;
            toolStripStatusLabel1.Text = $"{accion}: {nombreArchivo} ({porcentaje}%)";
        }

        private async Task AgregarUrl()
        {
            SetOcupado(true);

            try
            {
                var encolados = await ObtenerUrls();

                if (!encolados.Any())
                {
                    MessageBox.Show("No se encontraron URLs de YouTube válidas.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (var item in encolados)
                {
                    if (!colaUrls.Any(x => x.Url == item.Url) && !downloadscompleted.Any(x => x.Url == item.Url))
                        colaUrls.Add(item);
                    else
                        MessageBox.Show($"Ya está agregado: {item.Nombre}", "Tema duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                RefrescarPendientes();
                ActualizarTitulosGrillas();
                SetOcupado(false);
            }
        }

        /// <summary>
        /// Pide un directorio de destino hasta obtener uno válido.
        /// Devuelve false si el usuario cancela el diálogo, para no quedar reabriéndolo indefinidamente.
        /// </summary>
        private bool VerifySelectDirectory()
        {
            while (String.IsNullOrEmpty(outputDirectory))
            {
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return false;

                outputDirectory = folderBrowserDialog1.SelectedPath;
                destino_descargas_txt.Text = outputDirectory;
                toolStripStatusLabel1.Text = "";
            }
            return true;
        }

        private async Task<List<Encolado>> ObtenerUrls()
        {
            var encolados = new List<Encolado>();
            var textocopiado = Clipboard.GetText();
            encolados = await YoutubeClientExtensions.ObtenerURLsYTTitleAsync(textocopiado);

            if (!encolados.Any())
            {
                var texto = Interaction.InputBox("Ingrese la url del video");
                encolados = await YoutubeClientExtensions.ObtenerURLsYTTitleAsync(texto);
            }
            return encolados;
        }

        #endregion

        private void Form_Descargas_Load(object sender, EventArgs e)
        {

        }
    }
}
