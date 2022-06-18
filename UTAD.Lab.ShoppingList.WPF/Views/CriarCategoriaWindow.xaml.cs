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
    public partial class CriarCategoriaWindow : Window
    {
        private App app;
        public CriarCategoriaWindow()
        {
            InitializeComponent();

            app = App.Current as App;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearBoxes();
            this.Hide();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Adicionar Categoria?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (tbNome.Text != "" && tbNome.Text != null && tbDescrição.Text != "" && tbDescrição.Text != null && tbID.Text != "" && tbID.Text != null)
                {
                    if (!app._ModelListasCategorias.nomesCategorias.Contains(tbNome.Text.ToString()))
                    {
                        try
                        {
                            app._ModelListasCategorias.CriaCategoria(int.Parse(tbID.Text.ToString()), tbNome.Text.ToString(), tbDescrição.Text.ToString());
                            MessageBox.Show("Categoria inserida com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            MessageBox.Show("Erro! Insira valores válidos!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("ERRO! Impossível inserir uma Categoria já existente!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Erro! Preencha todos os campos!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearBoxes()
        {
            // Limpa os campos
            this.tbDescrição.Text = "";
            this.tbID.Text = "";
            this.tbNome.Text = "";
        }
    }
}
