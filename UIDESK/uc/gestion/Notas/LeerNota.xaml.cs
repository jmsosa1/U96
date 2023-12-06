using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.gestion.Notas
{
    /// <summary>
    /// Lógica de interacción para LeerNota.xaml
    /// </summary>
    public partial class LeerNota : MaterialWindow
    {
        NotaSahmV6 _nota = new NotaSahmV6();
        public LeerNota(NotaSahmV6 notaSahmV6)
        {
            InitializeComponent();
            _nota = notaSahmV6;
            DataContext = _nota;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            // vamos a usar el metodo printvisual solamente en este caso
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(this, "Nota");
        }
    }
}
