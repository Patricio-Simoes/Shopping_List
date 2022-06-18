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
    public partial class VerProdutoWindow : Window
    {
        private App app { get; set; }

        public VerProdutoWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaProdutos();

            // Adiciona as unidades

            cbUnidade.Items.Add("Individual");
            cbUnidade.Items.Add("Embalagem");
            cbUnidade.Items.Add("Quilo");
            cbUnidade.Items.Add("Grama");
            cbUnidade.Items.Add("Litro");
            cbUnidade.Items.Add("Centilitro");
            cbUnidade.Items.Add("Metro");
            cbUnidade.Items.Add("Centímetro");
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearBoxes();
            this.Hide();
        }

        private void editarProdutoButton_Click(object sender, RoutedEventArgs e)
        {
            app._EditarProdutoWindow.Show();
            // Passa os dados selecionados para a janela editar produto
            app._EditarProdutoWindow.cbNome.Text = cbNome.Text;
            app._EditarProdutoWindow.tbPreçoUnidade.Text = tbPreçoUnidade.Text.ToString();
            app._EditarProdutoWindow.tbDescrição.Text = tbDescrição.Text.ToString();
            app._EditarProdutoWindow.tbIDProduto.Text = tbIDProduto.Text.ToString();
            app._EditarProdutoWindow.cbCategoria.Text = tbCategoria.Text;
            app._EditarProdutoWindow.Show();
            ClearBoxes();
            this.Hide();
        }
        public void VisualizaProdutos()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesProdutos;
        }
        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNome.SelectedItem != null)
            {
                tbPreçoUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].preço.ToString();
                tbDescrição.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].descrição.ToString();
                tbIDProduto.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Produto.ToString();
                tbCategoria.Text = app._ModelListasCategorias.GetCategoria(app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Categoria);
                cbUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].unidade.ToString();
            }
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.cbNome.SelectedItem = null;
            this.tbPreçoUnidade.Text = "";
            this.tbDescrição.Text = "";
            this.tbIDProduto.Text = "";
            this.tbCategoria.Text = "";
        }
    }
}