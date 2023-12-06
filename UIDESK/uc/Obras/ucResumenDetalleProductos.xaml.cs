using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;


namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenDetalleProductos.xaml
    /// </summary>
    public partial class ucResumenDetalleProductos : UserControl
    {
        int _imputacion;
        BLLObras coreObras = new BLLObras();
        BLLProducto coreProducto = new BLLProducto();
        List<BalanceObraProductosDetalle> productos = new List<BalanceObraProductosDetalle>();
        List<CategoriaP> lista_catp = new List<CategoriaP>();
        List<CategoriaP> lista_cate_TipoProducto = new List<CategoriaP>(); //  categorias de productos correspondiente al tipo de producto seleccionado
        ObservableCollection<TipoProducto> lista_tipo = new ObservableCollection<TipoProducto>();
        public ICollectionView vistaProductos
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }
        int i;

        public ucResumenDetalleProductos(int imputacion)
        {
            InitializeComponent();
            _imputacion = imputacion;
            productos = coreObras.BalanceProductosUnaObra(_imputacion);
            dgDetalleProducto.ItemsSource = productos;
            dgDetalleProducto.DataContext = productos;
            lista_catp = coreObras.BalanceCatePUnaObra(_imputacion);
            cmbCategoriaProducto.ItemsSource = null;
            cmbCategoriaProducto.IsEnabled = false;
            chkVerExistencia.IsEnabled = false;
            lista_tipo = coreProducto.ListarTipos();
            cmbTipoProducto.ItemsSource = lista_tipo;
        }

        #region Vistas
        private bool filtroCatePro(object obj)
        {
            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;

            BalanceObraProductosDetalle bal = obj as BalanceObraProductosDetalle;

            return bal.IdCateP == categoriaP.IdCateP;


        }

        private bool filtroProExistenciaCategoria(object obj)
        {
            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;

            BalanceObraProductosDetalle bal = obj as BalanceObraProductosDetalle;


            return bal.IdCateP == categoriaP.IdCateP && bal.DifCantidad > 0;



        }
        private bool filtroProExistenciaGral(object obj)
        {

            BalanceObraProductosDetalle bal = obj as BalanceObraProductosDetalle;

            return bal.DifCantidad > 0;

        }

        private bool filtroTipoProducto(object obj)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            BalanceObraProductosDetalle bal = obj as BalanceObraProductosDetalle;
            return bal.IdTipoP == tipo.IdTipoP;
        }


        #endregion


        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                //aca deberiamos aplicar el filtro por categoria de producto
                vistaProductos.Filter = filtroCatePro;
                dgDetalleProducto.ItemsSource = vistaProductos;
                dgDetalleProducto.DataContext = vistaProductos;
                chkVerExistenciaGral.IsEnabled = false;
                int _sum = 0;
                foreach (var item in productos)
                {
                    if (item.IdCateP == categoriaP.IdCateP)
                    {


                        _sum = _sum + item.CantEntregada;
                    }
                }
                txbCTProductos.Text = _sum.ToString();
            }
        }

        private void dgDetalleProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chkVerExistencia_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                vistaProductos.Filter = filtroProExistenciaCategoria;
                dgDetalleProducto.ItemsSource = vistaProductos;
                dgDetalleProducto.DataContext = vistaProductos;

                int _sum = 0;
                foreach (var item in productos)
                {
                    if (item.IdCateP == categoriaP.IdCateP)
                    {


                        _sum = _sum + item.CantEntregada;
                    }
                }
                txbCTProductos.Text = _sum.ToString();
            }
        }

        private void chkVerExistencia_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {

            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                vistaProductos.Filter = filtroCatePro;
                dgDetalleProducto.ItemsSource = vistaProductos;
                dgDetalleProducto.DataContext = vistaProductos;
                int _sum = 0;
                foreach (var item in productos)
                {
                    if (item.IdCateP == categoriaP.IdCateP)
                    {


                        _sum = _sum + item.CantEntregada;
                    }
                }
                txbCTProductos.Text = _sum.ToString();
            }
        }

        private void btnReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //volvemos a cargar la lista con los resultados totales
            //cualquier filtro que se encuentre aplicado se borra
            CargarListadoGeneral();


        }

        private void CargarListadoGeneral()
        {
            productos = coreObras.BalanceProductosUnaObra(_imputacion);
            dgDetalleProducto.ItemsSource = productos;
            dgDetalleProducto.DataContext = productos;
            chkVerExistenciaGral.IsEnabled = true;
            chkVerExistenciaGral.IsChecked = false;
            cmbCategoriaProducto.SelectedItem = null;
            chkVerExistencia.IsChecked = false;
            chkVerExistencia.IsEnabled = false;
            cmbCategoriaProducto.IsEnabled = false;
            int _countItem = 0;
            foreach (var item in productos)
            {
                _countItem++;
            }

            txtRegistros.Text = _countItem.ToString();

        }

        private void chkVerExistenciaGral_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

            vistaProductos.Filter = filtroProExistenciaGral;
            dgDetalleProducto.ItemsSource = vistaProductos;
            dgDetalleProducto.DataContext = vistaProductos;
            //desabilitamos otras vistas
            chkVerExistencia.IsEnabled = false;
            cmbCategoriaProducto.IsEnabled = false;

        }

        private void chkVerExistenciaGral_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            //aca volvemos a cargar el listado con todo
            //lamando al metodo correspondiente
            CargarListadoGeneral();
        }

        private void btnReporteExistencia_Click(object sender, System.Windows.RoutedEventArgs e)
        {/*
            string _url = "http://pc-128/reports/report/ServerInformes/BalanceObraResumenProductos?imputacion= "  + _imputacion;
            System.Diagnostics.Process.Start(_url);*/
        }

        private void cmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            if (tipo != null)
            {
                vistaProductos.Filter = filtroTipoProducto;
                dgDetalleProducto.ItemsSource = vistaProductos;
                dgDetalleProducto.DataContext = vistaProductos;
                cmbCategoriaProducto.ItemsSource = null;
                cmbCategoriaProducto.IsEnabled = true;
                chkVerExistencia.IsEnabled = true;
                lista_cate_TipoProducto.Clear();
                foreach (var item in lista_catp)
                {
                    if (item.IdTipoP == tipo.IdTipoP)
                    {
                        lista_cate_TipoProducto.Add(item);
                    }

                }
                cmbCategoriaProducto.ItemsSource = lista_cate_TipoProducto;
                int _countItem = 0;
                foreach (var item in vistaProductos)
                {
                    _countItem++;
                }

                txtRegistros.Text = _countItem.ToString();
            }
        }

        private void btnExportarExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "Categoria";
            ws.Range["B1"].Value = "Producto";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Marca";
            ws.Range["E1"].Value = "Cod Inventario";
            ws.Range["F1"].Value = "Cant. Entregada";
            ws.Range["G1"].Value = "Cant. Devolucion";
            ws.Range["H1"].Value = "Existencia";
            i = 1;
            foreach (var item in productos)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.Categoria;
                ws.Range["B" + i].Value = item.Nombre;
                ws.Range["C" + i].Value = item.Modelo;
                ws.Range["D" + i].Value = item.Marca;
                ws.Range["E" + i].Value = item.CodInventario;
                ws.Range["F" + i].Value = item.CantEntregada;
                ws.Range["G" + i].Value = item.CantDevolucion;
                ws.Range["H" + i].Value = item.DifCantidad;
            }
        }
    }
}
