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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CuadroTexto = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.ArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AbrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esquinasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripPuertoSerie = new System.Windows.Forms.ToolStripMenuItem();
            this.CboPuertoSerie = new System.Windows.Forms.ToolStripComboBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ComenzarBtn = new System.Windows.Forms.Button();
            this.PuertoSerie = new System.IO.Ports.SerialPort(this.components);
            this.BtnAbrirCerrar = new System.Windows.Forms.Button();
            this.VisualizarPuntos = new System.Windows.Forms.Button();
            this.inst1 = new System.Windows.Forms.Label();
            this.inst2 = new System.Windows.Forms.Label();
            this.inst3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CuadroTexto
            // 
            this.CuadroTexto.Location = new System.Drawing.Point(12, 114);
            this.CuadroTexto.Multiline = true;
            this.CuadroTexto.Name = "CuadroTexto";
            this.CuadroTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CuadroTexto.Size = new System.Drawing.Size(445, 175);
            this.CuadroTexto.TabIndex = 0;
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
            this.AbrirToolStripMenuItem,
            this.esquinasToolStripMenuItem});
            this.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem";
            this.ArchivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ArchivoToolStripMenuItem.Text = "Archivo";
            // 
            // AbrirToolStripMenuItem
            // 
            this.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem";
            this.AbrirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.AbrirToolStripMenuItem.Text = "Abrir";
            this.AbrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // esquinasToolStripMenuItem
            // 
            this.esquinasToolStripMenuItem.Name = "esquinasToolStripMenuItem";
            this.esquinasToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.esquinasToolStripMenuItem.Text = "Esquinas";
            this.esquinasToolStripMenuItem.Click += new System.EventHandler(this.esquinasToolStripMenuItem_Click);
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
            // ComenzarBtn
            // 
            this.ComenzarBtn.Enabled = false;
            this.ComenzarBtn.Location = new System.Drawing.Point(479, 73);
            this.ComenzarBtn.Name = "ComenzarBtn";
            this.ComenzarBtn.Size = new System.Drawing.Size(99, 29);
            this.ComenzarBtn.TabIndex = 3;
            this.ComenzarBtn.Text = "Comenzar";
            this.ComenzarBtn.UseVisualStyleBackColor = true;
            this.ComenzarBtn.Click += new System.EventHandler(this.Comenzar_Click);
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
            // VisualizarPuntos
            // 
            this.VisualizarPuntos.Enabled = false;
            this.VisualizarPuntos.Location = new System.Drawing.Point(494, 249);
            this.VisualizarPuntos.Name = "VisualizarPuntos";
            this.VisualizarPuntos.Size = new System.Drawing.Size(75, 40);
            this.VisualizarPuntos.TabIndex = 16;
            this.VisualizarPuntos.Text = "Visualizar puntos";
            this.VisualizarPuntos.UseVisualStyleBackColor = true;
            this.VisualizarPuntos.Click += new System.EventHandler(this.VisualizarPuntos_Click);
            // 
            // inst1
            // 
            this.inst1.AutoSize = true;
            this.inst1.Location = new System.Drawing.Point(229, 24);
            this.inst1.Name = "inst1";
            this.inst1.Size = new System.Drawing.Size(156, 13);
            this.inst1.TabIndex = 17;
            this.inst1.Text = "2. Abrir un archivo de Drill .DRL";
            // 
            // inst2
            // 
            this.inst2.AutoSize = true;
            this.inst2.Location = new System.Drawing.Point(229, 9);
            this.inst2.Name = "inst2";
            this.inst2.Size = new System.Drawing.Size(211, 13);
            this.inst2.TabIndex = 18;
            this.inst2.Text = "1. Abrir un archivo de esquinas Mechanical";
            // 
            // inst3
            // 
            this.inst3.AutoSize = true;
            this.inst3.Location = new System.Drawing.Point(229, 40);
            this.inst3.Name = "inst3";
            this.inst3.Size = new System.Drawing.Size(142, 13);
            this.inst3.TabIndex = 19;
            this.inst3.Text = "3. Abrir el puerto y Comenzar";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(494, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 58);
            this.button1.TabIndex = 20;
            this.button1.Text = "Envío manual de datos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 301);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inst3);
            this.Controls.Add(this.inst2);
            this.Controls.Add(this.inst1);
            this.Controls.Add(this.VisualizarPuntos);
            this.Controls.Add(this.BtnAbrirCerrar);
            this.Controls.Add(this.ComenzarBtn);
            this.Controls.Add(this.CuadroTexto);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Form1";
            this.Text = "Driller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem AbrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        public System.IO.Ports.SerialPort PuertoSerie;
        private System.Windows.Forms.Button BtnAbrirCerrar;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemConfig;
        public System.Windows.Forms.ToolStripMenuItem ToolStripPuertoSerie;
        public System.Windows.Forms.ToolStripComboBox CboPuertoSerie;
        private System.Windows.Forms.Button VisualizarPuntos;
        public System.Windows.Forms.Button ComenzarBtn;
        public System.Windows.Forms.TextBox CuadroTexto;
        private System.Windows.Forms.ToolStripMenuItem esquinasToolStripMenuItem;
        private System.Windows.Forms.Label inst1;
        private System.Windows.Forms.Label inst2;
        private System.Windows.Forms.Label inst3;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.ToolStripMenuItem ArchivoToolStripMenuItem;

        public System.EventHandler configuracionToolStripMenuItem_Click { get; set; }
        
        public System.EventHandler toolStripMenuItem1_Click { get; set; }
    }
}

