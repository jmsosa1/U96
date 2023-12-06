using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.ComponentModel;

namespace ENTIDADES
{
    public class Empleado 
    {
      
         
        //DateTime _altaf;
        //private DateTime? _bajaf;

       

        // propiedades 
        public int IdEmpleado { get; set; }
        public int Dni { get; set; }
        public int IdLocalidad { get; set; }
        public int IdProvincia { get; set; }
        public string TCamisa { get; set; }
        public string TPantalon { get; set; }
        public string TCalzado { get; set; }
        public int IdSector { get; set; }
        public int IdCatEmpleado { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
           
        public string Direccion { get; set; }
           
        public string Email { get; set; }
        public string TeParticular { get; set; }
        public string TeCelular { get; set; }
        public string Gremio { get; set; }
        public string Nota { get; set; }
        public DateTime AltaF { get; set; }
        public DateTime? BajaF { get; set; }
        public byte[] Foto { get; set; }
        public string Estado { get; set; }
        public string NomCategoria { get; set; }
        public string NomSector { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
     

        //constructor 
        public Empleado()
        {
          

        }
      
    }
}
