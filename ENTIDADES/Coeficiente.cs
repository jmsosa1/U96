using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Coeficiente
    {
        public int IdCof { get; set; }
        public string NomCof { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorMed { get; set; }
        public decimal ValorMax { get; set; }
    }
}
