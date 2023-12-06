using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.Remitos;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucCompras.xaml
    /// </summary>
    public partial class ucCompras : UserControl
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<CompraP> lista_general = new ObservableCollection<CompraP>();
        CultureInfo ci = new CultureInfo("es-AR");
        CompraP _compra = new CompraP();
        public ICollectionView vistaCompras
        {
            get { return CollectionViewSource.GetDefaultView(lista_general); }
        }

        #endregion

        public ucCompras()
        {
            InitializeComponent();
            lista_general = coreProducto.ComprasListarTodos();
            dgCompras.ItemsSource = vistaCompras;
            dgCompras.DataContext = vistaCompras;
            CalcularCostoCompras();
        }

        #region filtros

        private bool filtroProveedor(object obj)
        {
            CompraP compraP = obj as CompraP;
            return compraP.NombreProveedor == _compra.NombreProveedor;
        }

        #endregion

        private void CalcularCostoCompras()
        {
            decimal _importeTotal = 0;
            int _cantregistros = lista_general.Count;
            foreach (var item in lista_general)
            {
                _importeTotal += item.ImporteCompra;
            }

            txtRegistros.Text = _cantregistros.ToString();
            txtImporteTotal.Text = _importeTotal.ToString("C", ci);

        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgCompras.SelectedIndex = -1;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ABMCompraP nuevacompra = new ABMCompraP();
            if (nuevacompra.ShowDialog() == true)
            {
                MessageBox.Show("Se registro la nueva compra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNuevoRemito_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCompras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CompraP p = dgCompras.SelectedItem as CompraP;
            if (p != null)
            {
                ucDetalleCompra._idcompra = p.IdCompra;

            }
        }

        private void btnNuevoRemito_Click_1(object sender, RoutedEventArgs e)
        {
            DIP nuevo_dip = new DIP();
            if (nuevo_dip.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego un registro de la compra", "Avisa", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mniAnular_Click(object sender, RoutedEventArgs e)
        {
            CompraP compraP = dgCompras.SelectedItem as CompraP;

            MessageBoxResult result = MessageBox.Show("Desea Borrar el registro de la compra?" + compraP.IdCompra + " Los remitos registrados seran vueltos a el estado anterior", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {

                //recorremos  el detalle  si es que existe y vamos cambiando los estado de los remitos registrados
                ObservableCollection<CompraPDetalle> detalle = coreProducto.CompraListarDetalleUna(compraP.IdCompra);
                if (detalle.Count > 0)
                {
                    //como existe detalle , recorremos la lista
                    foreach (var item in detalle)
                    {
                        coreProducto.AnularRegistradosDIP(item.IdRemito);
                    }
                }
                //borrar la compra
                coreProducto.CompraBorrarUna(compraP.IdCompra);
                MessageBox.Show("Se borro el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                lista_general = coreProducto.ComprasListarTodos();
                dgCompras.ItemsSource = lista_general;
                dgCompras.DataContext = lista_general;
                CalcularCostoCompras();
            }
        }

        private void mniFiltroProveedor_Click(object sender, RoutedEventArgs e)
        {
            _compra = dgCompras.SelectedItem as CompraP;
            vistaCompras.Filter = filtroProveedor;
            CalcularCostoCompras();

        }

        private void mniNoFiltroProve_Click(object sender, RoutedEventArgs e)
        {
            lista_general = coreProducto.ComprasListarTodos();
            dgCompras.ItemsSource = vistaCompras;
            dgCompras.DataContext = vistaCompras;
            CalcularCostoCompras();
        }
    }
}
