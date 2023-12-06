using BLL;
using System.Windows;
using System.Windows.Controls;
using UIDESK.Remitos;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucStock.xaml
    /// </summary>
    public partial class ucStock : UserControl
    {

        #region Declarativas
        BLLRemito coreRemitos = new BLLRemito();

        #endregion
        public ucStock()
        {
            InitializeComponent();


        }

        private void btnConsultaGrupo_Click(object sender, RoutedEventArgs e)
        {
            ucConsultasExistencias uc = new ucConsultasExistencias();
            ccGestionStk.Content = uc;
        }

        private void btnNuevoVCD_Click(object sender, RoutedEventArgs e)
        {
            VCD vCD = new VCD();

            if (vCD.ShowDialog() == true)
            {
                MessageBox.Show("Se genero el consumo del producto en deposito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }


        private void btnGestionIndu_Click(object sender, RoutedEventArgs e)
        {
            ucGestionIndumentaria uc = new ucGestionIndumentaria();
            ccGestionStk.Content = uc;
        }

        private void btnNuevoDIP_Click(object sender, RoutedEventArgs e)
        {
            DIP dIP = new DIP();
            if (dIP.ShowDialog() == true)
            {
                MessageBox.Show("Se registro el ingreso del producto a deposito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnNuevoDSD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConsultaStock_Click(object sender, RoutedEventArgs e)
        {
            ucSituacionStock uc = new ucSituacionStock();
            ccGestionStk.Content = uc;
        }

        private void btnConsultaEntregas_Click(object sender, RoutedEventArgs e)
        {
            ucGestionEPPEntregas ucGestionEPP = new ucGestionEPPEntregas();
            ccGestionStk.Content = ucGestionEPP;
        }

        private void btnNuevoDSO_Click(object sender, RoutedEventArgs e)
        {
            DSO nuevoRemito = new DSO();
            nuevoRemito._operacion = "Egreso";
            nuevoRemito.txtConcepto.Text = "Entrega";
            nuevoRemito.txtTipoDocu.Text = "DSO";

            if (nuevoRemito.ShowDialog() == true)
            {
                MessageBox.Show("Se registro la entrega", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnNuevoDDO_Click(object sender, RoutedEventArgs e)
        {
            DDO nuevoRemito = new DDO();
            nuevoRemito._operacion = "Devolucion";
            nuevoRemito.txtConcepto.Text = "Devolucion";
            nuevoRemito.txtTipoDocu.Text = "DDO";

            if (nuevoRemito.ShowDialog() == true)
            {
                MessageBox.Show("Se registro la devolucion", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnNuevoMov_Click(object sender, RoutedEventArgs e)
        {
            DMO nuevoDMo = new DMO();
            nuevoDMo.Show();
        }

        private void btnConsultaBajas_Click(object sender, RoutedEventArgs e)
        {
            ucProductosBajas uc = new ucProductosBajas();
            ccGestionStk.Content = uc;
        }
    }
}
