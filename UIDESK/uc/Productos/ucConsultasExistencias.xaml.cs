using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.imprimir;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucConsultasExistencias.xaml
    /// </summary>
    public partial class ucConsultasExistencias : UserControl
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<CategoriaP> lista_categorias = new ObservableCollection<CategoriaP>();
        ObservableCollection<ConsultaExistencias> existencia_obra = new ObservableCollection<ConsultaExistencias>();
        ObservableCollection<ConsultaExistencias> existencia_stock = new ObservableCollection<ConsultaExistencias>();

        public ICollectionView vistaCategorias
        {
            get { return CollectionViewSource.GetDefaultView(lista_categorias); }
        }

        #endregion
        public ucConsultasExistencias()
        {
            InitializeComponent();
            lista_categorias = coreProducto.ListarCategorias();

        }

        #region Filtros
        private bool filtroCategoria(object obj)
        {
            CategoriaP ct = obj as CategoriaP;

            return ct.NomCateP.Contains(txtBuscar.Text);

        }
        #endregion

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            vistaCategorias.Filter = filtroCategoria;
            dgCateEncontradas.ItemsSource = vistaCategorias;
            dgCateEncontradas.DataContext = vistaCategorias;
        }

        private void dgCateEncontradas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaP categoriaP = dgCateEncontradas.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {
                existencia_obra = coreProducto.ConsultaExistenciaEnObra(categoriaP.IdCateP);
                dgExistenciaObra.ItemsSource = existencia_obra;
                dgExistenciaObra.DataContext = existencia_obra;
                existencia_stock = coreProducto.ConsultaExistenciaEnStock(categoriaP.IdCateP);
                dgExistenciaStock.ItemsSource = existencia_stock;
                dgExistenciaStock.DataContext = existencia_stock;
                txtNombreCategoria.Text = categoriaP.NomCateP;
                txtExObra.Text = existencia_obra.Count.ToString();
                txtExDepo.Text = existencia_stock.Count.ToString();

            }

        }

        private void btnImprimiReporteObra_Click(object sender, RoutedEventArgs e)
        {
            CategoriaP categoriaP = dgCateEncontradas.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {


                ImprimirExistencia imprimir = new ImprimirExistencia(categoriaP.IdCateP, 1);
                imprimir.Show();
            }
        }

        private void btnImprimirReporteStock_Click(object sender, RoutedEventArgs e)
        {
            CategoriaP categoriaP = dgCateEncontradas.SelectedItem as CategoriaP;
            if (categoriaP != null)
            {


                ImprimirExistencia imprimir = new ImprimirExistencia(categoriaP.IdCateP, 2);
                imprimir.Show();
            }
        }
    }
}
