using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucObrasGeneral.xaml
    /// </summary>
    public partial class ucObrasGeneral : UserControl
    {

        #region DeclaracionVariables  

        List<Obra> lista_obras = new List<Obra>();
        BLLObras bllobras = new BLLObras();
        BLLBase bllBase = new BLLBase();

        List<CategoriaObra> cateobras = new List<CategoriaObra>();
        List<Provincia> lista_provincias = new List<Provincia>();
        List<Localidad> lista_localidad = new List<Localidad>();
        Provincia _provincia = new Provincia();
        Localidad localidad = new Localidad();
        CategoriaObra _catObra = new CategoriaObra();

        bool _existe_obra = false;

        public ICollectionView vistaObras
        {
            get { return CollectionViewSource.GetDefaultView(lista_obras); }
        }
        #endregion

        public ucObrasGeneral()
        {
            InitializeComponent();
            lista_obras = bllobras.ObrasActivas();
            dgGeneralObras.ItemsSource = lista_obras;
            dgGeneralObras.DataContext = lista_obras;
            cateobras = bllobras.ObrasCategoria();

            //cmbCategoriaObras.ItemsSource = cateobras;
            //cmbCategoriaObras.DataContext = cateobras;
            lista_provincias = bllBase.ListaProvincia();
            //cmbProvincias.ItemsSource = lista_provincias;
            //cmbProvincias.DataContext = lista_provincias;
            txtRegistros.Text = lista_obras.Count.ToString();

        }


        #region Filtros

        private bool filtroImputacion(object obj)
        {
            Obra ob = obj as Obra;
            int _imputacion;
            bool _conv = int.TryParse(txtBuscar.Text,out _imputacion); //tryparse devuelve un true si se pudo convertir el valor y lo almacena en _imputacion

            if (_conv)
            {
                return ob.Imputacion == _imputacion;
            }
            else
            {
                return ob.Cliente.Contains(txtBuscar.Text); 
            }
        }

        private bool filtroProvincia(object obj)
        {
            Obra ob = obj as Obra;

            return ob.IdProvincia == _provincia.IdProvincia;
        }

        private bool filtroCategoria(object obj)
        {
            Obra ob = obj as Obra;

            return ob.IdCateObra == _catObra.IdCateObra;
        }


        private bool filtroGeneral(object obj)
        {
            Obra obra = obj as Obra;
            return obra.Imputacion > 0;
        }
        #endregion

        #region EventosControles




        private void BtnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            Obra _obraSelGrid = dgGeneralObras.SelectedItem as Obra;
            if (_obraSelGrid == null)
            {
                MessageBox.Show("Debe Seleccionar una obra antes de modificarla", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                ABMObra aBMObra = new ABMObra(_obraSelGrid);

                if (aBMObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se modificaron los datos de la obra", "Aviso", MessageBoxButton.OK);
                    lista_obras = bllobras.ObrasActivas();
                    dgGeneralObras.ItemsSource = lista_obras;
                    dgGeneralObras.DataContext = lista_obras;
                }
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Obra _obra = new Obra();
            ABMObra _abmObra = new ABMObra(_obra);
            if (_abmObra.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego una nueva obra al sistema", "Aviso", MessageBoxButton.OK);
                lista_obras = bllobras.ObrasActivas();
                dgGeneralObras.ItemsSource = lista_obras;
                dgGeneralObras.DataContext = lista_obras;
            }

        }



        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            vistaObras.Filter = filtroImputacion;
        }


        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            //si se desea dar de baja , la obra cambia su estado a inactiva
            //_tipoOperacionObra = 3;
            Obra obra = dgGeneralObras.SelectedItem as Obra;
            MessageBoxResult r = MessageBox.Show("Desea dar de baja la obra :?" + obra.Imputacion + " " + obra.NombreObra + " " + obra.Cliente + "",
                    "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                //baja de la obra
                bllobras.BajaDeUnaObra(obra.Imputacion);
                lista_obras = bllobras.ObrasActivas();
                dgGeneralObras.ItemsSource = lista_obras;
                dgGeneralObras.DataContext = lista_obras;
                MessageBox.Show("Se dio de baja la obra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        //private void chkFiltroCategoria_Checked(object sender, RoutedEventArgs e)
        //{
        //    cmbCategoriaObras.IsEnabled = true;
        //}

        //private void chkFiltroCategoria_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    cmbCategoriaObras.IsEnabled = false;
        //    vistaObras.Filter = filtroGeneral;
        //}


        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgGeneralObras.SelectedIndex = -1;
        }

        private void DgVhGeneralObras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Obra _obra = dgGeneralObras.SelectedItem as Obra;
            if (_obra != null)
            {
                ucDetalleObra._imputacion = _obra.Imputacion;
            }
        }

        //private void CmbCategoriaObras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _catObra = cmbCategoriaObras.SelectedItem as CategoriaObra;
        //    if (_catObra != null)
        //    {
        //        vistaObras.Filter = filtroCategoria;
        //    }
        //}

        #endregion

        #region metodosPrivados




        #endregion



        private void btnAsignarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Obra obra = dgGeneralObras.SelectedItem as Obra;
            if (obra != null)
            {
                AsignarEmpleadoObra asignar = new AsignarEmpleadoObra(obra.Imputacion);
                if (asignar.ShowDialog() == true)
                {
                    MessageBox.Show("Se asigno correctamente el empleado a la obra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void btnCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (vistaObras != null && vistaObras.CanGroup == true)
            {
                vistaObras.GroupDescriptions.Clear();
                vistaObras.GroupDescriptions.Add(new PropertyGroupDescription("CateObra"));


            }
        }

        private void btnProvincia_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void cmbProvincias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _provincia = cmbProvincias.SelectedItem as Provincia;

        //    if (_provincia != null)
        //    {
        //        vistaObras.Filter = filtroProvincia;
        //    }
        //}

        //private void chkFiltroProvincia_Checked(object sender, RoutedEventArgs e)
        //{
        //    cmbProvincias.IsEnabled = true;
        //}

        //private void chkFiltroProvincia_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    cmbProvincias.IsEnabled = false;
        //    vistaObras.Filter = filtroGeneral;
        //}
    }
}
