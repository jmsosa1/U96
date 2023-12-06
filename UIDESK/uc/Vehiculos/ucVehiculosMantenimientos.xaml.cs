using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UIDESK.ABM;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucVehiculosMantenimientos.xaml
    /// </summary>
    public partial class ucVehiculosMantenimientos : UserControl
    {
        #region Declarativas


        BLLProveedor bllProveedor = new BLLProveedor();
        List<Proveedor> listaProveedores = new List<Proveedor>();
        ObservableCollection<Mante_vh> listaMantenimientos = new ObservableCollection<Mante_vh>();
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        CultureInfo ci = new CultureInfo("es-AR");
        DateTime _fechaActual;
        DateTime _fechaDesde;
        public ICollectionView vistaMantes
        {
            get { return CollectionViewSource.GetDefaultView(listaMantenimientos); }
        }

        #endregion

        public ucVehiculosMantenimientos()
        {
            InitializeComponent();

            _fechaDesde = DateTime.Today.AddDays(-30);
            _fechaActual = DateTime.Today.AddDays(1);
            listaMantenimientos = bLLVehiculos.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
            dgVhMantenimientos.DataContext = listaMantenimientos;
            dgVhMantenimientos.ItemsSource = listaMantenimientos;

            dtpDesde.SelectedDate = _fechaDesde;
            dtpHasta.SelectedDate = _fechaActual; ;
            vistaMantes.Filter = new Predicate<object>(filtroProveedor);
            vistaMantes.Filter = new Predicate<object>(filtroDomnio);
            vistaMantes.Filter = new Predicate<object>(filtroDominioExacto);

            decimal _costoTotalMantenimientos = 0;
            foreach (var item in listaMantenimientos)
            {
                _costoTotalMantenimientos = _costoTotalMantenimientos + item.Importe;
            }

            txtRegistros.Text = listaMantenimientos.Count.ToString();
            txtFDesde.Text = "Desde " + _fechaDesde.ToString("d", ci);
            txtFHasta.Text = "Hasta " + _fechaActual.ToString("d", ci);
            txtImporte.Text = _costoTotalMantenimientos.ToString("C", ci);
            vistaMantes.Filter = filtroDomnio;

        }

        #region Filtros


        private bool filtroDominioExacto(object obj)
        {
            Mante_vh _Vh = obj as Mante_vh;
            return _Vh.Dominio == txtBuscar.Text;
        }

        private bool filtroDomnio(object obj)
        {
            //
            Mante_vh _Vh = obj as Mante_vh;
            //buscar un dominio y proveedor
            if (chkFiltroProveedor.IsChecked == true)
            {
                return _Vh.Dominio.Contains(txtBuscar.Text)
                        && _Vh.NombreProve.Contains(txtNombreProveedor.Text);
            }
            else
            {
                //buscar solo dominio
                return _Vh.Dominio.Contains(txtBuscar.Text);
            }


        }

        private bool filtroProveedor(object obj)
        {
            Mante_vh _Vh = obj as Mante_vh;

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                // si no hay nada escrito en el buscador entonces devuelve solo la consulta para el proveedor

                return (_Vh.NombreProve.Contains(txtNombreProveedor.Text)
                    && _Vh.FechaFac >= _fechaDesde && _Vh.FechaFac <= _fechaActual);
            }
            else
            {
                //si se escribio algun dominio y ademas se necesita saber el proveeedor entonces
                return _Vh.Dominio.Contains(txtBuscar.Text)
                       && _Vh.NombreProve.Contains(txtNombreProveedor.Text)
                       && _Vh.FechaFac >= _fechaDesde && _Vh.FechaFac <= _fechaActual;
            }
        }
        #endregion

        #region Botones



        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgVhMantenimientos.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ABMMantenimiento aBMMantenimiento = new ABMMantenimiento();
            if (aBMMantenimiento.ShowDialog() == true)
            {

                listaMantenimientos = bLLVehiculos.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
                dgVhMantenimientos.DataContext = listaMantenimientos;
                dgVhMantenimientos.ItemsSource = listaMantenimientos;
                CalcularResultados();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                vistaMantes.Filter = filtroDomnio;
            }
            else
            {


                vistaMantes.Filter = filtroDominioExacto;
            }
            CalcularResultados();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int fila1 = 0;
            int fila2 = 0;
            //verificamos que se selecciona un registro
            Mante_vh mante_Vh = new Mante_vh();
            mante_Vh = dgVhMantenimientos.SelectedItem as Mante_vh;
            if (mante_Vh == null)
            {
                MessageBox.Show("debe seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
            // primero debemos preguntar si se desea borra el mantenimiento 
            MessageBoxResult result = new MessageBoxResult();
            result = MessageBox.Show("Desea borrar el mantenimiento?", "Aviso", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                fila1 = bLLVehiculos.AnularMantenimiento(mante_Vh.IdManteVh);
                fila2 = bLLVehiculos.AnularDetaMante(mante_Vh.IdManteVh);
                MessageBox.Show("De anulo el mantenimiento", "Aviso", MessageBoxButton.OK);
                listaMantenimientos = bLLVehiculos.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
                dgVhMantenimientos.DataContext = listaMantenimientos;
                dgVhMantenimientos.ItemsSource = listaMantenimientos;
            }
        }

        private void BtnFiltroFechas_Click(object sender, RoutedEventArgs e)
        {
            // si no esta chekeado la casilla del proveedor entonces

            listaMantenimientos = bLLVehiculos.ListarTodosLosMantenimientos(_fechaDesde, _fechaActual);
            dgVhMantenimientos.DataContext = listaMantenimientos;
            dgVhMantenimientos.ItemsSource = listaMantenimientos;
            CalcularResultados();
        }

        private void btnFiltroFacRem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFiltroOC_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Chekbox


        private void chkFiltroProveedor_Checked(object sender, RoutedEventArgs e)
        {
            //activa el filtro por proveedor
            txtNombreProveedor.IsEnabled = true;

        }

        private void chkFiltroProveedor_Unchecked(object sender, RoutedEventArgs e)
        {
            //desactiva el filtro por el proveedor
            txtNombreProveedor.Text = "";
            txtNombreProveedor.IsEnabled = false;
            vistaMantes.Filter = filtroDomnio;
            CalcularResultados();

        }
        #endregion

        #region DatePicker



        private void dtpHasta_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _fechaActual = dtpHasta.SelectedDate.Value;
        }

        private void DtpDesde_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _fechaDesde = dtpDesde.SelectedDate.Value;
        }

        #endregion



        private void dgVhMantenimientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mante_vh mante_ = new Mante_vh();
            mante_ = dgVhMantenimientos.SelectedItem as Mante_vh;
            if (mante_ != null)
            {
                ucDetalleFilaMantenimiento._idmantevh = mante_.IdManteVh;
            }
        }



        private void TxtNombreProveedor_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                vistaMantes.Filter = filtroProveedor;

                dgVhMantenimientos.DataContext = vistaMantes;
                dgVhMantenimientos.ItemsSource = vistaMantes;
                CalcularResultados();
            }
        }




        private void CalcularResultados()
        {
            int _contaop = 0;
            decimal _costo = 0;
            Mante_vh mante_ = new Mante_vh();
            foreach (var item in vistaMantes)
            {
                mante_ = item as Mante_vh;
                _costo = _costo + mante_.Importe;
                _contaop = _contaop + 1;
            }

            txtRegistros.Text = _contaop.ToString();
            txtImporte.Text = _costo.ToString("C", ci);
            txtFDesde.Text = "Desde " + _fechaDesde.ToString("d", ci);
            txtFHasta.Text = "Hasta " + _fechaActual.ToString("d", ci);

        }


    }
}
