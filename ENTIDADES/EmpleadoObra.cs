using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class EmpleadoObra
    {
        int _idempleado, _imputacion;

        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }

        public EmpleadoObra()
        { }
    }
}
