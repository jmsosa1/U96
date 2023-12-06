using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDSI.xaml
    /// </summary>
    public partial class ImprimirDSI : Window
    {
        int _idremito = 0;

        public ImprimirDSI(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimirDSI_Loaded);
        }

        private void ImprimirDSI_Loaded(object sender, RoutedEventArgs e)
        {
            reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/DSI?iddocumento= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
