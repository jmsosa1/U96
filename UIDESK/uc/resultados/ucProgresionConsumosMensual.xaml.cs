using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucProgresionConsumosMensual.xaml
    /// </summary>
    public partial class ucProgresionConsumosMensual : UserControl
    {
        List<ACCDetMesAnio> detMesAnio = new List<ACCDetMesAnio>();
        BLLVehiculos coreVh = new BLLVehiculos();
        int _anioSeleccionado;
        decimal _totalKm, _totalHs, _totalLts;
        public SeriesCollection SeriesKm { get; set; }
        public SeriesCollection SeriesHs { get; set; }
        public SeriesCollection SeriesL { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<decimal> values_km = new ChartValues<decimal>();
        public ChartValues<decimal> values_hs = new ChartValues<decimal>();
        public ChartValues<decimal> values_litros = new ChartValues<decimal>();

        public ucProgresionConsumosMensual()
        {
            InitializeComponent();
           
        }

        private void CrearGrafico()
        {
            //limpiamos las listas primero
            values_hs.Clear();
            values_km.Clear();
            values_litros.Clear();
            // definimos las etiquetas del eje x o sea los nombres de los meses
            Labels = new[] { "1", "2", "3", "4", "5","6","7","8","9","10","11","12" };
            //definimos el formato que va a tener el valor representado en el eje Y, en este caso, numeros
            YFormatter = value => value.ToString("N");

            //crear los valores con los que van a trabajar las series
            //recorremos la lista de valores que devuelve el evento clikd del boton buscar
            foreach (var item in detMesAnio)
            {
                values_km.Add( item.KmRegistrados);
                values_hs.Add(item.HsRegistradas);
                values_litros.Add(item.LtsConsumidosMes);
            }

            //creamos las 3 series, una para los km, otra para las hs y otra para los litros
            SeriesKm = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Kilometros",
                    Values = values_km
                }
               
            };
            grafKm.DataContext = this;
            SeriesHs = new SeriesCollection
            {
               
                new LineSeries
                {
                    Title = "Horas",
                    Values = values_hs
                }
              
            };
            grafHoras.DataContext = this;
            SeriesL = new SeriesCollection
            {
               
                new LineSeries
                {
                    Title = "Combustible",
                    Values = values_litros
                }
            };
            grafCombustible.DataContext = this;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnio.Text))
            {
                MessageBox.Show("Debe ingresar un año de busqueda", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                _anioSeleccionado = Convert.ToInt32(txtAnio.Text);
                detMesAnio = coreVh.DetalleConsumosMesPorAnio(_anioSeleccionado);
                dgDetalle.ItemsSource = detMesAnio;
                dgDetalle.DataContext = detMesAnio;
                CalcularTotales();
                CrearGrafico();
            }


        }

        private void CalcularTotales()
        {
            _totalHs = 0;
            _totalKm = 0;
            _totalLts = 0;
            foreach (var item in detMesAnio)
            {
                _totalHs = _totalHs + item.HsRegistradas;
                _totalKm = _totalKm + item.KmRegistrados;
                _totalLts = _totalLts + item.LtsConsumidosMes;
            }
            txtRegistros.Text = detMesAnio.Count.ToString();
            txtTotalKmAcu.Text = _totalKm.ToString("N");
            txtTotalHsAcu.Text = _totalHs.ToString("N");
            txtTotalCombustibles.Text = _totalLts.ToString("N");
            
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
