using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cajon
    {
        //campos
        int _idcajon, _idproducto, _cantidad_base, _idtipop,_idcatep,_idlineap;
        decimal _costoacumulado, _preciounitario;
        DateTime _ultima_modificacion;
        int _cantidad_agregada;
        int _total_cantidad;
        
        //propiedades
        public int IdCajon { get { return _idcajon; } set { _idcajon = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int IdTipoP { get { return _idtipop; } set { _idtipop = value; } }
        public int IdCatep { get { return _idcatep; } set { _idcatep = value; } }
        public int IdLineaP { get { return _idlineap; } set { _idlineap = value; } }
        public decimal CostoAcumulado { get { return _costoacumulado; } set { _costoacumulado = value; } }
        public DateTime UltimaModificacion { get { return _ultima_modificacion; } set { _ultima_modificacion = value; } }
        public int CantidadBase { get { return _cantidad_base; } set { _cantidad_base = value; } }
        public int CantidadAgregada { get { return _cantidad_agregada; } set { _cantidad_agregada = value; } }
        public int TotalCantidad { get { return _total_cantidad; } set { _total_cantidad = value; } }
        public decimal PrecioUnitario { get { return _preciounitario; } set { _preciounitario = value; } }
        //constructor
        public Cajon()
        { }
    }

}
