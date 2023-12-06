using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CostoCompraVehiculo
    {
        public int Mes { get; set; }
        public decimal Importe { get; set; }
        public int IdCate { get; set; }
        public string NombreCategoria { get; set; }
        public int Idvh { get; set; }
        public string Descripcion { get; set; }
        public string Dominio { get; set; }
        public string Marca { get; set; }
    }
}
