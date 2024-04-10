using BLL;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Office.Interop.Excel;
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

namespace UIDESK.uc.tablerocostos
{
    /// <summary>
    /// Lógica de interacción para ucProgresionCostosInteranual.xaml
    /// </summary>
    public partial class ucProgresionCostosInteranual : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLProducto coreProducto = new BLLProducto();
        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public decimal _ctCombustible; //variables que almacenan los totales
        private decimal _ctManteVh;
        private decimal _ctCompraVh;
        private decimal _ctCompraP;
        private decimal _ctManteP;
        public decimal _ctTotalMante, _ctTotalCompras;
        public decimal[] _combustibles;
        public decimal[] _mantenimientos;
        public decimal[] _compras;
       // public ChartValues<decimal> _valorCmb = new ChartValues<decimal>();
        //public ChartValues<decimal> _valorMante = new ChartValues<decimal>();
        //public ChartValues<decimal> _valorCompras = new ChartValues<decimal>();
        public List<string> lista_anios = new List<string>();
        //int _anioDesde;
        int _anioHasta;

        public ucProgresionCostosInteranual()
        {
            InitializeComponent();
            txtAnio.Text = DateTime.Today.Year.ToString();
            _anioHasta = Convert.ToInt32(txtAnio.Text);
            CalcularValores(); // primero se calculan los valores
            GenerarGrafico(); // luego el grafico
           
        }

        private void GenerarGrafico()
        {
            //graficoBarras.Series.Clear();
            //lista_anios.Clear();
            for (int i = 2019; i <= _anioHasta; i++)
            {
                lista_anios.Add(i.ToString());

            }
          
            Labels = lista_anios.ToArray();
            Formatter = value => value.ToString("C");
           
            graficoBarras.Series = 
             new LiveCharts.SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Combustibles",
                    Values = new ChartValues<decimal> {_combustibles[0], _combustibles[1], _combustibles[2], _combustibles[3] } // aca van los datos que se obtienen de los procedures
                    //Values = _valorCmb
                },
                new ColumnSeries
                {
                    Title = "Mantenimientos",
                    Values = new ChartValues<decimal> { _mantenimientos[0], _mantenimientos[1], _mantenimientos[2], _mantenimientos[3] }
                    //Values = _valorMante
                },

                new ColumnSeries
                {
                    Title = "Inversiones",
                    Values = new ChartValues<decimal> { _compras[0], _compras[1], _compras[2], _compras[3] }
                    //Values = _valorCompras
                } 
            };

       
            graficoBarras.DataContext = this;
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        public void CalcularValores()
        {
            //limpiamos los arrar de valores del grafico
            //_valorCmb.Clear();
            //_valorCompras.Clear();
            //_valorMante.Clear();
           
            //ponemos a cero las varibles de calculo
            _ctCombustible = 0;
            _ctManteVh = 0;
            _ctCompraVh = 0;
            _ctCompraP = 0;
            _ctManteP = 0;
            _ctTotalCompras = 0;
            _ctTotalMante = 0;

            //inicializamos los arrays
             _combustibles = new decimal[10];
             _mantenimientos = new decimal[10];
            _compras = new decimal[10];
            //mediante un bucle for realizamos los calculos de los valores que necesitamos agregar a los arrays
            int j = 0;
            for (int i = 2019; i <= _anioHasta; i++)
            {
                //aca ejecutamos los metodos que nos devuelven los valores decimales de los costos
                _ctCombustible = coreVh.CostoTotalCombustibleUnAnio(i);
                _ctManteVh = coreVh.CostoTotalMantenimientoUnAnio(i);
                _ctCompraVh = coreVh.CostoTotalComprasUnAnio(i);
                _ctCompraP = coreProducto.CostoTotalComprasUnAnio(i);
                _ctManteP = coreProducto.CostoTotalMantenimientoUnAnio(i);
                _ctTotalCompras = _ctCompraP + _ctCompraVh;
                _ctTotalMante = _ctManteP + _ctManteVh;
                //asigno las posicionies en los arrays
                _combustibles[j] = _ctCombustible;
                _mantenimientos[j] = _ctTotalMante;
                _compras[j] = _ctTotalCompras;
                j++;
                //_valorCmb.Add(_ctCombustible);
                //_valorCompras.Add(_ctTotalCompras);
               // _valorMante.Add(_ctTotalMante);     
            }
        }

        /*
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            //chekemaos que tengan valores los textbox
            if (string.IsNullOrEmpty(txtAnioHasta.Text))
            {
                MessageBox.Show("Faltan datos de los años", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                //pasamos los valores a las variables y chekeamos que el anio desde sea menor que el ano hasta
               // _anioDesde = Convert.ToInt32(txtAnioDesde.Text);
                _anioHasta = Convert.ToInt32(txtAnioHasta.Text);
                if (_anioDesde > _anioHasta)
                {
                    MessageBox.Show("El año desde no puede ser mayor al valor del año hasta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;

                }
                else
                {
                    CalcularValores();
                    GenerarGrafico();
                }
                
            }
        }
        */
        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            ws.Range["A1"].Value = "Resumen InterAnual:2019 - ";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["B1"].Value = 2022;
            ws.Range["B1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A4"].Value = "2019";
            ws.Range["A5"].Value = "2020";
            ws.Range["A6"].Value = "2021";
            ws.Range["A7"].Value = "2022";
            ws.Range["A7"].Value = "2023";
            ws.Range["A7"].Value = "2024";

            ws.Range["B3"].Value = "Combustibles";
            ws.Range["C3"].Value = "Mantenimientos";
            ws.Range["D3"].Value = "Inversiones";
            

            //iteramos en los arrays
            int j = 4;
            for (int i = 0; i < 4; i++)
            {
                ws.Range["B" + j].Value = _combustibles[i];
                ws.Range["C" + j].Value = _mantenimientos[i];
                ws.Range["D" + j].Value = _compras[i];
                j++;
            }
          
            ws.Range["B4", "D6"].NumberFormat = "$0,00";
            ws.Range["A3", "D7"].Borders.LineStyle = XlLineStyle.xlContinuous;
            

            ChartObjects xlCharts = null; // coleccion de graficos de la hoja
            ChartObject myChart = null; // contenedor del objeto chart
            Chart _chart = null; // objeto grafico en el que vamos a trabajar
            //ExcelPieChart pieChart 
            Range _chartRange = null;
            object misValue = System.Reflection.Missing.Value;
            xlCharts = (ChartObjects)ws.ChartObjects(Type.Missing);

            myChart = (ChartObject)xlCharts.Add(250, 50, 400, 250); // izq, arriba, ancho , alto
            _chart = myChart.Chart;
            _chart.ChartType = XlChartType.xlColumnClustered;


            _chartRange = ws.Range["A3","D7"]; // rango de celdas que se usan para el grafico

            _chart.HasTitle = true;
            _chart.ChartTitle.Caption = "Evolucion Costos InterAnual 2019-2024";


            _chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
            _chart.SetSourceData(_chartRange, misValue);

            myChart.Chart.HasLegend = true;
            myChart.Chart.ShowDataLabelsOverMaximum = true;


        }
    }
}
