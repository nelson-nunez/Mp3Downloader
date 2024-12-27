using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3_Downloader
{
    public partial class Form_1_Menu : Form
    {
        private Form_Descargas form_Descargas;
        private Form_Convertidor form_Convertidor;
        //private Form_Informe form_Informe;

        public Form_1_Menu()
        {
            InitializeComponent();

            // Hacer que esta ventana sea un contenedor MDI
            this.IsMdiContainer = true;

            // Establecer propiedades para restringir tamaño y evitar maximización
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            AbrirFormGeneral(ref form_Descargas);
        }

        private void AbrirForm_Descargas(object sender, EventArgs e)
        {
            AbrirFormGeneral(ref form_Descargas);
        }

        private void AbrirForm_Convertidor(object sender, EventArgs e)
        {
            AbrirFormGeneral(ref form_Convertidor);
        }

        private void AbrirFormGeneral<T>(ref T formulario) where T : Form, new()
        {
            if (formulario == null || formulario.IsDisposed)
            {
                T nuevoFormulario = new T();
                // Dentro del padre
                nuevoFormulario.MdiParent = this;
                // Sin controles de cerrar
                nuevoFormulario.ControlBox = false;
                // Sin bordes
                nuevoFormulario.FormBorderStyle = FormBorderStyle.None;
                // Establecer la ubicación centrada
                nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
                formulario = nuevoFormulario;
            }

            formulario.Show();
            formulario.BringToFront();
        }

        private void CerrarTodosLosFormulariosHijos(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}
