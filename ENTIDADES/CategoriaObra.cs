using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CategoriaObra
    {
        int _idcateobra;
        string _nombrecategoria;
        
        public int IdCateObra { get { return _idcateobra; } set { _idcateobra = value; } }
        public string NombreCateO { get { return _nombrecategoria; } set { _nombrecategoria = value; } }

        public CategoriaObra()
        {
           
        }
    }
}
