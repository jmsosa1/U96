using BLL;
using ENTIDADES;
using System;
using System.Windows;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMTipoDocuVH.xaml
    /// </summary>
    public partial class ABMTipoDocuVH : Window
    {

        public string _tipoOp = "A";
        BLLVehiculos coreVehiculo = new BLLVehiculos();

        public ABMTipoDocuVH(Docu_vh docu_)
        {
            InitializeComponent();
            DataContext = docu_;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreDocu.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la documentacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {


                if (_tipoOp == "A")
                {
                    //alta de un nuevo tipo de documentacion
                    Docu_vh d = new Docu_vh();

                    d.Descripcion = txtNombreDocu.Text;
                    coreVehiculo.VehiculoAltaTipoDocumentacion(d);
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    if (_tipoOp == "M")
                    {
                        //modificacion de un tipo existente
                        Docu_vh d = new Docu_vh();
                        d.IdDocuVH = Convert.ToInt32(txtIdDocuvh.Text);
                        d.Descripcion = txtNombreDocu.Text;
                        coreVehiculo.VehiculoModiTipoDocumentacion(d);
                        DialogResult = true;
                        this.Close();
                    }
                }
            }
        }
    }
}
