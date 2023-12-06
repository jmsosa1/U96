using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace UIDESK.uc.tablerocostos
{
    /// <summary>
    /// Lógica de interacción para ucCostoInversiones.xaml
    /// </summary>
    public partial class ucCostoInversiones : UserControl
    {
        BLLProducto coreProducto = new BLLProducto();
        BLLVehiculos coreVehiculo = new BLLVehiculos();
        List<CostosGenericos> costos_inv_p = new List<CostosGenericos>(); // costos inversiones productos
        List<CostoCompraVehiculo> costo_inv_v = new List<CostoCompraVehiculo>(); // costos inversiones vehiculos
        ObservableCollection<TipoProducto> tipos_p = new ObservableCollection<TipoProducto>();
        List<CategoriaVh> cate_vh = new List<CategoriaVh>();
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public LiveCharts.SeriesCollection SeriesCollection1 { get; set; }
        public ChartValues<decimal> values_totales_Pro = new ChartValues<decimal>();
        public ChartValues<decimal> values_totales_Vh = new ChartValues<decimal>();
        public ObservableCollection<ValoresTotales> valoresTotales = new ObservableCollection<ValoresTotales>();
        public List<ValoresGrid> lista1 ; // creamos esta lista que contendra los valores a calcular
        public List<CostosGenericos> lista2 ; // esta lista contiene los items filtrados
        public List<ValoresGrid> lista_grid_Vh;
        CultureInfo ci = new CultureInfo("es-AR");
        int _anioBuscar;
        public ucCostoInversiones()
        {
            InitializeComponent();
            _anioBuscar = DateTime.Today.Year;
            txtAnio.Text = _anioBuscar.ToString();
            costo_inv_v = coreVehiculo.CostoComprasCategoriasAnio(_anioBuscar);
            costos_inv_p = coreProducto.CostosComprasTipoProductoAnio(_anioBuscar); // calculamos los costos de inversion
            CalcularCostosTotales();
            CalcularCostosMensualesProductos();
            CalcularCostosMensualesVehiculos();
            GenerarGraficos();
        }

        private void CalcularCostosTotales()
        {
            //este metodo calcula los totales mensuales para productos y vehiculos
            //limpiamos los valores de las series
            values_totales_Pro.Clear();
            values_totales_Vh.Clear();
            valoresTotales.Clear();
            for (int i = 1; i < 13; i++)
            {
                ValoresTotales totales = new ValoresTotales();
                totales.Mes = i;
                totales.CostoInvProducto = costos_inv_p
                                           .Where(x => x.Mes == i)
                                           .Select(x => x.Importe)
                                           .Sum();
                totales.CostoInvVehiculo = costo_inv_v
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

        private void GenerarGraficos()
        {
           
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            YFormatter = value => value.ToString("C");

            SeriesCollection1 = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Inv Productos",
                    Values = values_totales_Pro

                },
                new LineSeries
                {
                    Title = "Inv Vehiculos",
                    Values = values_totales_Vh

                },

            };
            grafTotales.DataContext = this;
        }

        private void CalcularCostosMensualesVehiculos()
        {
            cate_vh = coreVehiculo.VehiculosListarCategoria();
            lista_grid_Vh = new List<ValoresGrid>(); // creamos esta lista que contendra los valores a calcular
            List<CostoCompraVehiculo> lista2 = new List<CostoCompraVehiculo>();
            foreach (var item in cate_vh)
            {
                ValoresGrid valores = new ValoresGrid();
                valores.Nombre = item.NomCate;
                lista2 = costo_inv_v.FindAll(x => x.IdCate == item.IdCateVh); //buscamos todos los costos que corresponden a un tipo
                foreach (var reg in lista2) // recorremos la lista de costos filtrados
                {
                    // para cada registro comprobamos el valor del mes y sumamos su valor a la propiedad correspondiente en la clase 
                    switch (reg.Mes)
                    {
                        case 1:
                            valores.Costo1 = valores.Costo1 + reg.Importe;
                            break;
                        case 2:
                            valores.Costo2 = valores.Costo2 + reg.Importe;
                            break;
                        case 3:
                            valores.Costo3 = valores.Costo3 + reg.Importe;
                            break;
                        case 4:
                            valores.Costo4 = valores.Costo4 + reg.Importe;
                            break;
                        case 5:
                            valores.Costo5 = valores.Costo5 + reg.Importe;
                            break;
                        case 6:
                            valores.Costo6 = valores.Costo6 + reg.Importe;
                            break;
                        case 7:
                            valores.Costo7 = valores.Costo7 + reg.Importe;
                            break;
                        case 8:
                            valores.Costo8 = valores.Costo8 + reg.Importe;
                            break;
                        case 9:
                            valores.Costo9 = valores.Costo9 + reg.Importe;
                            break;
                        case 10:
                            valores.Costo10 = valores.Costo10 + reg.Importe;
                            break;
                        case 11:
                            valores.Costo11 = valores.Costo11 + reg.Importe;
                            break;
                        case 12:
                            valores.Costo12 = valores.Costo12 + reg.Importe;
                            break;

                        default:
                            break;
                    }
                    valores.Total = valores.Total + reg.Importe;

                }
                if (valores.Total > 0 ) // esta validacion es para agregar solo las categorias que tienen costos
                {
                    lista_grid_Vh.Add(valores); // una vez recorrido , lo agregamos a la lista de valores del grid
                }
               
            }
            dgInvVh.ItemsSource = lista_grid_Vh;
            dgInvVh.DataContext = lista_grid_Vh;

        }

        private void CalcularCostosMensualesProductos()
        {
            // como funciona
            tipos_p = coreProducto.ListarTipos(); // listamos todos los tipos de productos
           

            lista1 = new List<ValoresGrid>(); // creamos esta lista que contendra los valores a calcular
            lista2 = new List<CostosGenericos>(); // esta lista contiene los items filtrados
            foreach (var item in tipos_p) // recorremos los tipos de productos
            {
                ValoresGrid valores = new ValoresGrid(); // creamos un nuevo objeto de la clase interna 
                valores.Nombre = item.NomTipo; // asignamos el nombre del tipo de producto que estamos calculando
                lista2 = costos_inv_p.FindAll(x => x.TipoCosto == item.IdTipoP); //buscamos todos los costos que corresponden a un tipo
                foreach (var reg in lista2) // recorremos la lista de costos filtrados
                {
                    // para cada registro comprobamos el valor del mes y sumamos su valor a la propiedad correspondiente en la clase 
                    switch (reg.Mes)
                    {
                        case 1: valores.Costo1 = valores.Costo1 + reg.Importe;
                            break;
                        case 2:
                            valores.Costo2 = valores.Costo2 + reg.Importe;
                            break;
                        case 3:
                            valores.Costo3 = valores.Costo3 + reg.Importe;
                            break;
                        case 4:
                            valores.Costo4 = valores.Costo4 + reg.Importe;
                            break;
                        case 5:
                            valores.Costo5 = valores.Costo5 + reg.Importe;
                            break;
                        case 6:
                            valores.Costo6 = valores.Costo6 + reg.Importe;
                            break;
                        case 7:
                            valores.Costo7 = valores.Costo7 + reg.Importe;
                            break;
                        case 8:
                            valores.Costo8 = valores.Costo8 + reg.Importe;
                            break;
                        case 9:
                            valores.Costo9 = valores.Costo9 + reg.Importe;
                            break;
                        case 10:
                            valores.Costo10 = valores.Costo10 + reg.Importe;
                            break;
                        case 11:
                            valores.Costo11 = valores.Costo11 + reg.Importe;
                            break;
                        case 12:
                            valores.Costo12 = valores.Costo12 + reg.Importe;
                            break;
                        
                        default:
                            break;
                    }
                    valores.Total = valores.Total + reg.Importe;
                    
                }
                lista1.Add(valores); // una vez recorrido , lo agregamos a la lista de valores del grid
            }
            dgInvPro.ItemsSource = lista1; // asignamos la lista al grid
            dgInvPro.DataContext = lista1;
            
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnio.Text))
            {
                MessageBox.Show("Debe ingresar un año determinado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                _anioBuscar = Convert.ToInt32(txtAnio.Text);
                costo_inv_v = coreVehiculo.CostoComprasCategoriasAnio(_anioBuscar);
                costos_inv_p = coreProducto.CostosComprasTipoProductoAnio(_anioBuscar);
                CalcularCostosTotales();
                CalcularCostosMensualesProductos();
                CalcularCostosMensualesVehiculos();
               
                GenerarGraficos();
            }
           
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
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

        private void dgInvPro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            ws.Range["B4"].Value = "Inversiones Productos";
            ws.Range["C4"].Value = "Inversiones Vehiculos";
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
            _chart.ChartTitle.Caption = "Resumen de inversiones durante el año: " + _anioBuscar;


            _chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
            _chart.SetSourceData(_chartRange, misValue);

            myChart.Chart.HasLegend = true;
            myChart.Chart.ShowDataLabelsOverMaximum = true;


            //generamos las otras grillas
            ws.Range["A21"].Value = "Inversiones Productos ";
            ws.Range["A21"].Font.Size = 12;
            ws.Range["A21"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A22"].Value = "Tipo Producto ";
            ws.Range["B22"].Value = "Enero";
            ws.Range["C22"].Value = "Febrero";
            ws.Range["D22"].Value = "Marzo";
            ws.Range["E22"].Value = "Abril";
            ws.Range["F22"].Value = "Mayo";
            ws.Range["G22"].Value = "Junio";
            ws.Range["H22"].Value = "Julio";
            ws.Range["I22"].Value = "Agosto";
            ws.Range["J22"].Value = "Septiembre";
            ws.Range["K22"].Value = "Octubre";
            ws.Range["L22"].Value = "Noviembre";
            ws.Range["M22"].Value = "Diciembre";
            ws.Range["N22"].Value = "Totales";
            ws.Range["A22", "N22"].Borders.LineStyle = XlLineStyle.xlContinuous; // lineas en todas las celdas
            int h = 23; //  indicador de posicion de celda
            foreach (var item in lista1)
            {
                ws.Range["A" + h].Value = item.Nombre;
              
                ws.Range["B" + h].Value = item.Costo1;
                
                ws.Range["C" + h].Value = item.Costo2;
               
                ws.Range["D" + h].Value = item.Costo3;
              
                ws.Range["E" + h].Value = item.Costo4;
               
                ws.Range["F" + h].Value = item.Costo5;
               
                ws.Range["G" + h].Value = item.Costo6;
                
                ws.Range["H" + h].Value = item.Costo7;
               
                ws.Range["I" + h].Value = item.Costo8;
               
                ws.Range["J" + h].Value = item.Costo9;
               
                ws.Range["K" + h].Value = item.Costo10;
               
                ws.Range["L" + h].Value = item.Costo11;
               
                ws.Range["M" + h].Value = item.Costo12;
                
                ws.Range["N" + h].Value = item.Total;
               
                ws.Range["A" + h, "N" + h].NumberFormat = "$0,00"; // lineas en todas las celdas
                ws.Range["A" + h, "N" + h].Borders.LineStyle = XlLineStyle.xlContinuous; // lineas en todas las celdas
                h++; // aumentamos una posicion el indice de celda
            }
            // generamos la grilla de las inversiones de los vehiculos

            ws.Range["A"+h].Value = "Inversiones Vehiculos ";
            ws.Range["A" + h].Font.Size = 12;
            ws.Range["A" + h].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            h++;
            ws.Range["A"+h].Value = "Tipo Producto ";
            ws.Range["B" + h].Value = "Enero";
            ws.Range["C" + h].Value = "Febrero";
            ws.Range["D"+h].Value = "Marzo";
            ws.Range["E"+h].Value = "Abril";
            ws.Range["F"+h].Value = "Mayo";
            ws.Range["G"+h].Value = "Junio";
            ws.Range["H"+h].Value = "Julio";
            ws.Range["I"+h].Value = "Agosto";
            ws.Range["J"+h].Value = "Septiembre";
            ws.Range["K"+h].Value = "Octubre";
            ws.Range["L"+h].Value = "Noviembre";
            ws.Range["M"+h].Value = "Diciembre";
            ws.Range["N"+h].Value = "Totales";
            ws.Range["A"+h, "N"+h].Borders.LineStyle = XlLineStyle.xlContinuous; // lineas en todas las celdas
            h++;
            foreach (var item in lista_grid_Vh)
            {
                ws.Range["A" + h].Value = item.Nombre;

                ws.Range["B" + h].Value = item.Costo1;

                ws.Range["C" + h].Value = item.Costo2;

                ws.Range["D" + h].Value = item.Costo3;

                ws.Range["E" + h].Value = item.Costo4;

                ws.Range["F" + h].Value = item.Costo5;

                ws.Range["G" + h].Value = item.Costo6;

                ws.Range["H" + h].Value = item.Costo7;

                ws.Range["I" + h].Value = item.Costo8;

                ws.Range["J" + h].Value = item.Costo9;

                ws.Range["K" + h].Value = item.Costo10;

                ws.Range["L" + h].Value = item.Costo11;

                ws.Range["M" + h].Value = item.Costo12;

                ws.Range["N" + h].Value = item.Total;

                ws.Range["A" + h, "N" + h].NumberFormat = "$0,00"; // lineas en todas las celdas
                ws.Range["A" + h, "N" + h].Borders.LineStyle = XlLineStyle.xlContinuous; // lineas en todas las celdas
                h++; // aumentamos una posicion el indice de celda
            }
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
    }
}
