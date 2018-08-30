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

        string pathArchivo;
        public Form1()
        {
            InitializeComponent();
        }

        // Menu
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pathArchivo = openFileDialog1.FileName;
            label1.Text = pathArchivo;

            Leer_Archivo(pathArchivo);
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
                    agujero.x = Int32.Parse(lineasArchivo[i].Substring(1, 7));
                    agujero.y = Int32.Parse(lineasArchivo[i].Substring(9, 7));
                    agujero.mecha = diametroMechaActual;
                    listaAgujeros.Add(agujero);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void calEjeZ_Click(object sender, EventArgs e)
        {

        }

        // Menu Puerto Serie
        private void puertoSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
