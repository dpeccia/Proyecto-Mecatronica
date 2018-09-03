using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.IO.Ports;

namespace ObtencionDatos
{
    public partial class Form1 : Form
    {
        // Creamos una clase agujero, que en principio usamos como estructura
        public class Agujero
        {
            public int x, y;
            public String xy;
            public float mecha;
        }

        // Creamos una clase mecha, que en principio usamos como estructura
        public class Mecha
        {
            public String nombre;
            public float diametro;
        }

        // Creamos una clase offset, que contiene las correcciones en xy y angulo
        public class Offset
        {
            public int x, y;
            public float angulo;
        }
        
        string readBuffer;
        string pathArchivo;

        Offset offsetGeneral;
        Offset offsetReal;

        Agujero extremo = new Agujero();
        Agujero punto1 = new Agujero();
        Agujero punto2 = new Agujero();

        public Form1()
        {
            InitializeComponent();
            extremo.x = 500;
            extremo.y = 500;
        }
        public void convertir_string_a_xy(Agujero agujero){}
        public string convertir_xy_int_a_string(float x, float y)
        {
            string xy = "";
            int x_nuevo, y_nuevo;
            x_nuevo = (int) x * 10;
            y_nuevo = (int) y * 10;
            xy += "X";
            if (x_nuevo >= 0)
            {
                xy += "+";
            }
            else
            {
                xy += "-";
                x_nuevo *= -1;
            }

            if (x_nuevo > 9999999)
                xy += x_nuevo.ToString();
            else
            {
                if (x_nuevo > 999999)
                {
                    xy += "0"+x_nuevo.ToString();
                }
                else
                {
                    if (x_nuevo > 99999)
                    {
                        xy += "00" + x_nuevo.ToString();
                    }
                    else
                    {
                        if (x_nuevo > 9999)
                        {
                            xy += "000" + x_nuevo.ToString();
                        }
                        else
                        {
                            xy += "0000" + x_nuevo.ToString();
                        }
                    }
                }
            }
            xy += "Y";
            if (y_nuevo >= 0)
            {
                xy += "+";
            }
            else
            {
                xy += "-";
                y_nuevo *= -1;
            }
            if (y_nuevo > 9999999)
                xy += y_nuevo.ToString();
            else
            {
                if (y_nuevo > 999999)
                {
                    xy += "0" + y_nuevo.ToString();
                }
                else
                {
                    if (y_nuevo > 99999)
                    {
                        xy += "00" + y_nuevo.ToString();
                    }
                    else
                    {
                        if (y_nuevo > 9999)
                        {
                            xy += "000" + y_nuevo.ToString();
                        }
                        else
                        {
                            xy += "0000" + y_nuevo.ToString();
                        }
                    }
                }
            }
            return xy;
        }

        // Abrir archivo para leer
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pathArchivo = openFileDialog1.FileName;
            if (pathArchivo != "openFileDialog1")
            {
                label1.Text = pathArchivo;
                Leer_Archivo(pathArchivo);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un archivo", "Error de seleccion");
            }
        }

        // Metodo de lectura del archivo en cuestion
        public void Leer_Archivo(string path)
        {
            string textoArchivo = null;
            string[] lineasArchivo = null;
            ArrayList listaAgujeros = new ArrayList();
            ArrayList listaMechas = new ArrayList();
            int cantMechas = 0;
            int separador = 0;
            bool igualPorciento = false;
           
            textBox1.Text += pathArchivo + Environment.NewLine;         // Muestra el path en el TextBox

            try
            {
                System.IO.File.OpenRead(path);
                textoArchivo = System.IO.File.ReadAllText(path);
                lineasArchivo = System.IO.File.ReadAllLines(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("No se pudo abrir el archivo");
            }

            textBox1.Text += Environment.NewLine;
        
            for (int i = 0; i < lineasArchivo.Length; i++)       // Cuenta cantidad de mechas
            {
                if (lineasArchivo[i] == "%")
                {
                    igualPorciento = true;
                    separador = i;
                }
                if (igualPorciento && lineasArchivo[i].Contains("T"))
                {
                    textBox1.Text += lineasArchivo[i];
                    cantMechas++;
                }
            }

            textBox1.Text += Environment.NewLine + "Mechas:" + cantMechas + Environment.NewLine;

            for (int i = 0; i < cantMechas; i++)        // Escribo la lista de mechas 
            {
                if (lineasArchivo[i + 2].Contains("T"))
                {
                    Mecha mecha = new Mecha();
                    mecha.nombre = lineasArchivo[i + 2].Substring(0, 3);
                    mecha.diametro = float.Parse(lineasArchivo[i + 2].Substring(4, 6)) / 10000;     // Dividimos por 1000 porque se come el .0
                    listaMechas.Add(mecha);
                }
            }

            textBox1.Text += "Lista Ready";
            String mechaActual;
            Mecha auxMecha = new Mecha();
            float diametroMechaActual = 0;
            
            for (int i = separador; i < lineasArchivo.Length; i++)      // Recorre el archivo
            {
                if (lineasArchivo[i].Substring(0, 1) == "T")            // Busca la mecha actual
                {
                    mechaActual = lineasArchivo[i].Substring(0, 3);
                    foreach (Mecha mecha in listaMechas)        
                    {
                        if (mecha.nombre == mechaActual)
                        {
                            diametroMechaActual = mecha.diametro;       // Le asigna el diametro a la mecha actual
                        }
                    }
                }
                if (lineasArchivo[i].Substring(0, 1) == "X")            // Busca agujeros a realizar por esa mecha
                {
                    Agujero agujero = new Agujero();
                    agujero.x = Int32.Parse(lineasArchivo[i].Substring(1, lineasArchivo[i].IndexOf('Y', 1) - 1));
                    agujero.y = Int32.Parse(lineasArchivo[i].Substring(lineasArchivo[i].IndexOf('Y', 1) + 1, lineasArchivo[i].Length - (lineasArchivo[i].IndexOf('Y', 1) + 1)));
                    agujero.mecha = diametroMechaActual;
                    listaAgujeros.Add(agujero);
                }
            }
        }

        public Offset Calibracion()
        {
            Offset correccion = new Offset();   
            Offset correccionPunto1 = new Offset();
            Offset correccionPunto2 = new Offset();

            string punto1 = "";
            string punto2 = "";
            string caracterRecibido;

            EnviarAgujero(extremo); // Envia agujero de extremo para prueba de profundidad
            while (readBuffer != "E")      // Espera hasta recibir confirmacion de fin de secuencia
            { }

            Enviar(punto1); //Envia primer punto de referencia
            //caracterRecibido = Recibir();
            while (readBuffer != "*")
            {
            }

            Enviar(punto2); //Envia segundo punto de referencia
            //caracterRecibido = Recibir();
            while (readBuffer != "*")
            {
            }
            
            correccion = CalculosOffset();
            Corregir();

            return correccion;
        }

        
        
        // Envio de datos
        public void Enviar(string caracteres)
        {
            if (puertoSerie.IsOpen)
            {
                puertoSerie.Write(caracteres);
            }
        }

        public void actualizarTexto(object sender, EventArgs e)
        {
            txtRecibir.Text += txtRecibir;
        }

        // Recepcion de datos 
        public void puertoSerie_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if(puertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = puertoSerie.ReadExisting();
                    this.Invoke(new EventHandler(actualizarTexto));
                }
                catch(Exception ex)
                {
                }
            }
        }
        // Guarda caracteres en readBuffer
        public string Recibir()
        {
            if(puertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = puertoSerie.ReadExisting();
                    //this.Invoke(new EventHandler());
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error" + e.Message);
                }
                
            }
            return readBuffer;
        }

        public void Corregir()
        {
            
        }

        public Offset CalculosOffset()
        {
            return new Offset();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void puertoSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        // Envia posicion de agujero
        public void EnviarAgujero(Agujero agujero)
        {
            Enviar(convertir_xy_int_a_string(agujero.x, agujero.y));    // Envia posicion de cambio de mecha para bajar y hacer prueba de altura 
            while (readBuffer != "*")      // Espera hasta recibir confirmacion de la posicion
            { }
        }

        private void calibrar_Click(object sender, EventArgs e)
        {
            Enviar("*");
            Calibracion();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPuertoSerie.Items.Clear();

            if (!puertoSerie.IsOpen)
            {
                try
                {
                    cboPuertoSerie.Items.AddRange(SerialPort.GetPortNames());

                }
                catch { }
            }
            else
            {
                // Si el puerto esta abierto, no muestra la lista
            }
        }

        // TODO: Revisar la seleccion de puerto, ahora hay que seleccionar 2 veces para que lo tome bien
        private void btnAbrirCerrar_Click_1(object sender, EventArgs e)
        {
            if (puertoSerie.IsOpen)
            {
                puertoSerie.DiscardInBuffer();
                puertoSerie.Close();
                btnAbrirCerrar.Text = "Abrir Puerto";
            }
            else
            {
                try
                {
                    puertoSerie.PortName = cboPuertoSerie.SelectedItem.ToString();
                    puertoSerie.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("El puerto " + cboPuertoSerie.SelectedItem + " está ocupado");
                }
                catch (Exception ex)
                {
                }
                btnAbrirCerrar.Text = "Cerrar Puerto";
            }
        }
        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void mmCorreccion_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Enviar(txtEscribir.Text);
            txtEscribir.Text = "";
        }

        
    }
}
