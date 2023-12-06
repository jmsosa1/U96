using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para AsignarEmpleadoObra.xaml
    /// </summary>
    public partial class AsignarEmpleadoObra : MaterialWindow
    {
        BLLObras coreObra = new BLLObras();
        ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();
        int _idobra;

        public AsignarEmpleadoObra(int imputacion)
        {
            InitializeComponent();
            _idobra = imputacion;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
