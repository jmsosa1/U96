using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class RMAProducto
    {
        private DateTime? bajaF;

        public int IdRma { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int Idtransporte { get; set; }
        public DateTime AltaF { get; set; }
        public DateTime? BajaF { get => bajaF; set => bajaF = value; }
        public string CausaRma { get; set; }
        public int idestadoRma { get; set; }
        public string NombreEstado { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string CodInventario { get; set; }
        public string Serie { get; set; }
        public string NombrProveedor { get; set; }
        public string NombreTransporte { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserCumple { get; set; }
        public string  UserCrea { get; set; }
        public string UserCumple { get; set; }
        public int Imputacion { get; set; }

        public RMAProducto()
        {

        }
    }
}
