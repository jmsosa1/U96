using MaterialDesignExtensions.Controls;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using UIDESK.uc.resultados;
using UIDESK.uc.Vehiculos;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para ResultadosVehiculos.xaml
    /// </summary>
    public partial class ResultadosVehiculos : MaterialWindow
    {
        public ResultadosVehiculos()
        {
            InitializeComponent();
        }

        private void btnMantenimientos_Click(object sender, RoutedEventArgs e)
        {
            ucResultadoMantenimientoVh mantenimientoVh = new ucResultadoMantenimientoVh();
            cc.Content = mantenimientoVh;
        }

        private void btnCombustibles_Click(object sender, RoutedEventArgs e)
        {
            ucResultadoCombustible combustible = new ucResultadoCombustible();
            cc.Content = combustible;
        }

        private void lsvNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "vwiConsumos":
                    ucResultadoCombustible combustible = new ucResultadoCombustible();
                    cc.Content = combustible;
                    break;
                case "vwiMantenimientos":
                    ucResultadoMantenimientoVh mantenimientoVh = new ucResultadoMantenimientoVh();
                    cc.Content = mantenimientoVh;
                    break;
                default:
                    break;
            }
        }

        private void lsvNavMante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "IMInfoAnual":
                    ucDatosManteVhAnio ManteVhAnio = new ucDatosManteVhAnio();
                    cc.Content = ManteVhAnio;
                    break;
                case "IMComparativaAnios":
                    ucDatosManteVhAnioGraficos anioGraficos = new ucDatosManteVhAnioGraficos();
                    cc.Content = anioGraficos;
                    break;
                case "IMCategorias":
                    ucDatosManteVhAnioCategorias anioCategorias = new ucDatosManteVhAnioCategorias();
                    cc.Content = anioCategorias;
                    break;
                case "IMVehiculo":
                    ucDatosManteVhAnioIndividual individual = new ucDatosManteVhAnioIndividual();
                    cc.Content = individual;
                    break;
                case "IMSituacionOp":
                    ucSituacionOperativa operativa = new ucSituacionOperativa();
                    cc.Content = operativa;
                    break;
                case "IMCostoManteKm":
                    ucCostoManteKm costoKm = new ucCostoManteKm();
                    cc.Content = costoKm;
                    break;
                case "IMCostoManteHs":
                    ucCostoManteHs costoHs = new ucCostoManteHs();
                    cc.Content = costoHs;
                    break;
                default:
                    break;
            }
        }

        private void lsvNavConsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ITConsumoAnual":
                    ucDatosConsumoAnio consumoAnio = new ucDatosConsumoAnio();
                    cc.Content = consumoAnio;
                    break;
                case "ITConsumoInterAnual":
                    ucDatosConsumoAnioGraficos anioGraficos = new ucDatosConsumoAnioGraficos();
                    cc.Content = anioGraficos;
                    break;
                case "ITConsumoCategorias":
                    ucDatosConsumoAnioCategorias anioCategorias = new ucDatosConsumoAnioCategorias();
                    cc.Content = anioCategorias;
                    break;
                case "ITConsumoIndividual":
                    ucDatosConsumoAnioIndividual individual = new ucDatosConsumoAnioIndividual();
                    cc.Content = individual;
                    break;
                case "ITProgresionConsumos":
                    ucProgresionConsumosMensual ucProgresion = new ucProgresionConsumosMensual();
                    cc.Content = ucProgresion;
                    break;
                default:
                    break;
            }
        }

        private void lsvNavFlota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ITAnalisisFlota":
                    ucSituacionFlota situacionFlota = new ucSituacionFlota();
                    cc.Content = situacionFlota;
                    break;
                case "ITPrediccion":
                    ucPrediccionFlota ucPrediccionFlota = new ucPrediccionFlota();
                    cc.Content = ucPrediccionFlota; 
                    break;
                //case "ITConsumoCategorias":
                //    ucDatosConsumoAnioCategorias anioCategorias = new ucDatosConsumoAnioCategorias();
                //    cc.Content = anioCategorias;
                //    break;
                //case "ITConsumoIndividual":
                //    ucDatosConsumoAnioIndividual individual = new ucDatosConsumoAnioIndividual();
                //    cc.Content = individual;
                //    break;
                //case "ITProgresionConsumos":
                //    ucProgresionConsumosMensual ucProgresion = new ucProgresionConsumosMensual();
                //    cc.Content = ucProgresion;
                //    break;
                default:
                    break;
            }
        }
    }
}
