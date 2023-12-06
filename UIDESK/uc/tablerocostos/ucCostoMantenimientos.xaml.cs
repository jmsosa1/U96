using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Globalization;

namespace UIDESK.uc.tablerocostos
{
    /// <summary>
    /// Lógica de interacción para ucCostoMantenimientos.xaml
    /// </summary>
    public partial class ucCostoMantenimientos : UserControl
    {
        BLLProducto coreProducto = new BLLProducto();
        BLLVehiculos coreVehiculo = new BLLVehiculos();
        List<CostosGenericos> costos_mante_vh = new List<CostosGenericos>(); // costos inversiones productos
        List<CostosGenericos> costos_mante_p = new List<CostosGenericos>(); // costos inversiones productos
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public LiveCharts.SeriesCollection SeriesCollection1 { get; set; }
        public ChartValues<decimal> values_totales_Pro = new ChartValues<decimal>();
        public ChartValues<decimal> values_totales_Vh = new ChartValues<decimal>();
        public ObservableCollection<ValoresTotales> valoresTotales = new ObservableCollection<ValoresTotales>();
        CultureInfo ci = new CultureInfo("es-AR");
        int _anioBuscar;

        public ucCostoMantenimientos()
        {
            InitializeComponent();
            _anioBuscar = DateTime.Today.Year;
            txtAnio.Text = _anioBuscar.ToString();
            costos_mante_vh = coreVehiculo.CostoMantenimientoAnioMes(_anioBuscar);
            costos_mante_p = coreProducto.CostoMantenimientoAnioMes(_anioBuscar);
            CalcularCostosTotales();
         
            GenerarGraficos();
        }

        private void GenerarGraficos()
        {

            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            YFormatter = value => value.ToString("C");

            SeriesCollection1 = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Mantenimientos Productos",
                    Values = values_totales_Pro

                },
                new LineSeries
                {
                    Title = "Mantenimientos Vehiculos",
                    Values = values_totales_Vh

                },

            };
            grafTotales.DataContext = this;
        }

        private void CalcularCostosTotales()
        {
            values_totales_Pro.Clear();
            values_totales_Vh.Clear();
            valoresTotales.Clear();
            for (int i = 1; i < 13; i++)
            {
                ValoresTotales totales = new ValoresTotales();
                totales.Mes = i;
                totales.CostoInvProducto = costos_mante_p
                                           .Where(x => x.Mes == i)
                                           .Select(x => x.Importe)
                                           .Sum();
                totales.CostoInvVehiculo = costos_mante_vh
                                           .Where(x => x.Mes == i)
                                           .Select(x => x.Importe)
                                           .Sum();
                valoresTotales.Add(totales);

            }
            dgTotales.ItemsSource = valoresTotales;
            dgTotales.DataContext = valoresTotales;
            // calcualo de valores para el grafico de totales
            decimal _tP = 0;
            decimal _tV = 0;
            foreach (var item in valoresTotales)
            {
                values_totales_Pro.Add(item.CostoInvProducto);
                values_totales_Vh.Add(item.CostoInvVehiculo);
                _tP += item.CostoInvProducto;
                _tV += item.CostoInvVehiculo;
            }
            txbInvPro.Text = _tP.ToString("C", ci);
            txbInvVh.Text = _tV.ToString("C", ci);
        }


        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //titulo del resumen en excel
            ws.Range["A1"].Value = "Planilla de Costos Mensuales ";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A2"].Value = "Año :";
            ws.Range["B2"].Value = _anioBuscar;
            ws.Range["A2", "B2"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            //seteamos el grid de los totales
            ws.Range["A4"].Value = "MES";
            ws.Range["B4"].Value = "Mantenimientos Productos";
            ws.Range["C4"].Value = "Mantenimientos Vehiculos";
            // pasamos los valores para este rango iterando la lista de totales
            int j = 5; //  indicador de posicion de celda
            foreach (var item in valoresTotales)
            {
                ws.Range["A" + j].Value = item.Mes;

                ws.Range["B" + j].Value = item.CostoInvProducto;


                ws.Range["C" + j].Value = item.CostoInvVehiculo;
                j++; // aumentamos una posicion el indice de celda
            }
            ws.Range["B5", "C17"].NumberFormat = "$0,00"; // formato moneda para los valores
            ws.Range["A5", "C17"].Borders.LineStyle = XlLineStyle.xlContinuous; // lineas en todas las celdas
            //armamos el grafico para este conjunto de celdas
            ChartObjects xlCharts = null; // coleccion de graficos de la hoja
            ChartObject myChart = null; // contenedor del objeto chart
            Chart _chart = null; // objeto grafico en el que vamos a trabajar
            //ExcelPieChart pieChart 
            Range _chartRange = null;
            object misValue = System.Reflection.Missing.Value;
            xlCharts = (ChartObjects)ws.ChartObjects(Type.Missing);

            myChart = (ChartObject)xlCharts.Add(300, 30, 400, 200); // izq, arriba, ancho , alto
            _chart = myChart.Chart;
            _chart.ChartType = XlChartType.xlLine;


            _chartRange = ws.Range["B4", "C17"]; // rango de celdas que se usan para el grafico

            _chart.HasTitle = true;
            _chart.ChartTitle.Caption = "Resumen de Mantenimientos durante el año: " + _anioBuscar;


            _chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
            _chart.SetSourceData(_chartRange, misValue);

            myChart.Chart.HasLegend = true;
            myChart.Chart.ShowDataLabelsOverMaximum = true;


            
            
        }

        public class ValoresGrid
        {

            public string Nombre { get; set; }
            public decimal Costo1 { get; set; }
            public decimal Costo2 { get; set; }
            public decimal Costo3 { get; set; }
            public decimal Costo4 { get; set; }
            public decimal Costo5 { get; set; }
            public decimal Costo6 { get; set; }
            public decimal Costo7 { get; set; }
            public decimal Costo8 { get; set; }
            public decimal Costo9 { get; set; }
            public decimal Costo10 { get; set; }
            public decimal Costo11 { get; set; }
            public decimal Costo12 { get; set; }
            public decimal Total { get; set; }
        }
        public class ValoresTotales
        {
            public int Mes { get; set; }
            public decimal CostoInvProducto { get; set; } // costo inversion mensual productos
            public decimal CostoInvVehiculo { get; set; } // csto inversion mensual vehiculos
        }

        private void btnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnio.Text))
            {
                MessageBox.Show("Debe ingresar un año determinado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                _anioBuscar = Convert.ToInt32(txtAnio.Text);
                
               
                costos_mante_vh = coreVehiculo.CostoMantenimientoAnioMes(_anioBuscar);
                costos_mante_p = coreProducto.CostoMantenimientoAnioMes(_anioBuscar);
                CalcularCostosTotales();
                GenerarGraficos();
            }
        }

        private void btnExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnio.Text))
            {
                MessageBox.Show("Debe ingresar un año determinado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {


                GenerateExcelFile();
            }
        }
    }
}
