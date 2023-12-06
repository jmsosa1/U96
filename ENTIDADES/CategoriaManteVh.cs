using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CategoriaManteVh
    {
        int _idcantemante;
        string _nomcate;

        public int IdCateMante { get { return _idcantemante; } set { _idcantemante = value; } }
        public string NomCate { get { return _nomcate; } set { _nomcate = value; } }

        public CategoriaManteVh()
        { }

    }
}
