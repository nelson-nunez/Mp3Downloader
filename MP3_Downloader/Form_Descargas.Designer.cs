namespace MP3_Downloader
{
    partial class Form_Descargas
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ImportarLista = new System.Windows.Forms.Button();
            this.button_ExportarLista = new System.Windows.Forms.Button();
            this.destino_descargas_txt = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_EliminarPendiente = new System.Windows.Forms.Button();
            this.button_EliminarTodos = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.button_ImportarLista);
            this.groupBox1.Controls.Add(this.button_ExportarLista);
            this.groupBox1.Controls.Add(this.destino_descargas_txt);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button_EliminarPendiente);
            this.groupBox1.Controls.Add(this.button_EliminarTodos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(979, 316);
            this.groupBox1.TabIndex = 142;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Descargas Pendientes";
            // 
            // button_ImportarLista
            // 
            this.button_ImportarLista.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_ImportarLista.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_ImportarLista.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ImportarLista.ForeColor = System.Drawing.Color.Black;
            this.button_ImportarLista.Image = global::MP3_Downloader.Properties.Resources.descargar1;
            this.button_ImportarLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ImportarLista.Location = new System.Drawing.Point(697, 114);
            this.button_ImportarLista.Name = "button_ImportarLista";
            this.button_ImportarLista.Size = new System.Drawing.Size(135, 30);
            this.button_ImportarLista.TabIndex = 154;
            this.button_ImportarLista.Text = "Importar Lista ";
            this.button_ImportarLista.UseVisualStyleBackColor = false;
            this.button_ImportarLista.Click += new System.EventHandler(this.button_ImportarLista_Click);
            // 
            // button_ExportarLista
            // 
            this.button_ExportarLista.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_ExportarLista.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_ExportarLista.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ExportarLista.ForeColor = System.Drawing.Color.Black;
            this.button_ExportarLista.Image = global::MP3_Downloader.Properties.Resources.exportar_archivo;
            this.button_ExportarLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ExportarLista.Location = new System.Drawing.Point(838, 114);
            this.button_ExportarLista.Name = "button_ExportarLista";
            this.button_ExportarLista.Size = new System.Drawing.Size(135, 30);
            this.button_ExportarLista.TabIndex = 153;
            this.button_ExportarLista.Text = "Exportar Lista ";
            this.button_ExportarLista.UseVisualStyleBackColor = false;
            this.button_ExportarLista.Click += new System.EventHandler(this.button_ExportarLista_Click);
            // 
            // destino_descargas_txt
            // 
            this.destino_descargas_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destino_descargas_txt.Location = new System.Drawing.Point(697, 63);
            this.destino_descargas_txt.Name = "destino_descargas_txt";
            this.destino_descargas_txt.ReadOnly = true;
            this.destino_descargas_txt.Size = new System.Drawing.Size(270, 45);
            this.destino_descargas_txt.TabIndex = 152;
            this.destino_descargas_txt.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(679, 271);
            this.dataGridView1.TabIndex = 137;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::MP3_Downloader.Properties.Resources.descargar;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(747, 263);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(180, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Descargar Todo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Descargar_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(160)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::MP3_Downloader.Properties.Resources.agregar;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(747, 222);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(180, 35);
            this.button2.TabIndex = 135;
            this.button2.Text = "Encolar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Encolar_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::MP3_Downloader.Properties.Resources.carpeta;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(697, 27);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(270, 30);
            this.button3.TabIndex = 130;
            this.button3.Text = " Seleccionar destino";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_EliminarPendiente
            // 
            this.button_EliminarPendiente.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_EliminarPendiente.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_EliminarPendiente.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_EliminarPendiente.ForeColor = System.Drawing.Color.Black;
            this.button_EliminarPendiente.Image = global::MP3_Downloader.Properties.Resources.eliminar;
            this.button_EliminarPendiente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_EliminarPendiente.Location = new System.Drawing.Point(697, 154);
            this.button_EliminarPendiente.Name = "button_EliminarPendiente";
            this.button_EliminarPendiente.Size = new System.Drawing.Size(135, 30);
            this.button_EliminarPendiente.TabIndex = 150;
            this.button_EliminarPendiente.Text = "Eliminar pendiente";
            this.button_EliminarPendiente.UseVisualStyleBackColor = false;
            this.button_EliminarPendiente.Click += new System.EventHandler(this.button_EliminarPendiente_Click);
            // 
            // button_EliminarTodos
            // 
            this.button_EliminarTodos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_EliminarTodos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_EliminarTodos.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_EliminarTodos.ForeColor = System.Drawing.Color.Black;
            this.button_EliminarTodos.Image = global::MP3_Downloader.Properties.Resources.eliminar;
            this.button_EliminarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_EliminarTodos.Location = new System.Drawing.Point(838, 154);
            this.button_EliminarTodos.Name = "button_EliminarTodos";
            this.button_EliminarTodos.Size = new System.Drawing.Size(135, 30);
            this.button_EliminarTodos.TabIndex = 151;
            this.button_EliminarTodos.Text = "Eliminar todos";
            this.button_EliminarTodos.UseVisualStyleBackColor = false;
            this.button_EliminarTodos.Click += new System.EventHandler(this.button_EliminarTodos_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.groupBox3.Location = new System.Drawing.Point(0, 323);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(979, 285);
            this.groupBox3.TabIndex = 144;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Descargas Finalizadas";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 21);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(955, 246);
            this.dataGridView2.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 611);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(979, 22);
            this.statusStrip1.TabIndex = 145;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(782, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(180, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // Form_Descargas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(979, 633);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_Descargas";
            this.Text = "Form_Descargas";
            this.Load += new System.EventHandler(this.Form_Descargas_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_EliminarPendiente;
        private System.Windows.Forms.Button button_EliminarTodos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.RichTextBox destino_descargas_txt;
        private System.Windows.Forms.Button button_ExportarLista;
        private System.Windows.Forms.Button button_ImportarLista;
    }
}

