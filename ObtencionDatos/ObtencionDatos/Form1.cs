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
        public static ArrayList listaAgujeros = new ArrayList();
        // Creamos una clase agujero, que en principio usamos como estructura
        public class Agujero
        {
            public float x, y;
            public String xy = "X+000000Y+000000";           
            public Mecha mecha;            
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
        bool archivoAbierto;

        //Offset offsetGeneral;
        Offset offsetReal = new Offset();

        Agujero extremo = new Agujero();
        Agujero agujeroAux = new Agujero();

        Agujero punto1 = new Agujero();
        Agujero punto2 = new Agujero();
        Agujero punto1real = new Agujero();
        Agujero punto2real = new Agujero();

        public ArrayList listaMechas = new ArrayList();

        //public void Convertir_string_a_xy(Agujero agujero){}

        public Form1()
        {
            InitializeComponent();
            extremo.x = 500;
            extremo.y = 500;
            MmCorreccion.Text = "00.0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string Convertir_xy_int_a_string(float x, float y)
        {
            string xy = "";
            int x_nuevo, y_nuevo;
            x_nuevo = (int)x * 10;
            y_nuevo = (int)y * 10;

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
            xy += x_nuevo.ToString().PadLeft(6, '0');

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
            xy += y_nuevo.ToString().PadLeft(6, '0');

            return xy;
        }

        public void PuntosExtremos()//OK
        {
            Mecha mechaAux1 = (Mecha)listaMechas[0];
            Mecha mechaAux2 = (Mecha)listaMechas[1];
            foreach (Agujero i in listaAgujeros)
            {
                if (i.mecha.nombre == mechaAux1.nombre)
                {
                    if (punto1.x > i.x)
                    {
                        punto1 = i;
                    }
                    else if (punto1.x == i.x && punto1.y < i.y)
                    {
                        punto1 = i;
                    }
                }
                if (i.mecha.nombre == mechaAux1.nombre || i.mecha.nombre == mechaAux2.nombre)
                {
                    if (punto2.y >= i.y)
                    {
                        punto2 = i;
                    }
                    else if (punto2.x == i.x && punto2.y < i.y)
                    {
                        punto2 = i;
                    }
                }
            }
            
            TextBox1.Text += "Puntos para calibración elegidos" + Environment.NewLine;
        }

        // Abrir archivo para leer
        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)//OK
        {
            OpenFileDialog1.ShowDialog();
            pathArchivo = OpenFileDialog1.FileName;
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
        // correccion a 0,0 para todos los puntos
        public void CorreccionPuntos()
        {
            Agujero agujeroAux = (Agujero) listaAgujeros[0];
            float minX = agujeroAux.x;
            float minY = agujeroAux.y;

            foreach (Agujero i in listaAgujeros)
            {
                if (i.x < minX)
                {
                    minX = i.x;
                }
                if(i.y < minY)
                {
                    minY = i.y;
                }                
            }
            foreach (Agujero i in listaAgujeros)
            {
                i.x -= minX;
                i.y -= minY;
                i.xy = Convertir_xy_int_a_string(i.x, i.y);
            }
        }
        // Metodo de lectura del archivo en cuestion
        public void Leer_Archivo(string path)//OK
        {
            string textoArchivo = null;
            string[] lineasArchivo = null;
            
            int cantMechas = 0;
            int separador = 0;
            bool igualPorciento = false;
           
            TextBox1.Text += pathArchivo + Environment.NewLine;         // Muestra el path en el TextBox

            try
            {
                System.IO.File.OpenRead(path);
                textoArchivo = System.IO.File.ReadAllText(path);
                lineasArchivo = System.IO.File.ReadAllLines(path);
                archivoAbierto = true;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("No se pudo abrir el archivo");
                return;
            }
            catch (ArgumentException e)
            {
                return;
            }
            
            TextBox1.Text += Environment.NewLine;

            for (int i = 0; i < lineasArchivo.Length; i++)       // Cuenta cantidad de mechas
            {
                if (lineasArchivo[i] == "%")
                {
                    igualPorciento = true;
                    separador = i;
                }
                if (igualPorciento && lineasArchivo[i].Contains("T"))
                {
                    TextBox1.Text += lineasArchivo[i];
                    cantMechas++;
                }
            }

            TextBox1.Text += Environment.NewLine + "Mechas:" + cantMechas + Environment.NewLine;

            for (int i = 0; i < cantMechas; i++)        // Escribo la lista de mechas 
            {
                if (lineasArchivo[i + 2].Contains("T"))
                {
                    Mecha mecha = new Mecha
                    {
                        nombre = lineasArchivo[i + 2].Substring(0, 3),
                        diametro = float.Parse(lineasArchivo[i + 2].Substring(4, 6))
                    };
                    listaMechas.Add(mecha);
                }
            }
            
            //String mechaActual;
            Mecha auxMecha = new Mecha();
            float diametroMechaActual = 0;

            for (int i = separador; i < lineasArchivo.Length; i++)      // Recorre el archivo
            {
                if (lineasArchivo[i].Substring(0, 1) == "T")            // Busca la mecha actual
                {
                    auxMecha.nombre = lineasArchivo[i].Substring(0, 3);
                    foreach (Mecha mecha in listaMechas)
                    {
                        if (mecha.nombre == auxMecha.nombre)
                        {
                            auxMecha.diametro = mecha.diametro;       // Le asigna el diametro a la mecha actual
                        }
                    }
                }
                if (lineasArchivo[i].Substring(0, 1) == "X")            // Busca agujeros a realizar por esa mecha
                {
                    Agujero agujero = new Agujero
                    {
                        x = Int32.Parse(lineasArchivo[i].Substring(1, lineasArchivo[i].IndexOf('Y', 1) - 1)),
                        y = Int32.Parse(lineasArchivo[i].Substring(lineasArchivo[i].IndexOf('Y', 1) + 1, lineasArchivo[i].Length - (lineasArchivo[i].IndexOf('Y', 1) + 1)))
                    };
                    agujero.mecha = new Mecha
                    {
                        diametro = auxMecha.diametro,
                        nombre = auxMecha.nombre
                    };
                    agujero.xy = Convertir_xy_int_a_string(agujero.x, agujero.y);
                    listaAgujeros.Add(agujero);
                }
            }
            CorreccionPuntos();
            TextBox1.Text += "Listas Terminadas" + Environment.NewLine;
            PuntosExtremos();
            TextBox1.Text += "Puntos para calibración guardados" + Environment.NewLine;
        }

        // Metodo para resetear agujeros a 0
        public void ResetAgujero(Agujero agujero)//OK
        {
            agujeroAux.x = 0;
            agujeroAux.y = 0;
            agujeroAux.xy = "X+000000Y+000000";
        }

        public void EnableButtons(bool estado)//OK
        {
            CorreccionXmas.Enabled = estado;
            CorreccionYmas.Enabled = estado;
            CorreccionXmenos.Enabled = estado;
            CorreccionYmenos.Enabled = estado;
        }

        // Metodo para secuencia completa de calibracion
        public void Calibracion()
        {
            Offset correccion = new Offset();   
            Offset correccionPunto1 = new Offset();
            Offset correccionPunto2 = new Offset();
            Agujero punto1 = new Agujero();
            Agujero punto2 = new Agujero();
            Agujero punto2real = new Agujero();
            Mecha mechaEnviar;
            string mecha;
            //enableButtons(true);
            MmCorreccion.Enabled = true;

            Calibrar.Enabled = false;

            // Calibración eje z
            //EnviarAgujero(extremo); // Envia agujero de extremo para prueba de profundidad
            while (readBuffer != "M")      // Espera hasta recibir confirmacion de fin de secuencia
            {
                Recibir();
            }
            TextBox1.Text += "Secuencia lista" + Environment.NewLine;

            mechaEnviar = (Mecha)listaMechas[0];    
            mecha = "M";
            mecha += Convert.ToString(mechaEnviar.diametro);
            while (mecha.Length < 6)
            {
                mecha += "0";
            }
            Enviar(mecha);  //Envia mecha para altura z

            EnableButtons(true);

            Enviar("A");    //Inicio ciclo ajuste
            // Calibación plano xy
            Enviar(punto1.xy); // Envia primer punto de referencia
            agujeroAux = punto1;    // Guarda punto en variable auxiliar
            while (readBuffer != "*")
            {
                Recibir();
            }
            TextBox1.Text += "Punto 1 Recibido"+Environment.NewLine;
            Enviar("A");    //Fin ciclo ajuste
            CalibracionLista.Enabled  = true;
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
            return;
        }
        
        // Envio de datos
        public void Enviar(string caracteres)//OK
        {
            if (PuertoSerie.IsOpen)
            {
                PuertoSerie.Write(caracteres);
            }
        }

        // Actualizacion del textbox de recepcion
        public void ActualizarTexto(object sender, EventArgs e)//OK
        {
            TxtRecibir.Text += readBuffer;
        }

        // Recepcion de datos
        public void PuertoSerie_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)//OK
        {
            if(PuertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = PuertoSerie.ReadExisting();
                    this.Invoke(new EventHandler(ActualizarTexto));
                }
                catch(Exception ex)
                {
                }
            }
        }

        // Guarda caracteres en readBuffer
        public string Recibir()//OK
        {
            if(PuertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = PuertoSerie.ReadExisting();
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
        
        private void PuertoSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        // Envia posicion de agujero
        public void EnviarAgujero(Agujero agujero)
        {
            Enviar(Convertir_xy_int_a_string(agujero.x, agujero.y));    // Envia posicion de cambio de mecha para bajar y hacer prueba de altura 
            /*while (readBuffer != "*")      // Espera hasta recibir confirmacion de la posicion
            {
                Recibir();
            }*/
            //resetReadBuffer();
        }

        private void Calibrar_Click(object sender, EventArgs e)//OK
        {
            Enviar("*");
            Calibracion();
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)//OK
        {
            CboPuertoSerie.Items.Clear();

            if (!PuertoSerie.IsOpen)
            {
                try
                {
                    CboPuertoSerie.Items.AddRange(SerialPort.GetPortNames());
                }
                catch { }
            }
            else
            {
                // Si el puerto esta abierto, no muestra la lista
            }
        }

        private void BtnAbrirCerrar_Click_1(object sender, EventArgs e)//OK
        {
            if (PuertoSerie.IsOpen)
            {
                PuertoSerie.DiscardInBuffer();
                PuertoSerie.Close();
                BtnAbrirCerrar.Text = "Abrir Puerto";
                Calibrar.Enabled = false;
            }
            else
            {
                try
                {
                    PuertoSerie.PortName = CboPuertoSerie.SelectedItem.ToString();
                    PuertoSerie.Open();
                    Calibrar.Enabled = true;
                    BtnAbrirCerrar.Text = "Cerrar Puerto";
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("El puerto " + CboPuertoSerie.SelectedItem + " está ocupado");
                }
                catch (Exception ex)
                {

                }                
            }
        }
        
        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void MmCorreccion_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void BtnEnviar_Click(object sender, EventArgs e)//OK
        {
            Enviar(TxtEscribir.Text);
            TxtEscribir.Text = "";
        }

        private void PuertoSerie_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            if (PuertoSerie.IsOpen)
            {
                try
                {
                    readBuffer = PuertoSerie.ReadExisting();
                    this.Invoke(new EventHandler(ActualizarTexto));
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void CalibracionLista_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                    offsetReal.x = agujeroAux.x + punto1.x; //Revisar
                    offsetReal.y = agujeroAux.y + punto1.y; //Revisar
                    punto1 = agujeroAux;    //Guarda variable auxiliar con corrección en punto de referencia
                    punto2.x += offsetReal.x;
                    punto2.y += offsetReal.y;
                    
                    CalibracionLista.Text = "Finalizar";
                    
                    Enviar(punto2.xy); //Envia segundo punto de referencia
                    agujeroAux = punto2; // Guarda punto en variable auxiliar

                    while (readBuffer != "*")
                    {
                        Recibir();
                    }
                    TextBox1.Text += "Agujero recibido"+Environment.NewLine;
                    state = 1;

                    break;
                case 1:
                    CalculosOffset(punto1, punto2, punto2real);
                    punto2 = agujeroAux; // Guarda variable auxiliar con corrección en punto de referencia
                    
                    EnableButtons(false);
                    MmCorreccion.Enabled = false;
                    Calibrar.Enabled = true;
                    state = 0;
                    CalibracionLista.Enabled = false;

                    break;
            }
        }

        private void Ciclo_Agujereado()
        {
            int i = 1;
            Mecha mechaAnterior = (Mecha) listaMechas[0];
            Mecha mechaActual = (Mecha) listaMechas[0];
            Enviar("S");
            foreach (Agujero agujero in listaAgujeros)
            {
                mechaActual.nombre = agujero.mecha.nombre;
                if (mechaActual.nombre != mechaAnterior.nombre)
                {
                    Enviar(mechaActual.diametro.ToString());
                }
                Enviar("P");
                EnviarAgujero(agujero);
                while (readBuffer != "*")
                {
                    Recibir();                        
                }
                TextBox1.Text += "AgujeroListo" + i + Environment.NewLine;
                if (i == listaAgujeros.Capacity)
                {
                    Enviar("F");
                }                
            }
        }

        private void CorreccionYmas_Click(object sender, EventArgs e)//OK Sujeto a cambios
        {
            agujeroAux.y += float.Parse(MmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }
        private void CorreccionXmas_Click(object sender, EventArgs e)//OK Sujeto a cambios
        {
            agujeroAux.x += float.Parse(MmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }
        private void CorreccionXmenos_Click(object sender, EventArgs e)//OK Sujeto a cambios
        {
            agujeroAux.y -= float.Parse(MmCorreccion.Text, System.Globalization.CultureInfo.InvariantCulture);
            EnviarAgujero(agujeroAux);
        }
        private void CorreccionYmenos_Click(object sender, EventArgs e)//OK Sujeto a cambios
        {
            agujeroAux.x -= float.Parse(MmCorreccion.Text, System.Globalization.CultureInfo.CurrentCulture);
            EnviarAgujero(agujeroAux);
        }
        private void BtnComenzar_Click(object sender, EventArgs e)//OK
        {
            Ciclo_Agujereado();
        }
        private void VisualizarPuntos_Click(object sender, EventArgs e)//Revisar
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
//TODO: Envio de puntos para calibracion
//TODO: Envio de puntos para agujereado
//TODO: Calibracion
