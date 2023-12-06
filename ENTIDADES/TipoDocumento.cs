using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TipoDocumento
    {
        int _idtiporem;
        string _codigo;
        string _descripcion;

        public int IdTipoRem { get { return _idtiporem; } set { _idtiporem = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public TipoDocumento()
        { }
    }
}
