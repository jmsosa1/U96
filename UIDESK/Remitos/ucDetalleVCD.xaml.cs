using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleVCD.xaml
    /// </summary>
    public partial class ucDetalleVCD : UserControl
    {
        public static int _iddocumento;
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<DocumentoDetalle> detalle_doc = new ObservableCollection<DocumentoDetalle>();

        public ucDetalleVCD()
        {
            InitializeComponent();
            detalle_doc = coreRemito.BuscarUnDocDetallePorId(_iddocumento);
            dgDetDSO.ItemsSource = detalle_doc;
            dgDetDSO.DataContext = detalle_doc;
        }
    }
}
