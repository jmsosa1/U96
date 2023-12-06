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
    /// Lógica de interacción para ItemPresupuesto.xaml
    /// </summary>
    public partial class ItemPresupuesto : MaterialWindow
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLProducto coreProducto = new BLLProducto();

        int _operacion = 0; // define el tipo de operacion a realizar en el formulario
        int _tipoItem = 0; // define la lista a mostrar en el combo de categoria
        Presupuesto_Item _Presupuesto_Item { get; set; } // objeto item del presupuesto
        List<DtoCategoria> dtoCategorias = new List<DtoCategoria>(); // lista de categorias que llenamos con el tipo que corresponde
        public ItemPresupuesto(int operacion, int tipoItem, Presupuesto_Item presupuesto_Item)
        {
            // en el constructor debemos pasar el tipo de operacion que se esta realizando
            // 1 - Alta , 2 - Modificacion ,  3 - Anulacion
            InitializeComponent();
            _operacion = operacion;
            _tipoItem = tipoItem;
            cmbCategorias.DataContext = dtoCategorias;
            cmbCategorias.ItemsSource = dtoCategorias;
            
            List<DtoTipos> _tipos = new List<DtoTipos>(); // creamos la lista de tipos para poder usarla en cada caso
            
            if (_tipoItem == 1) // evaluamos el valor de _tipoItem para ver que categorias tenemos que cargar
            { // vehiculos

                _tipos.Clear(); // limpiamos la lista de dto para poder usarla 
                List<TipoVh> listatipoVh = coreVh.ListarTiposVh(); // creamos la lista de tipos de vehiculos
                foreach (var item in listatipoVh)
                {
                    DtoTipos dto = new DtoTipos();
                    dto.idtipodto = item.IdTipoVh;
                    dto.NomTipoDto = item.NomTipo;
                    _tipos.Add(dto);
                }
                //asignamos al combobox 
                cmbTipos.ItemsSource = _tipos;
                cmbTipos.DataContext = _tipos;
                cmbTipos.SelectedIndex = 0;

            }
            else
            {
                //herramientas
               
                ObservableCollection<TipoProducto> listatipoP = new ObservableCollection<TipoProducto>(); // creamos a lista de tipos de herramientas
                listatipoP = coreProducto.ListarTipos();
                _tipos.Clear(); // limpiamos la lista de productos para poder usarla con este nuevo tipo
                foreach (var item in listatipoP)
                {
                    DtoTipos dto = new DtoTipos();
                    dto.idtipodto = item.IdTipoP;
                    dto.NomTipoDto = item.NomTipo;
                    _tipos.Add(dto);
                }
                //asignamos al combobox 
                cmbTipos.ItemsSource = _tipos;
                cmbTipos.DataContext = _tipos;
                cmbTipos.SelectedIndex = 0;

            }

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

        //clase dto de las categorias y tipos
        #region ClasesPropias
        class DtoTipos
        {
            public int idtipodto { get; set; }
            public string NomTipoDto { get; set; }
        }
        class DtoCategoria
        { public int idCateDto { get; set; } 
            public string NomCateDto { get; set; }
        }

        #endregion

        #region ComboBox


        private void cmbTipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DtoTipos dto = cmbTipos.SelectedItem as DtoTipos; 
            if (dto!= null) 
            {
                // antes de volver a asingar una nueva lista de elementos categoria, limpiamos la lista actual
                // y tambien ponemos a null las propiedades del combobox
                //esto es asi porque si no , despues de varias selecciones, tira una exepcion del tipo invalida de asignacion
                // porque los contenidos no coinciden con el origen ..y no me acuerdo bien , pero es un error raro.
                dtoCategorias.Clear();
                cmbCategorias.DataContext = null;
                cmbCategorias.ItemsSource = null;
                
                //una vez seleccionada el tipo debemos buscar la lista de categorias relacionadas
                // para eso verificamos de que tipo se trata
                if (_tipoItem == 1) // vehiculos
                {
                    //traemos la categorias del tipo seleccionado
                    ObservableCollection<CategoriaVh> categoriaVhs = coreVh.VehiculosListarCategoriaPorTipo(dto.idtipodto);
                    //creamos ahora la lista de categorias dto
                   
                   
                    foreach (var item in categoriaVhs)
                    {
                        DtoCategoria dto1  = new DtoCategoria();
                        dto1.idCateDto = item.IdCateVh;
                        dto1.NomCateDto = item.NomCate;
                        dtoCategorias.Add(dto1);
                    }

                    cmbCategorias.DataContext = dtoCategorias;
                    cmbCategorias.ItemsSource = dtoCategorias;
                    cmbCategorias.SelectedIndex = 0;
                }
                else
                {
                    dtoCategorias.Clear();

                }
            }
            else
            {
                dtoCategorias.Clear();
            }
        }

        private void cmbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DtoCategoria categoria = cmbCategorias.SelectedItem as DtoCategoria;
            if (categoria != null)
            {

            }
        }
        #endregion
    }

}
