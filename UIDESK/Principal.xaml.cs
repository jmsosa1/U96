using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UIDESK.ABM;
using UIDESK.Documentos;
using UIDESK.Remitos;
using UIDESK.uc.gestion.Notas;
using UIDESK.uc.Productos;
using UIDESK.uc.Laboratorio;
using MaterialDesignThemes.Wpf;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : MaterialWindow
    {
        // vinculacion con la capa de negocios
        BLLVehiculos bllVehiculos = new BLLVehiculos();
        BLLBase bllBase = new BLLBase();
        BLLGestion gestion = new BLLGestion();
        BLLForo coreforo = new BLLForo();
        BLLProducto coreProducto = new BLLProducto();
        BLLLaboratorio coreLab = new BLLLaboratorio();
        List<StockProducto> lista_productos = new List<StockProducto>();
        ObservableCollection<Producto> lista_instrumentos = new ObservableCollection<Producto>();
        ObservableCollection<Producto> instrumentos_por_vencer = new ObservableCollection<Producto>();
        //variables de la clase
        DateTime _ultimaFechaCAS; // almacena la ultima fecha desde la cual se calcularon las asignaciones de vehiculos
        int cantTareasSeguimiento; // almacena la cantidad de tareas que tiene asignado un usuario
        //Theme Code ========================>
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //===================================>



        public Principal() // constructor
        {
            InitializeComponent();

            _ultimaFechaCAS = bllBase.UltimaFechaCAS();
            string ultimaver = bllBase.UltimaVersion();
            txbVersion.Text = ultimaver;

            if (_ultimaFechaCAS != DateTime.Today.Date) // si las fechas son distintas
            {


                int numfilas = CalcularDiasAsignacionVehiculos(); // calculamos las asignaciones metodo privado
                int filafectada = bllBase.ActualizarFechaCAS(DateTime.Today.Date); // actualizamos la fecha CAS
                bdgNotificacion.Badge = numfilas; // notificamos
                bllBase.TareasCalcularDiasEjecucion(_ultimaFechaCAS); //calculo de dias de ejecucion de una tarea
                //modificacion del estado temporal de una tarea: despues de dos dias de diferencia , la tarea ya no es nueva
                gestion.TareaModicarEstadoTemporal(_ultimaFechaCAS);
                // idem anterior pero para las solicitudes
                gestion.SolicitudesModificarEstadoTemporal(_ultimaFechaCAS);
                //idem anterior pero para las ordenes de trabajo de mantenimiento
                gestion.OTMModificarEstadoTemporal(_ultimaFechaCAS);
                //revision de los vencimientos de la documentacion
                ActualizarEstadoDocumentacionVehiculo();
                //actualizamos los vencimientos de las fechas de las tareas delas maquinas;
                bllBase.RecalcularGapVencimientosDias(_ultimaFechaCAS);
                //revisamos es estado de las notas 
                coreforo.ActualizarSituacionNota(DateTime.Today.Date);
                //aca deberia ir la actualizacion de la situacion de los vencimientos de las calibraciones de los instrumentos
                coreLab.CalibracionesActualizarVencimientos();
                coreLab.CalibracionesActualizarEstadoInstrumentos();
            }

            //contamos las tareas que tiene pendiente el usuario que logeo
            int iduser = Contexto.CodUser;
            cantTareasSeguimiento = bllBase.CantidadTareasUsuario(iduser);
            bdgTareas.Badge = cantTareasSeguimiento;

            MostrarVehiculosDocVencida();
            MostrarVehiculosDocPorVencer();
            MostrarPlanInspeccionPorVencer();
            MostrarPlanInspeccionVencido();
            MostrarProductosEnPuntoPedido();
            MostrarInstrumentosVencidos();
            MostrarInstrumentosPorVencer();
            MostrarMaquinasConTareasVencidas();
        
            //bdgInstVencidos.Badge = 0;
            //bdgInstDocPV.Badge = 0;
        }

        private void MostrarMaquinasConTareasVencidas()
        {
            throw new NotImplementedException();
        }




        #region Vehiculos


        private void MostrarPlanInspeccionVencido()
        {
            int planesvencidos = bllVehiculos.PlanInspeccionPorEstado(3);
            bdgVHPlanVencido.Badge = planesvencidos;
        }

        private void MostrarPlanInspeccionPorVencer()
        {
            int planesAVencer = bllVehiculos.PlanInspeccionPorEstado(2);
            bdgVhPlanAVencer.Badge = planesAVencer;
        }

        private void MostrarVehiculosDocPorVencer()
        {
            //metodo que sirve para mostrar en un badge la cantidad de vehiculos con documentacion a vencer dentro de 15 dias
            List<VehiculoDocu> vh_doc_por_vencer = bllVehiculos.VehiculosListarDocAVencer();
            bdgVhDocPV.Badge = vh_doc_por_vencer.Count;
        }

        private void MostrarVehiculosDocVencida()
        {

            List<VehiculoDocu> vh_doc_v = bllBase.ChekeoDocumentacionVehiculos();
            bdgVhDocV.Badge = vh_doc_v.Count;
        }

        private void ActualizarEstadoDocumentacionVehiculo()
        {
            DateTime factual = DateTime.Today.Date; // fecha actual del sistema.
            //este metodo revisa los vencimientos de las distintas documentaciones de los vehiculos
            //y cambia el estado del registro a valor 2 , si está vencido.
            bllBase.ActualizaEstadoDocuVehiculos(factual);

        }

        private int CalcularDiasAsignacionVehiculos()
        {
            int filas;
            DateTime _fechaHoy = DateTime.Today.Date;
            filas = bllBase.CalcularDiasAsignacionVehiculo(_fechaHoy);
            return filas;
        }

        private void btnDocumentos_Click(object sender, RoutedEventArgs e)
        {
            //codigo de activacion de la aplicacion resultados
            //PrincipalResultados principalResultados = new PrincipalResultados();
            //principalResultados.Show();
            ResultadosVehiculos resultadosVehiculos = new ResultadosVehiculos();
            resultadosVehiculos.Show();
        }

        private void btnVehiculos_Click(object sender, RoutedEventArgs e)
        {
            //codigo de activacion de la aplicacion vehiculos
            PrincipalVehiculos principalVehiculos = new PrincipalVehiculos();
            principalVehiculos.Show();
        }

        private void BtnvhDocV_Click(object sender, RoutedEventArgs e)
        {
            //ImprimirDocVencidaVh imprimirDoc = new ImprimirDocVencidaVh();
            //imprimirDoc.Show();
            List<VehiculoDocu> doc_vencida = bllVehiculos.VehiculoListarDocuVencida();
            VhDocVencida vhDocVencida = new VhDocVencida(doc_vencida, "Vencidos");
            vhDocVencida.Show();
        }

        private void BtnvhDocPV_Click(object sender, RoutedEventArgs e)
        {
            //ImprimirDocPorVencerVH porVencerVH = new ImprimirDocPorVencerVH();
            //porVencerVH.Show();

            List<VehiculoDocu> doc_vencida = bllVehiculos.VehiculosListarDocAVencer();
            VhDocVencida vhDocVencida = new VhDocVencida(doc_vencida, "Proximos a Vencer");
            vhDocVencida.Show();
        }

        private void BtnPlanVencer_Click(object sender, RoutedEventArgs e)
        {
            //ImprimirPlanInspeccion imprimirPlan = new ImprimirPlanInspeccion(2);
            //imprimirPlan.Show();
            List<plan_inspeccion> tareas_vencen = bllVehiculos.PlanInspeccionEstadoTareas(2);
            PITareas pITareas = new PITareas(tareas_vencen, "Proximos Vencimientos");
            pITareas.Show();
        }

        private void BtnPlanVencido_Click(object sender, RoutedEventArgs e)
        {
            //ImprimirPlanInspeccion imprimirPlan = new ImprimirPlanInspeccion(3);
            //imprimirPlan.Show();
            List<plan_inspeccion> tareas_vencidas = bllVehiculos.PlanInspeccionEstadoTareas(3);
            PITareas pITareas = new PITareas(tareas_vencidas, "Vencidos");
            pITareas.Show();

        }

        private void BtnVhPPV_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlanMante_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Obras
        private void btnObras_Click(object sender, RoutedEventArgs e)
        {
            //codigo de activacion de la aplicacion obras
            PrincipalObras principalObras = new PrincipalObras();
            principalObras.Show();
        }

        private void btnAddObra_Click(object sender, RoutedEventArgs e)
        {
            Obra _obra = new Obra();
            ABMObra _abmObra = new ABMObra(_obra);
            if (_abmObra.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego una nueva obra al sistema", "Aviso", MessageBoxButton.OK);

            }
        }

        private void btnNuevoDSO_Click(object sender, RoutedEventArgs e)
        {
            DSO nuevoRemito = new DSO();
            nuevoRemito._operacion = "Egreso";
            nuevoRemito.txtConcepto.Text = "Entrega";
            nuevoRemito.txtTipoDocu.Text = "DSO";

            if (nuevoRemito.ShowDialog() == true)
            {
                MessageBox.Show("Se registro la entrega", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnNuevoDDO_Click(object sender, RoutedEventArgs e)
        {
            DDO nuevoRemito = new DDO();
            nuevoRemito._operacion = "Devolucion";
            nuevoRemito.txtConcepto.Text = "Devolucion";
            nuevoRemito.txtTipoDocu.Text = "DDO";

            if (nuevoRemito.ShowDialog() == true)
            {
                MessageBox.Show("Se registro la devolucion", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnNuevoMov_Click(object sender, RoutedEventArgs e)
        {
            DMO nuevoDMo = new DMO();
            nuevoDMo.Show();
        }


        #endregion

        #region Empleados

        private void btnAddEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            ABMEmpleado nuevoempleado = new ABMEmpleado(empleado);
            //nuevoempleado.txbOperacion.Text = "Alta de  Empleado";
            nuevoempleado.btnOperacion.Content = "Guardar";
            nuevoempleado._operacion = "A";
            if (nuevoempleado.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el empleado", "Aviso", MessageBoxButton.OK);

            }
        }

        private void btnTrabajadores_Click(object sender, RoutedEventArgs e)
        {
            //activamos la aplicacion trabajadores
            PrincipalEmpleados principalEmpleados = new PrincipalEmpleados();
            principalEmpleados.Show();
        }

        private void btnDDI_Click(object sender, RoutedEventArgs e)
        {
            DDI nuevoremito = new DDI();
            nuevoremito._operacion = "Devolucion";
            nuevoremito.txtConcepto.Text = "Devolucion";
            nuevoremito.txtTipoDocu.Text = "DDI";
            if (nuevoremito.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnDSI_Click(object sender, RoutedEventArgs e)
        {
            DSI nuevoremito = new DSI();
            nuevoremito._operacion = "Entrega";
            nuevoremito.txtConcepto.Text = "Entrega";
            nuevoremito.txtTipoDocu.Text = "DSI";
            if (nuevoremito.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        #endregion

        #region Productos

        private void MostrarProductosEnPuntoPedido()
        {
            lista_productos = coreProducto.ListarProductosEnPP(1);
            bdgProductosPP.Badge = lista_productos.Count;
        }

        private void MostrarInstrumentosVencidos()
        {
            lista_instrumentos = coreLab.ListarInstrumentosVencidos();
            bdgInstVencidos.Badge = lista_instrumentos.Count;
        }

        private void MostrarInstrumentosPorVencer()
        {
            instrumentos_por_vencer = coreLab.ListarInstrumentosPorVencer();
            bdgInstDocPV.Badge = instrumentos_por_vencer.Count;
        }


        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            //activamos la aplicacion productos
            PrincipalProductos principalProductos = new PrincipalProductos();
            principalProductos.Show();
        }

        private void btnAbastecimientos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResultadoHerramientas_Click(object sender, RoutedEventArgs e)
        {
            //PpalResultadoHyM ppalResultadoHyM = new PpalResultadoHyM();
            //ppalResultadoHyM.Show();
            TableroControlCostos tableroControlCostos = new TableroControlCostos();
            tableroControlCostos.Show();
        }

        private void btnAdmHerramientas_Click(object sender, RoutedEventArgs e)
        {
            PrincipalAdmHyM principalAdmHyM = new PrincipalAdmHyM();
            principalAdmHyM.Show();
        }

        private void btnAddProducto_Click(object sender, RoutedEventArgs e)
        {
            ABMProducto nuevoProducto = new ABMProducto(0);
            if (nuevoProducto.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el producto", "Aviso", MessageBoxButton.OK);

            }
        }

        private void btnInstVencidos_Click(object sender, RoutedEventArgs e)
        {
            InstrumentosVencidos instrumentos = new InstrumentosVencidos(lista_instrumentos);
            instrumentos.Show();
        }

        private void btnIntsDocPV_Click(object sender, RoutedEventArgs e)
        {
            InstrumentosPorVencer vencer = new InstrumentosPorVencer(instrumentos_por_vencer);
            vencer.Show();

        }

        private void btnListPP_Click(object sender, RoutedEventArgs e)
        {
            ListaProductosPP _verlista = new ListaProductosPP(lista_productos);
            _verlista.Show();
        }

        #endregion

        #region Gestion

        private void btnTareasSector_Click(object sender, RoutedEventArgs e)
        {
            //muestra la carpeta de tareas del sector de forma general
            Contexto.CodP = 0; // seteamo a cero cuando queremos ver todas las operacion
            CarpetaTareasSector carpeta = new CarpetaTareasSector();

            carpeta.Show();
        }

        private void BtnTareasNotificacion_Click(object sender, RoutedEventArgs e)
        {
            //muestra la carpeta de tareas del sector solo con las tareas que tiene seguimiento el usuario
            //solo si la cantidad de tareas es mayor que cero

            if (cantTareasSeguimiento > 0)
            {


                Contexto.CodP = 1; // seteamos a 1 cuando solo queremos ver las tareas que sigue el usuario actual logeado
                CarpetaTareasSector carpeta = new CarpetaTareasSector();
                carpeta.Show();
            }
            else
            {
                MessageBox.Show("El usuario no tiene actividades pendientes", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnAddNotaSahm_Click(object sender, RoutedEventArgs e)
        {
            Crear _crearNota = new Crear();
            if (_crearNota.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la nota", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }




        #endregion

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void btnAcercaDe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            //Theme Code ========================>
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
            //===================================>
        }

        private void btnMaqVencidos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
