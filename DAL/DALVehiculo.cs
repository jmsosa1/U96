using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.ObjectModel;
using System.IO;

namespace DAL
{
    public class DALVehiculo
    {
        DALConexion conexion = new DALConexion();



        public DALVehiculo()
        { }



        #region[ABM DATOS]
        public int VehiculoAlta(Vehiculo vehiculo)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta";

            cmd.Parameters.AddWithValue("@descripcion", vehiculo.Descripcion);
            cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
            cmd.Parameters.AddWithValue("@dominio", vehiculo.Dominio);
            cmd.Parameters.AddWithValue("@numchasis", vehiculo.NumeroChasis);
            cmd.Parameters.AddWithValue("@nummotor", vehiculo.NumeroMotor);
            cmd.Parameters.AddWithValue("@color", vehiculo.Color);
            cmd.Parameters.AddWithValue("@animodelo", vehiculo.AnioModelo);
            cmd.Parameters.AddWithValue("@cilindrada", vehiculo.Cilindrada);
            cmd.Parameters.AddWithValue("@ndelatero", vehiculo.NeuDelantero);
            cmd.Parameters.AddWithValue("@ntrasero", vehiculo.NeuTrasero);
            cmd.Parameters.AddWithValue("@fechacompra", vehiculo.FechaCompra);
            cmd.Parameters.AddWithValue("@valorcompra", vehiculo.ValorCompra);
            cmd.Parameters.AddWithValue("@altaf", vehiculo.AltaF);
            cmd.Parameters.AddWithValue("@garantia", vehiculo.Garantia);

            cmd.Parameters.AddWithValue("@valorhora", vehiculo.ValorHora);
            cmd.Parameters.AddWithValue("@valorkm", vehiculo.ValorKm);
            cmd.Parameters.AddWithValue("@idcombustible", vehiculo.IdCombustible);
            cmd.Parameters.AddWithValue("@cantiejes", vehiculo.CantiEjes);
            cmd.Parameters.AddWithValue("@rastreo", vehiculo.RSat);
            cmd.Parameters.AddWithValue("@seguimiento", vehiculo.SegSat);
            cmd.Parameters.AddWithValue("@idmarca", vehiculo.IdMarca);
            cmd.Parameters.AddWithValue("@idcate", vehiculo.IdCate);
            cmd.Parameters.AddWithValue("@idlinea", vehiculo.IdLinea);
            cmd.Parameters.AddWithValue("@estado", vehiculo.Estado);
            cmd.Parameters.AddWithValue("@kminicial", vehiculo.KmInicial);
            cmd.Parameters.AddWithValue("@hsinicial", vehiculo.HsInicial);
            cmd.Parameters.AddWithValue("@codigo_radio", vehiculo.CodigoRadio);
            cmd.Parameters.AddWithValue("@km_distribucion", vehiculo.KmKitDistribucion);
            //parametros agregados el 3/3/2020
            cmd.Parameters.AddWithValue("@largo", vehiculo.Largo);
            cmd.Parameters.AddWithValue("@ancho", vehiculo.Ancho);
            cmd.Parameters.AddWithValue("@alto", vehiculo.Alto);
            cmd.Parameters.AddWithValue("@peso", vehiculo.Peso);
            cmd.Parameters.AddWithValue("@carga", vehiculo.CargaMaxima);
            cmd.Parameters.AddWithValue("@volumen", vehiculo.VolumenCarga);
            cmd.Parameters.AddWithValue("@superficie", vehiculo.SuperficieCarga);
            cmd.Parameters.AddWithValue("@costorepo", vehiculo.CostoReposicionDls);


            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public int VehiculoActualiza(Vehiculo vehiculo)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualiza";

            cmd.Parameters.AddWithValue("@idvh", vehiculo.IdVh);
            cmd.Parameters.AddWithValue("@descripcion", vehiculo.Descripcion);
            cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
            cmd.Parameters.AddWithValue("@dominio", vehiculo.Dominio);
            cmd.Parameters.AddWithValue("@numchasis", vehiculo.NumeroChasis);
            cmd.Parameters.AddWithValue("@nummotor", vehiculo.NumeroMotor);
            cmd.Parameters.AddWithValue("@color", vehiculo.Color);
            cmd.Parameters.AddWithValue("@animodelo", vehiculo.AnioModelo);
            cmd.Parameters.AddWithValue("@cilindrada", vehiculo.Cilindrada);
            cmd.Parameters.AddWithValue("@ndelantero", vehiculo.NeuDelantero);
            cmd.Parameters.AddWithValue("@ntrasero", vehiculo.NeuTrasero);
            cmd.Parameters.AddWithValue("@fechacompra", vehiculo.FechaCompra);
            cmd.Parameters.AddWithValue("@valorcompra", vehiculo.ValorCompra);
            cmd.Parameters.AddWithValue("@garantia", vehiculo.Garantia);
            cmd.Parameters.AddWithValue("@litrohora", vehiculo.LitroHora);
            cmd.Parameters.AddWithValue("@valorhora", vehiculo.ValorHora);
            cmd.Parameters.AddWithValue("@valorkm", vehiculo.ValorKm);
            cmd.Parameters.AddWithValue("@idcombustible", vehiculo.IdCombustible);
            cmd.Parameters.AddWithValue("@cantiejes", vehiculo.CantiEjes);
            cmd.Parameters.AddWithValue("@rastreo", vehiculo.RSat);
            cmd.Parameters.AddWithValue("@seguimiento", vehiculo.SegSat);
            cmd.Parameters.AddWithValue("@idmarca", vehiculo.IdMarca);
            cmd.Parameters.AddWithValue("@idcate", vehiculo.IdCate);
            cmd.Parameters.AddWithValue("@idlinea", vehiculo.IdLinea);
            cmd.Parameters.AddWithValue("@kminicial", vehiculo.KmInicial);
            cmd.Parameters.AddWithValue("@hsinicial", vehiculo.HsInicial);
            cmd.Parameters.AddWithValue("@kmlitro", vehiculo.KmLitro);
            cmd.Parameters.AddWithValue("@codigo_radio", vehiculo.CodigoRadio);
            cmd.Parameters.AddWithValue("@km_distribucion", vehiculo.KmKitDistribucion);
            //parametros agregados el 3/3/2020
            cmd.Parameters.AddWithValue("@largo", vehiculo.Largo);
            cmd.Parameters.AddWithValue("@ancho", vehiculo.Ancho);
            cmd.Parameters.AddWithValue("@alto", vehiculo.Alto);
            cmd.Parameters.AddWithValue("@peso", vehiculo.Peso);
            cmd.Parameters.AddWithValue("@carga", vehiculo.CargaMaxima);
            cmd.Parameters.AddWithValue("@volumen", vehiculo.VolumenCarga);
            cmd.Parameters.AddWithValue("@superficie", vehiculo.SuperficieCarga);
            cmd.Parameters.AddWithValue("@costorepo", vehiculo.CostoReposicionDls);


            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }

        public int VehiculoValidarDominio(string dominio_a_buscar)
        {
            int filaResultado;
            string _dominio = dominio_a_buscar;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Validar_Dominio";
            cmd.Parameters.AddWithValue("@dominio", _dominio);
            try
            {
                conexion.AbriConexion();
                filaResultado = Convert.ToInt16(cmd.ExecuteScalar());
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return filaResultado;
        }

        public void VehiculoBaja(int idvh, int idcausa, DateTime fecha,int idusuario, string causa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Baja";
            cmd.Parameters.AddWithValue("@idvh", idvh);
            cmd.Parameters.AddWithValue("@idcausa", idcausa);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@idusuario", idusuario);
            cmd.Parameters.AddWithValue("@causa", causa);
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

        public int VehiculoBorrarUno(int _idvh)
        {
            int filasAfectadas;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Borrar_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                filasAfectadas = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filasAfectadas;
        }
        #endregion

        #region[ABM CONSUMOS ]

        public void VehiculoActualizaHorasAcumuladas(int _idvh, decimal horas, int _imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualiza_HsAcumulada";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@hstrabajo", horas);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void VehiculoActualizaKmAcumulados(int _idvh, decimal kilometros, int _imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualiza_KmAcu";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@kmrecorridos", kilometros);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void VehiculoActualizaKmAcuMenos(int _idvh, decimal kilometros, int _imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualiza_KmAcu_Menos";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@kmrecorridos", kilometros);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void VehiculoActualizaHsAcuMenos(int _idvh, int horas, int _imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Actualiza_HsAcu_Menos";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@horas", horas);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void VehiculoActualizaDocVencida(Docu_vh docu_Vh)
        { }

        public void VehiculoActualizaAmortizacion(string dominio, decimal amortiza)
        { }

        public int RegistrarConsumo(ConsumoCombustible consumo)
        {
            int filaafectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Registrar_Consumo";
            cmd.Parameters.AddWithValue("@idvh", consumo.IdVh);
            cmd.Parameters.AddWithValue("@altaf", consumo.FechaReg);
            cmd.Parameters.AddWithValue("@meskm", consumo.Meskm);
            cmd.Parameters.AddWithValue("@kmrecorrido", consumo.KmRecorrido);
            cmd.Parameters.AddWithValue("@horastrabajo", consumo.HorasTrabajo);
            cmd.Parameters.AddWithValue("@imputacion", consumo.Imputacion);
            cmd.Parameters.AddWithValue("@anioconsumo", consumo.AnioConsumo);
            cmd.Parameters.AddWithValue("@preciolitro", consumo.PrecioLitro);
            cmd.Parameters.AddWithValue("@fechaconsumo", consumo.FechaConsumo);
            cmd.Parameters.AddWithValue("@litroscmb", consumo.LitrosCmbKM);
            cmd.Parameters.AddWithValue("@litroscmbhoras", consumo.LitrosCmbHoras);
            cmd.Parameters.AddWithValue("@costocmbkm", consumo.CostoCmbKm);
            cmd.Parameters.AddWithValue("@costocmbhoras", consumo.CostoCmbHoras);
            cmd.Parameters.AddWithValue("@costounidad", consumo.CostoUnidadConsumo);
            cmd.Parameters.AddWithValue("@costototalunidad", consumo.TotalCostoUnidadConsumo);



            try
            {
                conexion.AbriConexion();
                filaafectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaafectada;

        }


        public void RecalcularConsumoCombustibleKM(int _idvh, decimal _kmlitro, decimal _preciolitro)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Recalcular_CComb_KM";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@kmlitro", _kmlitro);
            cmd.Parameters.AddWithValue("@preciolitro", _preciolitro);
            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RecalcularConsumoCombustibleHS(int _idvh, decimal _hslitro, decimal _preciolitro)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Recalcular_CComb_HS";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            cmd.Parameters.AddWithValue("@hslitro", _hslitro);
            cmd.Parameters.AddWithValue("@preciolitro", _preciolitro);
            try
            {
                conexion.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal TotalKM_Consumo_Uno(int _idvh)
        {
            decimal totalConcepto;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Calcular_TotalesKM_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                object result = cmd.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                totalConcepto = Convert.ToDecimal(result);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return totalConcepto;
        }
        public decimal TotalHs_Consumo_Uno(int _idvh)
        {
            decimal totalConcepto;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Calcular_TotalesHs_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                object result = cmd.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                totalConcepto = Convert.ToDecimal(result);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return totalConcepto;

        }
        public decimal TotalLitros_Consumo_Uno(int _idvh)
        {
            decimal totalConcepto;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Calcular_TotalesLitros_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                object result = cmd.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                totalConcepto = Convert.ToDecimal(result);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return totalConcepto;
        }
        public decimal TotalCostoKM_Consumo_Uno(int _idvh)
        {
            decimal totalConcepto;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Calcular_TotalesCostoKM_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                object result = cmd.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                totalConcepto = Convert.ToDecimal(result);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return totalConcepto;
        }
        public decimal TotalCostoHs_Consumo_Uno(int _idvh)
        {
            decimal totalConcepto;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Calcular_TotalesCostoHS_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                object result = cmd.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                totalConcepto = Convert.ToDecimal(result);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return totalConcepto;
        }

        public int BorrarUnConsumo(int _idcmb)
        {
            int filaborrada = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Borrar_Un_Consumo";
            cmd.Parameters.AddWithValue("@idcmb", _idcmb);
            try
            {
                conexion.AbriConexion();
                filaborrada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return filaborrada;
        }

        public ACCombustible ResumenConsumosCombustiblesAnio(int _anio)
        {
            //metodo que devuelve los resultados del consumo de combustible en un año 
            ACCombustible data = new ACCombustible();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Consumo_combustible_costo_por_anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@total", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@totalcantidadhoras", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@totalcantidadkm", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@totallitroshoras", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@totallitroskm", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@consumomediohoras", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@consumomediokm", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@costohoras", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@costokm", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                cmd.ExecuteNonQuery();
                data.CostoTotalConsumoAnual = Convert.ToDecimal(cmd.Parameters["@total"].Value);
                data.CantidadHoras = Convert.ToInt32(cmd.Parameters["@totalcantidadhoras"].Value);
                data.CostoHoras = Convert.ToDecimal(cmd.Parameters["@costohoras"].Value);
                data.CantidadKm = Convert.ToInt32(cmd.Parameters["@totalcantidadkm"].Value);
                data.CostoKm = Convert.ToDecimal(cmd.Parameters["@costokm"].Value);
                data.AvgHoras = Convert.ToDecimal(cmd.Parameters["@consumomediohoras"].Value);
                data.AvgKm = Convert.ToDecimal(cmd.Parameters["@consumomediokm"].Value);
                data.LitrosHoras = Convert.ToInt32(cmd.Parameters["@totallitroshoras"].Value);
                data.LitrosKm = Convert.ToInt32(cmd.Parameters["@totallitroskm"].Value);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return data;
        }

        public List<ACCDetMesAnio> ResumenConsumoMensAnio(int _anio)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Consumo_Combustible_Costo_Por_Anio_Y_Mes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACCDetMesAnio detMesAnio = new ACCDetMesAnio();
                    detMesAnio.MesDelAnio = (int)reader["mes_consumo"];

                    detMesAnio.CCMesAnio = (decimal)reader["importe"];

                    resumen.Add(detMesAnio);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resumen;
        }

        public List<ConsumoAnios> ResumenConsumoAniovsAnio()
        {
            List<ConsumoAnios> lista_consumos = new List<ConsumoAnios>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Consumo_Combustible_Anio_Vs_Anio";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsumoAnios consumo = new ConsumoAnios();
                    consumo.Anio = (int)reader["anioconsumo"];
                    consumo.CostoAnio = (decimal)reader["costototal"];
                    lista_consumos.Add(consumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_consumos;
        }

        public List<ACC_CategoriaAnio> ResumenConsumoCategoriaAnio(int _anio)
        {
            List<ACC_CategoriaAnio> lista_categorias = new List<ACC_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Consumo_combustible_Costo_por_Anio_por_Categoria";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACC_CategoriaAnio aCC_ = new ACC_CategoriaAnio();
                    aCC_.NombreCategoria = (string)reader["nomcate"];
                    aCC_.CantidaConsumosCate = (int)reader["cantidavhcate"];
                    aCC_.CConsumoCategoria = (decimal)reader["costocategoria"];
                    lista_categorias.Add(aCC_);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_categorias;

        }

        public List<ACCDetMesAnio> ResumenConsumoVehiculo(int _anio, int _idvh)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Consumo_Combustible_Costo_Por_Vehiculo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@anio", _anio);

            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACCDetMesAnio detMesAnio = new ACCDetMesAnio();
                    detMesAnio.MesDelAnio = (int)reader["mes_consumo"];

                    detMesAnio.CCMesAnio = (decimal)reader["importe"];
                    detMesAnio.KmRegistrados = (decimal)reader["kmregistrados"];
                    detMesAnio.HsRegistradas = (decimal)reader["hsregistrada"];
                    detMesAnio.LtsConsumidosMes = (decimal)reader["ltscmb"];
                    resumen.Add(detMesAnio);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resumen;
        }

        #endregion

        #region[ABM BASE]

        public int CombustibleAlta(Combustible combustible)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Alta";
            cmd.Parameters.AddWithValue("@nombre", combustible.Descripcion);
            cmd.Parameters.AddWithValue("@plitroactual", combustible.PrecioLitroActual);
            cmd.Parameters.AddWithValue("@ultimamodi", combustible.UltimaModi);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int CombustibleModi(Combustible combustible)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Modifica_Uno";
            cmd.Parameters.AddWithValue("@idcmb", combustible.IdCombustible);
            cmd.Parameters.AddWithValue("@nombre", combustible.Descripcion);
            cmd.Parameters.AddWithValue("@plitroactual", combustible.PrecioLitroActual);
            cmd.Parameters.AddWithValue("@ultimamodi", combustible.UltimaModi);
            cmd.Parameters.AddWithValue("@plitroanterior", combustible.PrecioLitroAnterior);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int CombustibleBorrar(int idcmb)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Borra_Uno";
            cmd.Parameters.AddWithValue("idcmb", idcmb);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int CombustibleExisteEnVehiculo(int idcmb)
        {

            int filas = 0;
            int registrosafectados = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Existe_En_Vehiculo";
            cmd.Parameters.AddWithValue("@idcmb", idcmb);
            cmd.Parameters.Add("@cantidadfilas", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                registrosafectados = Convert.ToInt16(cmd.Parameters["@cantidadfilas"].Value);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return registrosafectados;
        }

        public int CombustibleAumentarPreciosTodos(decimal porcentaje)
        {
            int filas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Aumentar_PrecioLitro_Todos";
            cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
            cmd.Parameters.AddWithValue("@ultimamodi", DateTime.Today.Date);
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filas;
        }

        public int CombustibleBajaPreciosTodos(decimal porcentaje)
        {
            int filas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Combustible_Bajar_PrecioLitro_Todos";
            cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
            cmd.Parameters.AddWithValue("@ultimamodi", DateTime.Today.Date);
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filas;
        }


        public int VehiculoAgregarCategoria(CategoriaVh _cate)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_CategoriaVH_Alta";
            cmd.Parameters.AddWithValue("@nomcatevh", _cate.NomCate);
            cmd.Parameters.AddWithValue("@idtipovh", _cate.IdTipoVh);
            cmd.Parameters.AddWithValue("@cdp", _cate.CostoDiarioParo);
            cmd.Parameters.AddWithValue("@cdu", _cate.CostoDiarioUso);
            cmd.Parameters.AddWithValue("@cuc", _cate.CostoUnidadCategoria);
            cmd.Parameters.AddWithValue("@unidadcate", _cate.UnidadCate);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;

        }

        public int VehiculoModificarCategoria(CategoriaVh categoria)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_CategoriaVH_Modi";
            cmd.Parameters.AddWithValue("@nomcatevh", categoria.NomCate);
            cmd.Parameters.AddWithValue("@idcatevh", categoria.IdCateVh);
            cmd.Parameters.AddWithValue("@idtipovh", categoria.IdTipoVh);
            cmd.Parameters.AddWithValue("@cdp", categoria.CostoDiarioParo);
            cmd.Parameters.AddWithValue("@cdu", categoria.CostoDiarioUso);
            cmd.Parameters.AddWithValue("@cuc", categoria.CostoUnidadCategoria);
            cmd.Parameters.AddWithValue("@unidadcate", categoria.UnidadCate);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;

        }

        public int VehiculoBorraCategoria(int _idcate)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_CategoriaVH_Borrar";

            cmd.Parameters.AddWithValue("@idcatevh", _idcate);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int CategoriaExisteEnVehiculo(int _idcatevh)
        {
            int filas = 0;
            int registrosafectados = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Categoria_Existe_En_Vehiculo";
            cmd.Parameters.AddWithValue("@idcatevh", _idcatevh);
            cmd.Parameters.Add("@cantidadfilas", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                registrosafectados = Convert.ToInt16(cmd.Parameters["@cantidadfilas"].Value);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return registrosafectados;
        }


        public int VehiculoAgregarLinea(LineVh lineVh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_LineaVH_Alta";

            cmd.Parameters.AddWithValue("@idcatevh", lineVh.IdCateVh);
            cmd.Parameters.AddWithValue("@nomlineavh", lineVh.NomLineaVh);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int VehiculoModiLinea(LineVh lineVh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_LineaVH_Modi";

            cmd.Parameters.AddWithValue("@idlineavh", lineVh.IdLineaVh);
            cmd.Parameters.AddWithValue("@nomlineavh", lineVh.NomLineaVh);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int VehiculoBorrarLinea(int idlineavh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_LineaVH_Borrar" +
                "";

            cmd.Parameters.AddWithValue("@idlineavh", idlineavh);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int LineaExisteEnVehiculo(int _idlineavh)
        {
            int filas = 0;
            int registrosafectados = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_LineaVH_Existe_En_Vehiculo";
            cmd.Parameters.AddWithValue("@idlineavh", _idlineavh);
            cmd.Parameters.Add("@cantidadfilas", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                registrosafectados = Convert.ToInt16(cmd.Parameters["@cantidadfilas"].Value);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return registrosafectados;
        }

        public int MarcaAlta(MarcaVh _marcavh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_MarcaVH_Alta";
            cmd.Parameters.AddWithValue("@nombremarcavh", _marcavh.NombreMarca);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int MarcaModificar(MarcaVh _marcaVh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_MarcaVH_Modi";
            cmd.Parameters.AddWithValue("@nombremarcavh", _marcaVh.NombreMarca);
            cmd.Parameters.AddWithValue("@idmarca", _marcaVh.IdMarca);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }


        #endregion

        #region[ABM PLANIFICACION]

        public int VehiculoAltaPlanificacion(PlanificacionVH planificacionVH)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta_Planificacion";
            cmd.Parameters.AddWithValue("@idvh", planificacionVH.IdVh);
            cmd.Parameters.AddWithValue("@fdesde", planificacionVH.FDesde);
            cmd.Parameters.AddWithValue("@fhasta", planificacionVH.FHasta);
            cmd.Parameters.AddWithValue("@imputacion", planificacionVH.Imputacion);
            cmd.Parameters.AddWithValue("@solicitante", planificacionVH.Solicitante);
            cmd.Parameters.AddWithValue("@notas", planificacionVH.Notas);

            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public int VehiculoBajaPlanificacion(int _idplanificacion)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Baja_Planificacion";
            cmd.Parameters.AddWithValue("@idpl", _idplanificacion);

            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public int VehiculoAnulaPLanificacion(int _idpl, string _notab, DateTime _fbaja)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Anular_Planificacion";
            cmd.Parameters.AddWithValue("@idpl", _idpl);
            cmd.Parameters.AddWithValue("@notabaja", _notab);
            cmd.Parameters.AddWithValue("@fbaja", _fbaja);


            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }

        public bool VehiculoCambioEstadoPlanificacion(int _idplanificacion, string _nuevoestado)
        {
            bool cambioestado = true;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Cambio_Estado_Planificacion";
            cmd.Parameters.AddWithValue("@idpl", _idplanificacion);
            cmd.Parameters.AddWithValue("@estadonuevo", _nuevoestado);

            try
            {
                conexion.AbriConexion();
                int filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
                if (filaAfectada == 1)
                {
                    cambioestado = true;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return cambioestado;
        }

        public int VehiculoModiPlanificacion(PlanificacionVH planificacionVH)
        {
            int resultado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta_Planificacion";
            cmd.Parameters.AddWithValue("@idvh", planificacionVH.IdVh);
            cmd.Parameters.AddWithValue("@fdesde", planificacionVH.FDesde);
            cmd.Parameters.AddWithValue("@fhasta", planificacionVH.FHasta);
            cmd.Parameters.AddWithValue("@imputacion", planificacionVH.Imputacion);
            cmd.Parameters.AddWithValue("@solicitante", planificacionVH.Solicitante);
            cmd.Parameters.AddWithValue("@notas", planificacionVH.Notas);

            try
            {
                conexion.AbriConexion();
                resultado = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }

        public bool ValidaFechasPlan(int _idvh, DateTime _fdesde, DateTime _fhasta)
        {
            bool fechasIncorrectas = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Validar_Fechas";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@fdesde", _fdesde);
            cmd.Parameters.AddWithValue("@fhasta", _fhasta);

            PlanificacionVH vH = new PlanificacionVH();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (reader.Read())
                {
                    vH.IdVh = (int)reader["idvh"];
                }
                conexion.CerrarConexion();
                if (vH.IdVh != 0)
                {
                    fechasIncorrectas = true;
                }

            }
            catch (Exception)
            {

                throw;
            }


            return fechasIncorrectas;

        }

        public int AltaAsignacion(Asignacion_vh asignacion_Vh)
        {
            int filaAfectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta_Asignacion";

            cmd.Parameters.AddWithValue("@idvh", asignacion_Vh.IdVh);
            cmd.Parameters.AddWithValue("@imputacion", asignacion_Vh.Imputacion);
            cmd.Parameters.AddWithValue("@idempleado", asignacion_Vh.IdEmpleado);
            cmd.Parameters.AddWithValue("@fechain", asignacion_Vh.FechaIn);
            cmd.Parameters.AddWithValue("@estado", asignacion_Vh.EstadoAsignacion);
            cmd.Parameters.AddWithValue("@situacion", asignacion_Vh.SituacionAsignacion);
            cmd.Parameters.AddWithValue("@idcate", asignacion_Vh.IdCatevh);
            cmd.Parameters.AddWithValue("@altaf", asignacion_Vh.AltaF);
            try
            {
                conexion.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;
        }

        public void BajaAsignacion(int _idasig, DateTime _fechafin)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Baja_Asignacion";
            cmd.Parameters.AddWithValue("@idasig", _idasig);
            cmd.Parameters.AddWithValue("@fechafin", _fechafin);

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
        public int BorrarAsignacion(int _idasig)
        {
            int filaAfectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Borra_Asignacion";
            cmd.Parameters.AddWithValue("@idasig", _idasig);


            try
            {
                conexion.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;
        }

        public void CambioSF(int _idvh, int _idsf)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Cambia_SF";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@nuevasf", _idsf);
            try
            {
                conexion.AbriConexion();
                int filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AltaBOVh(int _idvh, int _imputacion, int _idtipovh, int _idcatevh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idtipovh", _idtipovh);
            cmd.Parameters.AddWithValue("@idcatevh", _idcatevh);
            int filaAfectada;
            try
            {
                conexion.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;
        }

        public int AltaAutorizacion(Autorizacion_vh autorizacion_Vh)
        {
            int filaAfectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta_Autorizacion";

            cmd.Parameters.AddWithValue("@idvh", autorizacion_Vh.IdVh);
            cmd.Parameters.AddWithValue("@idempleado", autorizacion_Vh.IdEmpleado);
            cmd.Parameters.AddWithValue("@altaf", autorizacion_Vh.AltaF);
            cmd.Parameters.AddWithValue("@estado", autorizacion_Vh.Estado);
            cmd.Parameters.AddWithValue("@finicio", autorizacion_Vh.Finicio);
            try
            {
                conexion.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;
        }

        public void BorrarAutorizacion(int id) 
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Baja_Autorizacion";
            cmd.Parameters.AddWithValue("@idaut", id);
            

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

        #region[consultas base]
        public ObservableCollection<Vehiculo> VehiculosListarActivos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculos_Activos";

            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                vehiculos = ArmarListaVehiculos(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return vehiculos;
        }

        public List<Vehiculo> ListaParaRemision()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculos_Activos";

            List<Vehiculo> vehiculos = new List<Vehiculo>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                vehiculos = ArmarListaParaRemision(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return vehiculos;
        }

        //public ObservableCollection<Vehiculo> VehiculosListarBajas()
        //{ }

        public int ExisteBalanceObra(int _idvh, int _imputacion)
        {
            int filaAfectada;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Existe_Balance_Obra";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conexion.AbriConexion();
                filaAfectada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return filaAfectada;

        }

        public ObservableCollection<CategoriaVh> VehiculosListarCategoriaPorTipo(int idtipo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Listar_Categorias_Tipo";
            cmd.Parameters.AddWithValue("@idtipovh", idtipo);

            ObservableCollection<CategoriaVh> lista = new ObservableCollection<CategoriaVh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaVh cate = new CategoriaVh();
                    cate.IdCateVh = (int)reader["idcatevh"];
                    cate.NomCate = (string)reader["nomcate"];
                    cate.IdTipoVh = (int)reader["idtipovh"];
                    cate.CostoDiarioParo = (decimal)reader["costo_diario_parada"];
                    cate.CostoDiarioParo = (decimal)reader["costo_diario_uso"];
                    cate.CostoUnidadCategoria = (decimal)reader["costo_unidad_categoria"];
                    cate.UnidadCate = (string)reader["unidad_cate"];
                    lista.Add(cate);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<TipoVh> ListarTiposVh()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_listar_Tipos";
            List<TipoVh> tipoVhs = new List<TipoVh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TipoVh tipo = new TipoVh();
                    tipo.IdTipoVh = (int)reader["idtipovh"];
                    tipo.NomTipo = (string)reader["nomtipo"];
                    tipoVhs.Add(tipo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tipoVhs;
        }

        public ObservableCollection<Vehiculo> VehiculosListarPorCategorias(int _idcategoria)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculos_Por_Categoria";
            cmd.Parameters.AddWithValue("@idcate", _idcategoria);

            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                vehiculos = ArmarListaVehiculos(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return vehiculos;
        }

        public Vehiculo BuscarPorId(int _idvehiculo)
        {
            Vehiculo vehiculo = new Vehiculo();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Buscar_Id";
            cmd.Parameters.AddWithValue("@idvh", _idvehiculo);

            try
            {
                conexion.AbriConexion();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                vehiculo = ArmarVehiculo(dataReader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return vehiculo;
        }

        public ObservableCollection<Vehiculo> VehiculoBuscarDominio(string dominio)
        {
            string _busca_dominio = dominio + "%";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Buscar_Dominio";
            cmd.Parameters.AddWithValue("@dominio", _busca_dominio);
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                vehiculos = ArmarListaVehiculos(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return vehiculos;
        }

        public Vehiculo VehiculoBuscarUnDominio(string _dominio)
        {
            string _busca_dominio = _dominio;
            Vehiculo vehiculo = new Vehiculo();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Buscar_Dominio";
            cmd.Parameters.AddWithValue("@dominio", _busca_dominio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                vehiculo = ArmarVehiculo(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return vehiculo;
        }

        public List<Combustible> ListaCombustibles()
        {
            List<Combustible> combustibles = new List<Combustible>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Listar_Combustibles";
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();

                while (lectura.Read())
                {
                    Combustible combustible = new Combustible();
                    combustible.IdCombustible = (int)lectura["idcombustible"];
                    combustible.Descripcion = lectura["nombre"].ToString();
                    combustible.PrecioLitroActual = (decimal)lectura["preciolitroactual"];
                    combustible.PrecioLitroAnterior = (decimal)lectura["preciolitroanterior"];
                    combustible.UltimaModi = (DateTime)lectura["ultimamodi"];
                    combustibles.Add(combustible);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return combustibles;
        }

        public Combustible BuscarUnCombustible(int _idcmb)
        {
            Combustible combustible = new Combustible();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Buscar_Un_Combustible";
            cmd.Parameters.AddWithValue("@idcmb", _idcmb);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    combustible.IdCombustible = (int)reader["idcombustible"];
                    combustible.Descripcion = reader["nombre"].ToString();
                    combustible.PrecioLitroActual = (decimal)reader["preciolitroactual"];
                    combustible.PrecioLitroAnterior = (decimal)reader["preciolitroanterior"];
                    combustible.UltimaModi = (DateTime)reader["ultimamodi"];
                }

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return combustible;

        }

        public List<CategoriaVh> ListaCategoriaVh()
        {
            List<CategoriaVh> categorias = new List<CategoriaVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Categorias";
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();

                while (lectura.Read())
                {
                    CategoriaVh categoriaVh = new CategoriaVh();
                    categoriaVh.IdCateVh = (int)lectura["idcatevh"];
                    categoriaVh.NomCate = lectura["nomcate"].ToString();
                    categoriaVh.IdTipoVh = (int)lectura["idtipovh"];
                    categoriaVh.CostoDiarioParo = (decimal)lectura["costo_diario_parada"];
                    categoriaVh.CostoDiarioUso = (decimal)lectura["costo_diario_uso"];
                    categoriaVh.UnidadCate = (string)lectura["unidad_cate"];
                    categoriaVh.CostoUnidadCategoria = (decimal)lectura["costo_unidad_categoria"];
                    categorias.Add(categoriaVh);

                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return categorias;

        }

        public List<LineVh> ListarLineasVh(int idcatevh)
        {
            List<LineVh> lineVhs = new List<LineVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Lineas";
            cmd.Parameters.AddWithValue("@idcatevh", idcatevh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();

                while (lectura.Read())
                {
                    LineVh lineVh = new LineVh();
                    lineVh.IdLineaVh = (int)lectura["idlineavh"];
                    lineVh.NomLineaVh = lectura["nomlinea"].ToString();
                    lineVh.IdCateVh = (int)lectura["idcatevh"];
                    lineVhs.Add(lineVh);

                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lineVhs;
        }

        public List<MarcaVh> ListarMarcaVh()
        {
            List<MarcaVh> marcaVhs = new List<MarcaVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Marcas";
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                while (lectura.Read())
                {
                    MarcaVh marcaVh = new MarcaVh();
                    marcaVh.IdMarca = (int)lectura["idmarcavh"];
                    marcaVh.NombreMarca = lectura["nombre_marca"].ToString();
                    marcaVhs.Add(marcaVh);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return marcaVhs;
        }

        public List<Docu_vh> ListaDocuVh()
        {
            List<Docu_vh> docu_Vhs = new List<Docu_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                while (lectura.Read())
                {
                    Docu_vh docu = new Docu_vh();
                    docu.IdDocuVH = (int)lectura["iddocuvh"];
                    docu.Descripcion = lectura["nombredocu"].ToString();
                    docu_Vhs.Add(docu);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return docu_Vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarTodasAsignaciones()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Asignaciones_Todas";

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }


        public ObservableCollection<Asignacion_vh> ListarAsignaciones()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Asignaciones_Activas";

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesUno(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Buscar_Asignaciones_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }

        public int BuscarAsignacionActiva(int _idvh)
        {
            int fila; //id  de la asignacion activa de un vehiculo

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Buscar_Id_Asignacion_Activa";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                fila = Convert.ToInt32(cmd.ExecuteScalar());
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesPorCategoria(int _idcatevh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Asignaciones_Por_Categoria";
            cmd.Parameters.AddWithValue("@idcategoria", _idcatevh);

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesEnCursoPorCategoria(int _idcatevh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Asignaciones_EnCurso_Por_Categoria";
            cmd.Parameters.AddWithValue("@idcategoria", _idcatevh);

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesFinalizadas()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "pa_Vehiculo_Asignaciones_Finalizadas";

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                asignacion_Vhs = ArmarAsignaciones(lectura_reg);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return asignacion_Vhs;
        }

        public bool ExisteAsignacionActiva(int _idvh)
        {
            int fila;
            bool existe;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Existe_Asignacion_Activa";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();

                fila = Convert.ToInt32(cmd.ExecuteScalar());
                conexion.CerrarConexion();
                if (fila == 0)
                {
                    existe = false;
                }
                else { existe = true; }

            }
            catch (Exception)
            {

                throw;
            }
            return existe;

        }

        public ObservableCollection<PlanificacionVH> ListarTodasPlanificaciones()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Programacion";

            ObservableCollection<PlanificacionVH> planificacionVHs = new ObservableCollection<PlanificacionVH>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                planificacionVHs = ArmarPlanificacion(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return planificacionVHs;
        }

        public ObservableCollection<ConsumoCombustible> ListarConsumosUnVehiculo(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Listar_Consumo_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            ObservableCollection<ConsumoCombustible> consumoCombustibles = new ObservableCollection<ConsumoCombustible>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                consumoCombustibles = ArmarConsumosCombustibles(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return consumoCombustibles;

        }

        public ObservableCollection<ConsumoCombustible> ListarTodosLosConsumos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Listar_Consumos";


            ObservableCollection<ConsumoCombustible> consumoCombustibles = new ObservableCollection<ConsumoCombustible>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                consumoCombustibles = ArmarConsumosCombustibles(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return consumoCombustibles;
        }

        public ObservableCollection<Autorizacion_vh> ListarAutorizacionesActivas()
        {
            ObservableCollection<Autorizacion_vh> autorizacion_Vhs = new ObservableCollection<Autorizacion_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Autorizaciones_Activas";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Autorizacion_vh autorizacion_ = new Autorizacion_vh();
                    autorizacion_.IdAut = (int)reader["idaut"];
                    autorizacion_.IdVh = (int)reader["idvh"];
                    autorizacion_.IdEmpleado = (int)reader["idempleado"];
                    autorizacion_.AltaF = (DateTime)reader["altaf"];
                    autorizacion_.Estado = (string)reader["estado_autorizacion"];
                    autorizacion_.NumAutorizacion = (string)reader["numautorizacion"];
                    autorizacion_.Valor = (int)reader["valor_autorizacion"];
                    autorizacion_.DominioVh = (string)reader["dominio"];
                    autorizacion_.Modelo = (string)reader["modelo"];
                    autorizacion_.NombreEmpleado = (string)reader["nombre"];
                    autorizacion_.Marca = (string)reader["nombre_marca"];
                    autorizacion_Vhs.Add(autorizacion_);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return autorizacion_Vhs;
        }

        public ObservableCollection<Autorizacion_vh> ListarAutorizacionesActivasUno(int _idvh)
        {
            ObservableCollection<Autorizacion_vh> autorizacion_Vhs = new ObservableCollection<Autorizacion_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Autorizaciones_Activas_Uno";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Autorizacion_vh autorizacion_ = new Autorizacion_vh();
                    autorizacion_.IdAut = (int)reader["idaut"];
                    autorizacion_.IdVh = (int)reader["idvh"];
                    autorizacion_.IdEmpleado = (int)reader["idempleado"];
                    autorizacion_.AltaF = (DateTime)reader["altaf"];
                    autorizacion_.Estado = (string)reader["estado_autorizacion"];
                    autorizacion_.NumAutorizacion = (string)reader["numautorizacion"];
                    autorizacion_.Valor = (int)reader["valor_autorizacion"];
                    autorizacion_.DominioVh = (string)reader["dominio"];
                    autorizacion_.Modelo = (string)reader["modelo"];
                    autorizacion_.NombreEmpleado = (string)reader["nombre"];
                    autorizacion_.Marca = (string)reader["nombre_marca"];
                    autorizacion_.DNI = (int)reader["dni"];
                    DateTime _finicio = (DateTime)reader["finicio"];
                    autorizacion_.Finicio = _finicio.Date;
                    autorizacion_Vhs.Add(autorizacion_);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return autorizacion_Vhs;
        }

        public List<Empleado> VehiculoEmpleadosAutorizados(int idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Empleados_Autorizados";
            cmd.Parameters.AddWithValue("@idvh", idvh);
            List<Empleado> listaEmpleado = new List<Empleado>();

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)reader["idempleado"];
                    empleado.Nombre = (string)reader["nombre"];
                    listaEmpleado.Add(empleado);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return listaEmpleado;
        }

        #endregion


        #region[mantenimientos]
        public int AltaMantenimiento(Mante_vh mante_Vh)
        {
            int filas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Alta";

            cmd.Parameters.AddWithValue("@idvh", mante_Vh.IdVh);
            cmd.Parameters.AddWithValue("@altaf", mante_Vh.AltaF);
            cmd.Parameters.AddWithValue("@idprove", mante_Vh.IdProve);
            cmd.Parameters.AddWithValue("@imputacion", mante_Vh.Imputacion);
            cmd.Parameters.AddWithValue("@numfac", mante_Vh.NumFactura);
            cmd.Parameters.AddWithValue("@numremito", mante_Vh.NumRemito);
            cmd.Parameters.AddWithValue("@oc", mante_Vh.OrdenCompra);
            cmd.Parameters.AddWithValue("@fechafac", mante_Vh.FechaFac);
            cmd.Parameters.AddWithValue("@fecharem", mante_Vh.FechaRem);
            cmd.Parameters.AddWithValue("@importe", mante_Vh.Importe);
            cmd.Parameters.AddWithValue("@kmmante", mante_Vh.KmMante);
            cmd.Parameters.AddWithValue("@hsmante", mante_Vh.HorasMante);
            cmd.Parameters.AddWithValue("@idempleado", mante_Vh.IdEmpleado);
            cmd.Parameters.AddWithValue("@tipomante", mante_Vh.TipoMante);
            cmd.Parameters.AddWithValue("@dominio", mante_Vh.Dominio);

            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;

            }

            return filas;
        }

        public void AltaDetaMante(DetManteVh detManteVh)
        {
            int filas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Alta_Detalle";

            cmd.Parameters.AddWithValue("@idmantevh", detManteVh.IdManteVh);
            cmd.Parameters.AddWithValue("@idcatemante", detManteVh.IdCateMante);
            cmd.Parameters.AddWithValue("@descrimante", detManteVh.DescriMante);
            cmd.Parameters.AddWithValue("@cantidad", detManteVh.Cantidad);
            cmd.Parameters.AddWithValue("@costo", detManteVh.CostoItem);

            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int AnularMantenimiento(int _idmantevh)
        {
            int filaBorrada = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Borrar";
            cmd.Parameters.AddWithValue("@idmantevh", _idmantevh);
            try
            {
                conexion.AbriConexion();
                filaBorrada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return filaBorrada;
        }

        public int AnularDetaMante(int _idmantevh)
        {
            int filaBorrada = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Borrar_Detalle";
            cmd.Parameters.AddWithValue("@idmantevh", _idmantevh);
            try
            {
                conexion.AbriConexion();
                filaBorrada = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return filaBorrada;
        }

        public ObservableCollection<Mante_vh> ListarTodosLosMantenimientos(DateTime f1, DateTime f2)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Todos_F1_F2";
            cmd.Parameters.AddWithValue("@f1", f1);
            cmd.Parameters.AddWithValue("@f2", f2);

            ObservableCollection<Mante_vh> mante_Vhs = new ObservableCollection<Mante_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                mante_Vhs = ArmarMantenimientosVh(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return mante_Vhs;
        }

        public ObservableCollection<Mante_vh> VehiculoListarTodosLosMantenimientosUno(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Uno_Total";
            cmd.Parameters.AddWithValue("@idvh", _idvh);


            ObservableCollection<Mante_vh> mante_Vhs = new ObservableCollection<Mante_vh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                mante_Vhs = ArmarMantenimientosVh(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return mante_Vhs;
        }

        public List<CategoriaManteVh> ListarCateMante()
        {

            List<CategoriaManteVh> listaCate = new List<CategoriaManteVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Listar_Cate_Mante";
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaManteVh categoria = new CategoriaManteVh();
                    categoria.IdCateMante = (int)reader["idcatemante"];
                    categoria.NomCate = (string)reader["nomcate"];
                    listaCate.Add(categoria);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return listaCate;
        }

        public Mante_vh BuscarUnMantenimiento(int _idmantevh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Buscar_Uno";
            cmd.Parameters.AddWithValue("@idmantevh", _idmantevh);
            Mante_vh mante_ = new Mante_vh();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                mante_ = ArmarUnMantenimiento(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return mante_;
        }

        public List<DetManteVh> ListarDetallesManteUno(int _idmantevh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Detalle_Uno";
            cmd.Parameters.AddWithValue("@idmantevh", _idmantevh);

            List<DetManteVh> detManteVhs = new List<DetManteVh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                detManteVhs = ArmarDetMante(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return detManteVhs;
        }

        public List<DetManteVh> ListarDetallesManteIdvh(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Detalle_Uno_Idvh";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            List<DetManteVh> detManteVhs = new List<DetManteVh>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                detManteVhs = ArmarDetManteIdvh(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return detManteVhs;
        }
        private List<DetManteVh> ArmarDetMante(SqlDataReader reader)
        {
            List<DetManteVh> vhs = new List<DetManteVh>();
            while (reader.Read())
            {
                DetManteVh det = new DetManteVh();
                det.IdDet = (int)reader["iddet"];
                det.IdManteVh = (int)reader["idmantevh"];
                det.IdCateMante = (int)reader["idcatemante"];
                det.DescriMante = (string)reader["descri_mante"];
                det.Cantidad = (int)reader["cantidad"];
                det.CostoItem = (decimal)reader["costo"];
                det.NomCateMante = (string)reader["nomcate"];
                det.IdProve = (int)reader["idprove"];
                det.RazonSocial = (string)reader["razonsocial"];
                if (reader["fechafac"] != DBNull.Value)
                {
                    det.FechaFac = (DateTime)reader["fechafac"];
                }
                else
                {
                    det.FechaFac = null;
                }

                vhs.Add(det);

            }
            return vhs;
        }

        private List<DetManteVh> ArmarDetManteIdvh(SqlDataReader reader)
        {
            List<DetManteVh> vhs = new List<DetManteVh>();
            while (reader.Read())
            {
                DetManteVh det = new DetManteVh();
                det.IdDet = (int)reader["iddet"];
                det.IdManteVh = (int)reader["idmantevh"];
                det.IdCateMante = (int)reader["idcatemante"];
                det.DescriMante = (string)reader["descri_mante"];
                det.Cantidad = (int)reader["cantidad"];
                det.CostoItem = (decimal)reader["costo"];
                det.NomCateMante = (string)reader["nomcate"];
                det.IdProve = (int)reader["idprove"];
                det.RazonSocial = (string)reader["razonsocial"];
                if (reader["fechafac"] != DBNull.Value)
                {
                    det.FechaFac = (DateTime)reader["fechafac"];
                }
                else
                {
                    det.FechaFac = null;
                }
                det.KmDetMante = (decimal)reader["kmmante"];
                det.HsDetMante = (int)reader["horasmante"];
                vhs.Add(det);
            }
            return vhs;
        }

        public Mante_vh ObtenerUltimoIdMante()
        {
            Mante_vh ultimomante = new Mante_vh();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Ultimo_Registro";
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ultimomante.IdManteVh = (int)reader["idmantevh"];

                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return ultimomante;
        }

        public List<ConsumoAnios> ResumenManteAniovsAnio()
        {
            List<ConsumoAnios> lista_consumos = new List<ConsumoAnios>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculos_Mante_Total_Por_Anios";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsumoAnios consumo = new ConsumoAnios();
                    consumo.Anio = (int)reader["ANIO"];
                    consumo.CostoAnio = (decimal)reader["IMPORTE"];
                    lista_consumos.Add(consumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_consumos;
        }
        public ObservableCollection<ConsumoAnios> ResumenManteVhAnioMesvsMes(int _anio)
        {
            ObservableCollection<ConsumoAnios> lista_consumos = new ObservableCollection<ConsumoAnios>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "pa_Vehiculo_Mante_Por_Mes_Anio_Total";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsumoAnios consumo = new ConsumoAnios();
                    consumo.Anio = (int)reader["MES"];
                    consumo.CostoAnio = (decimal)reader["IMPORTE"];
                    lista_consumos.Add(consumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_consumos;
        }

        public List<ACMVH_CategoriaAnio> ResumenMantevhCategoriasAnio(int _anio)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "pa_Vehiculo_Mante_Por_Cate_Total_Anio";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACMVH_CategoriaAnio aCMVH = new ACMVH_CategoriaAnio();
                    aCMVH.IdCateManteVh = (int)reader["idcatemante"];
                    aCMVH.NombreCategoria = (string)reader["nomcate"];
                    aCMVH.CantidadIncidencias = (int)reader["CantidadCategoria"];
                    aCMVH.CostoTotalCategoria = (decimal)reader["CostoCantidad"];
                    aCMVH.CostoPromedioCategoria = 0;
                    lista_costos.Add(aCMVH);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;
        }

        public List<ACMVH_CategoriaAnio> ResumenIncidenciasUnaCategoria(int _anio, int _idcategoria)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculos_Costo_Mante_Por_Incidencias";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@idcatevh", _idcategoria);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACMVH_CategoriaAnio aCMVH = new ACMVH_CategoriaAnio();
                    aCMVH.IdCateManteVh = 0;
                    aCMVH.NombreCategoria = (string)reader["nomcate"];
                    aCMVH.CantidadIncidencias = (int)reader["CantidadCategoria"];
                    aCMVH.CostoTotalCategoria = (decimal)reader["CostoCantidad"];
                    aCMVH.CostoPromedioCategoria = 0;
                    lista_costos.Add(aCMVH);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;
        }

        public List<ACMVH_CategoriaAnio> ResumenManteVhCategoriasAnioUnVh(int _idvh, int _anio)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculo_Mante_Anual_PorCategorias";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACMVH_CategoriaAnio aCMVH = new ACMVH_CategoriaAnio();
                    aCMVH.IdCateManteVh = 0;
                    aCMVH.NombreCategoria = (string)reader["nomcate"];
                    aCMVH.CantidadIncidencias = (int)reader["CantidadCategoria"];
                    aCMVH.CostoTotalCategoria = (decimal)reader["CostoCantidad"];
                    aCMVH.CostoPromedioCategoria = 0;
                    lista_costos.Add(aCMVH);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;
        }

        public List<ACCDetMesAnio> ResumenManteVhMesAnioUnVh(int _idvh, int _anio)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculos_Mante_Mensual_Anio_UnVh";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@anio", _anio);

            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACCDetMesAnio detMesAnio = new ACCDetMesAnio();
                    detMesAnio.MesDelAnio = (int)reader["MES"];

                    detMesAnio.CCMesAnio = (decimal)reader["IMPORTE"];

                    resumen.Add(detMesAnio);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return resumen;
        }

        public List<ACMVH_CategoriaAnio> ResumenManteVhCategoriaVhMesAnio(int _anio, int _mes)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculos_Costo_Mante_Cate_Mensual_Anio";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@mes", _mes);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACMVH_CategoriaAnio aCMVH = new ACMVH_CategoriaAnio();
                    aCMVH.IdCateManteVh = (int)reader["idcatevh"];
                    aCMVH.NombreCategoria = (string)reader["nomcate"];
                    aCMVH.CantidadIncidencias = (int)reader["CantidadCategoria"];
                    aCMVH.CostoTotalCategoria = (decimal)reader["CostoCantidad"];
                    aCMVH.CostoPromedioCategoria = 0;
                    lista_costos.Add(aCMVH);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;
        }
        public List<ConsumoAnios> ResumenManteCostosAnualUnVh(int _idvh)
        {
            List<ConsumoAnios> lista_costos = new List<ConsumoAnios>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculo_Mante_Total_Por_Anio_UnVh";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsumoAnios costo = new ConsumoAnios();
                    costo.Anio = (int)reader["ANIO"];
                    costo.CostoAnio = (decimal)reader["IMPORTE"];
                    lista_costos.Add(costo);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;

        }

        public List<ACMVH_CategoriaAnio> CategoriasVhManteMesAnioUnVh(int _idvh, int _anio, int _mes)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculo_Categorias_Mante_Mes_Anio_Unvh";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@mes", _mes);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACMVH_CategoriaAnio aCMVH = new ACMVH_CategoriaAnio();
                    aCMVH.IdCateManteVh = 0;
                    aCMVH.NombreCategoria = (string)reader["nomcate"];
                    aCMVH.CantidadIncidencias = (int)reader["CantidadCategoria"];
                    aCMVH.CostoTotalCategoria = (decimal)reader["CostoCantidad"];
                    aCMVH.CostoPromedioCategoria = 0;
                    lista_costos.Add(aCMVH);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_costos;
        }

        public List<Vehiculo> DominiosMesAnioMantenimientos(int _anio, int _mes, int _idcatevh)
        {
            List<Vehiculo> lista_vehiculos = new List<Vehiculo>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Vehiculos_Dominios_Mes_Anio_Mantenimientos";
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            cmd.Parameters.AddWithValue("@mes", _mes);
            cmd.Parameters.AddWithValue("@idcatevh", _idcatevh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.IdVh = (int)reader["idvh"];
                    vehiculo.Dominio = (string)reader["dominio"];
                    vehiculo.Modelo = (string)reader["modelo"];
                    vehiculo.NomMarca = (string)reader["nombre_marca"];
                    vehiculo.RSat = (int)reader["CantIncidencias"];
                    lista_vehiculos.Add(vehiculo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_vehiculos;

        }

        public void VehiculoBuscarFactura(string _numfactura)
        { }


        public void VehiculosListarMantenimientosCategoria(int _idvehiculo)
        { }
        #endregion

        #region[documentacion]

        //agregar un nuevo registro de documentacion de un vehiculo
        public int VehiculoAgregarNuevaDocumentacion(VehiculoDocu vehiculoDocu)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Alta_Documentacion";
            cmd.Parameters.AddWithValue("@idvh", vehiculoDocu.IdVh);
            cmd.Parameters.AddWithValue("@iddocuvh", vehiculoDocu.IdDocuvh);
            cmd.Parameters.AddWithValue("@fvencimiento", vehiculoDocu.FVencimiento);
            cmd.Parameters.AddWithValue("@fdesde", vehiculoDocu.FDesde);
            cmd.Parameters.AddWithValue("@fhasta", vehiculoDocu.FHasta);
            cmd.Parameters.AddWithValue("@numerodoc", vehiculoDocu.NumeroDoc);
            cmd.Parameters.AddWithValue("@costo", vehiculoDocu.Costo);
            cmd.Parameters.AddWithValue("@nota", vehiculoDocu.Nota);
            cmd.Parameters.AddWithValue("@estadoreg", vehiculoDocu.EstadoReg);

            cmd.Parameters.AddWithValue("@altaf", vehiculoDocu.Altaf);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return fila;
        }

        // actualizar un registros de documentacion de un vehiculo 
        public int VehiculoActualizarDocumentacion(VehiculoDocu vehiculoDocu)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Modi_Documentacion";
            cmd.Parameters.AddWithValue("@idvhdoc", vehiculoDocu.IdVhDoc);
            cmd.Parameters.AddWithValue("@iddocuvh", vehiculoDocu.IdDocuvh);
            cmd.Parameters.AddWithValue("@fvencimiento", vehiculoDocu.FVencimiento);
            cmd.Parameters.AddWithValue("@fdesde", vehiculoDocu.FDesde);
            cmd.Parameters.AddWithValue("@fhasta", vehiculoDocu.FHasta);
            cmd.Parameters.AddWithValue("@numerodoc", vehiculoDocu.NumeroDoc);
            cmd.Parameters.AddWithValue("@costo", vehiculoDocu.Costo);
            cmd.Parameters.AddWithValue("@nota", vehiculoDocu.Nota);


            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return fila;
        }


        //listado de todas las documentaciones de un vehiculo
        public ObservableCollection<VehiculoDocu> VehiculoListarDocumentacion(int _idvh)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Documentacion_Todas";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_doc_vehiculos = ArmarListaDocumentacion(reader);


                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_doc_vehiculos;
        }

        //listado de todos los registros de un tipo de documentacion de un vehiculo
        public ObservableCollection<VehiculoDocu> VehiculoListarTipoDocumentacion(int _idvh, int _idtipodocu)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Doc_Todas_Tipo";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@idtipodoc", _idtipodocu);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_doc_vehiculos = ArmarListaDocumentacion(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_doc_vehiculos;
        }

        public ObservableCollection<VehiculoDocu> VehiculoListarDocVigente(int _idvh)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Doc_Vigente";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_doc_vehiculos = ArmarListaDocumentacion(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_doc_vehiculos;
        }

        private ObservableCollection<VehiculoDocu> ArmarListaDocumentacion(SqlDataReader reader)
        {
            ObservableCollection<VehiculoDocu> listatemp = new ObservableCollection<VehiculoDocu>();
            while (reader.Read())
            {
                VehiculoDocu docu = new VehiculoDocu();
                docu.IdVhDoc = (int)reader["idvhdoc"];
                docu.IdVh = (int)reader["idvh"];
                docu.IdDocuvh = (int)reader["iddocuvh"];
                docu.Costo = (decimal)reader["costo"];
                docu.EstadoReg = (int)reader["estadoreg"];
                if (docu.EstadoReg == 1)
                {
                    docu.Situacion = "Activo";
                }
                if (docu.EstadoReg == 2)
                {
                    docu.Situacion = "Vencido";
                }
                if (docu.EstadoReg == 3)
                {
                    docu.Situacion = "Cumplido";
                }
                if (reader["fvencimiento"] != DBNull.Value)
                {
                    docu.FVencimiento = (DateTime)reader["fvencimiento"];
                }
                else
                {
                    docu.FVencimiento = null;
                }

                if (reader["fdesde"] != DBNull.Value)
                {
                    docu.FDesde = (DateTime)reader["fdesde"];
                }
                else
                {
                    docu.FDesde = null;
                }
                if (reader["fhasta"] != DBNull.Value)
                {
                    docu.FHasta = (DateTime)reader["fhasta"];
                }
                else
                {
                    docu.FHasta = null;
                }
                docu.Nota = (string)reader["nota"];
                docu.NumeroDoc = (string)reader["numerodoc"];
                docu.NombreDocu = (string)reader["nombredocu"];

                docu.ControlFecha = (int)reader["controlfecha"];

                docu.DominioVh = (string)reader["dominio"];



                listatemp.Add(docu);
            }
            return listatemp;
        }

        public List<Docu_vh> VehiculoListarTipoDocu()
        {
            List<Docu_vh> listaDocu = new List<Docu_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Tipo_Docu";
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Docu_vh d = new Docu_vh();
                    d.IdDocuVH = (int)reader["iddocuvh"];
                    d.Descripcion = (string)reader["nombredocu"];
                    listaDocu.Add(d);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return listaDocu;

        }

        //borrar un registro de documentacion
        public int VehiculoBorrarUnaDocumentacion(int _idvhdoc)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Borra_Una_Documentacion";
            cmd.Parameters.AddWithValue("@idvhdoc", _idvhdoc);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int VehiculoCumplirUnRegistro(int _idvhdoc)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Cumplir_Una_Docu";
            cmd.Parameters.AddWithValue("@idvhdoc", _idvhdoc);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public void VehiculoAltaTipoDocumentacion(Docu_vh docu_Vh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_TipoDocu_Alta";
            cmd.Parameters.AddWithValue("@descripcion", docu_Vh.Descripcion);

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

        public void VehiculoModiTipoDocumentacion(Docu_vh docu_Vh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_TipoDocu_Modi";
            cmd.Parameters.AddWithValue("@idtipo", docu_Vh.IdDocuVH);
            cmd.Parameters.AddWithValue("@descripcion", docu_Vh.Descripcion);

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

        public List<VehiculoDocu> VehiculoListarDocuVencida()
        {
            // lista todos los vehiculos que tienen 1 o mas documentacion vencida
            List<VehiculoDocu> lista_doc_vehiculos = new List<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Doc_Vencida";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VehiculoDocu vhd = new VehiculoDocu();
                    vhd.IdVhDoc = (int)reader["idvhdoc"];
                    vhd.IdVh = (int)reader["idvh"];
                    vhd.DominioVh = (string)reader["dominio"];
                    vhd.DescriVh = (string)reader["descripcion"];
                    vhd.ModeloVh = (string)reader["modelo"];
                    vhd.NombreDocu = (string)reader["nombredocu"];
                    vhd.FVencimiento = (DateTime)reader["fvencimiento"];
                    vhd.Costo = (int)reader["cantidad"];
                    vhd.Situacion = "Vencido";
                    lista_doc_vehiculos.Add(vhd);
                }
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_doc_vehiculos;
        }

        public List<VehiculoDocu> VehiculosListarDocAVencer()
        {
            // lista todos los vehiculos que tienen 1 o mas documentacion vencida
            List<VehiculoDocu> lista_doc_vehiculos = new List<VehiculoDocu>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Doc_A_Vencer";

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VehiculoDocu vhd = new VehiculoDocu();
                   
                    vhd.IdVh = (int)reader["idvh"];
                    vhd.IdVhDoc = (int)reader["idvhdoc"];
                    vhd.DominioVh = (string)reader["dominio"];
                    vhd.DescriVh = (string)reader["descripcion"];
                    vhd.ModeloVh = (string)reader["modelo"];
                    vhd.NombreDocu = (string)reader["nombredocu"];
                    vhd.FVencimiento = (DateTime)reader["fvencimiento"];
                    vhd.Costo = (int)reader["cantidad"];
                    lista_doc_vehiculos.Add(vhd);
                }
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista_doc_vehiculos;
        }

        //alta de nota de documentacion
        public void VehiculoDocAltaNota(NotaDocuVh notaDocuVh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "NotaDocVh_Alta";
            cmd.Parameters.AddWithValue("@alta", notaDocuVh.FechaAlta);
            cmd.Parameters.AddWithValue("@usuario", notaDocuVh.IdUsuario);
            cmd.Parameters.AddWithValue("@contenido", notaDocuVh.Contenido);
            cmd.Parameters.AddWithValue("@idregistro", notaDocuVh.IdRegistro);
            cmd.Parameters.AddWithValue("@idtiponota", notaDocuVh.IdTipoNota);

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

        //borrado de una nota sobre documentacion de un vehiculo
        public void VehiculoDocDelete(int idnota)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "NotaDocVh_Borrar";
            cmd.Parameters.AddWithValue("@idnota", idnota);
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

        // todas la notas para una documentacion de vehiculo especifica

        public List<NotaDocuVh> VehiculoDocNotas(int idregistro, int idtiponota)
        {
            List<NotaDocuVh> lista = new List<NotaDocuVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "NotaDocVh_Listar";
            cmd.Parameters.AddWithValue("@idregistro", idregistro);
            cmd.Parameters.AddWithValue("@idtiponota", idtiponota);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NotaDocuVh nota = new NotaDocuVh();
                    nota.IdNota = (int)reader["idnota"];
                    nota.FechaAlta = (DateTime)reader["alta"];
                    nota.IdUsuario = (int)reader["idusuario"];
                    nota.Contenido = (string)reader["contenido"];
                    nota.IdTipoNota = (int)reader["idtiponota"];
                    nota.IdRegistro = (int)reader["idregistro"];
                    lista.Add(nota);
                }
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public void VehiculoCalcularCostoDocu(int idvh)
        { }
        #endregion
        #region Fotos



        public int GuardarFoto(FotoVh fotoVh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Foto_Grabar";
            cmd.Parameters.AddWithValue("@idvh", fotoVh.Idvh);
            cmd.Parameters.AddWithValue("@foto", fotoVh.Foto);
            cmd.Parameters.AddWithValue("@altaf", fotoVh.AltaF);
            cmd.Parameters.AddWithValue("@descripcion", fotoVh.DescriFoto);
            cmd.Parameters.AddWithValue("@titulo", fotoVh.Titulo);

            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int BorrarFoto(int _idfotovh)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Foto_Borrar";
            cmd.Parameters.AddWithValue("@idfotovh", _idfotovh);
            try
            {
                conexion.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public ObservableCollection<FotoVh> VehiculoListaFotos(int _idvh)
        {
            ObservableCollection<FotoVh> fotoVhs = new ObservableCollection<FotoVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Listar_Fotos";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FotoVh f = new FotoVh();
                    f.IdFotoVh = (int)reader["idfotovh"];
                    f.AltaF = (DateTime)reader["altaf"];
                    f.DescriFoto = (string)reader["descripcion"];
                    f.Idvh = (int)reader["idvh"];
                    f.Titulo = (string)reader["titulo"];
                    f.Foto = (byte[])reader["foto"];
                    fotoVhs.Add(f);

                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fotoVhs;
        }
        #endregion




       

        #region Repuestos

        public ObservableCollection<RepuestoVh> RepuestosVhUnVehiculo(int _idvehiculo)
        {
            ObservableCollection<RepuestoVh> repuestoVhs = new ObservableCollection<RepuestoVh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RepuestosVh_Un_Vehiculo";
            cmd.Parameters.AddWithValue("@idvh", _idvehiculo);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RepuestoVh rp = new RepuestoVh();
                    rp.IdRepuestoVh = (int)reader["idrepuestovh"];
                    rp.Descripcion = (string)reader["descripcion"];
                    rp.Nombre = (string)reader["nombre"];
                    rp.Idvh = (int)reader["idvh"];
                    rp.Modelo = (string)reader["modelo"];
                    rp.NumSerie = (string)reader["numserie"];
                    rp.IdProducto = (int)reader["idproducto"];
                    rp.PrecioUnit = (decimal)reader["precio_unitario"];
                    rp.CostoReposicion = (decimal)reader["costo_reposicion"];
                    rp.CodInventario = (string)reader["cod_inventario"];
                    rp.Marca = (string)reader["nombre_marca"];
                    repuestoVhs.Add(rp);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return repuestoVhs;
        }

        public void RepuestoAlta(int _idvh, int _idproducto, int _cantidad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "RepuestoVh_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            cmd.Parameters.AddWithValue("@cantidad", _cantidad);
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

        public void RepuestoBaja(int _idrepuesto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "RepuestoVh_Baja";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idrepuestovh", _idrepuesto);

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

        #region[balance]
        public void VehiculoBalance(int idvehiculo)
        { }
        #endregion

        #region ISO9000


        public void PlanInspeccionAlta(plan_inspeccion plan)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Alta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", plan.Idvh);
            cmd.Parameters.AddWithValue("@tarea", plan.Tarea);
            cmd.Parameters.AddWithValue("@constante", plan.ValorConstante);
            cmd.Parameters.AddWithValue("@fechainicio", plan.FechaInicio);
            cmd.Parameters.AddWithValue("@ultima", plan.Ultima_actualizacion);
            cmd.Parameters.AddWithValue("@estado", plan.Estado);
            cmd.Parameters.AddWithValue("@atributo", plan.AtributoComparativo);
            cmd.Parameters.AddWithValue("@actual_comparativo", plan.ValorActualComparativo);
            cmd.Parameters.AddWithValue("@limite_comparativo", plan.ValorLimiteComparativo);
            cmd.Parameters.AddWithValue("@gap", plan.Gap);
            cmd.Parameters.AddWithValue("@gap_alarma", plan.GapAlarma);
            cmd.Parameters.AddWithValue("@img", plan.ImageEstadoTemp);
            cmd.Parameters.AddWithValue("@valor_inicial", plan.ValorInicio);
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

        public void PlanInspeccionBaja(int _idplan)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Baja";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idplan", _idplan);
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

        public ObservableCollection<plan_inspeccion> PlanInspeccionUnVehiculo(int _idvh)
        {
            ObservableCollection<plan_inspeccion> inspeccions = new ObservableCollection<plan_inspeccion>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlanInspeccion_Un_Vehiculo";
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                inspeccions = ArmarPlanInspeccion(reader);

                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return inspeccions;
        }

        public ObservableCollection<plan_inspeccion> PlanInspeccionTareasActivasUnVehiculo(int _idvh)
        {
            ObservableCollection<plan_inspeccion> inspeccions = new ObservableCollection<plan_inspeccion>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlanInspeccion_Un_Vehiculo";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                inspeccions = ArmarPlanInspeccion(reader);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return inspeccions;
        }

        private ObservableCollection<plan_inspeccion> ArmarPlanInspeccion(SqlDataReader reader)
        {
            ObservableCollection<plan_inspeccion> plan_s = new ObservableCollection<plan_inspeccion>();
            while (reader.Read())
            {
                plan_inspeccion p = new plan_inspeccion();
                p.Idplan = (int)reader["idplan"];
                p.Idvh = (int)reader["idvh"];
                p.AtributoComparativo = (string)reader["atributo_comparativo"];
                if (reader["baja"] != DBNull.Value)
                {
                    p.Baja = (DateTime)reader["baja"];
                }
                else { p.Baja = null; }

                p.Estado = (int)reader["estado"];
                p.FechaInicio = (DateTime)reader["fecha_inicio"];
                p.Tarea = (string)reader["tarea"];
                p.Ultima_actualizacion = (DateTime)reader["ultima_actualizacion"];
                p.ValorActualComparativo = (decimal)reader["valor_actual_comparativo"];
                p.ValorConstante = (decimal)reader["valor_constante"];
                p.ValorLimiteComparativo = (decimal)reader["valor_limite_comparativo"];
                p.Gap = (decimal)reader["gap"];
                p.GapAlarma = (decimal)reader["gap_alarma"];
                if (p.Estado == 1)
                {
                    p.SituacionTarea = "Normal";
                }
                else
                {
                    if (p.Estado == 2)
                    {
                        p.SituacionTarea = "Por Vencer";
                    }
                    else
                    {
                        if (p.Estado == 3)
                        {
                            p.SituacionTarea = "Vencido";
                        }
                        else
                        {
                            p.SituacionTarea = "Concluido";
                        }
                    }
                }
                p.ImageEstadoTemp = (byte[])reader["img_estado"];
                p.ValorInicio = (decimal)reader["valor_inicio"];
                p.IdOtm = (int)reader["idotm"];
                plan_s.Add(p);

            }
            return plan_s;

        }

        public void PlanInspeccionCalcularGAP(int _idvh, decimal _consumo, int _idplan, DateTime _fechaUpdate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Calcular_GAP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.AddWithValue("@nuevo_consumo", _consumo);
            cmd.Parameters.AddWithValue("@idplan", _idplan);
            cmd.Parameters.AddWithValue("@ultima", _fechaUpdate);

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

        public void PlansInspeccionCalcularAlarma(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Calcular_Alarma";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);

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

        public void PlanInspeccionCambiarImagenVencido(int _idvh, byte[] _imagen)
        {
            //camnbia la imagen del estado del plan de inspeccion si el estado es igual a 3 o si el estado es igual a 2
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_ImagenVencido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            cmd.Parameters.AddWithValue("@img", _imagen);
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

        public void PlanInspeccionCambiarImagenPorVencer(int _idvh, byte[] _imagen)
        {
            //camnbia la imagen del estado del plan de inspeccion si el estado es igual a 3 o si el estado es igual a 2
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_ImagenPorVencer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);

            cmd.Parameters.AddWithValue("@img", _imagen);
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

        public void PlanInspeccionAsignarOTM(int _idotm, int _idplan)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Agregar_OTM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idotm", _idotm);
            cmd.Parameters.AddWithValue("@idplan", _idplan);
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

        public void PLanInspeccionCambiarEstadoDesdeOTM(int _idotm, int _valorestado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccionCambiarEstadoDesdeOtm";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idotm", _idotm);
            cmd.Parameters.AddWithValue("@valorestado", _valorestado);
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

        public void PlanInspeccionCalcularVencimiento(int _idvh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "PlanInspeccion_Calular_Vencimiento";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idvh", _idvh);
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

        public int PlanInspeccionPorEstado(int _idestado)
        {
            int registrosafectados = 0;
            int filas = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlanInspeccion_Por_Estados";
            cmd.Parameters.AddWithValue("@idestado", _idestado);
            cmd.Parameters.Add("@cantidadfilas", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                registrosafectados = Convert.ToInt16(cmd.Parameters["@cantidadfilas"].Value);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return registrosafectados;
        }

        public void DetallePlanInspAlta(plan_insp_detalle insp_Detalle)
        { }
        public void DetallePlanInsModi(plan_insp_detalle insp_Detalle)
        { }
        public void DetallePlanInsBorrar(int _iddet, int _idplan)
        { }

        public void VefTecnicaAlta(Vef_Tecnica vef_Tecnica)
        { }
        public void VefTecnicaModi(Vef_Tecnica vef_Tecnica)
        { }

        public void VefTecnicaBaja(int _idvef)
        { }

        public void VefTecnicaDetAlta(Vef_Tecnica_Detalle tecnica_Detalle)
        { }

        public void VefTecnicaDetModi(Vef_Tecnica_Detalle tecnica_Detalle)
        { }

        public void VefTecnicaDetBorrar(int _iddet)
        { }

        #endregion

        #region PLanInspeccion

        public List<plan_inspeccion> PlanInspeccionEstadoTareas(int idestado)
        {
            List<plan_inspeccion> lista = new List<plan_inspeccion>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlanInspeccion_EstadoTareas_Gral";
            cmd.Parameters.AddWithValue("@idestado", idestado);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista = ArmarListaTarea(reader);
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        private List<plan_inspeccion> ArmarListaTarea(SqlDataReader reader)
        {
            List<plan_inspeccion> plans = new List<plan_inspeccion>();
            while (reader.Read())
            {
                plan_inspeccion plan = new plan_inspeccion();
                plan.Idplan = (int)reader["idplan"];
                plan.Idvh = (int)reader["idvh"];
                plan.Dominio = (string)reader["dominio"];
                plan.Tarea = (string)reader["tarea"];
                plan.Gap = (decimal)reader["gap"];
                plan.AtributoComparativo = (string)reader["atributo_comparativo"];
                plan.Estado = (int)reader["estado"];
                plan.Modelo = (string)reader["modelo"];
                plan.Marca = (string)reader["nombre_marca"];
                plans.Add(plan);
            }
            return plans;
        }
        #endregion

        #region Consumos

        public List<ConsumoVhMes> ListaDominiosConsumosMes(int _mes, int _anio)
        {
            List<ConsumoVhMes> lista = new List<ConsumoVhMes>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Consumos_Por_Mes_Anual";
            cmd.Parameters.AddWithValue("@meskm", _mes);
            cmd.Parameters.AddWithValue("@anioconsumo", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConsumoVhMes consumo = new ConsumoVhMes();
                    consumo.AnioConsumo = (int)reader["anioconsumo"];
                    consumo.CCMBHS = (decimal)reader["CCMBHS"];
                    consumo.CCMBKM = (decimal)reader["CCMBKM"];
                    consumo.Descripcion = (string)reader["descripcion"];
                    consumo.Dominio = (string)reader["dominio"];
                    consumo.HsTrabajo = (decimal)reader["HsTRabajo"];
                    consumo.KmRecorrido = (decimal)reader["KmRecorrido"];
                    consumo.Marca = (string)reader["nombre_marca"];
                    consumo.Mes = (string)reader["meskm"];
                    consumo.Modelo = (string)reader["modelo"];
                    lista.Add(consumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;

        }

        public List<ACCDetMesAnio> DetalleConsumosMesPorAnio(int _anioConsulta)
        {
            List<ACCDetMesAnio> lista = new List<ACCDetMesAnio>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Totales_Mes_Anio";
            cmd.Parameters.AddWithValue("@anio", _anioConsulta);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ACCDetMesAnio detalleConsumo = new ACCDetMesAnio();
                    detalleConsumo.MesDelAnio = (int)reader["mes"];
                    detalleConsumo.HsRegistradas = (decimal)reader["HsTotalMes"];
                    detalleConsumo.KmRegistrados = (decimal)reader["KmTotalMes"];
                    detalleConsumo.LtsConsumidosMes = (decimal)reader["CombustibleTotalMes"];
                    detalleConsumo.CCMesAnio = 0;
                    lista.Add(detalleConsumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public ACCDetMesAnio DetalleConsumoUnMesUnAnio(int _anioConsulta, int _mesConsulta)
        {

            ACCDetMesAnio detalleConsumo = new ACCDetMesAnio();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_ConsumosCombTotales_Mes_Anio";
            cmd.Parameters.AddWithValue("@anio", _anioConsulta);
            cmd.Parameters.AddWithValue("@mes", _mesConsulta);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    detalleConsumo.MesDelAnio = (int)reader["mes"];
                    detalleConsumo.HsRegistradas = (decimal)reader["HsTotalMes"];
                    detalleConsumo.KmRegistrados = (decimal)reader["KmTotalMes"];
                    detalleConsumo.LtsConsumidosMes = (decimal)reader["CombustibleTotalMes"];


                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return detalleConsumo;
        }

       
        #endregion

        #region[metodos privados]
        private ObservableCollection<Vehiculo> ArmarListaVehiculos(SqlDataReader lectura)
        {
            ObservableCollection<Vehiculo> lista = new ObservableCollection<Vehiculo>();
            while (lectura.Read())
            {

                Vehiculo vehiculo = new Vehiculo();
                vehiculo.IdVh = (int)lectura["idvh"];
                vehiculo.Descripcion = (string)lectura["descripcion"];
                vehiculo.Modelo = (string)lectura["modelo"];
                vehiculo.Dominio = (string)lectura["dominio"];
                vehiculo.NumeroChasis = (string)lectura["numchasis"];
                vehiculo.NumeroMotor = (string)lectura["nummotor"];
                vehiculo.Color = (string)lectura["color"];
                vehiculo.AnioModelo = (string)lectura["aniomodelo"];
                vehiculo.Cilindrada = (string)lectura["cilindrada"];
                vehiculo.NeuDelantero = (string)lectura["neumadelantero"];
                vehiculo.NeuTrasero = (string)lectura["neumatrasero"];

                if (lectura["fechacompra"] != DBNull.Value)
                {
                    vehiculo.FechaCompra = (DateTime)lectura["fechacompra"];

                }
                else
                {
                    vehiculo.FechaCompra = null;
                }

                vehiculo.ValorCompra = (decimal)lectura["valorcompra"];
                vehiculo.AltaF = (DateTime)lectura["altaf"];
                vehiculo.BajaF = (DateTime)lectura["bajaf"];
                vehiculo.CausaBaja = (string)lectura["causabaja"];
                vehiculo.Garantia = (string)lectura["garantia"];
                vehiculo.LitroHora = (decimal)lectura["litrohora"];
                vehiculo.ValorHora = (decimal)lectura["valorhora"];
                vehiculo.ValorKm = (decimal)lectura["valorkm"];
                vehiculo.HorasAcumuladas = (int)lectura["horasacumuladas"];
                vehiculo.IdCombustible = (int)lectura["idcombustible"];
                vehiculo.CantiEjes = (int)lectura["cantiejes"];
                vehiculo.RSat = (int)lectura["rastreo_sat"];
                vehiculo.SegSat = (int)lectura["seguimiento_sat"];
                vehiculo.KmAcumulado = (decimal)lectura["km_acumulado"];
                vehiculo.KmLitro = (decimal)lectura["km_litro"];
                vehiculo.IdMarca = (int)lectura["idmarcavh"];
                vehiculo.IdSf = (int)lectura["idsf"];
                vehiculo.CantDocVencida = (int)lectura["cantdocvencida"];
                vehiculo.IdCate = (int)lectura["idcate"];
                vehiculo.IdLinea = (int)lectura["idlinea"];
                vehiculo.Estado = (string)lectura["estado"];
                vehiculo.ValorLitro = (decimal)lectura["valorlitro"];
                vehiculo.KmInicial = (decimal)lectura["kminicial"];
                vehiculo.HsInicial = (decimal)lectura["hsinicial"];


                vehiculo.NomCategoria = (string)lectura["nomcate"];
                vehiculo.NomCombustible = (string)lectura["nombre"];
                vehiculo.NomMarca = (string)lectura["nombre_marca"];
                vehiculo.NomSfisica = (string)lectura["nomsf"];
                vehiculo.CodigoRadio = (string)lectura["codigo_radio"];
                vehiculo.KmKitDistribucion = (int)lectura["km_kit_distribucion"];
                vehiculo.CostoReposicionDls = (decimal)lectura["costo_repo_dls"];
                vehiculo.Alto = (decimal)lectura["alto"];
                vehiculo.Ancho = (decimal)lectura["ancho"];
                vehiculo.Largo = (decimal)lectura["largo"];
                vehiculo.Peso = (decimal)lectura["peso"];
                vehiculo.VolumenCarga = (decimal)lectura["carga"];
                vehiculo.CargaMaxima = (decimal)lectura["volumen"];
                vehiculo.SuperficieCarga = (decimal)lectura["superficie"];
                lista.Add(vehiculo);
            }
            return lista;


        }

        private List<Vehiculo> ArmarListaParaRemision(SqlDataReader lectura)
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            while (lectura.Read())
            {

                Vehiculo vehiculo = new Vehiculo();
                vehiculo.IdVh = (int)lectura["idvh"];
                vehiculo.Descripcion = (string)lectura["descripcion"];
                vehiculo.Modelo = (string)lectura["modelo"];
                vehiculo.Dominio = (string)lectura["dominio"];
                vehiculo.IdMarca = (int)lectura["idmarcavh"];
                vehiculo.IdSf = (int)lectura["idsf"];
                vehiculo.NomCategoria = (string)lectura["nomcate"];
                vehiculo.NomMarca = (string)lectura["nombre_marca"];
                vehiculo.NomSfisica = (string)lectura["nomsf"];

                lista.Add(vehiculo);
            }
            return lista;

        }

        private Vehiculo ArmarVehiculo(SqlDataReader lectura)
        {
            Vehiculo vehiculo = new Vehiculo();
            while (lectura.Read())
            {
                vehiculo.IdVh = (int)lectura["idvh"];
                vehiculo.Descripcion = (string)lectura["descripcion"];
                vehiculo.Modelo = (string)lectura["modelo"];
                vehiculo.Dominio = (string)lectura["dominio"];
                vehiculo.NumeroChasis = (string)lectura["numchasis"];
                vehiculo.NumeroMotor = (string)lectura["nummotor"];
                vehiculo.Color = (string)lectura["color"];
                vehiculo.AnioModelo = (string)lectura["aniomodelo"];
                vehiculo.Cilindrada = (string)lectura["cilindrada"];
                vehiculo.NeuDelantero = (string)lectura["neumadelantero"];
                vehiculo.NeuTrasero = (string)lectura["neumatrasero"];

                if (lectura["fechacompra"] != DBNull.Value)
                {
                    vehiculo.FechaCompra = (DateTime)lectura["fechacompra"];

                }
                else
                {
                    vehiculo.FechaCompra = null;
                }

                vehiculo.ValorCompra = (decimal)lectura["valorcompra"];
                vehiculo.AltaF = (DateTime)lectura["altaf"];
                vehiculo.BajaF = (DateTime)lectura["bajaf"];
                vehiculo.CausaBaja = (string)lectura["causabaja"];
                vehiculo.Garantia = (string)lectura["garantia"];
                vehiculo.LitroHora = (decimal)lectura["litrohora"];
                vehiculo.ValorHora = (decimal)lectura["valorhora"];
                vehiculo.ValorKm = (decimal)lectura["valorkm"];
                vehiculo.HorasAcumuladas = (int)lectura["horasacumuladas"];
                vehiculo.IdCombustible = (int)lectura["idcombustible"];
                vehiculo.CantiEjes = (int)lectura["cantiejes"];
                vehiculo.RSat = (int)lectura["rastreo_sat"];
                vehiculo.SegSat = (int)lectura["seguimiento_sat"];
                vehiculo.KmAcumulado = (decimal)lectura["km_acumulado"];
                vehiculo.KmLitro = (decimal)lectura["km_litro"];
                vehiculo.IdMarca = (int)lectura["idmarcavh"];
                vehiculo.IdSf = (int)lectura["idsf"];
                vehiculo.CantDocVencida = (int)lectura["cantdocvencida"];
                vehiculo.IdCate = (int)lectura["idcate"];
                vehiculo.IdLinea = (int)lectura["idlinea"];
                vehiculo.Estado = (string)lectura["estado"];
                vehiculo.ValorLitro = (decimal)lectura["valorlitro"];
                vehiculo.KmInicial = (decimal)lectura["kminicial"];
                vehiculo.HsInicial = (decimal)lectura["hsinicial"];
                vehiculo.NomMarca = (string)lectura["nombre_marca"];
                vehiculo.NomCombustible = (string)lectura["nombre"];
                vehiculo.KmKitDistribucion = (int)lectura["km_kit_distribucion"];
                vehiculo.CostoReposicionDls = (decimal)lectura["costo_repo_dls"];

            }
            return vehiculo;
        }

        private ObservableCollection<PlanificacionVH> ArmarPlanificacion(SqlDataReader lectura)
        {
            ObservableCollection<PlanificacionVH> vHs = new ObservableCollection<PlanificacionVH>();
            while (lectura.Read())
            {
                PlanificacionVH planificacionVH = new PlanificacionVH();
                planificacionVH.IdPl = (int)lectura["idpl"];
                planificacionVH.IdVh = (int)lectura["idvh"];
                planificacionVH.Dominio = (string)lectura["dominio"];
                planificacionVH.Marca = (string)lectura["nombre_marca"];
                planificacionVH.Imputacion = (int)lectura["imputacion"];
                planificacionVH.Modelo = (string)lectura["modelo"];
                planificacionVH.NomObra = (string)lectura["nomobra"];
                planificacionVH.Notas = (string)lectura["notas"];
                planificacionVH.Solicitante = (string)lectura["solicitante"];
                planificacionVH.Estado = (string)lectura["estado"];
                DateTime _fesde = (DateTime)lectura["fdesde"];
                planificacionVH.FDesde = _fesde.Date;
                DateTime _fhasta = (DateTime)lectura["fhasta"];
                planificacionVH.FHasta = _fhasta.Date;
                planificacionVH.EstadoReg = (int)lectura["estadoreg"];
                planificacionVH.NotaBaja = (string)lectura["notabaja"];
                planificacionVH.FBaja = (DateTime)lectura["bajaf"];

                vHs.Add(planificacionVH);
            }
            return vHs;

        }

        private ObservableCollection<Asignacion_vh> ArmarAsignaciones(SqlDataReader reader)
        {

            ObservableCollection<Asignacion_vh> vhs = new ObservableCollection<Asignacion_vh>();
            while (reader.Read())
            {
                Asignacion_vh asignacion = new Asignacion_vh();
                asignacion.IdAsig = (int)reader["idasig"];
                asignacion.IdVh = (int)reader["idvh"];
                asignacion.IdEmpleado = (int)reader["idempleado"];
                asignacion.Imputacion = (int)reader["imputacion"];
                asignacion.FechaIn = (DateTime)reader["fechain"];

                if (reader["fechafin"] != DBNull.Value)
                {
                    asignacion.FechaFin = (DateTime)reader["fechafin"];

                }
                else
                {
                    asignacion.FechaFin = null;
                }


                asignacion.EstadoAsignacion = (string)reader["estadoasignacion"];
                asignacion.SituacionAsignacion = (string)reader["situacionasignacion"];
                asignacion.DiasAcumulados = (int)reader["diasacu"];
                asignacion.CostoAsignacion = (decimal)reader["costoasig"];
                asignacion.Dominio = (string)reader["dominio"];
                asignacion.Modelo = (string)reader["modelo"];
                asignacion.NombreEmpleado = (string)reader["nombre"];
                asignacion.NombreObra = (string)reader["nomobra"];
                asignacion.Cliente = (string)reader["cliente"];
                asignacion.HsAcuObra = (int)reader["horastrabajo"];
                asignacion.KmAcuObra = (int)reader["kmtrabajo"];
                asignacion.IdCatevh = (int)reader["idcatevh"];
                asignacion.Categoria = (string)reader["nomcate"];
                vhs.Add(asignacion);

            }
            return vhs;
        }

        private ObservableCollection<ConsumoCombustible> ArmarConsumosCombustibles(SqlDataReader reader)
        {
            ObservableCollection<ConsumoCombustible> consumos = new ObservableCollection<ConsumoCombustible>();
            while (reader.Read())
            {
                ConsumoCombustible ccb = new ConsumoCombustible();
                ccb.IdCmb = (int)reader["idcmb"];
                ccb.IdVh = (int)reader["idvh"];
                ccb.KmRecorrido = (decimal)reader["kmrecorrido"];
                ccb.LitrosCmbKM = (decimal)reader["litroscmb"];
                ccb.FechaReg = (DateTime)reader["fechareg"];
                ccb.LitrosCmbHoras = (decimal)reader["litroscmbhoras"];
                ccb.HorasTrabajo = (decimal)reader["horastrabajo"];
                ccb.CostoCmbHoras = (decimal)reader["costocmbhoras"];
                ccb.CostoCmbKm = (decimal)reader["costocmbkm"];
                ccb.Imputacion = (int)reader["imputacion"];
                ccb.AnioConsumo = (int)reader["anioconsumo"];
                ccb.Meskm = (string)reader["meskm"];
                ccb.PrecioLitro = (decimal)reader["precio_litro"];
                consumos.Add(ccb);


            }
            return consumos;
        }

        private ObservableCollection<Mante_vh> ArmarMantenimientosVh(SqlDataReader reader)
        {
            ObservableCollection<Mante_vh> mante_Vhs = new ObservableCollection<Mante_vh>();
            while (reader.Read())
            {
                Mante_vh mante_ = new Mante_vh();

                mante_.IdManteVh = (int)reader["idmantevh"];
                mante_.IdVh = (int)reader["idvh"];
                mante_.IdProve = (int)reader["idprove"];
                mante_.IdEmpleado = (int)reader["idempleado"];
                mante_.AltaF = (DateTime)reader["altaf"];
                if (reader["fechafac"] != DBNull.Value)
                {


                    mante_.FechaFac = (DateTime)reader["fechafac"];
                }
                else
                {
                    mante_.FechaFac = null;
                }

                if (reader["fecharem"] != DBNull.Value)
                {
                    mante_.FechaRem = (DateTime)reader["fecharem"];
                }
                else
                {
                    mante_.FechaRem = null;
                }

                mante_.Dominio = (string)reader["dominio"];
                mante_.HorasMante = (int)reader["horasmante"];
                mante_.Importe = (decimal)reader["importe"];
                mante_.Imputacion = (int)reader["imputacion"];
                mante_.KmMante = (int)reader["idmantevh"];
                //mante_.NombreEmpleado = (string)reader["nombre"];
                mante_.NumFactura = (string)reader["numfactura"];
                mante_.NumRemito = (string)reader["numremito"];
                mante_.OrdenCompra = (string)reader["ordencompra"];
                mante_.TipoMante = (string)reader["tipomante"];
                mante_.NombreProve = (string)reader["razonsocial"];
                mante_.Marca = (string)reader["nombre_marca"];
                mante_.Modelo = (string)reader["modelo"];

                mante_Vhs.Add(mante_);

            }
            return mante_Vhs;
        }

        private Mante_vh ArmarUnMantenimiento(SqlDataReader reader)
        {
            Mante_vh mante_ = new Mante_vh();

            while (reader.Read())
            {
                mante_.IdManteVh = (int)reader["idmantevh"];
                mante_.IdVh = (int)reader["idvh"];
                mante_.IdProve = (int)reader["idprove"];
                mante_.IdEmpleado = (int)reader["idempleado"];
                mante_.AltaF = (DateTime)reader["altaf"];
                if (reader["fechafac"] != DBNull.Value)
                {
                    mante_.FechaFac = (DateTime)reader["fechafac"];
                }
                else
                {
                    mante_.FechaFac = null;
                }

                if (reader["fecharem"] != DBNull.Value)
                {
                    mante_.FechaRem = (DateTime)reader["fecharem"];
                }
                else
                {
                    mante_.FechaRem = null;
                }

                mante_.Dominio = (string)reader["dominio"];
                mante_.HorasMante = (int)reader["horasmante"];
                mante_.Importe = (decimal)reader["importe"];
                mante_.Imputacion = (int)reader["imputacion"];
                mante_.KmMante = (decimal)reader["kmmante"];
                //mante_.NombreEmpleado = (string)reader["nombre"];
                mante_.NumFactura = (string)reader["numfactura"];
                mante_.NumRemito = (string)reader["numremito"];
                mante_.OrdenCompra = (string)reader["ordencompra"];
                mante_.TipoMante = (string)reader["tipomante"];
                mante_.NombreProve = (string)reader["razonsocial"];
                mante_.Marca = (string)reader["nombre_marca"];
                mante_.Modelo = (string)reader["modelo"];
            }
            return mante_;
        }



        #endregion


        #region Costos

        //costos consumos totales

        public decimal CostoTotalComprasUnAnio(int _anio)
        {
            decimal _costo = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculos_CostoTotalCompras_Por_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _anio = (int)reader["Anio"];
                    _costo = (decimal)reader["IMPORTE"];
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _costo;
        }


        public decimal CostoTotalCombustibleUnAnio(int _anio)
        {
            decimal _costo = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculos_CostoTotalConsumoCombustible_Por_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    _costo = (decimal)reader["TotalCosto"];
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _costo;
        }

        public decimal CostoTotalMantenimientoUnAnio(int _anio)
        {
            decimal _costo = 0;
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculos__Mante_Total_Un_Anio";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@anio", _anio);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _anio = (int)reader["ANIO"];
                    _costo = (decimal)reader["IMPORTE"];
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _costo;
        }


        //costos consumos mensuales para un anio

        //costo del consumo del combustible mes a mes 
        public List<CostosGenericos> CostoCombustibleAnioMes(int _anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Consumo_Combustible_Costo_Por_Anio_Y_Mes";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = _anio;
                    detalleConsumo.Mes = (int)reader["mes_consumo"];
                    detalleConsumo.Importe = (decimal)reader["importe"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = 1;
                    lista.Add(detalleConsumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        // costo del mantenimiento mes a mes anual
        public List<CostosGenericos>CostoMantenimientoAnioMes(int _anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Vehiculo_Mante_Por_Mes_Anio_Total";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = _anio;
                    detalleConsumo.Mes = (int)reader["MES"];
                    detalleConsumo.Importe = (decimal)reader["IMPORTE"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = 2;
                    lista.Add(detalleConsumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        // costo de las compras mes a mes anual
        public List<CostosGenericos>CostoComprasAnioMes( int _anio)
        {
            List<CostosGenericos> lista = new List<CostosGenericos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculo_Compras_Mes_Mes_Por_Anio";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostosGenericos detalleConsumo = new CostosGenericos();
                    detalleConsumo.Anio = _anio;
                    detalleConsumo.Mes = (int)reader["MES"];
                    detalleConsumo.Importe = (decimal)reader["IMPORTE"];
                    detalleConsumo.Moneda = "$";
                    detalleConsumo.TipoCosto = 3;
                    lista.Add(detalleConsumo);
                }
                conexion.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        //costos por categoria mes a mes en un anio determinado
        public List<CostoCompraVehiculo>CostoComprasCategoriasAnio(int _anio)
        {
            List<CostoCompraVehiculo> lista = new List<CostoCompraVehiculo>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Vehiculos_Costo_Compras_PorCategoria_Mes_Anio";
            cmd.Parameters.AddWithValue("@anio", _anio);

            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostoCompraVehiculo costo = new CostoCompraVehiculo();
                    costo.Mes = (int)reader["MES"];
                    costo.Importe = (decimal)reader["IMPORTE"];
                    costo.IdCate = (int)reader["idcatevh"];
                    costo.NombreCategoria = (string)reader["nomcate"];
                    costo.Idvh = (int)reader["idvh"];
                    costo.Descripcion = (string)reader["descripcion"];
                    costo.Dominio = (string)reader["dominio"];
                    costo.Marca = (string)reader["nombre_marca"];
                    lista.Add(costo);
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
