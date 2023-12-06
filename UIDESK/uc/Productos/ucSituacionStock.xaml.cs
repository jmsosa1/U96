using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucSituacionStock.xaml
    /// </summary>
    public partial class ucSituacionStock : UserControl
    {
        #region Declarativas

        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<StockTipoProducto> stk_activo_tipos = new ObservableCollection<StockTipoProducto>();
        ObservableCollection<StockTipoProducto> stk_mante_tipos = new ObservableCollection<StockTipoProducto>();
        ObservableCollection<StockTipoProducto> stk_pp_tipos = new ObservableCollection<StockTipoProducto>();
        ObservableCollection<StockCategoriaProducto> stk_categoria = new ObservableCollection<StockCategoriaProducto>();
        ObservableCollection<StockCategoriaProducto> stk_categoria_pp = new ObservableCollection<StockCategoriaProducto>();
        ObservableCollection<StockProducto> lista_productos = new ObservableCollection<StockProducto>();
        private decimal _costoTotalStockTipo;
        private int _tipoStockSeleccionado; //1 activo 2 mante 3 pp
        CultureInfo cultureInfo = new CultureInfo("es-Ar");
        #endregion

        public ucSituacionStock()
        {
            InitializeComponent();

            stk_activo_tipos = coreProducto.ListarStockActualTipoProductos(1, 2);//deposito 1 sf 2(en deposito)
            stk_mante_tipos = coreProducto.ListarStockActualTipoProductos(1, 3);// deposito 1 sf 3(mantenimiento)
            stk_pp_tipos = coreProducto.ListarStockPPTipoProducto(1, 2); //deposito ypunto pedido
            dgStkActivo.ItemsSource = stk_activo_tipos;
            dgStkActivo.DataContext = stk_activo_tipos;
            dgStkMante.ItemsSource = stk_mante_tipos;
            dgStkMante.DataContext = stk_mante_tipos;
            dgStkPP.ItemsSource = stk_pp_tipos;
            dgStkPP.DataContext = stk_pp_tipos;
            //llamamos al metodo calcular de costo del tipo de stock seleccionado
            CalcularCostosTiposStocks();

        }

        private void CalcularCostosTiposStocks()
        {
            foreach (var item in stk_activo_tipos)
            {
                _costoTotalStockTipo = _costoTotalStockTipo + item.CostoStockActual;
            }
            txbCTStkActivo.Text = _costoTotalStockTipo.ToString("C", cultureInfo);
            _costoTotalStockTipo = 0;
            foreach (var item in stk_mante_tipos)
            {
                _costoTotalStockTipo = _costoTotalStockTipo + item.CostoStockActual;
            }
            txbCTStkMante.Text = _costoTotalStockTipo.ToString("C", cultureInfo);
            _costoTotalStockTipo = 0;
            foreach (var item in stk_pp_tipos)
            {
                _costoTotalStockTipo = _costoTotalStockTipo + item.CostoStockActual;
            }
            txbCTStkPP.Text = _costoTotalStockTipo.ToString("C", cultureInfo);
            _costoTotalStockTipo = 0;
        }

        private void dgStkActivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            StockTipoProducto dt = dgStkActivo.SelectedItem as StockTipoProducto;
            if (dt != null)
            {
                //llamamos al metodo que devuelve las categorias para el tipo seleccionado
                stk_categoria = coreProducto.ListarStockActualCategoriasTipoProducto(dt.IdTipoP, 1, 2);
                dgStkCategoria.ItemsSource = stk_categoria;
                dgStkCategoria.DataContext = stk_categoria;
                txbNombreTipoProducto.Text = dt.NombreTipo;
                txbTipoStock.Text = "Stock Activo";
                _tipoStockSeleccionado = 1;
            }
        }

        private void dgStkMante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            StockTipoProducto dt = dgStkMante.SelectedItem as StockTipoProducto;
            if (dt != null)
            {
                //llamamos al metodo que devuelve las categorias para el tipo seleccionado
                stk_categoria = coreProducto.ListarStockActualCategoriasTipoProducto(dt.IdTipoP, 1, 3);
                dgStkCategoria.ItemsSource = stk_categoria;
                dgStkCategoria.DataContext = stk_categoria;
                txbNombreTipoProducto.Text = dt.NombreTipo;
                txbTipoStock.Text = "Stock Mantenimiento";
                _tipoStockSeleccionado = 2;
            }
        }

        private void dgStkPP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            StockTipoProducto dt = dgStkPP.SelectedItem as StockTipoProducto;
            if (dt != null)
            {
                //llamamos al metodo que devuelve las categorias para el tipo seleccionado
                stk_categoria_pp = coreProducto.ListarStockPPCategoriasTipoProducto(dt.IdTipoP, 1, 2);
                dgStkCategoria.ItemsSource = stk_categoria_pp;
                dgStkCategoria.DataContext = stk_categoria_pp;
                txbNombreTipoProducto.Text = dt.NombreTipo;
                txbTipoStock.Text = "Stock Punto Pedido";
                _tipoStockSeleccionado = 3;
            }
        }

        private void dgStkCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StockCategoriaProducto dt = dgStkCategoria.SelectedItem as StockCategoriaProducto;
            if (dt != null)
            {


                switch (_tipoStockSeleccionado)
                {
                    case 1:
                        lista_productos = coreProducto.ListarCantProXCateActivo(dt.IdCategoria, 1, 2);
                        dgStkProductosCate.ItemsSource = lista_productos;
                        dgStkProductosCate.DataContext = lista_productos;
                        break;
                    case 2:
                        lista_productos = coreProducto.ListarCantProXCateMante(dt.IdCategoria);
                        dgStkProductosCate.ItemsSource = lista_productos;
                        dgStkProductosCate.DataContext = lista_productos;
                        break;
                    case 3:
                        lista_productos = coreProducto.ListarCantProXCatePP(dt.IdCategoria, 1, 2);
                        dgStkProductosCate.ItemsSource = lista_productos;
                        dgStkProductosCate.DataContext = lista_productos;
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
