using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucDetallePlanManteVh.xaml
    /// </summary>
    public partial class ucDetallePlanManteVh : UserControl
    {
        public static int _idotm;

        BLLGestion gestion = new BLLGestion();
        BLLVehiculos corevh = new BLLVehiculos();
        ObservableCollection<OtmDetalle> dets = new ObservableCollection<OtmDetalle>();

        public ucDetallePlanManteVh()
        {
            InitializeComponent();
            dets = gestion.DetalleOTM(_idotm);
            dgDetalleOtm.DataContext = dets;
            dgDetalleOtm.ItemsSource = dets;


        }

        private void btnCancelarActividad_Click(object sender, RoutedEventArgs e)
        {
            OtmDetalle d = new OtmDetalle();
            d = dgDetalleOtm.SelectedItem as OtmDetalle;
            if (d.EstadoItem == "Cumplido" || d.EstadoItem == "Cancelado")
            {
                MessageBox.Show("No se puede tomar la actividad.Estado Cumplido", "Aviso", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult resultado = MessageBox.Show("Desea cancelar la actividad?", "Aviso", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                // si desea cancelar la actividad
                MessageBoxResult result = MessageBox.Show("Desea Ingresar una observacion?", "Aviso", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // si se quiere agregar una observacion
                    ObservacionOTM observacion = new ObservacionOTM(d); //creamos el formulario de observacion
                    observacion.operacion = "X";
                    observacion.txtOperacion.Text = "Cancelar Actividad - Agregar Observacion";
                    if (observacion.ShowDialog() == true)
                    {
                        MessageBox.Show("Se cancelo la actividad y se agrego la observacion", "Aviso", MessageBoxButton.OK);

                    }
                }
                else
                {
                    // si no se quiere agrega una observacion
                    d.FCumplimiento = DateTime.Today.Date;
                    d.Img_Observacion = null;
                    int fila = gestion.OtmDetalleCancelar(d);
                    dets = gestion.DetalleOTM(_idotm);
                    dgDetalleOtm.DataContext = dets;
                    dgDetalleOtm.ItemsSource = dets;

                }
                // aca deberia ir la actualizacion del estado del plan de inspeccion 
                //pasariamos como parametro el id de la otm ya que este es unico para cada registro y el valor del estado para el planinspeccion
                corevh.PLanInspeccionCambiarEstadoDesdeOTM(d.IdOtm, 4);
            }
        }

        private void btnCumplirActividad_Click(object sender, RoutedEventArgs e)
        {
            DateTime factual = DateTime.Today.Date; // campo usado para las fechas 
            OtmDetalle d = new OtmDetalle();
            d = dgDetalleOtm.SelectedItem as OtmDetalle;
            if (d.EstadoItem == "Cumplido" || d.EstadoItem == "Cancelado")
            {
                MessageBox.Show("No se puede tomar la actividad.Estado Cumplido", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (d.EstadoItem == "Pendiente")
            {
                MessageBox.Show("Antes de cumplir la actividad debe tener asignado un responsable", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                //preguntamos si quiere agregar una observacion a la actividad
                MessageBoxResult result = MessageBox.Show("Desea Ingresar una observacion de actividad?", "Aviso", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // si se quiere agregar una observacion
                    ObservacionOTM observacion = new ObservacionOTM(d); //creamos el formulario de observacion
                    observacion.operacion = "C";
                    observacion.txtOperacion.Text = "Cumplir Actividad - Agregar Observacion";
                    if (observacion.ShowDialog() == true)
                    {
                        MessageBox.Show("Se cumplio la actividad y se agrego la observacion", "Aviso", MessageBoxButton.OK);

                    }
                }
                else
                {
                    // si no se quiere agrega una observacion
                    d.FCumplimiento = DateTime.Today.Date;
                    d.Img_Observacion = null;
                    int fila = gestion.OtmDetalleCumplimiento(d);



                }
                // aca deberia ir la actualizacion del estado del plan de inspeccion 
                //pasariamos como parametro el id de la otm ya que este es unico para cada registro y el valor del estado para el planinspeccion
                corevh.PLanInspeccionCambiarEstadoDesdeOTM(d.IdOtm, 4);

                //calculamos el porcentaje de cumplimiento de la OTM

                int porcentaje = gestion.OtmCalcularCumplimiento(d.IdOtm);
                //si el porcentaje de cumplimiento es 100
                //entonces actualizamos el estado de la otm
                if (porcentaje == 100)
                {
                    gestion.OtmCumplirUna(d.IdOtm, factual);
                }

                dets = gestion.DetalleOTM(_idotm);
                dgDetalleOtm.DataContext = dets;
                dgDetalleOtm.ItemsSource = dets;
            }

        }

        private void btnTomarActividad_Click(object sender, RoutedEventArgs e)
        {
            OtmDetalle d = new OtmDetalle();
            d = dgDetalleOtm.SelectedItem as OtmDetalle;
            if (d.EstadoItem == "Cumplido" || d.EstadoItem == "Cancelado")
            {
                MessageBox.Show("No se puede tomar la actividad.Estado Cumplido", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (d.EstadoItem == "En Curso") // comprobamos que la tarea no tenga asignado otro usuario
            {
                MessageBox.Show("La tarea ya esta tomada por otro usuario", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                // si no tiene nadie a cargo, entonces actualizamos los datos del item
                d.UsuarioSeguidor = Contexto.CodUser;
                d.NombreSeguidor = Contexto.Nomuser;
                int fila = gestion.OtmDetalleTomarSeguidor(d);
                dets = gestion.DetalleOTM(_idotm);
                dgDetalleOtm.DataContext = dets;
                dgDetalleOtm.ItemsSource = dets;

            }
        }

        private void dgDetalleOtm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
