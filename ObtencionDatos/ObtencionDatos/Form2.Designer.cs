namespace ObtencionDatos
{
    partial class Form2
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
            this.btnAbrirCerrar = new System.Windows.Forms.Button();
            this.cboPuertos = new System.Windows.Forms.ComboBox();
            this.txtEnviar = new System.Windows.Forms.RichTextBox();
            this.txtRecibir = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnAbrirCerrar
            // 
            this.btnAbrirCerrar.Location = new System.Drawing.Point(0, 0);
            this.btnAbrirCerrar.Name = "btnAbrirCerrar";
            this.btnAbrirCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnAbrirCerrar.TabIndex = 5;
            // 
            // cboPuertos
            // 
            /*
            this.cboPuertos.Location = new System.Drawing.Point(0, 0);
            this.cboPuertos.Name = "cboPuertos";
            this.cboPuertos.Size = new System.Drawing.Size(121, 21);
            this.cboPuertos.TabIndex = 4;
            this.cboPuertos.SelectedIndexChanged += new System.EventHandler(this.cboPuertos_SelectedIndexChanged);*/
            // 
            // txtEnviar
            // 
            this.txtEnviar.Location = new System.Drawing.Point(15, 74);
            this.txtEnviar.Name = "txtEnviar";
            this.txtEnviar.Size = new System.Drawing.Size(257, 69);
            this.txtEnviar.TabIndex = 2;
            this.txtEnviar.Text = "";
            // 
            // txtRecibir
            // 
            this.txtRecibir.Location = new System.Drawing.Point(13, 174);
            this.txtRecibir.Name = "txtRecibir";
            this.txtRecibir.Size = new System.Drawing.Size(258, 67);
            this.txtRecibir.TabIndex = 3;
            this.txtRecibir.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtRecibir);
            this.Controls.Add(this.txtEnviar);
            this.Controls.Add(this.cboPuertos);
            this.Controls.Add(this.btnAbrirCerrar);
            this.Name = "Form2";
            this.Text = "Formo2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbrirCerrar;
        private System.Windows.Forms.ComboBox cboPuertos;
        private System.Windows.Forms.RichTextBox txtEnviar;
        private System.Windows.Forms.RichTextBox txtRecibir;
    }
}