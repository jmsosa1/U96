using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMDocVh.xaml
    /// </summary>
    public partial class ABMDocVh : MaterialWindow
    {
        List<Docu_vh> lstDocuVh = new List<Docu_vh>();
        BLLVehiculos bLL = new BLLVehiculos();
        VehiculoDocu vehiculoDocu = new VehiculoDocu();
        Vehiculo v = new Vehiculo();
        int _estadoreg = 1;// indica el estado del dominio para el caso que la fecha de vencimiento


        public ABMDocVh()
        {
            InitializeComponent();
            lstDocuVh = bLL.VehiculoListarTipoDocu();
            cmbTipoDoc.ItemsSource = lstDocuVh;
            vehiculoDocu.Costo = 0;
            grdDatos.DataContext = vehiculoDocu;
        }



        private void BtnOperacion_Click(object sender, RoutedEventArgs e)
        {

            // sea menor a la actual
            // comprobamos que exista un dominio
            if (string.IsNullOrEmpty(txtDominio.Text))
            {
                MessageBox.Show("Debe ingresar un numero de dominio", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (cmbTipoDoc.SelectedItem == null) // se tiene que seleccionar un tipo de documentacion
            {
                MessageBox.Show("Debe seleccionar un tipo de documento", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (rdbVencimiento.IsChecked == false && rdbCobertura.IsChecked== false)
            {
                MessageBox.Show("Debe seleccionar si es un Vencimiento o si es una Cobertura", "Aviso", MessageBoxButton.OK);
                return;
            }


            //validamos que si se escribio una fecha de vencimiento esta no sea menor a la actual, es decir
            //del pasado
            if (rdbVencimiento.IsChecked == true && dtpVencimiento.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha de vencimiento", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                //validamos que la fecha no sea menor a la actual
                if (dtpVencimiento.SelectedDate < DateTime.Today.Date)
                {
                    MessageBoxResult boxResult =
                     MessageBox.Show("La fecha de vencimiento es menor a la actual.Es correcto?", "Aviso", MessageBoxButton.YesNo);

                    if (boxResult == MessageBoxResult.Yes)
                    {
                        _estadoreg = 2;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (rdbCobertura.IsChecked == true)
            {
                //si alguna de las fechas es nulla
                if (dtpDesde.SelectedDate == null || dtpHasta.SelectedDate == null)
                {
                    MessageBox.Show("Falta alguna fecha Desde/Hasta", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    //comprobamos que la fecha hasta sea mayor a la desde
                    if (dtpHasta.SelectedDate < dtpDesde.SelectedDate)
                    {
                        MessageBox.Show("La fecha hasta no puede ser menor que la fecha desde", "Aviso", MessageBoxButton.OK);
                        return;
                    }
                    //comprobamos que la fecha hasta es menor o no que la fecha actual
                    if (dtpHasta.SelectedDate < DateTime.Today.Date)
                    {
                        MessageBoxResult message =
                        MessageBox.Show("La fecha hasta es menor que la actual. Es correcto?", "Aviso", MessageBoxButton.YesNo);
                        if (message == MessageBoxResult.Yes)
                        {
                            _estadoreg = 2;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            vehiculoDocu = ArmarDocumentacion();

            //grabamos el registro de la documentacion
            int fila = bLL.VehiculoAgregarNuevaDocumentacion(vehiculoDocu);
            if (fila != 0)
            {
                DialogResult = true;
            }



        }

        private VehiculoDocu ArmarDocumentacion()
        {
            Docu_vh docu_ = new Docu_vh();
            docu_ = cmbTipoDoc.SelectedItem as Docu_vh;
            vehiculoDocu.IdVh = v.IdVh;
            vehiculoDocu.IdDocuvh = docu_.IdDocuVH;
            vehiculoDocu.Altaf = DateTime.Today.Date;
            if (rdbVencimiento.IsChecked == true)
            {
                vehiculoDocu.FVencimiento = dtpVencimiento.SelectedDate.Value.Date;
                vehiculoDocu.ControlFecha = 1;
                vehiculoDocu.FDesde = null;
                vehiculoDocu.FHasta = null;
            }
            if (rdbCobertura.IsChecked == true)
            {// si es nula la fecha de vencimiento, entonces la logica indica que deberia indicarse un periodo
                //desde hasta
                vehiculoDocu.FVencimiento = null;
                vehiculoDocu.FDesde = dtpDesde.SelectedDate.Value.Date;
                vehiculoDocu.FHasta = dtpHasta.SelectedDate.Value.Date;
                vehiculoDocu.ControlFecha = 2;

            }
            vehiculoDocu.EstadoReg = _estadoreg;
            string _costo = txtCosto.Text;
            vehiculoDocu.Costo = decimal.Parse(_costo.Replace("$", ""));
            vehiculoDocu.Nota = txtNota.Text;
            vehiculoDocu.NumeroDoc = txtNumeroDoc.Text;
            return vehiculoDocu;
        }

        private void TxtDominio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                v = bLL.VehiculoBuscarUnDominio(txtDominio.Text);
                if (v == null)
                {
                    MessageBox.Show("No existe el vehiculo", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    txtModelo.Text = v.Modelo;
                    txtMarca.Text = v.NomMarca;
                    cmbTipoDoc.Focus();
                }
            }
        }



        private void RdbVencimiento_Checked(object sender, RoutedEventArgs e)
        {
            dtpVencimiento.IsEnabled = true;
            dtpDesde.IsEnabled = false;
            dtpHasta.IsEnabled = false;
            dtpVencimiento.Focus();
        }

        private void RdbCobertura_Checked(object sender, RoutedEventArgs e)
        {
            dtpVencimiento.IsEnabled = false;
            dtpDesde.IsEnabled = true;
            dtpHasta.IsEnabled = true;
            dtpDesde.Focus();
        }

        private void TxtCosto_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCosto.SelectAll();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
