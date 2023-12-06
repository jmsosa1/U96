using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class ConsultaExistencias
    {
        public int IdProducto { get; set; }
        public int IdEmpleado { get; set; }
        public int DifCantidad { get; set; }
        public int Imputacion { get; set; }
        public string NombreProducto { get; set; }
        public string CodInventario { get; set; }
        public string NombreEmpleado { get; set; }
        public int IdDeposito { get; set; }
        public string NombreDeposito { get; set; }
        public decimal CantDisponible { get; set; }
        public ConsultaExistencias()
        {

        }
    }
}
