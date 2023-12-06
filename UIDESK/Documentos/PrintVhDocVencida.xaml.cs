using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para PrintVhDocVencida.xaml
    /// </summary>
    public partial class PrintVhDocVencida : Window
    {
        List<VehiculoDocu> _lista = new List<VehiculoDocu>();

        public PrintVhDocVencida(List<VehiculoDocu> vehiculoDocus, string titulo)
        {
            InitializeComponent();
            _lista = vehiculoDocus;
            txbTitulo.Text = titulo;
            dgDetalle.ItemsSource = _lista;
            dgDetalle.DataContext = _lista;
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
                printDialog.PrintVisual(print, "Documentos Vencidos");
            }
            DialogResult = true;
            this.Close();
        }
    }
}
