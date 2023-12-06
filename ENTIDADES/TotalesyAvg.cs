using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    //clase que contiene propiedades que almacenan y calculan totales y promedios

    public class TotalesyAvg
    {
        //propiedades de la clase
        public decimal CostoTotalConsumo { get; set; }
        public decimal TotalKmConsumo { get; set; }
        public decimal TotalHsConsumo { get; set; }
        public decimal TotalLtsConsumo { get; set; }
        public decimal AvgCostoConsumo { get; set; }
        public decimal AvgKmConsumo { get; set; }
        public decimal AvgLtsConsumo { get; set; }
        public decimal AvgHsConsumo { get; set; }

        public TotalesyAvg()
        {

        }
    }
}
