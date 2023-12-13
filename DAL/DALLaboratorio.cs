using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALLaboratorio
    {
        #region Declarativas
        DALConexion conn = new DALConexion();

        #endregion

        public DALLaboratorio()
        {

        }

        #region Instrumentos



        //registra una calibracion en la bd
        public void NuevaCalibracionInstrumento(CalibracionInstrumento calibracion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Alta";
            cmd.Parameters.AddWithValue("@idproducto", calibracion.IdProducto);
            cmd.Parameters.AddWithValue("@fechacalibracion", calibracion.FechaUltimaCalibracion);
            cmd.Parameters.AddWithValue("@idprove", calibracion.IdProveedor);
            cmd.Parameters.AddWithValue("@certificado", calibracion.NumeroCertificado);
            cmd.Parameters.AddWithValue("@emisorcertificado", calibracion.EmisorCertificado);
            cmd.Parameters.AddWithValue("@nota", calibracion.Nota);
            cmd.Parameters.AddWithValue("@vencimientoactual", calibracion.VencimientoActual);
            cmd.Parameters.AddWithValue("@estado", calibracion.EstadoVencimiento);
            cmd.Parameters.AddWithValue("@resultado", calibracion.Resultado);
            cmd.Parameters.AddWithValue("@alta", calibracion.AltaRegistro);
            cmd.Parameters.AddWithValue("@validez", calibracion.ValidezDias);
            cmd.Parameters.AddWithValue("@codresultado", calibracion.Cod_Resultado);
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

        //busca los datos de una calibracion especifica
        public CalibracionInstrumento BuscarUnaCalibracionInstrumento(int idcalibracion)
        {
            CalibracionInstrumento calibracion = new CalibracionInstrumento();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Activa_Para_Un_Instrumento";
            cmd.Parameters.AddWithValue("@idcalibracion", idcalibracion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return calibracion;
        }

        //listar todas las calibraciones de un instrumento
        public List<CalibracionInstrumento> ListarTodasLasCalibracionesUnInstrumento(int idinstrumento)
        {
            List<CalibracionInstrumento> lista = new List<CalibracionInstrumento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibraciones_Un_Instrumento";
            cmd.Parameters.AddWithValue("@idproducto", idinstrumento);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CalibracionInstrumento calibracion = new CalibracionInstrumento();
                    calibracion.IdCalibracion = (int)reader["idinstrumento"];
                    calibracion.FechaUltimaCalibracion = (DateTime)reader["fecha_calibracion"];
                    calibracion.EmisorCertificado = (string)reader["emisor_certificado"];
                    calibracion.NumeroCertificado = (string)reader["certificado"];
                    calibracion.Resultado = (string)reader["resultado"];
                    calibracion.Nota = (string)reader["nota"];
                    calibracion.ValidezDias = (int)reader["rango_validez"];
                    calibracion.VencimientoActual = (DateTime)reader["vencimiento_actual"];
                    calibracion.EstadoVencimiento = (int)reader["estado_vencimiento"];
                    calibracion.EstadoCalibracion = (int)reader["estado_calibracion"];
                    calibracion.Cod_Resultado = (int)reader["cod_resultado"];
                    lista.Add(calibracion);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //listar instrumentos con vencimiento mes actual
        public List<Producto> CalibracionVenceMesActual(int mes)
        {
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@mes", mes);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //listar instrumento con vencimiento anio actual
        public List<Producto> CalibracionVenceAnioActual(int anio)
        {
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@anio", anio);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //listar las calibraciones realizadas por un laboratorio
        public List<CalibracionInstrumento> CalibracionesUnProveedor(int idproveedor)
        {
            List<CalibracionInstrumento> lista = new List<CalibracionInstrumento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibraciones_Listar_Por_Proveedor";
            cmd.Parameters.AddWithValue("@idprove", idproveedor);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CalibracionInstrumento calibracion = new CalibracionInstrumento();
                    calibracion.IdCalibracion = (int)reader["idinstrumento"];
                    calibracion.IdProducto = (int)reader["idproducto"];
                    calibracion.IdProveedor = (int)reader["idprove"];
                    calibracion.FechaUltimaCalibracion = (DateTime)reader["fecha_calibracion"];
                    calibracion.EmisorCertificado = (string)reader["emisor_certificado"];
                    calibracion.NumeroCertificado = (string)reader["certificado"];
                    calibracion.Resultado = (string)reader["resultado"];
                    calibracion.Nota = (string)reader["nota"];
                    calibracion.ValidezDias = (int)reader["rango_validez"];
                    calibracion.VencimientoActual = (DateTime)reader["vencimiento_actual"];
                    calibracion.EstadoVencimiento = (int)reader["estado_vencimiento"];
                    calibracion.OC = (string)reader["ordencompra"];
                    calibracion.ImporteCalibracion = (decimal)reader["importe_calibracion"];
                    calibracion.AltaRegistro = (DateTime)reader["alta_registro"];
                    lista.Add(calibracion);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //buscar un certificado de una calibracion
        public CalibracionInstrumento BuscarPorCertificado(string numerocertificado)
        {
            CalibracionInstrumento calibracion = new CalibracionInstrumento();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@numcertificado", numerocertificado);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return calibracion;
        }

        public void ActualizarEstadoInstrumento(int idproducto, string _estado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Actualiza_estado_Instrumento";
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@estado_calibracion", _estado);
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

        public void CalibracionesActualizarEstado(int idproducto, int estado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Actualiza_estado_Instrumento";
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            cmd.Parameters.AddWithValue("@estado", estado);
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

        public CalibracionInstrumento BuscarCalibracionActivaUnInstrumento(int idproducto)
        {
            CalibracionInstrumento calibracion = new CalibracionInstrumento();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Activa_Para_Un_Instrumento";
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    calibracion.IdCalibracion = (int)reader["idinstrumento"];
                    calibracion.FechaUltimaCalibracion = (DateTime)reader["fecha_calibracion"];
                    calibracion.EmisorCertificado = (string)reader["emisor_certificado"];
                    calibracion.NumeroCertificado = (string)reader["certificado"];
                    calibracion.Resultado = (string)reader["resultado"];
                    calibracion.Nota = (string)reader["nota"];
                    calibracion.ValidezDias = (int)reader["rango_validez"];
                    calibracion.VencimientoActual = (DateTime)reader["vencimiento_actual"];
                    calibracion.EstadoVencimiento = (int)reader["estado_vencimiento"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return calibracion;
        }

        public void BorrarCalibracion(int idcalibracion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibracion_Borrar_Una";
            cmd.Parameters.AddWithValue("@idcalibracion", idcalibracion);

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

        public ObservableCollection<CalibracionInstrumento> ListarCalibracionesUnInstrumento(int idproducto)
        {
            ObservableCollection<CalibracionInstrumento> lista = new ObservableCollection<CalibracionInstrumento>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Instrumentos_Calibraciones";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CalibracionInstrumento calibracion = new CalibracionInstrumento();
                    calibracion.IdProducto = (int)reader["idproducto"];
                    calibracion.NumeroCertificado = (string)reader["certificado"];
                    calibracion.EmisorCertificado = (string)reader["emisor_certificado"];

                    calibracion.Resultado = (string)reader["resultado"];
                    calibracion.ValidezDias = (int)reader["rango_validez"];
                    calibracion.OC = (string)reader["ordencompra"];
                    calibracion.Nota = (string)reader["nota"];
                    if (reader["fecha_calibracion"] == DBNull.Value)
                    {
                        calibracion.FechaUltimaCalibracion = null;
                    }
                    else
                    {
                        calibracion.FechaUltimaCalibracion = (DateTime)reader["fecha_calibracion"];
                    }
                    calibracion.RazonSocial = (string)reader["razonsocial"];
                    calibracion.EstadoVencimiento = (int)reader["estado_vencimiento"];
                    if (reader["vencimiento_actual"] == DBNull.Value)
                    {
                        calibracion.VencimientoActual = null;
                    }
                    else
                    {
                        calibracion.VencimientoActual = (DateTime)reader["vencimiento_actual"];
                    }

                    lista.Add(calibracion);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public ObservableCollection<Producto> ListarInstrumentos()
        {
            ObservableCollection<Producto> lista = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Instrumentos_Todos";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = (int)reader["idproducto"];
                    producto.Nombre = (string)reader["nombre"];
                    producto.CodInventario = (string)reader["cod_inventario"];
                    producto.Modelo = (string)reader["modelo"];
                    producto.NumSerie = (string)reader["numserie"];
                    producto.Marca = (string)reader["nombre_marca"];
                    if (reader["estado_calibracion"] == DBNull.Value)
                    {
                        producto.EstadoInstrumento = "No indica";
                    }
                    else
                    {
                        producto.EstadoInstrumento = (string)reader["estado_calibracion"];
                    }

                    producto.Escala = (string)reader["escala"];
                    producto.RangoMedicion = (string)reader["rango_medicion"];
                    producto.Exactitud = (string)reader["exactitud"];
                    producto.Patron = (int)reader["patron"];
                    if (producto.Patron == 1)
                    {
                        producto.EsPatron = "Si";
                    }
                    else
                    {
                        producto.EsPatron = "No";
                    }
                    producto.Situacion = (string)reader["nomsf"];
                    producto.Constrate = (int)reader["contraste"];
                    if (producto.Constrate == 1)
                    {
                        producto.NomConstraste = "Interno";
                    }
                    else
                    {
                        producto.NomConstraste = "Externo";
                    }

                    lista.Add(producto);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;

        }


        //listar instrumentos con calibracion vencida
        public ObservableCollection<Producto> ListarInstrumentosVencidos()
        {
            ObservableCollection<Producto> lista = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Calibraciones_Vencidas";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = (int)reader["idproducto"];
                    producto.Nombre = (string)reader["nombre"];
                    producto.CodInventario = (string)reader["cod_inventario"];
                    producto.Modelo = (string)reader["modelo"];
                    producto.NumSerie = (string)reader["numserie"];
                    producto.Marca = (string)reader["nombre_marca"];
                    if (reader["estado_calibracion"] == DBNull.Value)
                    {
                        producto.EstadoInstrumento = "No indica";
                    }
                    else
                    {
                        producto.EstadoInstrumento = (string)reader["estado_calibracion"];
                    }


                    producto.Situacion = (string)reader["nomsf"];

                    lista.Add(producto);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        //listar instrumentos con calibracion vencida
        public ObservableCollection<Producto> ListarInstrumentosPorVencer()
        {
            ObservableCollection<Producto> lista = new ObservableCollection<Producto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Calibraciones_Por_Vencer";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = (int)reader["idproducto"];
                    producto.Nombre = (string)reader["nombre"];
                    producto.CodInventario = (string)reader["cod_inventario"];
                    producto.Modelo = (string)reader["modelo"];
                    producto.NumSerie = (string)reader["numserie"];
                    producto.Marca = (string)reader["nombre_marca"];
                    if (reader["estado_calibracion"] == DBNull.Value)
                    {
                        producto.EstadoInstrumento = "No indica";
                    }
                    else
                    {
                        producto.EstadoInstrumento = (string)reader["estado_calibracion"];
                    }


                    producto.Situacion = (string)reader["nomsf"];

                    lista.Add(producto);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //actualiza la situacion de las fechas de vencimientos
        public void CalibracionesActualizarVencimientos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibraciones_Actualizar_Vencimientos";


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



        //actualiza el estado de situacion de un instrumento
        public void CalibracionesActualizarEstadoInstrumentos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Calibraciones_Actualizar_EstadoCalibraciones_Instrumentos";
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

       
    }
}
