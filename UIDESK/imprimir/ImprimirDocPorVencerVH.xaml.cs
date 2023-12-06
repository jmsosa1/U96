using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDocPorVencerVH.xaml
    /// </summary>
    public partial class ImprimirDocPorVencerVH : Window
    {
        public ImprimirDocPorVencerVH()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImprimirDocPorVencerVH_Loaded);
        }

        private void ImprimirDocPorVencerVH_Loaded(object sender, RoutedEventArgs e)
        {
            reporteBrowser.Source = new Uri("http://172.16.13.60/REports/report/ServerInformes/VehiculosDocPorVencer", UriKind.RelativeOrAbsolute);
        }
    }
}
