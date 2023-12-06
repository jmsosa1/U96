using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CategoriaVh
    {
        private int _idcatevh,_idtipovh;
        private string _nomcate;

        public int IdCateVh { get { return _idcatevh; } set { _idcatevh = value; } }
        public string NomCate { get { return _nomcate; } set { _nomcate = value; } }
        public int IdTipoVh { get { return _idtipovh; } set { _idtipovh = value; } }
       
        public decimal CostoDiarioParo { get; set; }
        public decimal CostoDiarioUso { get; set; }

        public CategoriaVh()
        { }
    }
}
