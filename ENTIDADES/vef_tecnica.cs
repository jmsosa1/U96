using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class Vef_Tecnica
    {
        public int IdVef { get; set; }
        public int IdVh { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpleado { get; set; }

        [Display(Name ="Fecha Verificacion")]
        public DateTime Fecha_Chek { get; set; }
        public DateTime Fecha_Entrega { get; set; }

        [Required]
        [Display(Name ="Obra/Sector")]
        public int Imputacion { get; set; }

        [Display(Name ="Horas")]
        public int Horas_Uso { get; set; }
        [Display(Name ="Km")]
        public decimal Km_Uso { get; set; }


        public Vef_Tecnica()
        {

        }
    }
}
