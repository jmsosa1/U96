using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.imprimir;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para ucDSIDDI.xaml
    /// </summary>
    public partial class ucDSIDDI : UserControl
    {
        #region Declarativa
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Documento> lista_dsi_ddi = new ObservableCollection<Documento>();
        public ICollectionView vistaRemitos
        {
            get { return CollectionViewSource.GetDefaultView(lista_dsi_ddi); }
        }

        #endregion
        public ucDSIDDI()
        {
            InitializeComponent();
            lista_dsi_ddi = coreRemito.ListarDocIndumentaria();
            dgDsiDdi.ItemsSource = lista_dsi_ddi;
            dgDsiDdi.DataContext = lista_dsi_ddi;
            CalcularRegistros();


        }

        private void CalcularRegistros()
        {
            int _ent = 0;
            int _dev = 0;
            txtRegistros.Text = lista_dsi_ddi.Count.ToString();
            foreach (var item in lista_dsi_ddi)
            {
                if (item.IdTipoRem == 8)
                {
                    _ent++;
                }
                if (item.IdTipoRem == 9)
                {
                    _dev++;
                }
            }

            txtTotalEntregas.Text = _ent.ToString();
            txtTotalDevoluciones.Text = _dev.ToString();
        }


        #region Filtros
        private bool filtroEmpleado(object obj)
        {

            Documento p = obj as Documento;
            //TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;

            return p.NombreEmpleado.Contains(txtEmpleado.Text);
        }

        private bool filtroObra(object obj)
        {
            Documento p = obj as Documento;
            return p.Imputacion == Convert.ToInt32(txtImputacion.Text);
        }
        #endregion
        private void btnReImprimir_Click(object sender, RoutedEventArgs e)
        {
            Documento documento = dgDsiDdi.SelectedItem as Documento;
            if (documento != null)
            {
                PrintIndumentaria printRemito = new PrintIndumentaria(documento.IdDocumento);
                if (printRemito.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //ImprimirDSI imprimir = new ImprimirDSI(documento.IdDocumento);
                //imprimir.Show();

            }
        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgDsiDdi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Documento d = dgDsiDdi.SelectedItem as Documento;
            if (d != null)
            {
                ucDetalleDSODDO._iddocumento = d.IdDocumento;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpleado.Text))
            {
                lista_dsi_ddi = coreRemito.ListarDocIndumentaria();
                dgDsiDdi.ItemsSource = lista_dsi_ddi;
                dgDsiDdi.DataContext = lista_dsi_ddi;
            }
            else
            {
                vistaRemitos.Filter = filtroEmpleado;
            }

        }

        private void btnDSI_Click(object sender, RoutedEventArgs e)
        {
            DSI nuevoremito = new DSI();
            nuevoremito._operacion = "Entrega";
            nuevoremito.txtConcepto.Text = "Entrega";
            nuevoremito.txtTipoDocu.Text = "DSI";
            if (nuevoremito.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnDDI_Click(object sender, RoutedEventArgs e)
        {
            DDI nuevoremito = new DDI();
            nuevoremito._operacion = "Devolucion";
            nuevoremito.txtConcepto.Text = "Devolucion";
            nuevoremito.txtTipoDocu.Text = "DDI";
            if (nuevoremito.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnBuscarObra_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                MessageBox.Show("Escriba una imputacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                vistaRemitos.Filter = filtroObra;
            }





        }
    }
}
