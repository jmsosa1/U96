using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirManteVsKm.xaml
    /// </summary>
    public partial class ImprimirManteVsKm : Window
    {
        public ImprimirManteVsKm()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImprimirManteVsKm_Loaded);
        }

        private void ImprimirManteVsKm_Loaded(object sender, RoutedEventArgs e)
        {
            reporteBrowser.Source = new Uri("http://172.16.13.60/reports/report/ServerInformes/VehiculosMantenimientoVsKm", UriKind.RelativeOrAbsolute);
        }
    }
}
