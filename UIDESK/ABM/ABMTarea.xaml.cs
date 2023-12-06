using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMTarea.xaml
    /// </summary>
    public partial class ABMTarea : Window
    {
        BLLGestion gestion = new BLLGestion();
        TareaSector tareaSector = new TareaSector();
        DetTareaSector det = new DetTareaSector();
        ObservableCollection<DetTareaSector> detTareas = new ObservableCollection<DetTareaSector>();
        public string operacion = "";

        public ABMTarea(TareaSector t)
        {
            InitializeComponent();
            tareaSector = t;
            grdDatosTarea.DataContext = tareaSector;

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtTitulo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTitulo.SelectAll();
        }


        private void btnOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MessageBox.Show("Debe ingresar un titulo para la tarea", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbImportancia.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una importancia para la tarea", "Aviso", MessageBoxButton.OK);
                return;
            }




            if (operacion == "A")
            {
                if (dgActividades.Items.Count == 0)
                {
                    MessageBox.Show("Debe agregar un item al detalle", "Aviso", MessageBoxButton.OK);
                    return;
                }
                // dar de alta una tarea
                TareaSector nuevaTarea = new TareaSector();
                nuevaTarea = ArmarEncabezadoTarea();

                int fila = gestion.TareaNuevaAlta(nuevaTarea);
                //obtenemos el ultimo idtarea para poder grabar el detalle
                TareaSector ultimaid = new TareaSector();
                ultimaid = gestion.ObetenerUltimoIdTarea();
                //grabamos el detalle
                foreach (var item in detTareas)
                {
                    item.IdTarea = ultimaid.IdTarea;
                    int detalle = gestion.DetalleTareaAlta(item);
                }

                if (fila != 0)
                {
                    MessageBox.Show("Se dio de alta la nueva tarea", "Aviso", MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show("NO se pudo grabar la tarea", "Aviso", MessageBoxButton.OK);
                }
                DialogResult = true;
                this.Close();

            }
            else
            {
                if (operacion == "M")
                {
                    //modificar titulo y fecha de necesidad
                    TareaSector modificartarea = new TareaSector();
                    modificartarea.IdTarea = tareaSector.IdTarea;
                    modificartarea.TituloTarea = txtTitulo.Text;
                    modificartarea.Fnecesidad = dtpFNecesidad.SelectedDate != null ? (DateTime?)dtpFNecesidad.SelectedDate.Value.Date : null;
                    modificartarea.ImportanciaTarea = ((ComboBoxItem)cmbImportancia.SelectedItem).Content.ToString();
                    modificartarea.Ultimamodi = DateTime.Today.Date;
                    int fila = gestion.TareaModificar(modificartarea);
                    if (fila != 0)
                    {
                        MessageBox.Show("Se dio de alta la nueva tarea", "Aviso", MessageBoxButton.OK);

                    }
                    else
                    {
                        MessageBox.Show("NO se pudo grabar la tarea", "Aviso", MessageBoxButton.OK);
                    }
                    DialogResult = true;
                    this.Close();
                }
                else
                {

                    //nada por aca todavia<z
                }
            }
        }

        private void txtDescriActividad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescriActividad.SelectAll();
        }

        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            DetTareaSector d = new DetTareaSector();
            d.NumItem = detTareas.Count + 1;
            d.EstadoItem = "Pendiente";
            d.DescriTarea = txtDescriActividad.Text;
            detTareas.Add(d);
            dgActividades.DataContext = detTareas;
            dgActividades.ItemsSource = detTareas;
            txtDescriActividad.Text = "";
            btnCerrarDrawBotton.Command.Execute(Dock.Bottom);
        }

        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
                                   // indiceItemABorrar = dgDetalle.SelectedIndex;
                                   // detManteVhs.RemoveAt(indiceItemABorrar);
            indiceItemABorrar = dgActividades.SelectedIndex;

            detTareas.RemoveAt(indiceItemABorrar); // borramos el item

            // modificamos el campo num item de los objetos de la coleccion 
            // como la clase de obejteos implementa InotifyPropertyChange se pueden reflejar
            //los cambios en la observablecollection
            for (int i = 0; i < detTareas.Count; i++)
            {
                detTareas[i].NumItem = i + 1;

            }

            dgActividades.ItemsSource = detTareas;

        }

        public TareaSector ArmarEncabezadoTarea()
        {
            TareaSector nuevaTarea = new TareaSector();
            nuevaTarea.UsuarioCreador = Contexto.CodUser;
            nuevaTarea.TituloTarea = txtTitulo.Text;
            nuevaTarea.Fnecesidad = dtpFNecesidad.SelectedDate != null ? (DateTime?)dtpFNecesidad.SelectedDate.Value.Date : null;

            nuevaTarea.Fcierre = null;
            nuevaTarea.EstadoTemporal = 1;
            nuevaTarea.EstadoTarea = "Pendiente";
            nuevaTarea.DiasEjecucion = 0;
            nuevaTarea.CantidadItems = detTareas.Count;
            nuevaTarea.AltaF = DateTime.Today.Date;
            nuevaTarea.Vencida = 0;
            nuevaTarea.PorcentajeCumplimiento = 0;
            nuevaTarea.ImportanciaTarea = ((ComboBoxItem)cmbImportancia.SelectedItem).Content.ToString();
            nuevaTarea.Ultimamodi = DateTime.Today.Date;
            // aca viene el codigo para almacenar la imagen cuando se da de alta una nueva tarea.

            byte[] img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\nueva-tarea.png");
            nuevaTarea.ImageEstadoTemp = img;

            return nuevaTarea;
        }
    }
}
