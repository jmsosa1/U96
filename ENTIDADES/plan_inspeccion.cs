using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ENTIDADES
{
    public class plan_inspeccion //: INotifyPropertyChanged
    {
        private DateTime? _baja;
        private byte[] _image_estado_temp;

        //public event PropertyChangedEventHandler PropertyChanged;

        public int Idplan { get; set; }
        public int Idvh { get; set; }
        public string Dominio { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Tarea { get; set; }
        public decimal ValorConstante { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? Baja { get => _baja; set => _baja = value; }
        public int Estado { get; set; } // 1 = activo , 2 = activo a vencer, 3= activo vencido, 0 =vencido baja
        public string AtributoComparativo { get; set; }
        public decimal ValorActualComparativo { get; set; }
        public decimal ValorLimiteComparativo { get; set; }
        public decimal ValorInicio { get; set; }
        public DateTime Ultima_actualizacion { get; set; }
        public string SituacionTarea { get; set; }
        public decimal Gap { get; set; }
        public decimal GapAlarma { get; set; } // este campo guarda el valor de calcular el porcentaje de aviso seleccionado + el valor actual del atributo de comparacion
        public byte[] ImageEstadoTemp { get { return _image_estado_temp; } set { _image_estado_temp = value; } }
        public int IdOtm { get; set; }
        public bool TieneNotas { get; set; }
        public plan_inspeccion()
        {

        }

        
    }
}
