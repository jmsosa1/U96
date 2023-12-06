using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucMantenimientos.xaml
    /// </summary>
    public partial class ucMantenimientos : UserControl
    {
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Mante_P> lista_mante = new ObservableCollection<Mante_P>();
        CultureInfo ci = new CultureInfo("es-AR");
        DateTime _fechaActual;
        DateTime _fechaDesde;

        public ucMantenimientos()
        {
            InitializeComponent();
            _fechaDesde = DateTime.Today.AddDays(-30);
            _fechaActual = DateTime.Today.AddDays(1);
            lista_mante = coreProducto.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
            dgMantenimientos.ItemsSource = lista_mante;
            dgMantenimientos.DataContext = lista_mante;
            txtRegistros.Text = lista_mante.Count.ToString();
            decimal _costoTotal = 0;
            foreach (var item in lista_mante)
            {
                _costoTotal = _costoTotal + item.ImporteFactura;
            }
            txtTotalCostoMante.Text = _costoTotal.ToString("C", ci);

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtpHasta_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _fechaActual = dtpHasta.SelectedDate.Value;
        }

        private void DtpDesde_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _fechaDesde = dtpDesde.SelectedDate.Value;
        }

        private void BtnFiltroFechas_Click(object sender, RoutedEventArgs e)
        {
            lista_mante = coreProducto.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
            dgMantenimientos.ItemsSource = lista_mante;
            dgMantenimientos.DataContext = lista_mante;
            txtRegistros.Text = lista_mante.Count.ToString();
            decimal _costoTotal = 0;
            foreach (var item in lista_mante)
            {
                _costoTotal = _costoTotal + item.ImporteFactura;
            }
            txtTotalCostoMante.Text = _costoTotal.ToString("C", ci);
        }
    }
}
