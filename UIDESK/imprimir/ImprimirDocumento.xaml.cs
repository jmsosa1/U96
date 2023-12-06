using System;
using System.Windows;
//using Microsoft.Reporting.WinForms;


namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirDocumento.xaml
    /// </summary>
    public partial class ImprimirDocumento : Window
    {
        int _idsol = 0;


        public ImprimirDocumento(int _idsolicitud)
        {
            InitializeComponent();
            _idsol = _idsolicitud;
            this.Loaded += new RoutedEventHandler(ImprimirDocumento_Loaded);

        }

        private void ImprimirDocumento_Loaded(object sender, RoutedEventArgs e)
        {
            reporteBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/SolicitudUna?idsol= " + _idsol, UriKind.RelativeOrAbsolute);

        }



    }
}
