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
    /// Lógica de interacción para BajaProducto.xaml
    /// </summary>
    public partial class BajaProducto : MaterialWindow

    {
        BLLProducto coreProducto = new BLLProducto();
        BLLObras coreObra = new BLLObras();
        BLLBase coreBase = new BLLBase();
        int idproducto = 0;
        Producto producto;
        Obra obra = new Obra();
        Deposito deposito = new Deposito();
        List<CausaBaja> lista_causas = new List<CausaBaja>();
        int _destino = 1; // 1 deposito, 2/ obra

        public BajaProducto(int _idproducto)
        {
            InitializeComponent();
            idproducto = _idproducto;
            producto = coreProducto.BuscarDatosUno(idproducto);
            DataContext = producto;
            lista_causas = coreBase.ListaCausasBaja();
            cmbCausaBaja.ItemsSource = lista_causas;
            cmbCausaBaja.DataContext = lista_causas;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //si se decide hacer la caja primero debemos vericar algunas cosas
            bool _valido = ValidarDatos();
            if (_valido)
            {
                string _desc = txtDescripcionCausa.Text;
                int _iduser = Contexto.CodUser;
                CausaBaja causa = cmbCausaBaja.SelectedItem as CausaBaja;
                int _imputacion = 0;
                if (obra != null)
                {
                    _imputacion = obra.Imputacion;
                }

                // si es valido los datos necesarios, entonces seguimos con el procedimiento}
                //1) registrar la baja del producto
                coreProducto.BajaUnProducto(producto.IdProducto, causa.IdCausaBaja, _iduser, _imputacion, _desc, DateTime.Today.Date);

                //2) poner a cero las cantidades de estock
                coreProducto.LiquidarElStockPorBaja(producto.IdProducto, Contexto.CodigoDeposito);
                //3) cambiar el estado del producto
                coreProducto.ProductoCambiarEstado(producto.IdProducto, 5); // idestado=5 es baja
                DialogResult = true;
            }
        }

        private bool ValidarDatos()
        {
            //verificamos que se haya seleccionado una causa
            if (cmbCausaBaja.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una causa de la baja del producto", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
            {
                if (rdObra.IsChecked == true)
                {
                    if (string.IsNullOrEmpty(txtImputacion.Text))
                    {
                        MessageBox.Show("Debe Indicar una imputacion de obra. DE lo contrario, desactive la casilla", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
                return true;
            }
        }

        #region RadioButtons





        #endregion

        #region TextBoxes


        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int _imputacion = Convert.ToInt32(txtImputacion.Text);
                if (_destino == 2)
                {
                    obra = coreObra.BuscarImputacion(_imputacion);
                    txtObraDeposito.Text = obra.Cliente;
                }

            }
        }

        private void rdObra_Unchecked(object sender, RoutedEventArgs e)
        {
            rdObra.IsChecked = false;
        }


        #endregion

        //  
        //                MessageBox.Show("Se dio de baja el producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
