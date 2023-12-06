using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Combustible
    {
        int _idcombustible;
        string _descripcion;
        decimal _preciolitroactual, _preciolitroanterior;
        DateTime _ultimamodi;

        public int IdCombustible { get { return _idcombustible; } set { _idcombustible = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public decimal PrecioLitroActual { get { return _preciolitroactual; } set { _preciolitroactual = value; } }
        public decimal PrecioLitroAnterior { get { return _preciolitroanterior; } set { _preciolitroanterior = value; } }
        public DateTime UltimaModi { get { return _ultimamodi; } set { _ultimamodi = value; } }

        public Combustible()
        {}
    }
}
