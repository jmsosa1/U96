using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DAL
{
    public class DALMaquinas
    {
        DALConexion conn = new DALConexion();

        public DALMaquinas() { }

        #region MAquinas

        // agregar un MPM
        public void AgregarMPM(Mpm mpm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Alta_Encabezado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", mpm.IdProducto);
            cmd.Parameters.AddWithValue("@altaf", mpm.AltaF);
            cmd.Parameters.AddWithValue("@cantidadtareas", mpm.CantidadTareas);
            cmd.Parameters.AddWithValue("@cumplimiento", mpm.Cumplimiento);
            cmd.Parameters.AddWithValue("@estado", mpm.Estado);
            cmd.Parameters.AddWithValue("@situacion", mpm.Situacion);
            cmd.Parameters.AddWithValue("@consumos", mpm.CantAcuUnidades);

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

        //agregar detalle MPM
        public void AgregarDetalleMPM(MpmDetalle detalle)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Alta_Detalle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", detalle.Idmpm);
            cmd.Parameters.AddWithValue("@elemento", detalle.ElementoObservable);
            cmd.Parameters.AddWithValue("@descritarea", detalle.DescriTarea);
            cmd.Parameters.AddWithValue("@frecuencia", detalle.Frecuencia);
            cmd.Parameters.AddWithValue("@unidad", detalle.Unidad);
            cmd.Parameters.AddWithValue("@estado", detalle.EstadoTarea);
            cmd.Parameters.AddWithValue("@gap", detalle.Gap);
            cmd.Parameters.AddWithValue("@finicio", detalle.FechaInicio);
            cmd.Parameters.AddWithValue("@fvencimiento", detalle.FechaVencimiento);
            cmd.Parameters.AddWithValue("@situacion", detalle.SituacionTarea);


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

        //borrar un detalle de un mpm
        public void BorrarDetalleMPM(int idmpmp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Borrar_Detalle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", idmpmp);

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
        // borrar un mpm y su detalle caso de un baja de la planilla
        public void BorrarUnMPM(int idmpm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Borrar_Uno";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", idmpm);

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

        //listar planillas de mantenimientos de todas las maquinas
        public ObservableCollection<Mpm> ListarMPM()
        {
            ObservableCollection<Mpm> lista = new ObservableCollection<Mpm>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mpm mpm = new Mpm();
                    Producto producto = new Producto();


                    lista.Add(mpm);
                }
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        //gestion de los elementos observables 
        public void ABMElementosObservables(ElementoMPM elemento, string operacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Alta_Encabezado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@op", operacion);
            cmd.Parameters.AddWithValue("@id", elemento.IdElemento);
            cmd.Parameters.AddWithValue("@nombre", elemento.NombreElemento);
            cmd.Parameters.AddWithValue("@descri", elemento.Descripcion);



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


        //obetener el ultimo idmpm
        public int ObtenerUltimoIdMPM()
        {
            int _ultimoIdDoc = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MPM_Obtener_Ultimo_Id";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _ultimoIdDoc = (int)reader["idmpm"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return _ultimoIdDoc;
        }

        // obtener la planilla activa de una maquina
        public Mpm ObtenerMPMUnaMaquina(int idmaquina)
        {
            Mpm mpm = new Mpm();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Obtener_Encabezado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", idmaquina);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mpm.AltaF = (DateTime)reader["altaf"];
                    mpm.CantidadTareas = (int)reader["cantidad_tareas"];
                    mpm.Cumplimiento = (decimal)reader["cumplimiento"];
                    mpm.Estado = (int)reader["estado"];
                    mpm.Idmpm = (int)reader["idmpm"];
                    mpm.IdProducto = (int)reader["idproducto"];
                    mpm.Situacion = (string)reader["situacion"];
                    mpm.CantAcuUnidades = (int)reader["cant_acu_unidades"];
                    if (reader["fecha_cierre"] == DBNull.Value)
                    {
                        mpm.FechaCierre = null;
                    }
                    else
                    {
                        mpm.FechaCierre = (DateTime)reader["fecha_cierre"];
                    }

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return mpm;
        }

        //obtener la lista de tareas de una planilla activa
        public ObservableCollection<MpmDetalle> ObtenerDetalleMPMUnaMaquina(int idmpm)
        {
            ObservableCollection<MpmDetalle> lista = new ObservableCollection<MpmDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Obtener_DetalleMPM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", idmpm);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MpmDetalle detalle = new MpmDetalle();
                    detalle.Id = (int)reader["id"];
                    detalle.Idmpm = (int)reader["idmpm"];
                    detalle.FechaInicio = (DateTime)reader["fecha_inicio"];
                    detalle.DescriTarea = (string)reader["descri_tarea"];
                    detalle.ElementoObservable = (string)reader["elemento"];
                    detalle.EstadoTarea = (string)reader["estado_tarea"];
                    detalle.Frecuencia = (int)reader["frecuencia"];
                    detalle.Unidad = (string)reader["unidad"];
                    detalle.Gap = (int)reader["gap"];

                    if (reader["fecha_vencimiento"] == DBNull.Value)
                    {
                        detalle.FechaVencimiento = null;
                    }
                    else
                    {
                        detalle.FechaVencimiento = (DateTime)reader["fecha_vencimiento"];
                    }
                    detalle.SituacionTarea = (string)reader["situacion_tarea"];

                    lista.Add(detalle);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        // borrar un tarea de la lista de tareas de la planilla
        public void BorrarTarea(int idtarea, int idmpm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Borrar_Una_Tarea";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idtarea", idtarea);
            cmd.Parameters.AddWithValue("@idmpm", idmpm);
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

        // cumplir una tarea
        public void CumplirUnaTarea(MpmRegistro registro)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Cumplir_Una_Tarea";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", registro.Idmpm);
            cmd.Parameters.AddWithValue("@fecha", registro.FechaRegEjecucion);
            cmd.Parameters.AddWithValue("@tipo", registro.TipoEjecutor);
            cmd.Parameters.AddWithValue("@idejecutor", registro.IdEjecutor);
            cmd.Parameters.AddWithValue("@resultado", registro.ResultadoEjecucion);
            cmd.Parameters.AddWithValue("@observaciones", registro.Observaciones);
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

        public void RegistrarConsumoMaquina(int idmpm, int consumo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "MPM_Reg_Consumo_Maq";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idmpm", idmpm);
            cmd.Parameters.AddWithValue("@consumo", consumo);

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

        // calcular la cantidad de tareas vencidas
        public List<MpmDetalle> TareasVencidasMaquinas(int ejecucion, string situacion)
        {
            List<MpmDetalle> lista = new List<MpmDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Mpm_Tareas_Vencidas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", 0);
            cmd.Parameters.AddWithValue("@ejecucion", ejecucion);
            cmd.Parameters.AddWithValue("@situacion", situacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MpmDetalle mpm = new MpmDetalle();
                    mpm.Id = (int)reader["id"];
                    mpm.Idmpm = (int)reader["idmpm"];
                    mpm.DescriTarea = (string)reader["descri_tarea"];
                    mpm.Frecuencia = (int)reader["frecuencia"];
                    mpm.Unidad = (string)reader["unidad"];
                    mpm.FechaVencimiento = (DateTime)reader["fecha_vencimiento"];
                    mpm.EstadoTarea = (string)reader["estado_tarea"];
                    mpm.Gap = (int)reader["gap"];
                    mpm.FechaInicio = (DateTime)reader["fecha_inicio"];
                    mpm.Ejecucion = (int)reader["ejecucion"];
                    mpm.SituacionTarea = (string)reader["situacion_tarea"];
                    mpm.IdProducto = (int)reader["idproducto"];
                    mpm.NombreProducto = (string)reader["nombre"];

                    lista.Add(mpm);
                }
                conn.CerrarConexion();
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
