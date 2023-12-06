using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class MpmRegistro
    {
        private DateTime? fechaRegEjecucion;
        //contiene los datos de la ejecucion de una tarea de mantenimiento programado
        public int  Id { get; set; }
        public int Idmpm { get; set; }
        public DateTime? FechaRegEjecucion { get => fechaRegEjecucion; set => fechaRegEjecucion = value; }
        public int TipoEjecutor { get; set; } // 1 si es un empleado o 2 si es un proveedor
        public int IdEjecutor { get; set; } // id empleado que realiza la ejecucion de la tarea
        public string ResultadoEjecucion { get; set; } // resultado de la ejecucion de la tarea correcto/incorrecto
        public string Observaciones { get; set; } // datos complementarios de la tarea
    }
}
