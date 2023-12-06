using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ModiPosicionProducto.xaml
    /// </summary>
    public partial class ModiPosicionProducto : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        StockProducto stockProducto = new StockProducto();
        public ModiPosicionProducto(StockProducto stk)
        {
            InitializeComponent();
            stockProducto = stk;
            txtColumnaAnterior.Text = stockProducto.Columna;
            txtEstanteAnterior.Text = stockProducto.Estante;
            txtFilaAnterior.Text = stockProducto.Fila;
            txtFrenteAnterior.Text = stockProducto.Frente;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            //datos de la nueva posicion


            if (string.IsNullOrEmpty(txtColumnaNuevo.Text))
            {
                MessageBox.Show("Debe indicar posicion: nueva columna", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (string.IsNullOrEmpty(txtFrenteNuevo.Text))
                {
                    MessageBox.Show("Debe indicar posicion: nuevo frente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtEstanteNuevo.Text))
                    {
                        MessageBox.Show("Debe indicar posicion: nuevo estante", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtFilaNuevo.Text))
                        {
                            MessageBox.Show("Debe indicar posicion:nueva fila", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            //si esta todo bien validado
                            string col = txtColumnaNuevo.Text;
                            string estante = txtEstanteNuevo.Text;
                            string fila = txtFilaNuevo.Text;
                            string frente = txtFrenteNuevo.Text;
                            coreProducto.ActualizarPosicionProducto(stockProducto.IdStk, stockProducto.IdDeposito, frente, col, estante, fila);
                            DialogResult = true;
                        }
                    }
                }
            }



        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
