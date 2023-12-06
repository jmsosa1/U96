using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Gastos_vh
    {
        int _idgastovh, _mes, _anio;
        decimal _totalmes;

        public int IdGastovh { get { return _idgastovh; } set { _idgastovh = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public decimal TotalMes { get { return _totalmes; } set { _totalmes = value; } }

        public Gastos_vh()
        { }

    }
}
