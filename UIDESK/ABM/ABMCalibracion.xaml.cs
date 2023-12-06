using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCalibracion.xaml
    /// </summary>
    public partial class ABMCalibracion : MaterialWindow
    {
        Producto _instrumento = new Producto();
        BLLProducto coreProducto = new BLLProducto();
        BLLProveedor coreProveedor = new BLLProveedor();
        BLLLaboratorio coreLab = new BLLLaboratorio();
        ObservableCollection<Proveedor> _listaProveedor = new ObservableCollection<Proveedor>();
        CalibracionInstrumento calibracion = new CalibracionInstrumento();
        CalibracionInstrumento _calibracionAnterior = new CalibracionInstrumento();

        public ABMCalibracion(Producto producto)
        {
            InitializeComponent();
            _instrumento = producto;
            DataContext = _instrumento;
            _listaProveedor = coreProveedor.ProveedorPorRubro(6); // rubro de instrumentos
            if (_listaProveedor == null)
            {
                MessageBox.Show("No existen proveedores del rubro instrumentos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            calibracion.IdProducto = _instrumento.IdProducto;
            calibracion.EstadoVencimiento = 13; // valido
            calibracion.AltaRegistro = DateTime.Today.Date;
            cmbProveedor.ItemsSource = _listaProveedor;
            cmbProveedor.DataContext = _listaProveedor;
            _calibracionAnterior = coreLab.BuscarCalibracionActivaUnInstrumento(_instrumento.IdProducto);// cargamos la ultima cablibracion
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            // primero chekeamos que esten todos los campos correctos
            if (cmbProveedor.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (dtpFechaCalibracion.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha de certificado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (dtpFechaProximoVencimiento.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha de proximo vencimiento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCertificado.Text))
            {
                MessageBox.Show("Debe indicar un numero de certificado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (rdb1.IsChecked == null && rdb2.IsChecked == null)
            {
                MessageBox.Show("Debe indicar un resultado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtValidez.Text))
            {
                MessageBox.Show("Debe indicar la validez", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //si esta todo bien procedemos a grabar los datos
            // antes debemos dar de baja la anterior calibracion
            // para eso devemos cambiar el estado de la calibracion a valor 2 inactivo
            // esto se hace directamente en el procedure que da de alta la nueva calibracion, ver SQLServer
            calibracion.Nota = txtObservaciones.Text;
            calibracion.EmisorCertificado = txtEmisorCertificado.Text;
            //calibracion.RutaArchivo = txtRutaArchivo.Text;
            calibracion.NumeroCertificado = txtCertificado.Text;
            calibracion.ValidezDias = Convert.ToInt32(txtValidez.Text);
            //verificamos que si la fecha de vencimiento actual es menor que la fecha actual
            if (calibracion.VencimientoActual < DateTime.Today)
            {
                calibracion.EstadoVencimiento = 14;//vencido
            }
          // grabamos el registro en la base de datos
            coreLab.NuevaCalibracionInstrumento(calibracion);
            // si el instrumento tiene resultado satisfactorio de la calibracion, hay que marcarlo como apto
            // de lo contrario debemos marcarlo como no apto
            //esto se hace actualizando el valor del campo estado_calibracion del producto
            if (calibracion.Cod_Resultado == 15)
            {
                coreLab.ActualizarEstadoInstrumento(calibracion.IdProducto, "Apto");
            }
            else
            {
                coreLab.ActualizarEstadoInstrumento(calibracion.IdProducto, "No Apto");
            }
   
                        
            DialogResult = true;


        }

        private void cmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proveedor proveedor = cmbProveedor.SelectedItem as Proveedor;
            calibracion.IdProveedor = proveedor.IdProve;

        }

        private void dtpFechaCalibracion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaCalibracion.SelectedDate.Value < _calibracionAnterior.FechaUltimaCalibracion)
            {
                MessageBox.Show("La fecha de calibracion debe ser mayor que la anterior:" + _calibracionAnterior.FechaUltimaCalibracion.Value.Date.ToString() + " ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                calibracion.FechaUltimaCalibracion = dtpFechaCalibracion.SelectedDate.Value;
            }


        }

        private void rdb1_Checked(object sender, RoutedEventArgs e)
        {
            calibracion.Resultado = "Satisfactorio";
            calibracion.Cod_Resultado = 15; // satisfactorio
        }

        private void rdb2_Checked(object sender, RoutedEventArgs e)
        {
            calibracion.Resultado = "No Satisfactorio";
            calibracion.Cod_Resultado = 16; // no satisfactorio
        }

        private void dtpFechaProximoVencimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            calibracion.VencimientoActual = dtpFechaProximoVencimiento.SelectedDate.Value;
        }





        private void btnRutaArchivo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Filter = "Image File(*.BMP,*.JPG,*.GIF,*.PNG|*.BMP,*.JPG,*.GIF,*.PNG" +
                                     "|All Files(*.*)|*.*";
            openFile.CheckFileExists = true;
            openFile.Multiselect = false;

            if (openFile.ShowDialog() == true)
            {

                //txtRutaArchivo.Text = openFile.FileName;

            }
        }

        private void txtValidez_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValidez.Text))
            {


                int _validez = Convert.ToInt32(txtValidez.Text);
                DateTime date = dtpFechaCalibracion.SelectedDate.Value.AddDays(_validez);
                dtpFechaProximoVencimiento.SelectedDate = date;
            }

        }
    }
}
