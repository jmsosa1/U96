using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosManteVhAnio.xaml
    /// </summary>
    public partial class ucDatosManteVhAnio : UserControl
    {

        #region Declarativas



        BLLVehiculos coreVh = new BLLVehiculos();
        BLLBase corebase = new BLLBase();
        ObservableCollection<ConsumoAnios> lista_mantevh_anual = new ObservableCollection<ConsumoAnios>(); //observacion: en esta ocacion usamos el campo Anio de la clase ConsumoAnios,
        //como el numero de mes y el campo CostoAnio como el costo total del mantenimiento del mes en cuestion
        List<Monedas> lista_monedas = new List<Monedas>(); // contiene la variacion de una moneda en el tiempo
        public List<ACMVH_CategoriaAnio> resumen_costos_mensuales_catevh = new List<ACMVH_CategoriaAnio>();
        List<Vehiculo> lista_vehiculos = new List<Vehiculo>(); // lista de vehiculos pertenecientes a una categoria determinada y afectada por los mantevh
        CultureInfo usdolar = new CultureInfo("en-US");
        CultureInfo pesosAr = new CultureInfo("es-AR");

        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }
        public decimal[] datosY = new decimal[12];
        public Func<double, string> YFormatter { get; set; }

        int _anioBuscar = 0;
        int _mesBuscar = 0;
        bool _flagdolar;
        decimal _costoTotalAnual = 0;
        decimal _costoPromedio = 0;

        #endregion  

        public ucDatosManteVhAnio()
        {
            InitializeComponent();
            YFormatter = value => value.ToString("C");
            Etiquetas = new[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec" };
            lista_monedas = corebase.ListarVariacionMoneda(DateTime.Today.Year);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            _flagdolar = false;
            _costoTotalAnual = 0;
            _costoPromedio = 0;
            stkMonedas.Visibility = Visibility.Hidden;
            dgResumenCategoria.Visibility = Visibility.Visible;
            dgResumenDominios.Visibility = Visibility.Visible;
            dgResumenVariacionMoneda.ItemsSource = null; // limpiamos la grilla de variacion de moneda 

            lvcCartesiano.Series.Clear();
            values_lineas.Clear();

            for (int i = 0; i < datosY.Length; i++)
            {
                datosY[i] = 0;
            }
            int _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text);
            lista_mantevh_anual = coreVh.ResumenManteVhAnioMesvsMes(_anioBuscar);
            dgResumenMensual.Columns[1].Header = "Costo ($)";
            dgResumenMensual.ItemsSource = lista_mantevh_anual;
            foreach (var item in lista_mantevh_anual)
            {
                _costoTotalAnual = _costoTotalAnual + item.CostoAnio;
            }
            _costoPromedio = _costoTotalAnual / lista_mantevh_anual.Count;

            txtCostoTotal.Text = _costoTotalAnual.ToString("C", pesosAr);
            txbCostoPromedio.Text = _costoPromedio.ToString("C", pesosAr);
            ArmarDatosSerie();
            lvcCartesiano.Series = new SeriesCollection // creamos la serie  de los datos que vamos a mostrar
            {
                new LineSeries{
                    Title="Costo Mensual Pesos" ,
                    Values = values_lineas,  // valores calculados anteriormente
                    DataLabels=true}
            };
            lvcCartesiano.DataContext = this;
        }

        private void ArmarDatosSerie()
        {
            foreach (var item in lista_mantevh_anual)
            {
                switch (item.Anio)
                {
                    case 1:
                        datosY[0] = item.CostoAnio;
                        break;
                    case 2:
                        datosY[1] = item.CostoAnio;
                        break;
                    case 3:
                        datosY[2] = item.CostoAnio;
                        break;
                    case 4:
                        datosY[3] = item.CostoAnio;
                        break;
                    case 5:
                        datosY[4] = item.CostoAnio;
                        break;
                    case 6:
                        datosY[5] = item.CostoAnio;
                        break;
                    case 7:
                        datosY[6] = item.CostoAnio;
                        break;
                    case 8:
                        datosY[7] = item.CostoAnio;
                        break;
                    case 9:
                        datosY[8] = item.CostoAnio;
                        break;
                    case 10:
                        datosY[9] = item.CostoAnio;
                        break;
                    case 11:
                        datosY[10] = item.CostoAnio;
                        break;
                    case 12:
                        datosY[11] = item.CostoAnio;
                        break;
                    default:
                        break;
                }

            }
            for (int i = 0; i < datosY.Length; i++)
            {
                values_lineas.Add(datosY[i]);
            }

        }

        private void btnDolar_Click(object sender, RoutedEventArgs e)
        {
            _costoTotalAnual = 0;
            _costoPromedio = 0;
            _flagdolar = true;
            lvcCartesiano.Series.Clear();
            values_lineas.Clear();
            lista_mantevh_anual.Clear();
            dgResumenMensual.ItemsSource = null;
            stkMonedas.Visibility = Visibility.Visible;
            dgResumenCategoria.ItemsSource = null;
            dgResumenCategoria.Visibility = Visibility.Hidden;
            dgResumenDominios.ItemsSource = null;
            dgResumenDominios.Visibility = Visibility.Hidden;

            for (int i = 0; i < datosY.Length; i++)
            {
                datosY[i] = 0;
            }
            int _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text);
            lista_mantevh_anual = coreVh.ResumenManteVhAnioMesvsMes(_anioBuscar);
            ArmarDatosSerieDolar();


            foreach (var item in lista_mantevh_anual)
            {
                _costoTotalAnual = _costoTotalAnual + item.CostoAnio;

            }
            _costoPromedio = _costoTotalAnual / lista_mantevh_anual.Count;
            txtCostoTotal.Text = _costoTotalAnual.ToString("C", usdolar);
            txbCostoPromedio.Text = _costoPromedio.ToString("C", usdolar);
            txtCostoTotal.Foreground = Brushes.DarkGreen;
            txbCostoPromedio.Foreground = Brushes.DarkGreen;
            dgResumenMensual.ItemsSource = lista_mantevh_anual;
            dgResumenVariacionMoneda.ItemsSource = lista_monedas;
            dgResumenMensual.Columns[1].Header = "Costo (u$s)";
            ArmarDatosSerie();
            lvcCartesiano.Series = new SeriesCollection // creamos la serie  de los datos que vamos a mostrar
            {
                new LineSeries{
                    Title="Costos Mensual Dolares" ,
                    Values = values_lineas,  // valores calculados anteriormente
                    DataLabels=true}
            };

            lvcCartesiano.DataContext = this;
        }

        private void ArmarDatosSerieDolar()
        {
            //la idea en este metodo es recorrer la coleccion actual y modificar el costo mensual con el nuevo valor que surge de 
            //diviidr ese costo por el valor de la moneda en ese mes
            foreach (var item in lista_mantevh_anual)
            {
                foreach (var moneda in lista_monedas)
                {

                    if (moneda.MesValor == item.Anio) // el campo anio es en realidad el numero de mes , para esta ocasion
                    {
                        if (moneda.Valor != 0)
                        {
                            item.CostoAnio = (item.CostoAnio / moneda.Valor);
                        }
                        else
                        {
                            item.CostoAnio = 0;
                        }

                    }

                }
            }
            YFormatter = value => value.ToString("C", usdolar);

        }

        private void dgResumenMensual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_flagdolar)
            {


                _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text);

                ConsumoAnios consumo = dgResumenMensual.SelectedItem as ConsumoAnios;
                if (consumo != null)
                {


                    _mesBuscar = Convert.ToInt32(consumo.Anio);
                    resumen_costos_mensuales_catevh = coreVh.ResumenManteVhCategoriaVhMesAnio(_anioBuscar, _mesBuscar);
                    dgResumenCategoria.ItemsSource = resumen_costos_mensuales_catevh;
                    dgResumenCategoria.DataContext = resumen_costos_mensuales_catevh;
                    //calculamos el nombre del mes
                    BuscarMes(_mesBuscar);
                }
            }
        }

        private void BuscarMes(int mesBuscar)
        {
            switch (mesBuscar)
            {
                case 1:
                    txbMes.Text = "Enero";
                    break;
                case 2:
                    txbMes.Text = "Febrero";
                    break;
                case 3:
                    txbMes.Text = "Marzo";
                    break;
                case 4:
                    txbMes.Text = "Abril";
                    break;
                case 5:
                    txbMes.Text = "Mayo";
                    break;
                case 6:
                    txbMes.Text = "Junio";
                    break;
                case 7:
                    txbMes.Text = "Julio";
                    break;
                case 8:
                    txbMes.Text = "Agosto";
                    break;
                case 9:
                    txbMes.Text = "Septiembre";
                    break;
                case 10:
                    txbMes.Text = "Octubre";
                    break;
                case 11:
                    txbMes.Text = "Noviembre";
                    break;
                case 12:
                    txbMes.Text = "Diciembre";
                    break;

                default:
                    break;
            }
        }

        private void dgResumenCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ACMVH_CategoriaAnio cate = dgResumenCategoria.SelectedItem as ACMVH_CategoriaAnio;
            if (cate != null)
            {


                lista_vehiculos = coreVh.DominiosMesAnioMantenimientos(_anioBuscar, _mesBuscar, cate.IdCateManteVh);
                dgResumenDominios.ItemsSource = lista_vehiculos;
            }
        }
    }
}
