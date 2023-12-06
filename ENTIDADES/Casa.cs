using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class Casa
    {
        private DateTime? bajaF;
        private DateTime altaF;
        private DateTime? finAlquiler;
        private DateTime? inicioAlquiler;

        //campos 



        //propiedades
        public int IdCasa { get; set; }
        public int IdLocalidad { get; set; }
        public  int IdProvincia { get; set; }
        public int Estado { get; set; }

        public DateTime? InicioAlquiler { get => inicioAlquiler; set => inicioAlquiler = value; }
        public DateTime? FinAlquiler { get => finAlquiler; set => finAlquiler = value; }

        public DateTime AltaF { get => altaF; set => altaF = value; }
        public DateTime? BajaF { get => bajaF; set => bajaF = value; }
        public int Imputacion { get; set; }

        public decimal CostoAlquiler { get; set; }
        public decimal CostoAcumulado { get; set; }
        public int Activo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Nota { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string CP { get; set; }
        //constructor
        public Casa()
        { }
    }
}
