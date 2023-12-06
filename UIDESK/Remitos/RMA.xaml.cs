using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UIDESK.imprimir;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para RMA.xaml
    /// </summary>
    public partial class RMA : MaterialWindow
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        BLLProveedor bLLProveedor = new BLLProveedor();
        BLLRemito coreRemito = new BLLRemito();
        BLLObras coreObra = new BLLObras();
        List<Proveedor> proveedors = new List<Proveedor>();
        ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>();
        Producto pro_rma = new Producto();

        #endregion

        public RMA(Producto producto)
        {
            InitializeComponent();
            pro_rma = producto;
            DataContext = pro_rma;
            transportes = bLLProveedor.ProveedorPorRubro(9); // proveedores de transportes
            cmbTransporte.ItemsSource = transportes;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarBasicos())
            {
                //esta todo bien, grabamos el RMA 
                GrabarEnBD();

                //preguntamos si quieren imprimir el rma
                // obtenemos el ultimo iddocumento 
                int _ultimoIddocumento = coreProducto.ObtenerUltimoIdRMA();
                MessageBoxResult re = MessageBox.Show("Se grabo el registro . Desea Imprimir? ", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (re == MessageBoxResult.Yes)
                {
                    RMAProducto rMA = coreProducto.BuscarUnRMAPorId(_ultimoIddocumento);
                    PrintRMA printRMA = new PrintRMA(rMA);
                    if (printRMA.ShowDialog() == true)
                    {
                        MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //ImprimirRMA imprimir = new ImprimirRMA(_ultimoIddocumento);
                    //imprimir.Show();
                }
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("hay datos que faltan o erroneos . Por favor corregir antes de seguir", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProve.Text);

            lstResultadoBusquedaProve.ItemsSource = proveedors;
        }

        private void btnSeleccionarProveedor_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto desde la seleccion en la lista 
            Proveedor p = lstResultadoBusquedaProve.SelectedItem as Proveedor;
            if (p != null)
            {
                Proveedor pv = bLLProveedor.BuscarPorId(p.IdProve);
                //pasamos los datos dle objeto a los cuadros de texto determinados
                txtIDProveedor.Text = pv.IdProve.ToString();
                txtNombreProve.Text = pv.RazonSocial;
                txtDireccion.Text = pv.Dir1;
                txtLocalidad.Text = pv.Localidad;
                txtProvincia.Text = pv.Provincia;
                txtTelefono1.Text = pv.Tel1;
                txtMailProveedor.Text = pv.Email;
                //cerramos el drawer de seleccion(izquierdo)
                btnCerrarDrawLeft.Command.Execute(Dock.Left);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void txtBuscarProve_TextChanged(object sender, TextChangedEventArgs e)
        {
            //nada
        }

        #region MetodosPrivados
        public bool ValidarBasicos()
        {
            //validamos que se haya elegido un proveedor y que exista una descripcion de la falla
            if (string.IsNullOrWhiteSpace(txtIDProveedor.Text))
            {
                MessageBox.Show("Debe seleccionar un proveedor ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtCausaRMA.Text))
                {
                    MessageBox.Show("Debe indicar una causa del problema ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else
                {
                    if (cmbTransporte.SelectedItem == null)
                    {
                        MessageBox.Show("Debe indicar un transporte", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(txtImputacion.Text))
                        {
                            MessageBox.Show("Debe indicar  una imputacion ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        private void GrabarEnBD()
        {
            //grabamos en la base de datos el registro
            Proveedor transporte = cmbTransporte.SelectedItem as Proveedor;
            RMAProducto nuevor = new RMAProducto();
            nuevor.IdProducto = Convert.ToInt32(txtIDProducto.Text);
            nuevor.IdProveedor = Convert.ToInt32(txtIDProveedor.Text);
            nuevor.AltaF = DateTime.Today.Date;
            nuevor.Idtransporte = transporte.IdProve;
            nuevor.CausaRma = txtCausaRMA.Text;
            nuevor.IdUserCrea = Contexto.CodUser;
            nuevor.Imputacion = Convert.ToInt32(txtImputacion.Text);
            coreProducto.NuevoRMAProducto(nuevor);

            //debemos cambia el estado del producto a "Mantenimiento";
            //buscamos el producto
            Producto _idp = coreProducto.BuscarDatosUno(nuevor.IdProducto);
            if (_idp.ControlSf == 1) // indica que el producto controla la situacion fisica
            {
                //actualizamos la situacion fisica si es que el producto asi lo requiere
                coreProducto.ActualizarSituacionFisica(nuevor.IdProducto, 3);
            }
        }

        private void BuscarIMputacion()
        {

            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            Obra obra = coreObra.BuscarImputacion(_imputacion);
            txtNombreObra.Text = obra.NombreObra;
            txtCliente.Text = obra.Cliente;
        }

        #endregion

        private void cmbTransporte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proveedor p = cmbTransporte.SelectedItem as Proveedor;
            Proveedor tpe = bLLProveedor.BuscarPorId(p.IdProve);
            txtDirTransporte.Text = tpe.Dir1;
            txtTeTransporte.Text = tpe.Tel1;
        }

        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuscarIMputacion();
            }
            if (e.Key == Key.Tab)
            {
                BuscarIMputacion();
            }
        }

        private void txtImputacion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
