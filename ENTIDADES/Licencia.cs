using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    // representa  a la tabla licencia-conducir
    public class Licencia
    {
        //campos
        int _idlicencia;
        int _idempleado;
        string _numlicencia;
        string _tarjetazul;
        string _psicofisico;
        string _licespecial;
        DateTime _vtolicencia;
        DateTime _vtoazul;
        DateTime _vtopsico;
        DateTime _vtoespecial;

        //propiedades
        public int IdLicencia { get { return _idlicencia; } set { _idlicencia = value; } }
        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public string NumLicencia { get { return _numlicencia; } set { _numlicencia = value; } }
        public string Tarjetazul { get { return _tarjetazul; } set { _tarjetazul = value; } }
        public string Psicofisico { get { return _psicofisico; } set { _psicofisico = value; } }
        public string Licespecial { get { return _licespecial; } set { _licespecial = value; } }
        public DateTime VtoLicencia { get { return _vtolicencia; } set { _vtolicencia = value; } }
        public DateTime VtoAzul { get { return _vtoazul; } set { _vtoazul = value; } }
        public DateTime VtoPsico { get { return _vtopsico; } set { _vtopsico = value; } }
        public DateTime VtoEspecial { get { return _vtoespecial; } set { _vtoespecial = value; } }

        // constructor
        public Licencia ()
        { }
    }
}
