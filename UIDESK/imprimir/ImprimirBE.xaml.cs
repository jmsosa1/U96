using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirBE.xaml
    /// </summary>
    public partial class ImprimirBE : Window
    {
        int _idempleado = 0;
        int _tipoReporte = 1;

        public ImprimirBE(int _ide, int _tipo)
        {
            InitializeComponent();
            _idempleado = _ide;
            _tipoReporte = _tipo;
            this.Loaded += new RoutedEventHandler(ImprimirBE_Loaded);
        }

        private void ImprimirBE_Loaded(object sender, RoutedEventArgs e)
        {
            if (_tipoReporte == 1)
            {
                reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/BalanceGeneral?idem= " + _idempleado + "&idempleado= " + _idempleado + " &rv:ParamMode=Collapsed", UriKind.RelativeOrAbsolute);
            }
            else
            {


                reporterBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/BalanceFaltantes?idem= " + _idempleado + "&idempleado= " + _idempleado + " &rv:ParamMode=Collapsed", UriKind.RelativeOrAbsolute);
            }
        }
    }
}
