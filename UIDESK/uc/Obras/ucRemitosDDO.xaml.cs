using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.imprimir;
using UIDESK.Remitos;


namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucRemitosDDO.xaml
    /// </summary>
    public partial class ucRemitosDDO : UserControl
    {
        #region Declarativas
        BLLRemito coreRemito = new BLLRemito();
        BLLEmpleados coreEmpleado = new BLLEmpleados();
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Documento> lista_doc = new ObservableCollection<Documento>();
        ObservableCollection<Documento> lista_ddo = new ObservableCollection<Documento>();
        ObservableCollection<DocumentoDetalle> lista_det_dso = new ObservableCollection<DocumentoDetalle>();
        public int _cantRemitos = 0;
        public int _filtroId = 0; // parametros que se recibe y sirve para determinar si se aplica filtro por id de documento
        DateTime _fechaDesde = DateTime.Now.AddDays(-30);
        DateTime _fechaHasta = DateTime.Now;
        #endregion

        public ucRemitosDDO(int _iddocu)
        {
            InitializeComponent();
            lista_doc = coreRemito.ListarDocObras(_fechaDesde, _fechaHasta);
            _filtroId = _iddocu;
            if (_filtroId == 0)
            {


                foreach (var item in lista_doc)
                {
                    if (item.IdTipoRem == 4)
                    {
                        lista_ddo.Add(item);
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
                        lista_ddo.Add(item);
                    }
                }
            }
            dgPrincipal.DataContext = lista_ddo;
            dgPrincipal.ItemsSource = lista_ddo;
        }


        #region Filtros

        #endregion

        private void dgPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento d = dgPrincipal.SelectedItem as Documento;
            if (d != null)
            {
                ucDetalleDocumento._iddocumento = d.IdDocumento;

            }
        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            // codigo para cerrar el detalle del registro seleccionado 
            //lo hacemos set la propiedad selectedindex a -1 (no hay objeto seleccionado)
            dgPrincipal.SelectedIndex = -1;
        }

        private void btnReImprimir_Click(object sender, RoutedEventArgs e)
        {
            Documento docObras = dgPrincipal.SelectedItem as Documento;
            if (docObras != null)
            {
                int _iddocu = docObras.IdDocumento;

                PrintRemitoObra printRemitoObra = new PrintRemitoObra(_iddocu);
                if (printRemitoObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //ImprimiDDO imprimir = new ImprimiDDO(_iddocu);
                //imprimir.Show();
            }

        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {

            Documento documento = dgPrincipal.SelectedItem as Documento;
            MessageBoxResult _result = MessageBox.Show("Desea anular el remito de obra numero :?" + documento.IdDocumento + "", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (_result == MessageBoxResult.Yes)
            {
                //aca arranca el procedimiento para anular el remito


                //2)actualizar el balance del empleado en funcion del tipo de concepto
                // buscamos el detalle del remito en cuenstion
                ObservableCollection<DocumentoDetalle> detalles = coreRemito.BuscarUnDocDetallePorId(documento.IdDocumento);
                foreach (var item in detalles)
                {
                    //item por item vamos aplicando la correccion
                    coreEmpleado.ActualizarBalancePorBajaDDO(documento.Imputacion, documento.IdEmpleado, item.CodigoItem, item.CantidadItem, item.PrecioItem);
                    // luego se debe actualizar el stock del deposito que figura en el documento restando las cantidades que ingresaron
                    coreProducto.ActualizarStock(item.CodigoItem, item.CantidadItem, documento.IdDeposito, "Egreso", item.PrecioItem);
                }
                //2) una vez que actualizamos los balances y el stock borramos el documento
                coreRemito.AnularUnRemitoObra(documento.IdDocumento);
                //3)una vez que se finalice la actualizacion informamos
                MessageBox.Show("Se Anulo el remito seleccionado y actualizado el balance del empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);



            }
        }
    }
}
