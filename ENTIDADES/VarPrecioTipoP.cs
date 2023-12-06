using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class VarPrecioTipoP 
    {
        //clase que sirve para representar las variaciones de precios de los tipos de producto en el tiempo
        public int Id { get; set; }
        public int IdTipoP { get; set; } // identificador del tipo de producto
        public DateTime FechaVar { get; set; } // fecha ultima variacion
        public decimal Porcentaje { get; set; } // valor de porcentaje de variacion

        public VarPrecioTipoP()
        {

        }
    }
}
