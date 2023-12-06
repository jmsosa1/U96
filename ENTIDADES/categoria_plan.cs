using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ENTIDADES
{
    public class categoria_plan
    {

        public int Idcateplan { get; set; }

        [Required]
        [Display(Name ="Categoria")]
        public string Nombre_categoria { get; set; }

        //constructor
        public categoria_plan()
        {

        }
    }
}
