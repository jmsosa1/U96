using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class DetManteVh
    {
        int _iddet,_idmantevh,  _idcatemantevh, _cantidad, _idprove, _hs_mante, _otm, _itemotm ;
        string _descrimante, _nomcatemante, _razonsocial;
        decimal _costoitem, _km_mante;
        private DateTime? _fechafac;

        public int IdDet { get { return _iddet; } set { _iddet = value; } }
        public int IdManteVh { get { return _idmantevh; } set { _idmantevh = value; } }
        public int IdCateMante { get { return _idcatemantevh; } set { _idcatemantevh = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public string DescriMante { get { return _descrimante; } set { _descrimante = value; } }
        public decimal CostoItem { get { return _costoitem; } set { _costoitem = value; } }
        public string NomCateMante { get { return _nomcatemante; } set { _nomcatemante = value; } }
        public int IdProve { get { return _idprove; } set { _idprove = value; } }
        public string RazonSocial { get { return _razonsocial; } set { _razonsocial = value; } }
        public DateTime? FechaFac { get => _fechafac; set => _fechafac = value; }
        public decimal KmDetMante { get { return _km_mante; } set { _km_mante = value; } }
        public int HsDetMante { get { return _hs_mante; } set { _hs_mante = value; } }
        public int Otm { get { return _otm; } set { _otm = value; } }
        public int ItemOtm { get { return _itemotm ; } set { _itemotm= value; } }
        public DetManteVh()
        { }
    }
}
