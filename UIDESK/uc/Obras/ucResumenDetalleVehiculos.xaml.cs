using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenDetalleVehiculos.xaml
    /// </summary>
    public partial class ucResumenDetalleVehiculos : UserControl
    {
        int _imputacion;
        BLLVehiculos coreVehiculos = new BLLVehiculos();
        BLLObras coreObras = new BLLObras();
        List<Asignacion_vh> colvehiculos = new List<Asignacion_vh>();
        List<Asignacion_vh> listaVhObra = new List<Asignacion_vh>();
        int i;
        public ICollectionView vistaVehiculos
        {
            get { return CollectionViewSource.GetDefaultView(colvehiculos); }
        }

        public ucResumenDetalleVehiculos(int imputacion)
        {
            InitializeComponent();
            _imputacion = imputacion;
            colvehiculos = coreObras.ListarAsignacionesUnaObra(_imputacion);
            listaVhObra = coreObras.ListaVehiculosUnaObra(_imputacion);
            dgDetalleVh.ItemsSource = colvehiculos;
            dgDetalleVh.DataContext = colvehiculos;
            dgVhAsignados.ItemsSource = listaVhObra;
            dgVhAsignados.DataContext = listaVhObra;
            txbCantidadVh.Text = colvehiculos.Count.ToString();
            txbCantidadVhAsignados.Text = listaVhObra.Count.ToString();
        }

        private void dgDetalleVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

            ws.Range["A1"].Value = "Id";
            ws.Range["B1"].Value = "Categoria";
            ws.Range["C1"].Value = "Dominio";
            ws.Range["D1"].Value = "Modelo";
            ws.Range["E1"].Value = "Dias Obra";
            ws.Range["F1"].Value = "Horas";
            ws.Range["G1"].Value = "KM";

            i = 1;
            foreach (var item in colvehiculos)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.IdVh;
                ws.Range["B" + i].Value = item.Categoria;
                ws.Range["C" + i].Value = item.Dominio;
                ws.Range["D" + i].Value = item.Modelo;
                ws.Range["E" + i].Value = item.DiasAcumulados;
                ws.Range["F" + i].Value = item.HsAcuObra;
                ws.Range["G" + i].Value = item.KmAcuObra;

            }
        }

        private void dgVhAsignados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnExExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateExcelFile2();
        }

        private void GenerateExcelFile2()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "Id";
            ws.Range["B1"].Value = "Dominio";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Marca";
            ws.Range["E1"].Value = "Categoria";


            i = 1;
            foreach (var item in listaVhObra)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.IdVh;
                ws.Range["B" + i].Value = item.Dominio;
                ws.Range["C" + i].Value = item.Modelo;
                ws.Range["D" + i].Value = item.Marca;
                ws.Range["E" + i].Value = item.Categoria;


            }
        }
    }
}
