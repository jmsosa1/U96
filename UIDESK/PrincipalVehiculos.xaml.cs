using MaterialDesignExtensions.Controls;
using System.Windows;
using UIDESK.uc.Presupuestos;
using UIDESK.uc.Vehiculos;


namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalVehiculos.xaml
    /// </summary>
    public partial class PrincipalVehiculos : MaterialWindow
    {
        public PrincipalVehiculos()
        {
            InitializeComponent();
            ucVehiculosGeneral ucVehiculosGeneral = new ucVehiculosGeneral();
            ccGral.Content = ucVehiculosGeneral;
        }

        private void rbActualidad_Click(object sender, RoutedEventArgs e)
        {
            ucVehiculoActualidad ucVehiculoActualidad = new ucVehiculoActualidad();
            ccGral.Content = ucVehiculoActualidad;
        }

        private void rbRegMante_Click(object sender, RoutedEventArgs e)
        {
            ucVehiculosMantenimientos ucVehiculosMantenimientos = new ucVehiculosMantenimientos();
            ccGral.Content = ucVehiculosMantenimientos;
        }

        private void rbPlanObra_Click(object sender, RoutedEventArgs e)
        {
            ucVehiculosProgramacion ucVehiculosProgramacion = new ucVehiculosProgramacion();
            ccGral.Content = ucVehiculosProgramacion;
        }

        private void rbConfiguraciones_Click(object sender, RoutedEventArgs e)
        {
            ucVehiculoAjustes ucVehiculoAjustes = new ucVehiculoAjustes();
            ccGral.Content = ucVehiculoAjustes;
        }

        private void rbRemitos_Click(object sender, RoutedEventArgs e)
        {
            ucRemitos ucRemitos = new ucRemitos();
            ccGral.Content = ucRemitos;
        }

        private void rbListado_Click(object sender, RoutedEventArgs e)
        {
            ucVehiculosGeneral ucVehiculosGeneral = new ucVehiculosGeneral();
            ccGral.Content = ucVehiculosGeneral;
        }

        private void rbPresupuestos_Click(object sender, RoutedEventArgs e)
        {
            ucPrespuestos ucPre = new ucPrespuestos();
            ccGral.Content = ucPre;
        }
    }
}
