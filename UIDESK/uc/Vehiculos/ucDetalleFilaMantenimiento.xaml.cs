using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows.Controls;


namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucDetalleFilaMantenimiento.xaml
    /// </summary>
    public partial class ucDetalleFilaMantenimiento : UserControl
    {
        BLLVehiculos bLL = new BLLVehiculos();
        List<DetManteVh> detManteVhs = new List<DetManteVh>();
        Mante_vh mante_Vh = new Mante_vh();
        public static int _idmantevh;


        public ucDetalleFilaMantenimiento()
        {
            InitializeComponent();
            mante_Vh = bLL.BuscarUnMantenimiento(_idmantevh);
            detManteVhs = bLL.ListarDetallesManteUno(_idmantevh);

            dgListaMantenimientos.DataContext = detManteVhs;
            dgListaMantenimientos.ItemsSource = detManteVhs;
            stkInfoAdicional.DataContext = mante_Vh;

        }
    }
}
