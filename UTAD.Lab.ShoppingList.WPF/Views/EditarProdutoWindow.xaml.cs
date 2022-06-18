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
    public partial class EditarProdutoWindow : Window
    {
        private App app;
        public EditarProdutoWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaProdutos();

            VisualizaCategorias();

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

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbIDProduto.Text != null && tbIDProduto.Text != "" && cbNome.Text != null && cbNome.Text != "" && cbUnidade.Text != null && cbUnidade.Text != ""
                && tbPreçoUnidade.Text != null && tbPreçoUnidade.Text != "" && cbCategoria.Text != null && cbCategoria.Text != "" && tbDescrição.Text != null && tbDescrição.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Deseja salvar as alterações efetuadas?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //  Altera os campos do Produto selecionado
                    try
                    {
                        app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Produto = int.Parse(tbIDProduto.Text.ToString());
                        app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Categoria = app._ModelListasCategorias.Categorias[cbCategoria.SelectedIndex].id_Categoria;
                        app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].descrição = tbDescrição.Text.ToString();
                        app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].preço = double.Parse(tbPreçoUnidade.Text.ToString());
                        app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].unidade = cbUnidade.Text.ToString();

                        MessageBox.Show("Produto atualizado com sucesso!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ERRO! Preencha os campos corretamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    app._MainWindow.Show();
                    ClearBoxes();
                    this.Hide();
                }
            }
            else
                MessageBox.Show("ERRO! Preencha todos os campos corretamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void VisualizaProdutos()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesProdutos;
        }
        public void VisualizaCategorias()
        {
            cbCategoria.ItemsSource = null;
            cbCategoria.ItemsSource = app._ModelListasCategorias.nomesCategorias;
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.cbNome.SelectedItem = null;
            this.tbPreçoUnidade.Text = "";
            this.tbDescrição.Text = "";
            this.tbIDProduto.Text = "";
            this.cbCategoria.Text = "";
            this.cbUnidade.Text = "";
        }

        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNome.SelectedItem != null)
            {
                tbPreçoUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].preço.ToString();
                tbDescrição.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].descrição.ToString();
                tbIDProduto.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Produto.ToString();
                cbCategoria.Text = app._ModelListasCategorias.GetCategoria(app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].id_Categoria);
                cbUnidade.Text = app._ModelListasCategorias.Produtos[cbNome.SelectedIndex].unidade.ToString();
            }
        }
    }
}
