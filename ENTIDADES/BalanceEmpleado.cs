using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class BalanceEmpleado
    {



        //propiedades autoimplementadas
        public int IdBe { get; set; } // id registro tabla balance
        public int IdEmpleado { get; set; } // id empleado
        public int IdProducto { get; set; } // id del producto
        public int IdTipoP { get; set; } // id de tipo de producto
        public int IdCateP { get; set; } // id categoria del producto
        public int IdLinea { get; set; } // id linea del producto
        public int Imputacion { get; set; } // imputacion de obra sector
        public int CantidadEntregada { get; set; } // cantidad acumulada del producto
        public int CantidadDevolucion { get; set; } //  cantidad acumulada de devolucion del producto
        public int CantidadDescuento { get; set; } // cantidad acumulada de descuento efectuados del producto
        public decimal CostoExistencia { get; set; } // valor costo acumulado del producto = precio unitario* cantidad entregada
        public decimal CostoDescuento { get; set; } // valor costo del descuento realizados = preciounitario * cantidad descuento
        public int DifCantidad { get; set; } //  diferencia entre cantidad entregada, devuelta y descontada
        public decimal CostoDevolucion { get; set; } // valor costo de la cantidad devuelta
        public decimal TarifaDiaria { get; set;  } // costo de alquiler diario de un producto, para aquellos que lo necesitan
        public int DiasAcumulado { get; set; } //  acumulacion de dias prestado o alquiler de un producto
        public int EstadoItem { get; set; }// indica si el item esta activo en funcion de la cantidad existencia
        public string NombreProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string ModeloProducto { get; set; }
        public string CodInventario { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string SerieProducto { get; set; }
        public string ClienteObra { get; set; }
        public string NomEstado { get; set; }


        //constructor
        public BalanceEmpleado()
        { }

    }
}
