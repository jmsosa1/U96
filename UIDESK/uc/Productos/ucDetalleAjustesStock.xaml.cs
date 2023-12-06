using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows.Controls;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleAjustesStock.xaml
    /// </summary>
    public partial class ucDetalleAjustesStock : UserControl
    {
        BLLProducto coreProducto = new BLLProducto();
        List<AjusteStockProducto> lista_ajuste = new List<AjusteStockProducto>();
        public static int _idproducto = 0;

        public ucDetalleAjustesStock()
        {
            InitializeComponent();
            lista_ajuste = coreProducto.ListarAjustesUnProducto(_idproducto);
            dgAjusteStock.ItemsSource = lista_ajuste;
            dgAjusteStock.DataContext = lista_ajuste;
        }
    }
}
