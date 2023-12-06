using MaterialDesignExtensions.Controls;
using System.Windows;
using UIDESK.uc.Productos;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalProductos.xaml
    /// </summary>
    public partial class PrincipalProductos : MaterialWindow
    {
        public PrincipalProductos()
        {
            InitializeComponent();
           
        }


        #region RadioButtons

        
        private void rbProductos_Click(object sender, RoutedEventArgs e)
        {
            ucProductosGeneral uc = new ucProductosGeneral();
            ctc.Content = uc;
        }

        private void rbProveedores_Click(object sender, RoutedEventArgs e)
        {
            ucProveedor uc = new ucProveedor();
            ctc.Content = uc;
        }

        private void rbCompras_Click(object sender, RoutedEventArgs e)
        {
            ucCompras uc = new ucCompras();
            ctc.Content = uc;
        }

        private void rbMantenimientos_Click(object sender, RoutedEventArgs e)
        {
            ucMantenimientos uc = new ucMantenimientos();
            ctc.Content = uc;
        }

        private void rbDocumentos_Click(object sender, RoutedEventArgs e)
        {
            ucDocumentosProductos uc = new ucDocumentosProductos();
            ctc.Content = uc;
        }

        #endregion

        private void rbDeposito_Click(object sender, RoutedEventArgs e)
        {
            ucStock ucStock = new ucStock();
            ctc.Content = ucStock;
        }
    }
}
