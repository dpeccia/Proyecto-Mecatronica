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
    public partial class GraficadorForm : Form
    {
        
        //public ObtencionDatos.Form1.Agujero agujerito;
        private const int mmPix = 5;
        private const int mmProteus = 1000;
        private System.Collections.ArrayList listaMechas;
        private System.Collections.ArrayList listaAgujeros;
        private Form1.Esquina[] esquinas = new Form1.Esquina[4];

        public GraficadorForm(   System.Collections.ArrayList listaMechas,
                        System.Collections.ArrayList listaAgujeros,
                        Form1.Esquina[] esquinas)
        {
            this.listaMechas = listaMechas;
            this.listaAgujeros = listaAgujeros;
            this.esquinas = esquinas;
            InitializeComponent();
            // Redimensionar:
            this.Size = new Size(esquinas[2].xCoord / mmProteus * mmPix + 50, esquinas[2].yCoord / mmProteus * mmPix + 70);
    
        }

        private void dibujarPlaca(PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(Pens.BlueViolet, 0, 0, esquinas[2].xCoord / mmProteus * mmPix, esquinas[2].yCoord / mmProteus * mmPix);
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, esquinas[2].xCoord / mmProteus * mmPix, esquinas[2].yCoord / mmProteus * mmPix);
        }

        private void dibujarAgujeros(PaintEventArgs e)
        {
            foreach (Form1.Agujero agujero in listaAgujeros)
            {
                /*
                e.Graphics.DrawEllipse(Pens.BlueViolet, 
                                        (agujero.x - agujero.mecha.diametro/20) * 10 * mmPix, 
                                        (agujero.y - agujero.mecha.diametro/20) * 10 * mmPix, 
                                        agujero.mecha.diametro * mmPix, 
                                        agujero.mecha.diametro * mmPix);
                */
                e.Graphics.FillEllipse( Brushes.Gold,
                                        (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        agujero.mecha.diametro * mmPix,
                                        agujero.mecha.diametro * mmPix);
                //e.Graphics.DrawEllipse(pen, (agujero.x+agujero.mecha.diametro/2) * 10 * mmPix, (agujero.y+agujero.mecha.diametro/2) * 10 * mmPix, agujero.mecha.diametro * mmPix, agujero.mecha.diametro * mmPix);
                //e.Graphics.DrawEllipse(pen, agujero.x * 10 * mmPix, agujero.y * 10 * mmPix, mmPix, mmPix);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            dibujarPlaca(e);
            dibujarAgujeros(e);
        }

        public void seleccionarAjuste(Form1.Agujero extremo)
        {
            Graphics gr = panel1.CreateGraphics();
            gr.FillEllipse( Brushes.Magenta,
                            (extremo.x - extremo.mecha.diametro / 20) * 10 * mmPix,
                            (extremo.y - extremo.mecha.diametro / 20) * 10 * mmPix,
                            extremo.mecha.diametro * mmPix,
                            extremo.mecha.diametro * mmPix);
        }

        public void deseleccionarAjuste(Form1.Agujero extremo)
        {
            Graphics gr = panel1.CreateGraphics();
            gr.FillEllipse( Brushes.Gold,
                            (extremo.x - extremo.mecha.diametro / 20) * 10 * mmPix,
                            (extremo.y - extremo.mecha.diametro / 20) * 10 * mmPix,
                            extremo.mecha.diametro * mmPix,
                            extremo.mecha.diametro * mmPix);
        }

        public void deseleccionarAgujero(Form1.Agujero agujero)
        {
            Graphics gr = panel1.CreateGraphics();
            gr.FillEllipse(Brushes.Green,
                                        (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        agujero.mecha.diametro * mmPix,
                                        agujero.mecha.diametro * mmPix);
            gr.DrawEllipse(Pens.Green,
                                    (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                    (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                    agujero.mecha.diametro * mmPix,
                                    agujero.mecha.diametro * mmPix);
        }

        public void seleccionarAgujero(Form1.Agujero agujero)
        {
            Graphics gr = panel1.CreateGraphics();
            gr.FillEllipse(Brushes.Red,
                                        (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        agujero.mecha.diametro * mmPix,
                                        agujero.mecha.diametro * mmPix);
            gr.DrawEllipse(Pens.Red,
                                        (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        agujero.mecha.diametro * mmPix,
                                        agujero.mecha.diametro * mmPix);
        }

        private void GraficadorForm_Load(object sender, EventArgs e)
        {

        }
    }
}


/*
    intern.Graphics.FillEllipse(Brushes.White,
                                        (agujero.x - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        (agujero.y - agujero.mecha.diametro / 20) * 10 * mmPix,
                                        agujero.mecha.diametro * mmPix,
                                        agujero.mecha.diametro * mmPix);
 */


/*

private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int i = 1;
            // int contadormechas;
            Pen pen = new Pen(Color.Black);
            g = panel1.CreateGraphics();
            ObtencionDatos.Form1.Agujero agujero = (ObtencionDatos.Form1.Agujero) ObtencionDatos.Form1.listaAgujeros[0];
            float menorX = agujero.x;
            float menorY = agujero.y;
            float mayorX = agujero.x;
            float mayorY = agujero.y;


            for (int p = 0; p < ObtencionDatos.Form1.listaAgujeros.Count; p++)
            {
                agujerito = (ObtencionDatos.Form1.Agujero)ObtencionDatos.Form1.listaAgujeros[p];
                if (agujerito.x < menorX)
                {
                    menorX = agujerito.x;
                }
                if (agujerito.y < menorY)
                {
                    menorY = agujerito.y;
                }
            }

            if (menorX < 0)
            {
                menorX = menorX * (-1);
            }
            if (menorY < 0)
            {
                menorY = menorY * (-1);
            }

            for (int p = 0; p < ObtencionDatos.Form1.listaAgujeros.Count; p++)
            {
                ObtencionDatos.Form1.Agujero agujerito = (ObtencionDatos.Form1.Agujero) ObtencionDatos.Form1.listaAgujeros[p];
                if (agujerito.x > mayorX)
                {
                    mayorX = agujerito.x;
                }
                if (agujerito.y > mayorY)
                {
                    mayorY = agujerito.y;
                }
            }

            if (mayorX < 0)
            {
                mayorX = mayorX * (-1);
            }
            if (mayorY < 0)
            {
                mayorY = mayorY * (-1);
            }

            float diferenciaX = mayorX - menorX;
            float factorX = diferenciaX / panel1.Width;

            float diferenciaY = mayorY - menorY;
            float factorY = diferenciaY / panel1.Height;
            factorX *= 10;
            factorY *= 10;


            ObtencionDatos.Form1.Agujero agujerote = (ObtencionDatos.Form1.Agujero) ObtencionDatos.Form1.listaAgujeros[0];
            float diametro = agujerote.mecha.diametro;
            diametro = diametro * 30;

            g.Clear(Color.White);
            g.DrawEllipse(pen, ((agujero.x + menorX) / factorX) * 2, ((agujero.y + menorY) / factorY) * 2, diametro, diametro);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            //g.FillEllipse(redBrush, (agujero.x + menorX) / factorX, (agujero.y + menorY) / factorY, diametro, diametro);

            while (i != ObtencionDatos.Form1.listaAgujeros.Count)
            {

                agujero = (ObtencionDatos.Form1.Agujero)ObtencionDatos.Form1.listaAgujeros[i];
                Console.WriteLine("{0}", (agujero.x + menorX) / factorX);
                Console.WriteLine("{0}", (agujero.y + menorY) / factorY);

                g.DrawEllipse(pen, ((agujero.x + menorX) / factorX) * 2, ((agujero.y + menorY) / factorY) * 2, (agujero.mecha.diametro) * 30, (agujero.mecha.diametro) * 30);

                //g.FillEllipse(blackBrush, (agujero.x + menorX) / factorX, (agujero.y + menorY) / factorY, (agujero.mecha) * 300, (agujero.mecha) * 300);
                i++;

            }
            
        }

*/