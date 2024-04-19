using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ConsumoHs.xaml
    /// </summary>
    public partial class RegistrarConsumo : MaterialWindow 
    {
        public string _tipoConsumo; // contiene la seleccion del tipo de consumo que se esta registrando
        ConsumoCombustible consumoReg = new ConsumoCombustible();
        BLLVehiculos bLL = new BLLVehiculos();
        BLLObras bllObras = new BLLObras();
        Vehiculo vehiculo = new Vehiculo();
        Obra obra = new Obra();
        ObservableCollection<plan_inspeccion> plan_Inspeccions = new ObservableCollection<plan_inspeccion>();

        public RegistrarConsumo()
        {
            InitializeComponent();


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //validar las entradas
            if (string.IsNullOrEmpty(txtObra.Text)) // tienen que haber un numero de imputacion
            {
                MessageBox.Show("Debe ingresar una imputacion de obra", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpFechaConsumo.SelectedDate == null)// se debe seleccionar una fecha de consumo
            {
                MessageBox.Show("Debe seleccionar una fecha de registro de consumo", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text)) // la cantidad no nuede estar vacia
            {
                MessageBox.Show("Debe ingresar una cantidad", "Anio", MessageBoxButton.OK);
                return;
            }
            // fin validacion

            // buscamos los datos del vehiculo
            vehiculo = bLL.VehiculoBuscarUnDominio(txtDominio.Text.ToString());
            // buscamos datos del combustible 
            Combustible combustible = new Combustible();
            combustible = bLL.BuscarUnCombustible(vehiculo.IdCombustible);
            //empezamos a setear 
            consumoReg.IdVh = vehiculo.IdVh;
            if (_tipoConsumo == "KM")
            {
                consumoReg.KmRecorrido = Convert.ToDecimal(txtCantidad.Text);


                if (vehiculo.KmLitro != 0)
                {
                    consumoReg.LitrosCmbKM = consumoReg.KmRecorrido / vehiculo.KmLitro; //calculamos listros consumidos en funcion de los km
                    consumoReg.CostoCmbKm = consumoReg.LitrosCmbKM * combustible.PrecioLitroActual;// calculamos el costo de los litros consumidos
                }
                consumoReg.HorasTrabajo = 0;
            }
            else
            {
                consumoReg.HorasTrabajo = Convert.ToDecimal(txtCantidad.Text);

                if (vehiculo.LitroHora != 0)
                {
                    consumoReg.LitrosCmbHoras = consumoReg.HorasTrabajo * vehiculo.LitroHora; // calculamos litros consumidos en funcion de las horeas 
                    consumoReg.CostoCmbHoras = consumoReg.LitrosCmbHoras * combustible.PrecioLitroActual;// calculamos el costo de los litros consumidos

                }
                consumoReg.KmRecorrido = 0;

            }

            consumoReg.FechaReg = DateTime.Today.Date;
            consumoReg.FechaConsumo = dtpFechaConsumo.SelectedDate.Value;

            consumoReg.Imputacion = obra.Imputacion;
            consumoReg.Meskm = consumoReg.FechaConsumo.ToString("MMMM");
            consumoReg.AnioConsumo = consumoReg.FechaConsumo.Year;
            consumoReg.PrecioLitro = combustible.PrecioLitroActual;


            //registramos el consumo de combustible en la tabla consumocombustibles
            int _fila = bLL.RegistrarConsumo(consumoReg);
            MessageBox.Show("Se registro el consumo correctamente", "Aviso", MessageBoxButton.OK);

            //obtener las tareas  activas , puede haber varias  a la vez que requieran distintos mantenimientos
            plan_Inspeccions = bLL.PlanInspeccionTareasActivasUnVehiculo(vehiculo.IdVh);

            if (_tipoConsumo == "KM")
            {
                bLL.VehiculoActualizaKmAcumulados(vehiculo.IdVh, consumoReg.KmRecorrido, consumoReg.Imputacion);
                // calculamos los consumos por hora km si el valor de km x litro es distinto de cero
                //if (vehiculo.KmLitro != 0)
                //{
                //  bLL.RecalcularConsumoCombustibleKM(vehiculo.IdVh, vehiculo.KmLitro, combustible.PrecioLitroActual);

                //}
                //else
                //{
                //  MessageBox.Show("No se pudo calcular el consumo de combustible por falta de datos", "Aviso", MessageBoxButton.OK);
                //}
                //actualizamos el plan de inspeccion para el vehiculo en funcion de los km consumidos
                //actualizar el gap de consumo del vehiculo
                foreach (var item in plan_Inspeccions)
                {
                    // recorremos las tareas activas y vamos calculando el gap, alarma y vencimiento
                    bLL.PlanInspeccionCalcularGAP(item.Idvh, consumoReg.KmRecorrido, item.Idplan, DateTime.Today);

                }
                
            }
            else
            {
                bLL.VehiculoActualizaHorasAcumuladas(vehiculo.IdVh, consumoReg.HorasTrabajo, consumoReg.Imputacion);
                // calculamos los consumos por hora si el valor de litro x hora es distinto de cero
                //if (vehiculo.LitroHora != 0)
                //{
                //  bLL.RecalcularConsumoCombustibleHS(vehiculo.IdVh, vehiculo.LitroHora, combustible.PrecioLitroActual);
                //}
                //else
                //{
                //  MessageBox.Show("No se pudo calcular el consumo de combustible por falta de datos", "Aviso", MessageBoxButton.OK);
                //}

                //actualizamos el plan de inspeccion para el vehiculo en funcion de las hs consumidos
                //actualizar el gap de consumo del vehiculo
                foreach (var item in plan_Inspeccions)
                {
                    // recorremos las tareas activas y vamos calculando el gap, alarma y vencimiento
                    bLL.PlanInspeccionCalcularGAP(item.Idvh, consumoReg.HorasTrabajo, item.Idplan, DateTime.Today);

                }
               
            }
            DialogResult = true;
            this.Close();
        }



        private void txtObra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                obra = bllObras.BuscarImputacion(Convert.ToInt16(txtObra.Text));
                if (obra.Imputacion != 0)
                {
                    txbNombreObra.Text = obra.NombreObra;
                }
                else
                {
                    MessageBox.Show("La obra no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }

            }
        }



        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtObra_LostFocus(object sender, RoutedEventArgs e)
        {
            obra = bllObras.BuscarImputacion(Convert.ToInt16(txtObra.Text));
            if (obra.Imputacion != 0)
            {
                txbNombreObra.Text = obra.NombreObra;
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
