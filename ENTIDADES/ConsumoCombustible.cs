using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ConsumoCombustible
    {
        int _idcmb, _idvh;
        decimal  _litroscmb, _litroscmhoras, _kmrecorrido, _horastrabajo;
        int _imputacion, _anio;
        DateTime _fechareg;
        decimal _costocmbhoras, _costocmbkm , _preciolitro;
        string _meskm;

        public int IdCmb { get { return _idcmb; } set { _idcmb = value; } }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }
        public decimal KmRecorrido { get { return _kmrecorrido; } set { _kmrecorrido = value; } }
        public string Meskm { get { return _meskm; } set { _meskm = value; } }
        public decimal LitrosCmbKM { get { return _litroscmb; } set { _litroscmb = value; } }
        public decimal LitrosCmbHoras { get { return _litroscmhoras; } set { _litroscmhoras = value; } }
        public decimal HorasTrabajo { get { return _horastrabajo; } set { _horastrabajo = value; } }
        public DateTime FechaReg { get { return _fechareg; } set { _fechareg = value; } }
        public DateTime FechaConsumo { get; set; }
        public decimal CostoCmbHoras { get { return _costocmbhoras; } set { _costocmbhoras = value; } }
        public decimal CostoCmbKm { get { return _costocmbkm; } set { _costocmbkm = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int AnioConsumo { get { return _anio; } set { _anio = value; } }
        public decimal PrecioLitro { get { return _preciolitro; } set { _preciolitro = value; } }

        public ConsumoCombustible()
        { }

    }
}
