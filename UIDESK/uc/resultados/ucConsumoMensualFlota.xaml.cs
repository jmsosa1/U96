using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucConsumoMensualFlota.xaml
    /// </summary>
    public partial class ucConsumoMensualFlota : UserControl
    {
        List<ConsumoVhMes> consumoVhMes = new List<ConsumoVhMes>();

        ACCDetMesAnio totalesMesAnio = new ACCDetMesAnio();
        BLLVehiculos coreVh = new BLLVehiculos();

        int _mesSeleccionado;
        int _anioSeleccionado;
        decimal _totalKm, _totalHs;


        public ucConsumoMensualFlota()
        {
            InitializeComponent();



        }



        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            _mesSeleccionado = dtpFechaSeleccionada.SelectedDate.Value.Month;
            _anioSeleccionado = dtpFechaSeleccionada.SelectedDate.Value.Year;

            consumoVhMes = coreVh.ListaDominiosConsumosMes(_mesSeleccionado, _anioSeleccionado);
            dgResultado.ItemsSource = consumoVhMes;
            dgResultado.DataContext = consumoVhMes;

            totalesMesAnio = coreVh.DetalleConsumoUnMesUnAnio(_anioSeleccionado, _mesSeleccionado);
            //txbmes.Text = _mesSeleccionado;
            //txbanio.Text = _anioSeleccionado.ToString();
            CalcularTotalesConsumos();


        }

        private void CalcularTotalesConsumos()
        {
            txtRegistros.Text = consumoVhMes.Count.ToString();
            foreach (var item in consumoVhMes)
            {
                _totalKm = _totalKm + item.KmRecorrido;
                _totalHs = _totalHs + item.HsTrabajo;
            }

            txtTotalKmAcu.Text = _totalKm.ToString("N2");
            txtTotalHsAcu.Text = _totalHs.ToString("N2");
            txtTotaLitros.Text = totalesMesAnio.LtsConsumidosMes.ToString("N2");

        }


        // string _mesSeleccionado = ((ComboBoxItem)cmbTipoMante.SelectedItem).Content.ToString();  
    }
}
