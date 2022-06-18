using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UTAD.Lab.ShoppingList.WPF.Models
{
    public class ModelListasCategorias
    {
        public List<Lista> Listas { get; private set; }
        public List<Categoria> Categorias { get; private set; }
        public List<Categoria> CategoriasPredefinidas { get; private set; }
        public List<Produto> Produtos { get; private set; }
        public List<Produto> ProdutosPredefinidos { get; private set; }
        public List<string> nomesProdutos { get; private set; }
        public List<string> nomesCategorias { get; private set; }
        public List<string> nomesListas { get; private set; }

        public ModelListasCategorias()
        {
            Listas = new List<Lista>();
            Categorias = new List<Categoria>();
            CategoriasPredefinidas = new List<Categoria>();
            Produtos = new List<Produto>();
            ProdutosPredefinidos = new List<Produto>();
            nomesCategorias = new List<string>(); 
            nomesProdutos = new List<string>();
            nomesListas = new List<string>();
        }
        // Lê as Categorias do ficheiro CategoriasXML.xml
        public void CarregaCategorias()
        {
            XDocument doc = XDocument.Load("CategoriasXML.xml");

            var cats = from al in doc.Descendants("Categoria") select al;

            foreach (var aux in cats)
            {
                Categoria c = new Categoria();
                c.id_Categoria = Int32.Parse(aux.Attribute("id_Categoria").Value);
                c.nome = aux.Attribute("nome").Value;
                c.observações = aux.Attribute("observações").Value;
                c.fixa = aux.Attribute("fixa").Value == "true";

               nomesCategorias.Add(aux.Attribute("nome").Value);
               Categorias.Add(c);
                if (c.fixa)
                    CategoriasPredefinidas.Add(c);
            }
        }
        // Lê os Produtos do ficheiro ProdutosXML.xml
        public void CarregaProdutos()
        {
            XDocument doc = XDocument.Load("ProdutosXML.xml");

            var prods = from al in doc.Descendants("Produto") select al;

            foreach (var aux in prods)
            {
                Produto p = new Produto();
                p.id_Produto = Int32.Parse(aux.Attribute("id_Produto").Value);
                p.id_Categoria = Int32.Parse(aux.Attribute("id_Categoria").Value);
                p.nome = aux.Attribute("nome").Value;
                p.descrição = aux.Attribute("descrição").Value;
                p.preço = double.Parse(aux.Attribute("preço").Value);
                p.unidade = aux.Attribute("unidade").Value;
                p.fixo = aux.Attribute("fixo").Value == "true";

                nomesProdutos.Add(aux.Attribute("nome").Value);
                Produtos.Add(p);
                if(p.fixo)
                    ProdutosPredefinidos.Add(p);
            }
            var lsts = from lst in doc.Root.Elements("Listas").Descendants("Lista") select lst;
        }
        // Lê as listas do ficheiro ListasXML.xml
        public void CarregaListas()
        {
            XDocument doc = XDocument.Load("ListasXML.xml");

            var lsts = from lst in doc.Root.Elements("Listas").Descendants("Lista") select lst;

            foreach (var aux in lsts)
            {
                Lista nova = new Lista();
                nova.id_Lista = int.Parse(aux.Attribute("id_Lista").Value);
                nova.nome = aux.Attribute("nome").Value;
                nova.produtos = new List<ProdutoAux>();

                var produtos = from al in aux.Descendants("Produto") select al;

                foreach (var tmp in produtos)
                {
                    ProdutoAux p = new ProdutoAux();
                    p.id_Produto = int.Parse(tmp.Attribute("id_Produto").Value);
                    p.nome = tmp.Attribute("nome").Value;
                    p.quantidade = double.Parse(tmp.Attribute("quantidade").Value);
                    p.comprado = tmp.Attribute("comprado").Value == "true";

                    nova.produtos.Add(p);
                }
                nomesListas.Add(nova.nome);
                Listas.Add(nova);
            }
        }
        public void CriaCategoria(int id_Categoria, string nome, string observações, bool fixa = false)
        {
            Categoria c = new Categoria { id_Categoria = id_Categoria, nome = nome, observações = observações};
            Categorias.Add(c);
            nomesCategorias.Add(c.nome.ToString());
        }
        public void CriaProduto(int id_Produto, int id_Categoria, string nome, string descrição, double preço, string unidade, bool fixo = false)
        {
            Produto p = new Produto { id_Produto = id_Produto, id_Categoria = id_Categoria, nome = nome, descrição = descrição, preço=preço, unidade=unidade, fixo=fixo };
            Produtos.Add(p);
            nomesProdutos.Add(p.nome.ToString());
        }
        public string GetCategoria(int id_Categoria)
        {
            foreach (Categoria cat in Categorias)
            {
                if (cat.id_Categoria == id_Categoria)
                {
                    return cat.nome;
                }
            }
            return "";
        }
        public int GetIDCategoria(string nome)
        {
            foreach (Categoria cat in Categorias)
            {
                if (cat.nome == nome)
                {
                    return cat.id_Categoria;
                }
            }
            return -1;
        }
        public int GetIDLista(string nome)
        {
            foreach (Lista lst in Listas)
            {
                if (lst.nome == nome)
                {
                    return lst.id_Lista;
                }
            }
            return -1;
        }
        public void AdicionaLista(int id_Lista, string nome, List<ProdutoAux> prods)
        {
            Lista l = new Lista();
            l.id_Lista = id_Lista;
            l.nome = nome;
            l.produtos = new List<ProdutoAux>();
            foreach (ProdutoAux aux in prods)
            {
                l.produtos.Add(aux);
            }
            Listas.Add(l);
            nomesListas.Add(l.nome);
        }
        // Guarda as Categorias existentes num ficheiro XML
        public void SaveCategorias()
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement("Categorias"));
            doc.Element("Categorias").Add(new XElement("Categoria"));

            XElement cats = doc.Root.Element("Categoria");

            foreach (Categoria aux in Categorias)
            {
                XElement no = new XElement("Categoria");
                no.Add(new XAttribute("id_Categoria", aux.id_Categoria));
                no.Add(new XAttribute("nome", aux.nome));
                no.Add(new XAttribute("observações", aux.observações));
                no.Add(new XAttribute("fixa", aux.fixa));

                cats.Add(no);
            }
            doc.Save("MyCategorias.xml");
        }
        // Guarda os Produtos existentes num ficheiro XML
        public void SaveProdutos()
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement("Produtos"));
            doc.Element("Produtos").Add(new XElement("Produto"));

            XElement cats = doc.Root.Element("Produto");

            foreach (Produto aux in Produtos)
            {
                XElement no = new XElement("Produto");
                no.Add(new XAttribute("id_Produto", aux.id_Produto));
                no.Add(new XAttribute("id_Categoria", aux.id_Categoria));
                no.Add(new XAttribute("nome", aux.nome));
                no.Add(new XAttribute("descrição", aux.descrição));
                no.Add(new XAttribute("preço", aux.preço));
                no.Add(new XAttribute("unidade", aux.unidade));
                no.Add(new XAttribute("fixo", aux.fixo));

                cats.Add(no);
            }
            doc.Save("MyProdutos.xml");
        }
        // Guarda as Listas existentes num ficheiro XML
        public void SaveListas()
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement("ListasCompras"));
            doc.Element("ListasCompras").Add(new XElement("Listas"));

            XElement lists = doc.Root.Element("Listas");

            foreach (Lista aux in Listas)
            {
                XElement no = new XElement("Lista");

                foreach (ProdutoAux p in aux.produtos)
                {
                    XElement tmpProd = new XElement("Produto");
                    tmpProd.Add(new XAttribute("id_Produto", p.id_Produto));
                    tmpProd.Add(new XAttribute("nome", p.nome));
                    tmpProd.Add(new XAttribute("quantidade", p.quantidade));
                    tmpProd.Add(new XAttribute("comprado", p.comprado));

                    no.Add(tmpProd);
                }
                lists.Add(no);
            }
            doc.Save("MyListas.xml");
        }
    }
}
