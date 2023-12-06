using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucGestionIndumentaria.xaml
    /// </summary>
    public partial class ucGestionIndumentaria : UserControl
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        BLLBase coreBase = new BLLBase();
        ObservableCollection<CategoriaP> stock_categoria = new ObservableCollection<CategoriaP>();
        ObservableCollection<StockProducto> stock_producto = new ObservableCollection<StockProducto>();
        ObservableCollection<CategoriaP> stock_categoria_anio = new ObservableCollection<CategoriaP>();
        ObservableCollection<StockProducto> stock_producto_anio = new ObservableCollection<StockProducto>();
        int i;
        #endregion

        public ucGestionIndumentaria()
        {
            InitializeComponent();
            stock_categoria = coreProducto.ListarStockActualCategoriasIndumentaria();
            dgStockCategorias.ItemsSource = stock_categoria;
            dgStockCategorias.DataContext = stock_categoria;
            txtIdDeposito.Text = Convert.ToString(1);
            txtNombreDeposito.Text = "Deposito Herrramientas";
        }


        #region Filtros

        #endregion

        #region Botones

        private void btnEstadoStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImprimirResumen_Click(object sender, RoutedEventArgs e)
        {



        }
        #endregion

        #region Textbox

        #endregion

        #region Datagrids

        #endregion

        private void dgStockCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaP p = dgStockCategorias.SelectedItem as CategoriaP;
            if (p != null)
            {
                int _iddepo = Convert.ToInt16(txtIdDeposito.Text);
                stock_producto = coreProducto.ListarStockActualIndumentariaUnaCategoria(p.IdCateP, _iddepo);
                dgStockDetCategorias.ItemsSource = stock_producto;
                dgStockDetCategorias.DataContext = stock_producto;
            }
        }

        private void txtIdDeposito_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }

        private void btnImprimirResultado_Click(object sender, RoutedEventArgs e)
        {
            //este es el procedimiento para imprimir el informe del stock de la categoria
            CategoriaP cp = dgStockCategorias.SelectedItem as CategoriaP;

            if (cp != null)
            {
                int _idcate = cp.IdCateP;
                int _iddepo = Convert.ToInt32(txtIdDeposito.Text);
                string _url = "http://pc-128/reports/report/ServerInformes/DetalleStockCategoriaProducto?idcatep=" + _idcate + "&iddeposito=" + _iddepo + "";
                _ = System.Diagnostics.Process.Start(_url);
            }
            else
            {
                MessageBox.Show("el objeto es nulo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
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

            ws.Range["A1"].Value = "Id";
            ws.Range["B1"].Value = "Producto";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Marca";
            ws.Range["E1"].Value = "Stock Actual";
            ws.Range["F1"].Value = "Punto Pedido";
            i = 1;
            foreach (var item in stock_producto)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.IdProducto;
                ws.Range["B" + i].Value = item.NombreProducto;
                ws.Range["C" + i].Value = item.ModeloProducto;
                ws.Range["D" + i].Value = item.Marca;
                ws.Range["E" + i].Value = item.StkActual;
                ws.Range["F" + i].Value = item.PuntoPedido;
                ws.Range["G" + i].Value = item.SituacionStock;
            }

        }

        private void btnExcelCategoria_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFileCategorias();
        }

        private void GenerateExcelFileCategorias()
        {
             Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
              app.Visible = true;
              app.WindowState = XlWindowState.xlMaximized;

              Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
              Worksheet ws = wb.Worksheets[1];
              //DateTime currentDate = DateTime.Now;

              ws.Range["A1"].Value = "Id";
              ws.Range["B1"].Value = "Categoria";
              ws.Range["C1"].Value = "Stock Actual";

              i = 1;
              foreach (var item in stock_categoria)
              {
                  i = i + 1;
                  ws.Range["A" + i].Value = item.IdCateP;
                  ws.Range["B" + i].Value = item.NomCateP;
                  ws.Range["C" + i].Value = item.StockActual;

              }
        }
    }
}
