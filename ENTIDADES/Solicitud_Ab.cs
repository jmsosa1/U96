using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace ENTIDADES
{
    public class Solicitud_Ab
    {
        public int IdSol { get; set; }
        public int UsuarioCreador { get; set; }
        public int Imputacion { get; set; }
        public string NomObra { get; set; }

        public int EstadoTemporal { get; set; }
        public int Vencida { get; set; }
        public int CantidadItems { get; set; }
        public int PorcentajeCumplimiento { get; set; }
        public int UsuarioSeguidor { get; set; }
        public string NomSeguidor { get; set; }
        public DateTime AltaF { get; set; }
        public DateTime FNecesidad { get; set; }
        public DateTime? Fcumplimiento { get; set; }
        public string Titulo { get; set; }
        public string EstadoSolicitud { get; set; }
        public string SolicitadoPor { get; set; }
        public byte[] ImgEstado { get; set; }
        public string Nota { get; set; }
        public int IdEmpleado { get; set; }

        public Solicitud_Ab()
        { }

       
    }
}
