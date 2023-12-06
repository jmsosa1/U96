using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.imprimir;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucDIPDSP.xaml
    /// </summary>
    public partial class ucDIPDSP : UserControl
    {
        /// <summary>
        /// remitos de ingreso de productos por compra y devoluciones de los mismos
        /// </summary>
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Documento> lista_dip = new ObservableCollection<Documento>();
        ObservableCollection<DocumentoDetalle> lista_det_dip = new ObservableCollection<DocumentoDetalle>();


        public ucDIPDSP()
        {
            InitializeComponent();
            lista_dip = coreRemito.RemitosProductosTodos();
            dgDocDipDdp.ItemsSource = lista_dip;
            dgDocDipDdp.DataContext = lista_dip;
        }

        private void dgDocDipDdp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento documento = dgDocDipDdp.SelectedItem as Documento;
            ucDetalleDocumento._iddocumento = documento.IdDocumento;
        }

        private void btnReImprimir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Documento documento = dgDocDipDdp.SelectedItem as Documento;
            if (documento == null)
            {
                MessageBox.Show("Debe seleccionar un documento antes de imprimir", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {


                ImprimirDIP imprimir = new ImprimirDIP(documento.IdDocumento);
                imprimir.Show();
            }

        }
    }
}
