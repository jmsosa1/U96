using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucResultadoCombustible.xaml
    /// </summary>
    public partial class ucResultadoCombustible : UserControl
    {
        public ucResultadoCombustible()
        {
            InitializeComponent();
        }



        private void btnConsumoAnio_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosConsumoAnio consumoAnio = new ucDatosConsumoAnio();
            grdContenido.Children.Add(consumoAnio);
        }

        private void btnConsumoAnioGrafico_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosConsumoAnioGraficos anioGraficos = new ucDatosConsumoAnioGraficos();
            grdContenido.Children.Add(anioGraficos);
        }

        private void btnConsumoCategorias_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosConsumoAnioCategorias anioCategorias = new ucDatosConsumoAnioCategorias();
            grdContenido.Children.Add(anioCategorias);
        }

        private void btnConsumoUnvh_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosConsumoAnioIndividual uc = new ucDatosConsumoAnioIndividual();
            grdContenido.Children.Add(uc);
        }

        private void btnProgresionConsumos_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucProgresionConsumosMensual ucProgresion = new ucProgresionConsumosMensual();
            grdContenido.Children.Add(ucProgresion);
        }
    }
}
