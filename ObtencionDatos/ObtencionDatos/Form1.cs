using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.IO.Ports;

namespace ObtencionDatos
{
    public partial class Form1 : Form
    {
        // Creamos una clase agujero, que en principio usamos como estructura
        public class Agujero
        {
            public float x, y;
            public String xy = "X+000000Y+000000";           
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
            public float x, y;
            public float angulo;
        }
        
        string readBuffer;
        string pathArchivo;
        int state;

        //Offset offsetGeneral;
        Offset offsetReal = new Offset();

        Agujero extremo = new Agujero();
        Agujero agujeroAux = new Agujero();

        Agujero punto1 = new Agujero();
        Agujero punto2 = new Agujero();
        Agujero punto1real = new Agujero();
        Agujero punto2real = new Agujero();

        
        public void convertir_string_a_xy(Agujero agujero){}

        public Form1()
        {
            InitializeComponent();
            extremo.x = 500;
            extremo.y = 500;
            mmCorreccion.Text = "00.0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
            /// MODIFICACION
            xy = x_nuevo.ToString().PadLeft(6, '0');
            /*
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
            }*/
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
            /// MODIFICACION
            xy += y_nuevo.ToString().PadLeft(6, '0');
            /*
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
            }*/
            return xy;
        }
        // Enviar todos los agujeros
        

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

        // Metodo para resetear agujeros a 0
        public void resetAgujero(Agujero agujero)
        {
            agujeroAux.x = 0;
            agujeroAux.y = 0;
            agujeroAux.xy = "X+000000Y+000000";
        }
        public void enableButtons(bool estado){
            correccionXmas.Enabled = estado;
            correccionYmas.Enabled = estado;
            correccionXmenos.Enabled = estado;
            correccionYmenos.Enabled = estado;
        }

        // Metodo para secuencia completa de calibracion
        public Offset Calibracion()
        {
            Offset correccion = new Offset();   
            Offset correccionPunto1 = new Offset();
            Offset correccionPunto2 = new Offset();
            Agujero punto1 = new Agujero();
            Agujero punto2 = new Agujero();
            Agujero punto2real = new Agujero();

            enableButtons(true);
            mmCorreccion.Enabled = true;

            calibrar.Enabled = false;

            // Calibración eje z
            EnviarAgujero(extremo); // Envia agujero de extremo para prueba de profundidad
            while (readBuffer != "E")      // Espera hasta recibir confirmacion de fin de secuencia
            {
                Recibir();
            }

            // Calibación plano xy
            Enviar(punto1.xy); // Envia primer punto de referencia
            agujeroAux = punto1;    // Guarda punto en variable auxiliar
            while (readBuffer != "*")
            {
                Recibir();
            }

            calibracionLista.Enabled  = true;
            /*
            while (!ready) // Si se hace click en siguiente, continúa
            {
                Recibir();
            }*/
            /*
            offsetReal.x = agujeroAux.x + punto1.x; //Revisar
            offsetReal.y = agujeroAux.y + punto1.y; //Revisar
            punto1 = agujeroAux;    //Guarda variable auxiliar con corrección en punto de referencia
            punto2.x += offsetReal.x;
            punto2.y += offsetReal.y;

            calibracionLista.Text = "Finalizar";
            
            Enviar(punto2.xy); //Envia segundo punto de referencia
            agujeroAux = punto2; // Guarda punto en variable auxiliar

            while (readBuffer != "*")
            {
                Recibir();
            }*/
            /*ready = false;
            CalculosOffset(punto1, punto2, punto2real);
            punto2 = agujeroAux; // Guarda variable auxiliar con corrección en punto de referencia
          
            enableButtons(false);
            mmCorreccion.Enabled = false;
            calibrar.Enabled = true;
            */
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

        // Actualizacion del textbox de recepcion
        public void actualizarTexto(object sender, EventArgs e)
        {
            txtRecibir.Text += readBuffer;
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

        public void CalculosOffset(Agujero punto1, Agujero punto2, Agujero punto2real)
        {
            float alpha, beta, gamma;
            Agujero puntoACorregir = new Agujero();
            Agujero puntoAux = new Agujero();

            alpha = (float) Math.Atan((punto1.y - punto2.y) / (punto1.x - punto2.x));
            beta = (float) Math.Atan((punto1.y - punto2real.y) / (punto1.x - punto2real.x));
            gamma = beta - alpha;

            offsetReal.angulo = gamma;

            /***Esto hay que hacerlo en otro metodo para todos los puntos a corregir***/
            puntoAux.x = puntoACorregir.x - punto1.x;
            puntoAux.y = puntoACorregir.y - punto1.y;

            puntoAux.x = (float) (puntoAux.x * Math.Cos(gamma) - puntoAux.y * Math.Sin(gamma));
            puntoAux.y = (float) (puntoAux.y * Math.Sin(gamma) + puntoAux.y * Math.Cos(gamma));

            puntoAux.x = puntoACorregir.x + punto1.x;
            puntoAux.y = puntoACorregir.y + punto1.y;
            /**********************************************************/

        }
        
        private void puertoSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        // Envia posicion de agujero
        public void EnviarAgujero(Agujero agujero)
        {
            Enviar(convertir_xy_int_a_string(agujero.x, agujero.y));    // Envia posicion de cambio de mecha para bajar y hacer prueba de altura 
            while (readBuffer != "*")      // Espera hasta recibir confirmacion de la posicion
            {
                Recibir();
            }
            //resetReadBuffer();
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
                calibrar.Enabled = false;
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

                calibrar.Enabled = true;
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

        private void puertoSerie_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            if (puertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = puertoSerie.ReadExisting();
                    this.Invoke(new EventHandler(actualizarTexto));
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void calibracionLista_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                    offsetReal.x = agujeroAux.x + punto1.x; //Revisar
                    offsetReal.y = agujeroAux.y + punto1.y; //Revisar
                    punto1 = agujeroAux;    //Guarda variable auxiliar con corrección en punto de referencia
                    punto2.x += offsetReal.x;
                    punto2.y += offsetReal.y;
                    
                    calibracionLista.Text = "Finalizar";
                    
                    Enviar(punto2.xy); //Envia segundo punto de referencia
                    agujeroAux = punto2; // Guarda punto en variable auxiliar

                    while (readBuffer != "*")
                    {
                        Recibir();
                    }
                    state = 1;

                    break;
                case 1:
                    CalculosOffset(punto1, punto2, punto2real);
                    punto2 = agujeroAux; // Guarda variable auxiliar con corrección en punto de referencia
                    
                    enableButtons(false);
                    mmCorreccion.Enabled = false;
                    calibrar.Enabled = true;
                    state = 0;
                    calibracionLista.Enabled = false;

                    break;
            }
        }

        private void correccionYmas_Click(object sender, EventArgs e)
        {
            agujeroAux.y += float.Parse(mmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }

        private void correccionXmas_Click(object sender, EventArgs e)
        {
            agujeroAux.x += float.Parse(mmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }

        private void correccionXmenos_Click(object sender, EventArgs e)
        {
            agujeroAux.y -= float.Parse(mmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }

        private void correccionYmenos_Click(object sender, EventArgs e)
        {
            agujeroAux.x -= float.Parse(mmCorreccion.Text, System.Globalization.CultureInfo.CurrentCulture);
            EnviarAgujero(agujeroAux);
        }

    }
}