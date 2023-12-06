using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.imprimir;
using UIDESK.Remitos;


namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucRemitosDMO.xaml
    /// </summary>
    public partial class ucRemitosDMO : UserControl
    {
        #region Declarativas
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Documento> lista_dso = new ObservableCollection<Documento>();
        ObservableCollection<DocumentoDetalle> lista_det_dso = new ObservableCollection<DocumentoDetalle>();
        ObservableCollection<Documento> lista_dmo = new ObservableCollection<Documento>();
        public int _cantRemitos = 0;
        public int _filtroId = 0; // parametros que se recibe y sirve para determinar si se aplica filtro por id de documento
        #endregion

        public ucRemitosDMO(int _iddocu)
        {
            InitializeComponent();
            lista_dso = coreRemito.ListarDoc14();
            _filtroId = _iddocu;
            if (_filtroId == 0) // si la varialbe de control de filtro es cero , entonces devuelvo una lista con todos los documentos
            {
                foreach (var item in lista_dso)
                {
                    lista_dmo.Add(item);
                }
            }
            else
            {
                // si la variable no es cero, entonces devuelvo un solo documento buscado
                foreach (var item in lista_dso)
                {
                    if (item.IdDocumento == _filtroId)
                    {
                        lista_dmo.Add(item);
                    }
                }
            }

            dgPrincipal.DataContext = lista_dmo;
            dgPrincipal.ItemsSource = lista_dmo;
        }

        private void dgPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento d = dgPrincipal.SelectedItem as Documento;
            if (d != null)
            {
                ucDetalleDocumento._iddocumento = d.IdDocumento;

            }
        }

        private void btnCerrarDetalle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // codigo para cerrar el detalle del registro seleccionado 
            //lo hacemos set la propiedad selectedindex a -1 (no hay objeto seleccionado)
            dgPrincipal.SelectedIndex = -1;
        }

        private void btnReImprimir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Documento docObras = dgPrincipal.SelectedItem as Documento;

            if (docObras != null)
            {
                int _iddocu = docObras.IdDocumento;
                PrintRemitoDMO printRemitoObra = new PrintRemitoDMO(_iddocu);
                //ImprimirDMO imprimir = new ImprimirDMO(_iddocu);
                //imprimir.Show();
                if (printRemitoObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnAnular_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*
            Documento documento = dgPrincipal.SelectedItem as Documento;
            MessageBoxResult _result = MessageBox.Show("Desea anular el remito de obra numero :?" + documento.IdDocumento + "", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (_result == MessageBoxResult.Yes)
            {
                //aca arranca el procedimiento para anular el remito
                //1) cambiar el estado del remito a estado anulado , valor 10
                string _concepto = documento.Concepto;
                coreRemito.AnularUnRemitoObra(documento.IdDocumento);
                //2)actualizar el balance del empleado en funcion del tipo de concepto
                // buscamos el detalle del remito en cuenstion
                ObservableCollection<DocumentoDetalle> detalles = coreRemito.BuscarUnDocDetallePorId(documento.IdDocumento);
                foreach (var item in detalles)
                {
                    //item por item vamos aplicando la correccion
                    coreRemito.RestauracionItemRemitoObra(documento.Imputacion, documento.IdEmpleado, item.CodigoItem, item.CantidadItem, documento.Concepto);
                }
                //3)una vez que se finalice la actualizacion informamos
                MessageBox.Show("Se Anulo el remito seleccionado y actualizado el balance del empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);



            }*/
        }
    }
}
