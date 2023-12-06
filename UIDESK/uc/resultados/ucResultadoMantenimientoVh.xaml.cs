using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucResultadoMantenimientoVh.xaml
    /// </summary>
    public partial class ucResultadoMantenimientoVh : UserControl
    {
        public ucResultadoMantenimientoVh()
        {
            InitializeComponent();
        }

        private void btnManteVhAnio_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosManteVhAnio ManteVhAnio = new ucDatosManteVhAnio();
            grdContenido.Children.Add(ManteVhAnio);
        }

        private void btnManteVhAnioGrafico_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosManteVhAnioGraficos anioGraficos = new ucDatosManteVhAnioGraficos();
            grdContenido.Children.Add(anioGraficos);
        }

        private void btnManteVhCategorias_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosManteVhAnioCategorias anioCategorias = new ucDatosManteVhAnioCategorias();
            grdContenido.Children.Add(anioCategorias);
        }

        private void btnManteVhUnvh_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucDatosManteVhAnioIndividual individual = new ucDatosManteVhAnioIndividual();
            grdContenido.Children.Add(individual);
        }

        private void btnSituacionOperativa_Click(object sender, RoutedEventArgs e)
        {
            grdContenido.Children.Clear();
            ucSituacionOperativa operativa = new ucSituacionOperativa();
            grdContenido.Children.Add(operativa);
        }
    }
}
