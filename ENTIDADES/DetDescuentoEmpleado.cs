﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class DetDescuentoEmpleado : DocumentoDetalle
    {
        public DateTime FechaRemito { get; set; }
        public decimal CostoTotalItem { get; set; }
    }
}
