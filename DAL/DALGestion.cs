using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ENTIDADES;
using System.Collections.ObjectModel;


namespace DAL
{
    //clase que contiene los metodos para las operaciones de las planificacion de las tareas del sector 
    //como asi tambien las planificaciones de los mantenimientos y 
    // las solictudes de abastecimiento

   public class DALGestion
    {
        //creamos un obejeto conexion desde la clase DALConexion
        DALConexion conn = new DALConexion();

        #region[tareas del sector]

        //alta de una tarea
        public int TareaNuevaAlta(TareaSector tareaSector)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Alta";
            cmd.Parameters.AddWithValue("@altaf", tareaSector.AltaF);
            cmd.Parameters.AddWithValue("@estado", tareaSector.EstadoTarea);
            cmd.Parameters.AddWithValue("@importancia", tareaSector.ImportanciaTarea);
            cmd.Parameters.AddWithValue("@creador", tareaSector.UsuarioCreador);
            cmd.Parameters.AddWithValue("@fnecesidad", tareaSector.Fnecesidad);
            cmd.Parameters.AddWithValue("@items", tareaSector.CantidadItems);
            cmd.Parameters.AddWithValue("@titulo", tareaSector.TituloTarea);
            cmd.Parameters.AddWithValue("@ultimamodi", tareaSector.Ultimamodi);
            cmd.Parameters.AddWithValue("@img", tareaSector.ImageEstadoTemp);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int TareaModificar(TareaSector tareaSector)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Modi";
            cmd.Parameters.AddWithValue("@idtarea", tareaSector.IdTarea);
            cmd.Parameters.AddWithValue("@importancia", tareaSector.ImportanciaTarea);
            cmd.Parameters.AddWithValue("@fnecesidad", tareaSector.Fnecesidad);
            cmd.Parameters.AddWithValue("@titulo", tareaSector.TituloTarea);
            cmd.Parameters.AddWithValue("@ultimamodi", tareaSector.Ultimamodi);
            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int TareaBorrar(int _idtarea)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Borrar";
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);
            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int TareaCancelar(int _idtarea, DateTime _fcancelado)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Cancelar_Una";
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);
            cmd.Parameters.AddWithValue("@fcancelado", _fcancelado);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public void TareaCumplirUna(int idtarea, DateTime _fcumplido)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Cumplir_Una";
            cmd.Parameters.AddWithValue("@idtarea", idtarea);
            cmd.Parameters.AddWithValue("@fcumplido", _fcumplido);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ConfirmarTareaBorrar(int _idtarea)
        {

            int filas = 0;
            int registrosafectados = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);
            cmd.Parameters.Add("@filasdetalle", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conn.AbriConexion();
                filas = cmd.ExecuteNonQuery();
                registrosafectados = Convert.ToInt16(cmd.Parameters["@filasdetalle"].Value);
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return registrosafectados;
        }

        public int DetalleTareaAlta(DetTareaSector detTareaSector)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Detalle_Alta";
            cmd.Parameters.AddWithValue("@idtarea", detTareaSector.IdTarea);
            cmd.Parameters.AddWithValue("@descripcion", detTareaSector.DescriTarea);
            cmd.Parameters.AddWithValue("@item",detTareaSector.NumItem);
            
            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;

        }

        public int DetalleTareaBorrar(int _iddet, int _idtarea)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Detalle_Borrar";
            cmd.Parameters.AddWithValue("@iddet", _iddet);
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int DetalleTareaCumplimiento(DetTareaSector det)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_TareaSector_Detalle_Cumplir"
            };
            cmd.Parameters.AddWithValue("@iddet", det.IdDetTarea);
            cmd.Parameters.AddWithValue("@idtarea",det.IdTarea);
            cmd.Parameters.AddWithValue("@fechacumplimiento", det.Fcumplimiento);
            cmd.Parameters.AddWithValue("@observacion", det.Observacion);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int DetalleTareaCancelar(DetTareaSector det)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_TareaSector_Detalle_Cancelar"
            };
            cmd.Parameters.AddWithValue("@iddet", det.IdDetTarea);
            cmd.Parameters.AddWithValue("@idtarea", det.IdTarea);
            cmd.Parameters.AddWithValue("@fechacancel", det.Fcumplimiento);
            cmd.Parameters.AddWithValue("@observacion", det.Observacion);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int DetalleTareaTomarSeguidor(DetTareaSector detTareaSector)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_TareaSector_Detalle_Tomar_Seguidor"
            };
            cmd.Parameters.AddWithValue("@iddet", detTareaSector.IdDetTarea);
            cmd.Parameters.AddWithValue("@idtarea",detTareaSector.IdTarea);
            cmd.Parameters.AddWithValue("@userseguidor", detTareaSector.IdSeguidor);

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public TareaSector ObetenerUltimoIdTarea()
        {
            TareaSector tareaSector = new TareaSector();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Ultima_Tarea";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tareaSector.IdTarea = (int)reader["idtarea"];

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tareaSector;
        }

        public ObservableCollection<TareaSector> TareasSectorTodas()
        {
            ObservableCollection<TareaSector> lista_tareas = new ObservableCollection<TareaSector>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Todas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_tareas = ArmarlistaTareas(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_tareas;
        }

        public ObservableCollection<TareaSector>TareasSectorSeguimientoUsuario(int idusuario)
        {
            ObservableCollection<TareaSector> lista_tareas = new ObservableCollection<TareaSector>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Mostrar_Seguimiento_Usuario";
            cmd.Parameters.AddWithValue("@idusuario ",idusuario);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_tareas = ArmarlistaTareas(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_tareas;
        }

        private ObservableCollection<TareaSector> ArmarlistaTareas(SqlDataReader reader)
        {
            ObservableCollection<TareaSector> tareas = new ObservableCollection<TareaSector>();
            while (reader.Read())
            {
                TareaSector t = new TareaSector();
                t.IdTarea = (int)reader["idtarea"];
                t.AltaF = (DateTime)reader["altaf"];
                t.CantidadItems = (int)reader["cantidad_items"];
                t.DiasEjecucion = (int)reader["dias_ejecucion"];
                t.EstadoTarea = (string)reader["estado_tarea"];
                t.EstadoTemporal = (int)reader["estado_temporal"];
               
                if (reader["fcierre"] != DBNull.Value)
                {
                    t.Fcierre = (DateTime)reader["fcierre"];
                }
                else
                {
                    t.Fcierre = null;
                }
               
                if (reader["fnecesidad"]!= DBNull.Value)
                {
                    t.Fnecesidad = (DateTime)reader["fnecesidad"];
                }
                else
                {
                    t.Fnecesidad = null;
                }
                t.ImportanciaTarea = (string)reader["importancia_tarea"];
                t.NombreCreador = (string)reader["nomuser"];
                t.PorcentajeCumplimiento = (int)reader["porcentaje_cumplimiento"];
                t.TituloTarea = (string)reader["titulo"];
                t.UsuarioCreador = (int)reader["usuario_creador"];
                t.Ultimamodi = (DateTime)reader["ultimamodi"];
                t.Vencida = (int)reader["vencida"];
                t.ImageEstadoTemp = (byte[])reader["img_estado"];
                tareas.Add(t);
            }
            return tareas;
        }

        public ObservableCollection<DetTareaSector>DetalleTareaSector(int _idtarea)
        {
            ObservableCollection<DetTareaSector> detTareas = new ObservableCollection<DetTareaSector>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Detalle_Una_Tarea";
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                detTareas = ArmarDetalleTareaSector(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return detTareas;
        }

        private ObservableCollection<DetTareaSector> ArmarDetalleTareaSector(SqlDataReader reader)
        {
            ObservableCollection<DetTareaSector> dets = new ObservableCollection<DetTareaSector>();
            while (reader.Read())
            {
                DetTareaSector d = new DetTareaSector();
                d.IdDetTarea= (int)reader["iddet"];
                d.IdTarea = (int)reader["idtarea"];
                d.DescriTarea = (string)reader["descripcion_tarea"];
                d.IdSeguidor = (int)reader["usuario_seguidor"];
                d.EstadoItem = (string)reader["estado_item"];
                if (reader["fcumplimiento"] != DBNull.Value)
                {
                    d.Fcumplimiento = (DateTime)reader["fcumplimiento"];
                }
                else
                {
                    d.Fcumplimiento = null;
                }
             
                d.NumItem = (int)reader["num_item"];
                d.Observacion = (string)reader["observacion"];
                d.NombreSeguidor = (string)reader["nomuser"];
                dets.Add(d);
            }
            return dets;
        }


        public int TareaCalcularCumplimiento(int _idtarea)
        {
            int porcentaje = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Calcular_Cumplimiento";
           
            cmd.Parameters.AddWithValue("@idtarea", _idtarea);
            cmd.Parameters.Add("@cumplimiento", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                porcentaje = Convert.ToInt16(cmd.Parameters["@cumplimiento"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return porcentaje;
        }


        public void TareaCalcularVencidas()
        { }

        public void TareaModicarEstadoTemporal(DateTime fechaactual)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_TareaSector_Modi_Estado_Tiempo";
            cmd.Parameters.AddWithValue("@fecha2", fechaactual);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void OTMModificarEstadoTemporal(DateTime fechaactual)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Modi_EstadoTemporal";
            cmd.Parameters.AddWithValue("@fecha2", fechaactual);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region[planes mantenimiento]

        public int OTMAlta(Otm otm)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Alta";
            cmd.Parameters.AddWithValue("@altaf",otm.Alta);
            cmd.Parameters.AddWithValue("@idvh",otm.Idvh);
            cmd.Parameters.AddWithValue("@idproducto", otm.Idproducto);
            
            cmd.Parameters.AddWithValue("@fnecesidad",otm.FNecesidad);
            
            cmd.Parameters.AddWithValue("@titulo",otm.Titulo);
            cmd.Parameters.AddWithValue("@nota",otm.Nota);
            cmd.Parameters.AddWithValue("@creador",otm.UsuarioCreador);
            cmd.Parameters.AddWithValue("@imagen",otm.Img_Estado);
            cmd.Parameters.AddWithValue("@tipo",otm.Tipo_Otm);
            cmd.Parameters.AddWithValue("@estado_otm", otm.Estado_Otm);
            cmd.Parameters.AddWithValue("@lecturakm", otm.LecturaKm);
            cmd.Parameters.AddWithValue("@lecturahs", otm.LecturaHs);
            cmd.Parameters.AddWithValue("@cantitems", otm.CantItems);
            
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return fila;
        }

        public int OTMBaja(int _idotm)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Baja";
            cmd.Parameters.AddWithValue("@idotm", _idotm);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return fila;

        }

        public void OTMCierre(Otm otm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Cierre";
            cmd.Parameters.AddWithValue("@idotm", otm.IdOtm);
            cmd.Parameters.AddWithValue("@observacion_cierre", otm.Observacion);
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


        
        public ObservableCollection<Otm> OTM_Todas_VH()
        {
            ObservableCollection<Otm> otms = new ObservableCollection<Otm>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Todas_Vh";
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                otms = ArmarOtmVh(lectura);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return otms;
        }

        private ObservableCollection<Otm> ArmarOtmVh(SqlDataReader reader)
        {
            ObservableCollection<Otm> lista_otm = new ObservableCollection<Otm>();
            while (reader.Read())
            {
                Otm ot = new Otm();
                ot.IdOtm = (int)reader["idotm"];
                ot.Alta = (DateTime)reader["altaf"];
                ot.Titulo = (string)reader["titulo"];
                ot.Dominio = (string)reader["dominio"];
                ot.Estado_Otm = (string)reader["estado_otm"];
                if (reader["fcumplimiento"] != DBNull.Value)
                {
                    ot.FCumplimiento = (DateTime)reader["fcumplimiento"];
                } else { ot.FCumplimiento = null; };
                if (reader["fnecesidad"] != DBNull.Value)
                {
                    ot.FNecesidad = (DateTime)reader["fnecesidad"];
                 }
                else
                {
                    ot.FNecesidad = null;
                } ;
                
               
                ot.Idvh =(int)reader["idvh"] ;
                ot.Img_Estado =(byte[])reader["img_estado"] ;
                ot.Nota =(string)reader["nota"] ;
                ot.Tipo_Otm = (int)reader["tipo_otm"];
                ot.UsuarioCreador =(int)reader["usuario_creador"] ;
                
                ot.NombreUsuario =(string)reader["nomuser"];
                ot.ModeloVh = (string)reader["modelo"];
                ot.DescriVh = (string)reader["descripcion"];
                ot.LecturaHs = (int)reader["lectura_hs"];
                ot.LecturaKm = (decimal)reader["lectura_km"];
                ot.CantItems = (int)reader["cantidad_items"];
                ot.PCumplido = (int)reader["porcentaje_cumplimiento"];
                ot.Est_Tmp = (int)reader["estado_temporal"];
                ot.Vencida = (int)reader["vencida"];

                lista_otm.Add(ot);
            }
            return lista_otm;
        }

        public ObservableCollection<Otm> OTM_Todas_Producto()
        {
            ObservableCollection<Otm> otms = new ObservableCollection<Otm>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Todas_Producto";
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                otms = ArmarOtmProducto(lectura);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return otms;
        }

        private ObservableCollection<Otm> ArmarOtmProducto(SqlDataReader reader)
        {
            ObservableCollection<Otm> lista_otm = new ObservableCollection<Otm>();
            while (reader.Read())
            {
                Otm ot = new Otm();
                ot.IdOtm = (int)reader["idotm"];
                ot.Alta = (DateTime)reader["altaf"];
                ot.Titulo = (string)reader["titulo"];
                
                ot.Estado_Otm = (string)reader["estado_otm"];
                if (reader["fcumplimiento"] != DBNull.Value)
                {
                    ot.FCumplimiento = (DateTime)reader["fcumplimiento"];
                }
                else { ot.FCumplimiento = null; };
                if (reader["fnecesidad"] != DBNull.Value)
                {
                    ot.FNecesidad = (DateTime)reader["fnecesidad"];
                }
                else
                {
                    ot.FNecesidad = null;
                };

                
                ot.Idvh = (int)reader["idproducto"];
                ot.Img_Estado = (byte[])reader["img_estado"];
                ot.Nota = (string)reader["nota"];
                ot.Tipo_Otm = (int)reader["tipo_otm"];
                ot.UsuarioCreador = (int)reader["usuario_creador"];
                
                ot.NombreUsuario = (string)reader["nomuser"];
                ot.NombreProducto = (string)reader["nombre"];
                ot.CodInventario = (string)reader["cod_inventario"];
                lista_otm.Add(ot);
            }
            return lista_otm;
        }


        public Otm ObtenerUltimoIdOtm()
        {
            Otm otm_ultimo_id = new Otm();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Ultima_Id";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    otm_ultimo_id.IdOtm = (int)reader["idotm"];

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return otm_ultimo_id;
        }

        public int DetalleOtmAlta(OtmDetalle otmDetalle)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Detalle_Alta";
            cmd.Parameters.AddWithValue("@idotm", otmDetalle.IdOtm);
            cmd.Parameters.AddWithValue("@numitem", otmDetalle.NumItem);
            cmd.Parameters.AddWithValue("@descri_item", otmDetalle.DescripcionItem);
            cmd.Parameters.AddWithValue("@idvh", otmDetalle.Idvh);
            cmd.Parameters.AddWithValue("@idcatemante", otmDetalle.IdCateMante);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;

        }

        public ObservableCollection<OtmDetalle>DetalleOTM(int _idotm)
        {

            ObservableCollection<OtmDetalle> det_otm = new ObservableCollection<OtmDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Detalle_Una";
            cmd.Parameters.AddWithValue("@idotm",_idotm);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                det_otm = ArmarDetalleOtm(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return det_otm;

        }

        public ObservableCollection<OtmDetalle>DetalleOTM_Cumplido_NR(int _idvh)
        {
            ObservableCollection<OtmDetalle> det_otm = new ObservableCollection<OtmDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Detalle_Cumplidos_NR_Idvh";
            cmd.Parameters.AddWithValue("@idvh",_idvh);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                det_otm = ArmarDetalleOtm(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return det_otm;

        }
        private ObservableCollection<OtmDetalle> ArmarDetalleOtm(SqlDataReader reader)
        {
            ObservableCollection<OtmDetalle> otms = new ObservableCollection<OtmDetalle>();
            while (reader.Read())
            {
                OtmDetalle detalle = new OtmDetalle();
               
                detalle.DescripcionItem =(string)reader["descripcion_item"];
                detalle.EstadoItem = (string)reader["estado_item"];
                detalle.FacturaProve =(string)reader["factura_proveedor"] ;
                detalle.IdCateMante =(int)reader["idcatemante"];
                detalle.IdDet =(int)reader["id_det_otm"];
                detalle.IdOtm =(int)reader["idotm"];
                detalle.IdProve =(int)reader["idprove"];
                detalle.NombreSeguidor =(string)reader["nomuser"];
                detalle.NomCateMante =(string)reader["nomcate"];
                detalle.NumItem =(int)reader["num_item"];
                detalle.Observacion =(string)reader["obs_item"];
                detalle.OcProve =(string)reader["oc_proveedor"];
                detalle.RazonProve =(string)reader["razonsocial"];
                detalle.RemitoProve =(string)reader["remito_proveedor"];
                detalle.UsuarioSeguidor =(int)reader["usuario_seguidor"];
                if (reader["fcumplimiento"]!=DBNull.Value)
                {
                    detalle.FCumplimiento = (DateTime)reader["fcumplimiento"];
                }
                else
                {
                    detalle.FCumplimiento = null;
                }
                if (reader["img_observacion"] == DBNull.Value)
                {
                    detalle.Img_Observacion = null;
                }
                else
                {
                    detalle.Img_Observacion = (byte[])reader["img_observacion"];
                }
                otms.Add(detalle);
            }
            return otms;
        }

        public int OtmDetalleTomarSeguidor(OtmDetalle otmDetalle)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_OTM_Detalle_Toma_Seguidor"
            };

            cmd.Parameters.AddWithValue("@iddetotm", otmDetalle.IdDet);
            cmd.Parameters.AddWithValue("@idotm", otmDetalle.IdOtm);
            cmd.Parameters.AddWithValue("@idusuario", otmDetalle.UsuarioSeguidor);

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int OtmDetalleCumplimiento(OtmDetalle detalle)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_OTM_Detalle_Cumplir"
            };
            cmd.Parameters.AddWithValue("@iddet", detalle.IdDet);
            cmd.Parameters.AddWithValue("@idotm", detalle.IdOtm);
            cmd.Parameters.AddWithValue("@fcumplimiento", detalle.FCumplimiento);
            cmd.Parameters.AddWithValue("@observacion", detalle.Observacion);
            cmd.Parameters.AddWithValue("@imagen", detalle.Img_Observacion);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }

        public int OtmDetalleCancelar(OtmDetalle detalle)
        {
            int tarea = 0;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn.AbriConexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "pa_OTM_Detalle_Cancelar"
            };
            cmd.Parameters.AddWithValue("@iddet", detalle.IdDet);
            cmd.Parameters.AddWithValue("@idotm", detalle.IdOtm);
            cmd.Parameters.AddWithValue("@fcumplimiento", detalle.FCumplimiento);
            cmd.Parameters.AddWithValue("@observacion", detalle.Observacion);
            cmd.Parameters.AddWithValue("@imagen", detalle.Img_Observacion);

            try
            {
                conn.AbriConexion();
                tarea = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }


        public int OtmCalcularCumplimiento(int _idotm)
        {
            int porcentaje = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Calcular_Cumplimiento";

            cmd.Parameters.AddWithValue("@idotm", _idotm);
            cmd.Parameters.Add("@cumplimiento", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                porcentaje = Convert.ToInt16(cmd.Parameters["@cumplimiento"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return porcentaje;

        }

        public void OtmCumplirUna(int _idotm, DateTime _fcumplido)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Cumpir_Una";
            cmd.Parameters.AddWithValue("@idotm", _idotm);
            cmd.Parameters.AddWithValue("@fcumplido", _fcumplido);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int OtmItemsCumplidosNoRegistrados(int _idvh)
        {
            int cantidadItem = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_OTM_Item_Cumplidos_No_Registrados";
            cmd.Parameters.AddWithValue("@idvh", _idvh);
            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                cantidadItem = Convert.ToInt16(cmd.Parameters["@cantidad"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return cantidadItem;
        }

        public void OtmRegistrarUnItemDetalle(OtmDetalle otmDetalle)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "OTM_Detalle_Registrar_Un_Item";
            cmd.Parameters.AddWithValue("@idotm", otmDetalle.IdOtm);
            cmd.Parameters.AddWithValue("@itemotm", otmDetalle.NumItem);
            cmd.Parameters.AddWithValue("@ocproveedor", otmDetalle.IdProve);
            cmd.Parameters.AddWithValue("@factproveedor", otmDetalle.FacturaProve);
            cmd.Parameters.AddWithValue("@remitoproveedor", otmDetalle.RemitoProve);
            cmd.Parameters.AddWithValue("@idprove", otmDetalle.IdProve);

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

        #region[solicitudes]

        public int SolicitudAB_alta(Solicitud_Ab solicitud_)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Alta";
            cmd.Parameters.AddWithValue("@altaf", solicitud_.AltaF);
            cmd.Parameters.AddWithValue("@creador", solicitud_.UsuarioCreador);
            cmd.Parameters.AddWithValue("@fnecesidad", solicitud_.FNecesidad);
            cmd.Parameters.AddWithValue("@imputacion", solicitud_.Imputacion);
            cmd.Parameters.AddWithValue("@titulo", solicitud_.Titulo);
            
            cmd.Parameters.AddWithValue("@img", solicitud_.ImgEstado);
            cmd.Parameters.AddWithValue("@idempleado", solicitud_.IdEmpleado);
            cmd.Parameters.AddWithValue("@nota", solicitud_.Nota);
            cmd.Parameters.AddWithValue("@cantidaditems", solicitud_.CantidadItems);

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;

        }

        public int SolicitudAB_alta_detalle(Solicitud_ab_detalle ab_Detalle)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_alta_detalle";
            cmd.Parameters.AddWithValue("@idsol", ab_Detalle.IdSol);
            cmd.Parameters.AddWithValue("@cantidad", ab_Detalle.Cantidad);
            cmd.Parameters.AddWithValue("@observacion", ab_Detalle.Observacion);
            cmd.Parameters.AddWithValue("@num_item", ab_Detalle.NumItem);
            cmd.Parameters.AddWithValue("@descri_item", ab_Detalle.DescriItem);

            

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public Solicitud_Ab ObtenerUltimoIdSolicitud()
        {
            Solicitud_Ab ultimsol = new Solicitud_Ab();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Ultima_IdSol";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ultimsol.IdSol  = (int)reader["idsol"];

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return ultimsol;
        }

        public ObservableCollection<Solicitud_Ab>SolicitudAb_Todas()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Todas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_solicitud = ArmarlistaSolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_solicitud;
        }

        private ObservableCollection<Solicitud_Ab> ArmarlistaSolicitud(SqlDataReader reader)
        {
            ObservableCollection<Solicitud_Ab> solicitudes = new ObservableCollection<Solicitud_Ab>();
            while (reader.Read())
            {
                Solicitud_Ab s = new Solicitud_Ab();
                s.AltaF = (DateTime)reader["altaf"];
                s.CantidadItems = (int)reader["cantidad_items"];
                s.EstadoSolicitud = (string)reader["estado_solicitud"];
                s.EstadoTemporal = (int)reader["estado_temporal"];
                if (reader["fcumplimiento"]!= DBNull.Value)
                {
                    s.Fcumplimiento = (DateTime)reader["fcumplimiento"];
                }
                else
                {
                    s.Fcumplimiento = null;
                }
                
                s.FNecesidad = (DateTime)reader["fnecesidad"];
                s.IdEmpleado = (int)reader["idempleado"];
                s.IdSol = (int)reader["idsol"];
                s.ImgEstado = (byte[])reader["img_estado"];
                s.Imputacion = (int)reader["imputacion"];
                s.NomObra = (string)reader["cliente"];
                s.NomSeguidor = (string)reader["nomuser"];
                s.Nota = (string)reader["nota"];
                s.PorcentajeCumplimiento = (int)reader["porcentaje_cumplimiento"];
                s.SolicitadoPor = (string)reader["nombre"]; // nombre del empleado / jefe que necesesita las herramientas
                s.Titulo = (string)reader["titulo"];
                s.UsuarioCreador = (int)reader["usuario_creador"];
                 s.UsuarioSeguidor= (int)reader["usuario_seguidor"];
                s.Vencida = (int)reader["vencida"];

                solicitudes.Add(s);
            }
            return solicitudes;
        }

        public ObservableCollection<Solicitud_ab_detalle>SolicitudAb_Detalle_Una(int _idsol)
        {
            ObservableCollection<Solicitud_ab_detalle> lista_detalle = new ObservableCollection<Solicitud_ab_detalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_Ab_Detalle_Una";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_detalle = ArmarDetallesolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_detalle;
        }

        private ObservableCollection<Solicitud_ab_detalle> ArmarDetallesolicitud(SqlDataReader reader)
        {
            ObservableCollection<Solicitud_ab_detalle> ab_Detalles = new ObservableCollection<Solicitud_ab_detalle>();
            while (reader.Read())
            {
                Solicitud_ab_detalle d = new Solicitud_ab_detalle();
                d.IdDetSol = (int)reader["iddetsol"];
                d.IdSol = (int)reader["idsol"];
                d.Cantidad = (int)reader["cantidad"];
                d.Observacion = (string)reader["observacion"];
                d.EstadoItem = (string)reader["estado_item"];
                d.NumItem = (int)reader["num_item"];
                d.DescriItem = (string)reader["descripcion_item"];
               


                ab_Detalles.Add(d);
            }
            return ab_Detalles;
        }

        public int SolicitudTomarUna(int _idsol, int _iduser)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Tomar";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            cmd.Parameters.AddWithValue("@iduser", _iduser);

            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return fila;

        }

        public int SolicitudAB_Anular(int _idsol)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_SolicitudAB_Anular_Una";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int SolicitudAB_Cumplir_Un_Item(int _idsol , int _iddet, int _iduser)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Detalle_Cumplir";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            cmd.Parameters.AddWithValue("@iddetsol", _iddet);
            cmd.Parameters.AddWithValue("@ucpitem", _iduser);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int SolicitudAB_Cancelar_Un_Item(int _idsol, int _iddet, int _iduser)
        {
            int fila = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Detalle_Cancelar";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            cmd.Parameters.AddWithValue("@iddetsol", _iddet);
            cmd.Parameters.AddWithValue("@ucitem", _iduser);
            try
            {
                conn.AbriConexion();
                fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();

            }
            catch (Exception)
            {

                throw;
            }
            return fila;
        }

        public int SolicitudAB_Calcular_Cumplimiento(int _idsol) //parametros output
        {
            int porcentaje = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitudes_AB_Calcular_Cumplimiento";

            cmd.Parameters.AddWithValue("@idsol", _idsol);
            cmd.Parameters.Add("@cumplimiento", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                porcentaje = Convert.ToInt16(cmd.Parameters["@cumplimiento"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return porcentaje;
        }

        public void SolicitudAB_Cumplir_Una(int _idsol, DateTime _fcumplido)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Cumplir_Una";
            cmd.Parameters.AddWithValue("@idsol", _idsol);
            cmd.Parameters.AddWithValue("@fcumplido", _fcumplido);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SolicitudesModificarEstadoTemporal(DateTime fechaactual)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitudes_AB_Modi_Estado_Tiempo";
            cmd.Parameters.AddWithValue("@fecha2", fechaactual);
            try
            {
                conn.AbriConexion();
                int fila = cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ObservableCollection<Solicitud_Ab>SolicitudAb_Pendiente()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Todas_Pendientes";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_solicitud = ArmarlistaSolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_solicitud;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Cumplido()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Todas_Cumplidas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_solicitud = ArmarlistaSolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_solicitud;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Anulado()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Todas_Anuladas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_solicitud = ArmarlistaSolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_solicitud;
        }


        public ObservableCollection<Solicitud_Ab> SolicitudesAB_JefeObra(string _jefe)
        {
            string _busca_jefe = _jefe + "%";
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_AB_Obra_Jefe";
            cmd.Parameters.AddWithValue("@nomjefe",_busca_jefe);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                lista_solicitud = ArmarlistaSolicitud(reader);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return lista_solicitud;

        }


        //este metodo devuelve el dataset con el encabezado de la solicitud para imprimir
        public DataSet SolicitudAb_Imprimir(int idsol) 
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Solicitud_Encabezado";
            cmd.Parameters.AddWithValue("@idsol", idsol);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn.AbriConexion();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "pa_Solicitud_Detalle";
            cmd1.Parameters.AddWithValue("@idsol", idsol);

            DataSet dataSet = new DataSet(); // creamos el dataset que vamos a devolver
            SqlDataAdapter daEncabezado = new SqlDataAdapter(cmd); //dataadapter para rellenar el dataset
            SqlDataAdapter daDetalle = new SqlDataAdapter(cmd1);
            try
            {
                conn.AbriConexion(); // abrimos coneccion
                daEncabezado.Fill(dataSet,"Solicitud_Encabezado");//llenamos el dataset
                daDetalle.Fill(dataSet, "Solicitud_Detalle");
                conn.CerrarConexion(); // cerramos conexion
                
            }
            catch (Exception)
            {

                throw;
            }
            return dataSet; //devolvemos el data set
        }

       
        #endregion

    }

}
