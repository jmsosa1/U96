using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucDetalleSolicitudAB.xaml
    /// </summary>
    public partial class ucDetalleSolicitudAB : UserControl
    {
        public static int ids; // variable estatica que se utiliza como parametro  para el detalle de la tarea seleccionada
        BLLGestion gestion = new BLLGestion();
        ObservableCollection<Solicitud_ab_detalle> dets = new ObservableCollection<Solicitud_ab_detalle>();

        public ucDetalleSolicitudAB()
        {
            InitializeComponent();
            dets = gestion.SolicitudAb_Detalle_Una(ids);
            dgItemsSolicitud.DataContext = dets;
            dgItemsSolicitud.ItemsSource = dets;

        }

        private void BtnCumplirItem_Click(object sender, RoutedEventArgs e)
        {


            int iduser = Contexto.CodUser;
            // buscamos el item seleccionado y ejecutamos el procedimiento
            Solicitud_ab_detalle d = new Solicitud_ab_detalle();
            d = dgItemsSolicitud.SelectedItem as Solicitud_ab_detalle;

            //preguntamos si quiere cumplir el item
            MessageBoxResult respuesta = MessageBox.Show("Cumplir el item : " + d.NumItem + "de la Solicitud: " + d.IdSol + "", "Aviso", MessageBoxButton.YesNo);
            if (respuesta == MessageBoxResult.Yes)
            {
                //cumplimos el item 
                int fila = gestion.SolicitudAB_Cumplir_Un_Item(d.IdSol, d.IdDetSol, iduser);

                // calculamos el cumplimiento de la solicitud
                int porcentaje = gestion.SolicitudAB_Calcular_Cumplimiento(d.IdSol);
                if (porcentaje == 100)
                {
                    //si el porcentaje es igual 100%
                    DateTime f = DateTime.Today.Date; // fecha cumpliemiento 100% solicitud            
                    gestion.SolicitudAB_Cumplir_Una(d.IdSol, f);
                }
                //refrescamos el grid
                dets = gestion.SolicitudAb_Detalle_Una(ids);
                dgItemsSolicitud.DataContext = dets;
                dgItemsSolicitud.ItemsSource = dets;
            }
            else
            {
                MessageBox.Show("Se cancelo la accion", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void BtnCancelarItem_Click(object sender, RoutedEventArgs e)
        {


            // buscamos el item seleccionado y ejecutamos el procedimiento
            Solicitud_ab_detalle d = new Solicitud_ab_detalle();
            d = dgItemsSolicitud.SelectedItem as Solicitud_ab_detalle;
            int iduser = Contexto.CodUser;
            //preguntamos si quiere cumplir el item
            MessageBoxResult respuesta = MessageBox.Show("Cancelar el item " + d.NumItem + " de la solicitud : " + d.IdSol + "?", "Aviso", MessageBoxButton.YesNo);

            if (respuesta == MessageBoxResult.Yes)
            {
                //llamamos al procedimiento, siempre respetando el orden de los parametros!!!!!!!
                //de lo contrario no tendran ningun efecto
                int fila = gestion.SolicitudAB_Cancelar_Un_Item(d.IdSol, d.IdDetSol, iduser);

                //refrescamos el grid
                dets = gestion.SolicitudAb_Detalle_Una(ids);
                dgItemsSolicitud.DataContext = dets;
                dgItemsSolicitud.ItemsSource = dets;
            }
            else
            {
                MessageBox.Show("Se cancelo la accion", "Aviso", MessageBoxButton.OK);
                return;
            }
        }
    }
}
