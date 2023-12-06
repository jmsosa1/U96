using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucSituacionOperativa.xaml
    /// </summary>
    public partial class ucSituacionOperativa : UserControl
    {
        #region Declarativa
        BLLResultado coreResultados = new BLLResultado();
        BLLVehiculos coreVehiculos = new BLLVehiculos();
        ObservableCollection<SituacionOperativaVh> lista_sop = new ObservableCollection<SituacionOperativaVh>();
        List<CategoriaVh> categorias = new List<CategoriaVh>();

        public ICollectionView vistaVehiculos
        {
            get { return CollectionViewSource.GetDefaultView(lista_sop); }
        }

        #endregion
        public ucSituacionOperativa()
        {
            InitializeComponent();
            lista_sop = coreResultados.SituacionOpListarTodas();
            categorias = coreVehiculos.VehiculosListarCategoria();
            cmbCategoriasVh.ItemsSource = categorias;
            AcondicionamientoListaSituacion();
            dgSituacionOP.ItemsSource = lista_sop;
            dgSituacionOP.DataContext = lista_sop;
        }


        #region Filtros
        private bool filtroCategoria(object obj)
        {
            SituacionOperativaVh vehiculo = obj as SituacionOperativaVh;
            if (cmbCategoriasVh.SelectedItem != null)
            {


                CategoriaVh categoria = cmbCategoriasVh.SelectedItem as CategoriaVh;

                return vehiculo.Categoria.Contains(categoria.NomCate);
            }
            else
            {
                return false;
            }
        }


        #endregion
        private void AcondicionamientoListaSituacion()
        {
            foreach (var item in lista_sop)
            {
                //caculamos los totales de la clase
                item.DiasUso = item.DiasAcumulados - item.DiasParo - item.DiasMante;
                item.CtDm = item.DiasMante * item.CostoDiarioParo;
                item.CtDP = item.DiasParo * item.CostoDiarioParo;
                item.TotalCDP = item.CtDm + item.CtDP;
                item.TotalCDU = item.CostoDiarioUso * item.DiasUso;
            }
            //cargamos la imagen de la relacion
            foreach (var item in lista_sop)
            {
                if (item.DiasUso > (item.DiasParo + item.DiasMante))
                {
                    byte[] img = File.ReadAllBytes(@"imagenes\se-green.png");
                    item.ImageEstado = img;
                }
                if (item.DiasUso < (item.DiasParo + item.DiasMante))
                {
                    byte[] img = File.ReadAllBytes(@"imagenes\se-red.png");
                    item.ImageEstado = img;
                }

            }
        }



        private void cmbCategoriasVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaVehiculos.Filter = filtroCategoria;
            dgSituacionOP.ItemsSource = vistaVehiculos;
            dgSituacionOP.DataContext = vistaVehiculos;
        }
    }
}
