using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para VCD.xaml
    /// </summary>
    public partial class VCD : MaterialWindow
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto(); // acciones para productos
        BLLRemito coreRemito = new BLLRemito(); // acciones para remitos / documentos
        BLLBase coreBase = new BLLBase();
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de productos
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos

        ObservableCollection<StockProducto> productos = new ObservableCollection<StockProducto>(); // lista de stock de productos

        Documento documento = new Documento(); //objeto documento usado en toda la clase

        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalles del documento



        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones
        StockProducto p_selec;// objeto que contiene al producto seleccionado del selector
        private int _iddeposito; //contiene el id del deposito que esta ejecutando el remito
        //private int _numeracion_remito;
        public string _operacion = ""; // indica el tipo de operacion que se esta haciendo con el remito
        int _ultimoIddocumento = 0;

        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }

        #endregion
        public VCD()
        {
            InitializeComponent();
            //llenamos las colecciones


            tipoProductos = coreProducto.ListarTipos();
            //ajustamos propiedades de los controles de datos
            cmbTipoProducto.ItemsSource = tipoProductos;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;

            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;

            vistaProductos.Filter = new Predicate<object>(filtroBuscarProducto);
            vistaProductos.Filter = new Predicate<object>(filtroTipoProducto);
            vistaProductos.Filter = new Predicate<object>(filtroCateProducto);
            vistaProductos.Filter = new Predicate<object>(productosPorDeposito);

            txtFechaVale.Text = DateTime.Today.ToShortDateString();
            txtTipoDocu.Text = "VCD";
            txtIdUsuario.Text = Contexto.CodUser.ToString();
            txtNombreUsuario.Text = Contexto.Nomuser;
            txtIdDepOrigen.Focus();

        }

        #region Filtros
        private bool productosPorDeposito(object obj)
        {
            int _iddepo = Convert.ToInt32(txtIdDepOrigen.Text);
            StockProducto producto = obj as StockProducto;

            return producto.IdDeposito == _iddepo; // aca usamos el id del deposito  debe ser desde el deposito elejido
        }

        private bool filtroCateProducto(object obj)
        {
            StockProducto p = obj as StockProducto;
            if (cmbCategoriaProducto.SelectedItem != null)
            {
                CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
                string _cateselec = ctp.NomCateP;
                return p.Categoria == _cateselec;
            }
            else
            {
                return p.Categoria == null;
            }
        }

        private bool filtroTipoProducto(object obj)
        {
            StockProducto p = obj as StockProducto;
            if (cmbTipoProducto.SelectedItem != null)
            {
                TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
                string _tiposelec = t.NomTipo;
                return p.TipoProducto == _tiposelec;
            }
            else
            {
                return p.TipoProducto == null;
            }

        }

        private bool filtroBuscarProducto(object obj)
        {
            StockProducto p = obj as StockProducto;
            return p.NombreProducto.Contains(txtBuscar.Text) || p.ModeloProducto.Contains(txtBuscar.Text) || p.Marca.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        }



        #endregion

        #region Botones

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                DialogResult = false;
                this.Close();
            }
        }


        private void btnSeleccion_Click(object sender, RoutedEventArgs e)
        {
            // cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del deposito desde donde
            //se hace la entrega de las herramienta          
            vistaProductos.Filter = productosPorDeposito;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //en este lugar debemos hacer algunas cosas
            //1)//validamos primero que esten todos los datos
            bool _valido = ValidarDatosObligatarios();

            if (_valido)
            {
                //2 validar detalle del remito
                bool _valido_detalle = ValidarDetalleRemito();
                //2) grabamos el encabezado si estan los datos correctos ingresados
                if (_valido_detalle == false)
                {
                    MessageBox.Show("No hay items del detalle o existen cantidades igual a cero. Debe corregir esto.", "Aviso",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {


                    GrabarEncabezadoEnBD();
                    //3) grabar el detalle
                    GrabarDetalleEnBD();
                    //4)actualizamos el stock de los productos en el deposito correspondiente 
                    ActualizarStockProductos();

                    //7 )imprimir el remito
                    MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_ultimoIddocumento);
                        // imprimir.Show();

                    }
                    this.Close();
                }


            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                DialogResult = false;
                this.Close();
            }
        }

        private void BtnAddDel_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
            indiceItemABorrar = dgDetalleDocu.SelectedIndex; //tomamos su indice en la lista detalle
            documentoDetalles.RemoveAt(indiceItemABorrar); // lo removemos de la misma
            //como es una observable collection el cambio se refleja de manera inmediata
            if (documentoDetalles.Count == 0) // si no se tiene ningun item en el detalle , desabilitamos el boton aceptar
            {
                btnAceptar.IsEnabled = false;
            }
            btnSeleccionarDrawBotton.IsEnabled = true;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
        }
        #endregion

        #region TextBoxes


        private void txtIdDepOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (string.IsNullOrEmpty(txtIdDepOrigen.Text))
                {
                    MessageBox.Show("Debe ingresar un numero de deposito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    txtIdDepOrigen.Focus();
                }
                else
                {

                    _iddeposito = Convert.ToInt16(txtIdDepOrigen.Text);
                    if (_iddeposito == 0)
                    {
                        //no puede ser cero
                        MessageBox.Show("El numero de deposito no puede ser cero", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else
                    {
                        Deposito deposito = coreBase.DepositoBuscarUno(_iddeposito);
                        if (deposito.IdDeposito == 0)
                        {
                            MessageBox.Show("El deposito ingresado no existe.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        else
                        {
                            //si el codigo del deposito ingresado es correcto cargamos los productos para ese deposito
                            //y habilitamos los distintos sectores del remito
                            txbNombreDepOrigen.Text = deposito.NomDepo;
                            productos = coreProducto.SelectorProductoPorDeposito(_iddeposito);
                            dgSeleProductos.ItemsSource = vistaProductos;
                            dgSeleProductos.DataContext = vistaProductos;
                            //bordeEncabezado.IsEnabled = true;
                            //btnSeleccion.IsEnabled = true;

                        }
                    }
                }
            }
        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab) // si se presiona enter o tab
            {
                DocumentoDetalle itemDetalle = dgDetalleDocu.SelectedItem as DocumentoDetalle;

                TextBox box = sender as TextBox; // el objeto sender lo tomamos como un textbox
                int _cantidad = Convert.ToInt32(box.Text); // convetirmos la cantidad ingresada
                if (_cantidad == 0 || _cantidad < 0) // si la cantidad es igual a cero o es negativa
                {
                    MessageBox.Show("La cantidad no puede ser cero o negativa", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    // si la cantidad tiene valores de formato correcto, entonces debemos validar contra el stock disponible del item
                    if (_cantidad > itemDetalle.StockDisponible) // si la cantidad ingresada es mayor que el stock disponible del producto
                    {
                        MessageBox.Show("La cantidad excede el stock disponible.Debe indicar una cantidad distinta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;

                    }
                    else
                    {
                        // si las cantidades son correctas, debemos entonces actualizar la cantidad del item seleccionado

                        itemDetalle.CantidadItem = _cantidad;
                        //si todo esta correcto entonces habilitamos el boton de seleccionar un producto 
                        btnSeleccionarDrawBotton.IsEnabled = true;
                    }

                }
            }
        }
        #endregion

        #region Combobox


        #endregion

        #region Grids

        #endregion

        #region Drawbottom

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)//buscador del producto
        {
            vistaProductos.Filter = filtroBuscarProducto; // cada vez que cambia el contenido de la propiedad text , se aplica el filtro a la vista
        }

        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)//seleccionar un producto 
        {
            //acciones del boton de seleccion del DrawBotton
            bool _existe_item_repetido = false;
            p_selec = dgSeleProductos.SelectedItem as StockProducto; // creamos un objeto producto desde la seleccion de la grid 

            //chekeamos que el producto no este en mantenimiento , es decir si la situacion fisica es igual a 3
            //primero lo buscamos
            Producto p = coreProducto.BuscarDatosUno(p_selec.IdProducto);
            if (p.Idsf == 3)
            {
                MessageBox.Show("El producto se encuentra en mantenimiento. No puede seleccionarlo", "Aviso", MessageBoxButton.OK);

                return;

            }
            // chekeamos que el producto tiene stock
            if (p_selec.StkDisponible == 0)
            {
                MessageBox.Show("El producto no tiene stock disponible. No puede seleccionarlo", "Aviso", MessageBoxButton.OK);

                return;
            }
            else
            {
                //ahora verificamos que no se repita el producto seleccionado en el detalle
                foreach (var item in documentoDetalles)
                {
                    if (item.CodigoItem.Equals(p_selec.IdProducto)) // chequeo de los codigos de producto
                    {
                        _existe_item_repetido = true; // seteamos la varaible de control del resultado de la verificacion
                        MessageBox.Show("El producto ya existe en el detalle.No puede agregar productos repetidos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break; //sale del bucle y queda esperando otra seleccion
                    }
                };

                if (!_existe_item_repetido) // si el resultado de la verificacion es falso
                {


                    //asignamos el valor de esas propiedades aun objeto del tipo documentodetalle
                    DocumentoDetalle dt = new DocumentoDetalle();
                    dt.CodigoItem = p_selec.IdProducto;
                    dt.Descripcion = p_selec.NombreProducto;
                    dt.CodIventario = p_selec.CodInventario;
                    dt.Marca = p_selec.Marca;
                    dt.Modelo = p_selec.ModeloProducto;
                    dt.CantidadItem = 0;
                    dt.TipoItem = 1;
                    dt.StockDisponible = p_selec.StkDisponible;
                    //guardamos el stock disponible del item en la variable de comparacion
                    _StkDisponibleItem = p_selec.StkDisponible;
                    //buscamos el producto para ver su precio actual
                    Producto pp = coreProducto.BuscarDatosUno(dt.CodigoItem);
                    //pasamos el precio del producto buscado al detalle del remito
                    dt.PrecioItem = pp.PrecioUnitario;
                    //agregamos el objeto documentodetalle a la coleccion de esos objetos
                    documentoDetalles.Add(dt);
                    //refrescamos la lista de detalle  algo importante para el remito
                    dgDetalleDocu.ItemsSource = documentoDetalles;
                    dgDetalleDocu.DataContext = documentoDetalles;
                    if (!btnAceptar.IsEnabled) // si el botom aceptar esta desactivado lo activamos por primera vez.
                    {
                        btnAceptar.IsEnabled = true;
                    }
                    btnSeleccionarDrawBotton.IsEnabled = false; //desabilitamos el boton de seleccion para que se compruebe las cantidades
                    //del evento del cuadro cantidad en el datagrid
                    btnCerrarDrawBotton.Command.Execute(Dock.Bottom);// cerramos el draw
                }
            }
        }

        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)//cambiar tipo producto
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
            cmbCategoriaProducto.ItemsSource = categoriaPs;
            vistaProductos.Filter = filtroTipoProducto;
        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)//cambiamos la categoria
        {
            vistaProductos.Filter = filtroCateProducto;
        }













        #endregion

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        #region MetodosPrivados





        private void ActualizarStockProductos()
        {
            //como se trata de un remito de consumo debemos restar al stock
            _operacion = "Egreso";
            int _iddeposito = Convert.ToInt32(txtIdDepOrigen.Text);
            foreach (var item in documentoDetalles)
            {
                coreProducto.ActualizarStock(item.CodigoItem, item.CantidadItem, _iddeposito, _operacion, item.PrecioItem);
            }

        }

        private void GrabarDetalleEnBD()
        {
            // obtenemos el ultimo iddocumento 
            _ultimoIddocumento = coreRemito.ObtenerUltimoIdDocumento();

            foreach (var item in documentoDetalles)
            {
                item.IdDocumento = _ultimoIddocumento;

                coreRemito.GrabarDetalle(item);

            }
        }

        private void GrabarEncabezadoEnBD()
        {


            documento.Alta = DateTime.Today;
            documento.IdTipoRem = 10; // vale consumo producto
            documento.Concepto = "Consumo";

            documento.CostoDocu = CalcularCostoDocumento(); //calculamos el costo del remito
            documento.IdEstado = 1;


            documento.IdDeposito = Convert.ToInt32(txtIdDepOrigen.Text);// deposito que envia los materiales
            documento.IdUsuario = Contexto.CodUser;

            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = DateTime.Today;

            coreRemito.GrabarEncabezado(documento);
        }

        private decimal CalcularCostoDocumento()
        {
            decimal _costo = 0;
            foreach (var item in documentoDetalles)
            {
                _costo = (item.PrecioItem * item.CantidadItem) + _costo;
            }
            return _costo;
        }

        private bool ValidarDatosObligatarios()
        {
            if (string.IsNullOrWhiteSpace(txtIdDepOrigen.Text))
            {
                MessageBox.Show("Debe ingresar el numero de deposito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                return true;

            }

        }

        private bool ValidarDetalleRemito()
        {
            bool _valido = true;
            //debemos validar el detalle del remito por si no se ingresan items o bien no tienen cantidades asignadas
            if (documentoDetalles.Count == 0)
            {

                return false;
            }
            else
            {
                foreach (var item in documentoDetalles)
                {
                    if (item.CantidadItem == 0)
                    {

                        _valido = false;
                        break;
                    }
                }
                return _valido;
            }

        }
        #endregion

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
