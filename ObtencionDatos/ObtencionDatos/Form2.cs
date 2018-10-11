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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public ObtencionDatos.Form1.Agujero agujerito;
        Graphics g;

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
    }
}
