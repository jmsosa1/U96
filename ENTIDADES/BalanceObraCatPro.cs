using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class BalanceObraCatPro
    {
        public int IdCateP { get; set; }
        public string NombreCateP { get; set; }
        public int CantEnviada { get; set; }
        public int CantDevolucion { get; set; }
        public decimal CostoItem { get; set; }
    }
}
