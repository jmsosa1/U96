using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.uc.Laboratorio;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucCalibraciones.xaml
    /// </summary>
    public partial class ucCalibraciones : UserControl
    {
        #region Declarativas

        BLLLaboratorio coreLab = new BLLLaboratorio();
        BLLProducto coreproducto = new BLLProducto();
        ObservableCollection<CalibracionInstrumento> lista_calibraciones = new ObservableCollection<CalibracionInstrumento>();
        ObservableCollection<Producto> lista_productos = new ObservableCollection<Producto>();
        List<CalibracionInstrumento> _calibracionesInstrumento = new List<CalibracionInstrumento>();

        public ICollectionView vistaProductos
        {
            get { return CollectionViewSource.GetDefaultView(lista_productos); }
        }
        #endregion  

        public ucCalibraciones()
        {
            InitializeComponent();
            lista_productos = coreLab.ListarInstrumentos();
            vistaProductos.Filter = filtroAptos;
            dgInstrumentos.ItemsSource = vistaProductos;
            dgInstrumentos.DataContext = vistaProductos;
            txtRegistros.Text = lista_productos.Count.ToString();
            int _aptos = lista_productos
                         .Where(x => x.EstadoInstrumento == "Apto")
                         .Count();
            txtAptos.Text = _aptos.ToString();
            int _noaptos = lista_productos
                         .Where(x => x.EstadoInstrumento == "No apto")
                         .Count();
            txtNoAptos.Text = _noaptos.ToString();
            int _indeterminados= lista_productos
                         .Where(x => x.EstadoInstrumento == "no indica")
                         .Count();
            txtIndeterminados.Text = _indeterminados.ToString();
        }


        #region Filtros

        private bool filtroBusqueda(object obj)
        {
            Producto p = obj as Producto;
            string _texto = txtBuscar.Text;
            return p.Nombre.Contains(_texto) || p.CodInventario.Contains(_texto) || p.NumSerie.Contains(_texto)
                || p.Modelo.Contains(_texto);
        }

        // nueva vista para mostrar solo los aptos
        private bool filtroAptos( object obj) 
        {
            Producto p = obj as Producto;
            return p.EstadoInstrumento == "Apto";
        }
        #endregion

        private void dgInstrumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto p = dgInstrumentos.SelectedItem as Producto;
            if (p != null)
            {
                ucDetalleFilaProducto._idproducto = p.IdProducto;
                ucDetalleFilaProducto._vistalab = true;
            }
        }

        private void btnCerrarDetalle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dgInstrumentos.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // agregar una nueva calibracio del instrumento seleccionado
            //primero chekeamos que se haya seleccionado un instrumento
            Producto producto = dgInstrumentos.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Debe seleccionar un instrumento ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // si esta seleccionado el instrumento lo pasamos como parametro del formulario de calibracion
                ABMCalibracion aBMCalibracion = new ABMCalibracion(producto);
                if (aBMCalibracion.ShowDialog() == true)
                {
                    MessageBox.Show("Se agrego la calibracion del instrumento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_productos = coreLab.ListarInstrumentos();
                    dgInstrumentos.ItemsSource = vistaProductos;
                    dgInstrumentos.DataContext = vistaProductos;
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            vistaProductos.Filter = filtroBusqueda;
        }



        private void btnFichaControl_Click(object sender, RoutedEventArgs e)
        {
            //primero chekear que se haya seleccionado un producto
            if (dgInstrumentos.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un insrumento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                Producto producto = dgInstrumentos.SelectedItem as Producto;

                _calibracionesInstrumento = coreLab.ListarTodasLasCalibracionesUnInstrumento(producto.IdProducto);
                //chekeamos que la lista no sea nulla
                if (_calibracionesInstrumento.Count > 0)
                {
                    FichaControlCalibraciones fichaControl = new FichaControlCalibraciones(producto);
                    if (fichaControl.ShowDialog() == true)
                    {

                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("No existen calibraciones para el instrumento elegido.Desea agregar una?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        ABMCalibracion aBMCalibracion = new ABMCalibracion(producto);
                        if (aBMCalibracion.ShowDialog() == true)
                        {
                            MessageBox.Show("Se agrego la calibracion del instrumento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                            lista_productos = coreLab.ListarInstrumentos();
                            dgInstrumentos.ItemsSource = vistaProductos;
                            dgInstrumentos.DataContext = vistaProductos;
                        }

                    }


                }



            }
        }
    }
}
