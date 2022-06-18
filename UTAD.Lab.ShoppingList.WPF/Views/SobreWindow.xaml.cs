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
    public partial class SobreWindow : Window
    {
        private App app;
        public SobreWindow()
        {
            InitializeComponent();

            app = App.Current as App;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            app._MainWindow.Show();
            this.Hide();
        }
    }
}
