using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class Monedas
    {
        //clase que referencia a las distintas monedas que maneja el sistema , en este caso dos , dolar y peso
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public string Simbolo { get; set; } // simbolo de la moneda
        public int MesValor { get; set; }
        public int AnioValor { get; set; }
        public decimal Variacion { get; set; }
        public decimal Valor { get; set; } // valor de la moneda respecto del peso en esa fecha
    }
}
