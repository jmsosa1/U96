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
    /// Lógica de interacción para ABMVehiculo.xaml
    /// </summary>
    public partial class ABMVehiculo : MaterialWindow
    {
        #region Declarativas


        // declaracion de objetos que se usaran en el formulario
        //Vehiculo nuevo_vehiculo = new Vehiculo();
        Vehiculo modi_vehiculo = new Vehiculo();
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        List<CategoriaVh> categoriaVhs = new List<CategoriaVh>();
        List<Combustible> combustibles = new List<Combustible>();
        List<MarcaVh> marcaVhs = new List<MarcaVh>();
        List<LineVh> lineVhs = new List<LineVh>();
        decimal _kminicial_actual, _kminicial_nuevo;
        decimal _hsinicial_actual, _hsinicial_nuevo;

        public string _codigoABM;
        int _idvehiculoModi;
        #endregion

        public ABMVehiculo(Vehiculo paramVh)  // pasamos como parametro un objeto vehiculo para el caso de la 
                                              //modificacion de datos
        {
            InitializeComponent();

            categoriaVhs = bLLVehiculos.VehiculosListarCategoria();
            combustibles = bLLVehiculos.VehiculosListarCombustibles();
            marcaVhs = bLLVehiculos.VehiculosListarMarcas();
            cmbCategoria.ItemsSource = categoriaVhs;
            cmbMarca.ItemsSource = marcaVhs;
            cmbTipoCombustible.ItemsSource = combustibles;
            modi_vehiculo = paramVh;
            gridPrincipal.DataContext = modi_vehiculo; // asiganmos al contexto de datos del grid

            if (modi_vehiculo.IdVh != 0)
            {
                //modi_vehiculo = paramVh;

                _idvehiculoModi = modi_vehiculo.IdVh;

                cmbTipoCombustible.SelectedIndex = combustibles.IndexOf(combustibles.FirstOrDefault(x => x.IdCombustible == modi_vehiculo.IdCombustible));

                cmbCategoria.SelectedIndex = categoriaVhs.IndexOf(categoriaVhs.FirstOrDefault(x => x.IdCateVh == modi_vehiculo.IdCate));
                lineVhs = bLLVehiculos.VehiculosListarLineas(modi_vehiculo.IdCate);
                cmbLineaVh.ItemsSource = lineVhs;
                cmbLineaVh.SelectedIndex = lineVhs.IndexOf(lineVhs.FirstOrDefault(z => z.IdLineaVh == modi_vehiculo.IdLinea));

                cmbMarca.SelectedIndex = marcaVhs.IndexOf(marcaVhs.FirstOrDefault(z => z.IdMarca == modi_vehiculo.IdMarca));
                _hsinicial_actual = modi_vehiculo.HsInicial; // horas iniciales actuales
                _kminicial_actual = modi_vehiculo.KmInicial; // km iniciales actuales

            }

        }

        #region Ventana


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion


        #region Botones

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            /////// INICIO ZONA VALIDACION DE DATOS NECESARIOS

            if (txtDominio.Text.Length == 0)
            {
                MessageBox.Show("Debe indicar un dominio", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe elegir una categoria", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (cmbMarca.SelectedItem == null)
            {
                MessageBox.Show("Debe elejir una marca", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (cmbTipoCombustible.SelectedItem == null)
            {
                MessageBox.Show("Debe elegir un combustible", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (cmbLineaVh.SelectedItem == null)
            {
                MessageBox.Show("Debe elegir una linea de vehiculo", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (txtAnioModelo.Text.Length != 0)
            {

                int esnumero = 0;
                if (int.TryParse(txtAnioModelo.Text, out esnumero)) // validamos que sea un numero 
                {


                    int _aniomodelo = Convert.ToInt16(txtAnioModelo.Text);

                    if (_aniomodelo > (DateTime.Today.Year + 1))
                    {
                        MessageBox.Show("El anio del modelo no puede ser mayor al siguiente");
                        return;
                    }
                }
            }

            ///////// FIN ZONA VALIDACION DATOS NECESARIOS ///// Y LA VALIDACION DE WPF???///


            //nuevo_vehiculo = CrearVehiculo(); // seteamos el nuevo objeto vehiculo con los datos ingresados

            if (_codigoABM == "A") // SI ES UNA ALTA 
            {
                bool existe_dominio = bLLVehiculos.ValidarDominio(modi_vehiculo.Dominio); //VERIFICAMOS QUE NO SE REPITA EL DOMINIO
                if (existe_dominio == true) //SI YA EXISTE, AVISAMOS
                {
                    MessageBox.Show("El Dominio del vehiculo ya existe", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else // SI NO EXISTE, GRABAMOS EL NUEVO REGISTRO
                {

                    int resultadoGrabacion = bLLVehiculos.VehiculoAlta(modi_vehiculo);
                }
            }
            else
            {
                if (_codigoABM == "M")// SI ES UNA ACTUALIZACION DE DATOS

                {
                    MessageBoxResult respuesta;
                    bool existe_dominio = bLLVehiculos.ValidarDominio(modi_vehiculo.Dominio);
                    if (existe_dominio == true) //SI YA EXISTE, AVISAMOS
                    {
                        respuesta = MessageBox.Show("El Dominio del vehiculo ya existe.Desea actualizar igual?", "Aviso", MessageBoxButton.YesNo);
                        if (respuesta == MessageBoxResult.Yes)
                        {

                            int resultadoUpdate = bLLVehiculos.VehiculoActualiza(modi_vehiculo);

                            ActualizarKmyHsAcumuladas();
                        }

                    }
                }
            }

            DialogResult = true;
            this.Close();
        }
        #endregion

        #region MetodosPrivados


        private void ActualizarKmyHsAcumuladas()
        {
            //actualizamos los km y horas acumuldas  por si hubo alguna modificacion en los km iniciales y horas iniciales
            _hsinicial_nuevo = Convert.ToDecimal(txtHsInicio.Text); // nuevos valores 
            _kminicial_nuevo = Convert.ToDecimal(txtKmInicio.Text); // nuevos valores 

            if (_hsinicial_actual != _hsinicial_nuevo)
            {
                //si el nuevo valor es distinto (mayo o menor) que el que tenia al momento de modificar el formulario (Valor actual)
                //pasamos la diferencia como parametro
                decimal _param = _hsinicial_nuevo - _hsinicial_actual;
                //esta diferencia sera positiva en caso que el valor nuevo sea mayor que el actual
                // y sera negativa si el valor nuevo sea menor que el valor actual
                bLLVehiculos.VehiculoActualizaHorasAcumuladas(modi_vehiculo.IdVh, _param, 0);
            }
            if (_kminicial_actual != _kminicial_nuevo)
            {
                decimal _param = _kminicial_nuevo - _kminicial_actual;
                bLLVehiculos.VehiculoActualizaKmAcumulados(modi_vehiculo.IdVh, _param, 0);
            }

        }

        /*
        private Vehiculo CrearVehiculo()
        {

            //Vehiculo vehiculo = new Vehiculo();
           
            //CategoriaVh categoriaVh = cmbCategoria.SelectedItem as CategoriaVh;
            //Combustible combustible = cmbTipoCombustible.SelectedItem as Combustible;
            //MarcaVh marcaVh = cmbMarca.SelectedItem as MarcaVh;
            //LineVh lineVh = cmbLineaVh.SelectedItem as LineVh;

            //vehiculo.IdVh = _idvehiculoModi;
            //vehiculo.Descripcion = txtDescripcion.Text;
            //vehiculo.Modelo = txtModelo.Text;
            //vehiculo.Dominio = txtDominio.Text;

            //vehiculo.IdMarca = marcaVh.IdMarca;

            /*if (lineVh.IdLineaVh == 0)
            {
                vehiculo.IdLinea = 0;
            }
            else
            {
                vehiculo.IdLinea = lineVh.IdLineaVh;
            }*/
        //vehiculo.IdCate = categoriaVh.IdCateVh;
        //vehiculo.IdCombustible = combustible.IdCombustible;
        // vehiculo.AltaF = DateTime.Today;

        //vehiculo.NumeroChasis = string.IsNullOrEmpty(txtNumeroChasis.Text)?"no indica":txtNumeroChasis.Text;

        //vehiculo.NumeroMotor = string.IsNullOrEmpty(txtNumeroMotor.Text)?"no indica":txtNumeroMotor.Text;

        //vehiculo.Color = string.IsNullOrEmpty(txtColor.Text)?"no indica":txtColor.Text;

        // vehiculo.AnioModelo =string.IsNullOrEmpty(txtAnioModelo.Text)?"no indica":txtAnioModelo.Text;

        //vehiculo.Cilindrada =string.IsNullOrEmpty(txtCilindrada.Text)?"no indica":txtCilindrada.Text;

        //vehiculo.NeuDelantero = string.IsNullOrEmpty(txtDelantero.Text)?"no indica":txtDelantero.Text;

        //vehiculo.NeuTrasero = string.IsNullOrEmpty(txtTrasero.Text) ? "no indica":txtTrasero.Text;
        //vehiculo.Garantia = string.IsNullOrEmpty(txtGarantia.Text) ? "no indica" : txtGarantia.Text;

        //vehiculo.FechaCompra = dtpFechaCompra.SelectedDate != null ? (DateTime?)dtpFechaCompra.SelectedDate.Value.Date : null;
        //vehiculo.CantiEjes = Convert.ToInt16(txtCantEjes.Text);
        /*if (chkRSat.IsChecked == true)
        {
            vehiculo.RSat = 1;
        }
        else
        {
            vehiculo.RSat = 0;
        }

        if (chkSegSat.IsChecked == true)
        {
            vehiculo.SegSat = 1;
        }
        else
        {
            vehiculo.SegSat = 0;
        }*/

        //zona valores decimales

        //string _valorcompra = txtValorCompra.Text;
        //vehiculo.ValorCompra = decimal.Parse(_valorcompra.Replace("$",""));

        //string _vkm = txtValorKm.Text;
        //vehiculo.ValorKm = decimal.Parse(_vkm.Replace("$",""));

        //string _vhs = txtValorHs.Text;
        //vehiculo.ValorHora= decimal.Parse(_vhs.Replace("$",""));


        //vehiculo.LitroHora = Convert.ToDecimal(txtLitroHora.Text);


        //vehiculo.KmLitro = Convert.ToDecimal(txtLitroKM.Text);
        //vehiculo.KmInicial =Convert.ToDecimal(txtKmInicio. Text);

        //vehiculo.HsInicial =Convert.ToDecimal(txtHsInicio.Text);    

        //vehiculo.Estado = "Activo";
        //vehiculo.ValorLitro =0;
        //vehiculo.CodigoRadio = txtCodigoRadio.Text;
        //vehiculo.KmKitDistribucion = Convert.ToInt32(txtKitDistribucion.Text);
        //vehiculo.Alto = Convert.ToDecimal(txtAlto.Text);
        //vehiculo.Ancho = Convert.ToDecimal(txtAncho.Text);
        //vehiculo.CargaMaxima = Convert.ToDecimal(txtCarga.Text);
        //vehiculo.Largo = Convert.ToDecimal(txtLargo.Text);
        //vehiculo.Peso = Convert.ToDecimal(txtPeso.Text);
        //vehiculo.VolumenCarga = Convert.ToDecimal(txtVolumen.Text);
        //vehiculo.SuperficieCarga = Convert.ToDecimal(txtSuperficie.Text);
        //string _costorepo = txtCostoRepoDls.Text;
        //vehiculo.CostoReposicionDls = decimal.Parse(_costorepo.Replace("$", ""));
        //return vehiculo;


        #endregion

        #region ComboBox
        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaVh categoria = cmbCategoria.SelectedItem as CategoriaVh;
            if (categoria != null)
            {


                lineVhs = bLLVehiculos.VehiculosListarLineas(categoria.IdCateVh);
                cmbLineaVh.ItemsSource = lineVhs;
                modi_vehiculo.IdCate = categoria.IdCateVh;
            }


        }
        private void cmbLineaVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineVh lineVh = cmbLineaVh.SelectedItem as LineVh;
            if (lineVh != null)
            {
                modi_vehiculo.IdLinea = lineVh.IdLineaVh;
            }

        }

        private void cmbTipoCombustible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Combustible combustible = cmbTipoCombustible.SelectedItem as Combustible;
            if (combustible != null)
            {


                modi_vehiculo.IdCombustible = combustible.IdCombustible;
            }

        }


        private void cmbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarcaVh marcaVh = cmbMarca.SelectedItem as MarcaVh;
            if (marcaVh != null)
            {


                modi_vehiculo.IdMarca = marcaVh.IdMarca;
            }
        }


        #endregion
        private void dtpFechaCompra_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region TextBoxes
        private void txtValorCompra_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValorCompra.SelectAll();
        }

        private void txtValorKm_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValorKm.SelectAll();
        }

        private void txtValorHs_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValorHs.SelectAll();
        }

        private void txtCantEjes_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCantEjes.SelectAll();

        }
        private void txtDelantero_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDelantero.SelectAll();
        }

        private void txtTrasero_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTrasero.SelectAll();
        }

        private void txtGarantia_GotFocus(object sender, RoutedEventArgs e)
        {
            txtGarantia.SelectAll();
        }

        private void txtDominio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDominio.SelectAll();
        }

        private void txtDescripcion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtModelo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtModelo.SelectAll();
        }

        private void txtAnioModelo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAnioModelo.SelectAll();
        }

        private void txtNumeroChasis_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumeroChasis.SelectAll();
        }

        private void txtNumeroMotor_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumeroMotor.SelectAll();
        }

        private void txtCilindrada_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCilindrada.SelectAll();
        }

        private void txtColor_GotFocus(object sender, RoutedEventArgs e)
        {
            txtColor.SelectAll();
        }

        private void txtLitroHora_GotFocus(object sender, RoutedEventArgs e)
        {
            txtLitroHora.SelectAll();
        }

        private void txtLitroKM_GotFocus(object sender, RoutedEventArgs e)
        {
            txtLitroKM.SelectAll();
        }

        private void txtKmInicio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtKmInicio.SelectAll();
        }

        private void txtHsInicio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtHsInicio.SelectAll();
        }

        private void txtCodigoRadio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCodigoRadio.SelectAll();
        }

        private void txtLargo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtLargo.SelectAll();
        }

        private void txtLargo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtAncho_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAncho.SelectAll();
        }

        private void txtAncho_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtAlto_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAlto.SelectAll();
        }

        private void txtAlto_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPeso_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPeso.SelectAll();
        }

        private void txtPeso_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCarga_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCarga.SelectAll();
        }

        private void txtCarga_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtVolumen_GotFocus(object sender, RoutedEventArgs e)
        {
            txtVolumen.SelectAll();
        }

        private void txtVolumen_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSuperficie_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSuperficie.SelectAll();
        }

        private void txtSuperficie_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtCostoRepoDls_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCostoRepoDls.SelectAll();
        }

        private void txtKitDistribucion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtKitDistribucion.SelectAll();
        }



        private void txtKmInicio_KeyDown(object sender, KeyEventArgs e)
        {
            txtHsInicio.SelectAll();
        }

        private void txtHsInicio_KeyDown(object sender, KeyEventArgs e)
        {


        }
        #endregion



        private void chkSegSat_Checked(object sender, RoutedEventArgs e)
        {
            modi_vehiculo.SegSat = 1;
        }

        private void chkRSat_Checked(object sender, RoutedEventArgs e)
        {
            modi_vehiculo.RSat = 1;
        }









    }
}
