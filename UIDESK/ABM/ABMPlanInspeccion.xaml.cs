using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMPlanInspeccion.xaml
    /// </summary>
    public partial class ABMPlanInspeccion : MaterialWindow 
    {
        BLLVehiculos coreVH = new BLLVehiculos();
        Vehiculo _vehiculoParametro = new Vehiculo();

        public ABMPlanInspeccion(Vehiculo vehiculo)
        {
            InitializeComponent();
            _vehiculoParametro = vehiculo;
            DataContext = vehiculo;
            txtFechaInicio.Text = DateTime.Today.ToShortDateString();
        }

        //private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Desea Cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        this.Close();
        //    }
        //}

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //validamos antes de grabar
            if (string.IsNullOrEmpty(txtTarea.Text))
            {
                MessageBox.Show("Debe describir una tarea", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbAtributoComparacion.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un atributo de comparacion", "Aviso", MessageBoxButton.OK);
                return;
            }
            /*if (string.IsNullOrEmpty(txtValorConstante.Text))
            {
                MessageBox.Show("Debe Ingresar un valor de constante", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                //validar que sea un numero 
            }*/

            /*if (string.IsNullOrEmpty(txtNuevoValorComparacion.Text))
            {
                MessageBox.Show("No existe valor de comparacion calculado. Haga click en el boton de calculadora", "Aviso", MessageBoxButton.OK);
                return;
            }*/

            if (string.IsNullOrEmpty(txtNuevoValorComparacion.Text))
            {
                MessageBox.Show("No existe valor de comparacion. Debe indicar uno", "Aviso", MessageBoxButton.OK);
                return;
            }

            //si todo esta bien , entonces grabamos la tarea
            //armamos el nuevo objeto
            plan_inspeccion nuevoPlan = new plan_inspeccion();
            nuevoPlan.Idvh = _vehiculoParametro.IdVh;
            nuevoPlan.Tarea = txtTarea.Text;
            nuevoPlan.Ultima_actualizacion = DateTime.Today;
            nuevoPlan.FechaInicio = DateTime.Today;
            // nuevoPlan.ValorConstante = Convert.ToDecimal(txtValorConstante.Text);
            nuevoPlan.ValorConstante = Convert.ToDecimal(txtValorConstante.Text);
            nuevoPlan.Gap = Convert.ToDecimal(txtGap.Text);
            nuevoPlan.ValorActualComparativo = Convert.ToDecimal(txtValorActualAtributo.Text);//valor actual del atributo comparativo
            nuevoPlan.ValorInicio = Convert.ToDecimal(txtValorActualAtributo.Text);//valor actual del atributo comparativo
            nuevoPlan.ValorLimiteComparativo = Convert.ToDecimal(txtNuevoValorComparacion.Text); // proximo valor a comparar del atributo
            nuevoPlan.Estado = 1; // activo
            string _atributoSeleccionado = ((ComboBoxItem)cmbAtributoComparacion.SelectedItem).Content.ToString();
            nuevoPlan.AtributoComparativo = _atributoSeleccionado;
            nuevoPlan.GapAlarma = Convert.ToDecimal(txtGapAlarma.Text);




            byte[] img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\progress-clock.png");
            nuevoPlan.ImageEstadoTemp = img;
            coreVH.PlanInspeccionAlta(nuevoPlan);
            DialogResult = true;
            this.Close();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CmbAtributoComparacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string _atributoSeleccionado = ((ComboBoxItem)cmbAtributoComparacion.SelectedItem).Content.ToString();
            if (_atributoSeleccionado == "Kilometros")
            {
                // 1) mostramos el valor actual del atributo
                txtValorActualAtributo.Text = Convert.ToString(_vehiculoParametro.KmAcumulado);
                btnCalcularValores.IsEnabled = true;
            }
            else
            {
                txtValorActualAtributo.Text = Convert.ToString(_vehiculoParametro.HorasAcumuladas);
                btnCalcularValores.IsEnabled = true;
            }
        }

        private void BtnCalcularValores_Click(object sender, RoutedEventArgs e)
        {
            decimal _constante;
            decimal _nuevoLimite;
            decimal _actualValor;
            decimal _gap;
            //((ComboBoxItem)cmbTipoMante.SelectedItem).Content.ToString()
            string _atributoSeleccionado = ((ComboBoxItem)cmbAtributoComparacion.SelectedItem).Content.ToString();
            if (string.IsNullOrEmpty(txtValorConstante.Text))
            {
                MessageBox.Show("Debe Ingresar un valor de constante", "Aviso", MessageBoxButton.OK);
                return;
            }


            if (_atributoSeleccionado == "Kilometros")
            {
                // 1) mostramos el valor actual del atributo si es kilometros , los acumulados
                txtValorActualAtributo.Text = Convert.ToString(_vehiculoParametro.KmAcumulado);
                _actualValor = _vehiculoParametro.KmAcumulado;
            }
            else
            {
                // 1) mostramos el valor actual del atributo si es horas , los acumulados
                txtValorActualAtributo.Text = Convert.ToString(_vehiculoParametro.HorasAcumuladas);
                _actualValor = _vehiculoParametro.HorasAcumuladas;
            }

            //calculamos los valores nuevos
            _constante = Convert.ToDecimal(txtValorConstante.Text);
            _nuevoLimite = _actualValor + _constante;
            _gap = _actualValor - _nuevoLimite;
            //mostramos en la interfaz
            txtNuevoValorComparacion.Text = _nuevoLimite.ToString();
            txtGap.Text = _gap.ToString();

        }

        private void CmbConfigurarAlarma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbConfigurarAlarma.SelectedItem != null)
            {
                string _v_alarma = ((ComboBoxItem)cmbConfigurarAlarma.SelectedItem).Content.ToString();
                string _atributoSeleccionado = ((ComboBoxItem)cmbAtributoComparacion.SelectedItem).Content.ToString();

                decimal _valorActualGap = Convert.ToDecimal(txtGap.Text);
                decimal _valorAlarmaDisparo;
                if (_v_alarma == "10")
                {
                    _valorAlarmaDisparo = (((90 * _valorActualGap) / 100) * -1) + Convert.ToDecimal(txtValorActualAtributo.Text); // multiplicamos por -1 para que el valor sea positivo
                    txtGapAlarma.Text = _valorAlarmaDisparo.ToString();
                    txbNombreAtributo.Text = _atributoSeleccionado;
                }
                else
                {
                    _valorAlarmaDisparo = (((80 * _valorActualGap) / 100) * -1) + Convert.ToDecimal(txtValorActualAtributo.Text);
                    txtGapAlarma.Text = _valorAlarmaDisparo.ToString();
                    txbNombreAtributo.Text = _atributoSeleccionado;
                }
            }

        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
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
