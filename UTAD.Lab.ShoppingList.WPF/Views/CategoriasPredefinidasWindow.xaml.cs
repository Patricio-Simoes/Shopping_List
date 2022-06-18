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
    public partial class CategoriasPredefinidasWindow : Window
    {
        private App app;
        public CategoriasPredefinidasWindow()
        {
            InitializeComponent();

            app = App.Current as App;

            cbCategorias.Items.Add("Categorias");
            cbCategorias.Items.Add("Produtos");

            lvCategorias.ItemsSource = app._ModelListasCategorias.CategoriasPredefinidas;
            lvProdutos.ItemsSource = app._ModelListasCategorias.ProdutosPredefinidos;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            ClearLV();
            this.Hide();
        }

        private void cbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategorias.SelectedIndex == 0)
            {
                lvCategorias.Visibility = Visibility.Visible;
                lvCategorias.IsEnabled = true;
                lvProdutos.Visibility = Visibility.Hidden;
                lvProdutos.IsEnabled = false;
            }
            if (cbCategorias.SelectedIndex == 1)
            {
                lvCategorias.Visibility = Visibility.Hidden;
                lvCategorias.IsEnabled = false;
                lvProdutos.Visibility = Visibility.Visible;
                lvProdutos.IsEnabled = true;
            }
        }
        private void ClearLV()
        {
            cbCategorias.SelectedIndex = -1;
            lvCategorias.Visibility = Visibility.Hidden;
            lvCategorias.IsEnabled = false;
            lvProdutos.Visibility = Visibility.Hidden;
            lvProdutos.IsEnabled = false;
        }
    }
}
