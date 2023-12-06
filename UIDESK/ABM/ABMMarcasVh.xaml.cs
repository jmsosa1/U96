using BLL;
using ENTIDADES;
using System.Windows;
using System.Windows.Input;
using MaterialDesignExtensions.Controls;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMMarcasVh.xaml
    /// </summary>
    public partial class ABMMarcasVh : MaterialWindow
    {
        BLLVehiculos bLL = new BLLVehiculos();
        public string operacion = "";

        public ABMMarcasVh(MarcaVh _mvh)
        {
            InitializeComponent();
            grdABM.DataContext = _mvh;
        }



        private void TxtNombreMarcaVh_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombreMarcaVh.SelectAll();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            this.Close();
        }



        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            int fila = 0;
            MarcaVh marcaVh = new MarcaVh();
            if (string.IsNullOrEmpty(txtNombreMarcaVh.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la Marca de Vehiculo", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (operacion == "A")
            {
                //alta de marca
                marcaVh.NombreMarca = txtNombreMarcaVh.Text;
                fila = bLL.MarcaAlta(marcaVh);
            }
            else
            {
                if (operacion == "M")
                {
                    //modificar nombre de la marca
                    marcaVh.NombreMarca = txtNombreMarcaVh.Text;
                    fila = bLL.MarcaModificar(marcaVh);
                }
                else
                {
                    //borrra una marca
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
