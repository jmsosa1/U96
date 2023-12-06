using DAL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPresupuesto
    {
        DALConexion conn = new DALConexion();
        DALPresupuesto corePresupuesto = new DALPresupuesto();

        /* reglas de negocio de los presupuestos
         *
         - solo puede haber un presupuesto activo 
        - el presupuesto activo tiene que ser del corriente anio
        - el presupuesto se da de baja automaticamente una vez que cambia el anio en fecha de sistema
         - el presupuesto solo puede ser generado por usuarios administradores
        - el presupuesto solo puede ser actualizado por usuarios administradores
        - la unica actualizacion manual de un presupuesto activo es del porcentaje aprobado en funcion de lo presupuestado
        - la unica actualizacion automatica se da desde el registro de las compras de productos o vehiculos
        - para que exista un adicional de un presupuesto, debe existir el presupuesto en cuestion y debe estar activo.
        - el presupuesto puede tener muchos adicionales
        - los adicionales son un nuevo detalle del presupuesto activo sobre el cual se quiere generar 
        - el presupuesto puede ser visto por todos los usuarios
        - los presupuestos se deben expresar en moneda pesos y su equivalente en pesos
        - Notificar : cuando un item del presupuesto esta exediendo el porcentaje aprobado
        - Notificar: cuado el presupuesto excede el total aprobado
        - Notificar : cuando se da de alta un nuevo adicional
        - Notificar: cuando se imprime un presupuesto activo
        - Notificar : cuando se exporta a excel el presupuesto
        - Notificar : cuando se genera un adicional de un presupuesto
         */


        public bool ValidarEncabezadoPresupuesto(Presupuesto presupuesto)
        {
            //validamos que los datos ingresados al encabezado sean correctos
            return false;
        }

        public int ValidarExistenciaPresupuestoActivo() 
        {
            //validamos que exista o no  presupuesto activo
            // para eso debemos consultar en la tabla de presupuesto que si hay alguno activo
            int id_presupuesto = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "ValExistPresActivo";
           
            try
            {
                conn.AbriConexion();
                 int i = cmd.ExecuteNonQuery();
                id_presupuesto = Convert.ToInt32(cmd.Parameters["@idpresu"].Value);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return id_presupuesto;
        }

         public PresupuestoVh UltimoIdPresupuesto()
        {
            PresupuestoVh presupuestoVh = new PresupuestoVh();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "UltimoPResupuesto";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    presupuestoVh.IdPre = (int)reader["idpre"];
                    presupuestoVh.Numero = (string)reader["numero"];
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return presupuestoVh;
        }

        public List<Presupuesto> ListarTodosLosPresupuestos()
        {
            List<Presupuesto> lista = corePresupuesto.ListarTodosLosPresupuestos();
           
            return lista;
        }
        public Presupuesto BuscarUnPresupuesto(int idpre)
        {
            Presupuesto presupuesto = corePresupuesto.BuscarUnPresupuesto(idpre);
            return presupuesto;
        }
        public ObservableCollection<Presupuesto_Item> ListarDetalleUnPresupuesto(int idpresupuesto, int tipopre)
        {
            ObservableCollection<Presupuesto_Item> lista = corePresupuesto.ListarDetalleUnPresupuesto(idpresupuesto,tipopre);
            return lista;
        }
        //validamos que el presupuesto tenga los datos que necesitamos

        public bool ValidarDatosPresupuestoEncabezado (Presupuesto presupuesto)
        {
            if (presupuesto == null)
            {
                return false;
            }
            else
            {
                if (presupuesto.Titulo == "")
                {
                    return false;
                }
                else
                {
                    if (presupuesto.DolarPresupuesto == 0)
                    {
                        return false;
                    }
                    else { return true; }
                }
            }
        }
        public void AltaEncabezado(Presupuesto presupuesto)
        {
            corePresupuesto.AltaEncabezado(presupuesto);
        }

        public void AltaDetalle(Presupuesto_Item presupuesto_item, int idusuario)
        {
            corePresupuesto.AltaDetalle(presupuesto_item,idusuario);
        }
    }
}
