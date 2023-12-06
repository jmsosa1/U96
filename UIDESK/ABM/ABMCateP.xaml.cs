using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCateP.xaml
    /// </summary>
    public partial class ABMCateP : MaterialWindow 
    {
        #region Declarativas

        BLLProducto coreProducto = new BLLProducto();
        CategoriaP _catep = new CategoriaP();
        int _operacion = 0;
        int _idtipo = 0;
        #endregion
        public ABMCateP(CategoriaP catep, int ope)
        {
            InitializeComponent();
            _operacion = ope;
            _idtipo = catep.IdTipoP; // este parametro depende de la operacion que se hace, puede contener el id del tipo de producto o el id de la cate
            DataContext = catep;
            _catep = catep;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreCate.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la categoria de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ActualizarCategorias(_operacion);
                DialogResult = true;
            }
        }

        private void ActualizarCategorias(int _op)
        {
            string _valor = txtCostoReposicion.Text;
            decimal _costo = decimal.Parse(_valor.Replace("$", ""));

            if (_op == 1) // alta
            {
                coreProducto.CateProductoAlta(_idtipo, txtNombreCate.Text, _costo);
            }
            else
            {
                if (_op == 2) // modificacion
                {

                    coreProducto.CateProductoModi(_catep.IdCateP, txtNombreCate.Text, _costo);
                }
                else
                {
                    //baja
                }
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
