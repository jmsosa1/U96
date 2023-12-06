using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DAL;
using ENTIDADES;
using System.Collections.ObjectModel;


namespace BLL
{
   public  class BLLBase
    {
        #region Declarativas

       
        DALBase dalBase = new DALBase(); 
        #endregion

        public int CalcularDiasAsignacionVehiculo(DateTime _fechadiahoy)
        {
            int filasafectadas;
            filasafectadas = dalBase.CalcularDiasAsignacionVehiculo(_fechadiahoy);

            return filasafectadas;
        }

        public DateTime UltimaFechaCAS()
        {
            DateTime ultimafecha = dalBase.UltimaFechaCAS();
            return ultimafecha;
        }

        public int ActualizarFechaCAS(DateTime fecha)
        {
            int filaAfectada;
            filaAfectada = dalBase.ActualizarFechaCAS(fecha);
            return filaAfectada;
        }

        //registro de operaciones de un usuario
        public int UsuarioRegOp(LogOperaciones log)
        {
            int fila =0;
            fila = dalBase.UsuarioRegOp(log);
            return fila;
        }

        public List<Provincia> ListaProvincia()
        {
            List<Provincia> provincias = new List<Provincia>();
            provincias = dalBase.ListaProvincia();
            return provincias;
        }

        public List<Localidad> ListaLocalidad(int idprovincia)
        {
            List<Localidad> localidads = new List<Localidad>();
            localidads = dalBase.ListaLocalidad(idprovincia);
            return localidads;
        }


        public int AgregarLocalidad(Localidad localidad)
        {
            int fila = 0;
            fila = dalBase.AgregarLocalidad(localidad);
            return fila;
        }

        public int ModificarLocalidad(Localidad localidad)
        {
            int fila = 0;
            fila = dalBase.ModificarLocalidad(localidad);
            return fila;
        }

        public void TareasCalcularDiasEjecucion(DateTime _fechadiahoy)
        {
            dalBase.TareasCalcularDiasEjecucion(_fechadiahoy);
        }

        public int CantidadTareasUsuario(int idusuario)
        {
            //metodo que devuelve la cantidad de tareas que tiene seguimiento un usuario
            int cantidadtareas = dalBase.CantidadTareasUsuario(idusuario);
            return cantidadtareas;

        }

        public void ActualizaEstadoDocuVehiculos(DateTime fecha_actual)
        {

            dalBase.ActualizaEstadoDocuVehiculos(fecha_actual);
        }

        public List<VehiculoDocu> ChekeoDocumentacionVehiculos()
        {
            //procedimiento que chekea cuantos vehiculos tienen documentacion vencidad
            List<VehiculoDocu> vehiculos_doc_v = new List<VehiculoDocu>();
            vehiculos_doc_v = dalBase.ChekeoDocumentacionVehiculos();
            return vehiculos_doc_v;
        }

        public List<VehiculoDocu> ChekeoDocAVencer()
        {
            //procedimiento que chekea cuantos vehiculos tienen documentacion vencida
            List<VehiculoDocu> vehiculos_doc_v = new List<VehiculoDocu>();
            vehiculos_doc_v = dalBase.ChekeoDocAVencer();
            return vehiculos_doc_v;
        }

        public Deposito DepositoBuscarUno(int iddeposito)
        {
            Deposito deposito = new Deposito();
            deposito = dalBase.DepositoBuscarUno(iddeposito);
            return deposito;

        }

        public List<Monedas> ListarVariacionMoneda(int anio)
        {
            List<Monedas> variaciones =  dalBase.ListarVariacionMoneda(anio);
            return variaciones;
        }
        public List<Deposito> ListarDepositos()
        {
            List<Deposito> depositos = dalBase.ListarDepositos();
          
         
            return depositos;
        }

        public List<CausaBaja> ListaCausasBaja()
        {
            List<CausaBaja> causaBajas =dalBase.ListaCausasBaja();
           
            return causaBajas;
        }

        public string UltimaVersion()
        {
            string ultima_version = dalBase.UltimaVersion();
           
            return ultima_version;
        }

        public List<Monedas>ListaMonedasSimbolos()
        {
            List<Monedas> lista = dalBase.ListaMonedasSimbolos();
            return lista;
         
        }


        public List<Usuario> ListarUsuariosSistema()
        {
            List<Usuario> lista = dalBase.ListarUsuariosSistema();
            
            return lista;
        }

        #region Coeficientes
        public ObservableCollection<Coeficiente> ListarCoeficientes()
        {
            ObservableCollection<Coeficiente> lista = dalBase.ListarCoeficientes();
            return lista;
        }


        public void CoeficienteSave(Coeficiente coeficiente)
        {
            dalBase.CoeficienteSave(coeficiente);
        }

        public void CoeficienteDelete(int idcof)
        {
            dalBase.CoeficienteDelete(idcof);
        }
        public Coeficiente BuscarUno(int id)
        {
            Coeficiente coeficiente = dalBase.BuscarUno(id);
            return coeficiente;
        }
        #endregion

        #region MAquinasTaller

        public void RecalcularGapVencimientosDias(DateTime date)
        {
            dalBase.RecalcularGapVencimientosDias(date);
        }
        #endregion
        public BLLBase() //constructor de clase
        { }
    }
}
