using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosConsumoAnio.xaml
    /// </summary>
    public partial class ucDatosConsumoAnio : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        ACCombustible datosConsumo = new ACCombustible();
        List<ACCDetMesAnio> resumen_mensual = new List<ACCDetMesAnio>();
        public Func<ChartPoint, string> PointLabel { get; set; }
        public decimal punto1;
        public decimal punto2;

        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }
        public decimal[] datosY = new decimal[12];
        public Func<double, string> YFormatter { get; set; }
        public ucDatosConsumoAnio()
        {
            InitializeComponent();


            PointLabel = chartpoint => string.Format("{0:C}({1:P})", chartpoint.Y, chartpoint.Participation);
            YFormatter = value => value.ToString("C");
            Etiquetas = new[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec" };

            //lvcCartesiano.DataContext = this;

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            lvcCartesiano.Series.Clear();
            values_lineas.Clear();

            for (int i = 0; i < datosY.Length; i++)
            {
                datosY[i] = 0;
            }

            int _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text); // almacenamos la variable el anio a buscar los datos
            datosConsumo = coreVh.ResumenConsumosCombustiblesAnio(_anioBuscar); //  ejecutamos el metodo que nos trae los datos del resumen anual
            grdDatosConsumoAnio.DataContext = datosConsumo; // asignamos al datacontex de textos 
            punto1 = datosConsumo.CostoHoras; // primer valor del charpie
            punto2 = datosConsumo.CostoKm; // segundo valor del charpie
            resumen_mensual = coreVh.ResumenConsumoMensAnio(_anioBuscar); // ejecutamos el metodo que devuelve el resumen mensual anual de los consumos
            dgResumenMensual.ItemsSource = resumen_mensual; // lista detalle de consumo mensual
            ArmarDatosSerie();
            /* foreach (var item in resumen_mensual) // iteramos en el resultado del metodo anterior
             {
                 values_lineas.Add(item.CCMesAnio); // asignamos el valor a la lista de valores que vamos a representar con el grafico
                 ArmarArrayMeses(item.MesDelAnio); // llamamos al metodo que convierte el numero del mes en el nombre del mes
             }*/

            // Etiquetas = tags_lineas.ToArray(); // asiganmos al array de etiquetas del eje X los nombres calculados anteriormente
            //para que coincida con los valores que calculamos anteriormente

            //creamos la serie que va a representar el grafico chartpie con los datos que ya calculamos
            lvcTorta.Series = new SeriesCollection{
                new PieSeries { Title = "CostoHoras", Values = new ChartValues<decimal> { punto1 } ,DataLabels=true, LabelPoint=PointLabel,FontSize=14},
                new PieSeries { Title="CostoKM", Values = new ChartValues<decimal> { punto2} , DataLabels=true, LabelPoint=PointLabel,FontSize = 14 },


                };

            lvcCartesiano.Series = new SeriesCollection // creamos la serie  de los datos que vamos a mostrar
            {
                new LineSeries{
                    Title="Consumos" ,
                    Values = values_lineas,  // valores calculados anteriormente
                    DataLabels=true}
            };
            // asignamos el nuevo contexto de datos al grafico cartesiano

            lvcCartesiano.DataContext = this;
        }

        private void ArmarArrayMeses(int i)
        {

            switch (i)
            {
                case 1:
                    tags_lineas.Add("Enero");
                    break;
                case 2:
                    tags_lineas.Add("Febrero");
                    break;
                case 3:
                    tags_lineas.Add("Marzo");
                    break;
                case 4:
                    tags_lineas.Add("Abril");
                    break;
                case 5:
                    tags_lineas.Add("Mayo");
                    break;
                case 6:
                    tags_lineas.Add("Junio");
                    break;
                case 7:
                    tags_lineas.Add("Julio");
                    break;
                case 8:
                    tags_lineas.Add("Agosto");
                    break;
                case 9:
                    tags_lineas.Add("Septiembre");
                    break;
                case 10:
                    tags_lineas.Add("Octubre");
                    break;
                case 11:
                    tags_lineas.Add("Noviembre");
                    break;
                case 12:
                    tags_lineas.Add("Diciembre");
                    break;
                default:
                    break;
            }
        }

        private void ArmarDatosSerie()
        {
            foreach (var item in resumen_mensual)
            {
                switch (item.MesDelAnio)
                {
                    case 1:
                        datosY[0] = item.CCMesAnio;
                        break;
                    case 2:
                        datosY[1] = item.CCMesAnio;
                        break;
                    case 3:
                        datosY[2] = item.CCMesAnio;
                        break;
                    case 4:
                        datosY[3] = item.CCMesAnio;
                        break;
                    case 5:
                        datosY[4] = item.CCMesAnio;
                        break;
                    case 6:
                        datosY[5] = item.CCMesAnio;
                        break;
                    case 7:
                        datosY[6] = item.CCMesAnio;
                        break;
                    case 8:
                        datosY[7] = item.CCMesAnio;
                        break;
                    case 9:
                        datosY[8] = item.CCMesAnio;
                        break;
                    case 10:
                        datosY[9] = item.CCMesAnio;
                        break;
                    case 11:
                        datosY[10] = item.CCMesAnio;
                        break;
                    case 12:
                        datosY[11] = item.CCMesAnio;
                        break;
                    default:
                        break;
                }

            }
            for (int i = 0; i < datosY.Length; i++)
            {
                values_lineas.Add(datosY[i]);
            }
            //lvcCartesiano.Series = new SeriesCollection { new ColumnSeries { Title = "Consumos", Values = values_lineas, DataLabels = true } };
            //lvcCartesiano.DataContext = this;
        }
    }
}
