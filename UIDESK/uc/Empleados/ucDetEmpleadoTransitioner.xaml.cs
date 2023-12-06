using BLL;
using DAL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIDESK.imprimir;

namespace UIDESK.uc.Empleados
{
    /// <summary>
    /// Lógica de interacción para ucDetEmpleadoTransitioner.xaml
    /// </summary>
    public partial class ucDetEmpleadoTransitioner : UserControl
    {
        #region Declarativas


        BLLEmpleados coreEmpleado = new BLLEmpleados();
        BLLProducto coreProducto = new BLLProducto();
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Autorizacion_vh> autorizacion_Vhs = new ObservableCollection<Autorizacion_vh>();//lista de autorizaciones del vehiculo
        int _idempleado;
        int _idproducto;
        Empleado emp = new Empleado();
        public decimal CostoTotalBalance { get; set; }
        public int CantidadItems { get; set; }
        public decimal CostoBalanceFaltante { get; set; }
        public int CantidadItemsFaltantes { get; set; }
        ObservableCollection<BalanceEmpleado> balanceEmpleado = new ObservableCollection<BalanceEmpleado>();
        ObservableCollection<TipoProducto> tiposProducto = new ObservableCollection<TipoProducto>();
        ObservableCollection<CategoriaP> cateProducto = new ObservableCollection<CategoriaP>();
        List<DetDescuentoEmpleado> descuentosEmpleado = new List<DetDescuentoEmpleado>();
        ObservableCollection<Empleado> colEmpleados = new ObservableCollection<Empleado>();
        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(balanceEmpleado); }
        }
        public ICollectionView vistaEmpleados
        {
            get { return CollectionViewSource.GetDefaultView(colEmpleados); }
        }
        int _tipoVista = 1; // indica que tipo de vista se esta aplicando al listado: 1 vista general, 2 vista faltantes, 3 vista imputacion
        int i;
        #endregion

        public ucDetEmpleadoTransitioner()
        {
            InitializeComponent();
            colEmpleados = coreEmpleado.EmpleadosActivos();
            btnDatosPersonales.IsEnabled = false;
            //_idempleado = idempleado;
           
           

            //seteamos el bindind de los text block
            //https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-create-a-binding-in-code

          
        }

        private void MostrarBalance( int idempleado)
        {
            balanceEmpleado = coreEmpleado.BalanceEmpleado(_idempleado); // balance de herramientas del empleado
            descuentosEmpleado = coreEmpleado.ListaProductosDescuento(_idempleado); // lista de productos descontados al empleado
            dgDescuentos.ItemsSource = descuentosEmpleado;
            dgDescuentos.DataContext = descuentosEmpleado;
            CalcularCostoBalance(); // calculo del costo de los items en existencia
            vistaProductos.Filter = filtroSoloExistencias;
            dgBalanceEmpleado.ItemsSource = vistaProductos;
            dgBalanceEmpleado.DataContext = vistaProductos;
            CalcularCostoDescuentos();
            txbCantidadItems.Text = CantidadItems.ToString();
            txbCostoTotal.Text = CostoTotalBalance.ToString("C");
        }

        private void MostrarDatosPersonales(int idempleado)
        {
            emp = coreEmpleado.BuscarPorId(_idempleado); // datos adicionales del empleado
            grdDetallesEmpleado.DataContext = emp;
        }

        private void MostrarAutorizaciones(int idempleado)
        {
            autorizacion_Vhs = coreEmpleado.AutorizacionesDeVehiculo(_idempleado); //autorizaciones
            dgAutorizaciones.DataContext = autorizacion_Vhs;
            dgAutorizaciones.ItemsSource = autorizacion_Vhs;
        }

        private void MostrarProductos()
        {
            tiposProducto = coreProducto.ListarTipos(); // lista de tipos de productos 
            cmbTipoProducto.ItemsSource = tiposProducto;

            cmbCategoriaProducto.ItemsSource = cateProducto;
        }

        private void CalcularCostoDescuentos()
        {
            int _cantItems = 0;
            decimal _costoTotal = 0;
            foreach (var item in descuentosEmpleado)
            {
                _cantItems = _cantItems + item.CantidadItem;
                _costoTotal = _costoTotal + item.CostoTotalItem;
            }
            //una vez que contamos y sumamos
            txtTotalItems.Text = _cantItems.ToString();
            txtTotalDescuentos.Text = _costoTotal.ToString("C");
        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                vistaProductos.Filter = filtroCateProducto;
            }
            else
            {
                vistaProductos.Filter = filtroCateProductoObra;
            }
        }

        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            cateProducto = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
            cmbCategoriaProducto.ItemsSource = cateProducto;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                vistaProductos.Filter = filtroTipoProducto;
            }
            else
            {
                vistaProductos.Filter = filtroTipoProductoObra;
            }

        }

        private void dgBalanceEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BalanceEmpleado b = dgBalanceEmpleado.SelectedItem as BalanceEmpleado;
            if (b!= null)
            {
                _idproducto = b.IdProducto;

                
                ucMovimientoHerramienta._balance = balanceEmpleado;
                ucMovimientoHerramienta.idherra = _idproducto;
              
            }
        }

        #region Filtros

        private bool filtroSoloExistencias(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.DifCantidad > 0 && p.EstadoItem != 9;
        }
        private bool filtroObraProductos(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _imputacion = 0;
            if (!string.IsNullOrEmpty(txtImputacion.Text))
            {
                _imputacion = Convert.ToInt32(txtImputacion.Text);
            }
            return p.Imputacion == _imputacion;
        }

        private bool filtroCateProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _cateselec = 0;
            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (ctp != null)
            {


                _cateselec = ctp.IdCateP;

            }
            return p.IdCateP == _cateselec;


        }
        private bool filtroTipoProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _tiposelec = 0;
            TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
            if (t != null)
            {


                _tiposelec = t.IdTipoP;
            }
            return p.IdTipoP == _tiposelec;


        }

        private bool filtroTipoProductoObra(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            int _tiposelec = 0;

            TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
            if (t != null)
            {


                _tiposelec = t.IdTipoP;
            }
            return p.IdTipoP == _tiposelec && p.Imputacion == _imputacion;
        }
        private bool filtroCateProductoObra(object obj)
        {

            BalanceEmpleado p = obj as BalanceEmpleado;
            int _cateselec = 0;
            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (ctp != null)
            {


                _cateselec = ctp.IdCateP;

            }
            return p.IdCateP == _cateselec && p.Imputacion == _imputacion;
        }

        private bool filtroProductosFaltantes(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.EstadoItem == _idproducto;
        }

        private bool filtroUnaHerramienta (object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.IdProducto  == 9;  
        }
        #endregion

        private void btnResetBalance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtTituloVista.Text = "Vista Balance: general";
            balanceEmpleado = coreEmpleado.BalanceEmpleado(_idempleado);
            dgBalanceEmpleado.ItemsSource = balanceEmpleado;
            dgBalanceEmpleado.DataContext = balanceEmpleado;
            CalcularCostoBalance();
            txbCostoTotal.Text = Convert.ToString(CostoTotalBalance);
            txbCantidadItems.Text = Convert.ToString(CantidadItems);
            _tipoVista = 1;
        }

        private void btnFiltroObra_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            vistaProductos.Filter = filtroObraProductos;

        }

        private void btnFaltantes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // aca debemos ajecutar el procedimiento para filtrar por estado del producto como faltante
            //cambiamos primero el titulo de la vista
            txtTituloVista.Text = "Vista Balance: Faltantes";
            vistaProductos.Filter = filtroProductosFaltantes;
            txbCostoTotal.Text = Convert.ToString(CostoBalanceFaltante);
            txbCantidadItems.Text = Convert.ToString(CantidadItemsFaltantes);
            _tipoVista = 2;

        }

        private void btnImprimirBalance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //definimos el parametro del reporte
            // primero debemos preguntar si se quiere imprimir el listado general o el faltantes
            if (_tipoVista == 1)
            {
                //imprimir listado general
                ImprimirBE imprimirBE = new ImprimirBE(_idempleado, 1);
                imprimirBE.Show();
            }
            else
            {
                if (_tipoVista == 2)
                {
                    ImprimirBE imprimirBE = new ImprimirBE(_idempleado, 2);
                    imprimirBE.Show();
                }
            }
        }

        private void btnCEFaltante_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //cambiamos el estado del item a activo, asi de esa manera sale del faltante
            BalanceEmpleado item_b = dgBalanceEmpleado.SelectedItem as BalanceEmpleado;
            if (item_b == null)
            {
                MessageBox.Show("Debe seleccionar un item del listado", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                item_b.IdEmpleado = _idempleado;
                if (item_b.EstadoItem == 9)
                {

                    coreEmpleado.CambiarEstadoProductoEnBalance(item_b.IdProducto, item_b.Imputacion, _idempleado, 1);
                    balanceEmpleado = coreEmpleado.BalanceEmpleado(_idempleado);
                    dgBalanceEmpleado.ItemsSource = balanceEmpleado;
                    dgBalanceEmpleado.DataContext = balanceEmpleado;
                    MessageBox.Show("Se cambio el item a Activo", "Aviso", MessageBoxButton.OK);
                }
                else
                {
                    if (item_b.EstadoItem == 1)
                    {

                        coreEmpleado.CambiarEstadoProductoEnBalance(item_b.IdProducto, item_b.Imputacion, item_b.IdEmpleado, 9);
                        balanceEmpleado = coreEmpleado.BalanceEmpleado(_idempleado);
                        dgBalanceEmpleado.ItemsSource = balanceEmpleado;
                        dgBalanceEmpleado.DataContext = balanceEmpleado;
                        MessageBox.Show("Se cambio el item a Faltante", "Aviso", MessageBoxButton.OK);
                    }
                }

            }
        }

        #region MetodosPrivados
        private void CalcularCostoBalance()
        {
            decimal _costototal = 0;
            int _cantitems = 0;
            int _contador_faltantes = 0;
            decimal _costofaltante = 0;
            decimal _totalItem;
            //refactorizamos o actualizamos el costo de existencia porque esta mal registrado en la tabla balance
            foreach (var item in balanceEmpleado)
            {
                //calculamos los totales por item 
                _totalItem = item.PrecioUnitario * item.DifCantidad;
                //luego actualizamos 
                item.CostoExistencia = _totalItem;
            }

            foreach (var item in balanceEmpleado)
            {

                if (item.DifCantidad > 0)
                {
                    _costototal = item.CostoExistencia + _costototal;
                    _cantitems += 1;

                }
                if (item.EstadoItem == 9)
                {
                    _contador_faltantes += 1;
                    _costofaltante = _costofaltante + item.CostoExistencia;
                }
            }
            CantidadItems = _cantitems;
            CostoTotalBalance = _costototal;
            CostoBalanceFaltante = _costofaltante;
            CantidadItemsFaltantes = _contador_faltantes;
        }
        private void CalcularCostoBalanceImputacion(int imputacion)
        { }
        private void CalcularCostoTipoYCategoria(int tipo, int categoria)
        { }
        #endregion

        #region Botones


        private void btnImprimirBalanceObra_Click(object sender, RoutedEventArgs e)
        {
            // debemos imprimir el balande de obra
            // primero debemos verificar si ademas de la imputacion se selecciono algun tipo y alguna categoria
            if (string.IsNullOrWhiteSpace(txtImputacion.Text))
            {
                MessageBox.Show("Debe indicar una imputacion para poder generar el reporte", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // se ingreso un valor de imputacion
                int _imputacion = Convert.ToInt32(txtImputacion.Text);

                /* if (cmbTipoProducto.SelectedItem != null) // si se selecciono  un tipo de producto
                 {
                     TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
                     if (cmbCategoriaProducto.SelectedItem != null) // si se selecciono una categoria
                     {
                         CategoriaP cate = cmbCategoriaProducto.SelectedItem as CategoriaP;
                         //si se tambien se selecciono una categoria
                         //imprimimos el resumen para una imputacion, un tipo de producto y una categoria
                     }
                     else
                     {
                         //si no selecciono una categoria entonces imprimimo el resumen para una imputacion y un tipo de categoria

                     }
                 }
                 else
                 {
                     // si no selecciono ningun tipo de producto, entonces imprimimos el resumen de solo la imputacion
                     coreEmpleado.BalanceEmpleadoUnaImputacion(_imputacion, emp.IdEmpleado);
                 }*/
                //este es el procedimiento para imprimir el balance de un empleado para una obra determinada

                // string _url = "http://pc-128/reports/report/ServerInformes/TrabajadorBalanceHerramientasUnaObra?idempleado= " + _idempleado + "&imputacion=" + _imputacion;
                //System.Diagnostics.Process.Start(_url);


            }
        }

        private void btnImprimirTipos_Click(object sender, RoutedEventArgs e)
        {
            //debemos imprimir el balance del empleado para el tipo y categoria seleccionados
            if (cmbTipoProducto.SelectedItem != null) // si se selecciono  un tipo de producto
            {
                TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
                if (cmbCategoriaProducto.SelectedItem != null) // si se selecciono una categoria
                {
                    CategoriaP cate = cmbCategoriaProducto.SelectedItem as CategoriaP;
                    //si se tambien se selecciono una categoria
                    //imprimimos el resumen para un tipo de producto y una categoria
                }
                else
                {
                    //si no selecciono una categoria entonces imprimimo el resumen para un tipo de categoria

                }
            }
            else
            {
                //si no se selecciona ningun tipo de producto
                MessageBox.Show("Debe seleccionar un tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "ID";
            ws.Range["B1"].Value = "Cod Inventario";
            ws.Range["C1"].Value = "Producto";
            ws.Range["D1"].Value = "Marca";
            ws.Range["E1"].Value = "Precio Unit";
            ws.Range["F1"].Value = "Imputacion";
            ws.Range["G1"].Value = "Cliente";
            ws.Range["H1"].Value = "Cant Entregada";
            ws.Range["I1"].Value = "Cant Devolucion";
            ws.Range["J1"].Value = "Cant Descuento";
            ws.Range["K1"].Value = "Existencia Actual";
            ws.Range["L1"].Value = "Costo Existencia";

            i = 1;
            foreach (var item in balanceEmpleado)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.IdProducto;
                ws.Range["B" + i].Value = item.CodInventario;
                ws.Range["C" + i].Value = item.NombreProducto;
                ws.Range["D" + i].Value = item.MarcaProducto;
                ws.Range["E" + i].Value = item.PrecioUnitario;
                ws.Range["F" + i].Value = item.Imputacion;
                ws.Range["G" + i].Value = item.ClienteObra;
                ws.Range["H" + i].Value = item.CantidadEntregada;
                ws.Range["I" + i].Value = item.CantidadDevolucion;
                ws.Range["J" + i].Value = item.CantidadDescuento;
                ws.Range["K" + i].Value = item.DifCantidad;
                ws.Range["L" + i].Value = item.CostoExistencia;
                if (item.EstadoItem == 1)
                {
                    //color azul
                    ws.Range["A" + i].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                }
                else
                {
                    if (item.EstadoItem == 2)
                    {
                        //color rojo
                        ws.Range["A" + i].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    }
                    else
                    {
                        //color amarillo
                        ws.Range["A" + i].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    }
                }

            }

        }

        #endregion
        private void btnDelItemDescuento_Click(object sender, RoutedEventArgs e)
        {
            Documento doc;
            //aca tenemos que primero confirmar que se quiere borrar el item
            MessageBoxResult result = MessageBox.Show("Desea borrar el item", "Avisa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // llamamos al metodo que debe borrar el item 
                //este metodo lo que hace es descontar la cantidad del item seleccionado de la cantidad descuento en el balance
                // y la suma a la cantidad en existencia
                DetDescuentoEmpleado det = dgDescuentos.SelectedItem as DetDescuentoEmpleado;
                //buscamos la imputacion a la que pertenece ese vale
                doc = coreRemito.BuscarUnDocumentoPorId(det.IdDocumento);
                coreEmpleado.BorrarItemDescuento(det, _idempleado, doc.Imputacion);
                coreEmpleado.BorrarValeDescuento(det.IdDocumento);
                //luego debemos que refrescar la pantalla
                descuentosEmpleado = coreEmpleado.ListaProductosDescuento(_idempleado);
                dgDescuentos.ItemsSource = descuentosEmpleado;
                dgDescuentos.DataContext = descuentosEmpleado;
            }
        }

        private void btnBusqueda_Click(object sender, RoutedEventArgs e)
        {
           
            //lvwResultadoBusqueda.Items.Clear(); 

            if (string.IsNullOrEmpty(txtBusqueda.Text))
            {
                MessageBox.Show("No se encontraron resultados", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                vistaEmpleados.Filter = buscarEmpleado;
                lvwResultadoBusqueda.ItemsSource = vistaEmpleados;
            }
        }

        private bool buscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtBusqueda.Text);
        }

        private void lvwResultadoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Empleado empleado = lvwResultadoBusqueda.SelectedItem as Empleado;
            _idempleado = empleado.IdEmpleado;
            btnDatosPersonales.IsEnabled = true;
            MostrarBalance(empleado.IdEmpleado);
            MostrarAutorizaciones(empleado.IdEmpleado);
            MostrarDatosPersonales(empleado.IdEmpleado);
            MostrarProductos();

        }
    }
}
