using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CostosGenericos
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal Importe { get; set; }
        public string Moneda { get; set; }
        public int TipoCosto { get; set; }
        public string NombreTipo { get; set; }
    }
}
