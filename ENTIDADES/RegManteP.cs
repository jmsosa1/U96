using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class RegManteP
    {
        //campos 
        int _idregmante;
        string _remprove,_facprove, _ocprove;
        string _tipomantenimiento;
        int _idproducto;
        int _imputacion;
        int _idsector, _idusuario, _estadoreg, _idtiporem;
        DateTime _fecharemprove, _altaf, _bajaf, _fechafacprove;
        decimal _importefactura;

        //propiedades
        public int IdRegMante { get { return _idregmante; } set { _idregmante = value; } }
        public string RemProve { get { return _remprove; } set { _remprove = value; } }
        public string FacProve { get { return _facprove; } set { _facprove = value; } }
        public string OcProve { get { return _ocprove; } set { _ocprove = value; } }
        public string TipoMantenimiento { get { return _tipomantenimiento; } set { _tipomantenimiento = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int IdSector { get { return _idsector; } set { _idsector = value; } }
        public int Idusuario { get { return _idusuario; } set { _idusuario = value; } }
        public int EstadoReg { get { return _estadoreg; } set { _estadoreg = value; } }
        public int IdTipoRem { get { return _idtiporem; } set { _idtiporem = value; } }
        public DateTime FechaRemProve { get { return _fecharemprove; } set { _fecharemprove = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public DateTime BajaF { get { return _bajaf; } set { _bajaf = value; } }
        public DateTime FechaFacProve { get { return _fechafacprove; } set { _fechafacprove = value; } }
        public decimal ImporteFactura { get { return _importefactura; } set { _importefactura = value; } }
        //constructor
        public RegManteP()
        { }
    }
}
