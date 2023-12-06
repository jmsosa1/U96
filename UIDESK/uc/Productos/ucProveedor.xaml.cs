using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucProveedor.xaml
    /// </summary>
    public partial class ucProveedor : UserControl
    {

        #region Declarativas
        BLLProveedor core = new BLLProveedor();
        ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();
        List<RubroProve> rubros = new List<RubroProve>();
        

        public ICollectionView vistaProveedor
        {
            get { return CollectionViewSource.GetDefaultView(lista_proveedores); }
        }


        #endregion

        public ucProveedor()
        {
            InitializeComponent();
            lista_proveedores = core.ProveedorTodos();
            rubros = core.ListarRubros();
            cmbRubroProveedor.ItemsSource = rubros;
            dgGralProveedor.ItemsSource = lista_proveedores;
            dgGralProveedor.DataContext = lista_proveedores;

            

        }

        #region Filtros
        private bool filtroNombreRazon(object obj)
        {
            Proveedor p = obj as Proveedor;
            return p.Nombre.Contains(txtBuscar.Text) || p.RazonSocial.Contains(txtBuscar.Text);
        }
        #endregion


        #region BotonesABM


        private void BtnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            Proveedor p = dgGralProveedor.SelectedItem as Proveedor;
            ABMProveedor nuevo_proveedor = new ABMProveedor(p);
            if (nuevo_proveedor.ShowDialog() == true)
            {
                MessageBox.Show("Se modificaron los datos del proveedor", "Aviso", MessageBoxButton.OK);
                lista_proveedores = core.ProveedorTodos();
                dgGralProveedor.ItemsSource = lista_proveedores;
                dgGralProveedor.DataContext = lista_proveedores;
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor p = null;
            ABMProveedor nuevo_proveedor = new ABMProveedor(p);
            if (nuevo_proveedor.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el nuevo  proveedor", "Aviso", MessageBoxButton.OK);
                lista_proveedores = core.ProveedorTodos();
                dgGralProveedor.ItemsSource = lista_proveedores;
                dgGralProveedor.DataContext = lista_proveedores;
            }
        }

        private void btnModificarLocalidad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            Proveedor pv = dgGralProveedor.SelectedItem as Proveedor;
            if (pv != null)
            {
                MessageBoxResult r = MessageBox.Show("Desea dar de baja el proveedor: " + pv.RazonSocial, " Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    //antes de dar de baja el proveedor revisar y avisar si tienen remitos sin registrar
                    bool _vali = core.ValidarBajaUnProveedor(pv.IdProve);
                    if (_vali)
                    {
                        MessageBox.Show("el proveedor tiene Remitos sin registrar. No se puede dar de baja hasta corregir esto", "Aviso",
                             MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;

                    }
                    else
                    {
                        //baja confirmada 
                        core.BajaProveedor(pv.IdProve);
                        MessageBox.Show("Se dio de baja el proveedor", "Aviso",
                             MessageBoxButton.OK, MessageBoxImage.Information);
                        lista_proveedores = core.ProveedorTodos();
                        dgGralProveedor.ItemsSource = lista_proveedores;
                        dgGralProveedor.DataContext = lista_proveedores;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void btnModificarRubro_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Combobox



        private void cmbRubroProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RubroProve rubro = cmbRubroProveedor.SelectedItem as RubroProve;
            lista_proveedores = core.ProveedorPorRubro(rubro.IdRubro);
            dgGralProveedor.ItemsSource = lista_proveedores;
            dgGralProveedor.DataContext = lista_proveedores;
        }
        #endregion

        #region BotonesBusquedaYClasificacion
        private void mniAddCalificacion_Click(object sender, RoutedEventArgs e)
        {
            Proveedor proveedor = dgGralProveedor.SelectedItem as Proveedor;
            if (proveedor != null)
            {
                CalificacionProveedor cali = new CalificacionProveedor(proveedor);
                if (cali.ShowDialog() == true)
                {
                    MessageBox.Show("Calificacion actualizada", "Aviso", MessageBoxButton.OK);
                    lista_proveedores = core.ProveedorTodos();
                    dgGralProveedor.ItemsSource = lista_proveedores;
                    dgGralProveedor.DataContext = lista_proveedores;
                }
            }

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            vistaProveedor.Filter = filtroNombreRazon;
        }

        private void btnCalificacion_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Datagrid


        private void dgProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }




        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            //dgGralProveedor.SelectedIndex = -1;
        }
        #endregion  


        private void chkCUIT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgGralProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proveedor p = dgGralProveedor.SelectedItem as Proveedor;
            if (p != null)
            {
                ucDetallaFilaProveedor._idproveedor = p.IdProve;
            }
        }

        private void btnDetalleFila_Click(object sender, RoutedEventArgs e)
        {
            Proveedor p = dgGralProveedor.SelectedItem as Proveedor;
            if (p!= null)
            {
                ucDetallaFilaProveedor filaProveedor = new ucDetallaFilaProveedor(p.IdProve);
                ccDrawerDatosProveedor.Content = filaProveedor;
            }
        }

        private void btnVerDetalle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
