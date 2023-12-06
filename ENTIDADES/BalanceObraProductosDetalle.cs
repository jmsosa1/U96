using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class BalanceObraProductosDetalle: Producto
    {
        public int Imputacion { get; set; }
        public int CantEntregada { get; set; }
        public int CantDevolucion { get; set; }
        public int DifCantidad { get; set; }
        public decimal PrecioEnRemitoProducto { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
       



    }
}
