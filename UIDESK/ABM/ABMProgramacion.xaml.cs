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
    /// Lógica de interacción para ABMProgramacion.xaml
    /// </summary>
    public partial class ABMProgramacion : MaterialWindow 
    {
        PlanificacionVH planificacionVH = new PlanificacionVH();
        Vehiculo vhplanificado = new Vehiculo();
        ObservableCollection<Vehiculo> dominios = new ObservableCollection<Vehiculo>();
        BLLVehiculos bLL = new BLLVehiculos();
        BLLObras bLLObras = new BLLObras();
        Obra obra = new Obra();
        public string _codigoABM;

        public ABMProgramacion()
        {
            InitializeComponent();



            gridPrincipal.DataContext = planificacionVH;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // validadmos los datos
            if (string.IsNullOrEmpty(txtDominio.Text))
            {
                MessageBox.Show("Debe indicar un dominio", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                MessageBox.Show("Debe indicar una imputacion", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpInicio.SelectedDate.Value == null)
            {
                MessageBox.Show("Debe elegir una fecha de inicio", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpHasta.SelectedDate.Value == null)
            {
                MessageBox.Show("Debe elegir una fecha de finalizacion", "Aviso", MessageBoxButton.OK);
                return;
            }


            planificacionVH.Dominio = vhplanificado.Dominio;
            planificacionVH.IdVh = vhplanificado.IdVh;
            planificacionVH.FDesde = dtpInicio.SelectedDate.Value;
            planificacionVH.FHasta = dtpHasta.SelectedDate.Value;
            planificacionVH.Imputacion = Convert.ToInt16(txtImputacion.Text);
            planificacionVH.Solicitante = txtSolicitado.Text;
            planificacionVH.Notas = txtNotas.Text;

            //bloque de verificacion de las fechas desde y hasta.
            bool _fechasCorrectas = bLL.ValidaFechasPlan(planificacionVH.IdVh, planificacionVH.FDesde, planificacionVH.FHasta);
            if (_fechasCorrectas == true)
            {
                MessageBox.Show("La fechas de planificacion ya existe para ese vehiculo", "Aviso", MessageBoxButton.OK);
                return;
            }


            int resultado = bLL.VehiculoAltaPlanificacion(planificacionVH);



            DialogResult = true;

        }



        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }





        private void txtDominio_KeyDown(object sender, KeyEventArgs e)
        {
            bool existeDomnio = false;
            if (e.Key == Key.Enter)
            {
                //validamos primero el dominio
                existeDomnio = bLL.ValidarDominio(txtDominio.Text);

                if (existeDomnio)
                {
                    vhplanificado = bLL.VehiculoBuscarUnDominio(txtDominio.Text);
                    if (vhplanificado.IdSf == 8)
                    {
                        MessageBox.Show("El vehiculo esta inactivo.Debe indicar otro dominio", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        txtModelo.Text = vhplanificado.Modelo;
                        txtMarca.Text = vhplanificado.NomMarca;
                        txtDescripcion.Text = vhplanificado.Descripcion;
                    }
                }
                else
                {
                    MessageBox.Show("El vehiculo no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }
        }



        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int _imputacion = Convert.ToInt16(txtImputacion.Text);
                obra = bLLObras.BuscarImputacion(_imputacion);
                if (obra.Imputacion == 0)
                {
                    MessageBox.Show("La obra no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    txbObra.Text = obra.NombreObra;
                }
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
