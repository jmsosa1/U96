using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class NotaSahmV6
    {
        public int IdNota { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage ="El campo Titulo es requerido")]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="El campo es requerido")]
        public string Contenido { get; set; }
        public int IdEstado { get; set; }
        public int CantRespuesta { get; set; }
        public int CantLecturas { get; set; }
        public byte[] ImagenEstado { get; set; }
        public string NombreUsuario { get; set; }
        public byte[]ImagenUsuario { get; set; }
        public int Situacion { get; set; } // indica si la nota esta abierta o cerrada.
        //public int IdTipoNota { get; set; }  // indica el "sector" al que pertenece la nota


    }
}
