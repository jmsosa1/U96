using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class BalanceObraProductos
    {
        //esta clase contiene el resultado de los tipos , cantidad y costo de los tipos de productos enviados a una obra
        public int IdTipoPro { get; set; }
        public int CantEntregada { get; set; }
        public decimal CostoEntregas { get; set; }
        public string NombreTipo { get; set; }
        public int CantProDistintos { get; set; }
    }
}
