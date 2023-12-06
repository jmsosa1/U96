using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class TareasMante
    {
        int _idtarea, _idficha, _idtareaiso, _vencida;
        DateTime _fvencimiento, _fcumplido, _proximovencimiento;

        public int IdTarea { get { return _idtarea; } set { _idtarea = value; } }
        public int IdFicha { get { return _idficha; } set { _idficha = value; } }
        public int IdTareaIso { get { return _idtareaiso; } set { _idtareaiso = value; } }
        public int Vencida { get { return _vencida; } set { _vencida = value; } }
        public DateTime FVencimiento { get { return _fvencimiento; } set { _fvencimiento = value; } }
        public DateTime FCumplido { get { return _fcumplido; } set { _fcumplido = value; } }
        public DateTime ProximoVencimiento { get { return _proximovencimiento; } set { _proximovencimiento = value; }}

        public TareasMante()
        { }
    }
}
