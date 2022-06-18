using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class ProdutoAux
    {
        public int id_Produto { get; set; }
        public string nome { get; set; }
        public double quantidade { get; set; }
        public bool comprado { get; set; }
    }
}
