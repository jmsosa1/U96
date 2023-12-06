using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.ABM;


namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucVehiculoAjustes.xaml
    /// </summary>
    public partial class ucVehiculoAjustes : UserControl
    {
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        BLLBase bLLBase = new BLLBase();
        List<Combustible> ocCombustibles = new List<Combustible>();
        List<CategoriaVh> lstCategoriaVh = new List<CategoriaVh>();
        List<LineVh> lstLineVh = new List<LineVh>();
        List<Provincia> lstProvincia = new List<Provincia>();
        List<Localidad> lstLocalidad = new List<Localidad>();
        List<MarcaVh> marcaVhs = new List<MarcaVh>();
        List<Docu_vh> docu_Vhs = new List<Docu_vh>();
        List<Monedas> lista_monedas = new List<Monedas>();
        List<Monedas> lista_anual = new List<Monedas>();
        ObservableCollection<Coeficiente> lista_cof = new ObservableCollection<Coeficiente>();
        Coeficiente _coeficiente = new Coeficiente();
        public ucVehiculoAjustes()
        {
            InitializeComponent();
            ocCombustibles = bLLVehiculos.VehiculosListarCombustibles();
            dgCombustibles.ItemsSource = ocCombustibles;
            lstCategoriaVh = bLLVehiculos.VehiculosListarCategoria();
            lstProvincia = bLLBase.ListaProvincia();
            dgCategoria.ItemsSource = lstCategoriaVh;
            cmbProvincias.ItemsSource = lstProvincia;
            marcaVhs = bLLVehiculos.VehiculosListarMarcas();
            dgMarcasVh.ItemsSource = marcaVhs;
            dgMarcasVh.DataContext = marcaVhs;
            docu_Vhs = bLLVehiculos.VehiculoListarTipoDocu();
            dgDocuVh.ItemsSource = docu_Vhs;
            dgDocuVh.DataContext = docu_Vhs;
            lista_monedas = bLLBase.ListarVariacionMoneda(DateTime.Today.Year);
            txbAnioMoneda.Text = DateTime.Today.Year.ToString();
            //ArmarListaAnualMonedas();


            dgValorMoneda.ItemsSource = lista_anual;
            dgValorMoneda.DataContext = lista_anual;
            lista_cof = bLLBase.ListarCoeficientes();
            dgCof.ItemsSource = lista_cof;
            dgCof.DataContext = lista_cof;

        }

        private void ArmarListaAnualMonedas()
        {
            if (lista_monedas != null)
            {
                foreach (var item in lista_monedas)
                {


                    if (item.AnioValor == 2021)
                    {
                        lista_anual.Add(item);
                    }
                }

                int _mes = lista_anual[0].MesValor;
                decimal _valor_moneda_actual = lista_anual[0].Valor;

                foreach (var item in lista_anual)
                {
                    if (_mes == item.MesValor)
                    {
                        item.Variacion = 0;

                    }
                    else
                    {
                        item.Variacion = item.Valor - _valor_moneda_actual;
                        _valor_moneda_actual = item.Valor;
                        _mes = item.MesValor;
                    }
                }

            }


        }

        private void dgLocalidades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        #region[combustibles]
        private void btnAddComb_Click(object sender, RoutedEventArgs e)
        {
            Combustible c = new Combustible();
            ABMCombustible _x = new ABMCombustible(c);
            //_x.txbOperacion.Text = "Agregar un registro - Combustibles";
            _x.btnAccion.Content = "Guardar";
            _x.operacion = "A";
            _x.txtPrecioAnterior.IsEnabled = false; // no hayprecio anterior en un alta
            if (_x.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                ocCombustibles = bLLVehiculos.VehiculosListarCombustibles();
                dgCombustibles.ItemsSource = ocCombustibles;
            }


        }

        private void btnMComb_Click(object sender, RoutedEventArgs e)
        {


            Combustible c = new Combustible();
            c = dgCombustibles.SelectedItem as Combustible;
            if (c != null)
            {
                ABMCombustible _x = new ABMCombustible(c);
                // _x.txtNombre.Text = c.Descripcion;
                _x.operacion = "M";
                // _x.txtPrecio.Text = Convert.ToString(c.PrecioLitroActual);
                //_x.txbOperacion.Text = "Modificar un registro - Combustibles";
                _x.btnAccion.Content = "Guardar";
                // _x.txtPrecioAnterior.Text = Convert.ToString(c.PrecioLitroAnterior);

                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se  modifico el registro", "Aviso", MessageBoxButton.OK);
                    ocCombustibles = bLLVehiculos.VehiculosListarCombustibles();
                    dgCombustibles.ItemsSource = ocCombustibles;
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnDelComb_Click(object sender, RoutedEventArgs e)
        {
            bool existecmbenvh = false;
            Combustible c = new Combustible();
            c = dgCombustibles.SelectedItem as Combustible;
            if (c != null)
            {
                existecmbenvh = bLLVehiculos.CombustibleExisteEnVehiculo(c.IdCombustible);
                if (existecmbenvh == true)
                {
                    MessageBox.Show("El registro no se puede borrar porque existen restricciones", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {


                    ABMCombustible _x = new ABMCombustible(c);

                    _x.operacion = "D";

                    //_x.txbOperacion.Text = "Borrarun registro -Combustibles";
                    _x.btnAccion.Content = "Borrar";

                    if (_x.ShowDialog() == true)
                    {
                        MessageBox.Show("Se  elimino  el registro", "Aviso", MessageBoxButton.OK);
                        ocCombustibles = bLLVehiculos.VehiculosListarCombustibles();
                        dgCombustibles.ItemsSource = ocCombustibles;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }
        #endregion

        #region[categoria vh]

        private void dgCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaVh vh = new CategoriaVh();
            vh = dgCategoria.SelectedItem as CategoriaVh;
            if (vh != null)
            {
                lstLineVh = bLLVehiculos.VehiculosListarLineas(vh.IdCateVh);
                dgLineas.ItemsSource = lstLineVh;
            }
        }

        private void btnAddCate_Click(object sender, RoutedEventArgs e)
        {
            CategoriaVh c = new CategoriaVh();
            ABMCategoria _x = new ABMCategoria(c);

            _x.txtIdCategoria.IsEnabled = false;
            _x.operacion = "A";

            _x.txtNombreCate.Text = "Nombre Categoria";
            //_x.txbOperacion.Text = "Agregar un registro -Categorias";

            _x.btnAccion.Content = "Guardar";
            if (_x.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                lstCategoriaVh = bLLVehiculos.VehiculosListarCategoria();
                dgCategoria.ItemsSource = lstCategoriaVh;
                dgCategoria.DataContext = lstCategoriaVh;
            }


        }

        private void btnMCate_Click(object sender, RoutedEventArgs e)
        {
            CategoriaVh c = new CategoriaVh();
            c = dgCategoria.SelectedItem as CategoriaVh;
            if (c != null)
            {
                ABMCategoria _x = new ABMCategoria(c);

                _x.operacion = "M";

                //_x.txbOperacion.Text = "Modificar un registro -Categorias";

                _x.btnAccion.Content = "Modificar";
                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el registro", "Aviso", MessageBoxButton.OK);
                    lstCategoriaVh = bLLVehiculos.VehiculosListarCategoria();
                    dgCategoria.ItemsSource = lstCategoriaVh;
                    dgCategoria.DataContext = lstCategoriaVh;
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }
        }

        private void btnDelCate_Click(object sender, RoutedEventArgs e)
        {
            bool existecatevh = false;
            CategoriaVh c = new CategoriaVh();
            c = dgCategoria.SelectedItem as CategoriaVh;
            if (c != null)
            {
                existecatevh = bLLVehiculos.CategoriaExisteEnVehiculo(c.IdCateVh);
                if (existecatevh == true)
                {
                    MessageBox.Show("El registro no se puede borrar porque existen restricciones", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    ABMCategoria _x = new ABMCategoria(c);
                    _x.operacion = "D";
                   // _x.txbOperacion.Text = "Borrar un registro -Categorias";

                    _x.btnAccion.Content = "Borrar";
                    if (_x.ShowDialog() == true)
                    {
                        MessageBox.Show("Se elimino el registro", "Aviso", MessageBoxButton.OK);
                        lstCategoriaVh = bLLVehiculos.VehiculosListarCategoria();
                        dgCategoria.ItemsSource = lstCategoriaVh;
                        dgCategoria.ItemsSource = lstCategoriaVh;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }

        }
        #endregion

        #region[linea vh]
        private void btnAddLinea_Click(object sender, RoutedEventArgs e)
        {
            CategoriaVh c = new CategoriaVh();
            c = dgCategoria.SelectedItem as CategoriaVh;
            LineVh l = new LineVh();
            if (c != null)
            {
                l.IdCateVh = c.IdCateVh;
                ABMLinea _x = new ABMLinea(l);

                _x.txtIdLinea.IsEnabled = false;

               // _x.txbOperacion.Text = "Agregar un registro - Linea";

                _x.btnAccion.Content = "Guardar";

                _x.operacion = "A";

                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                    lstLineVh = bLLVehiculos.VehiculosListarLineas(c.IdCateVh);
                    dgLineas.ItemsSource = lstLineVh;

                }
            }
        }

        private void btnMLinea_Click(object sender, RoutedEventArgs e)
        {
            LineVh c = new LineVh();
            c = dgLineas.SelectedItem as LineVh;
            if (c != null)
            {
                ABMLinea _x = new ABMLinea(c);

                _x.txtIdLinea.IsEnabled = false;
                _x.txtNombre.Text = c.NomLineaVh;
                _x.txtIdCategoria.IsEnabled = false;
                //_x.txbOperacion.Text = "Modificar un registro - Linea";


                _x.operacion = "M";
                _x.btnAccion.Content = "Modificar";
                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el registro", "Aviso", MessageBoxButton.OK);
                    lstLineVh = bLLVehiculos.VehiculosListarLineas(c.IdCateVh);
                    dgLineas.ItemsSource = lstLineVh;
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                    return;
                }
            }

        }

        private void btnDelLinea_Click(object sender, RoutedEventArgs e)
        {
            bool existelineavh = false;
            LineVh lineVh = new LineVh();
            lineVh = dgLineas.SelectedItem as LineVh;
            if (lineVh != null)
            {
                existelineavh = bLLVehiculos.LineaExisteEnVehiculo(lineVh.IdLineaVh);
                if (existelineavh == true)
                {
                    MessageBox.Show("El registro no se puede borrar porque existen restricciones", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    ABMLinea _x = new ABMLinea(lineVh);
                    //_x.txbOperacion.Text = "Borrar un registro -Categorias";

                    _x.btnAccion.Content = "Borrar";
                    if (_x.ShowDialog() == true)
                    {
                        MessageBox.Show("Se elimino el registro", "Aviso", MessageBoxButton.OK);

                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }
        #endregion

        #region[localidad]
        private void dgProvincias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provincia provincia = new Provincia();
            //provincia = dgProvincias.SelectedItem as Provincia;
            if (provincia != null)
            {
                lstLocalidad = bLLBase.ListaLocalidad(provincia.IdProvincia);
                dgLocalidades.ItemsSource = lstLocalidad;
            }
        }

        private void btnAddLocalidad_Click(object sender, RoutedEventArgs e)
        {
            Provincia p = new Provincia();
            p = cmbProvincias.SelectedItem as Provincia;
            Localidad l = new Localidad();
            l.IdProvincia = p.IdProvincia;
            l.Provincia = p.Nombre;


            // p = dgProvincias.SelectedItem as Provincia;
            if (p != null)
            {
                ABMLocalidad _x = new ABMLocalidad(l);

                //_x.txbOperacion.Text = "Agregar un registro - Localidad";


                _x.btnAccion.Content = "Guardar";
                _x.operacion = "A";
                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                    lstLocalidad = bLLBase.ListaLocalidad(l.IdProvincia);
                    dgLocalidades.ItemsSource = lstLocalidad;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una provincia primero", "Aviso", MessageBoxButton.OK);
                return;
            }

        }

        private void btnMLocalidad_Click(object sender, RoutedEventArgs e)
        {
            Provincia p = new Provincia();
            p = cmbProvincias.SelectedItem as Provincia;
            if (p != null)
            {
                Localidad l = new Localidad();
                l = dgLocalidades.SelectedItem as Localidad;
                if (l != null)
                {
                    ABMLocalidad _x = new ABMLocalidad(l);

                    //_x.txbOperacion.Text = "Modificar un registro - Localidad";

                    _x.btnAccion.Content = "Modificar";
                    _x.operacion = "M";
                    if (_x.ShowDialog() == true)
                    {
                        MessageBox.Show("Se Modifico el registro", "Aviso", MessageBoxButton.OK);
                        lstLocalidad = bLLBase.ListaLocalidad(l.IdProvincia);
                        dgLocalidades.ItemsSource = lstLocalidad;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una localidad", "Aviso", MessageBoxButton.OK);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar una provincia primero", "Aviso", MessageBoxButton.OK);
                return;
            }


        }

        private void cmbProvincias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provincia provincia = new Provincia();
            provincia = cmbProvincias.SelectedItem as Provincia;
            if (provincia != null)
            {
                lstLocalidad = bLLBase.ListaLocalidad(provincia.IdProvincia);
                dgLocalidades.ItemsSource = lstLocalidad;
            }
        }

        #endregion

        #region[marcas vh]
        private void BtnDelMarcaVh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMMarcaVh_Click(object sender, RoutedEventArgs e)
        {
            MarcaVh mvh = dgMarcasVh.SelectedItem as MarcaVh;
            if (mvh != null)
            {
                ABMMarcasVh _x = new ABMMarcasVh(mvh);
                _x.txbOperacion.Text = "Modificar un registro - Marca Vehiculo";
                _x.btnAccion.Content = "Guardar";
                _x.operacion = "M";
                _x.txtIdMarcaVh.IsEnabled = false;
                if (_x.ShowDialog() == true)
                {
                    MessageBox.Show("Se Modifico el registro", "Aviso", MessageBoxButton.OK);
                    marcaVhs = bLLVehiculos.VehiculosListarMarcas();
                    dgMarcasVh.ItemsSource = marcaVhs;
                    dgMarcasVh.DataContext = marcaVhs;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void BtnAddMarcaVh_Click(object sender, RoutedEventArgs e)
        {
            MarcaVh mvh = new MarcaVh();
            ABMMarcasVh _x = new ABMMarcasVh(mvh);
            _x.txbOperacion.Text = "Agregar un registro - Marca Vehiculo";
            _x.btnAccion.Content = "Guardar";
            _x.operacion = "A";
            _x.txtIdMarcaVh.IsEnabled = false;
            if (_x.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK);
                marcaVhs = bLLVehiculos.VehiculosListarMarcas();
                dgMarcasVh.ItemsSource = marcaVhs;
                dgMarcasVh.DataContext = marcaVhs;
            }

        }

        #endregion

        private void btnAddDocu_Click(object sender, RoutedEventArgs e)
        {
            Docu_vh docu = new Docu_vh();
            ABMTipoDocuVH aBM = new ABMTipoDocuVH(docu);
            aBM._tipoOp = "A";
            if (aBM.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el nuevo tipo de  documentacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                docu_Vhs = bLLVehiculos.VehiculoListarTipoDocu();
                dgDocuVh.ItemsSource = docu_Vhs;
                dgDocuVh.DataContext = docu_Vhs;
            }
        }

        private void btnEditDocu_Click(object sender, RoutedEventArgs e)
        {
            Docu_vh docu = dgDocuVh.SelectedItem as Docu_vh;
            if (docu == null)
            {
                MessageBox.Show("Debe seleccionar un item de la lista ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                ABMTipoDocuVH aBM = new ABMTipoDocuVH(docu);
                aBM._tipoOp = "M";
                if (aBM.ShowDialog() == true)
                {
                    MessageBox.Show("Se actualizo el nombre de la documentacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    docu_Vhs = bLLVehiculos.VehiculoListarTipoDocu();
                    dgDocuVh.ItemsSource = docu_Vhs;
                    dgDocuVh.DataContext = docu_Vhs;
                }
            }
        }

        private void btnDelDocu_Click(object sender, RoutedEventArgs e)
        {

        }

        #region MonedaExtrangera
        private void btnAddValor_Click(object sender, RoutedEventArgs e)
        {
            Monedas monedas = new Monedas();
            ABMMoneda aBMMoneda = new ABMMoneda(monedas, "A");
            if (aBMMoneda.ShowDialog() == true)
            {

            }
        }

        private void btnMValor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorrarValor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgValorMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCof_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _coeficiente = dgCof.SelectedItem as Coeficiente;
        }

        private void btnAddRegistro_Click(object sender, RoutedEventArgs e)
        {
            Coeficiente nuevoCof = new Coeficiente();
            nuevoCof.NomCof = "Nombre";
            nuevoCof.ValorMax = 0;
            nuevoCof.ValorMed = 0;
            nuevoCof.ValorMin = 0;
            ABMCoeficiente _abm = new ABMCoeficiente(nuevoCof);
            if (_abm.ShowDialog()==true)
            {
                MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                lista_cof = bLLBase.ListarCoeficientes();
                dgCof.ItemsSource = lista_cof;
                dgCof.DataContext = lista_cof;
            }

        }

        private void btnDelRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (_coeficiente != null)
            {
                MessageBoxResult result = MessageBox.Show("Desea Eliminar el registro seleccionado?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bLLBase.CoeficienteDelete(_coeficiente.IdCof);
                    lista_cof = bLLBase.ListarCoeficientes();
                    dgCof.ItemsSource = lista_cof;
                    dgCof.DataContext = lista_cof;
                }
                
            }
        }

      

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_coeficiente != null)
            {


                ABMCoeficiente _abm = new ABMCoeficiente(_coeficiente);
                if (_abm.ShowDialog() == true)
                {
                    MessageBox.Show("Se agrego el registro", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_cof = bLLBase.ListarCoeficientes();
                    dgCof.ItemsSource = lista_cof;
                    dgCof.DataContext = lista_cof;
                }
            }
        }
    }
}
