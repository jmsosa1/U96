using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class DocumentoDetalle
    {

        public int CodigoItem { get; set; }
        public int CantidadItem { get; set; }
        public string CodIventario { get; set; }

        public string Descripcion { get; set; }

        public int EstadoItem { get; set; }

        public int IdDetDocu { get; set; }
        public int IdDocumento { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }

        public string NumSerio { get; set; }

        public decimal PrecioItem { get; set; }

        public decimal StockDisponible { get; set; }
        
        public int TipoItem { get; set; }  // indica si es un producto(1)  o un vehiculo(2) 
        
       
       
       
        
        
        
        public DocumentoDetalle()
        {

        }
    }
}
