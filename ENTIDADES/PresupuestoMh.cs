using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class PresupuestoMh :Presupuesto
    {
        public PresupuestoMh()
        {
            F_Alta = DateTime.Now.Date;
            IdEstado = 1;
            Estado = "Activo";
            IdSituacion = 17;
            Situacion = "Abierto";
            IdTipoPresupuesto = 20;
            TipoPresupuesto = "Herramientas";
            Anio = DateTime.Now.Year;
            MonedaPpal = 2;
            NomMonedaPpal = "Pesos";
            MonedaSec = 1;
            NomMonedaSec = "Dolar";
            MontoTotalAprobado = 0;
            MontoTotalEjecutado = 0;
            MontoTotalMonedaPpal = 0;
            DolarPresupuesto = 365;
        }
    }
}
