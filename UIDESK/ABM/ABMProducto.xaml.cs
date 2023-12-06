using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMProducto.xaml
    /// </summary>
    public partial class ABMProducto : MaterialWindow
    {
        #region Declaraciones


        BLLProducto bllProducto = new BLLProducto();
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>();
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>();
        ObservableCollection<SegmentoP> segmentoPs = new ObservableCollection<SegmentoP>();
        ObservableCollection<MarcaProductos> marcaProductos = new ObservableCollection<MarcaProductos>();
        ObservableCollection<KitProducto> kitProductos = new ObservableCollection<KitProducto>();
        Producto producto = new Producto();
        public static string _operacion;
        int _idproductoParametro = 0;

        #endregion  

        //constructor
        public ABMProducto(int _idproducto)
        {

            InitializeComponent();
            tipoProductos = bllProducto.ListarTipos();

            categoriaPs = bllProducto.ListarCategorias();

            segmentoPs = bllProducto.ListarSegmentos();

            _idproductoParametro = _idproducto;
            cmbKitProducto.ItemsSource = kitProductos;
         

            if (_idproductoParametro != 0) // si se trata de una modificacion de datos
            {
                //seteo del objeto en caso de una modificacion
                producto = bllProducto.BuscarDatosUno(_idproducto);
                txtIdTipo.Text = producto.IdTipoP.ToString();
                txtIdCategoria.Text = producto.IdCateP.ToString();
                txtIdSegmento.Text = producto.IdSegP.ToString();
                txtTipo.Text = producto.Tipo;
                txtCategoria.Text = producto.Categoria;
                txtSegmento.Text = producto.Segmento;
                //seteo tipos , categorias y segmentos  de producto
                //usamos para eso linq 
                // https://stackoverflow.com/questions/42491509/set-combobox-selectedindex

                //cmbTipo.SelectedIndex= producto.IdTipoP - 1 ;

                //cmbCategoria.SelectedIndex = categoriaPs.IndexOf(categoriaPs.FirstOrDefault(x => x.IdCateP == producto.IdCateP)); 

                // cmbLinea.SelectedIndex = segmentoPs.IndexOf(segmentoPs.FirstOrDefault(x=>x.IdSegmentoP==  producto.IdSegP)) ;
                //seteo marca de productos

                marcaProductos = bllProducto.ListarMarcas();
                cmbMarca.ItemsSource = marcaProductos;
                cmbMarca.SelectedIndex = marcaProductos.IndexOf(marcaProductos.FirstOrDefault(x => x.IdMarca == producto.IdMarcaP));

                //seteo de codigo de operacion
                _operacion = "M";
            }
            else
            {
                // si es una alta de producto


                txtIdTipo.Text = "0";
                txtIdCategoria.Text = "0";
                txtIdSegmento.Text = "0";
                marcaProductos = bllProducto.ListarMarcas();
                cmbMarca.ItemsSource = marcaProductos;
                _operacion = "A";
                //cmbTipos.ItemsSource = tipoProductos;
            }

            DataContext = producto;


        }



        #region EventosDeVentana
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        #region EventosBotones
        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // validamos el producto
            bool productoValido = ValidarProducto();
            if (productoValido)
            {
                //si los datos ingresados son validos, armamos el producto
                producto = ArmarProducto();

                if (_operacion == "A")
                {


                    //grabamos en la base de datos
                    bllProducto.ProductoAtlta(producto);
                    //buscamos el ultimo producto
                    Producto ultimo = bllProducto.ObtenerUltimoId();
                    //damos de alta en stock del deposito desde donde se da de alta
                    bllProducto.AltaEnStock(ultimo.IdProducto, 0, Contexto.CodigoDeposito);
                    DialogResult = true;
                   
                }
                else
                {
                    if (_operacion == "M")
                    {
                        bllProducto.ProductoModificar(producto);
                        DialogResult = true;
                        
                    }

                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region MetodosPrivados


        private Producto ArmarProducto()
        {
            Producto p = new Producto();
            string _costorepo = txtCostoReposicion.Text;
            string _preciounit = txtPrecioUnitario.Text;

            MarcaProductos mp = cmbMarca.SelectedItem as MarcaProductos;
            KitProducto kit = cmbKitProducto.SelectedItem as KitProducto;
            p.Nombre = txtNombre.Text;
            p.Accesorios = txtAccesorios.Text;
            p.EstadoItem = 1;
            p.AltaF = DateTime.Today;
            p.CodInventario = txtCodInventario.Text;
            p.Color = txtColor.Text;
            p.CondicionStk = 1;
            p.ControlSf = 1;
            p.CostoReposicion = decimal.Parse(_costorepo.Replace("$", ""));
            p.Descripcion = txtDescripcion.Text;
            p.Dimensiones = txtDimensiones.Text;
            p.EsKit = 1;
            p.IdCateP = Convert.ToInt32(txtIdCategoria.Text);
            if (mp != null)
            {
                p.IdMarcaP = mp.IdMarca;
            }
            else
            {
                p.IdMarcaP = 1;
            }

            p.IdSegP = Convert.ToInt32(txtIdSegmento.Text);

            p.IdTipoP = Convert.ToInt32(txtIdTipo.Text);
            p.Modelo = txtModelo.Text;
            if (kit != null)
            {
                p.NumeroKit = kit.IdKit;
            }
            else
            {
                p.NumeroKit = 0;
            }

            p.NumSerie = txtNumSerie.Text;
            p.PrecioUnitario = decimal.Parse(_preciounit.Replace("$", ""));
            p.IdProducto = _idproductoParametro;
            if (chkControlSf.IsChecked == true)
            {
                p.Idsf = 1;
            }
            else
            {
                p.Idsf = 7;
            }

            producto.Patron = 2;
            producto.Constrate = 3;
            producto.Escala = "no indica";
            producto.RangoMedicion = "no indica";
            producto.Exactitud = "no indica";
            return p;
        }

        private bool ValidarProducto()
        {
            //validamos los datos que necesitamos del producto
            int temp_tipo = 0;
            int temp_cate = 0;
            int temp_seg = 0;
            int.TryParse(txtIdTipo.Text, out temp_tipo);
            int.TryParse(txtIdCategoria.Text, out temp_cate);
            int.TryParse(txtIdSegmento.Text, out temp_seg);


            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es requerido", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtCodInventario.Text))
                {
                    MessageBox.Show("EL campo Codigo inventario es requerido", "Aviso", MessageBoxButton.OK);
                    return false;
                }
                else
                {
                    if (temp_tipo == 0)
                    {
                        MessageBox.Show("Debe indicar un tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return false;
                    }
                    else
                    {
                        if (temp_cate == 0)
                        {
                            MessageBox.Show("Debe indicar un numero de categoria de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return false;
                        }
                        else
                        {
                            if (temp_seg == 0)
                            {
                                MessageBox.Show("Debe indicar numero segemento de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }

                    }
                }
            }




        }



        #endregion


        #region EventosTextBox


        private void TxtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void TxtCodInventario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCodInventario.SelectAll();
        }

        private void TxtDescripcion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void TxtModelo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtModelo.SelectAll();
        }

        private void TxtNumSerie_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumSerie.SelectAll();
        }

        private void TxtPrecioUnitario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPrecioUnitario.SelectAll();
        }

        private void TxtCostoReposicion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCostoReposicion.SelectAll();
        }


        private void TxtDimensiones_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDimensiones.SelectAll();
        }

        private void TxtAccesorios_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAccesorios.SelectAll();
        }

        private void txtIdTipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int _id = Convert.ToInt16(txtIdTipo.Text);
                var tipo = tipoProductos.FirstOrDefault(x => x.IdTipoP == _id);
                if (tipo == null)
                {
                    MessageBox.Show("El tipo de producto no existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    txtTipo.Text = tipo.NomTipo;
                }
            }
        }

        private void txtIdCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int _idtipo = Convert.ToInt16(txtIdTipo.Text);
                int _id = Convert.ToInt16(txtIdCategoria.Text);
                categoriaPs = bllProducto.ListarCategoriasTipo(_idtipo);
                var categoria = categoriaPs.FirstOrDefault(x => x.IdCateP == _id);
                if (categoria == null)
                {
                    MessageBox.Show("La categoria del producto no existe para ese tipo de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txtCategoria.Text = categoria.NomCateP;
                }
            }

        }

        private void txtIdSegmento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int _idcate = Convert.ToInt16(txtIdCategoria.Text);
                int _id = Convert.ToInt16(txtIdSegmento.Text);
                segmentoPs = bllProducto.ListarSegmentoCategoria(_idcate);
                var segmento = segmentoPs.FirstOrDefault(x => x.IdSegmentoP == _id);
                if (segmento == null)
                {
                    MessageBox.Show("El Segmento del producto  no existe para esa categoria", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    txtSegmento.Text = segmento.NombreSeg;
                }
            }
        }

        private void txtIdTipo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtIdTipo.SelectAll();
        }

        private void txtIdCategoria_GotFocus(object sender, RoutedEventArgs e)
        {
            txtIdCategoria.SelectAll();
        }

        private void txtIdSegmento_GotFocus(object sender, RoutedEventArgs e)
        {
            txtIdSegmento.SelectAll();
        }


        #endregion

        #region EventosComboBox
        /*
        private void CmbTipo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TipoProducto t = cmbTipo.SelectedItem as TipoProducto;
            if (t != null)
            {


                categoriaPs = bllProducto.ListarCategoriasTipo(t.IdTipoP);
                cmbCategoria.ItemsSource = categoriaPs;
               
                cmbLinea.ItemsSource = null;
            }
        }

        private void CmbCategoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CategoriaP ct = cmbCategoria.SelectedItem as CategoriaP;
            if (ct != null)
            {


                segmentoPs = bllProducto.ListarSegmentoCategoria(ct.IdCateP);
                cmbLinea.ItemsSource = segmentoPs;
               
            }
        }
    */
        #endregion

        #region checkBox


        private void chkEsKit_Checked(object sender, RoutedEventArgs e)
        {
            txtNumKit.IsEnabled = true;
            cmbKitProducto.IsEnabled = true;
        }

        private void chkEsKit_Unchecked(object sender, RoutedEventArgs e)
        {
            txtNumKit.IsEnabled = false;
            cmbKitProducto.IsEnabled = false;

        }


        private void chkControlSf_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkControlSf_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel=false ;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MaterialWindow_Closed(object sender, EventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.Yes)
            //{
            //    this.Close();
            //}
        }

        private void MaterialWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        #endregion

        //private void cmbTipos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    TipoProducto t = cmbTipos.SelectedItem as TipoProducto;
        //    if (t != null)
        //    {
        //        categoriaPs = bllProducto.ListarCategoriasTipo(t.IdTipoP);
        //        cmbCategoria.ItemsSource = categoriaPs;
        //        cmbSegmento.ItemsSource = null;
        //    }
        //}

        //private void cmbCategoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    CategoriaP ct = cmbCategoria.SelectedItem as CategoriaP;
        //    if (ct != null)
        //    {
        //        segmentoPs = bllProducto.ListarSegmentoCategoria(ct.IdCateP);
        //        cmbSegmento.ItemsSource = segmentoPs;
        //    }
        //}
    }
}
