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

namespace UTAD.Lab.ShoppingList.WPF.Views
{
    public partial class RemoverProdutoWindow : Window
    {
        private App app;
        public RemoverProdutoWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaProdutos();
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            this.Hide();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].fixo)
            {
                MessageBoxResult result = MessageBox.Show("Deseja apagar o Produto selecionado?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        app._ModelListasCategorias.Produtos.RemoveAt(cbNome.SelectedIndex);
                        app._ModelListasCategorias.nomesProdutos.RemoveAt(cbNome.SelectedIndex);
                        MessageBox.Show("Produto eliminado com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Dá refresh às janelas associadas
                        app._VerProdutoWindow.VisualizaProdutos();
                        app._EditarProdutoWindow.VisualizaProdutos();
                        app._RemoverProdutoWindow.VisualizaProdutos();
                        app._MainWindow.Show();
                        ClearBoxes();
                        this.Hide();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro! Selecione um Produto válido!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show("O Produto selecionado não pode ser apagado!\nSelecione um Produto não predefinido!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void VisualizaProdutos()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesProdutos;
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.cbNome.SelectedItem = null;
            this.tbPreçoUnidade.Text = "";
            this.tbDescrição.Text = "";
            this.tbIDProduto.Text = "";
            this.tbCategoria.Text = "";
            this.tbUnidade.Text = "";
        }

        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNome.SelectedItem != null)
            {
                tbPreçoUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].preço.ToString();
                tbDescrição.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].descrição.ToString();
                tbIDProduto.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Produto.ToString();
                tbCategoria.Text = app._ModelListasCategorias.GetCategoria(app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Categoria);
                tbUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].unidade.ToString();
            }
        }
    }
}
