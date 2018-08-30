using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ObtencionDatos
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnAbrirCerrar_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen == false)
            {
                serialPort1.Open();
                btnAbrirCerrar.Text = "Cerrar";
            }
            else
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Close();
                btnAbrirCerrar.Text = "Abrir";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.PortName = cboPuertos.GetItemText(cboPuertos.SelectedItem);
        }

        private void cboPuertos_Click(object sender, EventArgs e) //handles cboPuertos.Click
        {
            
            string[] Portnames = SerialPort.GetPortNames();
            int i;
            
            cboPuertos.Items.Clear();

            if (Portnames.Length > 0)
            {
                for ( i = 0 ; i < Portnames.Length ; i++)
                {
                    try
                    {
                    serialPort1.PortName = Portnames[i];
                    serialPort1.Open();
                    serialPort1.Close();
                    cboPuertos.Items.Add(Portnames[i]);
                    }catch(Exception ex)
                    {
                    }
                }
            }
            else
            {
                MessageBox.Show("No se han detectado Puertos.");
                //MessageBox("No se han detectado Puertos.");
                //Me.Close();
            }
        }
    }
}
