namespace MP3_Downloader
{
    partial class Form_Convertidor
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
            this.destino_convertir_txt = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.salidas_txt = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.button_CambiarOrden = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // destino_convertir_txt
            // 
            this.destino_convertir_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.destino_convertir_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destino_convertir_txt.Location = new System.Drawing.Point(6, 59);
            this.destino_convertir_txt.Name = "destino_convertir_txt";
            this.destino_convertir_txt.ReadOnly = true;
            this.destino_convertir_txt.Size = new System.Drawing.Size(335, 75);
            this.destino_convertir_txt.TabIndex = 146;
            this.destino_convertir_txt.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.salidas_txt);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button_CambiarOrden);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.destino_convertir_txt);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.groupBox2.Location = new System.Drawing.Point(617, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 609);
            this.groupBox2.TabIndex = 145;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversión a MP3";
            // 
            // salidas_txt
            // 
            this.salidas_txt.BackColor = System.Drawing.Color.White;
            this.salidas_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salidas_txt.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salidas_txt.Location = new System.Drawing.Point(6, 287);
            this.salidas_txt.Name = "salidas_txt";
            this.salidas_txt.ReadOnly = true;
            this.salidas_txt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.salidas_txt.Size = new System.Drawing.Size(335, 317);
            this.salidas_txt.TabIndex = 149;
            this.salidas_txt.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(599, 609);
            this.dataGridView1.TabIndex = 146;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.IndianRed;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = global::MP3_Downloader.Properties.Resources.eliminar;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(78, 245);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(197, 30);
            this.button6.TabIndex = 148;
            this.button6.Text = "Eliminar duplicados";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button_EliminarDuplicados_Click);
            // 
            // button_CambiarOrden
            // 
            this.button_CambiarOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.button_CambiarOrden.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(160)))));
            this.button_CambiarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_CambiarOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CambiarOrden.ForeColor = System.Drawing.Color.White;
            this.button_CambiarOrden.Image = global::MP3_Downloader.Properties.Resources.reordenar;
            this.button_CambiarOrden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_CambiarOrden.Location = new System.Drawing.Point(78, 192);
            this.button_CambiarOrden.Name = "button_CambiarOrden";
            this.button_CambiarOrden.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button_CambiarOrden.Size = new System.Drawing.Size(197, 30);
            this.button_CambiarOrden.TabIndex = 150;
            this.button_CambiarOrden.Text = "Cambiar orden";
            this.button_CambiarOrden.UseVisualStyleBackColor = false;
            this.button_CambiarOrden.Click += new System.EventHandler(this.button_CambiarOrden_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::MP3_Downloader.Properties.Resources.carpeta1;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(6, 21);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(197, 30);
            this.button5.TabIndex = 145;
            this.button5.Text = "Carpeta seleccionada";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button_Directorio_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = global::MP3_Downloader.Properties.Resources.convertir;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(78, 140);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(197, 30);
            this.button4.TabIndex = 138;
            this.button4.Text = "Convertir todo a mp3";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button_Convertir_Click);
            // 
            // Form_Convertidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(979, 633);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_Convertidor";
            this.Text = "Form_Convertidor";
            this.Load += new System.EventHandler(this.Form_Convertidor_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RichTextBox destino_convertir_txt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RichTextBox salidas_txt;
        private System.Windows.Forms.Button button_CambiarOrden;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

