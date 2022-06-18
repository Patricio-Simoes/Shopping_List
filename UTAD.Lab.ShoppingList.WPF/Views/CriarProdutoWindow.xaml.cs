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
    public partial class CriarProdutoWindow : Window
    {
        private App app;
        public CriarProdutoWindow()
        {
            InitializeComponent();

            app = App.Current as App;

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
            MessageBoxResult result = MessageBox.Show("Adicionar Produto?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (tbNome.Text != "" && tbNome.Text != null && cbUnidade.Text != "" && cbUnidade.Text != null 
                    && tbPreçoUnidade.Text != "" && tbPreçoUnidade.Text != null && tbDescrição.Text != "" && tbDescrição.Text != null && cbCategoria.Text != "" 
                    && cbCategoria.Text != null && tbIDProduto.Text != "" && tbIDProduto.Text != null)
                {
                    if (!app._ModelListasCategorias.nomesProdutos.Contains(tbNome.Text.ToString()))
                    {
                        try
                        {
                            app._ModelListasCategorias.CriaProduto(int.Parse(tbIDProduto.Text.ToString()), app._ModelListasCategorias.GetIDCategoria(cbCategoria.Text.ToString()),
                            tbNome.Text.ToString(), tbDescrição.Text.ToString(), double.Parse(tbPreçoUnidade.Text.ToString()), cbUnidade.Text.ToString());
                            MessageBox.Show("Produto inserido com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            MessageBox.Show("Erro! Insira valores válidos!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("ERRO! Impossível inserir um Produto já existente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Erro! Preencha todos os campos!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Carrega as Categorias para a comboBox
        public void VisualizaCategorias()
        {
            cbCategoria.ItemsSource = null;
            cbCategoria.ItemsSource = app._ModelListasCategorias.nomesCategorias;
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.tbNome.Text = "";
            this.tbPreçoUnidade.Text = "";
            this.tbDescrição.Text = "";
            this.tbIDProduto.Text = "";
            this.cbUnidade.Text = "";
            this.cbCategoria.Text = "";
        }

        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
