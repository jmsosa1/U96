using MaterialDesignExtensions.Controls;
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
using ENTIDADES;
using BLL;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para SelecCausaBaja.xaml
    /// </summary>
    public partial class SelecCausaBaja : MaterialWindow 
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLBase coreBase = new BLLBase();
        List<CausaBaja> lista_causa = new List<CausaBaja>();
        Vehiculo _vehiculo = new Vehiculo();
        public SelecCausaBaja( Vehiculo vehiculo)
        {
            InitializeComponent();
            lista_causa = coreBase.ListaCausasBaja();
            cmbCausaBaja.ItemsSource = lista_causa;
            cmbCausaBaja.DataContext = lista_causa;
            _vehiculo = vehiculo;
        }

        private void btnRealizarBaja_Click(object sender, RoutedEventArgs e)
        {
            //cehekear si no se elijio una baja
            CausaBaja causa = cmbCausaBaja.SelectedItem as CausaBaja;
            if (causa!= null)
            {
                // si todo esta correcto procedemos a la baja
                coreVh.VehiculoBaja(_vehiculo.IdVh, causa.IdCausaBaja, DateTime.Today.Date, Contexto.CodUser, causa.NomCausa);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Debe elegir una causa", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }
    }
}
