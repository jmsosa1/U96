using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class LogOperaciones
    {
        

        //propiedades
        public int IdLog { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Tabla { get; set; }
        public int IdRegistro { get; set;  }
        public string CodigoOperacion { get; set; }

        //constructor 
        public LogOperaciones()
        { }

    }
}
