using System;
using System.Collections.Generic;
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
using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Drawing.Chart;

namespace UIDESK.uc.tablerocostos
{
    /// <summary>
    /// Lógica de interacción para ucResultadoGeneralCostos.xaml
    /// </summary>
    public partial class ucResultadoGeneralCostos : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLProducto coreProducto = new BLLProducto();
        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public Func<double, string> YFormatter { get; set; }
        //ACCombustible datosConsumo = new ACCombustible();
        public decimal punto1;
        public decimal punto2;
        public decimal punto3;
        public decimal _ctCombustible;
        public decimal _ctManteVh, _ctManteP;
        public decimal _ctCompraVh, _ctCompraP;
        public decimal _ctTotalMante, _ctTotalCompras;
        public decimal CostoTotalCombustibles { get; set; } 
        public decimal CostoTotalInversiones { get; set; }
        public decimal CostoTotalMantenimientos { get; set; }
        int _anioBuscar = DateTime.Today.Year;
        

        public ucResultadoGeneralCostos()
        {
            InitializeComponent();
           
           // CalcularLosPuntos();
           // ArmarGrafico();
            //CalcularTotales();
            
        }

        private void graficoPie_DataClick(object sender, ChartPoint chartPoint)
        {
            if (chartPoint.SeriesView.Title=="Combustibles")
            {
                MessageBox.Show("Combustibles", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (chartPoint.SeriesView.Title == "Mantenimientos")
            {
                MessageBox.Show("Mantenimientos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            if (chartPoint.SeriesView.Title == "Inversiones")
            {
                MessageBox.Show("Inversiones", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }

        private void CalcularTotales()
        {
            stkTotales.DataContext = null;
            CostoTotalCombustibles = _ctCombustible;
            CostoTotalInversiones = _ctTotalCompras;
            CostoTotalMantenimientos = _ctTotalMante;
            stkTotales.DataContext = this;
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        private void btnBuscarAnio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnioBuscar.Text))
            {
                MessageBox.Show("Debe ingresar un valor para el año ", "Buscar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                _anioBuscar = Convert.ToInt32(txtAnioBuscar.Text);
                CalcularLosPuntos();
                ArmarGrafico();
                CalcularTotales();
            }
        }

        private void ArmarGrafico()
        {
            PointLabel = chartpoint => string.Format("{0:C}({1:P})", chartpoint.Y, chartpoint.Participation);
            graficoPie.Series = new LiveCharts.SeriesCollection
            {
                new PieSeries { Title = "Combustibles", 
                                Values = new ChartValues<decimal> { punto1 } ,
                                DataLabels=true, 
                                LabelPoint=PointLabel,},
                new PieSeries { Title="Mantenimientos", Values = new ChartValues<decimal> { punto3} , DataLabels=true, LabelPoint=PointLabel},
                new PieSeries { Title = "Inversiones", Values = new ChartValues<decimal> { punto2 } ,DataLabels=true, LabelPoint=PointLabel, Fill=Brushes.DarkGreen },
              

            };
            //txtAnio.Text = _anioBuscar.ToString();
        }

        private void CalcularLosPuntos()
        {
            //aca ejecutamos los metodos que nos devuelven los valores decimales de los costos
            _ctCombustible = coreVh.CostoTotalCombustibleUnAnio(_anioBuscar);
            _ctManteVh = coreVh.CostoTotalMantenimientoUnAnio(_anioBuscar);
            _ctManteP = coreProducto.CostoTotalMantenimientoUnAnio(_anioBuscar);
            _ctCompraVh = coreVh.CostoTotalComprasUnAnio(_anioBuscar);
            _ctCompraP = coreProducto.CostoTotalComprasUnAnio(_anioBuscar);
           
            _ctTotalCompras = _ctCompraP + _ctCompraVh;
            _ctTotalMante = _ctManteP + _ctManteVh;
            // asignamos puntos para representar en el grafico
            punto1 = _ctCombustible;
            punto2 = _ctTotalCompras;
            punto3 = _ctTotalMante;
            
        }


        private void GenerateExcelFile() 
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            ws.Range["A1"].Value = "Resumen Año:";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["B1"].Value = _anioBuscar;
            ws.Range["B1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A4"].Value = "Combustibles";
            ws.Range["A4","B4"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSlateBlue);
            ws.Range["A4", "B4"].Font.Bold = true;
            ws.Range["A5"].Value = "Mantenimientos";
            ws.Range["A5","B5"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
            ws.Range["A5", "B5"].Font.Bold = true;
            ws.Range["A6"].Value = "Inversiones";
            ws.Range["A6","B6"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkSlateGray);
            ws.Range["A6", "B6"].Font.Bold = true;
      
            ws.Range["B4"].Value = _ctCombustible;
            ws.Range["B5"].Value = _ctTotalMante;
            ws.Range["B6"].Value = _ctTotalCompras;
            ws.Range["B4", "B6"].NumberFormat="$0,00";
            ws.Range["B4", "B6"].Borders.LineStyle = XlLineStyle.xlContinuous;
            ws.Range["A4", "A6"].Borders.LineStyle = XlLineStyle.xlContinuous;
            ws.Range["A4", "B6"].Borders.LineStyle = XlLineStyle.xlDouble;

            ChartObjects xlCharts = null; // coleccion de graficos de la hoja
            ChartObject myChart = null; // contenedor del objeto chart
            Chart _chart = null; // objeto grafico en el que vamos a trabajar
            //ExcelPieChart pieChart 
            Range _chartRange = null;
            object misValue = System.Reflection.Missing.Value;
            xlCharts = (ChartObjects)ws.ChartObjects(Type.Missing);
            
            myChart = (ChartObject)xlCharts.Add(250,50,400,250); // izq, arriba, ancho , alto
            _chart = myChart.Chart;
            _chart.ChartType = XlChartType.xlPie;
            
            
            _chartRange = ws.Range["A4","B6"]; // rango de celdas que se usan para el grafico

            _chart.HasTitle = true;
            _chart.ChartTitle.Caption = "Resumen Costos Generales";
            
            
            _chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
            _chart.SetSourceData(_chartRange,misValue);
            
            myChart.Chart.HasLegend = true;
            myChart.Chart.ShowDataLabelsOverMaximum = true;
            

        }
    }
}
