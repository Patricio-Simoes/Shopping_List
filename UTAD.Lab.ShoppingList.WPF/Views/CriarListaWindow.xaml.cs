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
    public partial class CriarListaWindow : Window
    {
        private App app;

        List<ProdutoAux> temp;

        public CriarListaWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            temp = new List<ProdutoAux>();

            VisualizaProdutos();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            this.Hide();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbNome.Text != null && tbNome.Text != "")
            {
                if (!app._ModelListasCategorias.nomesListas.Contains(tbNome.Text.ToString()))
                {
                    app._ModelListasCategorias.AdicionaLista(app._ModelListasCategorias.Listas.Last().id_Lista + 1, tbNome.Text.ToString(), temp);
                    MessageBox.Show("Lista adicionada com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    temp.Clear();
                    // Dá refresh às janelas associadas
                    app._VerListaWindow.VisualizaListas();
                    app._EditarListaWindow.VisualizaListas();
                    app._MainWindow.VisualizaListas();
                    app._MainWindow.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("ERRO! Impossível inserir uma Lista já existente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("ERRO! Verifique se preencheu corretamente os campos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void VisualizaProdutos()
        {
            Combo_Adicionar_Produtos.ItemsSource = null;
            Combo_Adicionar_Produtos.ItemsSource = app._ModelListasCategorias.nomesProdutos;
        }

        private void adicionaButton_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_Adicionar_Produtos.SelectedItem != null && tbQuantidade.Text != null && tbQuantidade.Text != "")
            {
                try {
                    ProdutoAux p = new ProdutoAux()
                    {
                        id_Produto = app._ModelListasCategorias.Produtos[Combo_Adicionar_Produtos.SelectedIndex].id_Produto,
                        nome = app._ModelListasCategorias.Produtos[Combo_Adicionar_Produtos.SelectedIndex].nome,
                        quantidade = double.Parse(tbQuantidade.Text.ToString()),
                        comprado = false
                    };
                    temp.Add(p);

                    UpdateLV();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro! Verifique se introduziu dados válidos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("ERRO! Selecione um Produto válido!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void UpdateLV()
        {
            lvListas.ItemsSource = null;
            lvListas.ItemsSource = temp;
        }
    }
}
