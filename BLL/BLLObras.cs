using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace BLL
{
    public class BLLObras
    {
        DALObra dALObra = new DALObra();
        public BLLObras()
        { }


        public Obra BuscarImputacion(int imputacion)
        {
            Obra obra = new Obra();
            obra = dALObra.BuscarImputacion(imputacion);
            return obra;
        }

        public int GrabarObra(Obra nuevaObra)
        {

            int fila = 0;
            dALObra.GrabarObra(nuevaObra);
            return fila;
        }

        public void ActualizarObra(Obra obra_actualizar)
        {
            dALObra.ActualizarObra(obra_actualizar);
        }
        public List<CategoriaObra> ObrasCategoria()
        {
            List<CategoriaObra> cateobras = new List<CategoriaObra>();
            cateobras = dALObra.ObrasCategoria();
            return cateobras;
        }

        public List<Obra> ObrasActivas()
        {

            List<Obra> lista_obras = new List<Obra>();
            lista_obras = dALObra.ObrasActivas();
            return lista_obras;

        }

        //validar imputacion 
        public bool ValidarNumeroImputacion(int imputacion)
        {
            bool existe_imputacion = dALObra.ValidarNumeroImputacion(imputacion);
            return existe_imputacion;
        }

        public List<Casa> ListarCasaUnaObra(int _imputacion)
        {
            List<Casa> casas = dALObra.ListarCasaUnaObra(_imputacion);


            return casas;
        }
        public void BajaDeUnaObra(int imputacion)
        {
            dALObra.BajaDeUnaObra(imputacion);
        }

        public List<BalanceObraTiposVehiculo> BalanceTiposVehiculo(int _imputacion)
        {
            List<BalanceObraTiposVehiculo> lista = dALObra.BalanceTiposVehiculo(_imputacion);

            return lista;
        }

        public List<BalanceObraProductos> BalanceTipoProductos(int _imputacion)
        {
            List<BalanceObraProductos> lista = dALObra.BalanceTipoProductos(_imputacion);

            return lista;
        }
        public List<Mante_vh> ListarGastosMantenimientoVehiculos(int _imputacion)
        {
            List<Mante_vh> lista = dALObra.ListarGastosMantenimientoVehiculos(_imputacion);

            return lista;
        }
        public List<Mante_P> ListarGastosMantenimientoProductos(int _imputacion)
        {
            List<Mante_P> lista = dALObra.ListarGastosMantenimientoProductos(_imputacion);

            return lista;
        }

        public List<BalanceObraEmpleados> ListarEmpleadosCostoUnaObra(int imputacion)
        {
            List<BalanceObraEmpleados> lista = dALObra.ListarEmpleadosCostoUnaObra(imputacion);

            return lista;

        }

        public List<BalanceObraCatPro> BalanceCatProducto(int _imputacion, int _idtipoproducto)
        {
            List<BalanceObraCatPro> lista = dALObra.BalanceCatProducto(_imputacion,_idtipoproducto);
            
            return lista;
        }

        public List<BalanceObraProductosDetalle> BalanceProductosUnaObra(int imputacion)
        {

            List<BalanceObraProductosDetalle> _balance = dALObra.BalanceProductosUnaObra(imputacion);

           
            return _balance;
        }

        public List<CategoriaP> BalanceCatePUnaObra(int imputacion)
        {
            List<CategoriaP> _balance = dALObra.BalanceCatePUnaObra(imputacion);
          
            return _balance;
        }

        public List<Asignacion_vh> ListarAsignacionesUnaObra(int _imputacion)
        {
            List<Asignacion_vh> lista = dALObra.ListarAsignacionesUnaObra(_imputacion);
           
            return lista;
        }

        public List<Asignacion_vh> ListaVehiculosUnaObra(int _imputacion)
        {
            List<Asignacion_vh> lista = dALObra.ListaVehiculosUnaObra(_imputacion);
            
            return lista;
        }

        public List<Empleado> ListarEmpleadosUnaObra(int _imputacion)
        {
            List<Empleado> lista = dALObra.ListarEmpleadosUnaObra(_imputacion);
           
            return lista;

        }


    }
}
