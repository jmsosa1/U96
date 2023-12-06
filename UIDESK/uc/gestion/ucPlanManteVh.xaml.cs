using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucPlanManteVh.xaml
    /// </summary>
    public partial class ucPlanManteVh : UserControl
    {
        // variables
        BLLGestion gestion = new BLLGestion();
        public string _tituloClase = "Mantenimiento de Vehiculos";
        ObservableCollection<Otm> lista_otm_vh = new ObservableCollection<Otm>();
        public ICollectionView vistaOtmvh
        {
            get { return CollectionViewSource.GetDefaultView(lista_otm_vh); }
        }


        //constructor
        public ucPlanManteVh()
        {
            InitializeComponent();
            lista_otm_vh = gestion.OTM_Todas_VH();


            vistaOtmvh.Filter = filtro_Otm;
            dgGralOtmVH.DataContext = vistaOtmvh;
            dgGralOtmVH.ItemsSource = vistaOtmvh;
            CalcularResultados();



        }

        private bool filtro_otm_Vehiculo(object obj)
        {
            Otm otm = obj as Otm;

            return otm.Dominio == txtBuscar.Text;
            //resultado de la consulta todos los registros del vehiculo

        }


        private bool filtro_Otm(object obj)
        {
            Otm otmfilter = obj as Otm;
            if (cmbEstadoTarea.SelectedItem != null)
            {
                string _estado = ((ComboBoxItem)cmbEstadoTarea.SelectedItem).Content.ToString();
                return otmfilter.Estado_Otm == _estado;
            }
            else
            {
                return otmfilter.Estado_Otm == "Pendiente";
            }

        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgGralOtmVH.SelectedIndex = -1;
            /*  lista_otm_vh = gestion.OTM_Todas_VH();
              vistaOtmvh.Filter = filtro_Otm;
              vistaOtmvh.Refresh();
              dgGralOtmVH.DataContext = vistaOtmvh;
              dgGralOtmVH.ItemsSource = vistaOtmvh;*/

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Otm _otm = new Otm();

            vistaOtmvh.Filter = filtro_otm_Vehiculo;

            CalcularResultados();
        }



        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {/*
            Otm otm = new Otm();
            OtmDetalle otmDetalle = new OtmDetalle();
            ABMOtm aBMOtm = new ABMOtm(otm,otmDetalle);
            
            if (aBMOtm.ShowDialog()==true)
            {
                MessageBox.Show("Se dio de alta la OTM de vehiculo", "Aviso", MessageBoxButton.OK);
                lista_otm_vh = gestion.OTM_Todas_VH();
                vistaOtmvh.Filter = filtro_Otm;
                vistaOtmvh.Refresh();
                dgGralOtmVH.DataContext = vistaOtmvh;
                dgGralOtmVH.ItemsSource = vistaOtmvh;

            }*/
        }

        private void DgGralOtmVH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Otm otm = dgGralOtmVH.SelectedItem as Otm;
            if (otm != null)
            {
                ucDetallePlanManteVh._idotm = otm.IdOtm;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBuscarProve.IsEnabled = false;
            vistaOtmvh.Filter = null;
            vistaOtmvh.Filter = filtro_Otm;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtBuscarProve.IsEnabled = true;

        }

        private void TxtBuscarProve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // aplicamos el filtro de la coleccion por proveedor

            }
        }


        private void BtnBaja_Click(object sender, RoutedEventArgs e)
        {
            Otm otmEliminar = dgGralOtmVH.SelectedItem as Otm;

            MessageBoxResult resultado = MessageBox.Show("Desea Cancelar la OTM?", "Aviso", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                //llamado a procedimiento que anula la OTM
                int fila = gestion.OTMBaja(otmEliminar.IdOtm);
                MessageBox.Show("Se Cancelo con exito la OTM", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void CmbEstadoTarea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaOtmvh.Filter = filtro_Otm;
            CalcularResultados();
        }



        private void CalcularResultados()
        {
            Otm _otm = new Otm();
            int _cp = 0;
            int _cc = 0;
            int _ca = 0;
            int _ct = 0;


            foreach (var item in vistaOtmvh)
            {
                _otm = item as Otm;
                if (_otm.Estado_Otm == "Pendiente")
                {
                    ++_cp;
                }
                if (_otm.Estado_Otm == "Cumplido")
                {
                    ++_cc;
                }
                if (_otm.Estado_Otm == "Cancelado")
                {
                    ++_ca;
                }

                ++_ct;
            }


            txtPendientes.Text = _cp.ToString();
            txtAnulados.Text = _ca.ToString();
            txtCumplidos.Text = _cc.ToString();
            txtRegistros.Text = _ct.ToString();
        }
    }

}
