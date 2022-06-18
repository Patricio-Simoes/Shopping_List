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
    public partial class EditarCategoriaWindow : Window
    {
        private App app;
        public EditarCategoriaWindow()
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
        // Carrega as Categorias para a comboBox
        public void VisualizaCategorias()
        {
            cbNome.ItemsSource = null;
            cbNome.ItemsSource = app._ModelListasCategorias.nomesCategorias;
        }
        // Confirma as alterações feitas ao editar antes de as guardar
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbNome.Text != "" && cbNome.Text != null && tbNome.Text != "" && tbNome.Text != null && tbID.Text != null && tbID.Text != "" && tbDescrição.Text != null && tbDescrição.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Deseja salvar as alterações efetuadas?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (!app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].fixa)
                    {
                        //  Altera os campos da Categoria selecionada
                        try
                        {
                            app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].id_Categoria = app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].id_Categoria;
                            app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].nome = tbNome.Text.ToString();
                            app._ModelListasCategorias.Categorias[cbNome.SelectedIndex].observações = tbDescrição.Text.ToString();
                            app._ModelListasCategorias.nomesCategorias[cbNome.SelectedIndex] = tbNome.Text.ToString();

                            MessageBox.Show("Categoria atualizada com sucesso!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("ERRO! Preencha os campos corretamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("ERRO! Impossível alterar uma Categoria predefinida", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    app._MainWindow.Show();
                    ClearBoxes();
                    this.Hide();
                }
            }
            else
                MessageBox.Show("ERRO! Preencha todos os campos corretamente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
