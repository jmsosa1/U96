using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class RepuestoProve
    {
        int _idrepuestovh, _idprove;
        decimal _precioultimacompra;
        DateTime _fechaultimacompra;

        public int IdRepuestoVh { get { return _idrepuestovh; } set { _idrepuestovh = value; } }
        public int IdProve { get { return _idprove; } set { _idprove = value; } }
        public decimal PrecioUltimaCompra { get { return _precioultimacompra; } set { _precioultimacompra = value; } }
        public DateTime FechaUltimaCompra { get { return _fechaultimacompra; } set { _fechaultimacompra = value; } }

        public RepuestoProve()
        { }
    }
}
