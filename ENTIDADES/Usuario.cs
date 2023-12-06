using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Usuario
    {
        int _idusuario, _estado;
        string _nomuser, _clave, _email, _celular, _iniciales, _rol;

        public int IdUsuario { get { return _idusuario; } set { _idusuario = value; } }
        public int Estado { get { return _estado; } set { _estado = value; } }
        public string NomUser { get { return _nomuser; } set { _nomuser = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public string Celular { get { return _celular; } set { _celular = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Iniciales { get { return _iniciales; } set { _iniciales = value; } }
        public string RolUsuario { get { return _rol; } set { _rol = value; } }
        public int Deposito { get; set; }

        public Usuario()
        { }
    }
}
