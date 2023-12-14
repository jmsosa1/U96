using BLL;
using ENTIDADES;
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

namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para ListaMaquinasConVencimiento.xaml
    /// </summary>
    public partial class ListaMaquinasConVencimiento : Window
    {
        BLLMaquinas coreM = new BLLMaquinas();
        List<MpmDetalle> lista = new List<MpmDetalle>();
        public ListaMaquinasConVencimiento(List<MpmDetalle> mpmDetalles)
        {
            InitializeComponent();
            lista = mpmDetalles;
           dgMaqTareasVencidas.ItemsSource = lista;
            dgMaqTareasVencidas.DataContext = lista;
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgMaqTareasVencidas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
