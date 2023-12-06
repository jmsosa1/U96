using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenDetalleEmpleados.xaml
    /// </summary>
    public partial class ucResumenDetalleEmpleados : UserControl
    {
        int _imputacion;
        BLLEmpleados coreEmpleados = new BLLEmpleados();
        BLLObras coreObras = new BLLObras();
        ObservableCollection<BalanceEmpleado> balance = new ObservableCollection<BalanceEmpleado>();
        List<Empleado> lista_empleados = new List<Empleado>();
        CultureInfo ci = new CultureInfo("es-AR");
        public ICollectionView vistaEmpleados
        {
            get { return CollectionViewSource.GetDefaultView(balance); }
        }
        public ucResumenDetalleEmpleados(int imputacion)
        {
            InitializeComponent();
            _imputacion = imputacion;
            lista_empleados = coreObras.ListarEmpleadosUnaObra(_imputacion);
            cmbEmpleados.ItemsSource = lista_empleados;
        }

        private void dgDetalleEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Empleado empleado = cmbEmpleados.SelectedItem as Empleado;
            if (empleado != null)
            {
                balance = coreEmpleados.BalanceEmpleadoUnaImputacion(_imputacion, empleado.IdEmpleado);
                dgDetalleEmpleados.ItemsSource = balance;
                dgDetalleEmpleados.DataContext = balance;

                decimal _costoTotal = 0;
                foreach (var item in balance)
                {
                    _costoTotal = _costoTotal + item.CostoExistencia;
                }
                txbCostoTotalHerramientas.Text = _costoTotal.ToString("C", ci);
            }
        }
    }
}
