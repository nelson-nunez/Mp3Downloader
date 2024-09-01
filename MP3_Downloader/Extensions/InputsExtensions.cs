using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_Downloader.Extensions
{
    public static class InputsExtensions
    {
        public static int InputBoxNumeric(this string title)
        {
            var valor = Interaction.InputBox(title);
            if (!int.TryParse(valor, out int valorsal))
                throw new Exception("El valor ingresado debe ser numérico.");

            return valorsal;
        }

        public static DateTime InputBoxDateNumeric(this string title)
        {
            var fechaStr = Interaction.InputBox(title);
            DateTime fechaVencimiento;
            if (!DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaVencimiento))
                throw new Exception("Formato de fecha incorrecto. Use DD/MM/AAAA.");

            return fechaVencimiento;
        }

        public static void PedirConfirmacion()
        {
            DialogResult result = MessageBox.Show("Desea continuar con la operación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                throw new Exception("Se canceló la operación");
        }
        public static void PedirConfirmacion(string Message)
        {
            DialogResult result = MessageBox.Show(Message, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                throw new Exception("Se canceló la operación");
        }
    }
}
