using System.Windows;
using System.Windows.Controls;
using UIDESK.Remitos;


namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucDocumentosProductos.xaml
    /// </summary>
    public partial class ucDocumentosProductos : UserControl
    {

        #region Declarativa



        #endregion
        public ucDocumentosProductos()
        {
            InitializeComponent();

        }





        private void rdBDIP_Checked(object sender, RoutedEventArgs e)
        {
            ucDIPDSP uc = new ucDIPDSP();
            ccDocumentos.Content = uc;

        }

        private void rdbDSI_Checked(object sender, RoutedEventArgs e)
        {
            ucDSIDDI uc = new ucDSIDDI();
            ccDocumentos.Content = uc;
        }

        private void rdbVCD_Checked(object sender, RoutedEventArgs e)
        {
            ucVCD uc = new ucVCD();
            ccDocumentos.Content = uc;
        }

        private void rdbVCE_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdbDSD_Checked(object sender, RoutedEventArgs e)
        {
            ucDSDDDD uc = new ucDSDDDD();
            ccDocumentos.Content = uc;
        }

        private void btnReImprimir_Click(object sender, RoutedEventArgs e)
        {
            

        }

    }
}
