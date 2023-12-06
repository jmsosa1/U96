using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Presupuesto_Item_Vh : Presupuesto_Item
    {
        public int TipoABM { get; set; } // 0 , es alta, 1 es modificacion, 2 baja
       public Presupuesto_Item_Vh() 
        {
            F_alta = DateTime.Today.Date;
            CodOp = 1;
            Porcentaje_aprobado = 0;
            Porcentaje_ejecutado = 0;
            Monto_Presupuestado = 0;
            Monto_Real_Ejecutado = 0;
            Monto_Aprobado =0;
            UltimaModificacion = DateTime.Today.Date;
            IdPre = 0;
            IdTipo = 0;
            IdCate = 0;
            NomCate = "";
            NomTipo = "";
            Operacion = "";
            TipoABM = 0;
        }
    }
}
