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
    /// Lógica de interacción para ucRemitosDSO.xaml
    /// </summary>
    public partial class ucRemitosDSO : UserControl
    {
        #region Declarativas
        BLLRemito coreRemito = new BLLRemito();
        BLLEmpleados coreEmpleado = new BLLEmpleados();
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Documento> lista_doc = new ObservableCollection<Documento>();
        ObservableCollection<Documento> lista_dso = new ObservableCollection<Documento>();
        ObservableCollection<DocumentoDetalle> lista_det_dso = new ObservableCollection<DocumentoDetalle>();
        public int _cantRemitos = 0;
        public int _filtroId = 0; // parametros que se recibe y sirve para determinar si se aplica filtro por id de documento
        #endregion



        public ucRemitosDSO(int _iddocu)
        {
            InitializeComponent();
            lista_doc = coreRemito.ListarDocObras();
            _filtroId = _iddocu;
            ArmarLista(_filtroId);

            /*  if (_filtroId==0) // si la varialbe de control de filtro es cero , entonces devuelvo una lista con todos los documentos
              {
                  foreach (var item in lista_doc)
                  {
                      if (item.IdTipoRem == 1)
                      {
                          lista_dso.Add(item);
                      }
                  }
              }
              else
              {
                  // si la variable no es cero, entonces devuelvo un solo documento buscado
                  foreach (var item in lista_doc)
                  {
                      if (item.IdDocumento==_filtroId)
                      {
                          lista_dso.Add(item);
                      }
                  }
              }*/

            dgPrincipal.DataContext = lista_dso;
            dgPrincipal.ItemsSource = lista_dso;
        }

        private void ArmarLista(int filtroId)
        {

            if (_filtroId == 0) // si la varialbe de control de filtro es cero , entonces devuelvo una lista con todos los documentos
            {
                foreach (var item in lista_doc)
                {
                    if (item.IdTipoRem == 1)
                    {
                        lista_dso.Add(item);
                    }
                }
            }
            else
            {
                // si la variable no es cero, entonces devuelvo un solo documento buscado
                foreach (var item in lista_doc)
                {
                    if (item.IdDocumento == _filtroId)
                    {
                        lista_dso.Add(item);
                    }
                }
            }
        }

        private void dgPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento d = dgPrincipal.SelectedItem as Documento;
            if (d != null)
            {
                ucDetalleDocumento._iddocumento = d.IdDocumento;

            }
        }

        private void btnReImprimir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Documento docObras = dgPrincipal.SelectedItem as Documento;
            if (docObras != null)
            {
                int _iddocu = docObras.IdDocumento;

                //llamamos a la ventana de imprimir documento
                PrintRemitoObra printRemitoObra = new PrintRemitoObra(_iddocu);
                if (printRemitoObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //metodo imprimir con RSS
                //ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_iddocu);
                //imprimir.Show();
            }
        }

        private void btnAnular_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Documento documento = dgPrincipal.SelectedItem as Documento;
            MessageBoxResult _result = MessageBox.Show("Desea anular el remito de obra numero :?" + documento.IdDocumento + "", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (_result == MessageBoxResult.Yes)
            {
                //aca arranca el procedimiento para anular el remito


                //1)actualizar el balance del empleado en funcion del tipo de concepto
                // buscamos el detalle del remito en cuestion
                ObservableCollection<DocumentoDetalle> detalles = coreRemito.BuscarUnDocDetallePorId(documento.IdDocumento);
                foreach (var item in detalles)
                {
                    //item por item vamos aplicando la correccion
                    coreEmpleado.ActualizarBalancePorBajaDSO(documento.Imputacion, documento.IdEmpleado, item.CodigoItem, item.CantidadItem, item.PrecioItem);
                    // luego se debe actualizar el stock del deposito que figura en el documento sumando las cantidades que egresaron
                    coreProducto.ActualizarStock(item.CodigoItem, item.CantidadItem, documento.IdDeposito, "Ingreso", item.PrecioItem);
                }
                //2) una vez que actualizamos los balances y el stock borramos el documento
                coreRemito.AnularUnRemitoObra(documento.IdDocumento);
                //3)una vez que se finalice la actualizacion informamos
                MessageBox.Show("Se Anulo el remito seleccionado y actualizado el balance del empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                ArmarLista(0);
                dgPrincipal.DataContext = lista_dso;
                dgPrincipal.ItemsSource = lista_dso;


            }
        }

        private void btnCerrarDetalle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // codigo para cerrar el detalle del registro seleccionado 
            //lo hacemos set la propiedad selectedindex a -1 (no hay objeto seleccionado)
            dgPrincipal.SelectedIndex = -1;
        }
    }
}
