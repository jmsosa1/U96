using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MaterialDesignExtensions.Controls;
namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMMoneda.xaml
    /// </summary>
    public partial class ABMMoneda : MaterialWindow
    {
        Monedas Monedas { get; set; }
        List<Monedas> lista = new List<Monedas>();
        BLLBase coreBase = new BLLBase();
        string _operacion = "";
        public ABMMoneda(Monedas monedas, string operacion)
        {
            InitializeComponent();
            Monedas = monedas;
            DataContext = Monedas;
            _operacion = operacion;
            lista = coreBase.ListaMonedasSimbolos();
            cmbMonedas.ItemsSource = lista;

            if (_operacion == "A")
            {
                btnAccion.Content = "Guardar";
            }
            if (_operacion == "M")
            {
                btnAccion.Content = "Actualizar";
            }
            if (_operacion == "B")
            {
                btnAccion.Content = "Borrar";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cerrar la ventana?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            // aca tiene que ir el codigo para grabar o actualizar el valor


        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
