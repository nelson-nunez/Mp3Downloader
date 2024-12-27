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
    public partial class Form_Convertidor : Form
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

        public Form_Convertidor()
        {
            InitializeComponent();        

            convirtiendo_Label.MaximumSize = new Size(450, 0);
            convirtiendo_Label.AutoSize = true;
            
            carpeta_a_conver_label.MaximumSize = new Size(450, 0);
            carpeta_a_conver_label.AutoSize = true;

            #region Directorios

            try
            {
                // Para mi PC, verifico si el directorio existe y lo preseteo
                var directorioNormal = "F:\\4.Mi música\\ZZ-NUEVAS-DESCARGAS";
                if (Directory.Exists(directorioNormal))
                {
                    outputDirectory = directorioNormal;
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
        
            dataGridView1.ConfigurarGrids();
        }

        private async void button_Directorio_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                convertedDirectory = folderBrowserDialog2.SelectedPath;
                carpeta_a_conver_label.Text = convertedDirectory;
                await ListarArchivosMp3Async();
            }
        }

        private async void button_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                SelectDirectory();
                if (IsOcupied)
                {
                    MessageBox.Show("Awantiaaaaa estoy trabajando");
                    return;
                }

                InputsExtensions.PedirConfirmacion("Desea continuar con la conversión? Se duplicaran todos los archivos mp3");
                await ConvertirDirectorio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListarArchivosMp3Async(); 
                button4.Text = "Convertir carpeta a mp3";
                convirtiendo_Label.Text = "";
                button4.BackColor = Color.White;
                IsOcupied = false;
            }
        }
      
        private async void button_EliminarDuplicados_Click(object sender, EventArgs e)
        {
            try
            {
                SelectDirectory(); 
                if (IsOcupied)
                {
                    MessageBox.Show("Awantiaaaaa estoy trabajando");
                    return;
                }
                    
                InputsExtensions.PedirConfirmacion("Desea continuar con la eliminación de duplicados?");
                button6.Text = "Eliminando duplicados...";
                await Task.Delay(2000);
                var cant = await convertedDirectory.EliminarArchivosDuplicadosAsync();
                MessageBox.Show("Se eliminaron " + cant + " archivos duplicados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListarArchivosMp3Async();
                button6.Text = "Eliminar duplicados";
                IsOcupied = false;
            }
        }

        private async void SelectDirectory()
        {
            while (String.IsNullOrEmpty(convertedDirectory))
            {
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                {
                    convertedDirectory = folderBrowserDialog2.SelectedPath;
                }
                await ListarArchivosMp3Async();
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

        private async Task ListarArchivosMp3Async()
        {
            string[] audioFiles = Directory.GetFiles(convertedDirectory, "*.mp3");
            var colaArchivos = new List<dynamic>();

            foreach (string filePath in audioFiles)
            {
                var fileInfo = new FileInfo(filePath);
                string nombre = fileInfo.Name;
                string extension = fileInfo.Extension;
                colaArchivos.Add(new { Nombre = nombre, Extension = extension });
            }

            dataGridView1.CargarGrid(new List<string> { "Nombre", "Duracion", "Extension" }, colaArchivos);
        }
    }
}
