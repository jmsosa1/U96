using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ProductoProve
    {
        int _idprove, _idproducto, _idtipop, _idcategoria, _idlinea;
        decimal _precioultimacompra;
        DateTime _fechaultimacompra;

        public int IdProve { get { return _idprove; } set { _idprove = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int IdTipop { get { return _idtipop; } set { _idtipop = value; } }
        public int IdCategoria { get { return _idcategoria; } set { _idcategoria = value; } }
        public int Idlinea { get { return _idlinea; } set { _idlinea = value; } }
        public decimal PrecioUltimaCompra { get { return _precioultimacompra; } set { _precioultimacompra = value; } }
        public DateTime FechaUltimaCompra { get { return _fechaultimacompra; } set { _fechaultimacompra = value; } }

        public ProductoProve()
        { }
    }
}
