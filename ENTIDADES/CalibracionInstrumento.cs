using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CalibracionInstrumento 
    {
        public int IdCalibracion { get; set; } 
        public int IdProducto { get; set; } // id del instrumento que se esta calibrando
        
        public DateTime AltaRegistro { get; set; } // alta en sistema de la calibracion
        public DateTime? BajaRegistro { get; set; } // baja en sistema de la calibracion
        public DateTime? FechaUltimaCalibracion { get; set; } // fecha que se hizo la calibracion
        public int IdProveedor { get; set; } // id proveedor de la calibracion
        public string RazonSocial { get; set; } // proveedor de la calibracion
        public string NumeroCertificado { get; set; } //
        public DateTime? VencimientoActual { get; set; } // fecha proximo vencimiento
        public string Nota { get; set; }  //  nota sobre la calibracion
        public string EmisorCertificado { get; set; }// lab que emite el certificado
        public string Resultado { get; set; } // resultado calibracion
        public int EstadoVencimiento { get; set; } // indica si esta valido el instrumento de acuerdo a la fecha de calibracion ultima, 1 es vigente, 2 es vencido
        public string OC { get; set; } // orden de compra de la calibracion
        public decimal ImporteCalibracion { get; set; } // importe que figura en la orden de compra
        public int ValidezDias { get; set; } // cantidad de dias que tiene validez el certificado
        public string RutaArchivo { get; set; } //  ruta de almacenamiento del archivo 
        public int Cod_Resultado { get; set; } 
        public int EstadoCalibracion { get; set; } // indica si la calibracion es la activa
     





    }
}
