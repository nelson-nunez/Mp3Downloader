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
    public partial class MP3Downloader : Form
    {
        #region Vars
      
        List<Encolado> colaUrls = new List<Encolado>();
        List<DownloadedVideo> downloadscompleted = new List<DownloadedVideo>();
        YoutubeClient youtube = new YoutubeClient();
        bool IsOcupied = false;
        //PATHS
        string outputDirectory = @"";
        string convertedDirectory = @"";

        #endregion

        public MP3Downloader()
        {
            InitializeComponent();
            dataGridView1.ConfigurarGrids();
            dataGridView2.ConfigurarGrids();
            dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);           
            dataGridView2.CargarGrid(new List<string> { "Titulo", "Extension", "TiempoDescarga", "Ubicacion" }, downloadscompleted);
           

            #region Configurar labels

            convirtiendo_Label.MaximumSize = new Size(430, 0);
            convirtiendo_Label.AutoSize = true;
            
            carpeta_a_conver_label.MaximumSize = new Size(350, 0);
            carpeta_a_conver_label.AutoSize = true;

            destino_descargas_label.MaximumSize = new Size(199,0);
            destino_descargas_label.AutoSize = true;

            #endregion

            #region Directorios

            try
            {
                // Para mi PC, verifico si el directorio existe y lo preseteo
                var directorioNormal = "F:\\4.Mi música\\ZZ-NUEVAS-DESCARGAS";
                if (Directory.Exists(directorioNormal))
                {
                    outputDirectory = directorioNormal;
                    destino_descargas_label.Text = outputDirectory;
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
        }
        #region Buttons

        private async void Descargar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(outputDirectory))
                SelectDirectory();

            if (IsOcupied)
            {
                MessageBox.Show("Awantiaaaaa estoy trabajando");
                return;
            }

            await DescargarVideosAsync();
        }

        private async void Encolar_Click(object sender, EventArgs e)
        {
            if (IsOcupied)
            {
                MessageBox.Show("Awantiaaaaa estoy trabajando");
                return;
            }

            await AgregarUrl();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectDirectory();
        }

        #endregion

        #region Descarga

        private async Task DescargarVideosAsync()
        {
            IsOcupied = true;
            string logFilePath = Path.Combine(outputDirectory, "log_tiempos.txt");
            try
            {
                for (int i = 0; i < colaUrls.Count; i++)
                {
                    var itemurl = colaUrls[i];
                    try
                    {
                        itemurl.Status = $"Descargando...";
                        dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);
                        var response = await youtube.DownloadMP3Async(itemurl.Url, outputDirectory);
                        downloadscompleted.Add(response);
                        // Actualizar estado a "Completado"
                        itemurl.Status = $"Completado {itemurl.Nombre}";
                        // Eliminar de la lista de encolados
                        colaUrls.RemoveAt(i);
                        i--; 
                    }
                    catch (Exception ex)
                    {
                        itemurl.Status = $"Error: {ex.Message}";
                        MessageBox.Show($"Error durante la descarga de {itemurl.Nombre}: {ex.Message}");
                    }
                    finally
                    {
                        dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);
                        dataGridView2.CargarGrid(new List<string> { "Titulo", "Extension", "TiempoDescarga", "Ubicacion" }, downloadscompleted);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsOcupied = false;
            }
        }

        private async Task AgregarUrl()
        {
            IsOcupied = true;

            try
            {
                var encolados = await ObtenerUrls();

                foreach (var item in encolados)
                {
                    if (!colaUrls.Any(x => x.Url == item.Url) && !downloadscompleted.Any(x => x.Url == item.Url))
                        colaUrls.Add(item);
                    else
                        MessageBox.Show("Ya está agregado ese tema gil");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);
                IsOcupied = false;
            }
        }

        private void SelectDirectory()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputDirectory = folderBrowserDialog1.SelectedPath;
                destino_descargas_label.Text = outputDirectory;
            }
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

        #region Conversion

        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                convertedDirectory = folderBrowserDialog2.SelectedPath;
                carpeta_a_conver_label.Text = convertedDirectory;
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOcupied)
                {
                    MessageBox.Show("Awantiaaaaa estoy trabajando");
                    return;
                }
                //Verifica que esté seleccionado
                if (String.IsNullOrEmpty(convertedDirectory))
                    SelectDirectory();

                InputsExtensions.PedirConfirmacion("Desea continuar con la conversión? Se duplicaran todos los archivos mp3");
                await ConvertirDirectorio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button4.Text = "Convertir carpeta a mp3";
                convirtiendo_Label.Text = "";
                button4.BackColor = Color.White;
                IsOcupied = false;
            }
        }

        private async Task ConvertirDirectorio()
        {
            IsOcupied = true;
            button4.Text = "Convirtiendo, espere..";
            button4.BackColor = Color.DarkRed;
            string[] audioFiles = Directory.GetFiles(convertedDirectory, "*.mp3");
            List<Task> conversionTasks = new List<Task>();
            foreach (string filePath in audioFiles)
            {
                convirtiendo_Label.Text = "Convirtiendo: " + filePath;
                await Task.Delay(100);
                Task conversionTask = YoutubeClientExtensions.ConvertToMP3Async(filePath, convertedDirectory);
                conversionTasks.Add(conversionTask);
            }
            // Esperar a que todas las conversiones terminen
            await Task.WhenAll(conversionTasks);
            MessageBox.Show("Todos los archivos se han convertido correctamente.");
            IsOcupied = false;
        }

        #endregion

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOcupied)
                {
                    MessageBox.Show("Awantiaaaaa estoy trabajando");
                    return;
                }
                if (String.IsNullOrEmpty(convertedDirectory))
                    SelectDirectory();
                InputsExtensions.PedirConfirmacion("Desea continuar con la eliminación de duplicados?");
                button6.Text = "Eliminando duplicados...";
                await Task.Delay(2000);
                var cant = await convertedDirectory.EliminarArchivosDuplicadosAsync();
                MessageBox.Show("Se eliminaron " + cant  + " archivos duplicados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button6.Text = "Eliminar duplicados";
                IsOcupied = false;
            }
        }
    }
}
