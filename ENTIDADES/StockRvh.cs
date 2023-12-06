using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class StockRvh
    {
        int _idstkrvh, _idproducto, _idtipop, _idcatep, _idlineap;
        int _iddeposito, _stkactual, _stkreservado;
        decimal _costostk;

        public int IdStkRvh { get { return _idstkrvh; } set { _idstkrvh = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int IdTipoP { get { return _idtipop; } set { _idtipop = value; } }
        public int IdCateP { get { return _idcatep; } set { _idcatep = value; } }
        public int IdLineaP { get { return _idlineap; } set { _idlineap = value; } }
        public int IdDeposito { get { return _iddeposito; } set { _iddeposito = value; } }
        public int StkActual { get { return _stkactual; } set { _stkactual = value; } }
        public int StkReservado { get { return _stkreservado; } set { _stkreservado = value; } }
        public decimal CostoStk { get { return _costostk; } set { _costostk = value; } }


        public StockRvh()
        { }
    }
}
