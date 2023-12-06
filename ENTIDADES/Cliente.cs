using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class Cliente
    {
        //campos privados
        
        string _razonsocial;
        int _cuit, _idcliente;
        string _contacto;

       
        //propiedades
        public int IdCliente { get { return _idcliente; } set { _idcliente = value; } }
        public string RazonSocial { get { return _razonsocial; } set { _razonsocial = value; } }
        public int Cuit { get { return _cuit; }
            set { _cuit = value;} }
        public string Contacto { get { return _contacto; } set { _contacto = value; } }
        //constructor
        public Cliente()
        { }
    }
}
