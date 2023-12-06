using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;
using System.Windows.Input;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCombustible.xaml
    /// </summary>
    public partial class ABMCombustible : MaterialWindow 
    {
        public string operacion = "";
        BLLVehiculos bLL = new BLLVehiculos();
        Combustible _comb = new Combustible();

        public ABMCombustible(Combustible _comb)
        {
            InitializeComponent();

            grdAbm.DataContext = _comb;


        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se Cancelo la operacion", "Aviso", MessageBoxButton.OK);
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            Combustible nuevocombustible = new Combustible();


            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar una nombre del combustible", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                nuevocombustible.Descripcion = txtNombre.Text;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debe ingresar un valor del precio actual", "Aviso", MessageBoxButton.OK);
                return;

            }


            if (operacion == "A")
            {// si es una alta
                string p = txtPrecio.Text;
                nuevocombustible.PrecioLitroActual = decimal.Parse(p.Replace("$", ""));
                string pa = txtPrecioAnterior.Text;
                nuevocombustible.PrecioLitroAnterior = decimal.Parse(pa.Replace("$", ""));
                nuevocombustible.UltimaModi = DateTime.Today.Date;
                fila = bLL.CombustibleAlta(nuevocombustible);

            }
            else
            {
                if (operacion == "M")
                {// si es una modificacion
                    nuevocombustible.IdCombustible = Convert.ToInt16(txtIdCombustible.Text);
                    string p = txtPrecio.Text;
                    nuevocombustible.PrecioLitroActual = decimal.Parse(p.Replace("$", ""));
                    string pa = txtPrecioAnterior.Text;
                    nuevocombustible.PrecioLitroAnterior = decimal.Parse(pa.Replace("$", ""));
                    nuevocombustible.UltimaModi = DateTime.Today.Date;
                    fila = bLL.CombustibleModi(nuevocombustible);
                }
                else
                {//si es una baja
                    nuevocombustible.IdCombustible = Convert.ToInt16(txtIdCombustible.Text);
                    fila = bLL.CombustibleBorrar(nuevocombustible.IdCombustible);
                }
            }
            if (fila != 0)
            {
                MessageBox.Show("Se registro correctamente", "Aviso", MessageBoxButton.OK);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("No se pudo realizar el registro", "Aviso", MessageBoxButton.OK);
                DialogResult = false;
            }
            
        }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtPrecio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPrecio.SelectAll();
        }

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
