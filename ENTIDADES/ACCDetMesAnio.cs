using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
  public class ACCDetMesAnio
    {
        //clase que contiene los resultados del consumo de combustible mensual en un año determinado
        public int MesDelAnio { get; set; } // numero del mes 
        public decimal CCMesAnio { get; set; } // costo en moneda del consumo del mes

        public decimal KmRegistrados { get; set; }
        public decimal HsRegistradas { get; set; }
        public decimal LtsConsumidosMes { get; set; }

        public ACCDetMesAnio()
        {

        }
    }
}
