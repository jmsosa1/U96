using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para AdmMarcaProductos.xaml
    /// </summary>
    public partial class AdmMarcaProductos : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<MarcaProductos> lista_Marcas = new ObservableCollection<MarcaProductos>();
        string _flagOperacion; //  bandera que define la operacion a realizar
        int _idmarca_sel; // bandera que contiene el id de la marca seleccionado

        public ICollectionView vistaMarcas // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(lista_Marcas); }
        }
        public AdmMarcaProductos()
        {
            InitializeComponent();
            lista_Marcas = coreProducto.ListarMarcas();
            dgMarcasProductos.ItemsSource = vistaMarcas;
            dgMarcasProductos.DataContext = vistaMarcas;

        }

        #region Filtros

        private bool filtroMarcas(object obj)
        {
            MarcaProductos marca = obj as MarcaProductos;

            return marca.NombreMarca.Contains(txtBuscar.Text); //  para busquedas de texto se usa la opcion "Contains"
        }
        #endregion
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtNombreMarca.IsEnabled = true;
            btnOk.IsEnabled = true;
            txtNombreMarca.Focus();
            MessageBox.Show("Ingrese la nueva marca en el cuadro de texto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            txtNombreMarca.Text = "Nueva Marca";
            txtNombreMarca.Focus();
            _flagOperacion = "A";

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (_flagOperacion == "A")
            {
                AgregarNuevaMarca();
            }

            if (_flagOperacion == "M")
            {
                MoficarMarcaExistente();
            }

            if (_flagOperacion == "B")
            {
                BajaMarcaExistente();
            }


        }

        private void BajaMarcaExistente()
        {
            //aca procedimientos para baja la nueva marca

            MessageBoxResult boxResult = MessageBox.Show("Desea dar de baja la marca del producto?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (boxResult == MessageBoxResult.Yes)
            {


                if (coreProducto.MarcaProductoBaja(_idmarca_sel))
                {
                    MessageBox.Show("Marca de productos actualizada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo grabar el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                };

            }
        }

        private void MoficarMarcaExistente()
        {
            //procedimiento para modificar una marca existente
            //aca procedimientos para grabar la nueva marca
            if (string.IsNullOrEmpty(txtNombreMarca.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la marca!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                string _nuevomarca = txtNombreMarca.Text;
                if (coreProducto.MarcaProductoModifica(_idmarca_sel, _nuevomarca))
                {
                    MessageBox.Show("Marca de productos actualizada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                };
                txtNombreMarca.Text = "";
                txtNombreMarca.IsEnabled = false;
                btnOk.IsEnabled = false;
                lista_Marcas = coreProducto.ListarMarcas();
                dgMarcasProductos.ItemsSource = lista_Marcas;
                dgMarcasProductos.DataContext = lista_Marcas;

            }
        }

        private void AgregarNuevaMarca()
        {
            //aca procedimientos para grabar la nueva marca
            if (string.IsNullOrEmpty(txtNombreMarca.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la marca!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                string _nuevomarca = txtNombreMarca.Text;
                if (coreProducto.MarcaProductoAlta(_nuevomarca))
                {
                    MessageBox.Show("Marca de productos actualizada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("No se pudo grabar el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                };
                txtNombreMarca.Text = "";
                txtNombreMarca.IsEnabled = false;
                btnOk.IsEnabled = false;
                lista_Marcas = coreProducto.ListarMarcas();
                dgMarcasProductos.ItemsSource = lista_Marcas;
                dgMarcasProductos.DataContext = lista_Marcas;

            }

        }

        private void txtNombreMarca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombreMarca.SelectAll();
        }

        private void btnModi_Click(object sender, RoutedEventArgs e)
        {
            MarcaProductos marca = dgMarcasProductos.SelectedItem as MarcaProductos;
            if (marca != null)
            {
                _idmarca_sel = marca.IdMarca;
                txtNombreMarca.Text = marca.NombreMarca;
                txtNombreMarca.Focus();
                _flagOperacion = "M";
            }

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            MarcaProductos marca = dgMarcasProductos.SelectedItem as MarcaProductos;
            if (marca != null)
            {
                _idmarca_sel = marca.IdMarca;
                _flagOperacion = "B";
            }

        }

        private void dgMarcasProductos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MarcaProductos marca = dgMarcasProductos.SelectedItem as MarcaProductos;

            txtNombreMarca.Text = marca.NombreMarca;
        }

        private void txtBuscar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            vistaMarcas.Filter = filtroMarcas;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
