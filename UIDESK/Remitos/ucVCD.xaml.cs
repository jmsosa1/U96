using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucVCD.xaml
    /// </summary>
    public partial class ucVCD : UserControl
    {
        #region Declarativa
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Documento> lista_vcd = new ObservableCollection<Documento>();

        #endregion
        public ucVCD()
        {
            InitializeComponent();
            lista_vcd = coreRemito.ListarVCP();
            dgVCD.ItemsSource = lista_vcd;
            dgVCD.DataContext = lista_vcd;

        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReImprimir_Click(object sender, RoutedEventArgs e)
        {
            Documento documento = dgVCD.SelectedItem as Documento;
            if (documento != null)
            {
                //ImprimirDSI imprimir = new ImprimirDSI(documento.IdDocumento);
                //imprimir.Show();

            }
        }

        private void dgVCD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento d = dgVCD.SelectedItem as Documento;
            if (d != null)
            {
                ucDetalleVCD._iddocumento = d.IdDocumento;
            }
        }
    }
}
