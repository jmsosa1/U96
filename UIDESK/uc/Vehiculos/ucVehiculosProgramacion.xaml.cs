using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.ABM;


namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucVehiculosProgramacion.xaml
    /// </summary>
    public partial class ucVehiculosProgramacion : UserControl
    {
        BLLVehiculos bLL = new BLLVehiculos();
        ObservableCollection<PlanificacionVH> planificacionVHs = new ObservableCollection<PlanificacionVH>();

        public ucVehiculosProgramacion()
        {
            InitializeComponent();
            planificacionVHs = bLL.ListarTodasPlanificaciones();
            dgRoadMap.DataContext = planificacionVHs;
            dgRoadMap.ItemsSource = planificacionVHs;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            PlanificacionVH planificacion = new PlanificacionVH();
            planificacion.FDesde = DateTime.Today.Date;
            planificacion.FHasta = DateTime.Today.Date;
            ABMProgramacion aBMProgramacion = new ABMProgramacion();
            //aBMProgramacion.txbOperacion.Text = "Alta Programacion";
            aBMProgramacion._codigoABM = "A";
            if (aBMProgramacion.ShowDialog() == true)
            {
                MessageBox.Show("El registro se grabo con exito", "Aviso", MessageBoxButton.OK);
                planificacionVHs = bLL.ListarTodasPlanificaciones();
                dgRoadMap.DataContext = planificacionVHs;
                dgRoadMap.ItemsSource = planificacionVHs;
            }
        }


        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgRoadMap.SelectedIndex = -1;
        }



        private void btnCambiarEstado_Click_1(object sender, RoutedEventArgs e)
        {
            PlanificacionVH vH = new PlanificacionVH();

            vH = dgRoadMap.SelectedItem as PlanificacionVH;
            if (vH.Estado == "Prevision")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Desea cambiar el estaodo de la planificacion?", "Aviso", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {


                    vH.Estado = "En Curso";

                    bool _cambioEstado = bLL.VehiculoCambioEstadoPlanificacion(vH.IdPl, vH.Estado);

                    if (_cambioEstado == true)
                    {
                        MessageBox.Show("Se cambio el estado del item", "Aviso", MessageBoxButton.OK);

                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede cambiar el estado. La planificacion ya esta en curso", "Aviso", MessageBoxButton.OK);

            }



            planificacionVHs = bLL.ListarTodasPlanificaciones();
            dgRoadMap.DataContext = planificacionVHs;
            dgRoadMap.ItemsSource = planificacionVHs;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            PlanificacionVH vH = new PlanificacionVH();
            vH = dgRoadMap.SelectedItem as PlanificacionVH;
            string _dominio = vH.Dominio;
            MessageBoxResult messageBoxResult;
            messageBoxResult = MessageBox.Show("Desea eliminar la programacion para el dominio: " + _dominio + "", "Aviso", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                int _filaBorrada = bLL.VehiculoBajaPlanificacion(vH.IdPl);
                if (_filaBorrada == 1)
                {
                    MessageBox.Show("Se elimino el registro", "Aviso", MessageBoxButton.OK);
                    planificacionVHs = bLL.ListarTodasPlanificaciones();
                    dgRoadMap.DataContext = planificacionVHs;
                    dgRoadMap.ItemsSource = planificacionVHs;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el registro", "Aviso", MessageBoxButton.OK);

                }
            }

        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            PlanificacionVH vH = new PlanificacionVH();
            vH = dgRoadMap.SelectedItem as PlanificacionVH;

            if (vH != null)
            {


                RechazoPlanificacion anularProgramacion = new RechazoPlanificacion(vH);
                anularProgramacion.txbOperacion.Text = "Rechazar Programacion";
                if (anularProgramacion.ShowDialog() == true)
                {
                    MessageBox.Show("El registro se rechazo  con exito", "Aviso", MessageBoxButton.OK);
                    planificacionVHs = bLL.ListarTodasPlanificaciones();
                    dgRoadMap.DataContext = planificacionVHs;
                    dgRoadMap.ItemsSource = planificacionVHs;
                }
            }
            else
            {
                MessageBox.Show("Debe elegir un registro para rechaza", "Aviso", MessageBoxButton.OK);
                return;
            }




        }


    }
}
