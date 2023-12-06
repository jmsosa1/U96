using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class PresupuestoVh : Presupuesto
    {
        public PresupuestoVh()
        {
            F_Alta = DateTime.Now.Date;
           
            IdEstado = 1;
            Estado = "Activo";
            IdSituacion = 17;
            Situacion = "Abierto";
            IdTipoPresupuesto = 19;
            TipoPresupuesto = "Vehiculos";
            Anio = DateTime.Now.Year;
            MonedaPpal = 2;
            NomMonedaPpal = "Pesos";
            MonedaSec = 1;
            NomMonedaSec = "Dolar";
            MontoTotalAprobado = 0;
            MontoTotalEjecutado = 0;
            MontoTotalMonedaPpal = 0;
            DolarPresupuesto = 700;
            Titulo = "Presupuesto de Vehiculos";
            Version = "1";
            Numero = "0";
            
        }
    }
}
