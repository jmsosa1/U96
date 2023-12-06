using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ENTIDADES;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para PrintFichaControlCalibraciones.xaml
    /// </summary>
    public partial class PrintFichaControlCalibraciones : Window
    {
        Producto _producto = new Producto();
        CalibracionInstrumento _calibracion = new CalibracionInstrumento();
        List<CalibracionInstrumento> _listaCalibraciones = new List<CalibracionInstrumento>();

        public PrintFichaControlCalibraciones(Producto producto, CalibracionInstrumento calibracion, List<CalibracionInstrumento> calibracionInstrumentos)
        {
            InitializeComponent();
            _producto = producto;
            _listaCalibraciones = calibracionInstrumentos;
            grEncabezado.DataContext = producto;
            _calibracion = calibracion;
            txtValidez.Text = _calibracion.ValidezDias.ToString();
            txtEstado.Text = _producto.EstadoInstrumento;
            dgDetalle.ItemsSource = _listaCalibraciones;
            dgDetalle.DataContext = _listaCalibraciones;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "Ficha Instrumento");
            }
            DialogResult = true;
            this.Close();
        }
    }
}
