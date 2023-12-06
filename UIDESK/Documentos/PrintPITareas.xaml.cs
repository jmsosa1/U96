using ENTIDADES;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para PrintPITareas.xaml
    /// </summary>
    public partial class PrintPITareas : Window
    {
        List<plan_inspeccion> _lista = new List<plan_inspeccion>();

        public PrintPITareas(List<plan_inspeccion> plan_s, string _titulo)
        {
            InitializeComponent();
            _lista = plan_s;
            dgDetalle.ItemsSource = _lista;
            dgDetalle.DataContext = _lista;
            txbTitulo.Text = _titulo;

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "Plan Inspeccion");
            }
            DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
