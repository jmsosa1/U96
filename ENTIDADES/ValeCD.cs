using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ValeCD
    {
        int _idvalecd, _idempleado, _idsector, _imputacion, _idcausabaja, _iddeposito,_estadoreg;
        DateTime _fechavale;
        string _tipovale, _observacion,_nomdepo, _nomobra, _nomempleado;
        //decimal _totalvale;
        
        public int IdValecd { get { return _idvalecd; } set { _idvalecd = value; } }
        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public int IdSector { get { return _idsector; } set { _idsector = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int IdCausaBaja { get { return _idcausabaja; } set { _idcausabaja = value; } }
        public int IdDepositp { get { return _iddeposito; } set { _iddeposito = value; } }
        public int EstadoReg {get{ return _estadoreg; } set { _estadoreg = value; } }
        public DateTime FechaVale { get { return _fechavale; } set { _fechavale = value; } }
        public string TipoVale { get { return _tipovale; } set { _tipovale = value; } }
        public string Observacion { get { return _observacion; } set { _observacion = value; } }
        public string NomDepo { get { return _nomdepo; } set { _nomdepo = value; } }
        public string NomObra { get { return _nomobra; } set { _nomobra = value; } }
        public string NomEmpleado { get { return _nomempleado; } set { _nomempleado = value; } }
        

        public ValeCD()
        { }
    }
}
