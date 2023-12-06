using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class ElementoMPM
    {
        //representa las objetos que son elementos observables en una planilla de mantenimiento

        public int IdElemento { get; set; }
        public string NombreElemento { get; set; }
        public string Descripcion { get; set; }
    }
}
