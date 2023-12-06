using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ConsumoVhMes
    {
        public string Dominio { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public decimal KmRecorrido { get; set; }
        public decimal HsTrabajo { get; set; }
        public decimal CCMBKM { get; set; }
        public decimal CCMBHS { get; set; }
        public string Mes { get; set; }
        public int AnioConsumo { get; set; }

    }
}
