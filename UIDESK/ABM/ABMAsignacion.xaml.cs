using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMAsignacion.xaml
    /// </summary>
    public partial class ABMAsignacion : MaterialWindow 
    {
        BLLEmpleados bLLEmpleados = new BLLEmpleados();
        BLLObras bLLObras = new BLLObras();
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        Vehiculo vehiculo = new Vehiculo();
        Obra obra = new Obra();
        List<Empleado> listaEmpleados = new List<Empleado>();
        Asignacion_vh asignacion_Vh = new Asignacion_vh();
        Empleado empleado = new Empleado();


        public ABMAsignacion(int _idvh)
        {
            InitializeComponent();

            listaEmpleados = bLLVehiculos.VehiculoEmpleadosAutorizados(_idvh);
            cmbEmpleado.ItemsSource = listaEmpleados; // cargamos la lista de empleados
        }




        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbObra.Text))
            {
                MessageBox.Show("Debe ingresar una imputacion de obra", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpInicio.SelectedDate == null)
            {
                MessageBox.Show("Debe elegir una fecha de incio", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpFin.SelectedDate < dtpInicio.SelectedDate)
            {
                MessageBox.Show("La fecha de finalizacion no puede ser menor que la de inicio", "Aviso", MessageBoxButton.OK);
                return;
            }
            //buscamos los datos del vehiculo
            vehiculo = bLLVehiculos.VehiculoBuscarUnDominio(txtDominio.Text.ToString());
            //tomamos el dato del empleado
            empleado = cmbEmpleado.SelectedItem as Empleado;

            //seteamos las propiedades del objeto asignacion
            asignacion_Vh.IdVh = vehiculo.IdVh;
            asignacion_Vh.Imputacion = obra.Imputacion;
            asignacion_Vh.IdEmpleado = empleado.IdEmpleado;
            asignacion_Vh.FechaIn = dtpInicio.SelectedDate.Value.Date;

            if (dtpFin.SelectedDate != null)
            {
                asignacion_Vh.FechaFin = dtpFin.SelectedDate.Value.Date;
            }
            else
            {
                asignacion_Vh.FechaFin = null;
            }

            asignacion_Vh.EstadoAsignacion = "Activa";
            asignacion_Vh.SituacionAsignacion = "En Curso";
            asignacion_Vh.IdCatevh = vehiculo.IdCate;
            asignacion_Vh.HsAcuObra = 0;
            asignacion_Vh.KmAcuObra = 0;
            asignacion_Vh.AltaF = DateTime.Today.Date;
            int _asignar = bLLVehiculos.AltaAsignacion(asignacion_Vh);//grabamos el registro
                                                                      //verificamos si existe en la tabla balance obra vehiculos
                                                                      // bool existeBalance = bLLVehiculos.ExisteBalanceObra(asignacion_Vh.IdVh, asignacion_Vh.Imputacion);
                                                                      // if (existeBalance==false)
                                                                      //{
                                                                      //creamos la entrada en la tabla balanceobravehiculos
                                                                      //   int crearBOVH = bLLVehiculos.AltaBOVh(asignacion_Vh.IdVh, asignacion_Vh.Imputacion, vehiculo.IdTipoVh, vehiculo.IdCate);

            //}
            if (obra.Imputacion == 13) // si es asigando a mantenimiento
            {
                bLLVehiculos.CambioSF(vehiculo.IdVh, 3);
            }
            if (obra.Imputacion == 17) // si es asignado a venta
            {
                bLLVehiculos.CambioSF(vehiculo.IdVh, 5);
            }
            else
            {
                bLLVehiculos.CambioSF(vehiculo.IdVh, 1); // cambiamos el estado de ls situacion fisica a "1"(obra)
            }

            if (_asignar != 0)
            {
                MessageBox.Show("El registro se grabo con exito", "Aviso", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("El registro no pudo se grabado", "Aviso", MessageBoxButton.OK);

            }
            DialogResult = true;
           
        }

        private void txtImputacion_LostFocus(object sender, RoutedEventArgs e)
        {
            //buscamos los datos de la obra
            obra = bLLObras.BuscarImputacion(Convert.ToInt16(txtImputacion.Text));

            if (obra.Imputacion != 0) //si la imputacion no es cero, es decir existe la obra
            {
                txbObra.Text = obra.NombreObra;
                txbCliente.Text = obra.Cliente;
            }
            else
            {
                MessageBox.Show("La obra no existe", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

       

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
