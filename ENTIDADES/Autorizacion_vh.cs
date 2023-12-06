using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
  public  class Autorizacion_vh
    {
        private int _idaut, _idvh, _idempleado, _valor, _dni;
        private string _numautorizacion, _estado, _nomempleado, _dominio, _marca, _modelo;
        DateTime _altaf , _finicio;
        private DateTime? _bajaf;
        private DateTime? _ffinal;

        public int IdAut { get { return _idaut; } set { _idaut = value; } }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }
        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public int Valor { get { return _valor; } set { _valor = value; } }
        public string NumAutorizacion { get { return _numautorizacion; } set { _numautorizacion = value; } }
        public string Estado { get { return _estado; } set { _estado = value; } }
        public DateTime Finicio { get { return _finicio; } set { _finicio = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public DateTime? BajaF { get => _bajaf; set => _bajaf = value; }
        public DateTime? Ffinal { get => _ffinal; set => _ffinal=value; }
        public string NombreEmpleado { get { return _nomempleado; } set { _nomempleado = value; } }
        public string DominioVh { get { return _dominio; } set { _dominio = value; } }
        public string Marca { get { return _marca; } set { _marca = value; } }
        public string Modelo { get { return _modelo; } set { _modelo = value; } }
        public int DNI { get { return _dni; } set { _dni = value; } }
       

        public Autorizacion_vh()
        { }
    }
}
