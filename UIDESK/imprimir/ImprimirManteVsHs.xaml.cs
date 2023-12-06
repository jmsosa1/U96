using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirManteVsHs.xaml
    /// </summary>
    public partial class ImprimirManteVsHs : Window
    {
        public ImprimirManteVsHs()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImprimirManteVsHs_Loaded);
        }

        private void ImprimirManteVsHs_Loaded(object sender, RoutedEventArgs e)
        {
            reportBrowser.Source = new Uri("http://172.16.13.60/reports/report/ServerInformes/VehiculosMantenimientoVsHs", UriKind.RelativeOrAbsolute);
        }
    }
}
