using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirRMA.xaml
    /// </summary>
    public partial class ImprimirRMA : Window
    {
        int _idremito = 0;

        public ImprimirRMA(int _iddocu)
        {
            InitializeComponent();
            _idremito = _iddocu;
            this.Loaded += new RoutedEventHandler(ImprimirRMA_Loaded);

        }

        private void ImprimirRMA_Loaded(object sender, RoutedEventArgs e)
        {
            reportBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/RMAProducto?idrma= " + _idremito, UriKind.RelativeOrAbsolute);
        }
    }
}
