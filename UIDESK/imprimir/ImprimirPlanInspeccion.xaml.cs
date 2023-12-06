using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirPlanInspeccion.xaml
    /// </summary>
    public partial class ImprimirPlanInspeccion : Window
    {
        int _estado;

        public ImprimirPlanInspeccion(int estado)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImprimirPlanInspeccion_Loaded);
            _estado = estado;
        }

        private void ImprimirPlanInspeccion_Loaded(object sender, RoutedEventArgs e)
        {
            reporteBrowser.Source = new Uri("http://172.16.13.60/REports/report/ServerInformes/VehiculosPlanInspeccionAVencer?idestado=" + _estado, UriKind.RelativeOrAbsolute);
        }
    }
}
