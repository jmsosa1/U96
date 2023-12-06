using MaterialDesignExtensions.Controls;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using UIDESK.uc.gestion;


namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para CarpetaTareasSector.xaml
    /// </summary>
    public partial class CarpetaTareasSector : MaterialWindow
    {

        public static string _tituloAppBar { get; set; }
        public static Image _imageAppBar = null;

        public CarpetaTareasSector()
        {
            InitializeComponent();
            //ucGestionPPal ucGestionPPal = new ucGestionPPal();
            //ctc.Content = ucGestionPPal;
            //_tituloAppBar = "Principal";
           
            
          
        }


        private void rbDiario_Click(object sender, RoutedEventArgs e)
        {


            ucTareasGeneral uc = new ucTareasGeneral();
            ctc.Content = uc;
           

        }

        private void rbManteVh_Click(object sender, RoutedEventArgs e)
        {

            ucPlanManteVh uc = new ucPlanManteVh();
            ctc.Content = uc;
           
        }

        private void rdManteHerra_Click(object sender, RoutedEventArgs e)
        {

            ucPlanManteHerra uc = new ucPlanManteHerra();
            ctc.Content = uc;
            
        }

        private void rdSolicitudesHerra_Click(object sender, RoutedEventArgs e)
        {

            ucSolicitudesAbastecimiento uc = new ucSolicitudesAbastecimiento();
            ctc.Content = uc;
            
        }

        private void rdNotas_Click(object sender, RoutedEventArgs e)
        {

            ucNotasSahmv6 uc = new ucNotasSahmv6();
            ctc.Content = uc;
           
        }

        //private void btnHome_Click(object sender, RoutedEventArgs e)
        //{
        //    ucGestionPPal ucGestionPPal = new ucGestionPPal();
        //    ctc.Content = ucGestionPPal;
        //    _tituloAppBar = "Principal";
        //}


    }
}
