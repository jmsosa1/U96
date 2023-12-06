using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para RegistrarMantenimiento.xaml
    /// </summary>
    public partial class RegistrarMantenimiento : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        BLLObras coreObra = new BLLObras();
        BLLProveedor bLLProveedor = new BLLProveedor();
        List<Proveedor> proveedors = new List<Proveedor>();
        Proveedor proveedor = new Proveedor();
        public decimal Importe { get; set; }
        Mante_P m = new Mante_P();

        int _idram_ingresado = 0;

        public RegistrarMantenimiento(Producto producto)
        {
            InitializeComponent();
            DataContext = m;
            Importe = 0;
            stpProducto.DataContext = producto;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //en este lugar debemos hacer algunas cosas
            //1)//validamos primero que esten todos los datos
            bool _valido = ValidarDatosObligatorios();

            if (_valido)
            {
                GrabarDatosEnBD();
                if (m.IdRma != 0)
                {
                    //si se ingreso un idrma se debe cambiar el estado del mismo
                    coreProducto.CambiarEstadoRMAProducto(_idram_ingresado, 7, DateTime.Today.Date, Contexto.CodUser); // estado 7 cumplido
                                                                                                                       //cambiamos la situacion fisica del producto 2 porque ingresa al deposito 
                    coreProducto.ActualizarSituacionFisica(m.IdProducto, 2);
                }
            }
            DialogResult = true;
        }



        #region MetodosPrivados

        private bool ValidarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtIdProve.Text))
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtFactura.Text))
                {
                    MessageBox.Show("Debe indicar un numero de factura o remito del proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else
                {
                    if (dtpFechaFactura.SelectedDate.Value == null)
                    {
                        MessageBox.Show("Debe indicar una fecha de factura o de remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(txtDetalle.Text))
                        {
                            MessageBox.Show("Debe indicar un detalle del mantenimieto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(txtImputacion.Text))
                            {
                                MessageBox.Show("Debe indicar una imputacion de obra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                                return false;
                            }
                            else
                            {

                                return true;
                            }
                        }
                    }
                }
            }
        }

        private void GrabarDatosEnBD()
        {
            //creamos el objeto de mantenimientos
            // Mante_P m = new Mante_P();
            m.AltaF = DateTime.Today;
            m.IdProducto = Convert.ToInt32(txtIdProducto.Text);
            m.IdProve = Convert.ToInt32(txtIdProve.Text);
            m.Imputacion = Convert.ToInt32(txtImputacion.Text);
            m.IdOtm = 0;
            m.NumFacturaProve = txtFactura.Text;
            m.NumRemitoProve = txtRemito.Text;
            m.OCProve = txtOC.Text;
            string _importe = txtImporte.Text;
            m.ImporteFactura = decimal.Parse(_importe.Replace("$", ""));

            m.FechaFactura = dtpFechaFactura.SelectedDate;

            m.DetalleMante = txtDetalle.Text;
            m.IdUsuario = Contexto.CodUser;
            m.IdRma = _idram_ingresado;

            //grabamos los datos en la base de datos
            coreProducto.AltaMantenimiento(m);
        }


        #endregion


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void txtRemito_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRemito.SelectAll();
        }

        private void txtOC_GotFocus(object sender, RoutedEventArgs e)
        {
            txtOC.SelectAll();
        }

        private void txtImporte_GotFocus(object sender, RoutedEventArgs e)
        {
            txtImporte.SelectAll();
        }



        private void txtFactura_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFactura.SelectAll();
        }

        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto desde la seleccion en la lista 
            Proveedor p = lstResultadoBusquedaProve.SelectedItem as Proveedor;
            if (p != null)
            {


                //pasamos los datos dle objeto a los cuadros de texto determinados
                txtIdProve.Text = p.IdProve.ToString();
                txbRazonSocial.Text = p.RazonSocial;
                //cerramos el drawer de seleccion(izquierdo)
                btnCerrarDrawLeft.Command.Execute(Dock.Left);
                txtFactura.Focus();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProve.Text);

            lstResultadoBusquedaProve.ItemsSource = proveedors;
        }

        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtImputacion.Text))
                {
                    MessageBox.Show("Debe ingresar un numero de imputacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    int _imputacion = Convert.ToInt32(txtImputacion.Text);
                    Obra obra = coreObra.BuscarImputacion(_imputacion);
                    if (obra == null)
                    {
                        MessageBox.Show("El numero de imputacion no es correcto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        txbNombreObra.Text = obra.NombreObra;
                        txbClienteObra.Text = obra.Cliente;

                    }
                }
            }
        }

        private void txtIdRma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //validamos el numero de rma que se ingresa
                int _idrma = Convert.ToInt32(txtIdRma.Text);
                bool _valido = coreProducto.ValidarRMA(_idrma);
                if (!_valido)
                {
                    MessageBox.Show("El RMA No existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    _idram_ingresado = _idrma;
                }
            }
        }
    }
}
