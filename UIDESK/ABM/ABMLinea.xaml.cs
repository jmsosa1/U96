using BLL;
using ENTIDADES;
using System;
using System.Windows;
using System.Windows.Input;
using MaterialDesignExtensions.Controls;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMLinea.xaml
    /// </summary>
    public partial class ABMLinea : MaterialWindow
    {
        public string operacion = "";
        BLLVehiculos bll = new BLLVehiculos();

        public ABMLinea(LineVh lineVh)
        {
            InitializeComponent();
            grPpal.DataContext = lineVh;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            this.Close();
        }

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            LineVh lineVh = new LineVh();
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la linea de vehiculo", "Aviso", MessageBoxButton.OK);
                return;

            }
            if (operacion == "A")
            {
                lineVh.IdCateVh = Convert.ToInt16(txtIdCategoria.Text);
                lineVh.NomLineaVh = txtNombre.Text;
                fila = bll.VehiculoAgregarLinea(lineVh);
            }
            else
            {
                if (operacion == "B")
                {
                    lineVh.IdLineaVh = Convert.ToInt16(txtIdLinea.Text);
                    lineVh.IdCateVh = Convert.ToInt16(txtIdCategoria.Text);
                    lineVh.NomLineaVh = txtNombre.Text;
                    fila = bll.VehiculoModiLinea(lineVh);
                }
                else
                {
                    lineVh.IdLineaVh = Convert.ToInt16(txtIdLinea.Text);
                    fila = bll.VehiculoBorrarLinea(lineVh.IdLineaVh);
                }
            }
            if (fila != 0)
            {
                MessageBox.Show("Se registro correctamente", "Aviso", MessageBoxButton.OK);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hubo problemas con el registro", "Aviso", MessageBoxButton.OK);
                DialogResult = false;
            }
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
