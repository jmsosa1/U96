using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTIDADES
{
    public class CategoriaEmpleado 
    {
        int _idcateempleado;
        string _nombre_categoria;

        public int IdCategoria { get { return _idcateempleado; } set { _idcateempleado = value; } }
        public string NombreCategoria { get { return _nombre_categoria; } set { _nombre_categoria = value; } }
        //constructor
        public CategoriaEmpleado()
        {
        }
    }
}
