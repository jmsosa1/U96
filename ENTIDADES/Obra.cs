using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
   public class Obra
    {
        //campos privados
  
        private DateTime? _fechainicio;
        private DateTime? _fechafin, _bajaf;   
        //DateTime _altaf;


        // propiedades
        public int IdObra { get; set; }
        public int Imputacion { get; set; }
        public string NombreObra { get; set; }
        public string DireccionObra { get; set; }
        public int IdLocalidad { get; set; }
        public int IdProvincia { get;set; }
        public string Estado { get; set; }
     

        public DateTime? FechaInicio { get => _fechainicio; set => _fechainicio = value; }
        public DateTime? FechaFin { get => _fechafin; set => _fechafin = value; }
        public int IdCateObra { get; set; }

        public decimal ValorObra { get; set; }
        public string Cuit { get; set; } 
        public DateTime AltaF { get; set; }
        public DateTime? BajaF { get => _bajaf; set { _bajaf = value; } }
        public string Cliente { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
       
        public string CateObra { get; set; }

        public Obra()
        { }
    }
}
