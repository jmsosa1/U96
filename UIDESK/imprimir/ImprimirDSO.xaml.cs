using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDSO.xaml
    /// </summary>
    public partial class ImprimirDSO : Window
    {
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>();
        Documento documento = new Documento();

        public ImprimirDSO(int _iddocu)
        {
            InitializeComponent();
            documento = coreRemito.BuscarUnDocumentoPorId(_iddocu);
            documentoDetalles = coreRemito.BuscarUnDocDetallePorId(_iddocu);
            grdEncabezado.DataContext = documento;
            grDetalleProductos.DataContext = documentoDetalles;
            txbNota.Text = documento.NotaRemito;

        }
    }
}
