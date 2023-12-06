using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.Remitos;
using UIDESK.uc.Empleados;

namespace UIDESK.uc.Empleados
{
    /// <summary>
    /// Lógica de interacción para ucGeneralEmpleados.xaml
    /// </summary>
    public partial class ucGeneralEmpleados : UserControl
    {
        //Empleado empleado = new Empleado();

        ObservableCollection<Empleado> colEmpleados = new ObservableCollection<Empleado>();
        List<CategoriaEmpleado> categoriaEmpleados = new List<CategoriaEmpleado>();
        BLLEmpleados bLL = new BLLEmpleados();
        CategoriaEmpleado _catEmpleado = new CategoriaEmpleado();
        public ICollectionView vistaEmpleados
        {
            get { return CollectionViewSource.GetDefaultView(colEmpleados); }
        }

        public ucGeneralEmpleados()
        {
            InitializeComponent();
            colEmpleados = bLL.EmpleadosActivos();
            dgEmpleados.ItemsSource = colEmpleados;
            dgEmpleados.DataContext = colEmpleados;
            categoriaEmpleados = bLL.ListaCateEmpleado();
            cmbCategorias.ItemsSource = categoriaEmpleados;
            //GenerarListaEmpleados();
            vistaEmpleados.Filter = new Predicate<object>(buscarEmpleado);
            txtRegistros.Text = colEmpleados.Count.ToString();

        }


        #region Filtros

        private bool filtroPorCategoria(object obj)
        {
            Empleado p = obj as Empleado;
            return p.IdCatEmpleado == _catEmpleado.IdCategoria;
        }

        private bool buscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtBuscar.Text);
        }

        private bool listadoGeneral(object obj)
        {
            Empleado p = obj as Empleado;
            return p.IdEmpleado > 0;
        }
        #endregion



        private void btnDetalleFila_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Empleado empleado = dgEmpleados.SelectedItem as Empleado;
            if (empleado != null)
            {
                //DetalleEmpleado detalleEmpleado = new DetalleEmpleado(empleado.IdEmpleado);
                //detalleEmpleado.Show();
                ucDetalleFilaEmpleado detalleFilaEmpleado = new ucDetalleFilaEmpleado(empleado.IdEmpleado);
                ccDrawerDatosEmpleado.Content = detalleFilaEmpleado;
                //ucDetEmpleadoTransitioner ucDet = new ucDetEmpleadoTransitioner(empleado.IdEmpleado);
                //ccDrawerDatosEmpleado.Content = ucDet;
            }
        }

        private void cmbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CategoriaEmpleado catemp = new CategoriaEmpleado();
            //catemp = cmbCategorias.SelectedItem as CategoriaEmpleado;
            //colEmpleados = bLL.EmpleadosPorCategoria(catemp.IdCategoria);
            //dgEmpleados.ItemsSource = colEmpleados;
            _catEmpleado = cmbCategorias.SelectedItem as CategoriaEmpleado;
            vistaEmpleados.Filter = filtroPorCategoria;
            int _countVista = 0;
            foreach (var item in vistaEmpleados)
            {
                _countVista++;
            }
            txtTotalCategoria.Text = _countVista.ToString();
        }

        private void chkFiltroCategoria_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbCategorias.IsEnabled = false;
            vistaEmpleados.Filter = listadoGeneral;
            txtTotalCategoria.Text = "";
        }

        private void chkFiltroCategoria_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbCategorias.IsEnabled = true;
        }

        private void DgEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BtnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                colEmpleados = bLL.EmpleadosActivos();
                dgEmpleados.ItemsSource = colEmpleados;
                dgEmpleados.DataContext = colEmpleados;
            }
            else
            {
                vistaEmpleados.Filter = buscarEmpleado;
            }

        }

        private void BtnAgregar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            ABMEmpleado nuevoempleado = new ABMEmpleado(empleado);
            //nuevoempleado.txbOperacion.Text = "Alta de  Empleado";
            nuevoempleado.btnOperacion.Content = "Guardar";
            nuevoempleado._operacion = "A";
            if (nuevoempleado.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el empleado", "Aviso", MessageBoxButton.OK);
                colEmpleados = bLL.EmpleadosActivos();
                dgEmpleados.ItemsSource = colEmpleados;
            }

        }

        private void BtnModicarDatos_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Empleado emp = dgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {


                ABMEmpleado nuevoempleado = new ABMEmpleado(emp);
                //nuevoempleado.txbOperacion.Text = "Actualizar datos Empleado";
                nuevoempleado.btnOperacion.Content = "Actualizar";
                nuevoempleado._operacion = "M";
                if (nuevoempleado.ShowDialog() == true)
                {
                    MessageBox.Show("Se Actualizaron los datos del empleado", "Aviso", MessageBoxButton.OK);
                    colEmpleados = bLL.EmpleadosActivos();
                    dgEmpleados.ItemsSource = colEmpleados;
                }
            }
        }

        private void BtnBaja_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnAddValeConsumo_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = dgEmpleados.SelectedItem as Empleado;
            VCE vale = new VCE(empleado, Contexto.CodUser, Contexto.Nomuser);
            if (vale.ShowDialog() == true)
            {
                MessageBox.Show("Se confecciono el vale de consumo al empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnAddValeDescuento_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = dgEmpleados.SelectedItem as Empleado;
            VDP vale = new VDP(empleado, Contexto.CodUser, Contexto.Nomuser);
            if (vale.ShowDialog() == true)
            {
                MessageBox.Show("Se confecciono el vale de descuento al empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
    }
}
