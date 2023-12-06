using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Provincia
    {
        //campos
        int _idprovincia;
        string _nombre;
        
        //propiedades
        public int IdProvincia { get { return _idprovincia; } set { _idprovincia = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
       

        // constructor
        public Provincia()
        { }
    }
}
