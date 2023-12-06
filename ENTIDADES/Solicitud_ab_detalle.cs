using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ENTIDADES
{
   public class Solicitud_ab_detalle 
    {
        
        public int IdDetSol { get; set; }
        public int IdSol { get; set; }
        public int Cantidad { get; set; }
        public int NumItem { get; set; } 
        public string Observacion { get; set; }
        public string EstadoItem { get; set; }
        public string DescriItem { get; set; }

        public Solicitud_ab_detalle()
        { }
    }
}
