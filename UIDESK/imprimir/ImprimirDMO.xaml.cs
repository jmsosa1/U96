using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDMO.xaml
    /// </summary>
    public partial class ImprimirDMO : Window
    {
        int _idremito = 0;

        public ImprimirDMO(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimiDMO_Loaded);
        }

        private void ImprimiDMO_Loaded(object sender, RoutedEventArgs e)
        {
            reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/RemitoDMO?iddocumento= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
