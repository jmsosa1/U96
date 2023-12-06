using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para CambiarPuntoPedido.xaml
    /// </summary>
    public partial class CambiarPuntoPedido : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        StockProducto stockProducto = new StockProducto();

        public CambiarPuntoPedido(StockProducto stock)
        {
            InitializeComponent();
            stockProducto = stock;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int stk = stockProducto.IdStk;
            int deposito = stockProducto.IdDeposito;
            decimal cantidad = Convert.ToDecimal(txtPPNuevo.Text);
            coreProducto.ActualizarPuntoPedido(stk, deposito, cantidad);
            DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
