using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class plan_insp_detalle
    {
        public int Iddet { get; set; }
        public int Idplan { get; set; }

        [Required]
        [Display(Name ="Tarea")]
        public string Tarea { get; set; }

        [Required]
        [Display(Name ="Frecuencia")]
        public string Frecuencia { get; set; }

        public int Idcateplan { get; set; }

        //constructor
        public plan_insp_detalle()
        {

        }
    }
}
