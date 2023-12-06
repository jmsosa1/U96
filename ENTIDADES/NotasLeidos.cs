using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class NotasLeidos
    {
        public int IdNota { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaLectura { get; set; }
        public string NombreUsuario { get; set; }
    }
}
