using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ENTIDADES;
using System.Collections.ObjectModel;

namespace DAL
{
    // clase que contiene todos los metodos para administrar los remitos de ingreso , egreso , devolucion de productos
    // registros de mantenimientos, registros de compras de productos y de vehiculos
    // vales de descuento y vales de consumo
    //vistas de remitos por empleados, obra, sector, producto,vehiculo, etc
    //
    public class DALRemito
    {

        DALConexion conexion = new DALConexion();

        #region ABMRemitoVehiculos
        //remito nuevo registro , parametros : objeto tipo Documento, 
      public void GrabarEncabezado(Documento documento)
      {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Documento_Alta_Encabezado";
            cmd.Parameters.AddWithValue("@numdocu",documento.NumDocumento);
            cmd.Parameters.AddWithValue("@idtiporem",documento.IdTipoRem);
            cmd.Parameters.AddWithValue("@alta",documento.Alta);
            cmd.Parameters.AddWithValue("@concepto",documento.Concepto);
            cmd.Parameters.AddWithValue("@imputacion",documento.Imputacion);
            cmd.Parameters.AddWithValue("@cost_docu",documento.CostoDocu);
            cmd.Parameters.AddWithValue("@idestado",documento.IdEstado);
            cmd.Parameters.AddWithValue("@remprove",documento.RemitoProveedor);
            cmd.Parameters.AddWithValue("@fecharemprove", documento.FechaRemProveedor);
            cmd.Parameters.AddWithValue("@numfacturaprove",documento.NumFacturaProveedor);
            cmd.Parameters.AddWithValue("@importefactura",documento.ImporteFacturaProveedor);
            cmd.Parameters.AddWithValue("@fechafactura",documento.FechaFacProveedor);
            cmd.Parameters.AddWithValue("@numoc",documento.NumeroOc);
            cmd.Parameters.AddWithValue("@registrado",documento.Registrado);
            
            cmd.Parameters.AddWithValue("@idempleado",documento.IdEmpleado);
            cmd.Parameters.AddWithValue("@idusuario",documento.IdUsuario);
            cmd.Parameters.AddWithValue("@idcasa",documento.IdCasa);
            cmd.Parameters.AddWithValue("@idprove",documento.IdProve);
            cmd.Parameters.AddWithValue("@iddeposito",documento.IdDeposito);
            cmd.Parameters.AddWithValue("@chofer", documento.Chofer);
            cmd.Parameters.AddWithValue("@notaremito", documento.NotaRemito);
            cmd.Parameters.AddWithValue("@fecharemito", documento.FechaRemito);
            cmd.Parameters.AddWithValue("@aclaraciones", documento.Aclaraciones);
            cmd.Parameters.AddWithValue("@autorizavale", documento.AutorizacionVale);
           
            
            
           
            cmd.Parameters.AddWithValue("@fecremdeporigen", documento.FechaRemDepOrigen);
            cmd.Parameters.AddWithValue("@iddepdestino", documento.IdDepDestino);
            cmd.Parameters.AddWithValue("@idtransporte", documento.IdTransporte);
            cmd.Parameters.AddWithValue("@idcausadescuento", documento.IdCausaDescuento);
            cmd.Parameters.AddWithValue("@transporte", documento.Transporte);
            cmd.Parameters.AddWithValue("@dep_origen", documento.NombreDepOrigen);
            cmd.Parameters.AddWithValue("@dep_destino", documento.NombreDepDestino);
           
            cmd.Parameters.AddWithValue("@retirapersonal", documento.RetiraPersonal);
            cmd.Parameters.AddWithValue("@remdeporigen", documento.RemDepOrigen);
            cmd.Parameters.AddWithValue("@imputacion_destino", documento.ImputacionDestino);
            cmd.Parameters.AddWithValue("@idemp_destino", documento.IdEmpleadoDestino);

            try
            {
                conexion.AbriConexion();
                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

      public int ObtenerUltimoIdDocumento()
       {
            int _ultimoIdDoc = 0;
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Documento_Obtener_UltimoId";
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _ultimoIdDoc = (int)reader["iddocumento"];

                }
                
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return _ultimoIdDoc;
        }

       public void GrabarDetalle(DocumentoDetalle detalle)
       {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Documento_Alta_Detalle";
            cmd.Parameters.AddWithValue("@iddocumento",detalle.IdDocumento);
            cmd.Parameters.AddWithValue("@codigo",detalle.CodigoItem);
            cmd.Parameters.AddWithValue("@cantidad",detalle.CantidadItem);
            cmd.Parameters.AddWithValue("@estado",detalle.EstadoItem);
            cmd.Parameters.AddWithValue("@precio",detalle.PrecioItem);
            cmd.Parameters.AddWithValue("@tipo",detalle.TipoItem);
           

            try
            {
                conexion.AbriConexion();
                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region RemitosObras

        public ObservableCollection<Documento> ListarDocObras()
        {
            ObservableCollection<Documento> lista_dip = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documentos_Mov_Obras";
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_dip = ArmarVisorDocObras(reader);

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_dip;
        }

        

        private ObservableCollection<Documento> ArmarVisorDocObras(SqlDataReader reader)
        {
            //lista de documentos de movimientos de productos al deposito : DIP y DDP
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();
            while (reader.Read())
            {
                Documento documento = new Documento();
                documento.IdDocumento = (int)reader["iddocumento"];
                documento.Alta = (DateTime)reader["altaf"];
                if (reader["baja"] != DBNull.Value)
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }

                documento.Concepto = (string)reader["concepto"];
                documento.CostoDocu = (decimal)reader["costo_docu"];
                documento.RemitoProveedor = (string)reader["remito_proveedor"];
                if (reader["fecha_rem_proveedor"] != DBNull.Value)
                {
                    documento.FechaRemProveedor = (DateTime)reader["fecha_rem_proveedor"];
                }
                else
                {
                    documento.FechaRemProveedor = null;
                }

                documento.NumFacturaProveedor = (string)reader["num_factura_proveedor"];
                documento.ImporteFacturaProveedor = (decimal)reader["importe_factura"];
                if (reader["facturaf"] != DBNull.Value)
                {
                    documento.FechaFacProveedor = (DateTime)reader["facturaf"];
                }
                else
                {
                    documento.FechaFacProveedor = null;
                }

                documento.NumeroOc = (string)reader["numero_oc"];
                documento.Registrado = (int)reader["registrado"];
                documento.IdDeposito = (int)reader["iddeposito"];
                documento.IdProve = (int)reader["idprove"];
                documento.IdUsuario = (int)reader["idusuario"];
                documento.Chofer = (string)reader["chofer"];
                documento.NotaRemito = (string)reader["nota_remito"];
                documento.FechaRemito = (DateTime)reader["fecharemito"];
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];
                if (reader["iddepdestino"] != DBNull.Value)
                {
                    documento.IdDepDestino = (int)reader["iddepdestino"];
                }
                else
                {
                    documento.IdDepDestino = 0;
                }

                documento.IdTransporte = (int)reader["idtransporte"];
                documento.RemDepOrigen = (string)reader["rem_dep_origen"];
                if (reader["fecha_rem_dep_origen"] != DBNull.Value)
                {
                    documento.FechaRemDepOrigen = (DateTime)reader["fecha_rem_dep_origen"];
                }
                else
                {
                    documento.FechaRemDepOrigen = null;
                }

                documento.NombreDepOrigen = (string)reader["dep_origen"];
                documento.NombreDepDestino = (string)reader["dep_destino"];
                documento.Transporte = (string)reader["razonsocial"];
                documento.RazonSocial = (string)reader["razonsocial"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.IdEmpleado = (int)reader["idempleado"];
                documento.NombreEmpleado = (string)reader["nombre"];
                documento.IdCasa = (int)reader["idcasa"];
                documento.Aclaraciones = (string)reader["aclaraciones"];
                documento.AutorizacionVale = (string)reader["autorizacion_vale"];
                documento.IdCausaDescuento = (int)reader["idcausadescuento"];
                documento.RetiraPersonal = (int)reader["retira_personal"];
                documento.Imputacion = (int)reader["imputacion"];
                documento.NombreObra = (string)reader["nomobra"];
                documento.ClienteObra = (string)reader["cliente"];
                lista.Add(documento);
            }
            return lista;
        }

        public Documento BuscarUnDocumentoPorId(int _iddocu)
        {
            Documento d = new Documento();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Encabezados_Pro_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _iddocu);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                d = ArmarEncabezadoDocu(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return d;
        }

        public ObservableCollection<DocumentoDetalle> BuscarUnDocDetallePorId(int _iddocu)
        {
            ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Detalle_Pro_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _iddocu);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                documentoDetalles = ArmarDetalleUnDocu(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return documentoDetalles;
        }

        private ObservableCollection<DocumentoDetalle> ArmarDetalleUnDocu(SqlDataReader reader)
        {
            ObservableCollection<DocumentoDetalle> dt = new ObservableCollection<DocumentoDetalle>();
            while (reader.Read())
            {
                DocumentoDetalle d = new DocumentoDetalle();
                d.IdDetDocu = (int)reader["iddetdocu"];
                d.IdDocumento = (int)reader["iddocumento"];
                d.CodigoItem = (int)reader["codigo_item"];
                d.CantidadItem = (int)reader["cantidad_item"];
                d.EstadoItem = (int)reader["estado_item"];
                d.PrecioItem = (decimal)reader["precio_item"];
                d.TipoItem = (int)reader["tipo_item"];
                d.Descripcion = (string)reader["nombre"];
                d.Marca = (string)reader["nombre_marca"];
                d.CodIventario = (string)reader["cod_inventario"];
                d.Modelo = (string)reader["modelo"];
                d.NumSerio = (string)reader["numserie"];
                dt.Add(d);

            }
            return dt;
        }

        private Documento ArmarEncabezadoDocu(SqlDataReader reader)
        {
            Documento documento= new Documento();
            while (reader.Read())
            {
               
                documento.IdDocumento = (int)reader["iddocumento"]; //*
                documento.NumDocumento = (string)reader["num_documento"];//*
                documento.Alta = (DateTime)reader["altaf"];//*
                documento.FechaRemito = (DateTime)reader["fecharemito"];//*
                documento.Concepto = (string)reader["concepto"];//*
                documento.Imputacion = (int)reader["imputacion"];//*
                documento.NotaRemito = (string)reader["nota_remito"];//*
                documento.IdEmpleado = (int)reader["idempleado"];//*                
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];//*
                documento.IdUsuario = (int)reader["idusuario"];
                documento.IdTransporte = (int)reader["idtransporte"];
                documento.ClienteObra = (string)reader["cliente"];
                documento.CuitObra = (string)reader["cuit_obra"];
                documento.NombreObra = (string)reader["nomobra"];
                documento.DirObra = (string)reader["direccion"];
                documento.Localidad = (string)reader["nomlocalidad"];
                documento.Provincia = (string)reader["nomprovincia"];
                documento.Chofer = (string)reader["chofer"];
                documento.NombreEmpleado = (string)reader["nombre"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.Transporte = (string)reader["transpo"];
                documento.TipoDocumento = (string)reader["codigo"];

                //zona de las fechas
                if (reader["baja"] != DBNull.Value)//*
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }

            }
            return documento;
        }

        public void AnularUnRemitoObra( int _iddocumento)
        {
            //este metodo anula , o sea cambia el estado de un remito de obra
            //anulanado el encabezado y el detalle
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Baja";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _iddocumento);
            try
            {
                conexion.AbriConexion();
                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
        }
       
      /*  public void RestauracionItemRemitoObra(int _imputacion, int _idempleado, int _idproducto, int _cantidad, decimal _precioItem)
        {
            // este metodo da marcha atras con los productos imputados a un empleado u obra
            //
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Empleado_Actualiza_Balance_BajaDoc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", _idempleado);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
            cmd.Parameters.AddWithValue("@precioitem", _precioItem);
            
            try
            {
                conexion.AbriConexion();
                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }*/

        


        public ObservableCollection<Documento> ListarDoc14()
        {
            ObservableCollection<Documento> lista_dip = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documentos_Mov_Obras_14";
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_dip = ArmarVisorDoc14(reader);

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_dip;
        }

        private ObservableCollection<Documento> ArmarVisorDoc14(SqlDataReader reader)
        {
            //lista de documentos de movimientos de productos al deposito : DIP y DDP
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();
            while (reader.Read())
            {
                Documento documento = new Documento();
                documento.IdDocumento = (int)reader["iddocumento"];
                documento.Alta = (DateTime)reader["altaf"];
                if (reader["baja"] != DBNull.Value)
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }

                documento.Concepto = (string)reader["concepto"];
                documento.NumDocumento = (string)reader["num_documento"];
                documento.TipoDocumento = (string)reader["codigo"];
                documento.IdUsuario = (int)reader["idusuario"];
                documento.Chofer = (string)reader["chofer"];
                documento.NotaRemito = (string)reader["nota_remito"];
                documento.FechaRemito = (DateTime)reader["fecharemito"];
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];
                documento.IdTransporte = (int)reader["idtransporte"];
                documento.RazonSocial = (string)reader["razonsocial"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.IdEmpleado = (int)reader["idempleado"];
                documento.NombreEmpleado = (string)reader["nombre"];
                documento.Imputacion = (int)reader["imputacion"];
                documento.ImputacionDestino = (int)reader["imputacion_destino"];
                documento.NombreObra = (string)reader["obra_origen"];
                documento.NOmbreObraDestino = (string)reader["obra_dest"];
                documento.ClienteObra = (string)reader["cliente_origen"];
                documento.ClienteDestino = (string)reader["cliente_dest"];
                lista.Add(documento);
            }
            return lista;
        }
        #endregion

        #region REmitosMOV
        public Documento BuscarUnMOVPorId(int _iddocu)
        {
            Documento d = new Documento();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Encabezado_MOV";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", _iddocu);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                d = ArmarEncabezadoMov(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return d;
        }

        private Documento ArmarEncabezadoMov(SqlDataReader reader)
        {
            Documento documento = new Documento();
            while (reader.Read())
            {

                documento.IdDocumento = (int)reader["iddocumento"]; //*
                documento.NumDocumento = (string)reader["num_documento"];//*
                documento.Alta = (DateTime)reader["altaf"];//*
                documento.FechaRemito = (DateTime)reader["fecharemito"];//*
                documento.Concepto = (string)reader["concepto"];//*
                documento.Imputacion = (int)reader["imputacion"];//*
                documento.NotaRemito = (string)reader["nota_remito"];//*
                documento.IdEmpleado = (int)reader["idempleado"];//*                
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];//*
                documento.IdUsuario = (int)reader["idusuario"];
                documento.IdTransporte = (int)reader["idtransporte"];
                documento.ClienteObra = (string)reader["cliente"];
                documento.CuitObra = (string)reader["cuit_obra"];
                documento.NombreObra = (string)reader["nomobra"];
                documento.DirObra = (string)reader["direccion"];
                documento.Localidad = (string)reader["nomlocalidad"];
                documento.Provincia = (string)reader["nomprovincia"];
                documento.Chofer = (string)reader["chofer"];
                documento.NombreEmpleado = (string)reader["nombre"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.Transporte = (string)reader["transpo"];
                documento.TipoDocumento = (string)reader["codigo"];
                documento.IdEmpleadoDestino = (int)reader["idempleado_destino"];
                documento.NombreEmpledoDestino = (string)reader["empleadodestino"];
                documento.ImputacionDestino = (int)reader["imputacion_destino"];
                documento.NOmbreObraDestino = (string)reader["obradestino"];
                documento.ClienteDestino = (string)reader["clientedestino"];

                //zona de las fechas
                if (reader["baja"] != DBNull.Value)//*
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }
              }
            return documento;
            }


        #endregion

        #region ConsultasBASe
        public int ObtenerUltimaNumeracionPapel()
        {
            int _ultimoIdDoc = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Remito_Obtener_Ultima_Numeracion";
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _ultimoIdDoc = (int)reader["numeracion"];

                }

                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return _ultimoIdDoc;
        }
        #endregion


        public ObservableCollection<Documento>ListarDesdeHasta( DateTime _fdesde, DateTime _fhasta)
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Listar_Desde_Hasta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fdesde", _fdesde);
            cmd.Parameters.AddWithValue("@fhasta", _fhasta);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                documentos = ArmarListaDocumentos(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return documentos;
        }

        public ObservableCollection<Documento>ListarRemitosREVH()
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Todos_REVH";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                documentos = ArmarListaDocumentos(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return documentos;
        }

        public List<DocumentoDetalle> DetalleUnDocumentoREVH(int iddocumento)
        {
            List<DocumentoDetalle> documentos = new List<DocumentoDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Detalle_Vh_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddocumento", iddocumento);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                documentos = ArmarDetalleDocREVH(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return documentos;
        }
     

        #region[armado del remito]
        private List<DocumentoDetalle> ArmarDetalleDocREVH(SqlDataReader reader)
        {
            List<DocumentoDetalle> lista = new List<DocumentoDetalle>();
            while (reader.Read())
            {
                DocumentoDetalle detalle = new DocumentoDetalle();
                detalle.IdDocumento = (int)reader["iddocumento"];
                detalle.CodigoItem = (int)reader["codigo_item"];
                detalle.CantidadItem = (int)reader["cantidad_item"];
                detalle.Descripcion = (string)reader["descripcion"];
                detalle.CodIventario = (string)reader["dominio"];
                detalle.Modelo = (string)reader["modelo"];
                detalle.Marca = (string)reader["nombre_marca"];
                lista.Add(detalle);
            }
            return lista;
        }

        private ObservableCollection<Documento> ArmarListaDocumentos(SqlDataReader reader)
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();
            while (reader.Read())
            {
                Documento documento = new Documento();

                documento.IdDocumento = (int)reader["iddocumento"]; //*
                documento.NumDocumento = (string)reader["num_documento"];//*
                documento.Alta = (DateTime)reader["altaf"];//*
                documento.FechaRemito = (DateTime)reader["fecharemito"];//*
                documento.Concepto = (string)reader["concepto"];//*
                documento.Imputacion = (int)reader["imputacion"];//*
                documento.NotaRemito = (string)reader["nota_remito"];//*
                documento.IdEmpleado = (int)reader["idempleado"];//*                
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];//*
                documento.IdUsuario = (int)reader["idusuario"];
                documento.IdTransporte = (int)reader["idtransporte"];
                documento.ClienteObra = (string)reader["cliente"];
                documento.NombreEmpleado = (string)reader["nombre"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.RazonSocial = (string)reader["razonsocial"];
                documento.TipoDocumento = (string)reader["codigo"];
                

                //zona de las fechas
                if (reader["baja"] != DBNull.Value)//*
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }
              
                

              
                //finalmente agregamos el registro a la collection
                documentos.Add(documento);

            }
            return documentos;
        }
        #endregion


        #region RemitosCompras

        public ObservableCollection<Documento>RemitosProductosTodos()
        {
            ObservableCollection<Documento> lista_dip = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Mov_Productos";
            cmd.CommandType = CommandType.StoredProcedure;
            

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_dip = ArmarListaDip(reader);
                
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_dip;
        }

        private ObservableCollection<Documento> ArmarListaDip(SqlDataReader reader)
        {
            //lista de documentos de movimientos de productos al deposito : DIP y DDP
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();
            while (reader.Read())
            {
                Documento documento = new Documento();
                documento.IdDocumento = (int)reader["iddocumento"];
                documento.Alta = (DateTime)reader["altaf"];
                if (reader["baja"] != DBNull.Value)
                {
                    documento.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    documento.Baja = null;
                }
              
                documento.Concepto = (string)reader["concepto"];
                documento.CostoDocu = (decimal)reader["costo_docu"];
                documento.RemitoProveedor = (string)reader["remito_proveedor"];
                if (reader["fecha_rem_proveedor"]!= DBNull.Value)
                {
                    documento.FechaRemProveedor = (DateTime)reader["fecha_rem_proveedor"];
                }
                else
                {
                    documento.FechaRemProveedor = null;
                }
                
                documento.NumFacturaProveedor = (string)reader["num_factura_proveedor"];
                documento.ImporteFacturaProveedor = (decimal)reader["importe_factura"];
                if (reader["facturaf"]!= DBNull.Value)
                {
                    documento.FechaFacProveedor = (DateTime)reader["facturaf"];
                }
                else
                {
                    documento.FechaFacProveedor = null;
                }
              
                documento.NumeroOc = (string)reader["numero_oc"];
                documento.Registrado = (int)reader["registrado"];
                documento.IdDeposito = (int)reader["iddeposito"];
                documento.IdProve = (int)reader["idprove"];
                documento.IdUsuario = (int)reader["idusuario"];
                documento.Chofer = (string)reader["chofer"];
                documento.NotaRemito = (string)reader["nota_remito"];
                documento.FechaRemito = (DateTime)reader["fecharemito"];
                documento.IdTipoRem = (int)reader["idtiporem"];
                documento.IdEstado = (int)reader["idestado"];
                if (reader["iddepdestino"]!= DBNull.Value)
                {
                    documento.IdDepDestino = (int)reader["iddepdestino"];
                }
                else
                {
                    documento.IdDepDestino = 0;
                }
                
                documento.IdTransporte = (int)reader["idtransporte"];
                documento.RemDepOrigen = (string)reader["rem_dep_origen"];
                if (reader["fecha_rem_dep_origen"]!= DBNull.Value)
                {
                    documento.FechaRemDepOrigen = (DateTime)reader["fecha_rem_dep_origen"];
                }
                else
                {
                    documento.FechaRemDepOrigen = null;
                }
                
                documento.NombreDepOrigen = (string)reader["dep_origen"];
                documento.NombreDepDestino = (string)reader["dep_destino"];
                documento.Transporte = (string)reader["transporte"];
                documento.RazonSocial = (string)reader["razonsocial"];
                documento.NombreUsuario = (string)reader["nomuser"];
                documento.IdEmpleado = (int)reader["idempleado"];
                documento.IdCasa = (int)reader["idcasa"];
                documento.Aclaraciones = (string)reader["aclaraciones"];
                documento.AutorizacionVale = (string)reader["autorizacion_vale"];
                documento.IdCausaDescuento = (int)reader["idcausadescuento"];
                documento.RetiraPersonal = (int)reader["retira_personal"];
                lista.Add(documento);
            }
            return lista;
        }

        public ObservableCollection<DocumentoDetalle> ListarDetallesDIP()
        {
            ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documento_Detalle_Pro_DIP";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DocumentoDetalle detalle = new DocumentoDetalle();
                    detalle.IdDetDocu = (int)reader["iddetdocu"];
                    detalle.IdDocumento = (int)reader["iddocumento"];
                    detalle.CodigoItem = (int)reader["codigo_item"];
                    detalle.CantidadItem = (int)reader["cantidad_item"];
                    detalle.CodIventario = (string)reader["cod_inventario"];
                    detalle.Descripcion = (string)reader["nombre"];
                    detalle.EstadoItem = (int)reader["estado_item"];
                    detalle.Marca = (string)reader["nombre_marca"];
                    detalle.Modelo = (string)reader["modelo"];
                    detalle.NumSerio = (string)reader["numserie"];
                    detalle.PrecioItem = (decimal)reader["precio_item"];
                    detalle.StockDisponible = 0;
                    detalle.TipoItem = (int)reader["tipo_item"];
                    documentoDetalles.Add(detalle);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return documentoDetalles;
        }

        #endregion


        #region Casas
        public List<Casa>CasasDeUnaObra(int _imputacion)
        {
            List<Casa> casas = new List<Casa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Casas_De_Una_Obra";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Casa casa = new Casa();
                    casa.IdCasa = (int)reader["idcasa"];
                    casa.Direccion = (string)reader["direccion"];
                    casas.Add(casa);
                }

                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return casas;
        }

        public Casa CasaBuscarPorId(int _idcasa)
        {
            Casa casa = new Casa();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Casa_Datos_Una";
            cmd.Parameters.AddWithValue("@idcasa", _idcasa);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    casa.IdCasa = (int)reader["idcasa"];
                    casa.Direccion = (string)reader["direccion"];
                    casa.Descripcion = (string)reader["descripcion"];
                    casa.AltaF = (DateTime)reader["altaf"];
                    casa.BajaF = (DateTime)reader["bajaf"];
                    casa.IdLocalidad = (int)reader["idlocalidad"];
                    casa.Nota = (string)reader["nota"];
                    casa.Activo = (int)reader["estado"];
                    casa.InicioAlquiler = (DateTime)reader["inicio_alquiler"];
                    casa.FinAlquiler = (DateTime)reader["fin_alquiler"];
                    casa.Imputacion = (int)reader["imputacion"];
                    casa.CostoAlquiler = (decimal)reader["costo_alquiler"];
                    casa.CostoAcumulado = (decimal)reader["costo_acumulado"];
                }

                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }

            return casa;

        }
        #endregion


        #region REmitosUnProducto

        public ObservableCollection<VisorDocObras>RemitosObraUnProducto(int idproducto)
        {
            ObservableCollection<VisorDocObras> lista_visor = new ObservableCollection<VisorDocObras>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Visor_Doc_Obras";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VisorDocObras visor = new VisorDocObras();
                    visor.IdDetDocu = (int)reader["iddetdocu"];
                    visor.IdDocumento = (int)reader["iddocumento"];
                    visor.IdProducto = (int)reader["codigo_item"];
                    visor.Cantidad = (int)reader["cantidad_item"];
                    visor.FechaRemito = (DateTime)reader["fecharemito"];
                    visor.Concepto = (string)reader["concepto"];
                    visor.Imputacion = (int)reader["imputacion"];
                    visor.IdEmpleado = (int)reader["idempleado"];
                    visor.NumDocumento = (string)reader["num_documento"];
                    visor.NomProducto = (string)reader["nompro"];
                    visor.Descripcion = (string)reader["descripcion"];
                    visor.CodIventario = (string)reader["cod_inventario"];
                    visor.Modelo = (string)reader["modelo"];
                    visor.Marca = (string)reader["nom_marca"];
                    visor.NombreEmpleado = (string)reader["nombre"];
                    visor.NombreObra = (string)reader["nomobra"];
                    lista_visor.Add(visor);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return lista_visor;
        }

        #endregion

        #region REmitosIndumentaria
        public ObservableCollection<Documento>ListarDocIndumentaria()
        {
            ObservableCollection<Documento> lista_dsiddi = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
        cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documentos_Mov_Indumentaria";
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
               lista_dsiddi = ArmarListaDocIndumentaria(reader);

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_dsiddi;

        }
        private ObservableCollection<Documento> ArmarListaDocIndumentaria(SqlDataReader reader)
        {
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();
            while (reader.Read())
            {
                Documento dc = new Documento();
                dc.IdDocumento = (int)reader["iddocumento"];
                dc.Alta = (DateTime)reader["altaf"];
                if (reader["baja"] != DBNull.Value)
                {
                    dc.Baja = (DateTime)reader["baja"];
                }
                else
                {
                    dc.Baja = null;
                }
                dc.Concepto = (string)reader["concepto"];
                dc.Imputacion = (int)reader["imputacion"];
                dc.CostoDocu = (decimal)reader["costo_docu"];
                dc.IdEmpleado = (int)reader["idempleado"];
                dc.IdDeposito = (int)reader["iddeposito"];
                dc.NombreDepOrigen = (string)reader["dep_origen"];
                dc.IdUsuario = (int)reader["idusuario"];
                dc.Chofer = (string)reader["chofer"];
                dc.NotaRemito = (string)reader["nota_remito"];
                if (reader["fecharemito"]!= DBNull.Value)
                {
                    dc.FechaRemito = (DateTime)reader["fecharemito"];
                }
                else { 
                    dc.FechaRemito = null; 
                }
                
                dc.IdTipoRem = (int)reader["idtiporem"];
                dc.IdEstado = (int)reader["idestado"];
                dc.IdTransporte = (int)reader["idtransporte"];
                dc.RetiraPersonal = (int)reader["retira_personal"];
                dc.NombreObra = (string)reader["nomobra"];
                dc.RazonSocial = (string)reader["razonsocial"];
                dc.NombreEmpleado = (string)reader["nombre"];

                int _dni = (int)reader["dni"];
                dc.DniEmpleado = _dni.ToString();
                dc.SectorEmpleado = (string)reader["nomsector"];

                dc.ClienteObra = (string)reader["cliente"];
                dc.DirObra = (string)reader["direccion"];
                dc.CuitObra = (string)reader["cuit_obra"];
                dc.Localidad = (string)reader["nomlocalidad"];
                dc.Provincia = (string)reader["nomprovincia"];
                dc.NombreEmpleado = (string)reader["nombre"];
                dc.NombreUsuario = (string)reader["nomuser"];
                lista.Add(dc);
            }
            return lista;
        }
        #endregion

        #region ValesConsumoDeposito
        public ObservableCollection<Documento> ListarVCP()
        {
            ObservableCollection<Documento> lista_vcd = new ObservableCollection<Documento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Documentos_VCP";
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Documento dc = new Documento();
                    dc.IdDocumento = (int)reader["iddocumento"];
                    dc.Alta = (DateTime)reader["altaf"];
                    if (reader["baja"] != DBNull.Value)
                    {
                        dc.Baja = (DateTime)reader["baja"];
                    }
                    else
                    {
                        dc.Baja = null;
                    }
                    dc.Concepto = (string)reader["concepto"];
                    dc.Imputacion = (int)reader["imputacion"];
                    dc.CostoDocu = (decimal)reader["costo_docu"];
                    dc.IdEmpleado = (int)reader["idempleado"];
                    dc.IdDeposito = (int)reader["iddeposito"];
                    dc.NombreDepOrigen = (string)reader["nomdepo"];
                    dc.IdUsuario = (int)reader["idusuario"];
                    dc.Chofer = (string)reader["chofer"];
                    dc.NotaRemito = (string)reader["nota_remito"];
                    if (reader["fecharemito"] != DBNull.Value)
                    {
                        dc.FechaRemito = (DateTime)reader["fecharemito"];
                    }
                    else
                    {
                        dc.FechaRemito = null;
                    }

                    dc.IdTipoRem = (int)reader["idtiporem"];
                    dc.IdEstado = (int)reader["idestado"];
                    dc.IdTransporte = (int)reader["idtransporte"];
                    dc.RetiraPersonal = (int)reader["retira_personal"];
                   

                    
                    dc.NombreUsuario = (string)reader["nomuser"];
                    lista_vcd.Add(dc);

                }

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_vcd;

        }
        #endregion

        #region RemitosEmpleado
         
        public ObservableCollection<Documento>ListarRemitosUnEmpleado(int idempleado)
        {
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Empleado_Listar_Documentos_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idempleado", idempleado);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Documento documento = new Documento();
                    documento.IdDocumento = (int)reader["iddocumento"];
                    documento.Alta = (DateTime)reader["altaf"];
                    if (reader["baja"] != DBNull.Value)
                    {
                        documento.Baja = (DateTime)reader["baja"];
                    }
                    else
                    {
                        documento.Baja = null;
                    }
                    documento.Concepto = (string)reader["concepto"];
                    documento.IdEstado = (int)reader["idestado"];
                    documento.IdEmpleado = (int)reader["idempleado"];
                    documento.IdUsuario = (int)reader["idusuario"];
                    documento.IdTipoRem = (int)reader["idtiporem"];
                    documento.NumDocumento = (string)reader["num_documento"];
                    documento.IdTransporte = (int)reader["idtransporte"];
                    documento.Chofer = (string)reader["chofer"];
                    documento.Imputacion = (int)reader["imputacion"];
                    documento.NotaRemito = (string)reader["nota_remito"];
                    documento.FechaRemito = (DateTime)reader["fecharemito"];
                    documento.ImputacionDestino = (int)reader["imputacion_destino"];
                    documento.CostoDocu = (decimal)reader["costo_docu"];
                    documento.IdCasa = (int)reader["idcasa"];
                    documento.Transporte = (string)reader["transporte"];
                    documento.NombreObra = (string)reader["nomobra"];
                    documento.ClienteObra = (string)reader["cliente"];
                    documento.NombreEmpleado = (string)reader["nombre"];
                    documento.NombreUsuario = (string)reader["nomuser"];
                    documento.RazonSocial = (string)reader["razonsocial"];
                    documento.TipoDocumento=(string)reader["codigo"];
                    lista.Add(documento);
                }

                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        #endregion
    }
}
