using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaTarea.xaml
    /// </summary>
    public partial class ucDetalleFilaTarea : UserControl
    {
        public static int idt; // variable estatica que se utiliza como parametro  para el detalle de la tarea seleccionada
        BLLGestion gestion = new BLLGestion();
        ObservableCollection<DetTareaSector> dets = new ObservableCollection<DetTareaSector>();


        public ucDetalleFilaTarea() //constructor de clase
        {
            InitializeComponent();
            dets = gestion.DetalleTareaSector(idt);
            dgListaActividades.ItemsSource = dets;
            dgListaActividades.DataContext = dets;
        }

        // tomar actividad
        private void btnTomarActividad_Click(object sender, RoutedEventArgs e)
        {
            DetTareaSector d = new DetTareaSector();
            d = dgListaActividades.SelectedItem as DetTareaSector;
            if (d.EstadoItem == "En Curso")
            {
                MessageBox.Show("La tarea ya esta tomada por otro usuario", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                d.IdSeguidor = Contexto.CodUser;
                int fila = gestion.DetalleTareaTomarSeguidor(d);
                dets = gestion.DetalleTareaSector(idt);
                dgListaActividades.ItemsSource = dets;
                dgListaActividades.DataContext = dets;
            }
        }

        // cumplir actividad
        private void btnCumplirActividad_Click(object sender, RoutedEventArgs e)
        {
            DateTime factual = DateTime.Today.Date; // campo usado para las fechas 
            // creamos un objeto del elemento seleccionado en la datagrid
            DetTareaSector d = new DetTareaSector();
            d = dgListaActividades.SelectedItem as DetTareaSector;
            //comprobamos el estado y si tiene asignado un seguidor
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
                    ObservacionActividad observacion = new ObservacionActividad(d); //creamos el formulario de observacion
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
                    d.Fcumplimiento = DateTime.Today.Date;
                    int fila = gestion.DetalleTareaCumplimiento(d);

                }
                int porcentaje = gestion.TareaCalcularCumplimiento(d.IdTarea);
                //si el porcentaje de cumplimiento es 100
                //entonces actualizamos el estado de la ta tarea
                if (porcentaje == 100)
                {
                    gestion.TareaCumplirUna(d.IdTarea, factual);
                }
                dets = gestion.DetalleTareaSector(idt);
                dgListaActividades.ItemsSource = dets;
                dgListaActividades.DataContext = dets;

            }
        }

        // cancelar actividad
        private void btnCancelarActividad_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto del elemento seleccionado en el datagrid
            DetTareaSector d = new DetTareaSector();
            d = dgListaActividades.SelectedItem as DetTareaSector;

            MessageBoxResult resultado = MessageBox.Show("Desea cancelar la actividad?", "Aviso", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                // si desea cancelar la actividad
                MessageBoxResult result = MessageBox.Show("Desea Ingresar una observacion?", "Aviso", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // si se quiere agregar una observacion
                    ObservacionActividad observacion = new ObservacionActividad(d);
                    observacion.operacion = "X";
                    observacion.txtOperacion.Text = "Cancelar Actividad - Agregar Observacion";
                    if (observacion.ShowDialog() == true)
                    {
                        MessageBox.Show("Se cancelo la actividad y se agrego la observacion", "Aviso", MessageBoxButton.OK);

                        dets = gestion.DetalleTareaSector(idt);
                        dgListaActividades.ItemsSource = dets;
                        dgListaActividades.DataContext = dets;
                    }
                }
                else
                {
                    //si no se quiere agregar una observacion

                    d.Fcumplimiento = DateTime.Today.Date;

                    int fila = gestion.DetalleTareaCancelar(d);

                    dets = gestion.DetalleTareaSector(idt);
                    dgListaActividades.ItemsSource = dets;
                    dgListaActividades.DataContext = dets;
                    MessageBox.Show("Se cancelo la actividad", "Aviso", MessageBoxButton.OK);

                }
            }

        }

    }
}
