using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows.Controls;
namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleDocumento.xaml
    /// </summary>
    public partial class ucDetalleDocumento : UserControl
    {
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<DocumentoDetalle> detalle_doc = new ObservableCollection<DocumentoDetalle>();

        public static int _iddocumento;

        public ucDetalleDocumento()
        {
            InitializeComponent();
            detalle_doc = coreRemito.BuscarUnDocDetallePorId(_iddocumento);
            dgDetRemito.ItemsSource = detalle_doc;
            dgDetRemito.DataContext = detalle_doc;
            txtCantidadItems.Text = detalle_doc.Count.ToString();
        }
    }
}
