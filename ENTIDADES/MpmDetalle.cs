using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class MpmDetalle
    {
        private DateTime? fechaVencimiento;
       

        public int Id { get; set; }
        public int Idmpm { get; set; }
        //public int IdElemento { get; set; } // id del elemento observable
        public DateTime FechaInicio { get; set; }
        public string ElementoObservable { get; set; } //  parte de la maquina a controlar
        public string DescriTarea { get; set; } // descripcion de la tarea a realizar
        public int Frecuencia { get; set; } // cantidad de comparacion y control de cumplimiento de la tarea .Valor de inicio contra el cual se compara el gap
        public string Unidad { get; set; } //  dias u horas
        public DateTime? FechaVencimiento { get => fechaVencimiento; set => fechaVencimiento = value; } // fecha de vencimiento, en caso que se comparen dias
        public string EstadoTarea { get; set; } // activa / cumplida
        public int Gap { get; set; }// cantidad de control para comparar contra la frecuencia
        public int Ejecucion { get; set; } // 1 registrado - 2 no registrado , indica si se ejecuto o no la tarea
        public int Alarma { get; set; }
        // para ejecutar las alarmas , siempre se considera que el valor del gap sea menor o igual a la alarma o mayor que cero para proximos a vencer
        // si el gap es mayor a la alarma pasa a representar los vencidos
        public string SituacionTarea { get; set; } // indica el estado, vencido, normal , por vencer
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
    }
}
