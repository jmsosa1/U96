using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UIDESK.uc.Presupuestos
{
    /// <summary>
    /// Lógica de interacción para ItemPresupuestoVh.xaml
    /// </summary>
    public partial class ItemPresupuestoVh : MaterialWindow
    {
        BLLVehiculos coreVh = new BLLVehiculos();
       

        int _operacion = 0; // define el tipo de operacion a realizar en el formulario
        int _tipoItem = 0; // define la lista a mostrar en el combo de categoria
        Presupuesto_Item _Presupuesto_Item { get; set; } // objeto item del presupuesto
        
        ObservableCollection<CategoriaVh> categorias = new ObservableCollection<CategoriaVh>();
        List<TipoVh> tipoVhs = new List<TipoVh>();
        public ItemPresupuestoVh(int operacion, int tipoItem, Presupuesto_Item presupuesto_Item)
        {
            // en el constructor debemos pasar el tipo de operacion que se esta realizando
            // 1 - Alta , 2 - Modificacion ,  3 - Anulacion
            InitializeComponent();
            _operacion = operacion;
            _tipoItem = tipoItem;
          
            tipoVhs = coreVh.ListarTiposVh();
            cmbTipos.ItemsSource = tipoVhs;
            cmbTipos.DataContext = tipoVhs;
            cmbTipos.SelectedIndex = 0;
         

           

            _Presupuesto_Item = presupuesto_Item;

            DataContext = _Presupuesto_Item;
        }

        #region Botones
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (_operacion == 1) //  alta del item
            {
                // debemos agregar el objeto presupuesto item a la lista que actualmente tiene el detalle


            }
            else

                if (_operacion == 2) // modificacion se deben cargar los datos del item 
            {
                //en este caso se pueden usar los dos textbox y los valores que se carguen deben pasarse al resumen de asignacion
                txtPresupuesto.IsEnabled = true;
                txtAprobado.IsEnabled = true;

            }
            else
            {
                // anulacion del item implica que los valores deben re asignarse.

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        #endregion

      

        #region ComboBox


        private void cmbTipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoVh dto = cmbTipos.SelectedItem as TipoVh;
            if (dto != null)
            {
                    //traemos la categorias del tipo seleccionado
                     categorias = coreVh.VehiculosListarCategoriaPorTipo(dto.IdTipoVh);
                    //creamos ahora la lista de categorias dto
                    cmbCategorias.DataContext = categorias;
                    cmbCategorias.ItemsSource = categorias;
                    cmbCategorias.SelectedIndex = 0;
             
            }
        }

        private void cmbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaVh categoria = cmbCategorias.SelectedItem as CategoriaVh;
            if (categoria != null)
            {

            }
        }
        #endregion
    }
}
