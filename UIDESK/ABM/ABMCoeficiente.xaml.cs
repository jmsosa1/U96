using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using ENTIDADES;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCoeficiente.xaml
    /// </summary>
    public partial class ABMCoeficiente : Window
    {
        BLLBase coreBase = new BLLBase();
        Coeficiente _coeficiente = new Coeficiente();
        
        public ABMCoeficiente( Coeficiente coeficiente)
        {
            InitializeComponent();
            _coeficiente = coeficiente;
            DataContext = _coeficiente;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // si los datos estan correctos grabamos
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // si esta todo bien grabamos, teniendo en cuenta que si los valores de los campos de texto
                coreBase.CoeficienteSave(_coeficiente);
                
                DialogResult = true;
            }
        }
    }
}
