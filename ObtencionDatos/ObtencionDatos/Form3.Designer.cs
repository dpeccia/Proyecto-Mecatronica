namespace ObtencionDatos
{
    partial class Form3
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
            this.enviarDato = new System.Windows.Forms.TextBox();
            this.recepcionDatos = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enviarDato
            // 
            this.enviarDato.Location = new System.Drawing.Point(12, 12);
            this.enviarDato.Name = "enviarDato";
            this.enviarDato.Size = new System.Drawing.Size(121, 20);
            this.enviarDato.TabIndex = 0;
            // 
            // recepcionDatos
            // 
            this.recepcionDatos.Location = new System.Drawing.Point(154, 12);
            this.recepcionDatos.Name = "recepcionDatos";
            this.recepcionDatos.Size = new System.Drawing.Size(121, 20);
            this.recepcionDatos.TabIndex = 2;
            this.recepcionDatos.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(263, 26);
            this.button1.TabIndex = 3;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.EnvioBtn);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 87);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.recepcionDatos);
            this.Controls.Add(this.enviarDato);
            this.Name = "Form3";
            this.Text = "Modo Manual";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox enviarDato;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox recepcionDatos;
    }
}