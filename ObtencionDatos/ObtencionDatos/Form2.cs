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
            string[] listaPuertos = SerialPort.GetPortNames();
            cboPuertos.Items.Clear();
            cboPuertos.Items.AddRange(listaPuertos);
        }
/*
        private void btnAbrirCerrar_Click(object sender, EventArgs e)
        {
            if (puertoSerie.IsOpen)
            {
                puertoSerie.DiscardInBuffer();
                puertoSerie.Close();
                btnAbrirCerrar.Text = "Abrir";
            }
            else
            {
                try
                {
                    puertoSerie.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("el Puerto " + cboPuertos.SelectedItem + " está ocupado");
                }
                btnAbrirCerrar.Text = "Cerrar";
            }
        }
        */
       /* 
        private void cboPuertos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (puertoSerie.IsOpen == false)
                puertoSerie.PortName = cboPuertos.GetItemText(cboPuertos.SelectedItem);
        }*/
        
        /*
        private void cboPuertos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Portnames = SerialPort.GetPortNames();
            int i;
            
            cboPuertos.Items.Clear();

            if (Portnames.Length > 0)
            {
                for (i = 0; i < Portnames.Length; i++)
                {
                    try
                    {
                        SerialPort.GetPortNames() = Portnames[i];
                        serialPort1.PortName = Portnames[i];
                        serialPort1.Open();
                        cboPuertos.Items.Add(Portnames[i]);
                    }
                    catch (Exception ex)
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
            if (serialPort1.IsOpen == false)
            {
                serialPort1.PortName = cboPuertos.GetItemText(cboPuertos.SelectedItem);
            }
        }

        
        /*
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
        */

        /*
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (serialPort1.IsOpen == false)
                serialPort1.PortName = cboPuertos.GetItemText(cboPuertos.SelectedItem);
        }
        */
        /*
        private void cboPuertos_Click(object sender, EventArgs e)
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
        */
    }
}
