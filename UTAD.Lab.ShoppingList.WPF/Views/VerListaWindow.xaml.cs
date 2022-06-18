using System;
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
using System.Windows.Shapes;
using UTAD.Lab.ShoppingList.WPF.Models;

namespace UTAD.Lab.ShoppingList.WPF.Views
{
    public partial class VerListaWindow : Window
    {
        private App app;
        public VerListaWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaListas();
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearBoxes();
            this.Hide();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearBoxes();
            this.Hide();
        }
        public void VisualizaListas()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesListas;
        }

        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double falta = 0;
            double pago = 0;

            if (cbNome.SelectedItem != null)
            {
                tbID.Text = app._ModelListasCategorias.Listas[cbNome.SelectedIndex].id_Lista.ToString();
                lvListas.ItemsSource = null;
                lvListas.ItemsSource = app._ModelListasCategorias.Listas[cbNome.SelectedIndex].produtos;
                gbListas.Header = app._ModelListasCategorias.Listas[cbNome.SelectedIndex].nome;

                // Trata de apresentar os valores pagos e por pagar

                foreach (ProdutoAux p in app._ModelListasCategorias.Listas[cbNome.SelectedIndex].produtos)
                {
                    // Valores em falta

                    if (!p.comprado)
                    {
                        foreach (Produto prod in app._ModelListasCategorias.Produtos)
                        {
                            if (prod.id_Produto == p.id_Produto)
                                falta += (prod.preço * p.quantidade);
                        }
                    }

                    // Valores pagos

                    if (p.comprado)
                    {
                        foreach (Produto prod in app._ModelListasCategorias.Produtos)
                        {
                            if (prod.id_Produto == p.id_Produto)
                                pago += (prod.preço * p.quantidade);
                        }
                    }
                }

                this.lblFalta.Text = "Valor em Falta:\n" + falta + "€";
                this.lblPago.Text = "Valor Pago:\n" + pago + "€";
            }
        }
        private void ClearBoxes()
        {
            lvListas.ItemsSource = null;
            cbNome.SelectedIndex = -1;
            tbID.Text = "";
            this.lblFalta.Text = "Valor em Falta:\n00,00€";
            this.lblPago.Text = "Valor Pago:\n00,00€";
        }

        private void editarListaButton_Click(object sender, RoutedEventArgs e)
        {
            app._EditarListaWindow.cbNome.SelectedIndex = this.cbNome.SelectedIndex;
            app._EditarListaWindow.tbID.Text = this.tbID.Text;
            app._EditarListaWindow.Show();
            ClearBoxes();
            this.Hide();
        }
    }
}
