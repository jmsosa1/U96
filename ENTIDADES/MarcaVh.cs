using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class MarcaVh
    {
        int _idmarca;
      
        string _nombremarca;

        public int IdMarca { get { return _idmarca; } set { _idmarca = value; } }
        public string NombreMarca { get { return _nombremarca; } set { _nombremarca = value; } }

        public MarcaVh()
        { }
    }
}
