using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    //clase que representa objetos del tipo mantenimiento de un producto

    public class Mante_P
    {
        private DateTime? fechaFactura;
        private DateTime? fechaRemito;
       
        public int IdManteP { get; set; }
        public int IdProducto { get; set; }
        public DateTime AltaF { get; set; }
       
        public int IdProve { get; set; }
        public string RazonSocial { get; set; }
        public int Imputacion { get; set; }
        public string NumFacturaProve { get; set; }
        public string NumRemitoProve { get; set; }
        public string OCProve { get; set; }
        public DateTime? FechaRemito { get => fechaRemito; set => fechaRemito = value; }
        public DateTime? FechaFactura { get => fechaFactura; set => fechaFactura = value; }
        public int IdOtm { get; set; }
        public decimal ImporteFactura { get; set; }
        public string DetalleMante { get; set; }
        public string ClienteObra { get; set; }
        public int IdUsuario { get; set; }
        public string NomUsuario { get; set; }
        public string NombreProducto { get; set; }
        public string CodInventario { get; set; }
        public int IdRma { get; set; }
        public Mante_P()
        {


        }
    }
}
