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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPuertoSerie = new System.Windows.Forms.ToolStripMenuItem();
            this.cboPuertoSerie = new System.Windows.Forms.ToolStripComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.calibrar = new System.Windows.Forms.Button();
            this.puertoSerie = new System.IO.Ports.SerialPort(this.components);
            this.btnAbrirCerrar = new System.Windows.Forms.Button();
            this.correccionYmas = new System.Windows.Forms.Button();
            this.correccionXmas = new System.Windows.Forms.Button();
            this.correccionXmenos = new System.Windows.Forms.Button();
            this.correccionYmenos = new System.Windows.Forms.Button();
            this.mmCorreccion = new System.Windows.Forms.MaskedTextBox();
            this.txtRecibir = new System.Windows.Forms.TextBox();
            this.txtEscribir = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.calibracionLista = new System.Windows.Forms.Button();
            this.btnComenzar = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 94);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(445, 149);
            this.textBox1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.toolStripMenuItemConfig});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(590, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // toolStripMenuItemConfig
            // 
            this.toolStripMenuItemConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPuertoSerie});
            this.toolStripMenuItemConfig.Name = "toolStripMenuItemConfig";
            this.toolStripMenuItemConfig.Size = new System.Drawing.Size(95, 20);
            this.toolStripMenuItemConfig.Text = "Configuración";
            // 
            // toolStripPuertoSerie
            // 
            this.toolStripPuertoSerie.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboPuertoSerie});
            this.toolStripPuertoSerie.Name = "toolStripPuertoSerie";
            this.toolStripPuertoSerie.Size = new System.Drawing.Size(137, 22);
            this.toolStripPuertoSerie.Text = "Puerto Serie";
            // 
            // cboPuertoSerie
            // 
            this.cboPuertoSerie.Name = "cboPuertoSerie";
            this.cboPuertoSerie.Size = new System.Drawing.Size(121, 23);
            this.cboPuertoSerie.Click += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
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
            // calibrar
            // 
            this.calibrar.Enabled = false;
            this.calibrar.Location = new System.Drawing.Point(479, 73);
            this.calibrar.Name = "calibrar";
            this.calibrar.Size = new System.Drawing.Size(99, 29);
            this.calibrar.TabIndex = 3;
            this.calibrar.Text = "Calibración";
            this.calibrar.UseVisualStyleBackColor = true;
            this.calibrar.Click += new System.EventHandler(this.calibrar_Click);
            // 
            // puertoSerie
            // 
            this.puertoSerie.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.puertoSerie_DataReceived_1);
            // 
            // btnAbrirCerrar
            // 
            this.btnAbrirCerrar.Location = new System.Drawing.Point(479, 34);
            this.btnAbrirCerrar.Name = "btnAbrirCerrar";
            this.btnAbrirCerrar.Size = new System.Drawing.Size(102, 23);
            this.btnAbrirCerrar.TabIndex = 0;
            this.btnAbrirCerrar.Text = "Abrir Puerto";
            this.btnAbrirCerrar.Click += new System.EventHandler(this.btnAbrirCerrar_Click_1);
            // 
            // correccionYmas
            // 
            this.correccionYmas.Enabled = false;
            this.correccionYmas.Location = new System.Drawing.Point(513, 114);
            this.correccionYmas.Name = "correccionYmas";
            this.correccionYmas.Size = new System.Drawing.Size(34, 34);
            this.correccionYmas.TabIndex = 4;
            this.correccionYmas.UseVisualStyleBackColor = true;
            this.correccionYmas.Click += new System.EventHandler(this.correccionYmas_Click);
            // 
            // correccionXmas
            // 
            this.correccionXmas.Enabled = false;
            this.correccionXmas.Location = new System.Drawing.Point(548, 146);
            this.correccionXmas.Name = "correccionXmas";
            this.correccionXmas.Size = new System.Drawing.Size(34, 34);
            this.correccionXmas.TabIndex = 5;
            this.correccionXmas.UseVisualStyleBackColor = true;
            this.correccionXmas.Click += new System.EventHandler(this.correccionXmenos_Click);
            // 
            // correccionXmenos
            // 
            this.correccionXmenos.Enabled = false;
            this.correccionXmenos.Location = new System.Drawing.Point(478, 146);
            this.correccionXmenos.Name = "correccionXmenos";
            this.correccionXmenos.Size = new System.Drawing.Size(34, 34);
            this.correccionXmenos.TabIndex = 13;
            this.correccionXmenos.Click += new System.EventHandler(this.correccionXmenos_Click);
            // 
            // correccionYmenos
            // 
            this.correccionYmenos.Enabled = false;
            this.correccionYmenos.Location = new System.Drawing.Point(513, 180);
            this.correccionYmenos.Name = "correccionYmenos";
            this.correccionYmenos.Size = new System.Drawing.Size(34, 34);
            this.correccionYmenos.TabIndex = 7;
            this.correccionYmenos.UseVisualStyleBackColor = true;
            this.correccionYmenos.Click += new System.EventHandler(this.correccionYmenos_Click);
            // 
            // mmCorreccion
            // 
            this.mmCorreccion.Culture = new System.Globalization.CultureInfo("en-US");
            this.mmCorreccion.Enabled = false;
            this.mmCorreccion.Location = new System.Drawing.Point(515, 154);
            this.mmCorreccion.Mask = "99.9";
            this.mmCorreccion.Name = "mmCorreccion";
            this.mmCorreccion.Size = new System.Drawing.Size(29, 20);
            this.mmCorreccion.TabIndex = 12;
            // 
            // txtRecibir
            // 
            this.txtRecibir.Location = new System.Drawing.Point(12, 68);
            this.txtRecibir.Name = "txtRecibir";
            this.txtRecibir.Size = new System.Drawing.Size(100, 20);
            this.txtRecibir.TabIndex = 9;
            // 
            // txtEscribir
            // 
            this.txtEscribir.Location = new System.Drawing.Point(175, 68);
            this.txtEscribir.Name = "txtEscribir";
            this.txtEscribir.Size = new System.Drawing.Size(100, 20);
            this.txtEscribir.TabIndex = 10;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(281, 66);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 11;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // calibracionLista
            // 
            this.calibracionLista.Enabled = false;
            this.calibracionLista.Location = new System.Drawing.Point(494, 220);
            this.calibracionLista.Name = "calibracionLista";
            this.calibracionLista.Size = new System.Drawing.Size(75, 23);
            this.calibracionLista.TabIndex = 14;
            this.calibracionLista.Text = "Siguiente";
            this.calibracionLista.UseVisualStyleBackColor = true;
            this.calibracionLista.Click += new System.EventHandler(this.calibracionLista_Click);
            // 
            // btnComenzar
            // 
            this.btnComenzar.Location = new System.Drawing.Point(382, 65);
            this.btnComenzar.Name = "btnComenzar";
            this.btnComenzar.Size = new System.Drawing.Size(75, 23);
            this.btnComenzar.TabIndex = 15;
            this.btnComenzar.Text = "Comenzar";
            this.btnComenzar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 255);
            this.Controls.Add(this.btnComenzar);
            this.Controls.Add(this.calibracionLista);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtEscribir);
            this.Controls.Add(this.txtRecibir);
            this.Controls.Add(this.mmCorreccion);
            this.Controls.Add(this.correccionYmenos);
            this.Controls.Add(this.correccionXmenos);
            this.Controls.Add(this.correccionXmas);
            this.Controls.Add(this.correccionYmas);
            this.Controls.Add(this.btnAbrirCerrar);
            this.Controls.Add(this.calibrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
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

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button calibrar;
        public System.IO.Ports.SerialPort puertoSerie;
        private System.Windows.Forms.Button btnAbrirCerrar;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConfig;
        public System.Windows.Forms.ToolStripMenuItem toolStripPuertoSerie;
        public System.Windows.Forms.ToolStripComboBox cboPuertoSerie;
        private System.Windows.Forms.Button correccionYmas;
        private System.Windows.Forms.Button correccionXmas;
        private System.Windows.Forms.Button correccionXmenos;
        private System.Windows.Forms.Button correccionYmenos;
        public System.Windows.Forms.MaskedTextBox mmCorreccion;
        private System.Windows.Forms.TextBox txtRecibir;
        private System.Windows.Forms.TextBox txtEscribir;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button calibracionLista;
        private System.Windows.Forms.Button btnComenzar;

        public System.EventHandler configuracionToolStripMenuItem_Click { get; set; }
        
        public System.EventHandler toolStripMenuItem1_Click { get; set; }
    }
}

