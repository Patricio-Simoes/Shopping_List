using UTAD.Lab.ShoppingList.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UTAD.Lab.ShoppingList.WPF.Models;

namespace UTAD.Lab.ShoppingList.WPF
{
    public partial class App : Application
    {
        // Views

        public MainWindow _MainWindow { get; set; }
        public VerProdutoWindow _VerProdutoWindow { get; set; }
        public EditarProdutoWindow _EditarProdutoWindow { get; set; }
        public RemoverProdutoWindow _RemoverProdutoWindow { get; set; }
        public CriarProdutoWindow _CriarProdutoWindow { get; set; }
        public CriarCategoriaWindow _CriarCategoriaWindow { get; set; }
        public EditarCategoriaWindow _EditarCategoriaWindow { get; set; }
        public RemoverCategoriaWindow _RemoverCategoriaWindow { get; set; }
        public VerCategoriaWindow _VerCategoriaWindow { get; set; }
        public SobreWindow _SobreWindow { get; set; }
        public CriarListaWindow _CriarListaWindow { get; set; }
        public EditarListaWindow _EditarListaWindow { get; set; }
        public VerListaWindow _VerListaWindow { get; set; }
        public CategoriasPredefinidasWindow _CategoriasPredefinidasWindow { get; set; }
        public CategoriasExistentesWindow _CategoriasExistentesWindow { get; set; }

        // Models

        public ModelListasCategorias _ModelListasCategorias { get; set; }

        public App()
        {
            // Models

            _ModelListasCategorias = new ModelListasCategorias();

            // Views

            _MainWindow = new MainWindow();
            _VerProdutoWindow = new VerProdutoWindow();
            _EditarProdutoWindow = new EditarProdutoWindow();
            _RemoverProdutoWindow = new RemoverProdutoWindow();
            _CriarProdutoWindow = new CriarProdutoWindow();
            _CriarCategoriaWindow = new CriarCategoriaWindow();
            _EditarCategoriaWindow = new EditarCategoriaWindow();
            _RemoverCategoriaWindow = new RemoverCategoriaWindow();
            _VerCategoriaWindow = new VerCategoriaWindow();
            _SobreWindow = new SobreWindow();
            _CriarListaWindow = new CriarListaWindow();
            _EditarListaWindow = new EditarListaWindow();
            _VerListaWindow = new VerListaWindow();
            _CategoriasPredefinidasWindow = new CategoriasPredefinidasWindow();
            _CategoriasExistentesWindow = new CategoriasExistentesWindow();

            // Chamada de funções

            _ModelListasCategorias.CarregaCategorias();
            _ModelListasCategorias.CarregaProdutos();
            _ModelListasCategorias.CarregaListas();
        }
    }
}
