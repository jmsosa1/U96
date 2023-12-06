using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucTareasGeneral.xaml
    /// </summary>
    public partial class ucTareasGeneral : UserControl
    {
        BLLGestion gestion = new BLLGestion();
        ObservableCollection<TareaSector> tareasGeneral = new ObservableCollection<TareaSector>();
        int _tareas_pendientes, _tareas_cumplidas, _tareas_canceladas;

        DateTime _fdesde;
        DateTime _fhasta;
        public  string _tituloClase = "Tareas";
        public ICollectionView vistaTareas
        {
            get { return CollectionViewSource.GetDefaultView(tareasGeneral); }
        }



        public ucTareasGeneral()
        {
            InitializeComponent();

            if (Contexto.CodP == 1)//codp es el codigo de operacion
            {
                tareasGeneral = gestion.TareasSectorSeguimientoUsuario(Contexto.CodUser); // cargamos la lista de tareas donde aparece el usuario siguiendo actividades
            }
            else
            {
                tareasGeneral = gestion.TareasSectorTodas();
            }
            vistaTareas.Filter = new Predicate<object>(TareasDelSector);
            vistaTareas.Filter = new Predicate<object>(TareasDelUsuario);
            vistaTareas.Filter = TareasDelSector;
            dgVhGeneral.ItemsSource = vistaTareas;
            dgVhGeneral.DataContext = vistaTareas;


            _fdesde = DateTime.Today.AddDays(-30);
            _fhasta = DateTime.Today.AddDays(1);
            dtpDesde.SelectedDate = _fdesde;
            dtpHasta.SelectedDate = _fhasta;
            //calculo de tareas para mostrar en barra de estatuas 

            _tareas_pendientes = 0;
            _tareas_cumplidas = 0;
            _tareas_canceladas = 0;
            foreach (var item in tareasGeneral)
            {

                if (item.EstadoTarea == "Pendiente")
                {
                    _tareas_pendientes = _tareas_pendientes + 1;
                }
                else
                {
                    if (item.EstadoTarea == "Cumplido")
                    {


                        _tareas_cumplidas = _tareas_cumplidas + 1;
                    }
                    else
                    {

                        _tareas_canceladas = _tareas_canceladas + 1;
                    }
                }
            }
            txtRegistros.Text = tareasGeneral.Count.ToString();
            txtPendientes.Text = _tareas_pendientes.ToString();
            txtCumplidos.Text = _tareas_cumplidas.ToString();
            txtAnulados.Text = _tareas_canceladas.ToString();


        }

        private bool TareasDelUsuario(object obj)
        {
            TareaSector t = obj as TareaSector;
            string _nomuser = txtBuscar.Text;
            string _estado = "";


            if (chkEstado.IsChecked == true) // si esta seleecionado un estado de la tarea
            {
                if (cmbEstadoTarea.SelectedItem != null)
                {


                    _estado = ((ComboBoxItem)cmbEstadoTarea.SelectedItem).Content.ToString(); // se guarda el estado seleccionado
                }

                if (chkFechas.IsChecked == true)
                {
                    _fdesde = dtpDesde.SelectedDate.Value;
                    _fhasta = dtpHasta.SelectedDate.Value;
                }
                return (t.NombreCreador == _nomuser && t.AltaF >= _fdesde && t.AltaF <= _fhasta && t.EstadoTarea == _estado);
            }
            else
            {
                //si no hay un estado seleccionado se traen todas las tareas del usuario en ese rango
                return (t.NombreCreador == _nomuser && t.AltaF >= _fdesde && t.AltaF <= _fhasta);
            }
        }

        private bool TareasDelSector(object obj)
        {
            TareaSector t = obj as TareaSector;
            string _estado = "";
            // DateTime _fdesde = dtpDesde.SelectedDate.Value;
            //DateTime _fhasta = dtpHasta.SelectedDate.Value;

            if (chkEstado.IsChecked == true)
            {

                if (cmbEstadoTarea.SelectedItem != null)
                {
                    _estado = ((ComboBoxItem)cmbEstadoTarea.SelectedItem).Content.ToString();
                }

                if (chkFechas.IsChecked == true)
                {
                    _fdesde = dtpDesde.SelectedDate.Value;
                    _fhasta = dtpHasta.SelectedDate.Value;
                }
                return (t.EstadoTarea == _estado && t.AltaF >= _fdesde && t.AltaF <= _fhasta);
            }

            return (t.EstadoTarea == "Pendiente");
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                tareasGeneral = gestion.TareasSectorTodas();
                dgVhGeneral.ItemsSource = vistaTareas;
                dgVhGeneral.DataContext = vistaTareas;
            }
            else
            {


                vistaTareas.Filter = TareasDelUsuario;
            }
        }

        //agregar una tarea
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            TareaSector t = new TareaSector();
            t.AltaF = DateTime.Today.Date; // fecha de alta
            t.NombreCreador = Contexto.Nomuser; // clase statica contiene los datos de la session de usuario
            t.EstadoTarea = "Pendiente";
            t.EstadoTemporal = 1; // la tarea es nueva
            t.Vencida = 0; // la tera no esta vencida

            ABMTarea nuevaTarea = new ABMTarea(t);
            nuevaTarea.btnOperacion.Content = "Guardar";
            nuevaTarea.txtOperacion.Text = "Nueva Tarea";
            nuevaTarea.operacion = "A";
            if (nuevaTarea.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la tarea", "Aviso", MessageBoxButton.OK);
                tareasGeneral = gestion.TareasSectorTodas();
                dgVhGeneral.ItemsSource = tareasGeneral;
                dgVhGeneral.DataContext = tareasGeneral; // refrescamos el grid para que se vea el cambio
            }

        }

        //modificar algunos datos de la tarea
        private void btnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            TareaSector t = new TareaSector();
            t = dgVhGeneral.SelectedItem as TareaSector;

            if (t.EstadoTarea == "Cumplido" || t.EstadoTarea == "Cancelado")// si la tarea esta cumplida no se puede modificar
            {
                MessageBox.Show("No se puede modificar la tarea porque ya esta Cumplida/Cancelada", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                ABMTarea moditarea = new ABMTarea(t);
                moditarea.btnOperacion.Content = "Modificar";
                moditarea.operacion = "M";
                moditarea.txtOperacion.Text = "Modificar datos tarea";
                moditarea.dgActividades.IsEnabled = false;
                moditarea.btnAddItem.IsEnabled = false;

                if (moditarea.ShowDialog() == true)
                {
                    MessageBox.Show("Se Modifico la tarea", "Aviso", MessageBoxButton.OK);
                    tareasGeneral = gestion.TareasSectorTodas();
                    dgVhGeneral.ItemsSource = tareasGeneral;
                    dgVhGeneral.DataContext = tareasGeneral;
                }

            }

        }

        //eliminar una tarea
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            TareaSector t = new TareaSector();
            t = dgVhGeneral.SelectedItem as TareaSector;
            if (t.EstadoTarea == "Cumplido" || t.EstadoTarea == "Cancelado")// si la tarea esta cumplida no se puede modificar
            {
                MessageBox.Show("No se puede eliminar la tarea porque ya esta Cumplida/Cancelada", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                //tenemos que validar que el usuario creador sea quien pueda eliminar la tarea
                if (t.UsuarioCreador == Contexto.CodUser)
                {
                    MessageBoxResult respuesta =
                    MessageBox.Show("Desea BORRAR la tarea " + t.TituloTarea +
                    "Creado por : " + t.NombreCreador + " ", "Aviso", MessageBoxButton.OKCancel);
                    if (respuesta == MessageBoxResult.OK)
                    {
                        //borrar tarea
                        fila = gestion.TareaBorrar(t.IdTarea);
                        MessageBox.Show("Se Borro la tarea seleccionada", "Aviso", MessageBoxButton.OK);
                        tareasGeneral = gestion.TareasSectorTodas();
                        dgVhGeneral.ItemsSource = tareasGeneral;
                        dgVhGeneral.DataContext = tareasGeneral;
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos para eliminar la tarea", "Aviso", MessageBoxButton.OK);
                    return;
                }


            }



        }

        //dar de baja una tarea
        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            TareaSector t = new TareaSector();
            t = dgVhGeneral.SelectedItem as TareaSector;

            if (t.EstadoTarea == "Cumplido" || t.EstadoTarea == "Cancelado")// si la tarea esta cumplida no se puede modificar
            {
                MessageBox.Show("No se puede eliminar la tarea porque ya esta Cumplida/Cancelada", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                //tenemos que validar que el usuario creador sea quien pueda eliminar la tarea
                if (t.UsuarioCreador == Contexto.CodUser)
                {
                    MessageBoxResult respuesta =
                   MessageBox.Show("Desea Cancelar   la tarea " + t.TituloTarea +
                   "Creado por : " + t.NombreCreador + " ", "Aviso", MessageBoxButton.OKCancel);
                    if (respuesta == MessageBoxResult.OK)
                    {
                        //cancelar la tarea seleccionada
                        DateTime f = DateTime.Today.Date;
                        fila = gestion.TareaCancelar(t.IdTarea, f);
                        MessageBox.Show("Se Cancelo la tarea seleccionada", "Aviso", MessageBoxButton.OK);
                        tareasGeneral = gestion.TareasSectorTodas();
                        dgVhGeneral.ItemsSource = tareasGeneral;
                        dgVhGeneral.DataContext = tareasGeneral;
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos para cancelar la tarea", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }
        }

        //evento cambio de seleccion en el data grid general
        // donde pasamos como parametro al control de detalle el id del registro
        private void dgVhGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TareaSector t = new TareaSector();
            t = dgVhGeneral.SelectedItem as TareaSector;
            if (t != null)
            {
                ucDetalleFilaTarea.idt = t.IdTarea;
            }
        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgVhGeneral.SelectedIndex = -1;

        }

        private void ChkEstado_Checked(object sender, RoutedEventArgs e)
        {
            cmbEstadoTarea.IsEnabled = true;
            cmbEstadoTarea.SelectedItem = null;
        }

        private void ChkEstado_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbEstadoTarea.IsEnabled = false;
            cmbEstadoTarea.SelectedItem = null;

            vistaTareas.Filter = TareasDelSector;


        }

        private void CmbEstadoTarea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaTareas.Filter = TareasDelSector;
            //string _estado = ((ComboBoxItem)cmbEstadoTarea.SelectedItem).Content.ToString();
            //txbEstatus.Text = "Mostrando Tareas :" + _estado;
        }

        private void ChkFechas_Checked(object sender, RoutedEventArgs e)
        {
            dtpDesde.IsEnabled = true;
            dtpHasta.IsEnabled = true;
        }

        private void ChkFechas_Unchecked(object sender, RoutedEventArgs e)
        {
            dtpDesde.IsEnabled = false;
            dtpHasta.IsEnabled = false;
        }
    }
}
