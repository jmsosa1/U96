using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimiDDO.xaml
    /// </summary>
    public partial class ImprimiDDO : Window
    {
        int _idremito = 0;

        public ImprimiDDO(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimirDDO);
        }

        private void ImprimirDDO(object sender, RoutedEventArgs e)
        {
            reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/DDO?iddocumento= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
