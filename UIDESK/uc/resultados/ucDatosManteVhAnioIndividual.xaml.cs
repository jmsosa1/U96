using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosManteVhAnioIndividual.xaml
    /// </summary>
    public partial class ucDatosManteVhAnioIndividual : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();

        List<ACCDetMesAnio> lista_costos_mes = new List<ACCDetMesAnio>();
        List<ACMVH_CategoriaAnio> lista_costos_cate = new List<ACMVH_CategoriaAnio>();
        List<ConsumoAnios> lista_costos_anuales = new List<ConsumoAnios>();

        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public ChartValues<decimal> values_lineas_cate = new ChartValues<decimal>();
        public ChartValues<decimal> values_costos_anuales = new ChartValues<decimal>();

        public List<string> tags_lineas = new List<string>();
        public List<string> tags_lineas_cate = new List<string>();
        public List<string> tags_costos_anuales = new List<string>();
        public string[] Etiquetas { get; set; }
        public string[] EtiquetasCate { get; set; }
        public string[] EtiquetasAnios { get; set; }

        public decimal[] datosY = new decimal[12];
        public decimal[] datosYAnios = new decimal[5];

        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> YFormatterMes { get; set; }
        public Func<double, string> YFormatterAnios { get; set; }
        public bool _existeDominio;

        public ucDatosManteVhAnioIndividual()
        {
            InitializeComponent();
            YFormatter = value => value.ToString("C");
            YFormatterMes = value => value.ToString("C");
            YFormatterAnios = value => value.ToString("C");
            Etiquetas = new[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec" };
            EtiquetasAnios = new[] {  "2018", "2019", "2020", "2021", "2022" };
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int _anio;
            decimal _costoTotal = 0;
            lvcCartesianoCategorias.Series.Clear();
            values_lineas.Clear();
            lista_costos_cate.Clear();
            lista_costos_mes.Clear();

            for (int i = 0; i < datosY.Length; i++)
            {
                datosY[i] = 0;
            }
            if (string.IsNullOrEmpty(txtDominio.Text) || string.IsNullOrEmpty(txtAnio.Text))//verificamo si se ingreso un dominio y el año
            {
                MessageBox.Show("Faltan datos  del dominio / año correcto", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                _anio = Convert.ToInt32(txtAnio.Text);
            }
            _existeDominio = coreVh.ValidarDominio(txtDominio.Text);
            if (_existeDominio)
            {
                //si el dominio existe , buscamos los datos del vehiculo
                Vehiculo vehiculo = coreVh.VehiculoBuscarUnDominio(txtDominio.Text);
                txbModelo.Text = vehiculo.Modelo;
                txbMArca.Text = vehiculo.NomMarca;
                txbAnioModelo.Text = vehiculo.AnioModelo;
                txbHoras.Text = vehiculo.HorasAcumuladas.ToString("N");
                txbKm.Text = vehiculo.KmAcumulado.ToString("N");
                //con los datos del vehiculo, armamos el grafico

                lista_costos_cate = coreVh.ResumenManteVhCategoriasAnioUnVh(vehiculo.IdVh, _anio);
                foreach (var item in lista_costos_cate)
                {
                    item.CostoPromedioCategoria = item.CostoTotalCategoria / item.CantidadIncidencias;
                    _costoTotal = _costoTotal + item.CostoTotalCategoria;
                }
                txbCostoTotal.Text = _costoTotal.ToString("C");
                lista_costos_mes = coreVh.ResumenManteVhMesAnioUnVh(vehiculo.IdVh, _anio);
                ArmarDatosSerie();

                //cargamos la planilla de costos por categorias
                dgPlanillaCostos.ItemsSource = lista_costos_cate;
                //armamos el grafico de barras de las categorias
                foreach (var item in lista_costos_cate)
                {
                    values_lineas_cate.Add(item.CostoTotalCategoria);
                    tags_lineas_cate.Add(item.NombreCategoria);
                }
                EtiquetasCate = tags_lineas_cate.ToArray();
                lvcCartesianoCategorias.Series = new SeriesCollection
                        {
                            new RowSeries{ Title="Consumos por categoria de vehiculo",Values=values_lineas_cate,DataLabels=true}
                        };
                lvcCartesianoCategorias.DataContext = this;
                //armamos el grafico de barras de la variacion interanual del costo de mantenimiento
                lista_costos_anuales = coreVh.ResumenManteCostosAnualUnVh(vehiculo.IdVh);
                ArmarSerieCostosAnuales();


            }
            else
            {
                MessageBox.Show("El dominio ingresado no existe", "Aviso", MessageBoxButton.OK);
                return;
            }

        }

        private void ArmarSerieCostosAnuales()
        {
            foreach (var item in lista_costos_anuales)
            {
                switch (item.Anio)
                {
                    case 2018:
                        datosYAnios[0] = item.CostoAnio;
                        break;
                    case 2019:
                        datosYAnios[1] = item.CostoAnio;
                        break;
                    case 2020:
                        datosYAnios[2] = item.CostoAnio;
                        break;
                    case 2021:
                        datosYAnios[3] = item.CostoAnio;
                        break;
                    case 2022:
                        datosYAnios[4] = item.CostoAnio;
                        break;
                    default:
                        break;
                }

            }

            for (int i = 0; i < datosYAnios.Length; i++)
            {
                values_costos_anuales.Add(datosYAnios[i]);
            }
            lvcCartesianoAnios.Series = new SeriesCollection
                {
                    new ColumnSeries{Title="Costo Anual Mantenimiento", Values=values_costos_anuales,DataLabels=true, Fill= Brushes.Red}
                };
            lvcCartesianoAnios.DataContext = this;
        }

        private void ArmarDatosSerie()
        {
            foreach (var item in lista_costos_mes)
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
            lvcCartesianoMeses.Series = new SeriesCollection { new LineSeries { Title = "Consumos", Values = values_lineas, DataLabels = true } };
            lvcCartesianoMeses.DataContext = this;
        }

    }
}
