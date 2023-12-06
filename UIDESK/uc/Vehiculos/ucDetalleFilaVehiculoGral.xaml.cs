using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.imprimir;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaVehiculoGral.xaml
    /// </summary>
    public partial class ucDetalleFilaVehiculoGral : UserControl
    {
        BLLVehiculos bllVh = new BLLVehiculos(); // operaciones de datos de vehiculos
        BLLBase bllBase = new BLLBase(); // operaciones de datos basicos de la aplicacion
        BLLProducto bllProducto = new BLLProducto(); // operaciones de datos con productos del tipo repuesto
        BLLGestion coregestion = new BLLGestion(); // operaciones de datos de gestion de OTM

        ObservableCollection<ConsumoCombustible> consumoCombustibles = new ObservableCollection<ConsumoCombustible>();//lista de consumos de comb
        ObservableCollection<Autorizacion_vh> autorizacion_Vhs = new ObservableCollection<Autorizacion_vh>();//lista de autorizaciones del vehiculo
        ObservableCollection<VehiculoDocu> vehiculoDocus = new ObservableCollection<VehiculoDocu>();//lista de documentos del vehiculo
        List<Docu_vh> docu_Vhs = new List<Docu_vh>(); // lista de tipos de documentacion
        ObservableCollection<FotoVh> lista_fotos = new ObservableCollection<FotoVh>(); //coleccion de fotos
        List<DetManteVh> detManteVhs = new List<DetManteVh>();
        List<CategoriaManteVh> categoriaManteVhs = new List<CategoriaManteVh>();
        ObservableCollection<RepuestoVh> repuestoVhs = new ObservableCollection<RepuestoVh>(); // lista de repuestos
        ObservableCollection<plan_inspeccion> plan_inspeccion = new ObservableCollection<plan_inspeccion>(); // lista de planes de inspeccion

        Vehiculo _vehiculo = new Vehiculo();
        public static int _idvh; // variable que contiene el id del vehiculo seleccionado 
        //otras variables usadas para calculos 
        decimal _totalkm, _totalhs, _totalLitros, _costototalkm, _costototalhs, _totalcostocombustibles;
        decimal _costodocactiva, _costoTotalDocumentacionVh, _costoTotalMante;

        //vistas
        public ICollectionView vistafotos
        {
            get { return CollectionViewSource.GetDefaultView(lista_fotos); }
        }

        public ICollectionView vistaCateMante
        {
            get { return CollectionViewSource.GetDefaultView(detManteVhs); }
        }

        public ICollectionView vistaDocu
        {
            get { return CollectionViewSource.GetDefaultView(vehiculoDocus); }
        }



        //constructor de clase
        public ucDetalleFilaVehiculoGral()
        {
            InitializeComponent();
            //datagrid consumos combustibles
            _vehiculo = bllVh.BuscarPorId(_idvh);
            grDetalles.DataContext = _vehiculo;
            stkDatosVehiculo.DataContext = _vehiculo;
            consumoCombustibles = bllVh.ListarConsumosUnVehiculo(_idvh);
            dgConsumosCombustible.DataContext = consumoCombustibles;
            dgConsumosCombustible.ItemsSource = consumoCombustibles;
            RefrescarConsumos();

            //datagrid autorizaciones
            autorizacion_Vhs = bllVh.ListarAutorizacionesActivasUno(_idvh);
            dgAutorizaciones.DataContext = autorizacion_Vhs;
            dgAutorizaciones.ItemsSource = autorizacion_Vhs;
            txbCantiAutorizaciones.Text = autorizacion_Vhs.Count.ToString();

            //datagrid detalle de mantenimientos
            detManteVhs = bllVh.ListarDetallesManteIdvh(_idvh); // listamos el detalle para el vehiculo
            dgDetMante.ItemsSource = detManteVhs;
            dgDetMante.DataContext = detManteVhs;
            RefrescarCostosMantenimiento();

            //datagrid documentacion
            vistaDocu.Filter = new Predicate<object>(filtroDocuVencida);
            vistaDocu.Filter = new Predicate<object>(filtroDocuVigente);
            RefrescarDocumentacion();
            cmbDocu.IsEnabled = false;


            //zona de carga de las fotos
            lista_fotos = bllVh.VehiculoListaFotos(_idvh);
            grdGaleria.DataContext = vistafotos;

            // datagrid repuestos del vehiculo
            repuestoVhs = bllVh.RepuestosVhUnVehiculo(_idvh);
            dgRepuestos.ItemsSource = repuestoVhs;
            dgRepuestos.DataContext = repuestoVhs;

            //datagrid plan de inspeccion del vehiculo
            plan_inspeccion = bllVh.PlanInspeccionUnVehiculo(_idvh);
            dgPlanInspeccion.ItemsSource = plan_inspeccion;
            dgPlanInspeccion.DataContext = plan_inspeccion;

        }

        #region[documentacion]


        private bool filtroDocuVigente(object obj)
        {
            VehiculoDocu docuVigente = obj as VehiculoDocu;
            return docuVigente.EstadoReg == 1;
        }

        private bool filtroDocuVencida(object obj)
        {
            VehiculoDocu docuVencida = obj as VehiculoDocu;
            return docuVencida.EstadoReg == 2;
        }

        private void BtnDocuGral_Click(object sender, RoutedEventArgs e)
        {
            RefrescarDocumentacion();
        }

        private void BtnDocuVigente_Click(object sender, RoutedEventArgs e)
        {
            vistaDocu.Filter = filtroDocuVigente;

            //txbCantDocu.Text = vehiculoDocus.Count.ToString();
            // _costodocactiva = CalcularCostoDocActiva();
            // _costoTotalDocumentacionVh = CalcularCostoTotalDoc();
            // txbCostoDocuActiva.Text = string.Format("{0:C}", _costodocactiva);
            // txbCostoTotalDoc.Text = string.Format("{0:C}", _costoTotalDocumentacionVh);
        }

        private void BtnCumplirDocu_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            // seleccionamos el registro a eliminar
            VehiculoDocu vhd = new VehiculoDocu();
            vhd = dgDocumentacion.SelectedItem as VehiculoDocu;
            MessageBoxResult result = MessageBox.Show("Cumplir el registro?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // procedimiento similar al de borrado de un registro
                fila = bllVh.VehiculoCumplirUnRegistro(vhd.IdVhDoc);
                RefrescarDocumentacion();
            }
        }


        private void CmbDocu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Docu_vh d = new Docu_vh();
            d = cmbDocu.SelectedItem as Docu_vh;
            vehiculoDocus = bllVh.VehiculoListarTipoDocumentacion(_idvh, d.IdDocuVH);
            dgDocumentacion.DataContext = vehiculoDocus;
            dgDocumentacion.ItemsSource = vehiculoDocus;
            txbCantDocu.Text = vehiculoDocus.Count.ToString();
            _costodocactiva = CalcularCostoDocActiva();
            txbCostoDocuActiva.Text = string.Format("{0:C}", _costodocactiva);

            _costoTotalDocumentacionVh = CalcularCostoTotalDoc();

            txbCostoTotalDoc.Text = string.Format("{0:C}", _costoTotalDocumentacionVh);
        }

        private void ChkVerDocu_Checked(object sender, RoutedEventArgs e)
        {
            cmbDocu.IsEnabled = true;
            cmbDocu.ItemsSource = bllVh.VehiculoListarTipoDocu();

        }

        private void ChkVerDocu_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbDocu.IsEnabled = false;
            RefrescarDocumentacion();
        }

        private void BtnEliminarDocu_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            // seleccionamos el registro a eliminar
            VehiculoDocu vhd = dgDocumentacion.SelectedItem as VehiculoDocu;
            MessageBoxResult message = System.Windows.MessageBox.Show("Desea Eliminar el registro?", "Aviso", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                //borramos el registro y actualizamos el grid
                fila = bllVh.VehiculoBorrarUnaDocumentacion(vhd.IdVhDoc);
                RefrescarDocumentacion();
            }
        }

        private void BtnVencida_Click(object sender, RoutedEventArgs e)
        {
            // codigo para mostrar el listado de documentacion vencida
            vistaDocu.Filter = filtroDocuVencida;

        }

        private void BtnImprimirDocu_Click(object sender, RoutedEventArgs e)
        {

        }

        private decimal CalcularCostoDocActiva()
        {
            decimal _costo = 0;
            foreach (var item in vehiculoDocus)
            {
                if (item.EstadoReg == 1) // doc activa
                {
                    _costo = _costo + item.Costo;
                }
            }
            return _costo;
        }

        private decimal CalcularCostoTotalDoc()
        {
            decimal _costoTotal = 0;
            foreach (var item in vehiculoDocus)
            {
                _costoTotal = _costoTotal + item.Costo;
            }
            return _costoTotal;
        }

        private void RefrescarDocumentacion()
        {
            vehiculoDocus = bllVh.VehiculoListarDocumentacion(_idvh);
            dgDocumentacion.DataContext = vehiculoDocus;
            dgDocumentacion.ItemsSource = vehiculoDocus;
            txbCantDocu.Text = vehiculoDocus.Count.ToString();
            _costodocactiva = CalcularCostoDocActiva();
            _costoTotalDocumentacionVh = CalcularCostoTotalDoc();
            txbCostoDocuActiva.Text = string.Format("{0:C}", _costodocactiva);
            txbCostoTotalDoc.Text = string.Format("{0:C}", _costoTotalDocumentacionVh);
        }

        #endregion

        #region[fotos]

        private void BtnAddFoto_Click(object sender, RoutedEventArgs e)
        {

            ABMFoto nuevaFoto = new ABMFoto();
            nuevaFoto._idvehiculo = _idvh;
            if (nuevaFoto.ShowDialog() == true)
            {
                MessageBox.Show("Se guardo la foto en del vehiculo", "Aviso", MessageBoxButton.OK);
                lista_fotos = bllVh.VehiculoListaFotos(_idvh);
                grdGaleria.DataContext = vistafotos;
            }
        }

        private void BtnDelFoto_Click(object sender, RoutedEventArgs e)
        {
            FotoVh foto = new FotoVh();
            foto = vistafotos.CurrentItem as FotoVh;
            bllVh.BorrarFoto(foto.IdFotoVh);
            lista_fotos = bllVh.VehiculoListaFotos(_idvh);
            grdGaleria.DataContext = vistafotos;
        }



        private void BtnPrevFoto_Click(object sender, RoutedEventArgs e)
        { //navegar hacia Atras
            vistafotos.MoveCurrentToPrevious();
            if (vistafotos.IsCurrentBeforeFirst)
            {
                vistafotos.MoveCurrentToLast();
            }
        }

        private void BtnNextFoto_Click(object sender, RoutedEventArgs e)
        {//navegar hacia atras
            vistafotos.MoveCurrentToNext();
            if (vistafotos.IsCurrentAfterLast)
            {
                vistafotos.MoveCurrentToFirst();
            }
        }

        #endregion


        #region[mantenimientos]

        //agrupar mantenimientos por categoria de mantenimiemtos
        private void BtnAgruparCateMante_Click(object sender, RoutedEventArgs e)
        {
            if (vistaCateMante != null && vistaCateMante.CanGroup == true)
            {
                vistaCateMante.GroupDescriptions.Clear();
                vistaCateMante.GroupDescriptions.Add(new PropertyGroupDescription("NomCateMante"));//"nomcatemante es el nombre del campo por el cual se agrupo

                btnDesCatMante.IsEnabled = true;
                btnAgruparCateMante.IsEnabled = false;
            }

        }

        //desagrupar los mantenimientos
        private void BtnDesCatMante_Click(object sender, RoutedEventArgs e)
        {
            if (vistaCateMante != null)
            {
                vistaCateMante.GroupDescriptions.Clear();
            }
            btnDesCatMante.IsEnabled = false;
            btnAgruparCateMante.IsEnabled = true;
        }

        //calculo de los costos de mantenimiento del vehiculo
        private decimal CostoTotalMantenimientos()
        {
            decimal _costoTotalMantemientos = 0;

            foreach (var item in detManteVhs)
            {
                _costoTotalMantemientos = _costoTotalMantemientos + item.CostoItem;
            }
            return _costoTotalMantemientos;
        }
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //titulo del resumen en excel
            ws.Range["A1"].Value = "Planilla de Mantenimientos del Vehiculo ";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A2"].Value = "Dominio :";
            ws.Range["B2"].Value = _vehiculo.Dominio;
            ws.Range["A3"].Value = "Descripcion:";
            ws.Range["B3"].Value = _vehiculo.Descripcion;
            ws.Range["A2", "B2"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A3", "B3"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A4"].Value = "Categoria";
            ws.Range["B4"].Value = "Descripcion";
            ws.Range["C4"].Value = "Fecha Fac";
            ws.Range["D4"].Value = "Cantidad";
            ws.Range["E4"].Value = "Costo";
            ws.Range["F4"].Value = "Proveedor";
            ws.Range["G4"].Value = "Km indicados";
            ws.Range["H4"].Value = "Hs indicadas";
            ws.Range["A4", "H4"].Font.Bold = true;
            // pasamos los valores para este rango iterando la lista de totales
            int j = 5; //  indicador de posicion de celda
            foreach (var item in detManteVhs)
            {
                ws.Range["A" + j].Value = item.NomCateMante;
                ws.Range["B" + j].Value = item.DescriMante;
                ws.Range["C" + j].Value = item.FechaFac;
                ws.Range["D" + j].Value = item.Cantidad;
                ws.Range["E" + j].Value = item.CostoItem;
                ws.Range["E" + j].NumberFormat = "$0,00";
                ws.Range["F" + j].Value = item.RazonSocial;
                ws.Range["G" + j].Value = item.KmDetMante;
                ws.Range["H" + j].Value = item.HsDetMante;
            

                j++; // aumentamos una posicion el indice de celda
            }
           
        }






        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefrescarCostosMantenimiento()
        {
            _costoTotalMante = CostoTotalMantenimientos();
            txbCostoTotalMante.Text = string.Format("{0:C}", _costoTotalMante);
            txbCantMantenimientos.Text = detManteVhs.Count.ToString();
        }



        #endregion

        #region[repuestos]

        private void BtnAddRepuesto_Click(object sender, RoutedEventArgs e)
        {

            DescriRepuesto agregarRepuesto = new DescriRepuesto(_idvh);
            if (agregarRepuesto.ShowDialog() == true)
            {
                MessageBox.Show("Se actualizo la lista de repuestos", "Aviso", MessageBoxButton.OK);
                repuestoVhs = bllVh.RepuestosVhUnVehiculo(_idvh);
                dgRepuestos.ItemsSource = repuestoVhs;
                dgRepuestos.DataContext = repuestoVhs;
            };
        }

        private void btnAgregarTipoDocu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelAutorizacion_Click(object sender, RoutedEventArgs e)
        {
            Autorizacion_vh autorizacion_ = dgAutorizaciones.SelectedItem as Autorizacion_vh;
            //aca se borra un item de la observable collection 
            MessageBoxResult result = MessageBox.Show("Desea borrar el item?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bllVh.BorrarAutorizacion(autorizacion_.IdAut);
                autorizacion_Vhs = bllVh.ListarAutorizacionesActivasUno(_idvh);
                dgAutorizaciones.DataContext = autorizacion_Vhs;
                dgAutorizaciones.ItemsSource = autorizacion_Vhs;
                txbCantiAutorizaciones.Text = autorizacion_Vhs.Count.ToString();
            }
        }

        private void BtnDelRepuesto_Click(object sender, RoutedEventArgs e)
        {
            RepuestoVh repuesto = dgRepuestos.SelectedItem as RepuestoVh;
            //aca se borra un item de la observable collection 
            MessageBoxResult result = MessageBox.Show("Desea borrar el item?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bllVh.RepuestoBaja(repuesto.IdRepuestoVh);
                repuestoVhs = bllVh.RepuestosVhUnVehiculo(_idvh);
                dgRepuestos.ItemsSource = repuestoVhs;
                dgRepuestos.DataContext = repuestoVhs;
            }
        }

        private void BtnImprimirRepuestos_Click(object sender, RoutedEventArgs e)
        {
            ImpListRepVh listRepVh = new ImpListRepVh(_idvh);
            listRepVh.Show();
        }


        #endregion

        #region[planinspeccion]
        private void BtnAddTareaPLan_Click(object sender, RoutedEventArgs e) //agregr una tarea al plan de inspeecion del vehiculo
        {
            Vehiculo vehiculo = bllVh.BuscarPorId(_idvh);
            ABMPlanInspeccion nuevo_plan = new ABMPlanInspeccion(vehiculo);
            if (nuevo_plan.ShowDialog() == true)
            {
                plan_inspeccion = bllVh.PlanInspeccionUnVehiculo(_idvh);
                dgPlanInspeccion.ItemsSource = plan_inspeccion;
                dgPlanInspeccion.DataContext = plan_inspeccion;
            }
        }

        private void BtnDeleteTareaPLan_Click(object sender, RoutedEventArgs e) //borrar la tarea seleccionada
        {
            //aca tomamos como parametro el id del plan
            // y lo damos de baja 
            MessageBoxResult result = MessageBox.Show("Desea dar de baja la tarea?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                plan_inspeccion plan_ = dgPlanInspeccion.SelectedItem as plan_inspeccion;
                bllVh.PlanInspeccionBaja(plan_.Idplan);
                plan_inspeccion = bllVh.PlanInspeccionUnVehiculo(_idvh);
                dgPlanInspeccion.ItemsSource = plan_inspeccion;
                dgPlanInspeccion.DataContext = plan_inspeccion;
            }

        }

        //crear a partir de la tarea seleccionada, una OTM
        private void BtnCrearTareaMante_Click(object sender, RoutedEventArgs e)
        {
            plan_inspeccion pl = dgPlanInspeccion.SelectedItem as plan_inspeccion;
            if (pl.IdOtm > 0)
            {
                MessageBox.Show("La Tarea selecionada ya tiene una OTM asignada", "Aviso Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Desea Crear una OTM para esta tarea?", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    Otm nuevaOtm = new Otm(); //creamos un nuevo objeto OTM
                    OtmDetalle detalleOtm = new OtmDetalle(); //detalle de la OTM
                    Vehiculo v = new Vehiculo(); // creamos un objeto vehiculo para contener los datos del mismo
                    v = bllVh.BuscarPorId(_idvh); // buscamos los datos del vehiculo y lo asignamos a la variable objeto
                                                  //seteamos la nueva OTM
                    nuevaOtm.Idvh = v.IdVh;
                    nuevaOtm.MarcaVh = v.NomMarca;
                    nuevaOtm.ModeloVh = v.Modelo;
                    nuevaOtm.DescriVh = v.Descripcion;
                    nuevaOtm.Alta = DateTime.Today;
                    nuevaOtm.Dominio = v.Dominio;
                    nuevaOtm.Estado_Otm = "Pendiente";
                    nuevaOtm.LecturaHs = Convert.ToInt32(v.HorasAcumuladas);
                    nuevaOtm.LecturaKm = Convert.ToInt32(v.KmAcumulado);
                    nuevaOtm.IdPlanInspeccion = pl.Idplan; // relacionamos la otm con el plan de inspeccion
                                                           //seteamos el detalle
                    detalleOtm.Idvh = pl.Idvh;
                    detalleOtm.DescripcionItem = pl.Tarea;
                    detalleOtm.NumItem = 1;
                    detalleOtm.IdCateMante = 1;
                    detalleOtm.NomCateMante = "Generico";
                    ABMOtm aBMOtm = new ABMOtm(nuevaOtm, detalleOtm);
                    if (aBMOtm.ShowDialog() == true)
                    {
                        MessageBox.Show("Se creo una nueva OTM para la tarea seleccionada", "Aviso", MessageBoxButton.OK);
                        //aca deberia ir la actualizacion del plan indicando que el mismo tiene una OTM asociada
                        Otm otm = coregestion.ObtenerUltimoIdOtm();
                        bllVh.PlanInspeccionAsignarOTM(otm.IdOtm, pl.Idplan);
                        dgPlanInspeccion.ItemsSource = plan_inspeccion;
                        dgPlanInspeccion.DataContext = plan_inspeccion;
                    }
                }
            }
        }


        #endregion



        #region[consumoscombustibles]

        private void btnBorrarConsumo_Click(object sender, RoutedEventArgs e)
        {
            ConsumoCombustible consumo = new ConsumoCombustible();
            Vehiculo vehiculo = new Vehiculo();
            vehiculo = bllVh.BuscarPorId(_idvh);
            // LogOperaciones log = new LogOperaciones();
            // al borrar un consumo , primero debemos preguntar si se quiere borrar .
            //luego , una vez borrado el consumo, se debe actualizar los consumos generales de vehiculo
            MessageBoxResult respuesta = new MessageBoxResult();
            respuesta = MessageBox.Show("Desea borrar el consumo seleccionado?", "Aviso", MessageBoxButton.OKCancel);
            if (respuesta == MessageBoxResult.OK)
            {
                //borrar el consumo de la tabla consumo_combustible
                //pa_Vehiculo_Borrar_Un_Consumo
                consumo = dgConsumosCombustible.SelectedItem as ConsumoCombustible;
                int fila = bllVh.BorrarUnConsumo(consumo.IdCmb);
                //actualizamos el kmacumulado de un vehiculo
                //pa_vehiculo_actualiza_kmacu_menos
                bllVh.VehiculoActualizaKmAcuMenos(_idvh, consumo.KmRecorrido, consumo.Imputacion);
                //actualizamos las horas acumuladas
                //pa_vehiculo_actualiza_hsacu_menos
                int _horas = Convert.ToInt32(consumo.HorasTrabajo);
                bllVh.VehiculoActualizaHsAcuMenos(_idvh, _horas, consumo.Imputacion);

                //actualizar los consumos generales
                consumoCombustibles = bllVh.ListarConsumosUnVehiculo(_idvh);
                dgConsumosCombustible.DataContext = consumoCombustibles;
                dgConsumosCombustible.ItemsSource = consumoCombustibles;
                RefrescarConsumos();
                // registramos la operacion del borrado
                //  log.IdUsuario = _iduser;
                //log.IdRegistro = consumo.IdCmb;
                //log.Fecha = DateTime.Today.Date;
                //log.Tabla = "consumos_combustibles";
                //log.CodigoOperacion = "DF";
                //fila = bllBase.UsuarioRegOp(log);
            }
            else
            {
                return;
            }
        }

        public void RefrescarConsumos()
        {
            _totalkm = bllVh.TotalKM_Consumo_Uno(_idvh);
            _totalhs = bllVh.TotalHs_Consumo_Uno(_idvh);
            _totalLitros = bllVh.TotalLitros_Consumo_Uno(_idvh);
            _costototalhs = bllVh.TotalCostoHs_Consumo_Uno(_idvh);
            _costototalkm = bllVh.TotalCostoKM_Consumo_Uno(_idvh);
            _totalcostocombustibles = _costototalhs + _costototalkm;

            txtTotalKM.Text = Convert.ToString(_totalkm);
            txtTotalHs.Text = Convert.ToString(_totalhs);
            txtTotalLitrosCombustible.Text = Convert.ToString(_totalLitros);
            CultureInfo ci = new CultureInfo("es-AR"); // creamos un objeto culture para dar formato al costo
            txtTotalCostoCombustible.Text = _totalcostocombustibles.ToString("C", ci); //aplicamos el formato al costo
        }
        #endregion

    }
}
