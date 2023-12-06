using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class BalanceObraSector
    {
        //campos
        int _imputacion;
        int _idsector;
        decimal _costoacumulado, _preciounitario;
        int _idbo, _idproducto, _cantientregada, _idtipop, _idcatep, _idlineap;
       
        //propiedades
        public int IdBo { get { return _idbo; } set { _idbo = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int IdTipoP { get { return _idtipop; } set { _idtipop = value; } }
        public int IdCatep { get { return _idcatep; } set { _idcatep = value; } }
        public int IdLineaP { get { return _idlineap; } set { _idlineap = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int IdSector { get { return _idsector; } set { _idsector = value; } }
        public decimal CostoAcumulado { get { return _costoacumulado; } set { _costoacumulado = value; } }
        public decimal PrecioUnit { get { return _preciounitario; } set { _preciounitario = value; } }
        public int CantiEntregada { get { return _cantientregada; } set { _cantientregada = value; } }
       
        //constructor
        public BalanceObraSector()
        { }
    }
}
