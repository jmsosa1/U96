using System.Windows.Controls;
using ENTIDADES;
using BLL;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucDetallaFilaProveedor.xaml
    /// </summary>
    public partial class ucDetallaFilaProveedor : UserControl
    {
        BLLProveedor bLL = new BLLProveedor();
        List<CompraP> lista_compras = new List<CompraP>();
        CultureInfo ci = new CultureInfo("es-Ar");
        decimal TotalCompras = 0;
       static public int _idproveedor;
        public ucDetallaFilaProveedor( int idproveedor)
        {
            InitializeComponent();
            _idproveedor = idproveedor;
            Proveedor prove = bLL.BuscarPorId(_idproveedor);
            lista_compras = bLL.ProveedorListaCompras(_idproveedor);
            TotalCompras= CalcularImporteTotalCompras();
            txbTotalCompras.Text = TotalCompras.ToString("C", ci);
            dgCompras.ItemsSource = lista_compras;
            dgCompras.DataContext = lista_compras;
            DataContext = prove;
        }

        private decimal CalcularImporteTotalCompras()
        {
            decimal _importe = 0;
            foreach (var item in lista_compras)
            {
                _importe = item.ImporteCompra + _importe;
            }
            return _importe;
        }
    }
}
