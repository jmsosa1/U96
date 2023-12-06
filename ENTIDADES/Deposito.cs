using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class Deposito
    {
        private DateTime? bajaF;

        public int IdDeposito { get; set; }
        public string NomDepo { get; set; }
        public string Direccion { get; set; }
        public string Responsable { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IdLocalidad { get; set; }
        public int IdProvincia { get; set; }
        public int Estado { get; set; }
        public DateTime AltaF { get; set; }
        public DateTime? BajaF { get => bajaF; set => bajaF = value; }
        public string NomLocalidad { get; set; }
        public string NomProvincia { get; set; }

        public Deposito()
        {

        }
    }
}
