namespace ObtencionDatos
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.ArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AbrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripPuertoSerie = new System.Windows.Forms.ToolStripMenuItem();
            this.CboPuertoSerie = new System.Windows.Forms.ToolStripComboBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.Calibrar = new System.Windows.Forms.Button();
            this.PuertoSerie = new System.IO.Ports.SerialPort(this.components);
            this.BtnAbrirCerrar = new System.Windows.Forms.Button();
            this.CorreccionYmas = new System.Windows.Forms.Button();
            this.CorreccionXmas = new System.Windows.Forms.Button();
            this.CorreccionXmenos = new System.Windows.Forms.Button();
            this.CorreccionYmenos = new System.Windows.Forms.Button();
            this.MmCorreccion = new System.Windows.Forms.MaskedTextBox();
            this.TxtRecibir = new System.Windows.Forms.TextBox();
            this.TxtEscribir = new System.Windows.Forms.TextBox();
            this.BtnEnviar = new System.Windows.Forms.Button();
            this.CalibracionLista = new System.Windows.Forms.Button();
            this.BtnComenzar = new System.Windows.Forms.Button();
            this.VisualizarPuntos = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(12, 94);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(445, 195);
            this.TextBox1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArchivoToolStripMenuItem,
            this.ToolStripMenuItemConfig});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(590, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // ArchivoToolStripMenuItem
            // 
            this.ArchivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AbrirToolStripMenuItem});
            this.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem";
            this.ArchivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ArchivoToolStripMenuItem.Text = "Archivo";
            // 
            // AbrirToolStripMenuItem
            // 
            this.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem";
            this.AbrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AbrirToolStripMenuItem.Text = "Abrir";
            this.AbrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemConfig
            // 
            this.ToolStripMenuItemConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripPuertoSerie});
            this.ToolStripMenuItemConfig.Name = "ToolStripMenuItemConfig";
            this.ToolStripMenuItemConfig.Size = new System.Drawing.Size(95, 20);
            this.ToolStripMenuItemConfig.Text = "Configuración";
            // 
            // ToolStripPuertoSerie
            // 
            this.ToolStripPuertoSerie.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CboPuertoSerie});
            this.ToolStripPuertoSerie.Name = "ToolStripPuertoSerie";
            this.ToolStripPuertoSerie.Size = new System.Drawing.Size(137, 22);
            this.ToolStripPuertoSerie.Text = "Puerto Serie";
            // 
            // CboPuertoSerie
            // 
            this.CboPuertoSerie.Name = "CboPuertoSerie";
            this.CboPuertoSerie.Size = new System.Drawing.Size(121, 23);
            this.CboPuertoSerie.Click += new System.EventHandler(this.ToolStripComboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path";
            // 
            // Calibrar
            // 
            this.Calibrar.Enabled = false;
            this.Calibrar.Location = new System.Drawing.Point(479, 73);
            this.Calibrar.Name = "Calibrar";
            this.Calibrar.Size = new System.Drawing.Size(99, 29);
            this.Calibrar.TabIndex = 3;
            this.Calibrar.Text = "Calibrar";
            this.Calibrar.UseVisualStyleBackColor = true;
            this.Calibrar.Click += new System.EventHandler(this.Calibrar_Click);
            // 
            // PuertoSerie
            // 
            this.PuertoSerie.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.PuertoSerie_DataReceived_1);
            // 
            // BtnAbrirCerrar
            // 
            this.BtnAbrirCerrar.Location = new System.Drawing.Point(479, 34);
            this.BtnAbrirCerrar.Name = "BtnAbrirCerrar";
            this.BtnAbrirCerrar.Size = new System.Drawing.Size(102, 23);
            this.BtnAbrirCerrar.TabIndex = 0;
            this.BtnAbrirCerrar.Text = "Abrir Puerto";
            this.BtnAbrirCerrar.Click += new System.EventHandler(this.BtnAbrirCerrar_Click_1);
            // 
            // CorreccionYmas
            // 
            this.CorreccionYmas.Enabled = false;
            this.CorreccionYmas.Location = new System.Drawing.Point(513, 114);
            this.CorreccionYmas.Name = "CorreccionYmas";
            this.CorreccionYmas.Size = new System.Drawing.Size(34, 34);
            this.CorreccionYmas.TabIndex = 4;
            this.CorreccionYmas.UseVisualStyleBackColor = true;
            this.CorreccionYmas.Click += new System.EventHandler(this.CorreccionYmas_Click);
            // 
            // CorreccionXmas
            // 
            this.CorreccionXmas.Enabled = false;
            this.CorreccionXmas.Location = new System.Drawing.Point(548, 146);
            this.CorreccionXmas.Name = "CorreccionXmas";
            this.CorreccionXmas.Size = new System.Drawing.Size(34, 34);
            this.CorreccionXmas.TabIndex = 5;
            this.CorreccionXmas.UseVisualStyleBackColor = true;
            this.CorreccionXmas.Click += new System.EventHandler(this.CorreccionXmenos_Click);
            // 
            // CorreccionXmenos
            // 
            this.CorreccionXmenos.Enabled = false;
            this.CorreccionXmenos.Location = new System.Drawing.Point(478, 146);
            this.CorreccionXmenos.Name = "CorreccionXmenos";
            this.CorreccionXmenos.Size = new System.Drawing.Size(34, 34);
            this.CorreccionXmenos.TabIndex = 13;
            this.CorreccionXmenos.Click += new System.EventHandler(this.CorreccionXmenos_Click);
            // 
            // CorreccionYmenos
            // 
            this.CorreccionYmenos.Enabled = false;
            this.CorreccionYmenos.Location = new System.Drawing.Point(513, 180);
            this.CorreccionYmenos.Name = "CorreccionYmenos";
            this.CorreccionYmenos.Size = new System.Drawing.Size(34, 34);
            this.CorreccionYmenos.TabIndex = 7;
            this.CorreccionYmenos.UseVisualStyleBackColor = true;
            this.CorreccionYmenos.Click += new System.EventHandler(this.CorreccionYmenos_Click);
            // 
            // MmCorreccion
            // 
            this.MmCorreccion.Culture = new System.Globalization.CultureInfo("en-US");
            this.MmCorreccion.Enabled = false;
            this.MmCorreccion.Location = new System.Drawing.Point(515, 154);
            this.MmCorreccion.Mask = "99.9";
            this.MmCorreccion.Name = "MmCorreccion";
            this.MmCorreccion.Size = new System.Drawing.Size(29, 20);
            this.MmCorreccion.TabIndex = 12;
            // 
            // TxtRecibir
            // 
            this.TxtRecibir.Location = new System.Drawing.Point(12, 68);
            this.TxtRecibir.Name = "TxtRecibir";
            this.TxtRecibir.Size = new System.Drawing.Size(100, 20);
            this.TxtRecibir.TabIndex = 9;
            // 
            // TxtEscribir
            // 
            this.TxtEscribir.Location = new System.Drawing.Point(175, 68);
            this.TxtEscribir.Name = "TxtEscribir";
            this.TxtEscribir.Size = new System.Drawing.Size(100, 20);
            this.TxtEscribir.TabIndex = 10;
            // 
            // BtnEnviar
            // 
            this.BtnEnviar.Location = new System.Drawing.Point(281, 66);
            this.BtnEnviar.Name = "BtnEnviar";
            this.BtnEnviar.Size = new System.Drawing.Size(75, 23);
            this.BtnEnviar.TabIndex = 11;
            this.BtnEnviar.Text = "Enviar";
            this.BtnEnviar.UseVisualStyleBackColor = true;
            this.BtnEnviar.Click += new System.EventHandler(this.BtnEnviar_Click);
            // 
            // CalibracionLista
            // 
            this.CalibracionLista.Enabled = false;
            this.CalibracionLista.Location = new System.Drawing.Point(494, 220);
            this.CalibracionLista.Name = "CalibracionLista";
            this.CalibracionLista.Size = new System.Drawing.Size(75, 23);
            this.CalibracionLista.TabIndex = 14;
            this.CalibracionLista.Text = "Siguiente";
            this.CalibracionLista.UseVisualStyleBackColor = true;
            this.CalibracionLista.Click += new System.EventHandler(this.CalibracionLista_Click);
            // 
            // BtnComenzar
            // 
            this.BtnComenzar.Location = new System.Drawing.Point(382, 65);
            this.BtnComenzar.Name = "BtnComenzar";
            this.BtnComenzar.Size = new System.Drawing.Size(75, 23);
            this.BtnComenzar.TabIndex = 15;
            this.BtnComenzar.Text = "Comenzar";
            this.BtnComenzar.UseVisualStyleBackColor = true;
            this.BtnComenzar.Click += new System.EventHandler(this.BtnComenzar_Click);
            // 
            // VisualizarPuntos
            // 
            this.VisualizarPuntos.Location = new System.Drawing.Point(494, 249);
            this.VisualizarPuntos.Name = "VisualizarPuntos";
            this.VisualizarPuntos.Size = new System.Drawing.Size(75, 40);
            this.VisualizarPuntos.TabIndex = 16;
            this.VisualizarPuntos.Text = "Visualizar puntos";
            this.VisualizarPuntos.UseVisualStyleBackColor = true;
            this.VisualizarPuntos.Click += new System.EventHandler(this.VisualizarPuntos_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 301);
            this.Controls.Add(this.VisualizarPuntos);
            this.Controls.Add(this.BtnComenzar);
            this.Controls.Add(this.CalibracionLista);
            this.Controls.Add(this.BtnEnviar);
            this.Controls.Add(this.TxtEscribir);
            this.Controls.Add(this.TxtRecibir);
            this.Controls.Add(this.MmCorreccion);
            this.Controls.Add(this.CorreccionYmenos);
            this.Controls.Add(this.CorreccionXmenos);
            this.Controls.Add(this.CorreccionXmas);
            this.Controls.Add(this.CorreccionYmas);
            this.Controls.Add(this.BtnAbrirCerrar);
            this.Controls.Add(this.Calibrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem ArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AbrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Calibrar;
        public System.IO.Ports.SerialPort PuertoSerie;
        private System.Windows.Forms.Button BtnAbrirCerrar;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemConfig;
        public System.Windows.Forms.ToolStripMenuItem ToolStripPuertoSerie;
        public System.Windows.Forms.ToolStripComboBox CboPuertoSerie;
        private System.Windows.Forms.Button CorreccionYmas;
        private System.Windows.Forms.Button CorreccionXmas;
        private System.Windows.Forms.Button CorreccionXmenos;
        private System.Windows.Forms.Button CorreccionYmenos;
        public System.Windows.Forms.MaskedTextBox MmCorreccion;
        private System.Windows.Forms.TextBox TxtRecibir;
        private System.Windows.Forms.TextBox TxtEscribir;
        private System.Windows.Forms.Button BtnEnviar;
        private System.Windows.Forms.Button CalibracionLista;
        private System.Windows.Forms.Button BtnComenzar;
        private System.Windows.Forms.Button VisualizarPuntos;

        public System.EventHandler configuracionToolStripMenuItem_Click { get; set; }
        
        public System.EventHandler toolStripMenuItem1_Click { get; set; }
    }
}

