using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenDetalle.xaml
    /// </summary>
    public partial class ucResumenDetalle : UserControl
    {
        #region Declarativas


        BLLObras coreObras = new BLLObras();
        BLLProducto coreProducto = new BLLProducto();
        BLLEmpleados coreEmpleados = new BLLEmpleados();
        BLLVehiculos coreVehiculos = new BLLVehiculos();
        ObservableCollection<BalanceEmpleado> balance = new ObservableCollection<BalanceEmpleado>();
        List<Asignacion_vh> colvehiculos = new List<Asignacion_vh>();
        List<BalanceObraProductosDetalle> productos = new List<BalanceObraProductosDetalle>();
        List<CategoriaP> lista_catp = new List<CategoriaP>();

        int _imputacion;

        public ICollectionView vistaVehiculos
        {
            get { return CollectionViewSource.GetDefaultView(colvehiculos); }
        }

        public ICollectionView vistaProductos
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }
        public ICollectionView vistaEmpleados
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }
        #endregion

        public ucResumenDetalle(int imputacion)
        {
            InitializeComponent();
            _imputacion = imputacion;
            productos = coreObras.BalanceProductosUnaObra(_imputacion);
            dgDetalleProducto.ItemsSource = productos;
            dgDetalleProducto.DataContext = productos;
            dgDetalleEmpleados.ItemsSource = productos;
            dgDetalleEmpleados.DataContext = productos;
            colvehiculos = coreObras.ListarAsignacionesUnaObra(_imputacion);
            lista_catp = coreObras.BalanceCatePUnaObra(_imputacion);
            dgDetalleVh.ItemsSource = colvehiculos;
            dgDetalleVh.DataContext = colvehiculos;
            cmbCategoriaProducto.ItemsSource = lista_catp;

        }



        #region Vistas
        private bool filtroCatePro(object obj)
        {
            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;


            BalanceObraProductosDetalle bal = obj as BalanceObraProductosDetalle;

            return bal.IdCateP == categoriaP.IdCateP;

        }
        #endregion
        private void dgDetalleProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgDetalleVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgDetalleEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aca deberiamos aplicar el filtro por categoria de producto
            vistaProductos.Filter = filtroCatePro;
            dgDetalleProducto.ItemsSource = vistaProductos;
            dgDetalleProducto.DataContext = vistaProductos;

            CategoriaP categoriaP = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {




                int _sum = 0;
                foreach (var item in productos)
                {
                    if (item.IdCateP == categoriaP.IdCateP)
                    {


                        _sum = _sum + item.CantEntregada;
                    }
                }
                txbCTProductos.Text = _sum.ToString();
            }
        }
    }
}
