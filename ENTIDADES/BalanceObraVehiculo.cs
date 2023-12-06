using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class BalanceObraVehiculo
    {
         int _idbov, _idvh, _idcatevh, _idlineavh, _horastrabajo, _mantenimientos,_chofer;
        int _imputacion, _diastrabajo;
         DateTime _fechaingreso, _fechasalida;
         decimal _costohoras, _costomantenimientos;
        

        public int IdBov { get { return _idbov; } set { _idbov = value; } }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }    
        public int IdCateVh { get { return _idcatevh; } set { _idcatevh = value; } }
        public int IdLineaVh { get { return _idlineavh; } set { _idlineavh = value; } }
        public int HorasTrabajo { get { return _horastrabajo; } set { _horastrabajo = value; } }
        public int Mantenimientos { get { return _mantenimientos; } set { _mantenimientos = value; } }
        public int Chofer { get { return _chofer; } set { _chofer = value; } }
        public DateTime FechaIngreso { get { return _fechaingreso; } set { _fechaingreso = value; } }
        public DateTime FechaSalida { get { return _fechasalida; } set { _fechasalida = value; } }
        public decimal CostoHoras { get { return _costohoras; } set { _costohoras = value; } }
        public decimal CostoMantenimientos { get { return _costomantenimientos; } set { _costomantenimientos = value; }}
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int DiasTrabajo { get { return _diastrabajo; } set { _diastrabajo = value; } }

        public BalanceObraVehiculo()
        { }
    }
}
