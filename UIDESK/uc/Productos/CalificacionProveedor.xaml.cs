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
using MaterialDesignExtensions.Controls;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para CalificacionProveedor.xaml
    /// </summary>
    public partial class CalificacionProveedor : MaterialWindow
    {
        Proveedor _proveedor = new Proveedor();
        decimal _calificacion = 0;
        BLLProveedor coreProveedor = new BLLProveedor();
       

        
        public CalificacionProveedor( Proveedor proveedor)
        {
           
            InitializeComponent();
            _proveedor = proveedor;
            DataContext = _proveedor;
           
        }

        private void sldPlazo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _proveedor.Plazo = Convert.ToInt32(sldPlazo.Value);

        }

        private void sldPrecio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _proveedor.Precio = Convert.ToInt32(sldPrecio.Value);

        }

        private void sldCalidad_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _proveedor.Calidad = Convert.ToInt32(sldCalidad.Value);
        }

        private void sldAtencion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _proveedor.Atencion = Convert.ToInt32(sldAtencion.Value);

        }

        private void CalcularCalificacion()
        {
            _calificacion = (_proveedor.Atencion + _proveedor.Calidad + _proveedor.Precio + _proveedor.Plazo) / 4;
            _proveedor.Calificacion =Convert.ToInt32( _calificacion);
            txbCalificacion.Text = _calificacion.ToString();
            if (_calificacion >= 0 && _calificacion <= 2)
            {
                ReadOnlyRatingBar.Value = 1;
            }
            if (_calificacion >= 3 && _calificacion <= 4)
            {
                ReadOnlyRatingBar.Value = 2;
            }
            if (_calificacion >= 5 && _calificacion <= 6)
            {
                ReadOnlyRatingBar.Value = 3;
            }
            if (_calificacion >= 7 && _calificacion <= 8)
            {
                ReadOnlyRatingBar.Value = 4;
            }
            if (_calificacion >= 9 && _calificacion <= 10)
            {
                ReadOnlyRatingBar.Value = 5;
            }


        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //CalcularCalificacion();
            //aca actualizamos la clasificacion del proveedor
            coreProveedor.ActualizarCalificacion(_proveedor.IdProve, _proveedor.Plazo, _proveedor.Calidad, _proveedor.Precio, _proveedor.Atencion);
            DialogResult = true;

        }
    }
}
