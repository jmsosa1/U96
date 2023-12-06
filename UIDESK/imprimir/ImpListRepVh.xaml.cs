using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImpListRepVh.xaml
    /// </summary>
    public partial class ImpListRepVh : Window
    {

        int _idvh;

        public ImpListRepVh(int idvh)
        {
            InitializeComponent();
            _idvh = idvh;
            this.Loaded += new RoutedEventHandler(ImpListRepVh_Loaded);
        }

        private void ImpListRepVh_Loaded(object sender, RoutedEventArgs e)
        {
            reportBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/ListaRepuestosUnVehiculo?idvh= " + _idvh, UriKind.RelativeOrAbsolute);
        }
    }
}
