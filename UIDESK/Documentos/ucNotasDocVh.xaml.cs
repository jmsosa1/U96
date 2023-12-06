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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using ENTIDADES;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para ucNotasDocVh.xaml
    /// </summary>
    public partial class ucNotasDocVh : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        NotaDocuVh _nota = new NotaDocuVh();
        List<NotaDocuVh> _lista = new List<NotaDocuVh>();

       public static int _idreg; // identificacion del registro
        public static int _idtiponota; // tipo de nota que necesitamos mostrar

        public ucNotasDocVh()
        {
            InitializeComponent();
            _lista = coreVh.VehiculoDocNotas(_idreg,_idtiponota);
            dgNotadoc.ItemsSource = _lista;
            dgNotadoc.DataContext = _lista;
        }

        private void btnDeleteNota_Click(object sender, RoutedEventArgs e)
        {
            coreVh.VehiculoDocDelete(_nota.IdNota);
            _lista = coreVh.VehiculoDocNotas(_idreg, _idtiponota);
            dgNotadoc.ItemsSource = _lista;
            dgNotadoc.DataContext = _lista;
        }

      
    }
}
