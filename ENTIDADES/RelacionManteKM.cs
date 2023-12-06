using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public  class RelacionManteKM
    {
        private byte[] _image_estado_temp;

        public int Idvh { get; set; }
        public string Dominio { get; set; }
        public string  Modelo { get; set; }
        public string Marca { get; set; }
        public decimal CostoManteAcu { get; set; }
        public decimal KmAcumulado { get; set; }
        public decimal Relacion { get; set; }
        public string Categoria { get; set; }
        public string AnioModelo { get; set; }
        public byte[] ImageEstado { get { return _image_estado_temp; } set { _image_estado_temp = value; } }

        public RelacionManteKM()
        {

        }
    }
}
