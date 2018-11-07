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
            public String xStr, yStr;
        }

        // Creamos una clase mecha, que en principio usamos como estructura
        //EDIT: todavia seguimos usandola como estructura
        public class Mecha
        {
            public String nombre;
            public float diametro;
            public String diametroStr;
            public Mecha(){}
            public Mecha(String nombre, float diametro, String diametroStr)
            {
                this.nombre = nombre;
                this.diametro = diametro;
                this.diametroStr = diametroStr;
            }
        }
        // Creamos una clase offset, que contiene las correcciones en xy y angulo
        public class Offset
        {
            public float x, y;
            public float angulo;
        }

        
        string pathArchivo;
        int numEsquina = 0;
        bool primerAjuste = true;
        public const int CORRECCIONMECHA = 10000;
        int indexAgujero = 0;
        // - Se usa para el método Recibir(), para ir recorriendo toda la listaAgujeros e ir enviandolos para perforación

        string estadoMecha;
        bool fin = false;

        int indexMecha = -1;
        // - Se usa para el método Recibir(), para ir posicionando la mecha según se vaya necesitando

        //Offset offsetGeneral;
        Offset offsetReal = new Offset();

        Agujero extremo = new Agujero();
        Agujero agujeroAux = new Agujero();

        Agujero punto1 = new Agujero();
        Agujero punto2 = new Agujero();
        Agujero punto1real = new Agujero();
        Agujero punto2real = new Agujero();

        public String[] esquinas = new String[4];

        public ArrayList listaMechas = new ArrayList();

        //public void Convertir_string_a_xy(Agujero agujero){}

        public Form1()
        {
            InitializeComponent();
            extremo.x = 500;
            extremo.y = 500;
            MmCorreccion.Text = "00.0";
        }

        // Abrir archivo para leer
        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)//OK
        {
            OpenFileDialog1.ShowDialog();
            pathArchivo = OpenFileDialog1.FileName;
            //if (pathArchivo != "openFileDialog1")
            if(!String.IsNullOrEmpty(pathArchivo) && File.Exists(pathArchivo))
            {
                CuadroTexto.Text += "Archivo de mechas cargado:" + Environment.NewLine + pathArchivo + Environment.NewLine + Environment.NewLine;
                Leer_Archivo(pathArchivo);
                AbrirToolStripMenuItem.Enabled = false;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un archivo", "Error de seleccion");
            }
        }

        public void PuntosExtremos()
        {
            Mecha mechaAux1 = (Mecha)listaMechas[0];
            Mecha mechaAux2 = (Mecha)listaMechas[1];
            foreach (Agujero agujero in listaAgujeros)
            {
                if (agujero.mecha.nombre == mechaAux1.nombre)
                {
                    if (punto1.x < agujero.x || (punto1.x == agujero.x && punto1.y < agujero.y))
                    {
                        punto1 = agujero;
                    }
                }
                if (agujero.mecha.nombre == mechaAux1.nombre || agujero.mecha.nombre == mechaAux2.nombre)
                {
                    if (punto2.y < agujero.y)
                    {
                        punto2 = agujero;
                    }
                    else if (punto2.x == agujero.x && punto2.y < agujero.y)
                    {
                        punto2 = agujero;
                    }
                }
            }
            CuadroTexto.Text += "Puntos para calibración elegidos" + Environment.NewLine;
        }

        // Metodo de lectura del archivo en cuestion
        public void Leer_Archivo(string path)
        {
            string[] lineasArchivo = null;

            try
            {
                System.IO.File.OpenRead(path);
                lineasArchivo = System.IO.File.ReadAllLines(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("No se pudo abrir el archivo");
                return;
            }
            catch (ArgumentException e)
            {
                return; // No se seleccionó un archivo
            }
            
            /// Escritura de la lista de mechas
            int indexArchivo = 0;
            while(!lineasArchivo[indexArchivo].Contains("%"))
            {
                if (lineasArchivo[indexArchivo].Substring(0,1) == "T")
                {
                    listaMechas.Add(new Mecha(lineasArchivo[indexArchivo].Substring(0,3), 
                                    float.Parse(lineasArchivo[indexArchivo].Substring(4,6))/CORRECCIONMECHA, 
                                    lineasArchivo[indexArchivo].Substring(4,6)));
                }
                indexArchivo++;
            }

            CuadroTexto.Text += "Mechas cargadas: " + listaMechas.Count + Environment.NewLine + Environment.NewLine;

            int indexMecha = -1;
            while (indexArchivo != lineasArchivo.Length)
            {
                if (lineasArchivo[indexArchivo].Substring(0, 1) == "T")
                    indexMecha++;
                if (lineasArchivo[indexArchivo].Substring(0, 1) == "X")            // Busca agujeros a realizar por esa mecha
                {
                    Agujero agujero = new Agujero
                    {
                        x = float.Parse(lineasArchivo[indexArchivo].Substring(1, lineasArchivo[indexArchivo].IndexOf('Y', 1) - 1)) / CORRECCIONMECHA,
                        y = float.Parse(lineasArchivo[indexArchivo].Substring(lineasArchivo[indexArchivo].IndexOf('Y', 1) + 1,lineasArchivo[indexArchivo].Length - (lineasArchivo[indexArchivo].IndexOf('Y', 1) + 1))) / CORRECCIONMECHA,
                        xStr = lineasArchivo[indexArchivo].Substring(1, lineasArchivo[indexArchivo].IndexOf('Y', 1) - 1),
                        yStr = lineasArchivo[indexArchivo].Substring(lineasArchivo[indexArchivo].IndexOf('Y', 1) + 1, lineasArchivo[indexArchivo].Length - (lineasArchivo[indexArchivo].IndexOf('Y', 1) + 1))
                    };
                    agujero.mecha = new Mecha
                    {
                        diametro = ((Mecha)listaMechas[indexMecha]).diametro,
                        nombre = ((Mecha)listaMechas[indexMecha]).nombre
                    };
                    if (agujero.xStr.Length - 1 < 6)
                    {
                        string ceros = null;
                        int largoCoord = agujero.xStr.Length - 1;
                        for (int j = 0; j < 6 - largoCoord; j++)
                        {
                            ceros += '0';
                        }
                        agujero.xStr = agujero.xStr.Substring(0, 1) + ceros + agujero.xStr.Substring(1, agujero.xStr.Length - 1);
                    }
                    if (agujero.yStr.Length - 1 < 6)
                    {
                        string ceros = null;
                        int largoCoord = agujero.yStr.Length - 1;
                        for (int j = 0; j < 6 - largoCoord; j++)
                        {
                            ceros += '0';
                        }
                        agujero.yStr = agujero.yStr.Substring(0, 1) + ceros + agujero.yStr.Substring(1, agujero.yStr.Length - 1);
                    }
                    agujero.xy = "X" + agujero.xStr + "Y" + agujero.yStr;
                    //agujero.xy = Convertir_xy_int_a_string(agujero.x, agujero.y);
                    listaAgujeros.Add(agujero);
                }
                indexArchivo++;
            }
            
            CuadroTexto.Text += "Listas Terminadas" + Environment.NewLine;
            PuntosExtremos();
            CuadroTexto.Text += "Puntos para calibración guardados" + Environment.NewLine + Environment.NewLine;
        }

        
        // correccion a 0,0 para todos los puntos
        public void CorreccionPuntosNegativos()
        {
            foreach (Agujero agujero in listaAgujeros)
            {
                agujero.x -= float.Parse(esquinas[0].Substring(1,7));
                agujero.y -= float.Parse(esquinas[0].Substring(9,7));
                agujero.xy = Convertir_xy_int_a_string(agujero.x, agujero.y);
            }
        }        

        public void EnviarMecha(Mecha mechaAEnviar)
        {
            string mecha;
            mecha = "M";
            //mecha += mechaAEnviar.diametro.ToString();
            mecha += mechaAEnviar.diametroStr;
            mecha = mecha.Replace(",", ".");
            while (mecha.Length < 6)
            {
                mecha += "0";
            }
            Enviar(mecha);
        }

        public void Leer_Archivo_Esquinas(String path)
        {
            string[] lineasArchivo = null;

            //Propias
            int indexArchivo = 0;
            string esquinaAux=null;
            int contadorNumero;
            int j;
            string coordAux = null;
            string esquinaFinalAux = null;
            int inicioCoordY;
            int largoCoord;
            try
            {
                System.IO.File.OpenRead(path);
                lineasArchivo = System.IO.File.ReadAllLines(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("No se pudo abrir el archivo");
                return;
            }
            catch (ArgumentException e)
            {
                return; // No se seleccionó un archivo
            }


            //Posicionamiento del indice de lectura de las esquinas
            while (lineasArchivo[indexArchivo][0] != 'X')
                indexArchivo++;

            for (int i = 0; i < 4; i++)
            {
                esquinaAux = lineasArchivo[indexArchivo+i];
                contadorNumero = 0;
                j = 1;
                while (esquinaAux[++j] != 'Y')
                    contadorNumero++;
                j++;
                inicioCoordY = j+1;
                coordAux = esquinaAux.Substring(2, contadorNumero);
                if (contadorNumero > 1)
                {
                    coordAux = coordAux.Substring(0, coordAux.Length - 2); // le saco los dos ceros del final
                    if (contadorNumero - 2 <= 5)
                    {
                        string ceros = null;
                        contadorNumero -= 2;
                        for (int k = 0; k < 6 - contadorNumero; k++)
                        {
                            ceros += '0'; 
                        }
                        coordAux = ceros + coordAux; // Le agrego ceros a la izquierda
                    }
                }

                largoCoord = coordAux.Length;
                for (int k = 0; k < 6 - largoCoord; k++) // Rellena con ceros a la derecha si hace falta (para tener 6 digitos)
                    coordAux += '0';
                
                esquinaFinalAux = "X+" + coordAux;

                contadorNumero = 0;
                while (esquinaAux[++j] != 'D')
                    contadorNumero++;
                coordAux = esquinaAux.Substring(inicioCoordY, contadorNumero);
                if (contadorNumero > 1)
                {
                    coordAux = coordAux.Substring(0, coordAux.Length - 2); // le saco los dos ceros del final
                    if (contadorNumero - 2 <= 5)
                    {
                        string ceros = null;
                        contadorNumero -= 2;
                        for (int k = 0; k < 6 - contadorNumero; k++)
                        {
                            ceros += '0';
                        }
                        coordAux = ceros + coordAux; // Le agrego ceros a la izquierda (para tener 6 digitos)
                    }
                }
                largoCoord = coordAux.Length;
                for (int k = 0; k < 6 - largoCoord; k++) // Rellena con ceros a la derecha si hace falta (para tener 6 digitos)
                    coordAux += '0';
                esquinaFinalAux += "Y+" + coordAux;

                //Para aca ya se tiene la esquina cargada en esquinaFinalAux
                //Carga en el vector de esquinas
                esquinas[i] = esquinaFinalAux;
            }
        }
        
        // Envio de datos
        public void Enviar(string caracteres)//OK
        {
            if (PuertoSerie.IsOpen)
            {
                PuertoSerie.Write(caracteres);
            }
            else
            {
                MessageBox.Show("Abrir el puerto para mandar el dato \" " + caracteres + "\"");
            }
        }

        // Guarda caracteres en readBuffer
        public void Recibir()
        {
            String estado = "";
            estado = PuertoSerie.ReadExisting();
            switch (estado[0])
            {
                case 'M':  // - Lo primero que envía el micro

                    estadoMecha = "";
                    indexMecha ++;
                    EnviarMecha((Mecha)listaMechas[indexMecha]); // - Ya envia correctamente
                    break;
                case 'A':
                    /*if (primerAjuste)
                    {
                        primerAjuste = false;
                        Enviar("A");
                    }
                    Enviar(esquinas[numEsquina++]);
                    if (numEsquina == 4)
                        numEsquina = 0;*/
                    if (numEsquina < 4)
                    {
                        if (primerAjuste)
                        {
                            primerAjuste = false;
                            Enviar("A");
                        }
                        Enviar(esquinas[numEsquina++]);
                    }
                    else if (numEsquina++ == 4)
                        Enviar(punto1.xy);
                    else
                    {
                        Enviar(punto2.xy);
                        numEsquina = 0;
                    }
                    break;
                case 'F': // - Lo tercero que envía el micro  
                    Enviar("*"); // - Agregado, da orden de inicio de perforación
                    break;
                case 'P': // - Orden de perforación
                    Enviar("P"); // - El primer agujero de perforación se envía como "PX+xxxxxxY+xxxxxxx"
                    Enviar(((Agujero)listaAgujeros[indexAgujero]).xy);
                    indexAgujero++;
                    break;
                case '*':
                    if (indexAgujero + 1 == listaAgujeros.Count)
                    {
                        Enviar(((Agujero)listaAgujeros[indexAgujero]).xy);
                        Enviar("F");
                        fin = true;
                        break;
                    }
                    if (estadoMecha != "esperaMecha" && fin == false)
                    {
                        Enviar(((Agujero)listaAgujeros[indexAgujero]).xy);
                        if(indexAgujero + 1 < listaAgujeros.Count){
                            if (((Agujero)listaAgujeros[indexAgujero]).mecha.diametro != ((Agujero)listaAgujeros[indexAgujero  + 1]).mecha.diametro){
                                Enviar("P");
                                estadoMecha = "esperaMecha";
                            }
                        }
                        indexAgujero++;     
                    }
                    break;
                case 'O':
                    Enviar("*");
                    primerAjuste = true;
                    break;
            }
            
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

        // Envia posicion de agujero
        public void EnviarAgujero(Agujero agujero)
        {
            Enviar(Convertir_xy_int_a_string(agujero.x, agujero.y));    // Envia posicion de cambio de mecha para bajar y hacer prueba de altura 
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
        
        private void PuertoSerie_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            Recibir();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)//OK
        {
            Enviar(TxtEscribir.Text);
            TxtEscribir.Text = "";
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
                    MessageBox.Show("El puerto " + CboPuertoSerie.SelectedItem + " está ocupado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El puerto " + CboPuertoSerie.SelectedItem + " no se ha podido abrir satisfactoriamente.");
                }
            }
        }

        //Leer las esquinas
        private void esquinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.ShowDialog();
            pathArchivo = OpenFileDialog1.FileName;
            //if (pathArchivo != "openFileDialog1")
            if(!String.IsNullOrEmpty(pathArchivo) && File.Exists(pathArchivo))
            {
                CuadroTexto.Text += "Archivo de esquinas cargado:" + Environment.NewLine + pathArchivo + Environment.NewLine;
                Leer_Archivo_Esquinas(pathArchivo);
                esquinasToolStripMenuItem.Enabled = false;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un archivo", "Error de seleccion");
            }
        }

        private void Calibrar_Click(object sender, EventArgs e)
        {
            //CorreccionPuntosNegativos();
            // POR EL MOMENTO SE EVITA VER SI HAY PUNTOS NEGATIVOS
            Enviar("*");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void VisualizarPuntos_Click(object sender, EventArgs e)
        {

        }

    }
}

