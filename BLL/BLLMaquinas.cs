using DAL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLMaquinas
    {
        DALMaquinas coreM = new DALMaquinas();
        DALConexion conn = new DALConexion();

        #region Maquinas



        // agregar un MPM
        public void AgregarMPM(Mpm mpm)
        {
            coreM.AgregarMPM(mpm);
        }

        //agregar detalle MPM
        public void AgregarDetalleMPM(MpmDetalle detalle)
        {
            coreM.AgregarDetalleMPM(detalle);
        }

        public int ObtenerUltimoIdMPM()
        {
            int _ultimoIdDoc = coreM.ObtenerUltimoIdMPM();
            return _ultimoIdDoc;
        }

        //validar que una maquina tenga planilla
        //mpm

        public bool ValidarExistenciaPlanillaMPM(int idproducto)
        {
            bool _existe;
            Mpm mpm = new Mpm();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MPM_Validar_Existencia";
            cmd.Parameters.AddWithValue("@idpro", idproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mpm.IdProducto = (int)reader["idproducto"];
                }
                if (mpm.IdProducto == 0)
                {
                    _existe = false;
                }
                else
                {
                    _existe = true;
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _existe;
        }

        // obtener la planilla activa de una maquina
        public Mpm ObtenerMPMUnaMaquina(int idmaquina)
        {
            Mpm mpm = coreM.ObtenerMPMUnaMaquina(idmaquina);

            return mpm;
        }

        //obtener la lista de tareas de una planilla activa
        public ObservableCollection<MpmDetalle> ObtenerDetalleMPMUnaMaquina(int idmpm)
        {
            ObservableCollection<MpmDetalle> lista = coreM.ObtenerDetalleMPMUnaMaquina(idmpm);

            return lista;
        }

        //borrar una tarea
        public void BorrarTarea(int idtarea, int idmpm)
        {
            coreM.BorrarTarea(idtarea, idmpm);
        }

        //borramos un detalle
        //borrar un detalle de un mpm
        public void BorrarDetalleMPM(int idmpmp)
        {
            coreM.BorrarDetalleMPM(idmpmp);

        }


        // borrar un mpm y su detalle caso de un baja de la planilla
        public void BorrarUnMPM(int idmpm)
        {
            coreM.BorrarUnMPM(idmpm);
        }

        public void CumplirUnaTarea(MpmRegistro registro)
        {
            coreM.CumplirUnaTarea(registro);
        }

        public void RegistrarConsumoMaquina(int idmpm, int consumo)
        {
            coreM.RegistrarConsumoMaquina(idmpm, consumo);
        }

        public List<MpmDetalle> TareasVencidasMaquinas(int ejecucion, string situacion)
        {
            List<MpmDetalle> lista = new List<MpmDetalle>();
            lista = coreM.TareasVencidasMaquinas(ejecucion,situacion);
            return lista;
        }
        #endregion

    }
}
