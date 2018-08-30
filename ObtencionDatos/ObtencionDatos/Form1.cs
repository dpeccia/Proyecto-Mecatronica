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
        public class Agujero
        {
            public int x, y;
            public String xy;
            public float mecha;
        }

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

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pathArchivo = openFileDialog1.FileName;
            label1.Text = pathArchivo;

            Leer_Archivo(pathArchivo);
        }

        public void Leer_Archivo(string path)
        {
            textBox1.Text += pathArchivo + Environment.NewLine;

            try
            {
                System.IO.File.OpenRead(path);
            }
            catch (FileNotFoundException e)
            {

            }
            string textoArchivo;
            string[] lineasArchivo;
            //string[][] listaMechas = new string[2][];
            ArrayList listaAgujeros = new ArrayList();
            ArrayList listaMechas = new ArrayList();

            int cantMechas = 0;
            int i = 0;
            int separador = 0;
            bool igualPorciento = false;

            textoArchivo = System.IO.File.ReadAllText(path);
            lineasArchivo = System.IO.File.ReadAllLines(path);

            textBox1.Text += Environment.NewLine;

            i = 0;
            foreach (string linea in lineasArchivo)         //Cuenta cantidad de mechas
            {
                if (linea == "%")
                {
                    igualPorciento = true;
                    separador = i;
                }
                if (igualPorciento && linea.Contains("T"))
                {
                    textBox1.Text += linea;
                    cantMechas++;
                }
                i++;
            }

            textBox1.Text += Environment.NewLine + "Mechas:" + cantMechas + Environment.NewLine;

            for (i = 0; i < cantMechas; i++)      //Escribo lista
            {
                if (lineasArchivo[i + 2].Contains("T"))
                {
                    //listaMechas[0][i] = lineasArchivo[i+2].Substring(0, 3);
                    Mecha mecha = new Mecha();
                    mecha.nombre = lineasArchivo[i + 2].Substring(0, 3);
                    mecha.diametro = float.Parse(lineasArchivo[i + 2].Substring(4, 6)) / 10000; //Dividimos por 1000 porque se come el .0 xd

                    listaMechas.Add(mecha);

                    //textBox1.Text += listaMechas[0][i] + Environment.NewLine;
                    //listaMechas[1][i] = lineasArchivo[i+2].Substring(4, 5);
                    //textBox1.Text += listaMechas[1][i] + Environment.NewLine;
                }
            }


            textBox1.Text += "Lista Ready";
            String mechaActual;
            Mecha auxMecha = new Mecha();
            float diametroMechaActual = 0;
            for (i = separador; i < lineasArchivo.Length; i++)
            {
                if (lineasArchivo[i].Substring(0, 1) == "T")
                {
                    mechaActual = lineasArchivo[i].Substring(0, 3);
                    foreach (Mecha m in listaMechas)
                    {
                        if (m.nombre == mechaActual)
                        {
                            diametroMechaActual = m.diametro;
                        }
                    }
                }
                if (lineasArchivo[i].Substring(0, 1) == "X")
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

        private void puertoSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Run(new Form2());
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
