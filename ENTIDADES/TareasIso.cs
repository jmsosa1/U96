using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TareasIso
    {
        int _idtarea;
        string _descri;
        string _periodo;

        public int IdTareaIso { get { return _idtarea; } set { _idtarea = value; } }
        public string Descri{get { return _descri; }set { _descri = value; }}
        public string Periodo { get { return _periodo; } set { _periodo = value; } }

        public TareasIso()
        { }
    }
}
