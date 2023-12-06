using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para AjustarStock.xaml
    /// </summary>
    public partial class AjustarStock : MaterialWindow 
    {
        BLLProducto coreProducto = new BLLProducto();
        StockProducto stockProducto = new StockProducto();
        List<CausaAjusteStock> lista_causas_aj = new List<CausaAjusteStock>();

        public AjustarStock(StockProducto stkp)
        {
            InitializeComponent();
            stockProducto = stkp;
            txtIdProducto.Text = stockProducto.IdProducto.ToString();
            txtNombre.Text = stockProducto.NombreProducto;
            txtDeposito.Text = stockProducto.Deposito;
            txtStkActual.Text = stockProducto.StkActual.ToString();
            lista_causas_aj = coreProducto.ListaCausaAjusteStock();
            cmbCausaAjuste.ItemsSource = lista_causas_aj;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            //ajustamos el stock
            //validamos primero
            CausaAjusteStock ca = cmbCausaAjuste.SelectedItem as CausaAjusteStock;
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar una cantidad", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                if (ca == null)
                {
                    MessageBox.Show("Debe seleccionar una causa de ajuste", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    //si esta todo bien
                    int usuario = Contexto.CodUser;
                    int producto = stockProducto.IdProducto;
                    int deposito = stockProducto.IdDeposito;
                    int cantidad = Convert.ToInt32(txtCantidad.Text);

                    coreProducto.AjustarStockUnProducto(usuario, producto, cantidad, deposito, ca.IdCausaAjuste);
                    DialogResult = true;
                }
            }


        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
