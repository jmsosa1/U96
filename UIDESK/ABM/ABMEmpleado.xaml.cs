using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMEmpleado.xaml
    /// </summary>
    public partial class ABMEmpleado : MaterialWindow 
    {
        BLLEmpleados bllempleados = new BLLEmpleados();
        BLLBase bllBase = new BLLBase();
        Empleado _empleado = new Empleado();
        List<CategoriaEmpleado> categorias = new List<CategoriaEmpleado>();
        List<SectorEmpleado> sectorEmpleados = new List<SectorEmpleado>();
        List<Provincia> provincias = new List<Provincia>();
        List<Localidad> localidades = new List<Localidad>();
        public string _operacion = "";

        public ABMEmpleado(Empleado em)
        {
            InitializeComponent();

            _empleado = em;
            categorias = bllempleados.ListaCateEmpleado();
            sectorEmpleados = bllempleados.ListarSectorEmpleado();
            cmbCategoria.ItemsSource = categorias;
            cmbSector.ItemsSource = sectorEmpleados;
            provincias = bllBase.ListaProvincia();
            cmbProvincia.ItemsSource = provincias;
            if (_empleado != null)
            {
                cmbCategoria.SelectedIndex = categorias.IndexOf(categorias.FirstOrDefault(x => x.IdCategoria == _empleado.IdCatEmpleado));
                cmbSector.SelectedIndex = sectorEmpleados.IndexOf(sectorEmpleados.FirstOrDefault(x => x.IdSector == _empleado.IdSector));
                cmbProvincia.SelectedIndex = provincias.IndexOf(provincias.FirstOrDefault(x => x.IdProvincia == _empleado.IdProvincia));
                cmbLocalidad.SelectedIndex = localidades.IndexOf(localidades.FirstOrDefault(x => x.IdLocalidad == _empleado.IdLocalidad));
            }

            DataContext = _empleado;
        }


        #region Botones
      

        private void BtnOperacion_Click(object sender, RoutedEventArgs e)
        {

            //chekeamos que tenga los datos necesarios
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de empleado", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtDNI.Text))
            {
                MessageBox.Show("Debe ingresar un DNI ", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoria", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbSector.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un sector", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbProvincia.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una provincia", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbLocalidad.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una localidad", "Aviso", MessageBoxButton.OK);
                return;
            }
            Empleado empleado = ArmarEmpleado();

            if (_operacion == "A")
            {
                MessageBoxResult result = MessageBox.Show("Dar de alta el nuevo empleado?", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //grabamos el registro
                    bllempleados.CrearEmpleado(empleado);
                    DialogResult = true;
                }
                else
                {
                    DialogResult = false;
                }
            }
            else
            {
                if (_operacion == "M")
                {
                    _empleado.IdSector = empleado.IdSector;
                    _empleado.IdLocalidad = empleado.IdLocalidad;
                    _empleado.IdProvincia = empleado.IdProvincia;
                    MessageBoxResult result = MessageBox.Show("Actualizar datos del empleado?", "Aviso", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        //grabamos el registro
                        bllempleados.ActualizarEmpleado(_empleado);
                        DialogResult = true;
                    }
                    else
                    {
                        DialogResult = false;
                    }
                }

            }


        }

        #endregion

        #region Privados
        private Empleado ArmarEmpleado()
        {
            Empleado emp = new Empleado();
            CategoriaEmpleado ct = new CategoriaEmpleado();
            ct = cmbCategoria.SelectedItem as CategoriaEmpleado;
            SectorEmpleado s = new SectorEmpleado();
            s = cmbSector.SelectedItem as SectorEmpleado;
            Provincia p = cmbProvincia.SelectedItem as Provincia;
            Localidad l = cmbLocalidad.SelectedItem as Localidad;
            emp.AltaF = DateTime.Today.Date;
            emp.BajaF = null;
            emp.Direccion = txtDireccion.Text;
            emp.Dni = Convert.ToInt32(txtDNI.Text);
            emp.IdCatEmpleado = ct.IdCategoria;
            //emp.IdSector = s.IdSector;
            emp.Nombre = txtNombre.Text;
            emp.TeCelular = txtCelular.Text;
            emp.TeParticular = txtTeParticular.Text;
            emp.Gremio = txtGremio.Text;
            emp.IdProvincia = p.IdProvincia;
            emp.IdLocalidad = l.IdLocalidad;
            emp.TCalzado = txtCalzado.Text;
            emp.TCamisa = txtCamisa.Text;
            emp.TPantalon = txtPantalon.Text;
            emp.IdSector = s.IdSector;
            return emp;
        }
        #endregion


        #region TextBoxes
        private void TxtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombre.SelectAll();

        }

        private void TxtDNI_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDNI.SelectAll();
        }

        private void TxtDireccion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDireccion.SelectAll();
        }

        private void TxtTeParticular_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTeParticular.SelectAll();
        }

        private void TxtCelular_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCelular.SelectAll();
        }
        private void TxtDNI_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        #endregion


        #region Ventana
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion




        #region Combobox

        private void CmbProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provincia p = cmbProvincia.SelectedItem as Provincia;

            localidades = bllBase.ListaLocalidad(p.IdProvincia);
            cmbLocalidad.ItemsSource = localidades;
        }

        #endregion

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
