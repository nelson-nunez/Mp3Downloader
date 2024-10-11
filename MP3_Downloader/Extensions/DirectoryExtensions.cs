using File = System.IO.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TagLib;

public static class DirectoryExtensions
{
    /// <summary>
    /// Elimina los archivos MP3 duplicados en un directorio, comparando tanto por nombre como por metadatos.
    /// Conserva el archivo con el prefijo "new_" y con la fecha de creación más reciente, eliminando el resto.
    /// </summary>
    /// <param name="directorio">El directorio donde buscar y eliminar los archivos duplicados.</param>
    public static async Task EliminarArchivosDuplicadosAsync(this string directorio)
    {
        if (string.IsNullOrEmpty(directorio) || !Directory.Exists(directorio))
        {
            throw new DirectoryNotFoundException("El directorio especificado no existe.");
        }

        // Obtener todos los archivos MP3 del directorio
        string[] audioFiles = Directory.GetFiles(directorio, "*.mp3");
        Dictionary<string, List<string>> nameMap = new Dictionary<string, List<string>>(); // Archivos por nombre
        Dictionary<string, List<string>> metadataMap = new Dictionary<string, List<string>>(); // Archivos por metadata

        // Primer bucle: organizar archivos en función del nombre y metadatos
        foreach (string filePath in audioFiles)
        {
            try
            {
                // Obtener el nombre del archivo sin la extensión y sin prefijo "new_"
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath).Replace("new_", "");

                // Agregar al diccionario de archivos por nombre
                if (!nameMap.ContainsKey(fileNameWithoutExtension))
                {
                    nameMap[fileNameWithoutExtension] = new List<string>();
                }
                nameMap[fileNameWithoutExtension].Add(filePath);

                // Leer la metadata del archivo (usando TagLib)
                var fileTag = TagLib.File.Create(filePath);
                string artist = fileTag.Tag.FirstAlbumArtist ?? "Unknown";
                string title = fileTag.Tag.Title ?? "Unknown";
                TimeSpan duration = fileTag.Properties.Duration;

                // Crear un identificador único por metadatos (título, artista y duración)
                string metadataKey = $"{artist}_{title}_{duration.TotalSeconds}";

                // Agregar al diccionario de archivos por metadata
                if (!metadataMap.ContainsKey(metadataKey))
                {
                    metadataMap[metadataKey] = new List<string>();
                }
                metadataMap[metadataKey].Add(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar archivo {filePath}: {ex.Message}");
            }
        }

        // Segundo bucle: eliminar duplicados por nombre
        foreach (var entry in nameMap)
        {
            if (entry.Value.Count > 1)
            {
                string fileToKeep = GetMostRecentFile(entry.Value);
                EliminarDuplicados(fileToKeep, entry.Value);
            }
        }

        // Tercer bucle: eliminar duplicados por metadata
        foreach (var entry in metadataMap)
        {
            if (entry.Value.Count > 1)
            {
                string fileToKeep = GetMostRecentFile(entry.Value);
                EliminarDuplicados(fileToKeep, entry.Value);
            }
        }
    }

    /// <summary>
    /// Compara una lista de archivos y devuelve el que tiene la fecha de creación más reciente,
    /// priorizando los que tienen el prefijo "new_".
    /// </summary>
    /// <param name="files">Lista de archivos a comparar.</param>
    /// <returns>El archivo que se debe conservar.</returns>
    private static string GetMostRecentFile(List<string> files)
    {
        string mostRecentFile = files[0];
        DateTime mostRecentTime = File.GetCreationTime(mostRecentFile);

        foreach (var file in files)
        {
            DateTime fileCreationTime = File.GetCreationTime(file);
            bool isNewFile = Path.GetFileName(file).StartsWith("new_");

            // Comparar las fechas de creación y priorizar el archivo con prefijo "new_"
            if ((isNewFile && !Path.GetFileName(mostRecentFile).StartsWith("new_")) || fileCreationTime > mostRecentTime)
            {
                mostRecentFile = file;
                mostRecentTime = fileCreationTime;
            }
        }
        return mostRecentFile;
    }

    /// <summary>
    /// Elimina todos los archivos duplicados excepto el que se debe conservar.
    /// </summary>
    /// <param name="fileToKeep">El archivo que se va a conservar.</param>
    /// <param name="allFiles">La lista de archivos que incluye los duplicados.</param>
    private static void EliminarDuplicados(string fileToKeep, List<string> allFiles)
    {
        foreach (var file in allFiles)
        {
            if (file != fileToKeep)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Archivo eliminado: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar archivo {file}: {ex.Message}");
                }
            }
        }
    }
}
