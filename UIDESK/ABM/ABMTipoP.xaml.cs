using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMTipoP.xaml
    /// </summary>
    public partial class ABMTipoP : MaterialWindow
    {
        #region Declarativa

        BLLProducto coreProducto = new BLLProducto();
        TipoProducto _tipoProducto = new TipoProducto();
        VarPrecioTipoP varPrecio = new VarPrecioTipoP();
        int _operacion = 0;
        #endregion
        public ABMTipoP(TipoProducto tipo , int operacion)
        {
            InitializeComponent();
            _tipoProducto = tipo;
            _operacion = operacion;
            DataContext = _tipoProducto;
            varPrecio = coreProducto.BuscarUltimaVariacionPrecioUnTipoProducto(_tipoProducto.IdTipoP);
            stkDatosPrecio.DataContext = varPrecio;

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreTipo.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ActualizarTipos(_operacion);
            }

        }

        private void ActualizarTipos(int _op)
        {
            if (_op == 1) // alta
            {

            }
            else
            {
                if (_op == 2) // modificacion
                {

                }
                else
                {
                    //baja
                }
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
