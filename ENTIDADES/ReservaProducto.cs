using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ReservaProducto
    {
        //campos
        int _idreserva;
        int _idstkpro;
        int _idproducto;
        int _imputacion;
        int _idsector;
        int _estadoreserva;
        DateTime _altaf;
        DateTime _bajaf;
        int _cantireserva;

        //propiedades
        public int IdReserva { get { return _idreserva; } set { _idreserva = value; } }
        public int IdStkPro { get { return _idstkpro; } set { _idstkpro = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int IdSector { get { return _idsector; } set { _idsector = value; } }
        public int EstadoReserva { get { return _estadoreserva; } set { _estadoreserva = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public DateTime BajaF { get { return _bajaf; } set { _bajaf = value; } }
        public int CantiReserva { get { return _cantireserva; } set { _cantireserva = value; } }

        //constructor
        public ReservaProducto()
        { }
    }
}
