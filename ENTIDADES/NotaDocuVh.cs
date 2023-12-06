using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class NotaDocuVh
    {
        public int IdNota { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuario { get; set; }
        public string Contenido { get; set; }
        public int IdRegistro { get; set; } // id del registro de de la coleccion de documentos o plan de inspeccion con el que se relacionann las notas
        public int IdTipoNota { get; set; } //  tipo de notas que necesitamos mostrar : 1 documentacion vencida 2 plan inspeccion

    }
}
