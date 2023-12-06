using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class VisorDocProductosCompras
    {
        //clase que expone los documentos de egreso y devolucion de productos comprados

        public int Cantidad { get; set; }
        
        public string Concepto { get; set; }



        public DateTime FechaRemProve { get; set; }
        public DateTime FechaRemito { get; set; }

        public int IdDetDocu { get; set; }
        public int IdDocumento { get; set; }
        public int IdProducto { get; set; }
       
        public int IdUsuario { get; set; }
        public int IdDepDestino { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }

        public string NumRemitoProve { get; set; }
        public string NomUsuario { get; set; }
        public string NomProducto { get; set; }
        public string NumFActura { get; set; }
        public string OCProve { get; set; }

        public int Registrado { get; set; }
        public VisorDocProductosCompras()
        {

        }

    }
}
