using System;
using System.Windows;

namespace UIDESK.imprimir
{
    /// <summary>
    /// Lógica de interacción para ImprimirExistencia.xaml
    /// </summary>
    public partial class ImprimirExistencia : Window
    {

        int _idcategoria;
        int _tiporesumen;

        public ImprimirExistencia(int _idcate, int tiporesumen)
        {
            InitializeComponent();
            _idcategoria = _idcate;
            _tiporesumen = tiporesumen;
            this.Loaded += new RoutedEventHandler(ImprimirExistencia_Loaded);
        }

        private void ImprimirExistencia_Loaded(object sender, RoutedEventArgs e)
        {
            if (_tiporesumen == 1) // resumen de existencia en obra
            {
                reportBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/CatProExtObra?idcategoria= " + _idcategoria, UriKind.RelativeOrAbsolute);
            }

            if (_tiporesumen == 2) // resumen de existencia en stock
            {
                reportBrowser.Source = new Uri("http://pc-128/reports/report/ServerInformes/CatProExtStock?idcategoria= " + _idcategoria, UriKind.RelativeOrAbsolute);
            }
        }
    }
}
