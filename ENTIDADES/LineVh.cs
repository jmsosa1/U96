using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class LineVh
    {
        int _idlineavh, _idcatevh;
        string _nombrelinea;

        public int IdLineaVh { get { return _idlineavh; } set { _idlineavh = value; } }
        public string NomLineaVh { get { return _nombrelinea; } set { _nombrelinea = value; } }
        public int IdCateVh { get { return _idcatevh; } set { _idcatevh = value; } }

        public LineVh()
        { }
    }
}
