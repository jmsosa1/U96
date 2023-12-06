using MaterialDesignExtensions.Controls;
using System.Windows;
using UIDESK.uc.Obras;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalObras.xaml
    /// </summary>
    public partial class PrincipalObras : MaterialWindow
    {
        public PrincipalObras()
        {
            InitializeComponent();
            //ucObrasGeneral uc = new ucObrasGeneral();
            //ccGral.Content = uc;
        }


        private void grdTitulo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void rbRemitos_Click(object sender, RoutedEventArgs e)
        {
            ucRemitosObras uc = new ucRemitosObras();
            ctc.Content = uc;
        }

        private void rbBalanceObra_Click(object sender, RoutedEventArgs e)
        {
            ucBalanceObra uc = new ucBalanceObra();
            ctc.Content = uc;
        }

        private void rbListado_Click(object sender, RoutedEventArgs e)
        {
            ucObrasGeneral uc = new ucObrasGeneral();
            ctc.Content = uc;
        }
    }
}
