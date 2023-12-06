using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.imprimir;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucRemitos.xaml
    /// </summary>
    public partial class ucRemitos : UserControl
    {
        BLLRemito bllRemitos = new BLLRemito();
        Documento _docEncabezado = new Documento();


        ObservableCollection<Documento> lista_documentos = new ObservableCollection<Documento>();
        public ICollectionView vistaRemitos
        {
            get { return CollectionViewSource.GetDefaultView(lista_documentos); }
        }

        public ucRemitos()
        {
            InitializeComponent();
            lista_documentos = bllRemitos.ListarRemitosREVH();
            dgGralRemitos.ItemsSource = lista_documentos;
            dgGralRemitos.DataContext = lista_documentos;
            //vistaRemitos.Filter = new System.Predicate<object>(buscarDocumento);
            //vistaRemitos.Filter = new Predicate<object>(buscarDocumentoImputacion);


            dtpFiltroDesde.SelectedDate = DateTime.Today.AddDays(-30);
            dtpFiltroHasta.SelectedDate = DateTime.Today.AddDays(1);

        }


        #region Filtros


        private bool buscarDocumentoImputacion(object obj)
        {
            Documento d = obj as Documento;
            int _imp;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                _imp = 0;
            }
            else
            {
                _imp = Convert.ToInt32(txtImputacion.Text);
            }
            return d.Imputacion == _imp;
        }



        private bool buscarDocumento(object obj)
        {
            Documento doc = obj as Documento;

            string _b = txtBuscar.Text;

            return doc.NumDocumento.Contains(_b) || doc.ClienteObra.Contains(_b) ||
            doc.NombreEmpleado.Contains(_b) || doc.Chofer.Contains(_b) || doc.Transporte.Contains(_b);
        }


        private bool buscarIdDocumento(object obj)
        {
            Documento d = obj as Documento;
            int _imp = Convert.ToInt32(txtBuscar.Text);
            return d.IdDocumento == _imp;

        }
        #endregion

        #region Botones
        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgGralRemitos.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ABMDocumento documento = new ABMDocumento();
            if (documento.ShowDialog() == true)
            {
                MessageBox.Show("Se genero un nuevo documento", "Aviso", MessageBoxButton.OK);
                lista_documentos = bllRemitos.ListarRemitosREVH();
                dgGralRemitos.ItemsSource = lista_documentos;
                dgGralRemitos.DataContext = lista_documentos;
            }
        }

        private void BtnBuscarFechas_Click(object sender, RoutedEventArgs e)
        {
            lista_documentos = bllRemitos.ListarDesdeHasta(dtpFiltroDesde.SelectedDate.Value, dtpFiltroHasta.SelectedDate.Value);
            dgGralRemitos.ItemsSource = lista_documentos;
            dgGralRemitos.DataContext = lista_documentos;
        }

        private void BtnBuscarImputacion_Click(object sender, RoutedEventArgs e)
        {
            vistaRemitos.Filter = buscarDocumentoImputacion;
        }

        private void BtnImprimirRemito_Click(object sender, RoutedEventArgs e)
        {
            Documento documento = dgGralRemitos.SelectedItem as Documento;
            if (documento == null)
            {
                MessageBox.Show("Debe seleccionar un documento para imprimir", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {

                //llamamos a la ventana de imprimir documento
                PrintRemitoVH printRemitoObra = new PrintRemitoVH(documento.IdDocumento);
                if (printRemitoObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //ImprimirRemitoVh imprimir = new ImprimirRemitoVh(documento.IdDocumento);
                //imprimir.Show();
            }

        }

        private void btnBuscarDoc_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if (int.TryParse(txtBuscar.Text, out temp))
            {


                vistaRemitos.Filter = buscarIdDocumento;
            }
            else
            {
                vistaRemitos.Filter = buscarDocumento;
            }
        }
        #endregion


        private void DgGralRemitos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Documento detalle = new Documento();
            detalle = dgGralRemitos.SelectedItem as Documento;
            if (detalle != null)
            {
                ucDetalleFilaDocumento._iddocver = detalle.IdDocumento;
            }
        }








    }
}
