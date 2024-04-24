using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCategoria.xaml
    /// </summary>
    public partial class ABMCategoria : MaterialWindow
    {
        BLLVehiculos bLL = new BLLVehiculos();
        public string operacion = "";
        List<TipoVh> tipoVhs = new List<TipoVh>();

        public ABMCategoria(CategoriaVh categoria)
        {
            InitializeComponent();
            grdPpal.DataContext = categoria;
            tipoVhs = bLL.ListarTiposVh();
            cmbTipoVh.ItemsSource = tipoVhs;

            if (categoria != null)
            {
                cmbTipoVh.SelectedIndex = tipoVhs.IndexOf(tipoVhs.FirstOrDefault(x => x.IdTipoVh == categoria.IdTipoVh));
            }


        }



        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {

            CategoriaVh categoriaVh = new CategoriaVh();
            TipoVh tipo = new TipoVh();
            if (operacion == "A")
            {
                if (string.IsNullOrEmpty(txtNombreCate.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre de categoria", "Aviso", MessageBoxButton.OK);
                    return;
                }

                if (cmbTipoVh.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un tipo de vehiculo", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    tipo = cmbTipoVh.SelectedItem as TipoVh;
                }

                string _cdp = txtCDP.Text;
                string _cdu = txtCDU.Text;
                string _cuc = txtCostoUnidad.Text;
                categoriaVh.NomCate = txtNombreCate.Text;
                categoriaVh.IdTipoVh = tipo.IdTipoVh;
                categoriaVh.CostoDiarioParo = decimal.Parse(_cdp.Replace("$", ""));
                categoriaVh.CostoDiarioUso = decimal.Parse(_cdu.Replace("$", ""));
                categoriaVh.CostoUnidadCategoria = decimal.Parse(_cuc.Replace("$", ""));
                categoriaVh.UnidadCate = txtUnidadCate.Text;

                bLL.VehiculoAgregarCategoria(categoriaVh);
            }
            else
            {
                if (operacion == "M")
                {
                    if (string.IsNullOrEmpty(txtNombreCate.Text))
                    {
                        MessageBox.Show("Debe ingresar un nombre de categoria", "Aviso", MessageBoxButton.OK);
                        return;
                    }

                    if (cmbTipoVh.SelectedItem == null)
                    {
                        MessageBox.Show("Debe seleccionar un tipo de vehiculo", "Aviso", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        tipo = cmbTipoVh.SelectedItem as TipoVh;
                    }

                    string _cdp = txtCDP.Text;
                    string _cdu = txtCDU.Text;
                    string _cuc = txtCostoUnidad.Text;
                    categoriaVh.NomCate = txtNombreCate.Text;
                    categoriaVh.IdCateVh = Convert.ToInt16(txtIdCategoria.Text);
                    categoriaVh.IdTipoVh = tipo.IdTipoVh;
                    categoriaVh.CostoDiarioParo = decimal.Parse(_cdp.Replace("$", ""));
                    categoriaVh.CostoDiarioUso = decimal.Parse(_cdu.Replace("$", ""));
                    categoriaVh.CostoUnidadCategoria = decimal.Parse(_cuc.Replace("$", ""));
                    categoriaVh.UnidadCate = txtUnidadCate.Text;
                    bLL.VehiculoModificarCategoria(categoriaVh);
                }
                else
                {
                    categoriaVh.IdCateVh = Convert.ToInt16(txtIdCategoria.Text);
                    bLL.VehiculoBorraCategoria(categoriaVh.IdCateVh);
                }
            }

            DialogResult = true;
           


        }



        private void txtNombreCate_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombreCate.SelectAll();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void txtCDP_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCDP.SelectAll();
        }

        private void txtCDU_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCDU.SelectAll();
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
