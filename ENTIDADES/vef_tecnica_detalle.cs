using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class Vef_Tecnica_Detalle
    {
        public int IdDet { get; set; }
        public int IdVef { get; set; }

        [Required]
        [Display(Name ="Inspeccion")]
        public string Tipo_Inspeccion { get; set; }

        [Required]
        [Display(Name ="Elemento")]
        public string Descri_Elemento { get; set; }

        public string Estado_Elemento { get; set; }
        public string Obs_Elemento { get; set; }

        public Vef_Tecnica_Detalle()
        {

        }
    }
}
