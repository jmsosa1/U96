using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class StockTipoProducto
    {
        public int IdTipoP { get; set; }
        public string NombreTipo { get; set; }
        public decimal StockActual { get; set; }
        public decimal CostoStockActual { get; set; }
        public int IdDeposito { get; set; }

        public StockTipoProducto()
        {

        }
    }
}
