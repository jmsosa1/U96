using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Gastos
    {
        //campos
        int _idgasto;
        int _mes;
        int _anio;
        decimal _totalmes;

        //propiedades
        public int IdGasto { get { return _idgasto; } set { _idgasto = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public decimal TotalMes { get { return _totalmes; } set { _totalmes = value; } }

        //constructor
        public Gastos()
        { }
    }
}
