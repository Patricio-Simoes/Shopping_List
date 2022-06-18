using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class Produto
    {
        public int id_Produto { get; set; }
        public int id_Categoria { get; set; }
        public string nome { get; set; }
        public string descrição { get; set; }
        public double preço { get; set; }
        public string unidade { get; set; }
        public bool fixo { get; set; }
    }
}
