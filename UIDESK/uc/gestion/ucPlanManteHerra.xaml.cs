using BLL;
using ENTIDADES;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.imprimir;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucPlanManteHerra.xaml
    /// </summary>
    public partial class ucPlanManteHerra : UserControl
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<RMAProducto> lista_rma = new ObservableCollection<RMAProducto>();
        public string _tituloClase = "Mantenimiento de Herramientas";
        public ICollectionView vistaRMA
        {
            get { return CollectionViewSource.GetDefaultView(lista_rma); }
        }
        #endregion

        public ucPlanManteHerra()
        {
            InitializeComponent();
            lista_rma = coreProducto.RMAListarTodos();
            dgVhGeneral.ItemsSource = lista_rma;
            dgVhGeneral.DataContext = lista_rma;
        }

        #region Filtros

        private bool filtroRMANombres(object obj)
        {
            RMAProducto rma = obj as RMAProducto;
            string _texto = txtBuscar.Text;

            return rma.NombreProducto.Contains(_texto) || rma.CodInventario.Contains(_texto) || rma.Marca.Contains(_texto) ||
             rma.Serie.Contains(_texto);



        }


        private bool filtroRMANumeros(object obj)
        {
            RMAProducto rma = obj as RMAProducto;

            int _numRMa = Convert.ToInt32(txtBuscar.Text);

            return rma.IdRma == _numRMa || rma.IdProducto == _numRMa;
        }

        private bool filtroPendientes(object obj)
        {
            RMAProducto rma = obj as RMAProducto;
            return rma.idestadoRma == 1;
        }

        #endregion

        #region Ventana

        #endregion

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgVhGeneral.SelectedIndex = -1;
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            RMAProducto rMA = dgVhGeneral.SelectedItem as RMAProducto;
            if (rMA != null)
            {
                MessageBoxResult re = MessageBox.Show("Desea dar de baja el RMA?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (re == MessageBoxResult.Yes)
                {
                    //borramos el rma
                    coreProducto.BajaRMAProducto(rMA.IdRma);
                    MessageBox.Show("Se dio de baja el RMA", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_rma = coreProducto.RMAListarTodos();
                    dgVhGeneral.ItemsSource = lista_rma;
                    dgVhGeneral.DataContext = lista_rma;
                }
            }
        }





        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if (int.TryParse(txtBuscar.Text, out temp)) // si lo ingresado es un numero
            {
                vistaRMA.Filter = filtroRMANumeros;
                txtRegistros.Text = dgVhGeneral.Items.Count.ToString();
            }
            else
            {
                vistaRMA.Filter = filtroRMANombres;
                txtRegistros.Text = dgVhGeneral.Items.Count.ToString();
            }

        }

        private void dgVhGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {

            RMAProducto rMA = dgVhGeneral.SelectedItem as RMAProducto;
            if (rMA != null)
            {
                MessageBoxResult re = MessageBox.Show("Desea Imprimir el documento?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (re == MessageBoxResult.Yes)
                {
                    PrintRMA printRMA = new PrintRMA(rMA);
                    if (printRMA.ShowDialog() == true)
                    {
                        MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //ImprimirRMA imprimir = new ImprimirRMA(rMA.IdRma);
                    //imprimir.Show();
                }
            }

        }

        private void btnCumplirRMA_Click(object sender, RoutedEventArgs e)
        {
            RMAProducto r = dgVhGeneral.SelectedItem as RMAProducto;
            if (r != null)
            {
                CumplirRMA cumplirRMA = new CumplirRMA(r);
                if (cumplirRMA.ShowDialog() == true)
                {
                    MessageBox.Show("Se cumplio el RMA", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_rma = coreProducto.RMAListarTodos();
                    dgVhGeneral.ItemsSource = lista_rma;
                    dgVhGeneral.DataContext = lista_rma;
                }
            }

        }

        private void btnVerPendientes_Click(object sender, RoutedEventArgs e)
        {
            vistaRMA.Filter = filtroPendientes;
            txtRegistros.Text = dgVhGeneral.Items.Count.ToString();
        }
    }
}
