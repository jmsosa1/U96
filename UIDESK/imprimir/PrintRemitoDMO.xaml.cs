using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para PrintRemitoDMO.xaml
    /// </summary>
    public partial class PrintRemitoDMO : Window
    {
        Documento documento = new Documento(); //objeto encabezado
        ObservableCollection<DocumentoDetalle> detalles = new ObservableCollection<DocumentoDetalle>(); // objeto detalle del remito
        BLLRemito coreRemito = new BLLRemito(); // nucleo de operaciones con remitos
        int _iddocu; //varible que contiene el id del documento que queremos imprimir

        public PrintRemitoDMO(int iddocumento)
        {
            InitializeComponent();
            _iddocu = iddocumento;
            documento = coreRemito.BuscarUnMOVPorId(_iddocu);
            detalles = coreRemito.BuscarUnDocDetallePorId(_iddocu);
            dgDetalle.DataContext = detalles;
            dgDetalle.ItemsSource = detalles;
            DataContext = documento;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
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
    }
}
