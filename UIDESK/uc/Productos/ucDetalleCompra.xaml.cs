using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleCompra.xaml
    /// </summary>
    public partial class ucDetalleCompra : UserControl
    {
        #region Declarativa
        BLLProducto coreProdcuto = new BLLProducto();
        ObservableCollection<CompraPDetalle> det = new ObservableCollection<CompraPDetalle>();
        public static int _idcompra;
        #endregion

        public ucDetalleCompra()
        {
            InitializeComponent();
            det = coreProdcuto.CompraListarDetalleUna(_idcompra);
            dgDetalleCompra.ItemsSource = det;
            dgDetalleCompra.DataContext = det;
        }
    }
}
