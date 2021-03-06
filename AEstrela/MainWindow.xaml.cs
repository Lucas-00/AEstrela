﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AEstrela
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int numeroLinhas = 50;
        private const int numeroColunas = 50;
        private int[,] tabuleiro = new int[numeroLinhas, numeroColunas];
        private Label[,] labels = new Label[numeroLinhas, numeroColunas];
        private ETipoClique tipoClique = ETipoClique.Origem;
        private Vertice origem = null;
        private Vertice meta = null;

        public MainWindow()
        {
            InitializeComponent();

            MontarTabuleiro();
        }

        private void MontarTabuleiro()
        {
            for (int i = 0; i < numeroLinhas; i++)
            {
                var sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Visibility = Visibility.Visible;
                containerPrincipal.Children.Add(sp);

                for (int j = 0; j < numeroColunas; j++)
                {
                    var lbl = new Label();
                    lbl.Name = $"lbl{i.ToString("00")}_{j.ToString("00")}";
                    lbl.BorderBrush = Brushes.Black;
                    lbl.BorderThickness = new Thickness(1);
                    lbl.Visibility = Visibility.Visible;
                    lbl.Background = Brushes.Silver;
                    labels[i, j] = lbl;

                    lbl.MouseDown += Lbl_MouseDown;

                    sp.Children.Add(lbl);
                }
            }
        }

        private void Lbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                var aux = (Label)sender;

                int i = Convert.ToInt32(aux.Name.Substring(3, 2));
                int j = Convert.ToInt32(aux.Name.Substring(6, 2));

                if (tipoClique == ETipoClique.Origem)
                {
                    tipoClique = ETipoClique.Meta;
                    aux.Background = Brushes.Blue;
                    tabuleiro[i, j] = 1;
                    origem = new Vertice(i, j);
                    lblMsg.Content = "Clique na meta";
                }
                else if (tipoClique == ETipoClique.Meta)
                {
                    tipoClique = ETipoClique.Obstaculo;
                    aux.Background = Brushes.Red;
                    tabuleiro[i, j] = 9;
                    meta = new Vertice(i, j);
                    lblMsg.Content = "Clique no(s) obstáculo(s)";

                    btnProcessar.Visibility = Visibility.Visible;
                }
                else
                {
                    aux.Background = Brushes.Black;
                    tabuleiro[i, j] = 2;
                }
            }
        }

        private void btnProcessar_Click(object sender, RoutedEventArgs e)
        {
            var alg = new AEstrelaProc();

            var caminho = alg.ProcessarERetornarCaminho(tabuleiro, numeroLinhas, numeroColunas, origem, meta, new Heuristica());

            MostrarCaminho(caminho);

            btnLimpar.Visibility = Visibility.Visible;
            btnProcessar.IsEnabled = true;

        }

        private void MostrarCaminho(List<Vertice> caminho)
        {
            if (caminho == null || caminho.Count == 0)
            {
                MessageBox.Show("Caminho não encontrado.");
            }
            else
            {
                foreach (var v in caminho)
                    if (tabuleiro[v.Linha, v.Coluna] == 0)
                        labels[v.Linha, v.Coluna].Background = Brushes.Green;
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < numeroLinhas; i++)
                for (int j = 0; j < numeroColunas; j++)
                    if (tabuleiro[i, j] == 0)
                        labels[i, j].Background = Brushes.Silver;

            btnLimpar.Visibility = Visibility.Hidden;
            btnProcessar.IsEnabled = true;
        }

        private enum ETipoClique
        {
            Origem,
            Meta,
            Obstaculo
        }
    }
}
