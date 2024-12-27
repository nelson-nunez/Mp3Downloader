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
            this.carpeta_a_conver_label = new System.Windows.Forms.Label();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.convirtiendo_Label = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_Duplicados = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // carpeta_a_conver_label
            // 
            this.carpeta_a_conver_label.AutoSize = true;
            this.carpeta_a_conver_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carpeta_a_conver_label.Location = new System.Drawing.Point(17, 78);
            this.carpeta_a_conver_label.Name = "carpeta_a_conver_label";
            this.carpeta_a_conver_label.Size = new System.Drawing.Size(19, 15);
            this.carpeta_a_conver_label.TabIndex = 146;
            this.carpeta_a_conver_label.Text = "...";
            // 
            // convirtiendo_Label
            // 
            this.convirtiendo_Label.AutoSize = true;
            this.convirtiendo_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convirtiendo_Label.Location = new System.Drawing.Point(17, 180);
            this.convirtiendo_Label.Name = "convirtiendo_Label";
            this.convirtiendo_Label.Size = new System.Drawing.Size(19, 15);
            this.convirtiendo_Label.TabIndex = 147;
            this.convirtiendo_Label.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.label_Duplicados);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.convirtiendo_Label);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.carpeta_a_conver_label);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 335);
            this.groupBox2.TabIndex = 145;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descargas a Convertir";
            // 
            // label_Duplicados
            // 
            this.label_Duplicados.AutoSize = true;
            this.label_Duplicados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Duplicados.Location = new System.Drawing.Point(17, 284);
            this.label_Duplicados.Name = "label_Duplicados";
            this.label_Duplicados.Size = new System.Drawing.Size(19, 15);
            this.label_Duplicados.TabIndex = 149;
            this.label_Duplicados.Text = "...";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.IndianRed;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Image = global::MP3_Downloader.Properties.Resources.eliminar;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(20, 235);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(250, 35);
            this.button6.TabIndex = 148;
            this.button6.Text = "Eliminar duplicados";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button_EliminarDuplicados_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::MP3_Downloader.Properties.Resources.carpeta1;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(20, 38);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(250, 30);
            this.button5.TabIndex = 145;
            this.button5.Text = "Carpeta seleccionada...";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button_Directorio_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = global::MP3_Downloader.Properties.Resources.convertir;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(20, 127);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(250, 35);
            this.button4.TabIndex = 138;
            this.button4.Text = "Convertir carpeta a mp3";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button_Convertir_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 353);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(755, 268);
            this.dataGridView1.TabIndex = 146;
            // 
            // Form_Convertidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 633);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_Convertidor";
            this.Text = "Form_Descargas";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label carpeta_a_conver_label;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label convirtiendo_Label;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label_Duplicados;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

