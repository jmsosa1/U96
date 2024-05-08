using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using DAL;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
   public class BLLRemito
    {
        DALRemito dAL = new DALRemito();

        public BLLRemito()
        {

        }

        #region Remitos
        public int ObtenerUltimoIdDocumento()
        {
            int _ultimoIdDocu = dAL.ObtenerUltimoIdDocumento();
            return _ultimoIdDocu;
        }

        public void GrabarEncabezado(Documento documento)
        {
            dAL.GrabarEncabezado(documento);
        }

        public void GrabarDetalle(DocumentoDetalle detalle)
        {
            dAL.GrabarDetalle(detalle);
        }

        public ObservableCollection<Documento> ListarRemitosREVH()
        {
            ObservableCollection<Documento> documentos = dAL.ListarRemitosREVH();


            return documentos;
        }

        public List<DocumentoDetalle> DetalleUnDocumentoREVH(int iddocumento)
        {
            List<DocumentoDetalle> documentos = new List<DocumentoDetalle>();
            documentos = dAL.DetalleUnDocumentoREVH(iddocumento);

            return documentos;
        }

        public ObservableCollection<Documento> ListarDesdeHasta(DateTime _fdesde, DateTime _fhasta)
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();
            documentos = dAL.ListarDesdeHasta(_fdesde, _fhasta);

            return documentos;
        }

        public ObservableCollection<Documento> ListarDocObras(DateTime fdesde, DateTime fhasta)
        {
            ObservableCollection<Documento> lista = dAL.ListarDocObras(fdesde,fhasta);

            return lista;
        }

        public ObservableCollection<Documento> RemitosProductosTodos()
        {
            ObservableCollection<Documento> lista_dip = new ObservableCollection<Documento>();
            lista_dip = dAL.RemitosProductosTodos();
            return lista_dip;
        }

        public ObservableCollection<DocumentoDetalle> ListarDetallesDIP()
        {
            ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>();
            documentoDetalles = dAL.ListarDetallesDIP();
            return documentoDetalles;
        }
        public int ObtenerUltimaNumeracionPapel()
        {
            int _ultimoIdDoc = dAL.ObtenerUltimaNumeracionPapel();

            

            return _ultimoIdDoc;
        }


        public bool ValidarRemitoProveedor(int _idprove, string num_remito)
        {
            bool remito_ok = false;  //controla el resultado del login del usuario

            DALConexion conexion = new DALConexion(); // creamos la conexion 
            try
            {
                // creamos el comando y seteamos sus parametros 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Validar_Exitencia_Remito_Proveedor";
                cmd.Parameters.AddWithValue("@remito_provee", num_remito);
                cmd.Parameters.AddWithValue("@idprove", _idprove);

                SqlDataReader Reg = null;
                Reg = cmd.ExecuteReader();
                if (Reg.Read())
                {
                    remito_ok = true;

                }
                else
                {
                    remito_ok = false;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return remito_ok;
        }

        public bool ValidarFacturaProveedor(int _idprove, string num_factura)
        {
            bool remito_ok = false;  //controla el resultado del login del usuario

            DALConexion conexion = new DALConexion(); // creamos la conexion 
            try
            {
                // creamos el comando y seteamos sus parametros 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Validar_Exitencia_Factura_Proveedor";
                cmd.Parameters.AddWithValue("@factura_provee", num_factura);
                cmd.Parameters.AddWithValue("@idprove", _idprove);

                SqlDataReader Reg = null;
                Reg = cmd.ExecuteReader();
                if (Reg.Read())
                {
                    remito_ok = true;

                }
                else
                {
                    remito_ok = false;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return remito_ok;
        }


        public Documento BuscarUnDocumentoPorId(int _iddocu)
        {
            Documento d = dAL.BuscarUnDocumentoPorId(_iddocu);
            
            return d;
        }


        public Documento BuscarUnMOVPorId(int _iddocu)
        {
            Documento d =dAL.BuscarUnMOVPorId(_iddocu);
            return d;
           
        }

        public ObservableCollection<DocumentoDetalle> BuscarUnDocDetallePorId(int _iddocu)
        {
            ObservableCollection<DocumentoDetalle> documentoDetalles = dAL.BuscarUnDocDetallePorId(_iddocu);
           
            return documentoDetalles;
        }

        public ObservableCollection<VisorDocObras> RemitosObraUnProducto(int idproducto)
        {
            ObservableCollection<VisorDocObras> lista_visor = dAL.RemitosObraUnProducto(idproducto);
            return lista_visor;

          
        }

        public void AnularUnRemitoObra(int iddocumento)
        {
            dAL.AnularUnRemitoObra(iddocumento);
        }

       /* public void RestauracionItemRemitoObra(int _imputacion, int _idempleado, int _idproducto, int _cantidad, )
        {
            dAL.RestauracionItemRemitoObra(_imputacion, _idempleado, _idproducto, _cantidad, _concepto);
        }*/

        public ObservableCollection<Documento> ListarDoc14()
        {
            ObservableCollection<Documento> lista_dip = dAL.ListarDoc14();
           
            return lista_dip;
        }

        public ObservableCollection<Documento> ListarDocIndumentaria()
        {
            ObservableCollection<Documento> lista_dsiddi = dAL.ListarDocIndumentaria();
           
            return lista_dsiddi;

        }
        #endregion



        #region Casas
        public Casa CasaBuscarPorId(int _idcasa)
        {
            Casa casa = dAL.CasaBuscarPorId(_idcasa);


            return casa;

        }

        public List<Casa> CasasDeUnaObra(int _imputacion)
        {
            List<Casa> casas = new List<Casa>();
            casas = dAL.CasasDeUnaObra(_imputacion);

            return casas;
        }
        #endregion

        #region ValesConsumoDeposito
        public ObservableCollection<Documento> ListarVCP()
        {
            ObservableCollection<Documento> lista_vcd = dAL.ListarVCP();
           
            return lista_vcd;

        }
        #endregion

        #region RemitosEmpleado

        public ObservableCollection<Documento> ListarRemitosUnEmpleado(int idempleado)
        {
            ObservableCollection<Documento> lista = dAL.ListarRemitosUnEmpleado(idempleado);

           
            return lista;
        }

        #endregion
    }
}
