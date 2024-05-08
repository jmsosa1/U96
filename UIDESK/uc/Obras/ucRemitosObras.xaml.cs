using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.imprimir;
using UIDESK.Remitos;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucRemitosObras.xaml
    /// </summary>
    public partial class ucRemitosObras : UserControl
    {
        #region Declarativas
        BLLRemito coreRemito = new BLLRemito();
        BLLEmpleados coreEmpleado = new BLLEmpleados();
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Documento> lista_doc = new ObservableCollection<Documento>();
        DateTime _fechaDesde = DateTime.Now.AddDays(-30); 
        DateTime _fechaHasta = DateTime.Now;
        public ICollectionView vistaRemitos
        {
            get { return CollectionViewSource.GetDefaultView(lista_doc); }
        }


        int _tipodocu = 0; // contiene el tipo de documento que estamos viendo en un momento determinado en el content controls
        int _imputacion = 0;


        #endregion

        public ucRemitosObras()
        {
            InitializeComponent();
            lista_doc = coreRemito.ListarDocObras(_fechaDesde,_fechaHasta);
            dgPrincipal.ItemsSource = lista_doc;
            dgPrincipal.DataContext = lista_doc;
           dtpDesde.SelectedDate = _fechaDesde;
            dtpHasta.SelectedDate = _fechaHasta;

        }

        #region Filtros

        private bool filtroTipoRemito (object obj)
        {
            //devuelve los documentos indicados en el tipo de remito seleccionado 
            //relacionados a una obra o en un listado general
            Documento p = obj as Documento;
            if (_imputacion == 0)
            {    //si no se indica la imputacion se genera un listado general de documentos 
                return p.IdTipoRem == _tipodocu;
            }
            else
            {
                // si se indica la imputacion se genera un listado de los tipos de documentos seleccionado
                // para esa obra en particular
                return p.IdTipoRem == _tipodocu && p.Imputacion == _imputacion;
            }
            
        }

        private bool filtroNumeroRemito(object obj)
        {
            // devuelve un solo objeto que corresponde al numero de remito indicado en el cuadro de busqueda
            Documento p = obj as Documento;
            int _iddocu = Convert.ToInt32(txtBuscar.Text);
            if (_iddocu == 0)
            { return false; }
            else
            {


                return p.IdDocumento == _iddocu;
            }
        }

        private bool filtroImputacion(object obj)
        {
            Documento p = obj as Documento;
             _imputacion = Convert.ToInt32(txtImputacion.Text);
            if (_tipodocu==0)
            {
                return p.Imputacion == _imputacion;
            }
            else
            {
                return p.Imputacion == _imputacion && p.IdTipoRem == _tipodocu;
            }
           
        }
        #endregion


        #region botones
        private void btnReImprimir_Click(object sender, RoutedEventArgs e)
        {
            Documento docObras = dgPrincipal.SelectedItem as Documento;
            if (docObras != null)
            {
                int _iddocu = docObras.IdDocumento;

                //llamamos a la ventana de imprimir documento
                // dependiendo del tipo de remito

                if (docObras.IdTipoRem == 1 || docObras.IdTipoRem == 4)
                {
                    //si es DSO / DDO
                    PrintRemitoObra printRemitoObra = new PrintRemitoObra(_iddocu);
                    if (printRemitoObra.ShowDialog() == true)
                    {
                        MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    //si es DMO
                    PrintRemitoDMO printRemitoObra = new PrintRemitoDMO(docObras.IdDocumento);
                    //ImprimirDMO imprimir = new ImprimirDMO(_iddocu);
                    //imprimir.Show();
                    if (printRemitoObra.ShowDialog() == true)
                    {
                        MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                //metodo imprimir con RSS
                //ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_iddocu);
                //imprimir.Show();


            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //para activar la busqueda por numero de documento , debemos primero saber que tipo de documento estamos viendo
            // pasamos ese tipo de documento como parametro al constructor del contenido en particular
           
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {

                // como esta en blanco, debo revisar el ultimo valor de tipodocu 
                //y aplicar el fltro para esos documentos
                vistaRemitos.Filter = filtroTipoRemito;
            }
            else
            {
                vistaRemitos.Filter = filtroNumeroRemito;
            }

        }

        #endregion




        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            RefrescarLista();
            _tipodocu = 1;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                _imputacion = 0;
            }
            vistaRemitos.Filter = filtroTipoRemito;

        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            RefrescarLista();
            _tipodocu = 4;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                _imputacion = 0;
            }
            vistaRemitos.Filter = filtroTipoRemito;
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            RefrescarLista();
            _tipodocu = 14;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                _imputacion = 0; //  si el cuadro de texto imputacion esta vacio , ponemos la imputacion en cero
            }
            vistaRemitos.Filter = filtroTipoRemito;
        }

        private void btnFiltroImputacion_Click(object sender, RoutedEventArgs e)
        {
            vistaRemitos.Filter = filtroImputacion;
        }

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
            dgPrincipal.SelectedIndex = -1;
        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void RefrescarLista()
        {
            // antes de aplicar el filtro, referescamos la lista con los valores de los datepicker
            _fechaDesde = dtpDesde.SelectedDate.Value;
            _fechaHasta = dtpHasta.SelectedDate.Value;
            //chekeamos que las fechas tengan sentido
            if (_fechaDesde > _fechaHasta)
            {
                MessageBox.Show("La fecha desde no puede ser mayor al valor de la fecha hasta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                lista_doc = coreRemito.ListarDocObras(_fechaDesde, _fechaHasta);
                dgPrincipal.ItemsSource = lista_doc;
                dgPrincipal.DataContext = lista_doc;

            }
        }
        
    }
}
