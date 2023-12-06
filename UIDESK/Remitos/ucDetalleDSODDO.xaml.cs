using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleDSODDO.xaml
    /// </summary>
    public partial class ucDetalleDSODDO : UserControl
    {
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<DocumentoDetalle> detalle_doc = new ObservableCollection<DocumentoDetalle>();

        public static int _iddocumento;

        public ucDetalleDSODDO()
        {
            InitializeComponent();
            detalle_doc = coreRemito.BuscarUnDocDetallePorId(_iddocumento);
            dgDetDSO.ItemsSource = detalle_doc;
            dgDetDSO.DataContext = detalle_doc;
        }
    }
}
