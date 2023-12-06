using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucProductosBajas.xaml
    /// </summary>
    public partial class ucProductosBajas : UserControl
    {
        BLLProducto coreproducto = new BLLProducto();
        ObservableCollection<Producto> lista_productos = new ObservableCollection<Producto>();

        public ucProductosBajas()
        {
            InitializeComponent();
            lista_productos = coreproducto.ListarTodosBajas();

            dgProductos.ItemsSource = lista_productos;
            dgProductos.DataContext = lista_productos;
            txtRegistros.Text = lista_productos.Count.ToString();
        }

        private void DgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto p = dgProductos.SelectedItem as Producto;
            if (p != null)
            {
                ucDetalleFilaProducto._idproducto = p.IdProducto;
                ucDetalleFilaProducto._vistalab = false;
            }
        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgProductos.SelectedIndex = -1;
        }
    }
}
