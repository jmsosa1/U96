using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Inversiones
    {
        //campos
        int _idinversion;
        int _mes;
        int _anio;
        decimal _totalmes;

        //propiedades
        public int IdInversion { get { return _idinversion; } set { _idinversion = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public decimal TotalMes { get { return _totalmes; } set { _totalmes = value; } }

        //constructor
        public Inversiones()
        { }
    }
}
