using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Presupuesto_Item
    {
        public int Id { get; set; }
        public int IdPre { get; set; } // id del presupuesto con el cual se relaciona el detalle
        public int IdTipo { get; set; } // indica el tipo de item si es Vehiculo o Producto 
        public  int IdCate { get; set; } // indica la categoria a la que corresponde dentro del tipo de item elegido
        public DateTime F_alta { get; set; } // fecha de alta del item
        public decimal Monto_Presupuestado { get; set; } // monto total prespuestado para el item
        public decimal Monto_Aprobado { get; set; } // monto real aprobado para el item
        public decimal Monto_Real_Ejecutado { get; set; } // monto real ejecutado del item
        public decimal Porcentaje_aprobado { get; set; } // porcentaje aprobado del monto presupuestado para el item. Se registra en BD. Lo ingresa el usuario
        public decimal Porcentaje_ejecutado { get; set; } // porcentaje ejecutado real del monto aprobado. Campo calculado desde el monto real ejecutado
        public DateTime UltimaModificacion { get; set; } // fecha de la ultima modificacion
        public int CodOp { get; set; } // codigo de la operacion que se esta haciendo sobre el objeto 1=alta, 2=modificacion
        public string Operacion { get; set; } //  nombre de la operacion de la ultima modificacion
        public string NomCate { get; set; } // nombre la categoria del objeto
        public string NomTipo { get; set; } // nombre del tipo del objeto
        public int NumItem { get; set; }
       
    }
}
