using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Mante_vh
    {
        //campos privados
        int _idmantevh, _idvh,_idprove,  _horasmante, _imputacion, _idempleado;
        string _numfactura, _numremito, _ordencompra, _tipomante, _dominio, _nomempleado, _nomprove;
        string _marca, _modelo;
        private  DateTime? _fechafac, _fecharem;
        DateTime  _altaf;
        decimal _importe , _kmmante;

        //propiedades

       public int IdManteVh { get { return _idmantevh; } set { _idmantevh = value; } }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }
        public int IdProve { get { return _idprove; } set { _idprove = value; } }
        
        public decimal KmMante { get { return _kmmante; } set { _kmmante = value; } }
        public int HorasMante { get { return _horasmante; } set { _horasmante = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public string NumFactura { get { return _numfactura; } set { _numfactura = value; } }
        public string NumRemito { get { return _numremito; } set { _numremito = value; } }
        public string OrdenCompra {get{ return _ordencompra; } set { _ordencompra = value; } }
        public string TipoMante { get { return _tipomante; } set { _tipomante = value; } }
        public DateTime? FechaFac { get => _fechafac; set => _fechafac = value; }
        public DateTime? FechaRem { get => _fecharem; set => _fecharem = value; }
        public decimal Importe { get { return _importe; } set { _importe = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public string NombreEmpleado { get { return _nomempleado; } set { _nomempleado = value; } }
        public string NombreProve { get { return _nomprove; } set { _nomprove = value; } }
        public string Dominio { get { return _dominio; } set { _dominio = value; } }
        public string Marca { get { return _marca; } set { _marca = value; } }
        public string Modelo { get { return _modelo; } set { _modelo = value; } }

        // constructor

        public Mante_vh()
        { }
    }

}
