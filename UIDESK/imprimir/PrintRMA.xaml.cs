using BLL;
using ENTIDADES;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para PrintRMA.xaml
    /// </summary>
    public partial class PrintRMA : Window
    {
        RMAProducto _rma = new RMAProducto();
        Proveedor _proveedor = new Proveedor();
        Proveedor _transporte = new Proveedor();
        BLLProveedor coreProve = new BLLProveedor();

        public PrintRMA(RMAProducto rMAProducto)
        {

            InitializeComponent();
            _rma = rMAProducto;
            DataContext = _rma;
            _proveedor = coreProve.BuscarPorId(_rma.IdProveedor);
            _transporte = coreProve.BuscarPorId(_rma.Idtransporte);
            txbDireccionProve.Text = _proveedor.Dir1;
            txbTelefono.Text = _proveedor.Tel1;
            txbLocalidad.Text = _proveedor.Localidad;
            txbEmail.Text = _proveedor.Email;
            txbProvincia.Text = _proveedor.Provincia;
            txbNombreTransporte.Text = _transporte.Nombre;

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "RMA");
            }
            DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
