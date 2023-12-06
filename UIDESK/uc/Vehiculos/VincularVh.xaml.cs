using System.Windows;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para VincularVh.xaml
    /// </summary>
    public partial class VincularVh : Window
    {
        public VincularVh()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea Cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {


                this.Close();
            }
        }

        private void BtnVincular_Click(object sender, RoutedEventArgs e)
        {
            //aca va el codigo para vincular un vehiculo a otro

        }
    }
}
