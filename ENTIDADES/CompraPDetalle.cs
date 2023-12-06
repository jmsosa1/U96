using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CompraPDetalle
    {
        private DateTime? fechaRemito;



        public int IdDet { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int IdRemito { get; set; } // id del documento con el cual se registro el ingreso del material al deposito
        public string RemitoProveedor { get; set; }
        public int Cantidad { get; set; } // cantidad del producto ingresada en el documento asociada
        public decimal PrecioItem { get; set; } // precio unitario del item,debe ingresarse a mano
        public decimal TotalItem { get; set; } // campo calculado en funcion de la cantidad y el precio unitario
        public string NomProducto { get; set; }
        public DateTime? FechaRemito { get => fechaRemito; set => fechaRemito = value; } // alta del registro en sistema
        public string Deposito { get; set; }
        public string MarcaProducto { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaIngreso { get; set; }

        public CompraPDetalle()
        {

        }

    } 

    
}
