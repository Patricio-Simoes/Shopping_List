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
    public partial class RemoverCategoriaWindow : Window
    {
        private App app;
        public RemoverCategoriaWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            VisualizaCategorias();
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearBoxes();
            this.Hide();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].fixa)
            {
                MessageBoxResult result = MessageBox.Show("Deseja apagar a Categoria selecionada?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        app._ModelListasCategorias.Categorias.RemoveAt(cbNome.SelectedIndex);
                        app._ModelListasCategorias.nomesCategorias.RemoveAt(cbNome.SelectedIndex);
                        MessageBox.Show("Categoria eliminada com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Dá refresh às janelas associadas
                        app._VerCategoriaWindow.VisualizaCategorias();
                        app._EditarCategoriaWindow.VisualizaCategorias();
                        app._RemoverCategoriaWindow.VisualizaCategorias();
                        app._MainWindow.Show();
                        ClearBoxes();
                        this.Hide();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro! Selecione uma Categoria válida!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show("A Categoria selecionada não pode ser apagada!\nSelecione uma Categoria não predefinida!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void VisualizaCategorias()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesCategorias;
        }
        // Ao alterar a seleção na comboBox, manda os valores da categoria selecionada para as restantes comboBoxes
        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNome.SelectedItem != null)
            {
                tbNome.Text = app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].nome.ToString();
                tbDescrição.Text = app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].observações.ToString();
                tbID.Text = app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].id_Categoria.ToString();
            }
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.cbNome.SelectedItem = null;
            this.tbDescrição.Text = "";
            this.tbID.Text = "";
            this.tbNome.Text = "";
        }
    }
}
