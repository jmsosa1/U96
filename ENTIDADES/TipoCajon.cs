using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TipoCajon
    {
        int _idtipocajon;
        string _nomtipo;
        decimal _costorepo;
        DateTime _altaf, _ultif;

        public int IdTipoCajon { get { return _idtipocajon; } set { _idtipocajon = value; } }
        public string NomTipo { get { return _nomtipo; } set { _nomtipo = value; } }
        public decimal CostoRepo { get { return _costorepo; } set { _costorepo = value; } }
        public DateTime Altaf { get { return _altaf; } set { _altaf = value; } }
        public DateTime UltiF { get { return _ultif; } set { _ultif = value; } }

        public TipoCajon()
        { }
    }
}
