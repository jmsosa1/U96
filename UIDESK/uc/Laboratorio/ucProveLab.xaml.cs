using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIDESK.uc.Laboratorio
{
    /// <summary>
    /// Lógica de interacción para ucProveLab.xaml
    /// </summary>
    public partial class ucProveLab : UserControl
    {
        #region Declarativas
        BLLProveedor core = new BLLProveedor();
        ObservableCollection<Proveedor> lista_proveedores = new ObservableCollection<Proveedor>();
       

        public ICollectionView vistaProveedor
        {
            get { return CollectionViewSource.GetDefaultView(lista_proveedores); }
        }


        #endregion
        public ucProveLab()
        {
            InitializeComponent();
            lista_proveedores = core.ProveedorPorRubro(6);
            
           
            dgGralProveedor.ItemsSource = lista_proveedores;
            dgGralProveedor.DataContext = lista_proveedores;
        }

        private void btnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void dgGralProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCerrarDetalle_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
