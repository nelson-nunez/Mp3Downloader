using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Downloader.Models
{
    public class DownloadedVideo
    {
        public string Titulo { get; set; }
        public DateTime HoraDescarga { get; set; }
        public string TiempoDescarga { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }
        public string Url { get; set; }

        public DownloadedVideo(string title, string url, string extension, DateTime horaDescarga, string tiempoDescarga, string ubicacion)
        {
            Titulo = title;
            Url = url;
            Extension = extension;
            HoraDescarga = horaDescarga;
            TiempoDescarga = tiempoDescarga;
            Ubicacion = ubicacion;
        }
    }
}
