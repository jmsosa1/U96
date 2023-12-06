using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class VehiculoDocu
    {
        
        private DateTime? _fvencimiento, _fdesde, _fhasta;
      

        public int IdVhDoc { get; set; }
        public int IdVh { get; set; }
        public int IdDocuvh { get; set; }
        public DateTime? FVencimiento { get => _fvencimiento; set { _fvencimiento = value; } }
        public DateTime? FDesde { get { return _fdesde; } set { _fdesde = value; } }
        public DateTime? FHasta { get { return _fhasta; } set { _fhasta = value; } }
        public decimal Costo { get; set; }
        public string Nota { get; set; }
        public string NumeroDoc { get; set; }
        
        public int EstadoReg { get; set; }
        public DateTime Altaf { get; set; }
        public string NombreDocu { get; set; }
        public string DominioVh { get; set; }
        public string Situacion { get; set; }
        public string DescriVh { get; set; }
        public int ControlFecha { get; set; }
        public string ModeloVh { get; set; }
        // Fvencimiento = fecha de vencimiento de la entrada actual
        // fdesde = fecha inicio de la cobertura / vigencia del documento
        //fhasta = fecha fin de la cobertura / vigencia del documento
        // costo= importe del documento
        // numerodoc = codigo alfanumerico que identifica al documento, ejemplo:numero poliza ,tajeta comb
        // nota = aclaracion del documento , si hace falta, ejemplo "Cuota 1" 
        //aniodoc = año actual del documento
        //estadoreg  = 1 -activo, 2-vencido , 3-cumplido
        // controlfecha = 1 - fechavencimiento , 2- fecha desde-hasta
        public bool TieneNotas { get; set; }
        public VehiculoDocu()
        { }
    }
}
