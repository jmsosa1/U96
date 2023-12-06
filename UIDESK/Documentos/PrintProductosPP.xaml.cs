using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para PrintProductosPP.xaml
    /// </summary>
    public partial class PrintProductosPP : Window
    {
        List<StockProducto> _lista_productos = new List<StockProducto>();
        public PrintProductosPP(List<StockProducto> stockProductos)
        {
            InitializeComponent();
            _lista_productos = stockProductos;
            txbTitulo.Text = "Listado de productos con punto de pedido";
            dgDetalle.ItemsSource = _lista_productos;
            dgDetalle.DataContext = _lista_productos;
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "Documentos Vencidos");
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
