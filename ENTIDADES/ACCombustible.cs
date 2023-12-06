using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ACCombustible
    {
        //clase que contiene los valores calculados del consumo de combustible general anual para toda la flota
        public decimal CostoTotalConsumoAnual { get; set; } // @total
        public int CantidadHoras { get; set; }//@totalcantidadhoras
        public int CantidadKm { get; set; } // @totalcantidadkm
        public int LitrosHoras { get; set; } // @totallitroshoras
        public int LitrosKm { get; set; } // @totallitroskm
        public decimal CostoHoras { get; set; } //@costohoras
        public decimal CostoKm { get; set; } // @costokm
        public decimal AvgHoras { get; set; } // @consumomediohoras
        public decimal AvgKm { get; set; } //@consumomediokm

        public ACCombustible()
        {

        }
    }
}
