using MaterialDesignExtensions.Controls;
using UIDESK.uc.Laboratorio;
using UIDESK.uc.Mantenimientos;
using UIDESK.uc.Productos;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalAdmHyM.xaml
    /// </summary>
    public partial class PrincipalAdmHyM : MaterialWindow
    {
        public PrincipalAdmHyM()
        {
            InitializeComponent();
          
        }

        private void rbLaboratorio_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ucCalibraciones uc = new ucCalibraciones();
            ctc.Content = uc;
        }

        private void rbProduccion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ucMaquinasProduccion uc = new ucMaquinasProduccion();
            ctc.Content = uc;
        }

        private void rbProveedor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ucProveLab uc = new ucProveLab();
            ctc.Content = uc;
        }
    }
}
