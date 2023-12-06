using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class MarcaProductos
    {
        //campos
        int _idmarca;
        int _idprove;
        string _nombremarca;

        //propiedades
        public int IdMarca { get { return _idmarca; } set { _idmarca = value; } }
        public string NombreMarca { get { return _nombremarca; } set { _nombremarca = value; } }
        public int IdProve { get { return _idprove; } set { _idprove = value; } }
        //constructor
        public MarcaProductos()
        { }
    }
}
