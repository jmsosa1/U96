using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class VhKmAvgCate
    {
        public int Idvh { get; set; }
        public string Modelo { get; set; }
        public string Dominio { get; set; }
        public string Marca { get; set; }
        public decimal CostoRepoDls { get; set; }
        public decimal PromedioConsumo { get; set; }
        public decimal PromedioMante { get; set; }
        public decimal PronosticoConsumo { get; set; }
        public decimal PronosticoMante { get; set; }
        public decimal KmAcumulado { get; set; }
        public int Reemplazo { get; set; }
    }
}
