using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class Categoria
    {
        public int id_Categoria { get; set; }
        public string nome { get; set; }
        public string observações { get; set; }
        public bool fixa { get; set; }
    }
    
}
