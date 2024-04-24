using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ConsumoCombustible
    {
       

        public int IdCmb { get; set; }
        public int IdVh { get; set; }
        public decimal KmRecorrido { get; set; }
        public string Meskm { get; set; }
        public decimal LitrosCmbKM { get; set; }
        public decimal LitrosCmbHoras { get; set; }
        public decimal HorasTrabajo { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaConsumo { get; set; }
        public decimal CostoCmbHoras { get; set; }
        public decimal CostoCmbKm { get; set; }
        public int Imputacion { get; set; }
        public int AnioConsumo { get; set; }
        public decimal PrecioLitro { get; set; }
        public decimal CostoUnidadConsumo { get; set; }
        public decimal TotalCostoUnidadConsumo { get; set; }

        public ConsumoCombustible()
        { }

    }
}
