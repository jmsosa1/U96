using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class BalanceObraTiposVehiculo
    {
        //esta clase contiene el resultado de las categorias, cantidad vehiculos asignados a una obra
        public int IdCateVh { get; set; }
        public string NombreCateVh { get; set; }
        public int CantidadAsignada { get; set; }
    }
}
