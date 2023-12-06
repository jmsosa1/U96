using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para DetallePlanilla.xaml
    /// </summary>
    public partial class DetallePlanilla : MaterialWindow 
    {
        int _idproducto;
        BLLLaboratorio coreLab = new BLLLaboratorio();
        Mpm mpm = new Mpm();
        ObservableCollection<MpmDetalle> lista_mpm = new ObservableCollection<MpmDetalle>();
        Producto _producto = new Producto();
        bool _existe_mpm;

        public DetallePlanilla(Producto producto)
        {
            InitializeComponent();
            _idproducto = producto.IdProducto;
            _existe_mpm = coreLab.ValidarExistenciaPlanillaMPM(_idproducto);

            if (_existe_mpm)
            {


                mpm = coreLab.ObtenerMPMUnaMaquina(_idproducto);
                lista_mpm = coreLab.ObtenerDetalleMPMUnaMaquina(mpm.Idmpm);
                DataContext = mpm;
                dgMPM.ItemsSource = lista_mpm;
                dgMPM.DataContext = lista_mpm;
            }
            _producto = producto;
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            // llamamos al formulario para imprimir

        }

        private void btnEjecucionTarea_Click(object sender, RoutedEventArgs e)
        {
            // llamamos al metodo de cumplir una tarea
        }

        private void btnRegConsumo_Click(object sender, RoutedEventArgs e)
        {
            RegistrarConsumoMaquina registrar = new RegistrarConsumoMaquina(_producto,mpm.Idmpm);
            if (registrar.ShowDialog() == true)
            {
                MessageBox.Show("Se registro el consumo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
