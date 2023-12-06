using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMAutorizacion.xaml
    /// </summary>
    public partial class ABMAutorizacion : MaterialWindow 
    {

        BLLEmpleados bLL = new BLLEmpleados();
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        ObservableCollection<Empleado> listaEmpleados = new ObservableCollection<Empleado>();
        List<Empleado> empleadosBuscador = new List<Empleado>();
        public ABMAutorizacion()
        {
            InitializeComponent();
        }

        private void txtEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Empleado empleadoBuscar = new Empleado();
                //buscamo el empleado en la tabla empleados
                // y rescatamos solo el id y el nombre
                string _nombre = txtEmpleado.Text; //nombre ingresado
                empleadoBuscar = bLL.BuscarPorNombre(_nombre);
                if (empleadoBuscar.IdEmpleado == 0)
                {
                    MessageBox.Show("El empleado no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    MessageBox.Show("Empleado encontrado", "Aviso", MessageBoxButton.OK);
                    listaEmpleados.Add(empleadoBuscar);
                    lstEmpleados.ItemsSource = listaEmpleados;
                }


            }
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // primero debemos ver que la lista no este vacia.
            if (lstEmpleados.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un empleado a la lista", "Aviso", MessageBoxButton.OK);
                return;
            }

            // si la lista contiene al menos un empleado entonces pasamos a armar los registos
            // para ello debemos recorre el contenido de la lista de empleados de lstEmpleados
            // e ir armando uno a uno los registros e insertandolos en la base de datos
            int contador = 0;
            foreach (var item in listaEmpleados)
            {
                int filaAfectada;
                Autorizacion_vh autorizacion_Vh = new Autorizacion_vh();
                autorizacion_Vh.IdVh = Convert.ToInt16(txtIdVh.Text);
                autorizacion_Vh.IdEmpleado = item.IdEmpleado;
                autorizacion_Vh.AltaF = DateTime.Today.Date;
                autorizacion_Vh.Finicio = DateTime.Today.Date;
                autorizacion_Vh.Estado = "Activa";
                filaAfectada = bLLVehiculos.AltaAutorizacion(autorizacion_Vh);
                contador = contador + 1;
            }

            if (contador != 0)
            {
                MessageBox.Show("Se agregaron los registros", "Aviso", MessageBoxButton.OK);
            }

            DialogResult = true;
            
        }

        private void btnDelEmpleado_Click(object sender, RoutedEventArgs e)
        {

            listaEmpleados.RemoveAt(lstEmpleados.Items.IndexOf(lstEmpleados.SelectedItem));
            lstEmpleados.ItemsSource = listaEmpleados;
        }

        private void txtEmpleado_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmpleado.SelectAll();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            string _razon = txtBuscar.Text + "%";
            if (e.Key == Key.Enter)
            {
                empleadosBuscador = bLL.EmpleadosPorNombre(_razon);
                lstResultadoBusqueda.ItemsSource = empleadosBuscador;
            }
        }

        private void btnSeleccionarDraw_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            empleado = lstResultadoBusqueda.SelectedItem as Empleado;
            txtEmpleado.Text = empleado.Nombre.ToString();
            btnCerrarDraw.Command.Execute(Dock.Right);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

       

        private void btnBuscarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            string _razon = txtBuscar.Text + "%";
            empleadosBuscador = bLL.EmpleadosPorNombre(_razon);
            lstResultadoBusqueda.ItemsSource = empleadosBuscador;
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
