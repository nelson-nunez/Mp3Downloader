using MP3_Downloader.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using FFMpegCore;
using FFMpegCore.Enums;

namespace MP3_Downloader.Extensions
{
    public static class YoutubeClientExtensions
    {
        #region Descargas
        public static async Task<DownloadedVideo> DownloadMP3Async(this YoutubeClient youtube, string videoUrl, string outputDirectory)
        {
            // Inicializar el cronómetro y la lista de logs
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatchtotal = new Stopwatch();
            string logFilePath = Path.Combine(outputDirectory, "timing_log.txt");
            List<string> logMessages = new List<string>();

            //// setting global options
            string ffmpegPath = @"F:\PROYECTOS WEB & SISTEMAS\Mp3_Downloader\MP3_Downloader\packages";
            //string ffmpegPath = Directory.GetCurrentDirectory();
            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = ffmpegPath });

            try
            {
                stopwatch.Start();
                stopwatchtotal.Start();

                #region Obtener la información del video

                var video = await youtube.Videos.GetAsync(videoUrl);
                if (video == null)
                    throw new InvalidOperationException("Video not found.");

                string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));

                stopwatch.Stop();
                logMessages.Add($"[DownloadMP3Async] GetAsync: {stopwatch.ElapsedMilliseconds} ms - Video: {video.Title}");

                #endregion

                #region Obtener el manifiesto de streams del video

                stopwatch.Restart();
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                stopwatch.Stop();
                logMessages.Add($"[DownloadMP3Async] GetManifestAsync: {stopwatch.ElapsedMilliseconds} ms - Video: {video.Title}");

                #endregion

                #region Crear el directorio de salida si no existe

                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                string tempOutputFilePath = Path.Combine(outputDirectory, $"{sanitizedTitle}.temp");
                string outputFilePath = Path.Combine(outputDirectory, $"{sanitizedTitle}.mp3");

                stopwatch.Restart();
                await youtube.Videos.Streams.DownloadAsync(streamInfo, tempOutputFilePath);
                stopwatch.Stop();
                logMessages.Add($"[DownloadMP3Async] DownloadAsync: {stopwatch.ElapsedMilliseconds} ms - Video: {video.Title}");

                #endregion

                #region Convertir el archivo descargado a MP3 usando FFmpeg

                stopwatch.Restart();
                await FFMpegArguments
                    .FromFileInput(tempOutputFilePath)
                    .OutputToFile(outputFilePath, overwrite: true, options => options.WithAudioCodec(AudioCodec.LibMp3Lame))
                    .ProcessAsynchronously();
                stopwatch.Stop();
                logMessages.Add($"[DownloadMP3Async] FFMpeg Convert to MP3: {stopwatch.ElapsedMilliseconds} ms - Video: {video.Title}");

                // Eliminar el archivo temporal
                if (File.Exists(tempOutputFilePath))
                {
                    File.Delete(tempOutputFilePath);
                }

                #endregion

                stopwatchtotal.Stop();

                return new DownloadedVideo(sanitizedTitle, videoUrl, "mp3", DateTime.Now, stopwatch.ElapsedMilliseconds.ToString(), outputFilePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error downloading video: {ex.Message}");
            }
            finally
            {
                File.AppendAllLines(logFilePath, logMessages);
            }
        }
        #endregion

        #region Conversión

        public static async Task<string> ConvertToMP3Async(string inputFilePath, string outputDirectory)
        {
            Stopwatch stopwatch = new Stopwatch();
            string logFilePath = Path.Combine(outputDirectory, "timing_log.txt");
            List<string> logMessages = new List<string>();

            // Configurar la ruta de FFmpeg
            //string ffmpegPath = Directory.GetCurrentDirectory();             
            string ffmpegPath = @"F:\PROYECTOS WEB & SISTEMAS\Mp3_Downloader\MP3_Downloader\packages";

            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = ffmpegPath });

            try
            {
                if (!File.Exists(inputFilePath))
                    throw new FileNotFoundException("El archivo especificado no existe.", inputFilePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
                if (fileNameWithoutExtension.StartsWith("converted_"))
                {
                    return inputFilePath;
                }
                string outputFilePath = Path.Combine(outputDirectory, $"converted_{fileNameWithoutExtension}.mp3");
                stopwatch.Start();

                // Convertir el archivo a MP3 usando FFmpeg
                await FFMpegArguments
                    .FromFileInput(inputFilePath)
                    .OutputToFile(outputFilePath, overwrite: true, options => options.WithAudioCodec(AudioCodec.LibMp3Lame))
                    .ProcessAsynchronously();

                stopwatch.Stop();
                logMessages.Add($"[ConvertToMP3Async] Conversion to MP3: {stopwatch.ElapsedMilliseconds} ms - File: {fileNameWithoutExtension}");

                return outputFilePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error converting file to MP3: {ex.Message}");
            }
            finally
            {
                File.AppendAllLines(logFilePath, logMessages);
            }
        }


        #endregion


        #region Youtube urls

        public static async Task<List<Encolado>> ObtenerURLsYTTitleAsync(this string clipboardText)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var urls = clipboardText.ObtenerURLs();
            var results = new List<Encolado>();
            stopwatch.Stop();
            Console.WriteLine($"[ObtenerURLsYTTitleAsync] ObtenerURLs: {stopwatch.ElapsedMilliseconds} ms");

            foreach (var url in urls)
            {
                if (EsURLYouTube(url))
                {
                    stopwatch.Restart();
                    var title = await GetYouTubeVideoTitle(url);
                    stopwatch.Stop();
                    Console.WriteLine($"[ObtenerURLsYTTitleAsync] GetYouTubeVideoTitle: {stopwatch.ElapsedMilliseconds} ms");
                    results.Add(new Encolado(url, title, "Encolado","0"));
                }
            }

            return results;
        }

        private static string[] ObtenerURLs(this string clipboardText)
        {
            var urlsEncontradas = Regex.Matches(clipboardText, @"(https?://\S+)");
            return urlsEncontradas.Cast<Match>().Select(match => match.Groups[1].Value).ToArray();
        }

        private static bool EsURLYouTube(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                   uri.Host.EndsWith("youtube.com", StringComparison.OrdinalIgnoreCase);
        }

        private static async Task<string> GetYouTubeVideoTitle(string videoUrl)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var youtube = new YoutubeClient();
                var videoId = VideoId.Parse(videoUrl);
                var video = await youtube.Videos.GetAsync(videoId);
                string title = video.Title;

                stopwatch.Stop();
                Console.WriteLine($"[GetYouTubeVideoTitle] Total Time: {stopwatch.ElapsedMilliseconds} ms");
                return title;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"[GetYouTubeVideoTitle] Error Time: {stopwatch.ElapsedMilliseconds} ms");
                return "Error: " + ex.Message;
            }
        }

        #endregion

    }
}
