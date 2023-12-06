using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using UIDESK.Documentos;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ListaProductosPP.xaml
    /// </summary>
    public partial class ListaProductosPP : Window
    {
        List<StockProducto> _lista_productos = new List<StockProducto>();
        CultureInfo cultureInfo = new CultureInfo("es-Ar");

        public ListaProductosPP(List<StockProducto> stockProductos )
        {
            InitializeComponent();
            _lista_productos = stockProductos;
            dgProductosPP.ItemsSource = _lista_productos;
            dgProductosPP.DataContext = _lista_productos;
            txbCantidad.Text = _lista_productos.Count.ToString();
            CalcularCostosPP();

        }

        private void CalcularCostosPP()
        {
            decimal _costo = 0;
            foreach (var item in _lista_productos)
            {
                _costo = _costo + (item.CostoStk * item.StkActual);
            }
            txbCosto.Text = _costo.ToString("C",cultureInfo);
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintProductosPP _print = new PrintProductosPP(_lista_productos);
            if (_print.ShowDialog()== true)
            {
                MessageBox.Show("Se imprimio el documento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

       
    }
}
