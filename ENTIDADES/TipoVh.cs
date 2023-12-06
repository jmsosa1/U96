using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TipoVh
    {
        int _idtipovh;
        string _nomtipo;

        public int IdTipoVh { get { return _idtipovh; } set { _idtipovh = value; } }
        public string NomTipo { get { return _nomtipo; } set { _nomtipo = value; } }

        public TipoVh()
        { }
    }
}
