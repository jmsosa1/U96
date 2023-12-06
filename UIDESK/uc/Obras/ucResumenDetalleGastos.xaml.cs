using System.Windows.Controls;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenDetalleGastos.xaml
    /// </summary>
    public partial class ucResumenDetalleGastos : UserControl
    {
        int _imputacion;
        public ucResumenDetalleGastos(int imputacion)
        {
            InitializeComponent();
            _imputacion = imputacion;
        }
    }
}
