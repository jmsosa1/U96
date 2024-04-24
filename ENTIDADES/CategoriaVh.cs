using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CategoriaVh
    {
        private int _idcatevh,_idtipovh;
        private string _nomcate;

        public int IdCateVh { get; set; }
        public string NomCate { get; set; }
        public int IdTipoVh { get; set; }
       
        public decimal CostoDiarioParo { get; set; }
        public decimal CostoDiarioUso { get; set; }
        public decimal CostoUnidadCategoria { get; set; }
        public string UnidadCate { get; set; } 

        public CategoriaVh()
        { }
    }
}
