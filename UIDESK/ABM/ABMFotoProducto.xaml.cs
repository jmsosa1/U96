using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMFotoProducto.xaml
    /// </summary>
    public partial class ABMFotoProducto : MaterialWindow
    {

        BLLProducto coreProducto = new BLLProducto();
        BitmapImage imagetemp = new BitmapImage();
        BitmapImage imageselec = new BitmapImage();
        Stream stream;
        byte[] imagenBytes;
        Microsoft.Win32.OpenFileDialog seleccionImagen = new Microsoft.Win32.OpenFileDialog();
        public int _idproducto; //  id del producto del cual se necesita cargar una foto 

        public ABMFotoProducto()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
                txtorigen.Text = openFile.FileName.ToUpper();
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
            //para guardar la foto debemos primero crear un objeto de la clase FotoVh
            FotoProducto nuevafoto = new FotoProducto();
            // comprobamos si se escribio algo en el titulo o descripcion de la foto
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                nuevafoto.Titulo = "no indica";
            }
            else
            {
                nuevafoto.Titulo = txtTitulo.Text;
            }




            if (string.IsNullOrEmpty(txtDescriFoto.Text))
            {
                nuevafoto.DescriFoto = "no indica";
            }
            else
            {
                nuevafoto.DescriFoto = txtDescriFoto.Text;
            }
            nuevafoto.AltaF = DateTime.Today.Date;
            nuevafoto.IdProducto = _idproducto;
            nuevafoto.Foto = imagenBytes;
            coreProducto.GuardarFoto(nuevafoto);
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
    }
}
