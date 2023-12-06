using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Presupuesto
    {
        public int IdPre { get; set; } // identificador unico del registro 
        public int IdUsuario { get; set; } // identificador del usuario que hace el presupuesto
        public string NomUsuarioCreador { get; set; }
        public int IdEstado { get; set; } //  identificador del estado del presupuesto 1= Activo, 2=Inactivo
        public string Estado { get; set; }
        public int Es_Adicional { get; set; } // indica si el presupuesto es adicional, valor = 0  Base , valor = 1 adicional
        public string Numero { get; set; } // numero del presupuesto generado por el sistema
        public string Version { get; set; } // version del presupuesto
        public DateTime F_Alta { get; set; } // fecha de alta del presupuesto
        public DateTime? F_Baja { get;set; } // fecha de baja del presupuesto
        public DateTime? F_Vencimiento { get; set; } // fecha de vencimiento del presupuesto
        public DateTime? F_UltimaModificacion { get; set; } //  fecha de la ultima modificacion del presupuesto
        public string UsuarioUMD { get; set; } // nombre del usuario que realizo la ultima modificacion
        public string Titulo { get; set; } // titulo del presupuesto
        public string Descripcion { get; set; } // descripcion del presupuesto 
        public int Anio { get; set; } // anio de ejecucion del presupuesto
        public decimal MontoTotalMonedaPpal { get; set; } // monto total en moneda principal, sale de la sumatoria de cada item del detalle
        public decimal MontoTotalMonedaSec { get; set; } // idem anterior, pero en moneda secundaria, dolar, calculado del monto principal
        public int MonedaPpal { get; set; } // id de la moneda principal, tabla moneda
        public string NomMonedaPpal { get; set; }
        public int MonedaSec { get; set; } // id de la moneda secundaria , tabla moneda
        public string NomMonedaSec { get; set; }
        public decimal DolarPresupuesto { get; set; } //  cotizacion del dolar con el que se rige el presupuesto
        public decimal MontoTotalAprobado { get; set; } // monto total aprobado del presupuesto, campo calculado de cada item del detalle
        public decimal MontoTotalEjecutado { get; set; } // monto total ejecutado en funcion de las inversiones , campo calculado de cada item del detalle
        public decimal PorcentajeAprobado { get; set; } //  porcentaje sobre el monto total presupuestado . Luego se usa para distribuir en los items
        public int DesviacionPresupuesto { get; set; } // valor de la posible desviancion en el monto presupuestado
        public int IdTipoPresupuesto { get; set; } //  tipo de presupuesto : 19 =Vehiculo / 20=Herramientas
        public  string TipoPresupuesto { get;set; } // tipo de presupuesto Valores : Vehiculos / Herramientas
        public int IdSituacion { get; set; } //  situacion del presupuesto : 17= Abierto, 18=Cerrado,
        public string Situacion { get; set; }

     
    }
}
