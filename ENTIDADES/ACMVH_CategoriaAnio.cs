using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public  class ACMVH_CategoriaAnio
    {
        //clase que se usa para mostrar el costo total de mantenimientos de vehiculos ordenados por categoria de mantenimiento
        //en el resumen grafico de resultados
        public int IdCateManteVh { get; set; }
        public string NombreCategoria { get; set; }
        public int CantidadIncidencias { get; set; }
        public decimal CostoTotalCategoria { get; set; }
        public decimal CostoPromedioCategoria { get; set; }

        public ACMVH_CategoriaAnio()
        {

        }
    }
}
