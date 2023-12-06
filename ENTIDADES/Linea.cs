using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    //clase que representa un objeto linea de producto
    public class Linea 
    {
        int _idlinea;
        string _nombre;
        int _idcatepro;

        public int IdLinea
        { get { return _idlinea; }
            set { _idlinea = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int IdCatepro
        {
            get { return _idcatepro; }
            set { _idcatepro = value; }

        }

        public Linea()
        { }
    }
}
