using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucGestionEPPEntregas.xaml
    /// </summary>
    public partial class ucGestionEPPEntregas : UserControl
    {

        BLLProducto coreProducto = new BLLProducto();
        BLLBase coreBase = new BLLBase();
        ObservableCollection<CategoriaP> stock_categoria_anio = new ObservableCollection<CategoriaP>();
        ObservableCollection<StockProducto> stock_producto_anio = new ObservableCollection<StockProducto>();
        List<Deposito> _lista_depositos = new List<Deposito>();
        ObservableCollection<SegmentoP> _segmentoPs = new ObservableCollection<SegmentoP>();
        SegmentoP _segmentoP = new SegmentoP();
        Deposito _deposito = new Deposito();

        public ICollectionView vistaProductos
        {
            get { return CollectionViewSource.GetDefaultView(stock_producto_anio); }
        }

        public ucGestionEPPEntregas()
        {
            InitializeComponent();
            _lista_depositos = coreBase.ListarDepositos();
            cmbDeposito.ItemsSource = _lista_depositos;
            cmbDeposito.DataContext = _lista_depositos;
        }

        #region Filtros

        private bool filtroSegmento(object obj)
        {
            
            StockProducto stock = obj as StockProducto;
            return stock.IdSegmento == _segmentoP.IdSegmentoP;
        }
        #endregion

        private void btnResumenAnio_Click(object sender, RoutedEventArgs e)
        {

            //primero chekeamos que se haya escrito un año en el textbox del año
            if (string.IsNullOrWhiteSpace(txtAnioSeleccion.Text))
            {
                MessageBox.Show("Debe ingresar un año para consultar", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                //si esta todo bien, aca debemos ejecutar la llamada al metodo
                int _anio = Convert.ToInt32(txtAnioSeleccion.Text);
                _deposito = cmbDeposito.SelectedItem as Deposito;
                
                stock_categoria_anio = coreProducto.ListarEntregasIndumentariaAnio(_anio, _deposito.IdDeposito);
                dgEntregas.ItemsSource = stock_categoria_anio;
                dgEntregas.DataContext = stock_categoria_anio;

            }

        }

        private void btnResumenF1F2_Click(object sender, RoutedEventArgs e)
        {
            //primero chekeamos que se haya seleccioanado las dos fechas
            if (dtpDesde.SelectedDate == null)
            {
                MessageBox.Show("Debe elegir una fecha Desde", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                if (dtphasta.SelectedDate == null)
                {
                    MessageBox.Show("Debe elegir una fecha Hasta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    //si esta todo bien
                    //primero blanqueamos la grid detalle
                    dgDetalleEntrega.ItemsSource = null;
                    dgDetalleEntrega.DataContext = "";
                    //luego seteamos los variables
                    //int _iddepo = Convert.ToInt16(txtIdDeposito.Text);
                    _deposito = cmbDeposito.SelectedItem as Deposito;
                    txtAnioSeleccion.Text = "";
                    stock_categoria_anio = coreProducto.ListarEntregasIndumentarioF1F2(_deposito.IdDeposito, dtpDesde.SelectedDate.Value, dtphasta.SelectedDate.Value);
                    dgEntregas.ItemsSource = stock_categoria_anio;
                    dgEntregas.DataContext = stock_categoria_anio;

                }

            }
        }

        private void btnImprimirResumen_Click(object sender, RoutedEventArgs e)
        {
            //este es el procedimiento para imprimir el informe del stock de la categoria
            /*CategoriaP cp = dgStockCategorias.SelectedItem as CategoriaP;

            if (cp != null)
            {
                int _idcate = cp.IdCateP;
                int _iddepo = Convert.ToInt32(txtIdDeposito.Text);
                string _url = "http://pc-128/reports/report/ServerInformes/DetalleStockCategoriaProducto?idcatep=" + _idcate + "&iddeposito=" + _iddepo + "";
                _ = System.Diagnostics.Process.Start(_url);
            }
            else
            {
                MessageBox.Show("el objeto es nulo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }*/
        }



        private void dgEntregas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaP p = dgEntregas.SelectedItem as CategoriaP;
            if (p != null)
            {
                //int _iddepo = Convert.ToInt16(txtIdDeposito.Text);
                _segmentoPs = coreProducto.ListarSegmentoCategoria(p.IdCateP);
                cmbSegmento.ItemsSource = _segmentoPs;
                cmbSegmento.DataContext = _segmentoPs;
                int _anio;
                if (string.IsNullOrWhiteSpace(txtAnioSeleccion.Text))
                {
                    //si el valor del campo anio es null , significa que estamos haciendo una consulta por fechas
                    //no se necesita el valor del año

                    stock_producto_anio = coreProducto.ListarDetalleUnaCategoriaF1F2(p.IdCateP, _deposito.IdDeposito, dtpDesde.SelectedDate.Value, dtphasta.SelectedDate.Value);
                    dgDetalleEntrega.ItemsSource = stock_producto_anio;
                    dgDetalleEntrega.DataContext = stock_producto_anio;
                   
                }
                else
                {
                    //si el valor del cuadro de texto del anio seleccionado es distinto de cero 
                    //entonces lo asignamos a la variable y lo pasamos como parametro
                    _anio = Convert.ToInt32(txtAnioSeleccion.Text);
                    
                    stock_producto_anio = coreProducto.ListarDetalleUnaCategoriaAnio(p.IdCateP,_deposito.IdDeposito, _anio);
                    dgDetalleEntrega.ItemsSource = stock_producto_anio;
                    dgDetalleEntrega.DataContext = stock_producto_anio;

                }
            }
        }

        private void cmbDeposito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _deposito = cmbDeposito.SelectedItem as Deposito;
        }

        private void cmbSegmento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // de acuerdo al segmento seleccionado se redefine la vista
            
            _segmentoP = cmbSegmento.SelectedItem as SegmentoP;
            if (_segmentoP != null)
            {


                vistaProductos.Filter = filtroSegmento;
                CalcularCantidades();
            }
        }


        private void CalcularCantidades()
        {
            decimal _cant = 0;
           
            foreach (var item in stock_producto_anio)
            {
                if (item.IdSegmento == _segmentoP.IdSegmentoP)
                {


                    _cant = _cant + item.StkActual;
                }
            }
            txbCantidadSegmento.Text = _cant.ToString();
        }
    }
}
