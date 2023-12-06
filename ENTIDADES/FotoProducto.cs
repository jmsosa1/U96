using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class FotoProducto
    {
        public int IdFotoP { get; set; }
        public int IdProducto { get; set; }
        public DateTime AltaF { get; set; }
        public string DescriFoto { get; set; }
        public string Titulo { get; set; }
        public byte[] Foto { get; set; }

        public FotoProducto()
        {

        }
    }
}
