using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class StockCategoriaProducto
    {
        public int IdCategoria { get; set; }
        public string NombreCate { get; set; }
        public int IdTipoP { get; set; }
        public decimal StockActual { get; set; }
        public decimal CostoStock { get; set; }
        public string NombreTipo { get; set; }
        public decimal PuntoPedido { get; set; }

        public StockCategoriaProducto()
        {

        }
    }
}
