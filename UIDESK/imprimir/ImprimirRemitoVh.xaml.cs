using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirRemitoVh.xaml
    /// </summary>
    public partial class ImprimirRemitoVh : Window
    {
        int _idremito = 0;

        public ImprimirRemitoVh(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimirRemitoVh_Loaded);

        }

        private void ImprimirRemitoVh_Loaded(object sender, RoutedEventArgs e)
        {
            reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/RemitoObraVehiculo?iddocumento= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
