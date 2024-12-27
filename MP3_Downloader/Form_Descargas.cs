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
        string convertedDirectory = @"";

        #endregion

        public Form_Descargas()
        {
            InitializeComponent();
            dataGridView1.ConfigurarGrids();
            dataGridView2.ConfigurarGrids();
            dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);           
            dataGridView2.CargarGrid(new List<string> { "Titulo", "Extension", "TiempoDescarga", "Ubicacion" }, downloadscompleted);

            #region Configurar labels

            destino_descargas_label.MaximumSize = new Size(170,0);
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

            if (String.IsNullOrEmpty(outputDirectory))
                toolStripStatusLabel1.Text = $"Seleccione un directorio de descarga";
        }

        #region Buttons

        private async void Descargar_Click(object sender, EventArgs e)
        {
            VerifySelectDirectory();

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
           //Entro a cambiarlo siempre
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputDirectory = folderBrowserDialog1.SelectedPath;
                destino_descargas_label.Text = outputDirectory;
            }
            toolStripStatusLabel1.Text = "";
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
                        toolStripStatusLabel1.Text = $"Descargando...";

                        dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);
                        var response = await youtube.DownloadMP3Async(itemurl.Url, outputDirectory);
                        // Convertir el archivo descargado a MP3 y eliminar el original
                        var tt = await YoutubeClientExtensions.ConvertDeletingToMP3Async(response.Ubicacion, outputDirectory);
                        downloadscompleted.Add(response);
                        // Actualizar estado a "Completado"
                        itemurl.Status = $"Completado {itemurl.Nombre}";
                        toolStripStatusLabel1.Text = $"Completado {itemurl.Nombre}";

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
                        MessageBox.Show("Ya está agregado ese tema");
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

        private void VerifySelectDirectory()
        {
            while (String.IsNullOrEmpty(outputDirectory))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    outputDirectory = folderBrowserDialog1.SelectedPath;
                    destino_descargas_label.Text = outputDirectory;
                }
                toolStripStatusLabel1.Text = "";
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
    }
}
