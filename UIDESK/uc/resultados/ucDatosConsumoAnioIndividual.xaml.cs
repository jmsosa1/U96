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
    /// Lógica de interacción para ucDatosConsumoAnioIndividual.xaml
    /// </summary>
    public partial class ucDatosConsumoAnioIndividual : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        TotalesyAvg TotalesyAvg = new TotalesyAvg();

        List<ACCDetMesAnio> lista_consumos = new List<ACCDetMesAnio>();
        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }
        public decimal[] datosY = new decimal[12];
        public Func<double, string> YFormatter { get; set; }
        public bool _existeDominio;


        public ucDatosConsumoAnioIndividual()
        {
            InitializeComponent();
            YFormatter = value => value.ToString("C");
            Etiquetas = new[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec" };
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int _anio;
            lvcCartesiano.Series.Clear();
            values_lineas.Clear();
            lista_consumos.Clear();
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
                //con los datos del vehiculo, armamos el grafico
                txbModelo.Text = vehiculo.Modelo;
                txbMArca.Text = vehiculo.NomMarca;
                txbHoras.Text = vehiculo.HorasAcumuladas.ToString("N");
                txbKm.Text = vehiculo.KmAcumulado.ToString("N");
                //con los datos del vehiculo, armamos el grafico
                lista_consumos = coreVh.ResumenConsumoVehiculo(_anio, vehiculo.IdVh);
                ArmarDatosSerie();
                dgResumenMensual.ItemsSource = lista_consumos;
                CalcularTotalesyPromedios(lista_consumos);
                grdTotalesYPromedios.DataContext = TotalesyAvg;
            }
            else
            {
                MessageBox.Show("El dominio ingresado no existe", "Aviso", MessageBoxButton.OK);
                return;
            }


        }

        private void CalcularTotalesyPromedios(List<ACCDetMesAnio> aCCs)
        {
            decimal _totalconsumo = 0;
            decimal _totalkm = 0;
            decimal _totallts = 0;
            decimal _totalhs = 0;
            decimal _avgconsumo = 0;
            decimal _avgkm = 0;
            decimal _avghs = 0;
            decimal _avglts = 0;

            //iteramos en la coleccion para calcular los totales y promedios
            foreach (var item in aCCs)
            {
                _totalconsumo += item.CCMesAnio;
                _totalkm += item.KmRegistrados;
                _totallts += item.LtsConsumidosMes;
                _totalhs += item.HsRegistradas;
            }
            //totales
            TotalesyAvg.CostoTotalConsumo = _totalconsumo;
            TotalesyAvg.TotalKmConsumo = _totalkm;
            TotalesyAvg.TotalHsConsumo = _totalhs;
            TotalesyAvg.TotalLtsConsumo = _totallts;
            //calculamos los promedios
            _avgconsumo = _totalconsumo / aCCs.Count;
            _avgkm = _totalkm / aCCs.Count;
            _avghs = _totalhs / aCCs.Count;
            _avglts = _totallts / aCCs.Count;
            TotalesyAvg.AvgCostoConsumo = _avgconsumo;
            TotalesyAvg.AvgHsConsumo = _avghs;
            TotalesyAvg.AvgKmConsumo = _avgkm;
            TotalesyAvg.AvgLtsConsumo = _avglts;
        }

        private void ArmarDatosSerie()
        {
            foreach (var item in lista_consumos)
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
            lvcCartesiano.Series = new SeriesCollection { new ColumnSeries { Title = "Consumos", Values = values_lineas, DataLabels = true } };
            lvcCartesiano.DataContext = this;
        }



    }
}
