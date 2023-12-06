using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class PlanCajon
    {
        //campos 
        int _idplantilla;
        int _idtipocajon;
        int _idproducto;
        int _cantidad_requerida;

        //propiedades
        public int IdPlantilla { get { return _idplantilla; } set { _idplantilla = value; } }
        public int IdTipoCajon { get { return _idtipocajon; } set { _idtipocajon = value; } }
        public int IdProducto { get { return _idproducto; } set { _idproducto = value; } }
        public int CantidadRequerida { get { return _cantidad_requerida; } set { _cantidad_requerida = value; } }

        //constructor
        public PlanCajon()
        { }

    }
}
