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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTAD.Lab.ShoppingList.WPF.Models;

namespace UTAD.Lab.ShoppingList.WPF
{
    public partial class MainWindow : Window
    {
        private App app;
        public MainWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaListas();
        }

        private void verProdutoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._VerProdutoWindow.Show();
            this.Hide();
            ClearBoxes();
        }

        private void editarProdutoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._EditarProdutoWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void removerProdutoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._RemoverProdutoWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void criarProdutoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._CriarProdutoWindow.tbIDProduto.Text = (app._ModelListasCategorias.Produtos.Last().id_Produto + 1).ToString();
            app._CriarProdutoWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void criarCategoriaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._CriarCategoriaWindow.tbID.Text = (app._ModelListasCategorias.Categorias.Last().id_Categoria + 1).ToString();
            app._CriarCategoriaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void editarCategoriaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._EditarCategoriaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void removerCategoriaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._RemoverCategoriaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void verCategoriaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._VerCategoriaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void sobreMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._SobreWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void removerListaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (listComboBox.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Deseja remover a lista selecionada?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    app._ModelListasCategorias.Listas[listComboBox.SelectedIndex].produtos.Clear();
                    app._ModelListasCategorias.Listas.RemoveAt(listComboBox.SelectedIndex);
                    app._ModelListasCategorias.nomesListas.RemoveAt(listComboBox.SelectedIndex);
                    VisualizaListas();
                    ClearLV();
                    // Dá refresh às janelas associadas
                    app._EditarListaWindow.VisualizaListas();
                    app._VerListaWindow.VisualizaListas();
                }
            }
            else
                MessageBox.Show("Selecione uma Lista válida!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void criarListaWindow_Click(object sender, RoutedEventArgs e)
        {
            app._CriarListaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void editarListaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._EditarListaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void verListasMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._VerListaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void adicionarListaButton_Click(object sender, RoutedEventArgs e)
        {
            app._CriarListaWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void categoriasPredefinidasMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._CategoriasPredefinidasWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        private void categoriasExistentesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            app._CategoriasExistentesWindow.Show();
            this.Hide();
            ClearBoxes();
        }
        public void VisualizaListas()
        {
            listComboBox.ItemsSource = null;
            listComboBox.ItemsSource = app._ModelListasCategorias.nomesListas;
        }
        private void ClearLV()
        {
            lvListas.ItemsSource = null;
        }

        private void listComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double falta = 0;
            double pago = 0;

            if (listComboBox.SelectedItem != null)
            {
                lvListas.ItemsSource = null;
                lvListas.ItemsSource = app._ModelListasCategorias.Listas[listComboBox.SelectedIndex].produtos;
                gbListas.Header = app._ModelListasCategorias.Listas[listComboBox.SelectedIndex].nome;

                // Trata de apresentar os valores pagos e por pagar

                foreach (ProdutoAux p in app._ModelListasCategorias.Listas[listComboBox.SelectedIndex].produtos)
                {
                    // Valores em falta

                    if (!p.comprado)
                    {
                        foreach(Produto prod in app._ModelListasCategorias.Produtos)
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
        private void guardarListaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja guardar as alterações feitas?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                app._ModelListasCategorias.SaveCategorias();
                app._ModelListasCategorias.SaveProdutos();
                app._ModelListasCategorias.SaveListas();
                MessageBox.Show("Alerações guardadas com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ClearBoxes()
        {
            lvListas.ItemsSource = null;
            listComboBox.SelectedIndex = -1;
            this.lblFalta.Text = "Valor em Falta:\n00,00€";
            this.lblPago.Text = "Valor Pago:\n00,00€";
        }
    }
}
