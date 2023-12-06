using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class StockProducto
    {
        //clase que representa la relacion entre  el stock de un producto en un deposito determinado
        public int IdStk { get; set; }
        public int IdProducto { get; set; }
        public int IdDeposito { get; set; }
        public decimal StkActual { get; set; } // stock actual del producto
        public decimal StkReservado { get; set; } // stock reservado actual del producto
        public decimal StkDisponible { get; set; } // stock disponible del producto - este es el que se ve en el selector
        public decimal PuntoPedido { get; set; } // punto de pedido del producto, cantidad donde se dispara el pedido de compras
        public int ControlPP { get; set; } // indicador de si el producto controla punto de pedido
        public decimal CostoStk { get; set; } // costo , en moneda , del stock total del producto en deposito determinado
        public string Estante { get; set; } // codigo del estante donde se encuentra el producto
        public string Fila { get; set; } // codigo de la fila donde se encuentra el producto
        public string Columna { get; set; } // codigo de la columna donde se encuentra  el producto
        public string Frente { get; set; } // codigo del frente donde se encuentra el producto
        public string NombreProducto { get; set; } // nombre del producto  - selector
        public string ModeloProducto { get; set; } // modelo del producto - selector
        public string CodInventario { get; set; } // codigo de inventario del producto - selector 
        public string NumSerieProducto { get; set; } // numero de serie del producto -selector
        public string Marca { get; set; } // marca del producto - selector
        public string Deposito { get; set; } // nombre del deposito donde se encuentra el producto
        public string TipoProducto { get; set; }
        public string Categoria { get; set; }
        public string SituacionStock { get; set; }
        public int IdSegmento { get; set; } // uso exclusivo en detalle de entregas realizadas - gestion del stock

        public StockProducto()
        {

        }
    }
}
