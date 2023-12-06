using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class SituacionOperativaVh:RelacionManteHs
    {
        public int DiasAcumulados { get; set; }
        public int DiasMante { get; set; }
        public int DiasParo { get; set; }
        public int DiasUso { get; set; }
        public int IdCate { get; set; }
        public decimal CtDm { get; set; }  // costo total de los dias de mantenimiento
        public decimal CtDP { get; set; } // costo total de los dias de paro
        public decimal CostoDiarioParo { get; set; } // costo individual de  un dia de paro o mantenimiento
        public decimal CostoDiarioUso { get; set; } // costo individual de un dia de uso del vehiculo
       
        public decimal TotalCDP { get; set; } // suma de CtDM + CdDP
        public decimal TotalCDU { get; set; }

        
        public SituacionOperativaVh()
        {
        }

    }
}
