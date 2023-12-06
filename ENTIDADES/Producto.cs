using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ENTIDADES
{
    public class Producto
    {
        private DateTime? _bajaF;

      

        public int IdProducto { get; set; }
        public string Nombre { get;set; }
        public string CodInventario { get; set; }
        public string Descripcion { get; set; }
        public string Modelo { get; set; }
        public string NumSerie { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal CostoReposicion { get; set; }
        public int CodigoBarra { get; set; }
        public string Accesorios { get; set; }
        public int CondicionStk { get; set; } //  indica la condicion del stock del producto : si es discontinuo, obsoleto, continuo
        public int ControlSf { get; set; } // indica si el producto tiene un seguimiento especifico 1 = si, 0=no
        public int EsKit { get; set; } // indica si el producto es parte de un conjunto de otras partes relacionadas en un todo.
        public int IdMarcaP { get; set; }
        public int Idsf { get; set; } // idnica la situacion fisica del producto, es decir donde se encuentra : en deposito, obra, mantenimiento, 
        public int IdTipoP { get; set; } // id tipo de producto
        public int IdCateP { get; set; } // id categoria del producto relacionado con el tipo 
        public int IdSegP { get; set; } // id segmento del producto relacionado con la categoria
        public int EstadoItem { get; set; } // estado del item : activo, inactivo
        public DateTime AltaF { get; set; } // fecha de alta en sistema
        public DateTime? BajaF { get => _bajaF; set => _bajaF = value; } // fecha de baja en sistema, es decir pasa a inactivo
        public int NumeroKit { get; set; } // indica el numero de kit de materiales al que pertenece el producto
        public string Dimensiones { get; set; } // dimensiones
        public string Color { get; set; } // color
        public string Marca { get; set; } // marca del producto correspondiente al suminstrado por el proveedor
        public string Tipo { get; set; } 
        public string Categoria { get; set; }
        public string Segmento { get; set; }
        public string Situacion { get; set; } //  situacion , relacionado con el id de situacion fisica
        public string TipoSituacionStk { get; set; } // situacion del stock se relaciona con la condicionstk
        public decimal StkDisponible { get; set; } // solo muestra la cantidad del deposito 1
        public string EstadoInstrumento { get; set; } // "estado_calibracion" indica , que al ser un instrumeto , es "apto" o "no apto". 
        public int Patron { get; set; } // indica si el instrumento es patron(1) o no (2) 
        public string EsPatron { get; set; }
        public string Escala { get; set; } // indica la escala para el caso del instrumento
        public string RangoMedicion { get; set; } // indica el rango de medicion del instrumento
        public string Exactitud { get; set; } // indica los valores de exactitud del instrumento
        public int Constrate { get; set; } // indica contraste  interno(1) , externo(2), o no contraste(3)
        public string NomConstraste { get; set; }

        public Producto()
        {
            
        }

      

     
    }
}
