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
    /// Lógica de interacción para ABMProveedor.xaml
    /// </summary>
    public partial class ABMProveedor : MaterialWindow 
    {
        Proveedor altaproveedor = new Proveedor();
        BLLProveedor bll = new BLLProveedor();
        BLLBase bLLBase = new BLLBase();
        List<Localidad> localidades = new List<Localidad>();
        List<Provincia> provincias = new List<Provincia>();
        List<RubroProve> rubroProves = new List<RubroProve>();

        int idproveedor = 0;
        public ABMProveedor(Proveedor proveedor)
        {
            InitializeComponent();
            provincias = bLLBase.ListaProvincia();
            rubroProves = bll.ListarRubros();
            cmbRubroProve.ItemsSource = rubroProves;
            cmbProvincia.ItemsSource = provincias;

            if (proveedor != null)
            {
                btnGuardar.Content = "Actualizar";
                grdPrincipal.DataContext = proveedor;
                cmbRubroProve.SelectedIndex = proveedor.IdRubro - 1;
                cmbProvincia.SelectedIndex = proveedor.IdProvincia - 1;
                cmbLocalidad.SelectedIndex = proveedor.IdLocalidad - 1;
                idproveedor = proveedor.IdProve;
            }
            else
            {
                // si se trata de una alta 
                cmbRubroProve.ItemsSource = rubroProves;
                cmbProvincia.ItemsSource = provincias;
                grdPrincipal.DataContext = altaproveedor;
            }
        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso");
            this.Close();
        }

        private void btnIgualRazon_Click(object sender, RoutedEventArgs e)
        {
            txtRazonSocial.Text = txtNombre.Text;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //validar datos
            if (string.IsNullOrEmpty(txtNombre.Text) && string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                MessageBox.Show("Debe ingresar un nombre o una razon Social", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbRubroProve.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rubro", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("Debe indicar un numero de CUIT", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                decimal cuit = Convert.ToDecimal(txtCuit.Text);
                bool cuitok = bll.ValidarProveedor(cuit);
                if (!cuitok)
                {
                    MessageBox.Show("El cuit ingresado ya existe en otro proveedor. Por favor ingrese uno correcto", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }
            if (cmbProvincia.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una provincia", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbLocalidad.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una localidad", "Aviso", MessageBoxButton.OK);
                return;
            }

            CrearProveedor();
            if (btnGuardar.Content.ToString() == "Actualizar")
            {
                altaproveedor.IdProve = idproveedor;
                bll.ActualizarProveedor(altaproveedor);
                MessageBox.Show("Se modifica el proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                if (btnGuardar.Content.ToString() == "Guardar")
                {
                    bll.GrabarProveedor(altaproveedor);
                    MessageBox.Show("Se agrego un el proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }



        }

        private void cmbProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Provincia provseleccionada = new Provincia();
            provseleccionada = cmbProvincia.SelectedItem as Provincia;
            if (provseleccionada != null)
            {


                localidades = bLLBase.ListaLocalidad(provseleccionada.IdProvincia);
                cmbLocalidad.ItemsSource = localidades;
            }
        }

        private void CrearProveedor()
        {
            RubroProve r = cmbRubroProve.SelectedItem as RubroProve;
            Provincia p = cmbProvincia.SelectedItem as Provincia;
            Localidad l = cmbLocalidad.SelectedItem as Localidad;
            altaproveedor.Nombre = string.IsNullOrEmpty(txtNombre.Text) ? "no indica" : txtNombre.Text;
            altaproveedor.RazonSocial = string.IsNullOrEmpty(txtRazonSocial.Text) ? "no indica" : txtRazonSocial.Text;
            altaproveedor.Altaf = DateTime.Today.Date;
            altaproveedor.Dir1 = string.IsNullOrEmpty(txtDir1.Text) ? "no indica" : txtDir1.Text;
            altaproveedor.Dir2 = string.IsNullOrEmpty(txtDir2.Text) ? "no indica" : txtDir2.Text;
            altaproveedor.Tel1 = string.IsNullOrEmpty(txtTel1.Text) ? "no indica" : txtTel1.Text;
            altaproveedor.Tel2 = string.IsNullOrEmpty(txtTel2.Text) ? "no indica" : txtTel2.Text;
            altaproveedor.Contacto = string.IsNullOrEmpty(txtContacto.Text) ? "no indica" : txtContacto.Text;
            altaproveedor.Email = string.IsNullOrEmpty(txtEmail.Text) ? "no indica" : txtEmail.Text;
            altaproveedor.Web = string.IsNullOrEmpty(txtSitioWEb.Text) ? "no indica" : txtSitioWEb.Text;
            altaproveedor.Cuit = Convert.ToDecimal(txtCuit.Text);
            altaproveedor.CuitTexto = txtCuit.Text;
            altaproveedor.IdRubro = r.IdRubro;
            altaproveedor.IdProvincia = p.IdProvincia;
            altaproveedor.IdLocalidad = l.IdLocalidad;
            altaproveedor.Calidad = 0;
            altaproveedor.Calificacion = 0;
            altaproveedor.Plazo = 0;
            altaproveedor.Precio = 0;
            altaproveedor.Atencion = 0;
        }



        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
