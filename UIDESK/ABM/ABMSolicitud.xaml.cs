using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMSolicitud.xaml
    /// </summary>
    public partial class ABMSolicitud : MaterialWindow
    {
        BLLEmpleados bLL = new BLLEmpleados();
        ObservableCollection<Empleado> listaEmpleados = new ObservableCollection<Empleado>();
        List<Empleado> empleadosBuscador = new List<Empleado>();
        ObservableCollection<Solicitud_ab_detalle> detsolicitud = new ObservableCollection<Solicitud_ab_detalle>();
        Solicitud_Ab solicitud_ = new Solicitud_Ab();
        BLLObras bLLObras = new BLLObras();
        Obra obra = new Obra();
        BLLGestion gestion = new BLLGestion();

        public ABMSolicitud(Solicitud_Ab ab)
        {
            InitializeComponent();
            solicitud_ = ab;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            DialogResult = false;
            this.Close();
        }

        //evento para buscar un empleado 
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            string _razon = txtBuscar.Text + "%";
            if (e.Key == Key.Enter)
            {
                empleadosBuscador = bLL.EmpleadosPorNombre(_razon);
                lstResultadoBusqueda.ItemsSource = empleadosBuscador;
            }
        }

        // seleccion de un empleado de la lista de empleados
        private void btnSeleccionarDraw_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de empelado", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                if (lstResultadoBusqueda.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un empleado", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    empleado = lstResultadoBusqueda.SelectedItem as Empleado;
                    txbNombreEmpleado.Text = empleado.Nombre.ToString();
                    txtSolicitadoPor.Text = empleado.IdEmpleado.ToString();
                    txtBuscar.Text = "";
                    btnCerrarDraw.Command.Execute(Dock.Right);
                }
            }
        }


        //confirmacion de un item de la solicitud
        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            //cada vez  que confirmamos el item de la solicitud 
            //lo tenemos que agregar a la observablecollection detsolicitud

            Solicitud_ab_detalle s = new Solicitud_ab_detalle();

            s.NumItem = detsolicitud.Count + 1;
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar una cantidad ", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                s.Cantidad = Convert.ToInt16(txtCantidad.Text);
            }
            if (string.IsNullOrEmpty(txtDescriItem.Text))
            {
                MessageBox.Show("Debe ingresar una descripcion del item", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                s.DescriItem = txtDescriItem.Text;
            }

            s.Observacion = txtObservacion.Text;
            s.EstadoItem = "Pendiente";
            //agregamos el item a la lista de items de la solicitud
            detsolicitud.Add(s);
            dgItemsSolicitud.DataContext = detsolicitud;
            dgItemsSolicitud.ItemsSource = detsolicitud;
            //ponemos todo en blanco 
            txtCantidad.Text = "";
            txtDescriItem.Text = "";
            txtObservacion.Text = "";
            //cerramos el draw
            btnCerrarDrawBotton.Command.Execute(Dock.Bottom);

        }

        private void txtTitulo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTitulo.SelectAll();
        }

        //eliminar un item del datagrid del detalle
        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
                                   // indiceItemABorrar = dgDetalle.SelectedIndex;
                                   // detManteVhs.RemoveAt(indiceItemABorrar);
            indiceItemABorrar = dgItemsSolicitud.SelectedIndex;
            detsolicitud.RemoveAt(indiceItemABorrar);
            // modificamos el campo num item de los objetos de la coleccion 
            // como la clase de obejteos implementa InotifyPropertyChange se pueden reflejar
            //los cambios en la observablecollection
            for (int i = 0; i < detsolicitud.Count; i++)
            {
                detsolicitud[i].NumItem = i + 1;

            }
            dgItemsSolicitud.ItemsSource = detsolicitud;
        }

        //guardar , modificar una solicitud
        private void btnOperacion_Click(object sender, RoutedEventArgs e)
        {
            //codigo para grabar o modificar una solicitud
            //verificamos que tenga un empleado 
            if (string.IsNullOrEmpty(txtSolicitadoPor.Text))
            {
                MessageBox.Show("Debe ingresar el codigo del empleado que solicita", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                MessageBox.Show("Debe ingresar la imputacion de obra", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (dtpFNecesidad.SelectedDate == null)
            {
                MessageBox.Show("Debe ingresar una fecha de necesidad", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (detsolicitud.Count == 0)
            {
                MessageBox.Show("Debe ingresar un item  al detalle", "Aviso", MessageBoxButton.OK);
                return;
            }

            //en el caso de las solicitudes no se pueden modificar, por lo tanto, se deben anular o borrar
            // si todos los datos estan correctos entonces procedemos a armar la solicitud
            solicitud_ = ArmarSolicitud();
            // grabamos la solicitud
            int fila = gestion.SolicitudAB_alta(solicitud_);
            Solicitud_Ab ultima = new Solicitud_Ab();
            ultima = gestion.ObtenerUltimoIdSolicitud();
            //grabamos el detalle
            foreach (var item in detsolicitud)
            {
                item.IdSol = ultima.IdSol;
                int detalle = gestion.SolicitudAB_alta_detalle(item);
            }
            if (fila != 0)
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }

            this.Close();

        }

        private Solicitud_Ab ArmarSolicitud()
        {
            Solicitud_Ab s = new Solicitud_Ab();
            //encabezado
            s.AltaF = DateTime.Today.Date;
            s.CantidadItems = detsolicitud.Count;
            s.EstadoSolicitud = "Pendiente";
            s.EstadoTemporal = 1; // la solicitud es nueva
            s.FNecesidad = dtpFNecesidad.SelectedDate.Value;
            s.IdEmpleado = Convert.ToInt32(txtSolicitadoPor.Text);
            s.Imputacion = Convert.ToInt32(txtImputacion.Text);
            s.NomObra = txbNombreObra.Text;
            s.NomSeguidor = "";
            s.Nota = "no indica";
            s.PorcentajeCumplimiento = 0;
            s.SolicitadoPor = txbNombreEmpleado.Text;
            s.Titulo = txtTitulo.Text;
            s.UsuarioCreador = Contexto.CodUser;
            s.UsuarioSeguidor = 6;
            s.Vencida = 0;

            // aca viene el codigo para almacenar la imagen cuando se da de alta una nueva tarea.

            byte[] img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\nueva-tarea.png");
            s.ImgEstado = img;
            return s;
        }

        private void TxtDescriItem_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescriItem.SelectAll();

        }

        private void TxtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar.SelectAll();
        }

        private void TxtImputacion_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        // buscar una obra cuando se apreta enter
        private void TxtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //buscamos los datos de la obra
                int _imp_obra = Convert.ToInt32(txtImputacion.Text);
                obra = bLLObras.BuscarImputacion(_imp_obra);

                if (obra.Imputacion != 0) //si la imputacion no es cero, es decir existe la obra
                {
                    txbNombreObra.Text = obra.NombreObra;
                    txbCliente.Text = obra.Cliente;
                }
                else
                {
                    MessageBox.Show("La obra no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }
        }

        //validacion de rango de la fecha de necesidad
        private void DtpFNecesidad_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFNecesidad.SelectedDate.Value.Year < DateTime.Today.Date.Year)
            {
                MessageBox.Show("La fecha de necesidad debe corresponder al año actual", "Aviso", MessageBoxButton.OK);
                dtpFNecesidad.SelectedDate = DateTime.Today.Date;
                return;
            }
        }

        private void TxtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
