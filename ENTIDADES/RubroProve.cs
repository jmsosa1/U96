using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class RubroProve
    {
        //campos
        int _idrubro;
        string _nombrerubro;

        public int IdRubro { get { return _idrubro; } set { _idrubro = value; } }
        public string NombreRubro { get { return _nombrerubro; } set { _nombrerubro = value; } }
        //constructor
        public RubroProve()
        { }
    }
}
