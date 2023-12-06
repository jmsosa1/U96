using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMObra.xaml
    /// </summary>
    public partial class ABMObra : MaterialWindow 
    {
        BLLObras bllobras = new BLLObras();
        BLLBase bllBase = new BLLBase();
        Obra nuevaobra = new Obra();
        Obra obraDatosModificar = new Obra();
        List<CategoriaObra> cateobras = new List<CategoriaObra>();
        List<Provincia> lista_provincias = new List<Provincia>();
        List<Localidad> lista_localidad = new List<Localidad>();
        Provincia provincia = new Provincia();
        Localidad localidad = new Localidad();
        bool _existe_obra = false;
        int _operacion = 1; // codigo de la operacion que se realiza 1= alta, 2= modificacion , 3= baja

        public ABMObra(Obra _obra)
        {
            InitializeComponent();
            cateobras = bllobras.ObrasCategoria();
            cmbCategoria.ItemsSource = cateobras;
            lista_provincias = bllBase.ListaProvincia();
            cmbProvincia.ItemsSource = lista_provincias;
            if (_obra.Imputacion > 0)
            {
                CargarDatosObra(_obra);
                obraDatosModificar = _obra;
                _operacion = 2;
            }
        }

        private void CargarDatosObra(Obra obra)
        {
            txtImputacion.Text = obra.Imputacion.ToString();
            txtImputacion.IsEnabled = false;
            txtCliente.Text = obra.Cliente;
            txtCuit.Text = obra.Cuit;
            txtDireccion.Text = obra.DireccionObra;
            txtNombre.Text = obra.NombreObra;
            int _indexProvincia = obra.IdProvincia;
            int _indexLocal = obra.IdLocalidad;
            int _indexCategoria = obra.IdCateObra;
            cmbProvincia.SelectedIndex = (_indexProvincia - 1);
            cmbLocalidad.SelectedIndex = (_indexLocal - 1);
            cmbCategoria.SelectedIndex = (_indexCategoria - 1);
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            DialogResult = false;
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                MessageBox.Show("Debe ingresar un numero de imputacion", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (_existe_obra)
            {
                MessageBox.Show("La obra ya existe ", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de obra", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Debe ingresar una direccion ", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("Debe ingresar un CUIT", "Aviso", MessageBoxButton.OK);
                return;
            }

            provincia = cmbProvincia.SelectedItem as Provincia;
            if (provincia == null)
            {
                MessageBox.Show("Debe Seleccionar una Provincia", "Aviso", MessageBoxButton.OK);
                return;
            }

            localidad = cmbLocalidad.SelectedItem as Localidad;
            if (localidad == null)
            {
                MessageBox.Show("Debe Seleccionar una Localidad", "Aviso", MessageBoxButton.OK);
                return;
            }
            //si esta toda la inforaciom correcta, grabamos los datos


            //determinamos que tipo de operacion se debe realizar
            if (_operacion == 1) // alta
            {
                nuevaobra = ArmarObra();
                bllobras.GrabarObra(nuevaobra);
                DialogResult = true;
            }
            else
            {
                if (_operacion == 2) // modificacion
                {
                    obraDatosModificar.Cliente = txtCliente.Text;
                    obraDatosModificar.Cuit = txtCuit.Text;
                    obraDatosModificar.DireccionObra = txtDireccion.Text;
                    obraDatosModificar.NombreObra = txtNombre.Text;
                    obraDatosModificar.IdProvincia = provincia.IdProvincia;
                    obraDatosModificar.IdLocalidad = localidad.IdLocalidad;
                    CategoriaObra co = new CategoriaObra();
                    co = cmbCategoria.SelectedItem as CategoriaObra;
                    obraDatosModificar.IdCateObra = co.IdCateObra;
                    bllobras.ActualizarObra(obraDatosModificar);
                    DialogResult = true;
                }
            }


        }

        private Obra ArmarObra()
        {
            CategoriaObra co = new CategoriaObra();
            co = cmbCategoria.SelectedItem as CategoriaObra;
            Obra obra = new Obra();
            obra.Imputacion = Convert.ToInt32(txtImputacion.Text);
            obra.NombreObra = txtNombre.Text;
            obra.Cliente = txtCliente.Text;
            obra.AltaF = DateTime.Today.Date;
            obra.Cuit = txtCuit.Text;
            obra.DireccionObra = txtDireccion.Text;
            obra.Estado = "Activa";
            obra.IdCateObra = co.IdCateObra;
            obra.IdLocalidad = localidad.IdLocalidad;
            obra.IdProvincia = provincia.IdProvincia;
            obra.Localidad = localidad.Nombre;
            obra.Provincia = provincia.Nombre;
            return obra;
        }


        private void CmbProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provincia provincia = cmbProvincia.SelectedItem as Provincia;
            lista_localidad = bllBase.ListaLocalidad(provincia.IdProvincia);
            cmbLocalidad.ItemsSource = lista_localidad;
        }

        private void TxtImputacion_LostFocus(object sender, RoutedEventArgs e)
        {
            int _imputacion_nueva = Convert.ToInt32(txtImputacion.Text);
            _existe_obra = bllobras.ValidarNumeroImputacion(_imputacion_nueva);
            if (_existe_obra)
            {
                MessageBox.Show("Esa obra ya existe", "Aviso", MessageBoxButton.OK);
                return;
            }

        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
