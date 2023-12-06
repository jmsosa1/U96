using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DAL
{
    public class DALForo
    {
        //creamos un obejeto conexion desde la clase DALConexion
        DALConexion conn = new DALConexion();


        public DALForo()
        {

        }

        #region Notas

        public void AgregarNota( NotaSahmV6 nota)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Nota_Add";
            cmd.Parameters.AddWithValue("@fecha", nota.FechaAlta);
            cmd.Parameters.AddWithValue("@titulo", nota.Titulo);
            cmd.Parameters.AddWithValue("@contenido", nota.Contenido);
            cmd.Parameters.AddWithValue("@idusuario", nota.IdUsuario);
            cmd.Parameters.AddWithValue("@imagenestado", nota.ImagenEstado);
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

        public void BorrarNota( int idnota )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Nota_Delete";
            cmd.Parameters.AddWithValue("@idnota", idnota);
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

        public void BajaNota(int idnota)
        {
            //este metod cambia el estado de una nota a inactivo
            // y la situacion a baja cuando se da de baja
            // 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Nota_Baja";
            cmd.Parameters.AddWithValue("@idnota", idnota);
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

        public void ActualizarSituacionNota(DateTime _fecha) 
        {
            //este metodo refiere a cambiar el estado de la nota de nuevo a vieja
            // y se ejecuta cuando se incia la aplicacion
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Nota_CambiarSituacion";
            cmd.Parameters.AddWithValue("@fechaactual", _fecha);
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

        public NotasLeidos BuscarNotaLeida( int idusuario, int idnota)
        {
            NotasLeidos l = new NotasLeidos();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idnota", idnota);
            cmd.Parameters.AddWithValue("@idusuario", idusuario);
            try
            {
                conn.AbriConexion();
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    l.IdNota = (int)reader["idnota"];
                    l.IdUsuario = (int)reader["idusuario"];
                    l.FechaLectura = (DateTime)reader["fechalectura"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return l;
        }

        public void MarcarComoLeidaNota(int idnota, int idusuario, DateTime fecha)
        {
            // este metodo refiere a confirmar la lectura de la nota por parte de alguien
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Foro_Leer_Una_Nota";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idusuario", idusuario);
            cmd.Parameters.AddWithValue("@idnota", idnota);
            cmd.Parameters.AddWithValue("@fechalectura", fecha);
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

      

        public void CerrarNota(int idnota)
        {
            //este metodo refiere a cambiar el estado a cumplido de la nota, por parte de quien la creo
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Cerrar_Nota";
            cmd.Parameters.AddWithValue("@idnota", idnota);
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

        public ObservableCollection<NotaSahmV6>ObtenerNotasActivas()
        {
            //metodoo que trae solamente las notas que estan activas y por lo tanto abiertas
            ObservableCollection<NotaSahmV6> notas = new ObservableCollection<NotaSahmV6>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Listar_Notas_Activas";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NotaSahmV6 nota = new NotaSahmV6();
                    nota.IdNota = (int)reader["idnota"];
                    nota.IdUsuario = (int)reader["idusuario"];
                    nota.IdEstado = (int)reader["idestado"];
                    nota.FechaAlta = (DateTime)reader["fechaalta"];
                    nota.CantLecturas = (int)reader["cantlecturas"];
                    nota.CantRespuesta = (int)reader["cantrespuesta"];
                    nota.Contenido = (string)reader["contenido"];
                    nota.Titulo = (string)reader["titulo"];
                    nota.Situacion = (int)reader["situacion"];
                    if (reader["imagenestado"]!= DBNull.Value)
                    {
                        nota.ImagenEstado = (byte[])reader["imagenestado"];
                    }
                    else
                    {
                        nota.ImagenEstado = null;
                    }
                   
                   /* if( reader["imagen_usuario"] != DBNull.Value)
                    {
                        nota.ImagenUsuario = (byte[])reader["imagen_usuario"];
                    }
                    else
                    {
                        nota.ImagenUsuario = null;
                    }*/
                    
                    nota.NombreUsuario = (string)reader["iniciales"];
                    notas.Add(nota);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return notas;
        }

        public ObservableCollection<NotaSahmV6> FiltrarNotasUnUsuario(int idusuario)
        {
            ObservableCollection<NotaSahmV6> lista = new ObservableCollection<NotaSahmV6>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Foro_Listar_Notas_Usuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idusuario", idusuario);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NotaSahmV6 nota = new NotaSahmV6();
                    nota.IdNota = (int)reader["idnota"];
                    nota.IdUsuario = (int)reader["idusuario"];
                    nota.IdEstado = (int)reader["idestado"];
                    nota.FechaAlta = (DateTime)reader["fechaalta"];
                    nota.CantLecturas = (int)reader["cantlecturas"];
                    nota.CantRespuesta = (int)reader["cantrespuesta"];
                    nota.Contenido = (string)reader["contenido"];
                    nota.Titulo = (string)reader["titulo"];
                    nota.Situacion = (int)reader["situacion"];
                    if (reader["imagenestado"] != DBNull.Value)
                    {
                        nota.ImagenEstado = (byte[])reader["imagenestado"];
                    }
                    else
                    {
                        nota.ImagenEstado = null;
                    }

                   /* if (reader["imagen_usuario"] != DBNull.Value)
                    {
                        nota.ImagenUsuario = (byte[])reader["imagen_usuario"];
                    }
                    else
                    {
                        nota.ImagenUsuario = null;
                    }*/

                    nota.NombreUsuario = (string)reader["iniciales"];
                    lista.Add(nota);
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

        #region Respuestas

        public void AgregarRespuesta (RespuestaNota respuesta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Respuesta_Add";
            cmd.Parameters.AddWithValue("@fecha", respuesta.Fecha);
            cmd.Parameters.AddWithValue("@idnota", respuesta.IdNota);
            cmd.Parameters.AddWithValue("@idusuario", respuesta.IdUsaurio);
            cmd.Parameters.AddWithValue("@contenido", respuesta.Contenido);
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

        public void BorrarRespuesta(int idrespuesta, int idnota)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Respuesta_Delete";
            cmd.Parameters.AddWithValue("@idrespuesta", idrespuesta);
            cmd.Parameters.AddWithValue("@idnota", idnota);
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

        public List<RespuestaNota>ObtnerRespuestasUnaNota(int idnota)
        {
            List<RespuestaNota> lista = new List<RespuestaNota>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Foro_Listar_Respuestas_Una_Nota";
            cmd.Parameters.AddWithValue("@idnota", idnota);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RespuestaNota respuesta = new RespuestaNota();
                    respuesta.IdRespuesta = (int)reader["idrespuesta"];
                    respuesta.IdUsaurio = (int)reader["idusario"];
                    respuesta.IdNota = (int)reader["idnota"];
                    respuesta.Fecha = (DateTime)reader["fecha"];
                    respuesta.Contenido = (string)reader["contenido"];
                    respuesta.NombreUsuarioResp = (string)reader["iniciales"];

                    lista.Add(respuesta);
                }
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
