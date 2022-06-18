using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class Lista
    {
        public int id_Lista { get; set; }
        public string nome { get; set; }
        public List<ProdutoAux> produtos { get; set; }
    }
}