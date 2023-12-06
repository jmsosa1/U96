using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class BajaProductos
    {
        public int IdBaja { get; set; }
        public int IdProducto { get; set; }
        public int IdCausaBaja { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaBaja { get; set; }
        public int IdEmpleado { get; set; }
        public int Imputacion { get; set; }
        public int IdUsuario { get; set; }

        public BajaProductos()
        {

        }
    }
}
