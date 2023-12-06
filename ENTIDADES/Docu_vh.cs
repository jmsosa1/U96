using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class Docu_vh
    {
        int _iddocuvh;
        string _descripcion;
        

        public int IdDocuVH { get { return _iddocuvh; } set { _iddocuvh = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public Docu_vh()
        { }
    }
}
