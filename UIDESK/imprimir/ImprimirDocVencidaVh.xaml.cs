using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDocVencidaVh.xaml
    /// </summary>
    public partial class ImprimirDocVencidaVh : Window
    {
        public ImprimirDocVencidaVh()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImprimirDocVencidaVh_Loaded);
        }

        private void ImprimirDocVencidaVh_Loaded(object sender, RoutedEventArgs e)
        {
            reporteBrowser.Source = new Uri("http://172.16.13.60/REports/report/ServerInformes/VehiculosDocVencida", UriKind.RelativeOrAbsolute);
        }
    }
}
