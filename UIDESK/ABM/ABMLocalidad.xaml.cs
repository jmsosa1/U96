using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMLocalidad.xaml
    /// </summary>
    public partial class ABMLocalidad : MaterialWindow 
    {
        BLLBase bLL = new BLLBase();

        public string operacion = "";
        public ABMLocalidad(Localidad l)
        {
            InitializeComponent();
            grDatosLocalidad.DataContext = l;
        }


        private void txtCP_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCP.SelectAll();
        }

        private void txtNombrelocalidad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombrelocalidad.SelectAll();
        }

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            if (string.IsNullOrEmpty(txtNombrelocalidad.Text))
            {
                MessageBox.Show("Debe ingresar el nombre de la localidad", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (operacion == "A")
            {
                Localidad localidad = new Localidad();
                localidad.IdProvincia = Convert.ToInt16(txtIdProvincia.Text);
                localidad.Provincia = txtProvincia.Text;
                localidad.Nombre = txtNombrelocalidad.Text;
                localidad.CP = txtCP.Text;
                fila = bLL.AgregarLocalidad(localidad);
            }
            else
            {
                if (operacion == "B")
                {
                    Localidad localidad = new Localidad();
                    localidad.IdProvincia = Convert.ToInt16(txtIdProvincia.Text);
                    localidad.Provincia = txtProvincia.Text;
                    localidad.Nombre = txtNombrelocalidad.Text;
                    localidad.CP = txtCP.Text;
                    fila = bLL.ModificarLocalidad(localidad);
                }
            }

            if (fila != 0)
            {
                MessageBox.Show("Se grabo el registro", "Aviso", MessageBoxButton.OK);
                DialogResult = true;
            }
            else
            {
                // MessageBox.Show("Hubo un problema con la grabacion", "Aviso", MessageBoxButton.OK);
                DialogResult = false;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
