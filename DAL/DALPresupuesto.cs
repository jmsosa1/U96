using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPresupuesto
    {
        #region Declarativas
        readonly DALConexion conn = new DALConexion();

        #endregion
        public DALPresupuesto() { }

        #region ABM

        /// <summary>
        /// este metodo registra el encabezado del presupuesto que sera el actual activo
        /// </summary>
        /// <param name="presupuesto"></param>
        public void AltaEncabezado(Presupuesto presupuesto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "PresupuestoActualiza";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idpre", presupuesto.IdPre);
            cmd.Parameters.AddWithValue("@num", presupuesto.Numero);
            cmd.Parameters.AddWithValue("@falta", presupuesto.F_Alta);
            cmd.Parameters.AddWithValue("@estado", presupuesto.IdEstado);
            cmd.Parameters.AddWithValue("@titulo", presupuesto.Titulo);
            cmd.Parameters.AddWithValue("@descripcion", presupuesto.Descripcion);
            cmd.Parameters.AddWithValue("@version", presupuesto.Version);
            cmd.Parameters.AddWithValue("@fvencimiento", presupuesto.F_Vencimiento);
            cmd.Parameters.AddWithValue("@usuario", presupuesto.IdUsuario);
            cmd.Parameters.AddWithValue("@montototal", presupuesto.MontoTotalMonedaPpal);
            cmd.Parameters.AddWithValue("@mppal", presupuesto.MonedaPpal);
            cmd.Parameters.AddWithValue("@msec", presupuesto.MonedaSec);
            cmd.Parameters.AddWithValue("@adicional", presupuesto.Es_Adicional);
            cmd.Parameters.AddWithValue("@dolarpre", presupuesto.DolarPresupuesto);
            cmd.Parameters.AddWithValue("@anio", presupuesto.Anio);
            cmd.Parameters.AddWithValue("@poraprobado", presupuesto.PorcentajeAprobado);
            cmd.Parameters.AddWithValue("@tipopre", presupuesto.IdTipoPresupuesto);
            cmd.Parameters.AddWithValue("@situacion", presupuesto.IdSituacion);
            cmd.Parameters.AddWithValue("@fultimamodi", presupuesto.F_UltimaModificacion);
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// este metodo registra el detalle del presupuesto actual activo 
        /// </summary>
        /// <param name="presupuesto_item"></param>
        public void AltaDetalle(Presupuesto_Item presupuesto_item, int idusuario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "PresupuestoDetalleActualiza";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iditem", presupuesto_item.Id);
            cmd.Parameters.AddWithValue("@idpre", presupuesto_item.IdPre);
            cmd.Parameters.AddWithValue("@monto_presupuesto", presupuesto_item.Monto_Presupuestado);
            cmd.Parameters.AddWithValue("@monto_aprobado", presupuesto_item.Monto_Aprobado);
            cmd.Parameters.AddWithValue("@monto_real", presupuesto_item.Monto_Real_Ejecutado);
            cmd.Parameters.AddWithValue("@poraprobado",presupuesto_item.Porcentaje_aprobado);
            cmd.Parameters.AddWithValue("@tipo", presupuesto_item.IdTipo);
            cmd.Parameters.AddWithValue("@cate", presupuesto_item.IdCate);
            cmd.Parameters.AddWithValue("@alta", presupuesto_item.F_alta);
            cmd.Parameters.AddWithValue("@ultimamodi", presupuesto_item.UltimaModificacion);
            cmd.Parameters.AddWithValue("@codop", presupuesto_item.CodOp);
            cmd.Parameters.AddWithValue("@nombreop", presupuesto_item.Operacion);
            cmd.Parameters.AddWithValue("@usuario", idusuario);
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Actualizaciones
        /// <summary>
        /// Este Metodo lo que hace es que cada vez que se registra una compra, toma el dato del tipo de producto, la categoria y el importe
        /// y actualiza el item del detalle del presupuesto activo
        /// </summary>
        /// <param name="idtipo"></param>
        /// <param name="idcategoria"></param>
        /// <param name="importe"></param>
        public void ActualizarDetalleSegunCompraProducto( int idtipo, int idcategoria , decimal importe)
        { }

        /// <summary>
        /// este metodo es similar al de producto, solo que se aplica a compras de vehiculos
        /// </summary>
        /// <param name="idtipo"></param>
        /// <param name="idcategoria"></param>
        /// <param name="importe"></param>
        public void ActualizarDetalleSegunCompraVehiculo( int idtipo, int idcategoria, decimal importe)
        { }

        #endregion

        #region Bajas

        /// <summary>
        /// este metodo pone en inactivo a un presupuesto y su detalle. Solo debe haber un presupuesto activo
        /// </summary>
        /// <param name="idpresupuesto"></param>
        /// <param name="fechaBaja"></param>
        private void BajaPresupuesto(int idpresupuesto, DateTime fechaBaja)
        {

        }
        #endregion

        #region Listados
        /// <summary>
        /// este  metodo devuevle todos los presupuestos realizados, incluido el activo , los que estan abiertos ,los de baja
        /// </summary>
        /// <returns></returns>
         public List<Presupuesto>ListarTodosLosPresupuestos()
         {
            List<Presupuesto> lista = new List<Presupuesto> ();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Presupuesto_Listar_Todos";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Presupuesto presupuesto = new Presupuesto();
                    presupuesto.IdPre = (int)reader["idpre"];
                    presupuesto.Numero = (string)reader["numero"];
                    presupuesto.Version = (string)reader["num_version"];
                    presupuesto.Anio = (int)reader["anio"];
                    presupuesto.DolarPresupuesto = (decimal)reader["dolar_presupuesto"];
                    presupuesto.Titulo = (string)reader["titulo"];
                    presupuesto.F_Alta = (DateTime)reader["f_alta"];
                    presupuesto.MontoTotalMonedaPpal = (decimal)reader["monto_total_moneda_ppal"];
                    presupuesto.F_UltimaModificacion = (DateTime)reader["f_ultimamodi"];
                    presupuesto.UsuarioUMD = (string)reader["nomuser"];
                    presupuesto.Estado = (string)reader["estadopre"];
                    presupuesto.Situacion = (string)reader["situacionpre"];
                    presupuesto.NomMonedaPpal = (string)reader["monppal"];
                    presupuesto.NomMonedaSec = (string)reader["monsec"];
                    presupuesto.TipoPresupuesto = (string)reader["tipopresupuesto"];
                    presupuesto.MontoTotalMonedaSec = presupuesto.MontoTotalMonedaPpal / presupuesto.DolarPresupuesto;
                    lista.Add(presupuesto);

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public ObservableCollection<Presupuesto_Item>ListarDetalleUnPresupuesto(int idpresupuesto, int tipopre)
        {
            ObservableCollection<Presupuesto_Item> lista = new ObservableCollection<Presupuesto_Item> ();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "PresupuestoDetalleUno";
            cmd.CommandType  = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idpre", idpresupuesto);
            cmd.Parameters.AddWithValue("@tipo_pre", tipopre);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                { 
                    Presupuesto_Item item = new Presupuesto_Item();
                    item.IdPre = (int)reader["id"];
                    item.IdCate = (int)reader["idcate"];
                    item.IdPre = (int)reader["idpre"];
                    item.F_alta = (DateTime)reader["f_alta"];
                    item.IdTipo = (int)reader["idtipo"];
                    item.CodOp = (int)reader["cod_op"];
                    item.Operacion = (string)reader["nombre_op"];
                    item.UltimaModificacion = (DateTime)reader["f_ultimamodi"];
                    item.Monto_Presupuestado = (decimal)reader["monto_presupuestado"];
                    item.Monto_Aprobado = (decimal)reader["monto_aprobado"];
                    item.Monto_Real_Ejecutado = (decimal)reader["monto_real_ejecutado"];
                    item.Porcentaje_aprobado = (decimal)reader["prtj_aprobado"];
                  
                    item.NomCate = (string)reader["categoria"];

                    item.NomTipo = (string)reader["nomtipo"];
                    lista.Add(item);
                    
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public Presupuesto BuscarUnPresupuesto(int idpre)
        {
            Presupuesto presupuesto = new Presupuesto();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "PresupuestoBuscarUno";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idpre", idpre);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    presupuesto.IdPre = (int)reader["idpre"];
                    presupuesto.Numero = (string)reader["numero"];
                    presupuesto.Anio = (int)reader["anio"];
                    presupuesto.IdTipoPresupuesto = (int)reader["idtipo_pre"];
                    presupuesto.TipoPresupuesto = (string)reader["tipo_pre"];
                    presupuesto.DolarPresupuesto = (decimal)reader["dolar_presupuesto"];
                    presupuesto.DesviacionPresupuesto = (int)reader[""];
                    presupuesto.Es_Adicional = (int)reader["es_adicional"];
                    presupuesto.F_Alta = (DateTime)reader["f_alta"];
                    presupuesto.Titulo = (string)reader["titulo"];
                    presupuesto.Descripcion = (string)reader["descripcion"];
                    presupuesto.F_Baja = (DateTime)reader["f_baja"];
                    // presupuesto.F_UltimaModificacion = (DateTime)reader[""];
                    presupuesto.F_Vencimiento = (DateTime)reader["f_vencimiento"];
                    presupuesto.IdEstado = (int)reader["idestado"];
                    presupuesto.IdSituacion = (int)reader["idsituacion"];
                    presupuesto.IdUsuario = (int)reader["idusario"];
                    // presupuesto.UsuarioUMD = (string)reader[""];
                    presupuesto.MonedaPpal = (int)reader["moneda_ppal"];
                    presupuesto.MonedaSec = (int)reader["moneda_sec"];

                    presupuesto.MontoTotalMonedaPpal = (decimal)reader["monto_total_moneda_ppal"];

                    presupuesto.PorcentajeAprobado = (decimal)reader["prtj_aprobado"];
                    presupuesto.Version = (string)reader["num_version"];
                    presupuesto.Situacion = (string)reader["situacion"];

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return presupuesto;
        }
        #endregion
    }
}
