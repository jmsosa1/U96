using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ModiCondicionStock.xaml
    /// </summary>
    public partial class ModiCondicionStock : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        List<SituacionStk> lista_stk = new List<SituacionStk>();
        Producto p_param = new Producto();

        public ModiCondicionStock(StockProducto p)
        {
            InitializeComponent();

            p_param = coreProducto.BuscarDatosUno(p.IdProducto);
            txtSituacionAnterior.Text = p_param.TipoSituacionStk;
            lista_stk = coreProducto.ListarSituacionStk();
            cmbSituacionStk.ItemsSource = lista_stk;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            //actualizar la condicion del stock del producto seleccionado
            SituacionStk sc = cmbSituacionStk.SelectedItem as SituacionStk;
            if (sc != null)
            {
                coreProducto.CambiarSituacionStockcUnProducto(p_param.IdProducto, sc.IdSituacionStk);

                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un valor ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
