using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Downloader.Models
{
    public class Encolado
    {
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public string Tiempo { get; set; }


        public Encolado(string url, string nombre, string status, string tiempo)
        {
            Url = url;
            Nombre = nombre;
            Status = status;
            Tiempo = tiempo;
        }
    }
}
