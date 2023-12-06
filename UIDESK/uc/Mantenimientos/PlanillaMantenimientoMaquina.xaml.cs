using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para PlanillaMantenimientoMaquina.xaml
    /// </summary>
    public partial class PlanillaMantenimientoMaquina : MaterialWindow 
    {
        Producto _producto = new Producto();
        Mpm mpm = new Mpm();
        DateTime _fincio;
        DateTime _fvencimiento;
        string _unidad;
        ObservableCollection<MpmDetalle> detalle = new ObservableCollection<MpmDetalle>();
        BLLLaboratorio coreLab = new BLLLaboratorio();
        int _operacion; // indica el tipo de operacion  1= alta , 2 editar

        public PlanillaMantenimientoMaquina(Producto producto) //pasamos el producto seleccionado como parametro
        {
            InitializeComponent();
            _producto = producto;
          
            stkDatosMaquina.DataContext = _producto; // asignamos le producto pasado como data contex

            mpm = coreLab.ObtenerMPMUnaMaquina(producto.IdProducto);
            if (mpm.Idmpm > 0) // se trata de una actualizacion
            {
                detalle = coreLab.ObtenerDetalleMPMUnaMaquina(mpm.Idmpm);
                dgTareas.ItemsSource = detalle; // en este momento detalle es null, por lo tanto la grilla esta "vacia"
                dgTareas.DataContext = detalle;
                _operacion = 2;
            }
            else
            {
                //se trata de una nueva planilla
                dgTareas.ItemsSource = detalle; // en este momento detalle es null, por lo tanto la grilla esta "vacia"
                dgTareas.DataContext = detalle;
                _operacion = 1;
            }
            
            
           

        }

        //asignamos valor a las propiedades del objeto mpm
        private void CrearMPm()
        {
            mpm.AltaF = DateTime.Today.Date;
            mpm.IdProducto = _producto.IdProducto;
            mpm.Estado = 1;
            mpm.CantAcuUnidades = 0;
            mpm.Cumplimiento = 0;
            mpm.Situacion = "Pendiente";
            mpm.CantidadTareas = detalle.Count();
        }

        //seleccion de unidad dias
        private void rbDias_Click(object sender, RoutedEventArgs e)
        {
            _unidad = "Dias";
            int _validez = Convert.ToInt32(txtFrecuencia.Text);
            DateTime date = dtFInicio.SelectedDate.Value.AddDays(_validez);
            
            dtFVencimiento.SelectedDate = date;
            _fvencimiento = date;
            DateTime _fhoy = DateTime.Today;
          

            if (_fvencimiento < date)
            {
                // si la fecha de vencimiento es menor a la del dia de hoy el gap es positivo, o sea te pasaste dias
                txtGap.Text = (_fvencimiento.Date - DateTime.Today).TotalDays.ToString();
            }
            else
            {
                //si no, el gap es negativo, significa que faltan dias para el vencimiento
                txtGap.Text = ((_fvencimiento.Date - DateTime.Today).TotalDays * -1).ToString();
            }



            //txtGap.Text =  (_validez * -1).ToString();
        }

        //selecciona de unidad horas
        private void rbHoras_Click(object sender, RoutedEventArgs e)
        {
            _unidad = "Horas";
            dtFVencimiento.SelectedDate = null;
            int _gap = Convert.ToInt32(txtFrecuencia.Text) * -1;
            txtGap.Text = _gap.ToString();
        }

        //si se selecciona una fecha distinta se cambia el valor del campo correspondiente
        private void dtFInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            _fincio = dtFInicio.SelectedDate.Value.Date;
        }

       
        //agregar una nueva tarea al detalle
        private void addNuevaTarea_Click(object sender, RoutedEventArgs e)
        {
            MpmDetalle tarea = new MpmDetalle();
            tarea.ElementoObservable = txtElementoObservable.Text;
            tarea.DescriTarea = txtDescriTarea.Text;
            tarea.EstadoTarea = "Activa";
            tarea.FechaInicio = dtFInicio.SelectedDate.Value.Date;
            tarea.Unidad = _unidad;
            if (tarea.Unidad =="Dias")
            {
                tarea.FechaVencimiento = dtFVencimiento.SelectedDate.Value;
              
            }
            else
            {
                tarea.FechaVencimiento = null;
            }
            tarea.Frecuencia =Convert.ToInt32(txtFrecuencia.Text);
            if (string.IsNullOrEmpty(txtGap.Text))
            {
                tarea.Gap = 0;
            }
            else
            {
                tarea.Gap = Convert.ToInt32(txtGap.Text);
            }
            tarea.Alarma = tarea.Gap / 2;

            //agregamos la tarea al detalle de tareas
            //calculamos el valor de la situacion de la tarea en funcion del gap solo si la fecha de vencimiento es no nula
            if (tarea.Gap < 0 && tarea.Gap < -15) // si el gap es negativo y mayor que - 15 
            {
                tarea.SituacionTarea = "Normal";
            }
            else
            {
                if (tarea.Gap < 0 && tarea.Gap > -15) // si el gap es negativo y menor que -15
                {
                    tarea.SituacionTarea = "Proxima Vencer";
                }
                else
                {
                    tarea.SituacionTarea = "Vencida"; // si el gap es positivo
                    
                }
            }
          
            detalle.Add(tarea);
            dgTareas.ItemsSource = detalle;
            dgTareas.DataContext = detalle;
        }

        //llamar al formulario para agregar tareas
        private void addTarea_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtElementoObservable.Text))
            {
                MessageBox.Show("Primero debe indicar un elemento observable", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
               
                txtGap.Text = 0.ToString();
                dtFInicio.SelectedDate = DateTime.Today.Date;
                dtFVencimiento.SelectedDate = null;
                txtFrecuencia.Text = 0.ToString();
               
            }
           
        }

        //guardar la planilla
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // accion para guardar 
            //chekeamos que haya algo en del detalle
            if (detalle.Count == 0)
            {
                MessageBox.Show("No hay un detalle en la planilla. Debe ingresar al menos un item", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {

                if (_operacion==1)// alta
                {
                    // grabamos el encabezado
                    CrearMPm();
                    coreLab.AgregarMPM(mpm);
                    //obtenemos el ultimo id mpm
                    int _id = coreLab.ObtenerUltimoIdMPM();
                    //grabamos el detalle
                    foreach (var item in detalle)
                    {
                        item.Idmpm = _id;
                        coreLab.AgregarDetalleMPM(item);
                    }
                }
                else
                {
                    //se trata de una actualizacion
                    //borrar el detalle primero 
                    coreLab.BorrarDetalleMPM(mpm.Idmpm);
                    
                    //grabamos el detalle usando el campo idmpm del mpm que buscamos en el constructor
                    foreach (var item in detalle)
                    {
                        item.Idmpm = mpm.Idmpm;
                        coreLab.AgregarDetalleMPM(item);
                    }
                }
               

                //cerramos
                DialogResult = true;
                this.Close();
            }
            
        }

        

        private void txtFrecuencia_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFrecuencia.SelectAll();
        }

        private void btnBorrarItem_Click(object sender, RoutedEventArgs e)
        {
            // borramos un tarea del detalla
            MpmDetalle mpmDetalle = dgTareas.SelectedItem as MpmDetalle;
            // antes de permitir borrar , debemos evaluar si la tarea ya esta ejecutada 
           
            MessageBoxResult result = MessageBox.Show("Eliminar la tarea seleccionada?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //llamamos al metodo de borrado
                //coreLab.BorrarTarea(mpmDetalle.Id, mpmDetalle.Idmpm);
                //refrescamos la pantalla
                //detalle = coreLab.ObtenerDetalleMPMUnaMaquina(mpm.Idmpm);
                //dgTareas.ItemsSource = detalle; // en este momento detalle es null, por lo tanto la grilla esta "vacia"
                //dgTareas.DataContext = detalle;
                detalle.RemoveAt(dgTareas.SelectedIndex);
            }

        }

        private void btnBorrarPlanilla_Click(object sender, RoutedEventArgs e)
        {
            // borrar la planilla implica que se borre tambien el detalle
            MessageBoxResult result = MessageBox.Show("Esta seguro de eliminar la planilla y su detalle?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //llamamos al metodo de borrado
                coreLab.BorrarUnMPM(mpm.Idmpm);
                MessageBox.Show("Se borro la planilla de mantenimiento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                //cerramos el formulario
                DialogResult = true;
                this.Close();

            }
        }
    }
}
