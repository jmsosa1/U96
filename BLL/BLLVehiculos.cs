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
    public class BLLVehiculos
    {


        DALVehiculo dALVehiculo = new DALVehiculo();
        DALBase dalBase = new DALBase();

        

        #region[ABM]
        public int VehiculoAlta(Vehiculo vehiculo)
        {
            int registro = dALVehiculo.VehiculoAlta(vehiculo);
            return registro;
        }

        public int VehiculoActualiza(Vehiculo vehiculo)
        {
            int registro = dALVehiculo.VehiculoActualiza(vehiculo);
            return registro;
        }
        public void VehiculoBaja(int idvh, int idcausa, DateTime fecha, int idusuario, string causa)
        {
            dALVehiculo.VehiculoBaja(idvh, idcausa, fecha, idusuario, causa);
        }

        public bool ValidarDominio(string dominio)
        {
            bool resulValidacion = false;
            int fila = dALVehiculo.VehiculoValidarDominio(dominio);
            if (fila==0)
            {
                resulValidacion = false;
            }
            else { resulValidacion = true; }

            return resulValidacion;
        }

        public int VehiculoBorrarUno(int _idvh)
        {
            int _filasAfectadas = dALVehiculo.VehiculoBorrarUno(_idvh);
            return _filasAfectadas;

        }
        #endregion

        #region[planificacion]
        public int VehiculoAltaPlanificacion(PlanificacionVH planificacionVH)
        {
            int _filasafectadas = dALVehiculo.VehiculoAltaPlanificacion(planificacionVH);
            return _filasafectadas;
        }
        public int VehiculoBajaPlanificacion(int _idplanificacion)
        {
            int _filasafectadas = dALVehiculo.VehiculoBajaPlanificacion(_idplanificacion);
            return _filasafectadas;
        }

        public bool VehiculoCambioEstadoPlanificacion(int _idplanificacion, string _nuevoestado)
        {
            bool cambioestado = true;
            cambioestado = dALVehiculo.VehiculoCambioEstadoPlanificacion(_idplanificacion, _nuevoestado);
            return cambioestado;
        }
        public int VehiculoModiPlanificacion(PlanificacionVH planificacionVH)
        {
            int _filasAfectadas = dALVehiculo.VehiculoModiPlanificacion(planificacionVH);
            return _filasAfectadas;
        }
        public int VehiculoAnulaPLanificacion(int _idpl, string _notab, DateTime _fbaja)
        {
            int _filaAfectadas = dALVehiculo.VehiculoAnulaPLanificacion(_idpl, _notab, _fbaja);
            return _filaAfectadas;
        }
        public ObservableCollection<PlanificacionVH> ListarTodasPlanificaciones()
        {
            ObservableCollection<PlanificacionVH> planificacionVHs = new ObservableCollection<PlanificacionVH>();
            planificacionVHs = dALVehiculo.ListarTodasPlanificaciones();
            return planificacionVHs;
        }

        public bool ValidaFechasPlan(int idvh, DateTime _desde, DateTime _hasta)
        {
            bool _validaFechas = dALVehiculo.ValidaFechasPlan(idvh, _desde, _hasta);
            return _validaFechas;

        }

        #endregion

        #region[consumos]
        public int RegistrarConsumo(ConsumoCombustible consumo)
        {
            int filaAfectada = dALVehiculo.RegistrarConsumo(consumo);
            return filaAfectada;
        }

        public void VehiculoActualizaHorasAcumuladas(int _idvh, decimal horas, int _imputacion)
        {
            dALVehiculo.VehiculoActualizaHorasAcumuladas(_idvh, horas, _imputacion);
        }
        public void VehiculoActualizaKmAcumulados(int _idvh, decimal kilometros, int _imputacion)
        {
            dALVehiculo.VehiculoActualizaKmAcumulados(_idvh, kilometros, _imputacion);
        }
        public void RecalcularConsumoCombustibleKM(int _idvh, decimal _kmlitro,  decimal _preciolitro)
        {
            dALVehiculo.RecalcularConsumoCombustibleKM(_idvh, _kmlitro, _preciolitro);
        }

        public void RecalcularConsumoCombustibleHS(int _idvh, decimal _hslitro, decimal _preciolitro)
        {
            dALVehiculo.RecalcularConsumoCombustibleHS(_idvh,  _hslitro, _preciolitro);
        }

        public int BorrarUnConsumo(int idcmb)
        {
            int filab = dALVehiculo.BorrarUnConsumo(idcmb);
            return filab;
        }
        public void VehiculoActualizaKmAcuMenos(int _idvh, decimal kilometros, int _imputacion)
        {
            dALVehiculo.VehiculoActualizaKmAcuMenos(_idvh, kilometros, _imputacion);
        }

        public ObservableCollection<ConsumoCombustible> ListarConsumosUnVehiculo(int idvh)
        {
            ObservableCollection<ConsumoCombustible> consumoCombustibles = new ObservableCollection<ConsumoCombustible>();
            consumoCombustibles = dALVehiculo.ListarConsumosUnVehiculo(idvh);
            return consumoCombustibles;
        }

        public ObservableCollection<ConsumoCombustible> ListarTodosLosConsumos()
        {
            ObservableCollection<ConsumoCombustible> consumoCombustibles = new ObservableCollection<ConsumoCombustible>();
            consumoCombustibles = dALVehiculo.ListarTodosLosConsumos();
            return consumoCombustibles;
        }

        public decimal TotalKM_Consumo_Uno(int idvh)
        {
            decimal totalConcepto;
            totalConcepto = dALVehiculo.TotalKM_Consumo_Uno(idvh);
            return totalConcepto;
        }
        public decimal TotalHs_Consumo_Uno(int idvh)
        {
            decimal totalConcepto;
            totalConcepto = dALVehiculo.TotalHs_Consumo_Uno(idvh);
            return totalConcepto;

        }
        public decimal TotalLitros_Consumo_Uno(int idvh)
        {
            decimal totalConcepto;
            totalConcepto = dALVehiculo.TotalLitros_Consumo_Uno(idvh);
            return totalConcepto;
        }
        public decimal TotalCostoKM_Consumo_Uno(int idvh)
        {
            decimal totalConcepto;
            totalConcepto = dALVehiculo.TotalCostoKM_Consumo_Uno(idvh);
            return totalConcepto;
        }
        public decimal TotalCostoHs_Consumo_Uno(int idvh)
        {
            decimal totalConcepto;
            totalConcepto = dALVehiculo.TotalCostoHs_Consumo_Uno(idvh);
            return totalConcepto;
        }

        public void VehiculoActualizaHsAcuMenos(int idvh, int horas, int imputacion)
        {

            dALVehiculo.VehiculoActualizaHsAcuMenos(idvh, horas, imputacion);

        }

        public List<ACCDetMesAnio> ResumenConsumoMensAnio(int _anio)
        {
            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            resumen = dALVehiculo.ResumenConsumoMensAnio(_anio);
            return resumen;
        }

        public List<ConsumoAnios> ResumenConsumoAniovsAnio()
        {
            List<ConsumoAnios> lista_consumos = dALVehiculo.ResumenConsumoAniovsAnio();
            return lista_consumos;
        }

        public List<ACC_CategoriaAnio> ResumenConsumoCategoriaAnio(int anio)
        {
            List<ACC_CategoriaAnio> lista_categorias = new List<ACC_CategoriaAnio>();
            lista_categorias = dALVehiculo.ResumenConsumoCategoriaAnio(anio);

            return lista_categorias;

        }
        public List<ACCDetMesAnio> ResumenConsumoVehiculo(int anio, int idvh)
        {
            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            resumen = dALVehiculo.ResumenConsumoVehiculo(anio, idvh);  
            return resumen;
        }

        public List<ConsumoVhMes> ListaDominiosConsumosMes(int _mes, int _anio)
        {
            List<ConsumoVhMes> lista = dALVehiculo.ListaDominiosConsumosMes(_mes,_anio);
           
            return lista;

        }


        public List<ACCDetMesAnio> DetalleConsumosMesPorAnio(int _anioConsulta)
        {
            List<ACCDetMesAnio> consumo = dALVehiculo.DetalleConsumosMesPorAnio(_anioConsulta);
           
            return consumo;
        }

        public ACCDetMesAnio DetalleConsumoUnMesUnAnio(int _anioConsulta, int _mesConsulta)
        {

            ACCDetMesAnio detalleConsumo = dALVehiculo.DetalleConsumoUnMesUnAnio(_anioConsulta, _mesConsulta);
            
            return detalleConsumo;
        }

        #endregion

        #region[asignaciones y autorizaciones]
        public int AltaAsignacion(Asignacion_vh asignacion_Vh)
        {
            int filaAfectada = dALVehiculo.AltaAsignacion(asignacion_Vh);
            return filaAfectada;
        }

        public void BajaAsignacion(int idasig, DateTime fechafin)
        {
            
             dALVehiculo.BajaAsignacion(idasig, fechafin);
            
        }

        public int BorrarAsignacion(int idasig)
        {
            int fila;
            fila = dALVehiculo.BorrarAsignacion(idasig);
            return fila;
        }

        public void CambioSF(int _idvh, int _idsf)
        {
            dALVehiculo.CambioSF(_idvh, _idsf);
        }

        public int AltaBOVh(int idvh, int imputacion, int idtipovh, int idcatevh)
        {
            int fila = dALVehiculo.AltaBOVh(idvh, imputacion, idtipovh, idcatevh);
            return fila;
        }

        public int AltaAutorizacion(Autorizacion_vh autorizacion_Vh)
        {
            int filaAfectada;
            
            filaAfectada = dALVehiculo.AltaAutorizacion(autorizacion_Vh);
            return filaAfectada;
        }
        public void BorrarAutorizacion(int id)
        {
            dALVehiculo.BorrarAutorizacion(id);


        }
        public ObservableCollection<Asignacion_vh> ListarAsignaciones()
        {
            ObservableCollection<Asignacion_vh> vhs = new ObservableCollection<Asignacion_vh>();
            vhs = dALVehiculo.ListarAsignaciones();
            return vhs;

        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesUno(int idvh)
        {
            ObservableCollection<Asignacion_vh> vhs = new ObservableCollection<Asignacion_vh>();
            vhs = dALVehiculo.ListarAsignacionesUno(idvh);
            return vhs;
        }

        public bool ExisteBalanceObra(int idvh, int imputacion)
        {
            bool _existe;
            int fila = dALVehiculo.ExisteBalanceObra(idvh, imputacion);
            if (fila == 0)
            {
                _existe = false;
            }
            else
            {
                _existe = true;
            }
            return _existe;
        }

        public ObservableCollection<Autorizacion_vh> ListarAutorizacionesActivas()
        {
            ObservableCollection<Autorizacion_vh> vhs = new ObservableCollection<Autorizacion_vh>();
            vhs = dALVehiculo.ListarAutorizacionesActivas();
            return vhs;
        }
        public ObservableCollection<Autorizacion_vh> ListarAutorizacionesActivasUno(int idvh)
        {
            ObservableCollection<Autorizacion_vh> vhs = new ObservableCollection<Autorizacion_vh>();
            vhs = dALVehiculo.ListarAutorizacionesActivasUno(idvh);
            return vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesFinalizadas()
        {
            ObservableCollection<Asignacion_vh> vhs = new ObservableCollection<Asignacion_vh>();
            vhs = dALVehiculo.ListarAsignacionesFinalizadas();
            return vhs;

        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesPorCategoria(int idcatevh)
        {
            ObservableCollection<Asignacion_vh> vhs = new ObservableCollection<Asignacion_vh>();
            vhs = dALVehiculo.ListarAsignacionesPorCategoria(idcatevh);
            return vhs;
        }

        public ObservableCollection<Asignacion_vh> ListarAsignacionesEnCursoPorCategoria(int _idcatevh)
        {
            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            asignacion_Vhs = dALVehiculo.ListarAsignacionesEnCursoPorCategoria(_idcatevh);
            return asignacion_Vhs;
        }

        public List<Empleado> VehiculoEmpleadosAutorizados(int idvh)
        {
            List<Empleado> empleados = new List<Empleado>();
            empleados = dALVehiculo.VehiculoEmpleadosAutorizados(idvh);
            return empleados;
        }

        public bool ExisteAsignacionActiva(int idvh)
        {
            bool existeAsignacion = dALVehiculo.ExisteAsignacionActiva(idvh);
            return existeAsignacion;
        }

        public ObservableCollection<Asignacion_vh> ListarTodasAsignaciones()
        {
            

            ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
            asignacion_Vhs = dALVehiculo.ListarTodasAsignaciones();
            return asignacion_Vhs;
        }

        public int BuscarAsignacionActiva(int _idvh)
        {
            int fila = dALVehiculo.BuscarAsignacionActiva(_idvh);
            return fila;
        }


        #endregion


        #region[combustibles]
        public int CombustibleAlta(Combustible combustible)
        {
            int fila = 0;
            fila = dALVehiculo.CombustibleAlta(combustible);
            return fila;
        }

        public int CombustibleModi(Combustible combustible)
        {
            int fila = 0;
            fila = dALVehiculo.CombustibleModi(combustible);
            return fila;
        }

        public int CombustibleBorrar(int idcmb)
        {
           
            int fila = 0;
            fila = dALVehiculo.CombustibleBorrar(idcmb);
            
            return fila;
        }
        public bool CombustibleExisteEnVehiculo(int idcmb)
        {
            bool resultado = false;
            int fila = 0;
            fila = dALVehiculo.CombustibleExisteEnVehiculo(idcmb);
            if (fila == 0)
            {
                resultado = false;
            }
            else
            {
                resultado = true;
            }
            return resultado;
        }

        public int CombustibleAumentarPreciosTodos(decimal porcentaje)
        {
            int filas = 0;
            filas = dALVehiculo.CombustibleAumentarPreciosTodos(porcentaje);
            return filas;
        }

        public int CombustibleBajaPreciosTodos(decimal porcentaje)
        {
            int filas = 0;
            filas = dALVehiculo.CombustibleBajaPreciosTodos(porcentaje);
            
            return filas;
        }
        public List<Combustible> VehiculosListarCombustibles()
        {
            List<Combustible> combustibles = new List<Combustible>();
            combustibles = dALVehiculo.ListaCombustibles();
            return combustibles;
        }
        public Combustible BuscarUnCombustible(int idcmb)
        {
            Combustible combustible = new Combustible();
            combustible = dALVehiculo.BuscarUnCombustible(idcmb);
            return combustible;
        }

        public ACCombustible ResumenConsumosCombustiblesAnio(int anio)
        {
            ACCombustible data = new ACCombustible();
            data = dALVehiculo.ResumenConsumosCombustiblesAnio(anio);
            return data;
        }

            #endregion

        #region[categorias y lineas]
        public int VehiculoAgregarCategoria(CategoriaVh categoria)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoAgregarCategoria(categoria);
            return fila;

        }

        public int VehiculoModificarCategoria(CategoriaVh categoria)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoModificarCategoria(categoria);
            return fila;

        }

        public int VehiculoBorraCategoria(int _idcate)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoBorraCategoria(_idcate);
            return fila;
        }

        public bool CategoriaExisteEnVehiculo(int idcatevh)
        {
            bool resultado = false;
            int fila = 0;
            fila = dALVehiculo.CategoriaExisteEnVehiculo(idcatevh);
            if (fila == 0)
            {
                resultado = false;
            }
            else
            {
                resultado = true;
            }
            return resultado;
        }

        public int VehiculoAgregarLinea(LineVh lineVh)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoAgregarLinea(lineVh);
            return fila;
        }
        public int VehiculoModiLinea(LineVh lineVh)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoModiLinea(lineVh);
            return fila;
        }

        public int VehiculoBorrarLinea(int idlineavh)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoBorrarLinea(idlineavh);
            return fila;
        }

        public bool LineaExisteEnVehiculo(int idlineavh)
        {
            bool resultado = false;
            int fila = 0;
            fila = dALVehiculo.LineaExisteEnVehiculo(idlineavh);
            if (fila == 0)
            {
                resultado = false;
            }
            else
            {
                resultado = true;
            }
            return resultado;
        }

        public List<CategoriaVh> VehiculosListarCategoria()
        {
            List<CategoriaVh> categoriaVhs = new List<CategoriaVh>();
            categoriaVhs = dALVehiculo.ListaCategoriaVh();
            return categoriaVhs;
        }

        public List<LineVh> VehiculosListarLineas(int _idcatevh)
        {
            List<LineVh> lineVhs = new List<LineVh>();
            lineVhs = dALVehiculo.ListarLineasVh(_idcatevh);
            return lineVhs;
        }

        public ObservableCollection<Vehiculo> VehiculosListarPorCategorias(int _idcategoria)
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            vehiculos = dALVehiculo.VehiculosListarPorCategorias(_idcategoria);
            return vehiculos;
        }
        public ObservableCollection<CategoriaVh> VehiculosListarCategoriaPorTipo(int idtipo)
        {
           
            ObservableCollection<CategoriaVh> lista = dALVehiculo.VehiculosListarCategoriaPorTipo(idtipo);
           
            return lista;
        }

        #endregion

        #region[consultas base]
        public ObservableCollection<Vehiculo> VehiculosListarActivos()
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            vehiculos = dALVehiculo.VehiculosListarActivos();
            return vehiculos;
        }

        //public ObservableCollection<Vehiculo> VehiculosListarBajas()
        //{
        //    ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
        //    vehiculos = dALVehiculo.VehiculosListarBajas();
        //    return vehiculos;
        //}

        public List<MarcaVh>VehiculosListarMarcas()
        {
            List<MarcaVh> marcaVhs = new List<MarcaVh>();
            marcaVhs = dALVehiculo.ListarMarcaVh();
            return marcaVhs;
        }

        public int MarcaAlta(MarcaVh _marcavh)
        {
            int fila = dALVehiculo.MarcaAlta(_marcavh);
             
            return fila;
        }

        public int MarcaModificar(MarcaVh _marcaVh)
        {
            int fila = dALVehiculo.MarcaModificar(_marcaVh);
            
            return fila;
        }

        public ObservableCollection<Vehiculo> VehiculoBuscarDominio(string dominio)
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            vehiculos = dALVehiculo.VehiculoBuscarDominio(dominio);
            return vehiculos;
        }

        public Vehiculo VehiculoBuscarUnDominio(string _dominio)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo = dALVehiculo.VehiculoBuscarUnDominio(_dominio);
            return vehiculo;
        }
        public Vehiculo BuscarPorId(int _idvehiculo)
        {
            Vehiculo vehiculo = dALVehiculo.BuscarPorId(_idvehiculo);
            return vehiculo;

        }

        public List<TipoVh> ListarTiposVh()
        {
            
            List<TipoVh> tipoVhs = new List<TipoVh>();
            tipoVhs = dALVehiculo.ListarTiposVh();
            return tipoVhs;
        }

        public ObservableCollection<RepuestoVh> RepuestosVhUnVehiculo(int idvehiculo)
        {
            ObservableCollection<RepuestoVh> repuestoVhs = new ObservableCollection<RepuestoVh>();
            repuestoVhs = dALVehiculo.RepuestosVhUnVehiculo(idvehiculo);
            return repuestoVhs;
        }



        #endregion


        #region[mantenimientos]
        public int AltaMantenimiento(Mante_vh mante_Vh)
        {
            int filaAlta = 0;
            filaAlta = dALVehiculo.AltaMantenimiento(mante_Vh);
            return filaAlta;
        }

        public void AltaDetaMante(DetManteVh detManteVh)
        {
           
          dALVehiculo.AltaDetaMante(detManteVh);
            
        }
        public int AnularMantenimiento(int _idmantevh)
        {
            int filaBorra = 0;
            filaBorra = dALVehiculo.AnularMantenimiento(_idmantevh);
            return filaBorra;
        }
        public int AnularDetaMante(int _idmantevh)
        {
            int filaBorra = 0;
            filaBorra = dALVehiculo.AnularDetaMante(_idmantevh);
            return filaBorra;
        }

        public List<CategoriaManteVh> ListarCateMante()
        {
            List<CategoriaManteVh> categoriaMantes = new List<CategoriaManteVh>();
            categoriaMantes = dALVehiculo.ListarCateMante();
            return categoriaMantes;
        }
        public Mante_vh ObtenerUltimoIdMante()
        {
            Mante_vh ultimoMante = new Mante_vh();
            ultimoMante = dALVehiculo.ObtenerUltimoIdMante();
            return ultimoMante;
        }

        public ObservableCollection<Mante_vh> VehiculoListarTodosLosMantenimientosUno(int _idvh)
        {
            ObservableCollection<Mante_vh> _Vhs = new ObservableCollection<Mante_vh>();
            _Vhs = dALVehiculo.VehiculoListarTodosLosMantenimientosUno(_idvh);
            return _Vhs;
        }

        public Mante_vh BuscarUnMantenimiento(int idmantevh)
        {
            Mante_vh mante = new Mante_vh();
            mante = dALVehiculo.BuscarUnMantenimiento(idmantevh);
            return mante;
        }

        public ObservableCollection<Mante_vh> ListarTodosLosMantenimientos(DateTime f1, DateTime f2)
        {
            ObservableCollection<Mante_vh> mante_s = new ObservableCollection<Mante_vh>();
            mante_s = dALVehiculo.ListarTodosLosMantenimientos(f1, f2);
            return mante_s;

        }

        public List<DetManteVh> ListarDetallesManteUno(int idmantevh)
        {
            List<DetManteVh> dets = new List<DetManteVh>();
            dets = dALVehiculo.ListarDetallesManteUno(idmantevh);
            return dets;
        }
        public List<DetManteVh> ListarDetallesManteIdvh(int idvh)
        {
           
            List<DetManteVh> detManteVhs = new List<DetManteVh>();
            detManteVhs = dALVehiculo.ListarDetallesManteIdvh(idvh);
            return detManteVhs;
        }

        public List<ConsumoAnios> ResumenManteAniovsAnio()
        {
            List<ConsumoAnios> lista_consumos = new List<ConsumoAnios>();
            lista_consumos = dALVehiculo.ResumenManteAniovsAnio();
            return lista_consumos;
        }

        public List<ACMVH_CategoriaAnio> ResumenMantevhCategoriasAnio(int anio)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            lista_costos = dALVehiculo.ResumenMantevhCategoriasAnio(anio);
            return lista_costos;
        }

        public List<ACMVH_CategoriaAnio> ResumenIncidenciasUnaCategoria(int anio, int idcategoria)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            lista_costos = dALVehiculo.ResumenIncidenciasUnaCategoria(anio,idcategoria);
            return lista_costos;
        }

        public List<ACMVH_CategoriaAnio> ResumenManteVhCategoriasAnioUnVh(int idvh, int anio)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            lista_costos = dALVehiculo.ResumenManteVhCategoriasAnioUnVh(idvh, anio);
            return lista_costos;
        }

        public List<ACCDetMesAnio> ResumenManteVhMesAnioUnVh(int idvh, int anio)
        {
            List<ACCDetMesAnio> resumen = new List<ACCDetMesAnio>();
            resumen = dALVehiculo.ResumenManteVhMesAnioUnVh(idvh, anio);  
            return resumen;
        }

        public List<ConsumoAnios> ResumenManteCostosAnualUnVh(int idvh)
        {
            List<ConsumoAnios> lista_costos = new List<ConsumoAnios>();
            lista_costos = dALVehiculo.ResumenManteCostosAnualUnVh(idvh);
            return lista_costos;

        }

        public ObservableCollection<ConsumoAnios> ResumenManteVhAnioMesvsMes(int anio)
        {
            ObservableCollection<ConsumoAnios> lista_consumos = new ObservableCollection<ConsumoAnios>();
            lista_consumos = dALVehiculo.ResumenManteVhAnioMesvsMes(anio);
            return lista_consumos;
        }

        public List<ACMVH_CategoriaAnio> ResumenManteVhCategoriaVhMesAnio(int anio, int mes)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            lista_costos = dALVehiculo.ResumenManteVhCategoriaVhMesAnio(anio, mes);
            return lista_costos;
        }
        public List<ACMVH_CategoriaAnio> CategoriasVhManteMesAnioUnVh(int idvh, int anio, int mes)
        {
            List<ACMVH_CategoriaAnio> lista_costos = new List<ACMVH_CategoriaAnio>();
            lista_costos = dALVehiculo.CategoriasVhManteMesAnioUnVh(idvh, anio, mes);
            return lista_costos;
        }

        public List<Vehiculo> DominiosMesAnioMantenimientos(int anio, int mes, int idcatevh)
        {
            List<Vehiculo> lista_vehiculos = new List<Vehiculo>();
            lista_vehiculos = dALVehiculo.DominiosMesAnioMantenimientos(anio, mes, idcatevh);
            return lista_vehiculos;

        }
        public void VehiculoBuscarFactura(string _numfactura)
        { }

        public void VehiculoListarMantenimientos(int _idvehiculo)
        { }
        public void VehiculosListarMantenimientosCategoria(int _idvehiculo)
        { }

        #endregion

        #region[documentacion]

        public int VehiculoAgregarNuevaDocumentacion(VehiculoDocu vehiculoDocu)
        {
            int fila = 0;

            fila = dALVehiculo.VehiculoAgregarNuevaDocumentacion(vehiculoDocu);
            return fila;
        }
        public int VehiculoActualizarDocumentacion(VehiculoDocu vehiculoDocu)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoActualizarDocumentacion(vehiculoDocu);

            return fila;
        }

        public ObservableCollection<VehiculoDocu> VehiculoListarDocumentacion( int idvh)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            lista_doc_vehiculos = dALVehiculo.VehiculoListarDocumentacion(idvh);
            return lista_doc_vehiculos;
        }

        public List<Docu_vh> VehiculoListarTipoDocu()
        {
            List<Docu_vh> listaDocu = new List<Docu_vh>();
            listaDocu = dALVehiculo.VehiculoListarTipoDocu();
            return listaDocu;

        }

        public ObservableCollection<VehiculoDocu> VehiculoListarTipoDocumentacion(int _idvh, int _idtipodocu)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            lista_doc_vehiculos = dALVehiculo.VehiculoListarTipoDocumentacion(_idvh, _idtipodocu);
            return lista_doc_vehiculos;
        }


        public int VehiculoBorrarUnaDocumentacion(int _iddocuvh)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoBorrarUnaDocumentacion(_iddocuvh);
            return fila;
        }

        public int VehiculoCumplirUnRegistro(int _idvhdoc)
        {
            int fila = 0;
            fila = dALVehiculo.VehiculoCumplirUnRegistro(_idvhdoc);
            return fila;
        }

        public ObservableCollection<VehiculoDocu> VehiculoListarDocVigente(int _idvh)
        {
            ObservableCollection<VehiculoDocu> lista_doc_vehiculos = new ObservableCollection<VehiculoDocu>();
            lista_doc_vehiculos = dALVehiculo.VehiculoListarDocVigente(_idvh);
            return lista_doc_vehiculos;
        }

        public void VehiculoAltaTipoDocumentacion(Docu_vh docu_Vh)
        {
            dALVehiculo.VehiculoAltaTipoDocumentacion(docu_Vh);
        }

        public void VehiculoModiTipoDocumentacion(Docu_vh docu_Vh)
        {
            dALVehiculo.VehiculoModiTipoDocumentacion(docu_Vh);
        }

        public List<VehiculoDocu> VehiculoListarDocuVencida()
        {
            // lista todos los vehiculos que tienen 1 o mas documentacion vencida
            List<VehiculoDocu> lista_doc_vehiculos = dALVehiculo.VehiculoListarDocuVencida();
            return lista_doc_vehiculos;
        }


        public List<VehiculoDocu> VehiculosListarDocAVencer()
        {
            // lista todos los vehiculos que tienen 1 o mas documentacion vencida
            List<VehiculoDocu> lista_doc_vehiculos = dALVehiculo.VehiculosListarDocAVencer();
           
            return lista_doc_vehiculos;
        }

        public void VehiculoDocAltaNota(NotaDocuVh notaDocuVh)
        {
            dALVehiculo.VehiculoDocAltaNota(notaDocuVh);
        }

        //borrado de una nota sobre documentacion de un vehiculo
        public void VehiculoDocDelete(int idnota)
        {
            dALVehiculo.VehiculoDocDelete(idnota);
        }

        // todas la notas para una documentacion de vehiculo especifica

        public List<NotaDocuVh> VehiculoDocNotas(int idregistro, int idtiponota)
        {
            List<NotaDocuVh> lista = dALVehiculo.VehiculoDocNotas(idregistro, idtiponota);
            
            return lista;
        }
        public void VehiculoValidarFechasDocumentacion(DateTime fnueva, DateTime factual)
        { }
        public void VehiculoValidarVencimientoDocumentacion(Docu_vh docu_Vh)
        { }
        #endregion

        #region Fotos

        
        public int GuardarFoto(FotoVh fotoVh)
        {
            int fila = dALVehiculo.GuardarFoto(fotoVh);
            return fila;
        }

        public int BorrarFoto(int idfotovh)
        {
            int fila = 0;
            fila = dALVehiculo.BorrarFoto(idfotovh);

            return fila;
        }

        public ObservableCollection<FotoVh> VehiculoListaFotos(int _idvh)
        {
            ObservableCollection<FotoVh> fotoVhs = new ObservableCollection<FotoVh>();
            fotoVhs = dALVehiculo.VehiculoListaFotos(_idvh);
            return fotoVhs;
        }
        #endregion




        #region[balance]
        public void VehiculoBalance(int idvehiculo)
        { }


        #endregion

        #region RemisionObra
        public List<Vehiculo> ListaParaRemision()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            vehiculos = dALVehiculo.ListaParaRemision();
            return vehiculos;
        }
        #endregion

        #region Repuestos

        public void RepuestoAlta(int idvh, int idpro, int cant)
        {
            dALVehiculo.RepuestoAlta(idvh, idpro, cant);
        }

        public void RepuestoBaja(int idrepuesto)
        {
            dALVehiculo.RepuestoBaja(idrepuesto);
        }
        #endregion

        #region ISO900
        public void PlanInspeccionAlta(plan_inspeccion plan)
        {
            dALVehiculo.PlanInspeccionAlta(plan);
        }

        public void PlanInspeccionBaja(int _idplan)
        {
            dALVehiculo.PlanInspeccionBaja(_idplan);
        }

        public ObservableCollection<plan_inspeccion> PlanInspeccionUnVehiculo(int _idvh)
        {
            ObservableCollection<plan_inspeccion> inspeccions = new ObservableCollection<plan_inspeccion>();
            inspeccions = dALVehiculo.PlanInspeccionUnVehiculo(_idvh);
            return inspeccions;
        }

        public ObservableCollection<plan_inspeccion> PlanInspeccionTareasActivasUnVehiculo(int _idvh)
        {
            ObservableCollection<plan_inspeccion> inspeccions = new ObservableCollection<plan_inspeccion>();
            inspeccions = dALVehiculo.PlanInspeccionTareasActivasUnVehiculo(_idvh);
            return inspeccions;
        }

        public void PlanInspeccionCalcularGAP(int _idvh, decimal _consumo, int _idplan, DateTime _fecha)
        {
            dALVehiculo.PlanInspeccionCalcularGAP(_idvh, _consumo, _idplan,_fecha);
        }

        public void PlansInspeccionCalcularAlarma(int _idvh)
        {
            dALVehiculo.PlansInspeccionCalcularAlarma(_idvh);
        }

        public void PlanInspeccionCalcularVencimiento(int _idvh)
        {
            dALVehiculo.PlanInspeccionCalcularVencimiento(_idvh);
        }

        public void PlanInspeccionCambiarImagenVencido(int _idvh, byte[] _imagen)
        {
            dALVehiculo.PlanInspeccionCambiarImagenVencido( _idvh,  _imagen);
        }

        public void PlanInspeccionCambiarImagenPorVencer(int _idvh,  byte[] _imagen)
        {
            dALVehiculo.PlanInspeccionCambiarImagenPorVencer( _idvh,   _imagen);
        }

        public int PlanInspeccionPorEstado(int _idestado)
        {
            int registrosafectados = dALVehiculo.PlanInspeccionPorEstado(_idestado);
          
            return registrosafectados;
        }
        public void PlanInspeccionAsignarOTM(int _idotm , int _idplan)
        {
            dALVehiculo.PlanInspeccionAsignarOTM(_idotm, _idplan);
        }

        public void PLanInspeccionCambiarEstadoDesdeOTM(int _idotm, int _valorestado)
        {
            dALVehiculo.PLanInspeccionCambiarEstadoDesdeOTM(_idotm, _valorestado);
        }

        #endregion


        #region Validaciones

       public bool ValidarBorrado( int idvh)
        {
            //int fila = 0;
            //variales que contienen los valores de los parametros de salidad del procedimientos
            int autorizaciones = 0;
            int asignaciones = 0;
            int mantenimientos = 0;
            int remitos = 0;
            int planinspeccion = 0;
            int documentos = 0;
            bool pase = false;
            DALConexion conexion = new DALConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Vehiculo_Validar_Borrado";
            //definimos los parametroe de entrada y los de salida
            cmd.Parameters.AddWithValue("@idvh", idvh);
            cmd.Parameters.Add("@autorizaciones", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@asignaciones", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@mantenimientos", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@remitos", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@planinspeccion", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@documentos", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                conexion.AbriConexion();
                //ejecutamos el comando
                cmd.ExecuteNonQuery();
                //tenemos que leer los parametros de salida que arroja el procedimientos
                autorizaciones = Convert.ToInt16(cmd.Parameters["@autorizaciones"].Value);
                asignaciones = Convert.ToInt16(cmd.Parameters["@asignaciones"].Value);
                mantenimientos = Convert.ToInt16(cmd.Parameters["@mantenimientos"].Value);
                remitos = Convert.ToInt16(cmd.Parameters["@remitos"].Value);
                planinspeccion = Convert.ToInt16(cmd.Parameters["@planinspeccion"].Value);
                documentos = Convert.ToInt16(cmd.Parameters["@documentos"].Value);
                conexion.CerrarConexion();
                //ahora evaluamos los valores para ver que valor se devuelve
                if (autorizaciones>0)
                {
                    pase = false;
                }
                else
                {
                    if (asignaciones>0)
                    {
                        pase = false;
                    }
                    else
                    {
                        if (mantenimientos>0)
                        {
                            pase = false;
                        }
                        else
                        {
                            if (remitos > 0)
                            {
                                pase = false;
                            }
                            else
                            {
                                if (planinspeccion>0)
                                {
                                    pase = false;
                                }
                                else
                                {
                                    if (documentos>0)
                                    {
                                        pase = false;
                                    }
                                    else
                                    {
                                        pase = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return pase;
        }

        #endregion
        public BLLVehiculos()
        { }


        public List<plan_inspeccion> PlanInspeccionEstadoTareas(int idestado)
        {
            List<plan_inspeccion> lista = dALVehiculo.PlanInspeccionEstadoTareas(idestado);
            return lista;
        }

        #region Costos

        public decimal CostoTotalComprasUnAnio(int _anio)
        {
            decimal _costo = dALVehiculo.CostoTotalComprasUnAnio(_anio);
            return _costo;
        }


        public decimal CostoTotalCombustibleUnAnio(int _anio)
        {
            decimal _costo = dALVehiculo.CostoTotalCombustibleUnAnio(_anio);
            return _costo;
        }

        public decimal CostoTotalMantenimientoUnAnio(int _anio)
        {
            decimal _costo = dALVehiculo.CostoTotalMantenimientoUnAnio(_anio);
           

            return _costo;
        }


        //costos consumos mensuales para un anio

        //costo del consumo del combustible mes a mes 
        public List<CostosGenericos> CostoCombustibleAnioMes(int _anio)
        {
            List<CostosGenericos> lista = dALVehiculo.CostoCombustibleAnioMes(_anio);
            return lista;
        }

        // costo del mantenimiento mes a mes anual
        public List<CostosGenericos> CostoMantenimientoAnioMes(int _anio)
        {
            List<CostosGenericos> lista = dALVehiculo.CostoMantenimientoAnioMes(_anio);
           
            return lista;
        }

        // costo de las compras mes a mes anual
        public List<CostosGenericos> CostoComprasAnioMes(int _anio)
        {
            List<CostosGenericos> lista = dALVehiculo.CostoComprasAnioMes(_anio);
          
            return lista;
        }

        // costo compras categorias de vehiculos
        public List<CostoCompraVehiculo> CostoComprasCategoriasAnio(int _anio)
        {
            List<CostoCompraVehiculo> lista = dALVehiculo.CostoComprasCategoriasAnio(_anio);
            return lista;
        }
        #endregion

    }
}
