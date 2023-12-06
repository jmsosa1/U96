using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class RepuestoVh
    {
        

       
        public int IdRepuestoVh { get ;  set ;  }
        public string Descripcion { get ; set;  }
        public int Idvh { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string NumSerie { get; set; }
        public string CodInventario { get; set; }
        public decimal PrecioUnit { get; set; }
        public decimal CostoReposicion { get; set; }
        public string Marca { get; set; }

        public RepuestoVh()
        { }
    }
}
