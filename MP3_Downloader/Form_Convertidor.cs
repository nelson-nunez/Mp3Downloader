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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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

        private void EscribirSalida(string mensaje)
        {
            salidas_txt.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}{Environment.NewLine}");
            salidas_txt.SelectionStart = salidas_txt.TextLength;
            salidas_txt.ScrollToCaret();
        }

        private void SetBotonesEnabled(bool habilitado)
        {
            button4.Enabled = habilitado;
            button5.Enabled = habilitado;
            button6.Enabled = habilitado;
            button_CambiarOrden.Enabled = habilitado;
        }

        private static string LimpiarNombre(string nombre)
        {
            string resultado = Regex.Replace(nombre, "new_", "", RegexOptions.IgnoreCase);
            resultado = resultado.Replace('_', ' ');
            resultado = resultado.ToLowerInvariant();
            resultado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(resultado);
            resultado = Regex.Replace(resultado, @"[^\p{L}\p{Nd}\s-]", "");
            resultado = Regex.Replace(resultado, @"\s+", " ").Trim();
            return resultado;
        }

        private async void button_Directorio_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                convertedDirectory = folderBrowserDialog2.SelectedPath;
                destino_convertir_txt.Text = convertedDirectory;
                await ListarArchivosMp3Async();
            }
        }

        private async void button_Convertir_Click(object sender, EventArgs e)
        {
            if (!await SelectDirectory())
            {
                EscribirSalida("Operación cancelada: no se seleccionó ninguna carpeta.");
                return;
            }
            if (IsOcupied)
            {
                EscribirSalida("Awantiaaaaa estoy trabajando");
                return;
            }

            try
            {
                InputsExtensions.PedirConfirmacion("Desea continuar con la conversión? Se duplicaran todos los archivos mp3");
                await ConvertirDirectorio();
            }
            catch (Exception ex)
            {
                EscribirSalida("Error: " + ex.Message);
            }
            finally
            {
                await ListarArchivosMp3Async();
                button4.Text = "Convertir todo a mp3";
                button4.BackColor = Color.DarkSeaGreen;
                IsOcupied = false;
                SetBotonesEnabled(true);
            }
        }
      
        private async void button_EliminarDuplicados_Click(object sender, EventArgs e)
        {
            if (!await SelectDirectory())
            {
                EscribirSalida("Operación cancelada: no se seleccionó ninguna carpeta.");
                return;
            }
            if (IsOcupied)
            {
                EscribirSalida("Awantiaaaaa estoy trabajando");
                return;
            }

            try
            {
                InputsExtensions.PedirConfirmacion("Desea continuar con la eliminación de duplicados?");
                IsOcupied = true;
                SetBotonesEnabled(false);
                button6.Text = "Eliminando duplicados...";
                await Task.Delay(2000);
                var cant = await convertedDirectory.EliminarArchivosDuplicadosAsync();
                EscribirSalida("Se eliminaron " + cant + " archivos duplicados.");
            }
            catch (Exception ex)
            {
                EscribirSalida("Error: " + ex.Message);
            }
            finally
            {
                await ListarArchivosMp3Async();
                button6.Text = "Eliminar duplicados";
                IsOcupied = false;
                SetBotonesEnabled(true);
            }
        }

        private async void button_CambiarOrden_Click(object sender, EventArgs e)
        {
            if (!await SelectDirectory())
            {
                EscribirSalida("Operación cancelada: no se seleccionó ninguna carpeta.");
                return;
            }
            if (IsOcupied)
            {
                EscribirSalida("Awantiaaaaa estoy trabajando");
                return;
            }

            try
            {
                InputsExtensions.PedirConfirmacion("Desea continuar con el cambio de orden? Se renombrarán todas las canciones de la carpeta.");
                IsOcupied = true;
                SetBotonesEnabled(false);
                button_CambiarOrden.Text = "Cambiando orden...";

                string[] audioFiles = Directory.GetFiles(convertedDirectory, "*.mp3");
                var numerosUsados = new HashSet<int>();
                var random = new Random();

                foreach (string filePath in audioFiles)
                {
                    string directorio = Path.GetDirectoryName(filePath);
                    string extension = Path.GetExtension(filePath);
                    string nombreBase = Regex.Replace(Path.GetFileNameWithoutExtension(filePath), @"^\d{3}_", "");
                    nombreBase = LimpiarNombre(nombreBase);

                    int numero;
                    do
                    {
                        numero = random.Next(0, 1000);
                    } while (!numerosUsados.Add(numero));

                    string nuevaRuta = Path.Combine(directorio, $"{numero:D3}_{nombreBase}{extension}");
                    File.Move(filePath, nuevaRuta);
                }

                EscribirSalida("Se cambió el orden de " + audioFiles.Length + " canciones.");
            }
            catch (Exception ex)
            {
                EscribirSalida("Error: " + ex.Message);
            }
            finally
            {
                await ListarArchivosMp3Async();
                button_CambiarOrden.Text = "Cambiar orden";
                IsOcupied = false;
                SetBotonesEnabled(true);
            }
        }

        private async Task<bool> SelectDirectory()
        {
            while (String.IsNullOrEmpty(convertedDirectory))
            {
                if (folderBrowserDialog2.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                convertedDirectory = folderBrowserDialog2.SelectedPath;
                await ListarArchivosMp3Async();
            }
            return true;
        }
    
        private async Task ConvertirDirectorio()
        {
            IsOcupied = true;
            SetBotonesEnabled(false);
            button4.Text = "Convirtiendo, espere..";
            button4.BackColor = Color.DarkRed;
            string[] audioFiles = Directory.GetFiles(convertedDirectory, "*.mp3");
            List<Task> conversionTasks = new List<Task>();
            foreach (string filePath in audioFiles)
            {
                EscribirSalida("Convirtiendo: " + Path.GetFileName(filePath));
                await Task.Delay(100);
                Task conversionTask = YoutubeClientExtensions.ConvertToMP3Async(filePath, convertedDirectory);
                conversionTasks.Add(conversionTask);
            }
            // Esperar a que todas las conversiones terminen
            await Task.WhenAll(conversionTasks);
            EscribirSalida("Todos los archivos se han convertido correctamente.");
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

        private void Form_Convertidor_Load(object sender, EventArgs e)
        {

        }
    }
}
