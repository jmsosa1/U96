using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class RespuestaNota
    {

        public int IdRespuesta { get; set; }
        public int IdNota { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        //public int IdEstado { get; set; }
        public int IdUsaurio { get; set; }
        public string NombreUsuarioResp { get; set; }
    }
}
