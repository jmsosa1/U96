using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ENTIDADES
{
    public class PlanificacionVH 
    {
        int _idpl, _idvh, _imputacion, _estadoreg;
        string _solicitante, _notas, _estado, _dominio, _marca, _modelo, _nomobra;
        string _notabaja;
        DateTime _fdesde, _fhasta , _fbaja;

       

        public int IdPl { get { return _idpl; } set { _idpl = value; } }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }
        public DateTime FDesde { get { return _fdesde; } set { _fdesde = value; }}
        public DateTime FHasta { get { return _fhasta; } set { _fhasta = value; } }
        public int Imputacion { get { return _imputacion; }
            set { _imputacion = value; }
              }
        public int EstadoReg { get { return _estadoreg; } set { _estadoreg = value; } }
        public string Solicitante { get { return _solicitante; }
            set { _solicitante = value;
               
            } }
        public string Notas { get { return _notas; } set { _notas = value; } }
        public string Estado { get { return _estado; } set { _estado = value; } }
        public string Dominio { get { return _dominio; }
            set { _dominio = value;
                } }

        public string Marca { get { return _marca; } set { _marca = value; } }
        public string Modelo { get { return _modelo; } set { _modelo = value; } }
        public string NomObra { get { return _nomobra; } set { _nomobra = value; } }
        public string NotaBaja { get { return _notabaja; } set { _notabaja = value; } }
        public DateTime FBaja { get { return _fbaja; } set { _fbaja = value; } }

        public PlanificacionVH()
        { }
    }
}
