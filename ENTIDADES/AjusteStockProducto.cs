using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class AjusteStockProducto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int IdDeposito { get; set; }
        public int IdCausa { get; set; }
        public string NomDeposito { get; set; }
        public string NomUsuario { get; set; }
        public string NomCausa { get; set; }

        public AjusteStockProducto()
        {

        }
    }
}
