using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using ENTIDADES;
using System.Collections.ObjectModel;

namespace BLL
{

    public class BLLProveedor
    {
        DALProveedor dAL = new DALProveedor();
        DALConexion conn = new DALConexion();
        //constructor de clase
        public BLLProveedor()
        { }

        public void GrabarProveedor(Proveedor nuevoProveedor)
        {
            dAL.CrearProveedor(nuevoProveedor);
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            dAL.ActualizarProveedor(proveedor);
        }

        public void BajaProveedor(int idproveedor)
        {
            dAL.BajaProveedor(idproveedor);

        }

        public bool ValidarProveedor(decimal cuit_proveedor)
        {

            bool resultadoValidacion = false;//controla el resultado del validacion proveedor
            int existeProveedor;
            DALProveedor dALProveedor = new DALProveedor();

            existeProveedor = dALProveedor.ProveedorValidarCuit(cuit_proveedor);
            if (existeProveedor == 0)
            {
                resultadoValidacion = false; //el proveedor no existe ,posible alta
            }
            else
            {
                resultadoValidacion = true; // el proveedor existe posible actualizacion o error de
                                            // ingreso de datos
            }

            return resultadoValidacion;
        }

        public bool ValidarBajaUnProveedor(int idproveeedor)
        {
            bool resultadoValidacion;
            int _cantidadDip; // cantidad de remitos de ingreso de productos pendientes
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Proveedor_Existe_Remitos_SinRegistrar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproveedor", idproveeedor);
            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                _cantidadDip = Convert.ToInt32(cmd.Parameters["@cantidad"].Value);
                conn.CerrarConexion();
                if (_cantidadDip>0)
                {
                    resultadoValidacion = false;
                }
                else
                {
                    resultadoValidacion = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return resultadoValidacion;
        }
        public ObservableCollection<Proveedor> ProveedorTodos()
        {
            ObservableCollection<Proveedor> proveedors = dAL.ProveedorTodos();
            return proveedors;
        }

        public List<Proveedor> ProveedorCombobox(string _razon)
        {
            List<Proveedor> proveedors = new List<Proveedor>();
            proveedors = dAL.ProveedorCombobox(_razon);
            return proveedors;
        }

        public Proveedor BuscarPorId(int idprove)
        {
            Proveedor proveedor = new Proveedor();
            proveedor = dAL.BuscarPorId(idprove);
            return proveedor;
        }

        public List<RubroProve> ListarRubros()
        {
            List<RubroProve> rubroProves = new List<RubroProve>();
            rubroProves = dAL.ListarRubros();
            return rubroProves;
        }

        public ObservableCollection<Proveedor> ProveedorPorRubro(int numrubro)
        {
            //campos locales

            ObservableCollection<Proveedor> lista_proveedores = dAL.ProveedorPorRubro(numrubro);

            return lista_proveedores; // devolvemos la lista

        }
        public void ActualizarCalificacion(int idprove, int plazo, int calidad, int precio, int atencion)
        {
            dAL.ActualizarCalificacion(idprove, plazo, calidad, precio, atencion);
            
        }

        public List<CompraP> ProveedorListaCompras(int idprove)
        {
            List<CompraP> lista = dAL.ProveedorListaCompras(idprove);
          
            return lista;
        }


    }
}
