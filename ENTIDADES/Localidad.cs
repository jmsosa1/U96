using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
     public class Localidad
    {
        //campos
        int _idlocalidad;
        int _idprovincia;
        string _nombre;
        string _nomprovincia;
        string _cp;
        string _departamento;
        decimal _longitud;
        decimal _latitud;

        //propiedades
        public int IdLocalidad { get { return _idlocalidad; } set { _idlocalidad = value; } }
        public int IdProvincia { get { return _idprovincia; } set { _idprovincia = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string CP { get { return _cp; } set { _cp = value; } }
        public string Departamento { get { return _departamento; } set { _departamento = value; } }
        public decimal Longitud { get { return _longitud; } set { _longitud = value; } }
        public decimal Latitud { get { return _latitud; } set { _latitud = value; } }
        public string Provincia { get { return _nomprovincia; } set { _nomprovincia = value; } }
        //constructor
        public Localidad()
        { }
    }
}
