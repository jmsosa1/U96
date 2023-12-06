using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
  public  class VisorDocObras
    {
        //clase que expone los documentos de egreso y devolucion de obras

        public int Cantidad { get; set; }
        public string CodIventario { get; set; }
        public string Concepto { get; set; }


        public string Descripcion { get; set; }

        public DateTime FechaRemito { get; set; }

        public int IdDetDocu { get; set; }
        public int IdDocumento { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpleado { get; set; }
        public int Imputacion { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }

        public string NumSerio { get; set; }
        public string NumDocumento { get; set; }
        public string NombreEmpleado { get; set; }
        public string NomProducto { get; set; }
        public string NombreObra { get; set; }

        public VisorDocObras()
        {

        }

    }
}
