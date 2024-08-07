using MaterialDesignExtensions.Controls;
using System.Windows;
using UIDESK.Remitos;
using UIDESK.uc.Empleados;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalEmpleados.xaml
    /// </summary>
    public partial class PrincipalEmpleados : MaterialWindow
    {
        public PrincipalEmpleados()
        {
            InitializeComponent();
           // ucDetEmpleadoTransitioner uc = new ucDetEmpleadoTransitioner();
            //ctc.Content = uc;
        }

        private void btnMinizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rdbEmpleados_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnNormalizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void grdTitulo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void rbListado_Click(object sender, RoutedEventArgs e)
        {
            ucGeneralEmpleados uc = new ucGeneralEmpleados();
            ctc.Content = uc;
        }

        private void rbIndumentaria_Click(object sender, RoutedEventArgs e)
        {
            ucDSIDDI uc = new ucDSIDDI();
            ctc.Content = uc;
        }
    }
}
