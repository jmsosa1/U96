using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class Proveedor
    {
       
        private DateTime?  _bajaf;
        


        //propiedades
        public int IdProve { get; set; }
        public int IdProvincia { get; set; }
        public int IdLocalidad { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Dir1 { get; set; }
        public decimal Cuit { get; set; }
        public string Dir2 { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Contacto { get; set; }
        public int IdRubro { get; set; }
        public int Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime Altaf { get; set; }
        public DateTime? BajaF { get => _bajaf; set { _bajaf = value; } }
        public int Atencion { get; set; }
        public int Plazo { get; set; }
        public int Precio { get; set; }
        public int Calidad { get; set; }
        public int Calificacion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Rubro { get; set; }
        public string CuitTexto { get; set; }
        //constructor
        public Proveedor()
        { }
    }
}
