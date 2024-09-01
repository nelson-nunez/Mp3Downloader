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
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;

namespace MP3_Downloader
{
    public partial class Form1 : Form
    {
        #region Vars
        string outputDirectory = @"";
        string convertedDirectory = @"";
        List<Encolado> colaUrls = new List<Encolado>();
        List<DownloadedVideo> downloadscompleted = new List<DownloadedVideo>();
        YoutubeClient youtube = new YoutubeClient();
        bool IsOcupied = false;
        #endregion

        public Form1()
        {
            InitializeComponent();
            dataGridView1.ConfigurarGrids();
            dataGridView2.ConfigurarGrids();
            dataGridView1.CargarGrid(new List<string> { "Nombre", "Status", "Tiempo" }, colaUrls);
            dataGridView2.Mostrar(downloadscompleted);

            #region Configurar labels

            convertLabel.MaximumSize = new Size(295, 0);
            convertLabel.AutoSize = true;
            
            convertLabel.MaximumSize = new Size(295, 0);
            convertLabel.AutoSize = true;

            label5.MaximumSize = new Size(199,0);
            label5.AutoSize = true;

            #endregion
 
            // Verificar si el directorio existe
            var directorio_normal = "F:\\4.Mi música\\ZZ-NUEVAS-DESCARGAS";
            if (Directory.Exists(directorio_normal))
            {
                outputDirectory = directorio_normal;
                label5.Text = outputDirectory;
            }
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
                        dataGridView2.Mostrar(downloadscompleted);
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
                label5.Text = outputDirectory;
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
                label1.Text = convertedDirectory;
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
                InputsExtensions.PedirConfirmacion("Desea continuar con la conversión? Se duplicaran todos los archivos mp3");

                IsOcupied = true;
                button4.Text = "Convirtiendo, espere..";
                button4.BackColor = Color.DarkRed;

                string[] audioFiles = Directory.GetFiles(convertedDirectory, "*.mp3");
                List<Task> conversionTasks = new List<Task>();

                foreach (string filePath in audioFiles)
                {
                    convertLabel.Text = "Convirtiendo: " + filePath;
                    await Task.Delay(100);
                    Task conversionTask = YoutubeClientExtensions.ConvertToMP3Async(filePath, convertedDirectory);
                    conversionTasks.Add(conversionTask);
                }

                // Esperar a que todas las conversiones terminen
                await Task.WhenAll(conversionTasks);
                MessageBox.Show("Todos los archivos se han convertido exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button4.Text = "Convertir todo a mp3";
                convertLabel.Text = "";
                button4.BackColor = Color.White;
                IsOcupied = false;
            }
        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsOcupied)
            {
                MessageBox.Show("Awantiaaaaa estoy trabajando");
                return;
            }
            //Aca agregar opciones para sacar de la lista
        }
    }
}
