using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.ObjectModel;
using ENTIDADES;
using DAL;

namespace BLL
{
    public class BLLEmpleados
    {
        DALEmpleados dAL = new DALEmpleados();
        public BLLEmpleados()
        { }

        //este metodo lo llamamos desde la intefaz de grabar un empleado, es decir cuando se hace click
        //en el boton de guardar.
        //en funcion del valor devuelto ,se debera desplegar un comentario sobre si se grabo con existo
        // o bien el empleado ya existe y se actualizo el mismo 
        public void CrearEmpleado(Empleado nuevo)
        {
            dAL.CrearEmpleado(nuevo);
        }

        public void ActualizarEmpleado(Empleado empleado)
        {
            dAL.ActualizarEmpleado(empleado);

        }
        //validacion de existencia del empleado en base a su dni
        public bool ValidarEmpleado(int dni_empleado)
        {
            bool resultadoValidacion = false;//controla el resultado del validacion proveedor
            int existeEmpleado;
            DALEmpleados dALEmpleados = new DALEmpleados();

            existeEmpleado = dALEmpleados.EmpleadoValidarDni(dni_empleado);
            if (existeEmpleado == 0)
            {
                resultadoValidacion = false; //el empleado no existe ,posible alta
            }
            else
            {
                resultadoValidacion = true; // el empleado existe posible actualizacion o error de
                                            // ingreso de datos
            }

            return resultadoValidacion;
        }

        //metodo para verificar el login del empleado que inicia en sistema
        public bool LoginEmpleado(string nomuser, string pass)
        {
            bool login_ok = false;  //controla el resultado del login del usuario

            DALConexion conexion = new DALConexion(); // creamos la conexion 
            try
            {
                // creamos el comando y seteamos sus parametros 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Usuario_Valida_Login";
                cmd.Parameters.AddWithValue("@nomuser", nomuser);
                cmd.Parameters.AddWithValue("@clave", pass);
                SqlDataReader Reg = null;
                Reg = cmd.ExecuteReader();
                if (Reg.Read())
                {
                    login_ok = true;

                }
                else
                {
                    login_ok = false;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return login_ok;

        }

        public Usuario DatosUsuario(string nomuser, string pass)
        {
            DALConexion conexion = new DALConexion(); // creamos la conexion 
            try
            {
                Usuario usuario = new Usuario();
                // creamos el comando y seteamos sus parametros 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion.AbriConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_Usuario_Valida_Login";
                cmd.Parameters.AddWithValue("@nomuser", nomuser);
                cmd.Parameters.AddWithValue("@clave", pass);
                SqlDataReader Reg = null;
                Reg = cmd.ExecuteReader();
                while (Reg.Read())
                {
                    usuario.Iniciales = (string)Reg["iniciales"];
                    usuario.RolUsuario = (string)Reg["roluser"];
                    usuario.IdUsuario = (int)Reg["idusuario"];
                    usuario.NomUser = (string)Reg["nomuser"];
                    usuario.Email = (string)Reg["email"];
                    usuario.Deposito = (int)Reg["deposito"];

                }

                return usuario;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ObservableCollection<Empleado> EmpleadosActivos()
        {

            DALEmpleados dALEmpleados = new DALEmpleados();
            ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();
            empleados = dALEmpleados.EmpleadosActivos();
            return empleados;
        }

        public List<CategoriaEmpleado> ListaCateEmpleado()
        {
            DALEmpleados dALEmpleados = new DALEmpleados();
            List<CategoriaEmpleado> categoriaEmpleados = new List<CategoriaEmpleado>();
            categoriaEmpleados = dALEmpleados.ListaCateEmpleado();
            return categoriaEmpleados;
        }

        public ObservableCollection<Empleado> EmpleadosPorCategoria(int _idcate)
        {
            DALEmpleados dALEmpleados = new DALEmpleados();
            ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();
            empleados = dALEmpleados.EmpleadosPorCategoria(_idcate);
            return empleados;
        }

        public Empleado BuscarPorNombre(string nom_empleado)
        {
            Empleado empleado = new Empleado();
            empleado = dAL.BuscarPorNombre(nom_empleado);
            return empleado;
        }

        public List<Empleado> EmpleadosPorNombre(string nombre)
        {
            List<Empleado> empleados = new List<Empleado>();
            empleados = dAL.EmpleadosPorNombre(nombre);
            return empleados;
        }


        public ObservableCollection<Autorizacion_vh> AutorizacionesDeVehiculo(int _idempleado)
        {

            ObservableCollection<Autorizacion_vh> autorizacion_s = new ObservableCollection<Autorizacion_vh>();
            autorizacion_s = dAL.AutorizacionesDeVehiculo(_idempleado);
            return autorizacion_s;
        }

        public Empleado BuscarPorId(int idempleado)
        {

            Empleado empleado = new Empleado();
            empleado = dAL.BuscarPorId(idempleado);
            return empleado;


        }

        public List<SectorEmpleado> ListarSectorEmpleado()
        {

            List<SectorEmpleado> lista_sectores = new List<SectorEmpleado>();
            lista_sectores = dAL.ListarSectorEmpleado();

            return lista_sectores;

        }


        #region Balance
        public void ActualizarBalance(int idempleado, int idproducto, int cantidad, int imputacion, decimal preciop)
        {
            dAL.ActualizarBalance(idempleado, idproducto, cantidad, imputacion, preciop);
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleado(int _idempleado)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleado(_idempleado);
           
            return _balance;

        }

        public void ActualizarBalancePorDevolucion(int idempleado, int idproducto, int cantidad, int imputacion)
        {
            dAL.ActualizarBalancePorDevolucion(idempleado, idproducto, cantidad, imputacion);
        }

        public void CambiarEstadoProductoEnBalance(int idproducto, int imputacion, int idempleado, int idestado)
        {
            dAL.CambiarEstadoProductoEnBalance(idproducto,imputacion,idempleado,idestado);
        }

        public void ActualizarBalancePorDescuento(int _idempleado, int _idproducto, int _cantidad, int _imputacion, decimal _preciop)
        {
            dAL.ActualizarBalancePorDescuento(_idempleado, _idproducto, _cantidad, _imputacion, _preciop);
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnaImputacion(int _imputacion, int _idempleado)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleadoUnaImputacion(_imputacion,_idempleado);
            
           
            return _balance;
        }
        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnaImputacionTipoP(int _imputacion, int _idempleado, int _idtipo)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleadoUnaImputacionTipoP(_imputacion, _idempleado, _idtipo);
           
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnaImputacionTipoYCategoria(int _imputacion, int _idempleado, int _idtipo, int _idcate)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleadoUnaImputacionTipoYCategoria(_imputacion, _idempleado, _idtipo, _idcate);
           
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnTipoP(int _idempleado, int _idtipop)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleadoUnTipoP(_idempleado,_idtipop);
            
            return _balance;
        }

        public ObservableCollection<BalanceEmpleado> BalanceEmpleadoUnTipoYCategoria(int _idempleado, int _idtipo, int _idcate)
        {
            ObservableCollection<BalanceEmpleado> _balance = dAL.BalanceEmpleadoUnTipoYCategoria(_idempleado, _idtipo, _idcate);
           
            return _balance;

        }
        public void ActualizarBalancePorBajaDSO(int _imputacion, int _idempleado, int _idproducto, int _cantidad, decimal _precioItem)
        {
            // este metodo da marcha atras con los productos imputados a un empleado u obra
            //
            dAL.ActualizarBalancePorBajaDSO(_imputacion, _idempleado, _idproducto, _cantidad, _precioItem);
        }

        public void ActualizarBalancePorBajaDDO(int _imputacion, int _idempleado, int _idproducto, int _cantidad, decimal _precioItem)
        {
            // este metodo da marcha atras con los productos imputados a un empleado u obra
            //
            dAL.ActualizarBalancePorBajaDDO(_imputacion, _idempleado, _idproducto, _cantidad, _precioItem);
        }

        public List<DetDescuentoEmpleado> ListaProductosDescuento(int idempleado)
        {
            List<DetDescuentoEmpleado> lista = dAL.ListaProductosDescuento(idempleado);
           
            return lista;
        }

        public void BorrarItemDescuento(DetDescuentoEmpleado det, int idempleado, int imputacion)
        {

            dAL.BorrarItemDescuento(det, idempleado,imputacion);
        }

        public void BorrarValeDescuento(int iddocumento)
        {
            dAL.BorrarValeDescuento(iddocumento);
        }


        public void ActualizarFotoEmpleado(Empleado empleado)
        {
            dAL.ActualizarFotoEmpleado(empleado);
        }

        #endregion

    }
}
