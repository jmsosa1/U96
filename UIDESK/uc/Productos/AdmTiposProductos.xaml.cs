using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using UIDESK.ABM;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para AdmTiposProductos.xaml
    /// </summary>
    public partial class AdmTiposProductos : MaterialWindow 
    {
        #region Declarativas

        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>();
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>();
        ObservableCollection<SegmentoP> segmentoPs = new ObservableCollection<SegmentoP>();
        string _tipoOperacion; // indica el tipo de operacion seleccionado en la barra de operaciones 

        //estas vaiables publicas se usan para intercambiar informacion de que tipo , categoria y linea estan seleccionados en todo momento
        public int _tiposelect = 0;
        public int _cateselec = 0;
        public int _lineaselec = 0;
        public ICollectionView vistaCategorias
        {
            get { return CollectionViewSource.GetDefaultView(categoriaPs); }
        }

        CultureInfo ci = new CultureInfo("es-Ar");
        #endregion

        public AdmTiposProductos()
        {
            InitializeComponent();
            tipoProductos = coreProducto.ListarTipos();
            categoriaPs = null;
            segmentoPs = null;
            dgTipos.ItemsSource = tipoProductos;
            dgTipos.DataContext = tipoProductos;
            dgCategoria.ItemsSource = categoriaPs;
            dgCategoria.DataContext = categoriaPs;
            dgSegmento.ItemsSource = segmentoPs;
            dgSegmento.DataContext = segmentoPs;

        }


        #region EventosVentana


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion

        #region EventosDeBotones

        private void btnAddTipo_Click(object sender, RoutedEventArgs e)
        {
            TipoProducto tipo = new TipoProducto();
            ABMTipoP aBM = new ABMTipoP(tipo,1);
            if (aBM.ShowDialog()== true)
            {
                MessageBox.Show("Se agrego el nuevo tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                tipoProductos = coreProducto.ListarTipos();
                dgTipos.ItemsSource = tipoProductos;
                dgTipos.DataContext = tipoProductos;
            }


        }

        private void btnMTipo_Click(object sender, RoutedEventArgs e)
        {
            TipoProducto tipo = dgTipos.SelectedItem as TipoProducto;
            if (tipo != null)
            {
                ABMTipoP aBM = new ABMTipoP(tipo, 2);
                if (aBM.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    tipoProductos = coreProducto.ListarTipos();
                    dgTipos.ItemsSource = tipoProductos;
                    dgTipos.DataContext = tipoProductos;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelTipo_Click(object sender, RoutedEventArgs e)
        {

            TipoProducto tipo = dgTipos.SelectedItem as TipoProducto;
            if (tipo != null)
            {

                _tipoOperacion = "BT";

            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnDelCate_Click(object sender, RoutedEventArgs e)
        {
            CategoriaP categoriaP = dgCategoria.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                MessageBoxResult respuesta = MessageBox.Show("Desea Eliminar la categoria seleccionada?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (respuesta == MessageBoxResult.Yes)
                {
                    //borrar el segmento, pero antes verificar si existen productos con este segmento
                }
            }
        }

        private void btnMCate_Click(object sender, RoutedEventArgs e)
        {
            TipoProducto tipo = dgTipos.SelectedItem as TipoProducto;
            CategoriaP categoriaP = dgCategoria.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                ABMCateP nueva_catep = new ABMCateP(categoriaP, 2); // pasamos como uno de los parametros el id de la categoria
                nueva_catep.txtNombreTipo.Text = tipo.NomTipo;

                if (nueva_catep.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo el listado de categorias ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                    dgCategoria.ItemsSource = categoriaPs;
                    dgCategoria.DataContext = categoriaPs;

                }
            }


        }

        private void btnAddCate_Click(object sender, RoutedEventArgs e)
        {
            TipoProducto tipo = dgTipos.SelectedItem as TipoProducto;
            if (tipo != null)
            {
                CategoriaP c = new CategoriaP();
                c.IdTipoP = tipo.IdTipoP;
                ABMCateP nueva_catep = new ABMCateP(c, 1);
                nueva_catep.txtNombreTipo.Text = tipo.NomTipo;
                if (nueva_catep.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo el listado de categorias ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgTipos.ItemsSource = tipoProductos;
                    dgTipos.DataContext = tipoProductos;
                    dgCategoria.ItemsSource = categoriaPs;
                    dgCategoria.DataContext = categoriaPs;

                }
            }


        }

        private void btnAddSeg_Click(object sender, RoutedEventArgs e)
        {
            CategoriaP categoriaP = dgCategoria.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                SegmentoP segmento = new SegmentoP();
                segmento.IdCateP = categoriaP.IdCateP;
                ABMSegP nuevo_seg = new ABMSegP(segmento, 1);
                nuevo_seg.txtNombreCate.Text = categoriaP.NomCateP;
                if (nuevo_seg.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo el listado de categorias", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgSegmento.ItemsSource = segmentoPs;
                    dgSegmento.DataContext = segmentoPs;
                }
            }

        }

        private void btnMSeg_Click(object sender, RoutedEventArgs e)
        {
            CategoriaP categoriaP = dgCategoria.SelectedItem as CategoriaP;
            SegmentoP segmentoP = dgSegmento.SelectedItem as SegmentoP;
            if (categoriaP != null && segmentoP != null)
            {
                ABMSegP nuevo_seg = new ABMSegP(segmentoP, 2);
                 nuevo_seg.txtNombreCate.Text = categoriaP.NomCateP;
                //nuevo_seg.txtNombreSeg.Text = segmentoP.NombreSeg;
                if (nuevo_seg.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo el listado de categorias", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgSegmento.ItemsSource = segmentoPs;
                    dgSegmento.DataContext = segmentoPs;
                }
            }

        }

        private void btnDelSeg_Click(object sender, RoutedEventArgs e)
        {
            SegmentoP segmentoP = dgSegmento.SelectedItem as SegmentoP;
            if (segmentoP != null)
            {
                MessageBoxResult respuesta = MessageBox.Show("Desea Eliminar el segmento seleccionado?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (respuesta == MessageBoxResult.Yes)
                {
                    //borrar el segmento, pero antes verificar si existen productos con este segmento
                }
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region EventosDeGrid
        private void dgTipos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TipoProducto tipo = dgTipos.SelectedItem as TipoProducto;
            if (tipo != null)
            {


                categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
                dgCategoria.ItemsSource = categoriaPs;
                dgCategoria.DataContext = categoriaPs;
                dgSegmento.ItemsSource = null;
                dgSegmento.DataContext = null;

            }

        }

        private void dgCategoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CategoriaP categoriaP = dgCategoria.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {


                segmentoPs = coreProducto.ListarSegmentoCategoria(categoriaP.IdCateP);
                dgSegmento.ItemsSource = segmentoPs;
                dgSegmento.DataContext = segmentoPs;
            }
        }

        private void dgSegmento_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SegmentoP segmentoP = dgSegmento.SelectedItem as SegmentoP;
            if (segmentoP != null)
            {

            }
        }





        #endregion

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private bool filtroCategoria(object obj)
        {
            CategoriaP categoria = obj as CategoriaP;
            return categoria.NomCateP.Contains(txtBuscar.Text);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // buscar
                if (!string.IsNullOrEmpty(txtBuscar.Text))
                {
                    vistaCategorias.Filter = filtroCategoria;
                }

            }
        }

        private void btnSeleccionTCL_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
