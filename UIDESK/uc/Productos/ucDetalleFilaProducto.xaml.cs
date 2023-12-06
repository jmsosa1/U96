using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.imprimir;
using UIDESK.uc.Productos;

namespace UIDESK.uc
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaProducto.xaml
    /// </summary>
    public partial class ucDetalleFilaProducto : UserControl
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        BLLRemito coreRemito = new BLLRemito();
        public static int _idproducto = 0;// codigo del producto para grabar las fotos
        public static bool _vistalab = false; // codigo que indica si se esta en la vista de laboratorio
        ObservableCollection<StockProducto> stockDelProducto = new ObservableCollection<StockProducto>();
        ObservableCollection<VisorDocObras> lista_visor = new ObservableCollection<VisorDocObras>();
        ObservableCollection<FotoProducto> lista_fotos = new ObservableCollection<FotoProducto>(); //coleccion de fotos
        ObservableCollection<Mante_P> lista_mante = new ObservableCollection<Mante_P>();// collecion de mantenimientos de un vehiculo
        ObservableCollection<CompraP> lista_compras = new ObservableCollection<CompraP>(); // coleccion de compras del producto
        ObservableCollection<RMAProducto> lista_rma = new ObservableCollection<RMAProducto>();
        Producto _producto = new Producto();
        CultureInfo ci = new CultureInfo("es-AR");
        public decimal CostoTotalMante { get; set; }
        //vistas
        public ICollectionView vistafotos
        {
            get { return CollectionViewSource.GetDefaultView(lista_fotos); }
        }
        public ICollectionView vistamante
        {
            get { return CollectionViewSource.GetDefaultView(lista_mante); }
        }

        public ICollectionView vistamovimientos
        {
            get { return CollectionViewSource.GetDefaultView(lista_visor); }
        }
        #endregion


        public ucDetalleFilaProducto()
        {
            InitializeComponent();
            stockDelProducto = coreProducto.StockDeUnProducto(_idproducto);
            dgStock.ItemsSource = stockDelProducto;
            dgStock.DataContext = stockDelProducto;
            lista_visor = coreRemito.RemitosObraUnProducto(_idproducto);
            dgMovimientos.ItemsSource = lista_visor;
            dgMovimientos.DataContext = lista_visor;
            lista_fotos = coreProducto.ProductoListaFotos(_idproducto);
            grdGaleria.DataContext = lista_fotos;
            lista_mante = coreProducto.ListarManteUnProducto(_idproducto);
            dgMantenimientos.ItemsSource = lista_mante;
            dgMantenimientos.DataContext = lista_mante;
            lista_compras = coreProducto.CompraListarUnProducto(_idproducto);
            dgCompras.ItemsSource = lista_compras;
            dgCompras.DataContext = lista_compras;
            lista_rma = coreProducto.ListarRMAUnProducto(_idproducto);
            dgRMA.DataContext = lista_rma;
            dgRMA.ItemsSource = lista_rma;
            _producto = coreProducto.BuscarDatosUno(_idproducto);
            CalcularCostoMante();
            CalcularCostoCompras();
            CalcularMovimientos();
            DesactivarPestanas();
        }

        private void DesactivarPestanas()
        {
            if (_vistalab)
            {

                itemRma.IsEnabled = false;
                itemCompras.IsEnabled = false;
                itemStock.IsEnabled = false;
            }    
            
        }

        private void CalcularMovimientos()
        {
            txtCantidadMovimientos.Text = lista_visor.Count.ToString();
        }

        private void CalcularCostoCompras()
        {
            int _cantidadU = 0;
            decimal _importe = 0;
            foreach (var item in lista_compras)
            {
                _importe += item.TotalItemUno;
                _cantidadU = _cantidadU + item.CantidadUno; ;
            }
            txtCantCompras.Text = _cantidadU.ToString();
            txtTotalCostoCompras.Text = _importe.ToString("C", ci);
        }



        #region filtros
        private bool filtroProveedor(object obj)
        {
            Mante_P mante = obj as Mante_P;
            int _idprove = Convert.ToInt32(txtIdProveedor.Text);
            return mante.IdProve == _idprove;

        }

        private bool filtroMoviPorObra(object obj)
        {
            VisorDocObras visor = obj as VisorDocObras;
            int _imputacion = Convert.ToInt32(txtBusqueda.Text);
            return visor.Imputacion == _imputacion;
        }
        private bool filtroMoviEmpleado(object obj)
        {
            VisorDocObras visor = obj as VisorDocObras;
            int _idempleado = Convert.ToInt32(txtBusqueda.Text);
            return visor.IdEmpleado == _idempleado;
        }




        #endregion

        #region Fotos


        private void BtnPrevFoto_Click(object sender, RoutedEventArgs e)
        {
            //navegar hacia Atras
            vistafotos.MoveCurrentToPrevious();
            if (vistafotos.IsCurrentBeforeFirst)
            {
                vistafotos.MoveCurrentToLast();
            }
        }

        private void BtnNextFoto_Click(object sender, RoutedEventArgs e)
        {
            //navegar hacia adelante
            vistafotos.MoveCurrentToNext();
            if (vistafotos.IsCurrentAfterLast)
            {
                vistafotos.MoveCurrentToFirst();
            }
        }

        private void BtnDelFoto_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuestadialogo = MessageBox.Show("Desea Borrar la foto del producto?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (respuestadialogo == MessageBoxResult.Yes)
            {
                FotoProducto foto = new FotoProducto();
                foto = vistafotos.CurrentItem as FotoProducto;
                coreProducto.BorrarFoto(foto.IdFotoP);
                lista_fotos = coreProducto.ProductoListaFotos(_idproducto);
                grdGaleria.DataContext = lista_fotos;
            }

        }

        private void BtnAddFoto_Click(object sender, RoutedEventArgs e)
        {
            ABMFotoProducto aBMFotoProducto = new ABMFotoProducto();
            aBMFotoProducto._idproducto = _idproducto;
            if (aBMFotoProducto.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la foto del producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                lista_fotos = coreProducto.ProductoListaFotos(_idproducto);
                grdGaleria.DataContext = lista_fotos;
            }
        }
        #endregion

        #region Stock



        private void btnCambiarPosicion_Click(object sender, RoutedEventArgs e)
        {
            StockProducto st = dgStock.SelectedItem as StockProducto;
            if (st != null)
            {
                ModiPosicionProducto modi = new ModiPosicionProducto(st);

                if (modi.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico la posicion del producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    stockDelProducto = coreProducto.StockDeUnProducto(_idproducto);
                    dgStock.ItemsSource = stockDelProducto;
                    dgStock.DataContext = stockDelProducto;
                    //se deber registrar la operacion
                }
            }
        }

        private void btnCambiarPuntoPedido_Click(object sender, RoutedEventArgs e)
        {
            StockProducto st = dgStock.SelectedItem as StockProducto;
            if (st != null)
            {
                CambiarPuntoPedido modipp = new CambiarPuntoPedido(st);
                modipp.txtPPActual.Text = st.PuntoPedido.ToString();

                if (modipp.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el punto de pedido del producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    stockDelProducto = coreProducto.StockDeUnProducto(st.IdProducto);
                    dgStock.ItemsSource = stockDelProducto;
                    dgStock.DataContext = stockDelProducto;
                }
            }
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            if (rdbTodos.IsChecked == true)
            {
                //filtramos por todos los movimientos
                lista_visor = coreRemito.RemitosObraUnProducto(_idproducto);
                dgMovimientos.ItemsSource = lista_visor;
                dgMovimientos.DataContext = lista_visor;
                CalcularMovimientos();

            }
            if (rdbObra.IsChecked == true)
            {
                //filtramos los documentos por obra
                vistamovimientos.Filter = filtroMoviPorObra;
                CalcularMovimientos();

            }
            if (rdbEmpleado.IsChecked == true)
            {
                //filtramos los documentos por empleado
                vistamovimientos.Filter = filtroMoviEmpleado;
                CalcularMovimientos();
            }
        }



        private void btnCambiarSituacionStock_Click(object sender, RoutedEventArgs e)
        {
            StockProducto sp = new StockProducto();
            if (sp != null)
            {
                ModiCondicionStock modiCondicionStock = new ModiCondicionStock(sp);
                if (modiCondicionStock.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo la situacion de stock del producto ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    //se deberia registrar la operacion
                }
            }

        }


        private void btnAjustarStock_Click(object sender, RoutedEventArgs e)
        {
            StockProducto p = dgStock.SelectedItem as StockProducto;
            if (p != null)
            {
                AjustarStock ajustar = new AjustarStock(p);
                if (ajustar.ShowDialog() == true)
                {
                    MessageBox.Show("Se ajusto el stock del producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    stockDelProducto = coreProducto.StockDeUnProducto(p.IdProducto);
                    dgStock.ItemsSource = stockDelProducto;
                    dgStock.DataContext = stockDelProducto;
                    // se deberia registrar esta operacion
                }
            }



        }

        private void dgStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ucDetalleAjustesStock._idproducto = _idproducto;
        }
        #endregion

        #region Mantenimientos
        private void btnfiltroProveMante_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProveedor.Text))
            {
                MessageBox.Show("Debe indicar un codigo de proveedor antes", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                vistamante.Filter = filtroProveedor;
            }

        }


        private void CalcularCostoMante()
        {
            decimal _costo = 0;
            int _cantidadMante = 0;
            foreach (var item in lista_mante)
            {
                _costo += item.ImporteFactura;
                _cantidadMante += 1;
            };

            txtRegistros.Text = _cantidadMante.ToString();
            txtTotalCosto.Text = _costo.ToString("C", ci);
        }
        #endregion



        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            RMAProducto rma = dgRMA.SelectedItem as RMAProducto;
            if (rma == null)
            {
                MessageBox.Show("Debe elegir un registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                PrintRMA printRMA = new PrintRMA(rma);
                if (printRMA.ShowDialog() == true)
                {
                    MessageBox.Show("Debe elegir un registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                //ImprimirRMA imprimir = new ImprimirRMA(rma.IdRma); lineas anteriores cuando andaba el servidor de reportes
                //imprimir.Show();

            }
        }

        private void dgRMA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRMA.SelectedItem == null)
            {

            }
        }

        private void btnExportarMov_Click(object sender, RoutedEventArgs e)
        {
            ExportarMovimientos();
        }

        private void ExportarMovimientos()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;
            int i;
            ws.Range["A1"].Value = "Producto:";
            ws.Range["A2"].Value = _producto.Nombre;
            ws.Range["B2"].Value = _producto.CodInventario;
            ws.Range["A3"].Value = "Detalle de Movimientos";
            ws.Range["A4"].Value = "Fecha";
            ws.Range["B4"].Value = "Remito";
            ws.Range["C4"].Value = "Concepto";
            ws.Range["D4"].Value = "Imputacion";
            ws.Range["E4"].Value = "Obra";
            ws.Range["F4"].Value = "Destinatario";
            i = 4;
            foreach (var item in lista_visor)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.FechaRemito;
                ws.Range["B" + i].Value = item.IdDocumento;
                ws.Range["C" + i].Value = item.Concepto;
                ws.Range["D" + i].Value = item.Imputacion;
                ws.Range["E" + i].Value = item.NombreObra;
                ws.Range["F" + i].Value = item.NombreEmpleado;
            }
        }

        private void btnExportarCompras_Click(object sender, RoutedEventArgs e)
        {
            ExportarCompras();
        }

        private void ExportarCompras()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;
            int i;
            ws.Range["A1"].Value = "Fecha";
            ws.Range["B1"].Value = "Proveedor";
            ws.Range["C1"].Value = "Cantidad";
            ws.Range["D1"].Value = "Precio Unit";
            ws.Range["E1"].Value = "Total";
           
            i = 1;
            foreach (var item in lista_compras)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.FechaCompra;
                ws.Range["B" + i].Value = item.NombreProveedor;
                ws.Range["C" + i].Value = item.CantidadUno;
                ws.Range["D" + i].Value = item.PrecioUniUno;
                ws.Range["E" + i].Value = item.TotalItemUno;
                
            }
        }

        private void btnExportarMantenimientos_Click(object sender, RoutedEventArgs e)
        {
            ExportarMantenimientos();
        }

        private void ExportarMantenimientos()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;
            int i;
            ws.Range["A1"].Value = "Fecha Factura";
            ws.Range["B1"].Value = "Proveedor";
            ws.Range["C1"].Value = "Detalle";
            ws.Range["D1"].Value = "Factura";
            ws.Range["E1"].Value = "Importe";
            ws.Range["F1"].Value = "Imputacion";
            ws.Range["G1"].Value = "Cliente";
            i = 1;
            foreach (var item in lista_mante)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.FechaFactura;
                ws.Range["B" + i].Value = item.IdProve;
                ws.Range["C" + i].Value = item.DetalleMante;
                ws.Range["D" + i].Value = item.NumFacturaProve;
                ws.Range["E" + i].Value = item.ImporteFactura;
                ws.Range["F" + i].Value = item.Imputacion;
                ws.Range["G"].Value = item.ClienteObra;
            }
        }
    }
}
