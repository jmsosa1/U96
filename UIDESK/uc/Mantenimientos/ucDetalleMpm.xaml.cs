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
namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleMpm.xaml
    /// </summary>
    public partial class ucDetalleMpm : UserControl
    {
        public static int _idproducto;
        BLLLaboratorio coreLab = new BLLLaboratorio();
        BLLMaquinas coreMaq = new BLLMaquinas();
        Mpm mpm = new Mpm();
        ObservableCollection<MpmDetalle> lista_mpm = new ObservableCollection<MpmDetalle>();
        bool _existe_mpm;

        public ucDetalleMpm()
        {
            InitializeComponent();
            _existe_mpm = coreMaq.ValidarExistenciaPlanillaMPM(_idproducto);

            if (_existe_mpm)
            {


                mpm = coreMaq.ObtenerMPMUnaMaquina(_idproducto);
                lista_mpm = coreMaq.ObtenerDetalleMPMUnaMaquina(mpm.Idmpm);
                DataContext = mpm;
                dgMPM.ItemsSource = lista_mpm;
                dgMPM.DataContext = lista_mpm;
            }

        }
    }
}
