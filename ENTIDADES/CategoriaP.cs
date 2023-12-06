using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CategoriaP
    {
        public int IdCateP { get; set; }
        public int IdTipoP { get; set; }
        public string NomCateP { get; set; }
        public decimal CostoReposicion { get; set; }
        public decimal StockActual { get; set; }

        public CategoriaP()
        {
            
        }
    }
}
