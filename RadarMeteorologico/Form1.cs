

/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 * Curso Técnico em Informática - Data de Entrega: 25 / 05 / 2026
 * Autores do Projeto: Lucas Pierre Mendes Rodrigues
 *                     Leonardo de Almeida Henrique
 *
 * Turma: 3J
 * Projeto 2ºBimestre 
 * Observação: < colocar se houver>
 * 
 * 
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RadarMeteorologico
{
    public partial class Form1 : Form
    {
        int tamanhoRadar = 150;

        int centroX = 0;
        int centroY = 0;

        int origemX = 0;
        int origemY = 0;

        int anguloAtual = 0;

        bool alternarFeixe = true;

        List<Point> listaPontos = new List<Point>();
        int quantidadeCliques = 0;

        List<Tuple<Point, Point, int>> linhasRadar =
            new List<Tuple<Point, Point, int>>();

        int modoSelecionado = 0;

        System.Windows.Forms.Timer animacaoRadar =
            new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Radar Meteorologico Estilizado";

            this.KeyPreview = true;

            animacaoRadar.Interval = 20;
            animacaoRadar.Tick += new EventHandler(AtualizarRadar);
            animacaoRadar.Start();

            this.Focus();
        }

        private void AtualizarRadar(object sender, EventArgs e)
        {
            anguloAtual = (anguloAtual + 2) % 360;

            if (anguloAtual == 0)
                alternarFeixe = !alternarFeixe;

            pictureBox1.Invalidate();
        }

        private void DesenharPixel(Graphics grafico, int px, int py, Color cor)
        {
            Pen lapis = new Pen(cor, 1);

            grafico.DrawLine(lapis, px, py, px + 1, py);

            lapis.Dispose();
        }

        private void DesenharLinha(Graphics grafico,
                                   int x1, int y1,
                                   int x2, int y2,
                                   Color cor,
                                   float largura,
                                   float[] estilo)
        {
            Pen lapis = new Pen(cor, largura);

            if (estilo != null)
                lapis.DashPattern = estilo;

            grafico.DrawLine(lapis, x1, y1, x2, y2);

            lapis.Dispose();
        }

        private void LinhaComGradiente(Graphics grafico,
                                       int x1, int y1,
                                       int x2, int y2,
                                       Color corInicio,
                                       Color corFinal,
                                       float espessura)
        {
            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);

            int totalPontos = Math.Max(deltaX, deltaY);

            if (totalPontos == 0)
                return;

            for (int i = 0; i <= totalPontos; i++)
            {
                float proporcao = (float)i / totalPontos;

                int r = (int)(corInicio.R + proporcao * (corFinal.R - corInicio.R));
                int g = (int)(corInicio.G + proporcao * (corFinal.G - corInicio.G));
                int b = (int)(corInicio.B + proporcao * (corFinal.B - corInicio.B));

                Color corAtual = Color.FromArgb(255, r, g, b);

                int posX = x1 + (int)((x2 - x1) * proporcao);
                int posY = y1 + (int)((y2 - y1) * proporcao);

                DesenharPixel(grafico, posX, posY, corAtual);

                if (espessura >= 2)
                {
                    DesenharPixel(grafico, posX + 1, posY, corAtual);
                    DesenharPixel(grafico, posX, posY + 1, corAtual);
                    DesenharPixel(grafico, posX + 1, posY + 1, corAtual);
                }

                if (espessura >= 4)
                {
                    DesenharPixel(grafico, posX - 1, posY, corAtual);
                    DesenharPixel(grafico, posX, posY - 1, corAtual);
                    DesenharPixel(grafico, posX + 2, posY, corAtual);
                    DesenharPixel(grafico, posX, posY + 2, corAtual);
                }
            }
        }

        private void CriarCirculo(Graphics grafico,
                                  int xCentro,
                                  int yCentro,
                                  int raio,
                                  Color cor,
                                  float espessura,
                                  float[] estilo)
        {
            for (int i = 0; i < 360; i++)
            {
                double angulo1 = i * Math.PI / 180.0;
                double angulo2 = (i + 1) * Math.PI / 180.0;

                int pontoX1 = xCentro + (int)(raio * Math.Cos(angulo1));
                int pontoY1 = yCentro + (int)(raio * Math.Sin(angulo1));

                int pontoX2 = xCentro + (int)(raio * Math.Cos(angulo2));
                int pontoY2 = yCentro + (int)(raio * Math.Sin(angulo2));

                DesenharLinha(grafico,
                              pontoX1, pontoY1,
                              pontoX2, pontoY2,
                              cor,
                              espessura,
                              estilo);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics grafico = e.Graphics;

            int meioX = pictureBox1.Width / 2;
            int meioY = pictureBox1.Height / 2;

            centroX = meioX;
            centroY = meioY;

            origemX = meioX;
            origemY = meioY;

            grafico.Clear(Color.White);

            // =========================
            // CÍRCULOS DO RADAR
            // =========================

            CriarCirculo(grafico,
                         meioX,
                         meioY,
                         tamanhoRadar,
                         Color.FromArgb(0, 180, 0),
                         1,
                         null);

            CriarCirculo(grafico,
                         meioX,
                         meioY,
                         tamanhoRadar * 2 / 3,
                         Color.FromArgb(0, 140, 0),
                         1,
                         null);

            CriarCirculo(grafico,
                         meioX,
                         meioY,
                         tamanhoRadar / 3,
                         Color.FromArgb(0, 110, 0),
                         1,
                         null);

            // =========================
            // LINHAS CENTRAIS
            // =========================

            DesenharLinha(grafico,
                          meioX - tamanhoRadar,
                          meioY,
                          meioX + tamanhoRadar,
                          meioY,
                          Color.FromArgb(0, 120, 0),
                          1,
                          null);

            DesenharLinha(grafico,
                          meioX,
                          meioY - tamanhoRadar,
                          meioX,
                          meioY + tamanhoRadar,
                          Color.FromArgb(0, 120, 0),
                          1,
                          null);

            // =========================
            // FEIXE DO RADAR
            // =========================

            int destinoX =
                origemX +
                (int)(tamanhoRadar *
                Math.Cos(anguloAtual * 3.15 / 180.0));

            int destinoY =
                origemY +
                (int)(tamanhoRadar *
                Math.Sin(anguloAtual * 3.15 / 180.0));

            Color corInicial;
            Color corFinal;

            if (alternarFeixe)
            {
                corInicial = Color.Lime;
                corFinal = Color.FromArgb(50, 50, 50);
            }
            else
            {
                corInicial = Color.FromArgb(50, 50, 50);
                corFinal = Color.Lime;
            }

            LinhaComGradiente(grafico,
                              meioX,
                              meioY,
                              destinoX,
                              destinoY,
                              corInicial,
                              corFinal,
                              4);

            // =========================
            // LINHAS CRIADAS PELO USUÁRIO
            // =========================

            foreach (Tuple<Point, Point, int> item in linhasRadar)
            {
                if (item.Item3 == 1)
                {
                    float[] tracejado = { 5f, 2f };

                    DesenharLinha(grafico,
                                  item.Item1.X,
                                  item.Item1.Y,
                                  item.Item2.X,
                                  item.Item2.Y,
                                  Color.Red,
                                  2,
                                  tracejado);
                }
                else if (item.Item3 == 2)
                {
                    float[] tracoPonto = { 5f, 2f, 1f, 2f };

                    DesenharLinha(grafico,
                                  item.Item1.X,
                                  item.Item1.Y,
                                  item.Item2.X,
                                  item.Item2.Y,
                                  Color.Blue,
                                  3,
                                  tracoPonto);
                }
            }

            // =========================
            // MARCAÇÃO DO PRIMEIRO CLIQUE
            // =========================

            if (quantidadeCliques == 1 &&
                listaPontos.Count == 1)
            {
                DesenharPixel(grafico,
                              listaPontos[0].X,
                              listaPontos[0].Y,
                              Color.Yellow);

                DesenharPixel(grafico,
                              listaPontos[0].X + 1,
                              listaPontos[0].Y,
                              Color.Yellow);

                DesenharPixel(grafico,
                              listaPontos[0].X,
                              listaPontos[0].Y + 1,
                              Color.Yellow);

                DesenharPixel(grafico,
                              listaPontos[0].X + 1,
                              listaPontos[0].Y + 1,
                              Color.Yellow);
            }

            // =========================
            // PAINEL INFERIOR
            // =========================

            Font fonteRadar =
    new Font("Consolas", 10, FontStyle.Bold);

            int painelY = pictureBox1.Height - 40;

            SolidBrush fundoPainel =
                new SolidBrush(Color.FromArgb(20, 20, 20));

            grafico.FillRectangle(
                fundoPainel,
                0,
                painelY,
                pictureBox1.Width,
                40);

           

            fonteRadar.Dispose();
            fundoPainel.Dispose();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (modoSelecionado == 0)
                return;

            int mouseX = e.X;
            int mouseY = e.Y;

            listaPontos.Add(new Point(mouseX, mouseY));

            quantidadeCliques++;

            if (quantidadeCliques == 2)
            {
                linhasRadar.Add(
                    new Tuple<Point, Point, int>(
                        listaPontos[0],
                        listaPontos[1],
                        modoSelecionado));

                listaPontos.Clear();

                quantidadeCliques = 0;
            }

            pictureBox1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 ||
                e.KeyCode == Keys.NumPad1)
            {
                modoSelecionado = 1;
            }
            else if (e.KeyCode == Keys.D2 ||
                     e.KeyCode == Keys.NumPad2)
            {
                modoSelecionado = 2;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (quantidadeCliques == 0)
                modoSelecionado = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}