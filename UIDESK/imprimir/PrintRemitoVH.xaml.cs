using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para PrintRemitoVH.xaml
    /// </summary>
    public partial class PrintRemitoVH : Window
    {
        Documento documento = new Documento(); //objeto encabezado
        ObservableCollection<DocumentoDetalle> detalles = new ObservableCollection<DocumentoDetalle>(); // objeto detalle del remito
        List<DocumentoDetalle> detalle_doc_vh = new List<DocumentoDetalle>();
        BLLRemito coreRemito = new BLLRemito(); // nucleo de operaciones con remitos
        int _iddocu; //varible que contiene el id del documento que queremos imprimir

        public PrintRemitoVH(int iddocumento)
        {
            InitializeComponent();
            _iddocu = iddocumento;
            documento = coreRemito.BuscarUnDocumentoPorId(_iddocu);
            detalle_doc_vh = coreRemito.DetalleUnDocumentoREVH(_iddocu);
            dgDetalle.DataContext = detalle_doc_vh;
            dgDetalle.ItemsSource = detalle_doc_vh;
            DataContext = documento;
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "DSO");
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
