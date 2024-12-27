namespace MP3_Downloader
{
    partial class Form_1_Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.descargas_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertidor_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.info_MenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargas_MenuItem,
            this.convertidor_MenuItem,
            this.info_MenuItem1,
            this.salirToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 135;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // descargas_MenuItem
            // 
            this.descargas_MenuItem.Image = global::MP3_Downloader.Properties.Resources.descargar;
            this.descargas_MenuItem.Name = "descargas_MenuItem";
            this.descargas_MenuItem.Size = new System.Drawing.Size(88, 20);
            this.descargas_MenuItem.Text = "Descargas";
            this.descargas_MenuItem.Click += new System.EventHandler(this.AbrirForm_Descargas);
            // 
            // convertidor_MenuItem
            // 
            this.convertidor_MenuItem.Image = global::MP3_Downloader.Properties.Resources.mp3;
            this.convertidor_MenuItem.Name = "convertidor_MenuItem";
            this.convertidor_MenuItem.Size = new System.Drawing.Size(98, 20);
            this.convertidor_MenuItem.Text = "Convertidor";
            this.convertidor_MenuItem.Click += new System.EventHandler(this.AbrirForm_Convertidor);
            // 
            // info_MenuItem1
            // 
            this.info_MenuItem1.Image = global::MP3_Downloader.Properties.Resources.papel;
            this.info_MenuItem1.Name = "info_MenuItem1";
            this.info_MenuItem1.Size = new System.Drawing.Size(82, 20);
            this.info_MenuItem1.Text = "Informes";
            // 
            // salirToolStripMenuItem1
            // 
            this.salirToolStripMenuItem1.Image = global::MP3_Downloader.Properties.Resources.cerrar;
            this.salirToolStripMenuItem1.Name = "salirToolStripMenuItem1";
            this.salirToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.salirToolStripMenuItem1.Text = "Salir";
            this.salirToolStripMenuItem1.Click += new System.EventHandler(this.CerrarTodosLosFormulariosHijos);
            // 
            // Form_1_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MP3_Downloader.Properties.Resources.logo1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form_1_Menu";
            this.Text = "Form_Menu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem descargas_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertidor_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem info_MenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem1;
    }
}