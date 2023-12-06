using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Mpm
    {
        //clase que representa los mantenimientos programados para una maquina (MPM)
        public int Idmpm { get; set; }
        public int IdProducto { get; set; } // identificador del producto 
        public DateTime AltaF { get; set; } // fecha de creacion de la planilla
        //public string ElementoObservable { get; set; } //  parte de la maquina a controlar
        public int CantidadTareas { get; set; } // cantidad de tareas asignadas para la planilla
        public int Estado { get; set; } // estado de la planilla, 1: activa , 2:inactiva, indica si el elemento es valido todavia
        public string Situacion { get; set; } // "Pendiente" / "Parcial" / "Completa"
        public decimal Cumplimiento { get; set; } // porcentaje de cumplimiento de la totalidad de las tareas
        //public string UnidadControl { get; set; }// unidad de control : dias / horas
        public int CantAcuUnidades { get; set; } // cantidad de unidades acumuladas , ejemplo: 20 dias, 200hs
        public DateTime? FechaCierre { get; set; } // fecha de finalizacion ya sea por cumplimiento total o baja de la planilla
       
    }
}
