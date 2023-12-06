using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucGestionPPal.xaml
    /// </summary>
    public partial class ucGestionPPal : UserControl
    {
        public ucGestionPPal()
        {
            InitializeComponent();
        }

        private void rbDiario_Click(object sender, RoutedEventArgs e)
        {
            CarpetaTareasSector._tituloAppBar = "Diario";
           
            ucTareasGeneral uc = new ucTareasGeneral();
            ctcppal.Content = uc;

        }

        private void rbManteVh_Click(object sender, RoutedEventArgs e)
        {
            CarpetaTareasSector._tituloAppBar = "Plan Mantenimiento Vehiculos";
          
            ucPlanManteVh uc = new ucPlanManteVh();
            ctcppal.Content = uc;
        }

        private void rdManteHerra_Click(object sender, RoutedEventArgs e)
        {
            CarpetaTareasSector._tituloAppBar = "Plan Mantenimiento Herramientas";
           
            ucPlanManteHerra uc = new ucPlanManteHerra();
            ctcppal.Content = uc;
        }

        private void rdSolicitudesHerra_Click(object sender, RoutedEventArgs e)
        {
            CarpetaTareasSector._tituloAppBar = "Solicitudes de abastecimiento";
            
            ucSolicitudesAbastecimiento uc = new ucSolicitudesAbastecimiento();
            ctcppal.Content = uc;
        }

        private void rdNotas_Click(object sender, RoutedEventArgs e)
        {
            CarpetaTareasSector._tituloAppBar = "Notas del sistema";
            
            ucNotasSahmv6 uc = new ucNotasSahmv6();
            ctcppal.Content = uc;
        }

    }
}
