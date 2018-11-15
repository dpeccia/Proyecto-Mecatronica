using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObtencionDatos
{
    public partial class Form3 : Form
    {
        System.IO.Ports.SerialPort PuertoSerieManual = new System.IO.Ports.SerialPort();
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(String PortName)
        {
            CheckForIllegalCrossThreadCalls = false;
            
            PuertoSerieManual.PortName = PortName;
            PuertoSerieManual.BaudRate = 9600;

            PuertoSerieManual.Open();
            PuertoSerieManual.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recepcion);
            InitializeComponent();
        }

        // Envio de datos
        public void Envio(string caracteres)
        {
            if (PuertoSerieManual.IsOpen)
            {
                PuertoSerieManual.Write(caracteres);
            }
            else
            {
                MessageBox.Show("Abrir el puerto para mandar el dato \" " + caracteres + "\"");
            }
        }

        public void Recepcion(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            recepcionDatos.Text = PuertoSerieManual.ReadExisting();
        }

        private void EnvioBtn(object sender, EventArgs e)
        {
            Envio(enviarDato.Text);            
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            PuertoSerieManual.Close();
        }

    }
}
