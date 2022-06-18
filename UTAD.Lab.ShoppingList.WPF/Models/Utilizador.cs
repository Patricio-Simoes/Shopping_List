using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class Utilizador
    {
        public int id_Utilizador { get; set; }
        public string nome { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string pais { get; set; }
        public string foto { get; set; }
    }
}
