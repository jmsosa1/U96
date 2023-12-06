using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.tablerocostos
{
    /// <summary>
    /// Lógica de interacción para ucProgresionCostosMensual.xaml
    /// </summary>
    public partial class ucProgresionCostosMensual : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLProducto coreProducto = new BLLProducto();
        public LiveCharts.SeriesCollection SeriesCollection1 { get; set; }
        public LiveCharts.SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<decimal> values_cmb = new ChartValues<decimal>();
        public ChartValues<decimal> values_mante = new ChartValues<decimal>();
        public ChartValues<decimal> values_inv = new ChartValues<decimal>();
        public ChartValues<decimal> values_totales = new ChartValues<decimal>();
        public List<CostosGenericos> _costosCombustiblesTotales = new List<CostosGenericos>();
        public List<CostosGenericos> _costoMantenimientosTotales = new List<CostosGenericos>();
        public List<CostosGenericos> _costosComprasTotales = new List<CostosGenericos>();
        public List<CostosGenericos> _costoManteVh = new List<CostosGenericos>();
        public List<CostosGenericos> _costosComprasVh = new List<CostosGenericos>();
        public List<CostosGenericos> _costoManteP = new List<CostosGenericos>();
        public List<CostosGenericos> _costosComprasP = new List<CostosGenericos>();
        public List<ValoresGrid> lista = new List<ValoresGrid>();
        int _anioBuscar = 2021;
        
        public ucProgresionCostosMensual()
        {
            InitializeComponent();
            txtAnio.Text = _anioBuscar.ToString();
            CalcularValoresMedios();
            CalcularValoresTotales();
            GenerarGraficoTotal();
            GenerarGraficoMedio();
        }

        private void GenerarGraficoMedio()
        {
            

            Labels = new[] { "1", "2", "3", "4", "5","6","7","8","9","10","11","12" };
            YFormatter = value => value.ToString("C");

            SeriesCollection2 = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Combustibles",
                    Values =values_cmb
                },
                new LineSeries
                {
                    Title = "Mantenimientos",
                    Values = values_mante

                },
                new LineSeries
                {
                    Title = "Inversiones",
                    Values = values_inv

                }
            };
            grafMedios.DataContext = this;
        }

        private void CalcularValoresTotales()
        {
            decimal _costovh = 0;
            decimal _costop = 0;
            // mantenimientos
            for (int i = 1; i < 13; i++)
            {
                CostosGenericos costos = new CostosGenericos();
                costos.Anio = _anioBuscar;
                costos.Mes = i;
                costos.Moneda = "$";
                costos.TipoCosto = 2;
                _costovh = _costoManteVh
                    .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
                _costop = _costoManteP
                    .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
                //costos.Importe = _costoManteVh.FirstOrDefault(x => x.Mes == i).Importe + _costoManteP.FirstOrDefault(x => x.Mes == i).Importe;
                costos.Importe = _costop + _costovh;
                _costoMantenimientosTotales.Add(costos);
            }
            //compras
             _costovh = 0;
             _costop = 0;

            for (int i = 1; i < 13; i++)
            {
                CostosGenericos costos = new CostosGenericos();
                costos.Anio = _anioBuscar;
                costos.Mes = i;
                costos.Moneda = "$";
                costos.TipoCosto = 3;
                _costovh = _costosComprasVh
                   .Where(x => x.Mes == i)
                   .Select(x => x.Importe)
                   .FirstOrDefault();
                _costop = _costosComprasP
                    .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
                //costos.Importe = _costosComprasVh.FirstOrDefault(x => x.Mes == i).Importe + _costosComprasP.FirstOrDefault(x => x.Mes == i).Importe;
                costos.Importe = _costop + _costovh;
                _costosComprasTotales.Add(costos);
            }
            //Combustibles
            _costosCombustiblesTotales = coreVh.CostoCombustibleAnioMes(_anioBuscar);
            //totales para grid 
            //List<ValoresGrid> lista = new List<ValoresGrid>();
            lista = new List<ValoresGrid>();
            decimal _costoT1, _costoT2, _costoT3 = 0;
            for (int i = 1; i < 13; i++)
            {
                ValoresGrid valores = new ValoresGrid();
                valores.Mes = i;
                _costoT1 = _costosCombustiblesTotales
                     .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
               _costoT2 = _costoMantenimientosTotales
                     .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
                _costoT3 = _costosComprasTotales
                     .Where(x => x.Mes == i)
                    .Select(x => x.Importe)
                    .FirstOrDefault();
                valores.TotalCombustibles = _costoT1;
                valores.TotalMante = _costoT2;
                valores.TotalCompra = _costoT3;
                valores.CostoTotal = _costoT1 + _costoT2 + _costoT3;
                lista.Add(valores);
            }

            dgPlanillaCostos.ItemsSource = lista;
            dgPlanillaCostos.DataContext = lista;
            values_totales.Clear();
            values_cmb.Clear();
            values_inv.Clear();
            values_mante.Clear();
            foreach (var item in lista)
            {
                if (item.CostoTotal != 0) // esta condicion es para evitar valores que sean cero y afecten la grafica
                {
                    values_totales.Add(item.CostoTotal);
                }
                if (item.TotalCombustibles!=0)
                {
                    values_cmb.Add(item.TotalCombustibles);
                }
                if (item.TotalMante!=0)
                {
                    values_mante.Add(item.TotalMante);
                }
                if (item.TotalCompra!=0)
                {
                    values_inv.Add(item.TotalCompra);
                }
            }
        }

        private void GenerarGraficoTotal()
        {
            Labels = new[] { "1", "2", "3", "4", "5","6","7","8","9","10","11","12" };
            YFormatter = value => value.ToString("C");

            SeriesCollection1 = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Costos Totales",
                    Values = values_totales,
                   
                }
               
            };
            grafTotales.DataContext = this;
        }

        private void CalcularValoresMedios()
        {
            //caculo de listas de valores para los combustibles, los mantenimientos , las compras
            // totales sin discriminar entre un tipo y otro 
            
            //Mantenimientos
            _costoManteVh = coreVh.CostoMantenimientoAnioMes(_anioBuscar);
            _costoManteP = coreProducto.CostoMantenimientoAnioMes(_anioBuscar);
            //Compras
            _costosComprasVh = coreVh.CostoComprasAnioMes(_anioBuscar);
            _costosComprasP = coreProducto.CostoComprasAnioMes(_anioBuscar);

            // para calcular los totales 
           
        }

       
        public class ValoresGrid
        {
            public int Mes { get; set; }
            public decimal TotalCombustibles { get; set; }
            public decimal TotalMante { get; set; }
            public decimal TotalCompra { get; set; }
            public decimal CostoTotal { get; set; }
        }

        private void dgPlanillaCostos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnio.Text))
            {
                MessageBox.Show("Debe ingresar un anio", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                _anioBuscar = Convert.ToInt32(txtAnio.Text);
                if (_anioBuscar < 2019)
                {
                    MessageBox.Show("El anio debe ser mayor o igual a 2019", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }else
                {
                    //calculamos todo para el nuevo anio
                    CalcularValoresMedios();
                    CalcularValoresTotales();
                    GenerarGraficoTotal();
                    GenerarGraficoMedio();
                }
            }
            
        }


        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            ws.Range["A1"].Value = "Planilla de Costos Mensuales ";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A2"].Value = "Año :";
            ws.Range["B2"].Value = _anioBuscar;
            ws.Range["A2","B2"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A4"].Value = "MES";
            ws.Range["B4"].Value = "Combustibles";
            ws.Range["C4"].Value = "Mantenimientos";
            ws.Range["D4"].Value = "Inversiones";
            ws.Range["E4"].Value = "Total";



            //iteramos en los arrays
            decimal _sumcmb =0, _sumMante=0, _sumCompra=0, _sumTotal = 0;
            int j = 5;
            foreach (var item in lista)
            {
                ws.Range["A" + j].Value = item.Mes;

                ws.Range["B" + j].Value = item.TotalCombustibles;
                _sumcmb = _sumcmb + item.TotalCombustibles;

                ws.Range["C" + j].Value = item.TotalMante;
                _sumMante = _sumMante + item.TotalMante;

                ws.Range["D" + j].Value = item.TotalCompra;
                _sumCompra = _sumCompra + item.TotalCompra;

                ws.Range["E" + j].Value = item.CostoTotal;
                _sumTotal = _sumTotal + item.CostoTotal;
                j++;
            }
            ws.Range["B17"].Value = _sumcmb;
           
            ws.Range["C17"].Value = _sumMante;
            
            ws.Range["D17"].Value = _sumCompra;
           
            ws.Range["E17"].Value = _sumTotal;
           
            ws.Range["B17", "E17"].NumberFormat = "$0,00";
            ws.Range["B17", "E17"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
            ws.Range["B17", "E17"].Font.Bold=true;

            ws.Range["B5", "E16"].NumberFormat = "$0,00";
            ws.Range["A4", "E16"].Borders.LineStyle = XlLineStyle.xlContinuous;
            ws.Range["E5", "E16"].Font.Bold = true;
            ws.Range["B4"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
            ws.Range["C4"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
            ws.Range["D4"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            ws.Range["E4"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);


            ChartObjects xlCharts = null; // coleccion de graficos de la hoja
            ChartObject myChart = null; // contenedor del objeto chart
            Chart _chart = null; // objeto grafico en el que vamos a trabajar
            //ExcelPieChart pieChart 
            Range _chartRange = null;
            object misValue = System.Reflection.Missing.Value;
            xlCharts = (ChartObjects)ws.ChartObjects(Type.Missing);

            myChart = (ChartObject)xlCharts.Add(350, 100, 500, 300); // izq, arriba, ancho , alto
            _chart = myChart.Chart;
            _chart.ChartType = XlChartType.xlLine;


            _chartRange = ws.Range["B4", "E16"]; // rango de celdas que se usan para el grafico

            _chart.HasTitle = true;
            _chart.ChartTitle.Caption = "Evolucion de Costos Mensuales durante el año: " + _anioBuscar;


            _chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
            _chart.SetSourceData(_chartRange, misValue);

            myChart.Chart.HasLegend = true;
            myChart.Chart.ShowDataLabelsOverMaximum = true;


        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }
    }
}
