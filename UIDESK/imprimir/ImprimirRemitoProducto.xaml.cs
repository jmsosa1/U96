using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirRemitoProducto.xaml
    /// </summary>
    public partial class ImprimirRemitoProducto : Window
    {
        int _idremito = 0;

        public ImprimirRemitoProducto(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimirRemitoVh_Loaded);
        }

        private void ImprimirRemitoVh_Loaded(object sender, RoutedEventArgs e)
        {
            reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/RemitoObraProducto?iddocumento= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
