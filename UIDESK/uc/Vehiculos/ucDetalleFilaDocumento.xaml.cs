using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows.Controls;


namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaDocumento.xaml
    /// </summary>
    public partial class ucDetalleFilaDocumento : UserControl
    {
        BLLRemito BLLRemito = new BLLRemito();
        List<DocumentoDetalle> lista_detalle = new List<DocumentoDetalle>();

        public static int _iddocver;

        public ucDetalleFilaDocumento()
        {
            InitializeComponent();

            lista_detalle = BLLRemito.DetalleUnDocumentoREVH(_iddocver);
            dgDetalleDocu.ItemsSource = lista_detalle;
            dgDetalleDocu.DataContext = lista_detalle;


        }
    }
}
