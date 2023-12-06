using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para AMBFotoEmpleado.xaml
    /// </summary>
    public partial class AMBFotoEmpleado : MaterialWindow
    {
        BitmapImage imagetemp = new BitmapImage();
        BitmapImage imageselec = new BitmapImage();
        Empleado _empleado { get; set; }
        Stream stream;
        byte[] imagenBytes;
        Microsoft.Win32.OpenFileDialog seleccionImagen = new Microsoft.Win32.OpenFileDialog();
        public int _idvehiculo; //  id del vehiculo del cual se necesita cargar una foto 
        public AMBFotoEmpleado(Empleado empleado)
        {
            InitializeComponent();
            _empleado = empleado;
        }

        private void BtnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Filter = "Image File(*.BMP,*.JPG,*.GIF,*.PNG|*.BMP,*.JPG,*.GIF,*.PNG" +
                                     "|All Files(*.*)|*.*";
            openFile.CheckFileExists = true;
            openFile.Multiselect = false;

            if (openFile.ShowDialog() == true)
            {

                imageselec.BeginInit();
                imageselec.UriSource = new Uri(openFile.FileName);
                imageselec.EndInit();
                imgfoto.Source = imageselec;

                stream = openFile.OpenFile();
                imagenBytes = new byte[stream.Length];
                stream.Read(imagenBytes, 0, (int)stream.Length);
            }

        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

            //para grabar la foto,primero debemos verificar que se haya elegido algo
            if (imgfoto.Source ==null)
            {
                MessageBox.Show("Debe seleccionar una foto ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // si existe una foto , procedemos a guardar la misma
            }
                



            DialogResult = true;
        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
