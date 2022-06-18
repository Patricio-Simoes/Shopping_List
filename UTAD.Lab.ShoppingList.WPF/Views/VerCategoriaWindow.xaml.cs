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
    public partial class VerCategoriaWindow : Window
    {
        private App app { get; set; }
        public VerCategoriaWindow()
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

        private void editarCategoriaButton_Click(object sender, RoutedEventArgs e)
        {
            // Passa os dados selecionados para a janela editar categoria
            app._EditarCategoriaWindow.cbNome.Text = cbNome.Text;
            app._EditarCategoriaWindow.tbNome.Text = tbNome.Text.ToString();
            app._EditarCategoriaWindow.tbDescrição.Text = tbDescrição.Text.ToString();
            app._EditarCategoriaWindow.Show();
            ClearBoxes();
            this.Hide();
        }
        // Carrega as Categorias para a comboBox
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