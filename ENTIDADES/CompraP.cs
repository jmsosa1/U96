using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CompraP
    {
        private DateTime? fechaCompra;
        private DateTime? fechaFactura;

        public int IdCompra { get; set; }
        public int IdProve { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Alta { get; set; }
        public DateTime? FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public DateTime? FechaFactura { get => fechaFactura; set => fechaFactura = value; }
        public string OC { get; set; }
        public string NumFactura { get; set; }
        public decimal ImporteCompra { get; set; }
        public string Observaciones { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreUsuario { get; set; }
        //propiedades solo usadas en el caso de la solapa de compras del producto solamanete usado para esa consulta
        public int CantidadUno { get; set; }
        public decimal PrecioUniUno { get; set; }
        public decimal TotalItemUno { get; set; }
        //
        public CompraP()
        {
            IdProve = 0;
            IdUsuario = 0;
            Alta = DateTime.Today;
            FechaCompra = null;
            FechaFactura = null;
            OC = "";
            NumFactura ="";
            ImporteCompra = 0;
            Observaciones = "";
            NombreProveedor = "";
            NombreUsuario = "";
            CantidadUno = 0;
            PrecioUniUno = 0;
            TotalItemUno = 0;
        }
    }
}
