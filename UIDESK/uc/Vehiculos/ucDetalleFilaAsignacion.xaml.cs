using BLL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaAsignacion.xaml
    /// </summary>
    /// 
    public partial class ucDetalleFilaAsignacion : UserControl
    {
        BLLVehiculos bLLVh = new BLLVehiculos();
        BLLBase BLLBase = new BLLBase();
        public static int _idasig;  //el valor de este campo se recibe del evento selectionchanged del grid principal dgActualidadVh

        DateTime _fechaFin;

        public ucDetalleFilaAsignacion()
        {
            InitializeComponent();

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            //si no se selecciono una fecha, le avisamos
            if (dtpFechaFinaliza.SelectedDate == null)
            {
                MessageBox.Show("Debe Seleccionar una fecha de finalizacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            // si ya tenemos el dato de la fecha de fin ,entonces llamamos al metodo que actualiza la asignacion
            _fechaFin = dtpFechaFinaliza.SelectedDate.Value;


            bLLVh.BajaAsignacion(_idasig, _fechaFin.Date);

            MessageBox.Show("El registro se actualizo con exito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            btnActualizar.IsEnabled = false;

            dtpFechaFinaliza.IsEnabled = false;



        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFFinal.Text))
            {

                if (dtpFechaFinaliza.IsEnabled == false)
                {
                    dtpFechaFinaliza.IsEnabled = true;
                    btnActualizar.IsEnabled = true;
                }
                else
                {
                    dtpFechaFinaliza.IsEnabled = false;
                    btnActualizar.IsEnabled = false;
                }
            }
            else
            { MessageBox.Show("La Asignacionacion esta finalizada", "Aviso", MessageBoxButton.OK); }

        }
    }
}
