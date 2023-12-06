using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENTIDADES
{
    public class Documento
    {
        private DateTime? _baja;
        private DateTime? _fechaRemProveedor;
        private DateTime? _fechaFacProveedor;
        private DateTime? fechaRemDepOrigen;
        private DateTime? fechaRemito;

        public DateTime Alta { get; set; } // fecha de creacion del registro, segun sistema
        public string Aclaraciones { get; set; } // nota aclaratoria adicional del descuento al empleado
        public string AutorizacionVale { get; set; } // nombre de quien autoriza el vale de descuento

        public DateTime? Baja { get => _baja; set => _baja = value; } // fecha de rechazo del remito segun el sistema

        public string CausaDescuento { get; set; } // causa del descuento, relacionada con el id de la causa
        public string Chofer { get; set; } // nombre del chofer que transporta el documento
        public string ClienteObra { get; set; } // nombre del cliente de la obra
        public string ClienteDestino { get; set; }
        public string Concepto { get; set; } // concepto del documento, entrega o devolucion, descuento ,consumo
        public decimal CostoDocu { get; set; } // costo del documento
        public string CuitObra { get; set; }

        public string DniEmpleado { get; set; }
        public string DirCasa { get; set; } // direccion de la casa
        public string DirObra { get; set; }
        public string Estado { get; set; } // estado del documento , relaciona con el id de estado


        public DateTime? FechaRemito { get => fechaRemito; set => fechaRemito = value; } // fecha "real" del remito, puede no coincidir con la fecha de alta del registro en sistema
        public DateTime? FechaRemProveedor { get => _fechaRemProveedor; set => _fechaRemProveedor = value; } // fecha remito proveedor
        public DateTime? FechaFacProveedor { get => _fechaFacProveedor; set => _fechaFacProveedor = value; } // fecha de la factura
        public DateTime? FechaRemDepOrigen { get => fechaRemDepOrigen; set => fechaRemDepOrigen = value; }

        public int IdDocumento { get; set; } // ID registro en tabla documento
        public int IdDeposito { get; set; } // id deposito origen del documento
        public int IdDepDestino { get; set; } // id deposito destino del documento
        public int IdCasa { get; set; }// id casa asociada a la obra
        public int IdProve { get; set; } // id del proveedor de los materiales comprados
        public int IdUsuario { get; set; } // id del usuario que realiza el documento
        public int IdEstado { get; set; } // id estado del documento 1=activo , 2=rechazado
        public int IdTipoRem { get; set; } // id tipo documento segun tabla de tipos de documentos
        public int IdEmpleado { get; set; } // id empleado que recibe o devuelve los productos
        public int IdEmpleadoDestino { get; set; } // id empleado destino se usa para el movimiento de obras
        public int IdTransporte { get; set; } // id del transporte , segun rubro del proveedor 
        public int IdCausaDescuento { get; set; } // id de la causa del descuento de producto a un empleado
        public decimal ImporteFacturaProveedor { get; set; } // importe que figura en la factura del proveedor
        public int Imputacion { get; set; } // numero de imputacion de la obra
        public int ImputacionDestino { get; set; }

        public string Localidad { get; set; }

        public string NumDocumento { get; set; } // numero de documento que figura en el talonario impreso tipo R
        public string NumFacturaProveedor { get; set; } // numero de la factura del proveedor adjunta con la compra del material
        public string NumeroOc { get; set; } // numero de la orden de compra al proveedor
        public string NombreUsuario { get; set; } // nombre del usario que realizo el remito
        public string NombreEmpleado { get; set; } // nombre del empleado que envia el producto
        public string NombreEmpledoDestino { get; set; } // nombre del empleado que recibe el producto
        public string NombreDepOrigen { get; set; }// nombre del deposito que envia materiales
        public string NombreDepDestino { get; set; } // nombre del deposito que recibe materiales se usa para el

        public string NotaRemito { get; set; } // nota aclaratoria incluida en el remito
        public string NombreObra { get; set; }
        public string NOmbreObraDestino { get; set; }

        public string Provincia { get; set; }

        public int Registrado { get; set; } // 1 no negristrado, 2 registrado indica si se registro el remito como compra dentro del sistema
        public string RemitoProveedor { get; set; } // numero del remito del proveedor adjunto con la compra del material
        public string RazonSocial { get; set; } //  razon social del proveedor
        public int RetiraPersonal { get; set; } // 1= si , 2=no , indica si el empleado retira o devuelve personalmente las productos
        public string RemDepOrigen { get; set; } // numero del remito del deposito interno que envia la mercaderia comprada 

        public string Transporte { get; set; }
        public string TipoDocumento { get; set; } // nombre del documento generado segun la ocacion DSO, DDO,DIP , etc

        public string SectorEmpleado { get; set; }

        public Documento()
        {
            // en el constructor ponemos los valores por defecto de las propiedades
            Alta = DateTime.Today;
            Aclaraciones = "";
            AutorizacionVale = "";

            Baja = null;

            CausaDescuento = "";
            Chofer = "";
            ClienteObra = "";
            Concepto = "";
            CostoDocu = 0;

            DniEmpleado = "";
            DirCasa = "";
            Estado = "";
            FechaRemito = DateTime.Today;
            FechaFacProveedor = null;
            FechaRemProveedor = null;
            fechaRemDepOrigen = null;

            IdCasa = 0;
            IdCausaDescuento = 0;
            IdDepDestino = 0;
            IdDeposito = 0;
            IdEmpleado = 0;
            IdEmpleadoDestino = 0;
            IdEstado = 1;
            IdProve = 0;
            IdTipoRem = 0;
            IdTransporte = 24;
            IdUsuario = 0;
            ImporteFacturaProveedor = 0;
            Imputacion = 0;
            ImputacionDestino = 0;
            Localidad = "";
            NumDocumento = "";

            NumFacturaProveedor = "";
            NumeroOc = "";
            NombreEmpleado = "";
            NombreUsuario = "";
            NombreDepOrigen = "";
            NombreDepDestino = "";
            NotaRemito = "";

            Provincia = "";
            Registrado = 0;
            RemitoProveedor = "";
            RazonSocial = "";
            RetiraPersonal = 0;
            RemDepOrigen = "";
            Transporte = "";
            TipoDocumento = "";
            SectorEmpleado = "";
            ClienteDestino = "";
        }
    }
}
