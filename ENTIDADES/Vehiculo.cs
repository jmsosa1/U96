using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class Vehiculo : INotifyPropertyChanged
    {
        
      
        
        private DateTime? _bajaf, _fechacompra;
        //byte[] _foto;

        public int IdVh { get; set; }

        [Required(ErrorMessage ="El campo es requerido")]
        public string Descripcion { get; set; } 
        [Required(ErrorMessage ="El campo es requerido")]
        public string Dominio { get; set; } 
        [Required(ErrorMessage ="El camop es requerido")]
        public string Modelo { get; set; }

        public decimal LitroHora { get; set; } = 0;
        public decimal ValorHora { get; set; } = 0;

        public decimal ValorKm { get; set; } = 0;

        public int IdCombustible { get; set; }
        public int CantiEjes { get; set; } = 0;
        public int RSat { get; set; } = 1;
        public int SegSat { get; set; } = 1;
        public string NumeroChasis { get; set; } = "no indica";
        public string NumeroMotor { get; set; } = "no indica";
        public string Color { get; set; } = "colo";
        public string AnioModelo { get; set; } = "no indica"; 
        public string Cilindrada { get; set; } = "no indica"; 
        public string NeuDelantero { get; set; } = "no indica";
        public string NeuTrasero { get; set; } = "no indica";

        public decimal ValorCompra { get; set; } = 0;
        public DateTime AltaF { get; set; } = DateTime.Today;
        public DateTime? BajaF { get => _bajaf; set => _bajaf = value; } 
        public DateTime? FechaCompra { get => _fechacompra; set => _fechacompra = value; }
        public string CausaBaja { get; set; } = "no indica";
        public string Garantia { get; set; } = "no indica";
        public decimal Amortizacion { get; set; } = 0;
        public decimal KmAcumulado { get; set; } = 0;
        public decimal KmLitro { get; set; } = 0;
        public int IdMarca { get; set; } = 0;
        public decimal HorasAcumuladas { get; set; } = 0;
        public int IdSf { get; set; }
        public int CantDocVencida { get; set; } = 0;
        public decimal ValorLitro { get; set; } = 0;
        public int IdTipoVh { get; set; }
        public int IdCate { get; set; }
        public int IdLinea { get; set; }
        public string Estado { get; set; } = "Activo";

        // public byte[] FotoVh { get { return _foto; } set { _foto = value; } }

        public string NomMarca { get; set; } = "no indica";
        public string NomCombustible { get; set; } = "no indica";
        public string NomSfisica { get; set; } = "no indica";
        public string NomCategoria { get; set; } = "no indica"; 
        public decimal KmInicial { get; set; } = 0; 
        public decimal HsInicial { get; set; } = 0;
        public string CodigoRadio { get; set; } = "no indica";
        public int KmKitDistribucion { get; set; } = 0;
        public decimal Largo { get; set; } = 0;
        public decimal Ancho { get; set; } = 0;
        public decimal Alto { get; set; } = 0;
        public decimal Peso { get; set; } = 0;
        public decimal CargaMaxima { get; set; } = 0;
        public decimal VolumenCarga { get; set; } = 0;
        public decimal SuperficieCarga { get; set; } = 0;
        public decimal CostoReposicionDls { get; set; } = 0;// costo reposicion en dolares
        public event PropertyChangedEventHandler PropertyChanged;

        //implementacion del metodo de propertyChanged
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e); //generar el evento 
        }

        public Vehiculo()
        {


        }
    }
}
