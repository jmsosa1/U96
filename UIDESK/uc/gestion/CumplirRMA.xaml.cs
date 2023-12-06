using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para CumplirRMA.xaml
    /// </summary>
    public partial class CumplirRMA : MaterialWindow 
    {
        BLLProducto coreProducto = new BLLProducto();
        RMAProducto _rma = new RMAProducto();
        public CumplirRMA(RMAProducto rMAProducto)
        {
            InitializeComponent();
            _rma = rMAProducto;
            DataContext = _rma;
            dtpFecha.SelectedDate = DateTime.Today.Date;
            txtUsuario.Text = Contexto.Nomuser;
            txtIdUsuario.Text = Contexto.CodUser.ToString();
        }

        //private void btnCancelar_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult r = MessageBox.Show("Desea Cancelar la Operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (r == MessageBoxResult.Yes)
        //    {
        //        this.Close();
        //        DialogResult = false;
        //    }
        //}

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {// en este metodo debemos solamente cambiar el estado del rma


            coreProducto.CambiarEstadoRMAProducto(_rma.IdRma, 7, dtpFecha.SelectedDate.Value.Date, Contexto.CodUser); // estado = 7 "cumplido"
            //debemos cambiar la situacion fisica del producto
            coreProducto.ActualizarSituacionFisica(_rma.IdProducto, 2);
            DialogResult = true;

        }

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult r = MessageBox.Show("Desea Cancelar la Operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (r == MessageBoxResult.Yes)
        //    {

        //        DialogResult = false;
        //        this.Close();
        //    }
        //}

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
