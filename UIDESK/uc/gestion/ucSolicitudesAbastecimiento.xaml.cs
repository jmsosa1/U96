using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.imprimir;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucSolicitudesAbastecimiento.xaml
    /// </summary>
    public partial class ucSolicitudesAbastecimiento : UserControl
    {
        BLLGestion gestion = new BLLGestion();
        public string _tituloClase = "Solicitudes de Abastecimiento";
        ObservableCollection<Solicitud_Ab> solicitudes_todas = new ObservableCollection<Solicitud_Ab>();



        public ICollectionView vistaSolicitudes
        {
            //esta propiedad nos permite  tener siempre actualizada la vista con los datos de la ObservableCollection
            get { return CollectionViewSource.GetDefaultView(solicitudes_todas); }
        }

        public ucSolicitudesAbastecimiento()
        {
            InitializeComponent();
            solicitudes_todas = gestion.SolicitudAb_Todas(); // creamos la coleccion y esta se asigna a la vista
            vistaSolicitudes.Filter = new Predicate<object>(SolObraEstado);
            vistaSolicitudes.Filter = new Predicate<object>(SolJefeObra);
            //vistaSolicitudes.Filter = new Predicate<object>(SolObra);
            vistaSolicitudes.Filter = SolObraEstado;
            vistaSolicitudes.Refresh();
            dgVhGeneral.DataContext = vistaSolicitudes; // asignamos la vista al datagrid
            dgVhGeneral.ItemsSource = vistaSolicitudes; // asignamos la vita al datagrid


        }

        // private bool SolObra(object obj)
        //{
        //throw new NotImplementedException();
        //}

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgVhGeneral.SelectedIndex = -1;
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            Solicitud_Ab solicitud_ = dgVhGeneral.SelectedItem as Solicitud_Ab;
            if (solicitud_ != null)
            {
                MessageBoxResult result = MessageBox.Show("Desea Anular la solicitud : " + solicitud_.IdSol + "", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //anular la solicitud
                    int fila = gestion.SolicitudAB_Anular(solicitud_.IdSol);
                    MessageBox.Show("Se anulo la solcitud", "Aviso", MessageBoxButton.OK);

                    //refrescamos el grid
                    solicitudes_todas = gestion.SolicitudAb_Todas();
                    dgVhGeneral.DataContext = vistaSolicitudes; // asignamos la vista al datagrid
                    dgVhGeneral.ItemsSource = vistaSolicitudes;

                }
                else
                {
                    //no hacer nada
                    return;
                }
            }
        }





        //agregar solicitud
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            Solicitud_Ab ab = new Solicitud_Ab();
            ab.AltaF = DateTime.Today.Date;
            ab.UsuarioCreador = Contexto.CodUser;
            ab.EstadoTemporal = 1;
            ab.EstadoSolicitud = "Pendiente";

            ABMSolicitud nuevasolicitud = new ABMSolicitud(ab);
            nuevasolicitud.txbAltaF.Text = DateTime.Today.Date.ToShortDateString();
            nuevasolicitud.txbCreador.Text = Contexto.Nomuser;
            nuevasolicitud.txbEstadoSolicitud.Text = "Pendiente";
            nuevasolicitud.txtOperacion.Text = "ALta Solicitud";
            nuevasolicitud.btnOperacion.Content = "Guardar";
            if (nuevasolicitud.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la solicitud", "Aviso", MessageBoxButton.OK);
                solicitudes_todas = gestion.SolicitudAb_Todas();
                dgVhGeneral.DataContext = solicitudes_todas;
                dgVhGeneral.ItemsSource = solicitudes_todas;
            }
            else
            {
                MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);

                return;
            }

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            // buscar solicitudes de un jefe de obra
            //solicitudes_todas = gestion.SolicitudesAB_JefeObra(txtBuscar.Text);
            vistaSolicitudes.Filter = SolJefeObra;
            vistaSolicitudes.Refresh();
            dgVhGeneral.DataContext = vistaSolicitudes; // asignamos la vista al datagrid
            dgVhGeneral.ItemsSource = vistaSolicitudes; // asignamos la vita al datagrid
            //txtStatusBar.Text = "Mostrando solicitudes de abastecimiento para :" + txtBuscar.Text ;

        }

        private bool SolJefeObra(object obj)
        {
            Solicitud_Ab solicitud = obj as Solicitud_Ab;
            chkImputacion.IsEnabled = false;
            if (chkEstadoSol.IsChecked == true)
            {
                //si ademas hay seleccionado un estado de solicitud para una obra entonces

                string _estado = ((ComboBoxItem)cmbEstadoSolicitud.SelectedItem).Content.ToString(); // filtro por estado sol

                //devolvemos el listado de solicitudes de una obra determinada  y pendientes

                return (solicitud.EstadoSolicitud == _estado && solicitud.SolicitadoPor.Contains(txtBuscar.Text));


            }
            else
            {


                return (solicitud.SolicitadoPor.Contains(txtBuscar.Text));
            }
        }

        //evento cambio de seleccion del datagrid general
        //donde pasamos el id de la solicitud como parametro al constructor del detalle
        private void dgVhGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Solicitud_Ab s = new Solicitud_Ab();
            s = dgVhGeneral.SelectedItem as Solicitud_Ab;
            if (s != null)
            {
                ucDetalleSolicitudAB.ids = s.IdSol;
            }
        }

        private void BtnTomarSolicitud_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            //cuando se quiere tomar la solicitud, se debe verificar 
            // si ya no esta tomada por alguien anterior o si la sol esta anulada
            Solicitud_Ab s = new Solicitud_Ab();
            s = dgVhGeneral.SelectedItem as Solicitud_Ab;
            if (s.EstadoSolicitud == "Anulado")
            {
                MessageBox.Show("No puede tomar la solicitud porque esta anulada", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                if (s.UsuarioSeguidor != 6) // si ya tiene seguidor
                {
                    MessageBox.Show("No puede tomar la solicitud porque ya tiene asignada un usuario", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    MessageBoxResult r = new MessageBoxResult();
                    r = MessageBox.Show("Desea tomar la solicitud para su seguimiento?", "Aviso", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {


                        //si todo esta bien, actualizamos la solicitud
                        //con datos de id solicitud y id usuario del sistema
                        fila = gestion.SolicitudTomarUna(s.IdSol, Contexto.CodUser);
                    }
                    else
                    {
                        //nada
                    }
                }
            }

            if (fila != 0)
            {
                MessageBox.Show("Se actualizo la solicitud", "Aviso", MessageBoxButton.OK);
                solicitudes_todas = gestion.SolicitudAb_Todas();
                dgVhGeneral.DataContext = solicitudes_todas;
                dgVhGeneral.ItemsSource = solicitudes_todas;

            }

        }




        private void BtnBuscarImputacion_Click(object sender, RoutedEventArgs e)
        {
            //buscar todas las solicitudes de una imputacion particular
            vistaSolicitudes.Filter = SolObraEstado;
            vistaSolicitudes.Refresh();
        }

        private void BtnImprimirSol_Click(object sender, RoutedEventArgs e)
        {
            Solicitud_Ab s = new Solicitud_Ab();
            s = dgVhGeneral.SelectedItem as Solicitud_Ab;
            if (s != null)
            {
                ImprimirDocumento imprimir = new ImprimirDocumento(s.IdSol);
                imprimir.Show();
            }
        }

        private void ChkEstadoSol_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbEstadoSolicitud.SelectedItem = null;
            cmbEstadoSolicitud.IsEnabled = false;

            vistaSolicitudes.Filter = SolObraEstado;

            vistaSolicitudes.Refresh();

        }

        private void ChkEstadoSol_Checked(object sender, RoutedEventArgs e)
        {
            cmbEstadoSolicitud.IsEnabled = true;
        }

        private void ChkImputacion_Checked(object sender, RoutedEventArgs e)
        {
            txtImputacion.IsEnabled = true;
        }

        private void ChkImputacion_Unchecked(object sender, RoutedEventArgs e)
        {
            txtImputacion.Text = "";
            txtImputacion.IsEnabled = false;
            vistaSolicitudes.Filter = null;
            vistaSolicitudes.Filter = SolObraEstado;
            vistaSolicitudes.Refresh();

        }

        private void CmbEstadoSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filtramos la solicitud en fucion del estado de la misma

            vistaSolicitudes.Filter = SolObraEstado;
            vistaSolicitudes.Refresh();

        }

        private bool SolObraEstado(object so)
        {
            Solicitud_Ab solobra = so as Solicitud_Ab; // convertimos el objeto parametro a uno de tipo solicitud

            if (chkEstadoSol.IsChecked == true && chkImputacion.IsChecked == true)
            {
                // si queremos mostrar el estado de solicitudes para una obra determinada
                string _estado = ((ComboBoxItem)cmbEstadoSolicitud.SelectedItem).Content.ToString(); // filtro por estado sol
                int _imputacion = Convert.ToInt32(txtImputacion.Text); // filtro por imputacion
                return (solobra.Imputacion == _imputacion && solobra.EstadoSolicitud == _estado);

            }

            if (chkEstadoSol.IsChecked == false && chkImputacion.IsChecked == false)
            {
                //si el estado y la imputacion estan desactivados mostramos solo lo pendiente
                //txtStatusBar.Text = "Mostrando solicitudes de abastecimiento Pendientes";
                return (solobra.EstadoSolicitud == "Pendiente");
            }


            if (chkEstadoSol.IsChecked == true && chkImputacion.IsChecked == false) // si se seleciono una imputacion de obra
            { //si esta seleccionado el estado y no la imputacion   
              //si ademas hay seleccionado un estado de solicitud para una obra entonces

                string _estado = ((ComboBoxItem)cmbEstadoSolicitud.SelectedItem).Content.ToString(); // filtro por estado sol

                //devolvemos el listado de solicitudes de una obra determinada  y pendientes
                // txtStatusBar.Text = "Mostrando solicitudes de abastecimiento obra estado:" + _estado;
                return (solobra.EstadoSolicitud == _estado);


            }
            else
            {
                int _imputacion = Convert.ToInt32(txtImputacion.Text); // filtro por imputacion
                                                                       //si no esta seleccionado el estado entonces esta seleccionado la imputacion
                                                                       //txtStatusBar.Text = "Mostrando solicitudes de abastecimiento para obra: " + txtImputacion.Text;

                return (solobra.Imputacion == _imputacion);
            }

        }  //logica del filtro aplicado a la vista





    }
}
