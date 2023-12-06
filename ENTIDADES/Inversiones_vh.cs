using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Inversiones_vh
    {
        int  _mes, _anio;
        int _idinversionvh;
        decimal _totalmes;

        public int IdInversionVh { get { return _idinversionvh; } set { _idinversionvh = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public decimal TotalMes { get { return _totalmes; } set { _totalmes = value; } }

        public Inversiones_vh()
        { }
    }
}
