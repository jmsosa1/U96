using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UIDESK.imprimir;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para DIP.xaml
    /// </summary>
    public partial class DIP : MaterialWindow 
    {
        #region Declarativas

        private int _iddeposito; //contiene el id del deposito que esta ejecutando el remito
        ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de producto
        //ObservableCollection<Documento> lista_rem_proveedor = new ObservableCollection<Documento>();
        BLLRemito coreRemito = new BLLRemito(); // acciones para remitos / documentos
        BLLProveedor bLLProveedor = new BLLProveedor(); // contiene las operaciones de los proveedores
        List<Proveedor> proveedors = new List<Proveedor>(); // lista de proveedores
        BLLProducto coreProducto = new BLLProducto(); // contiene las operaciones sobre los productos
        Proveedor proveedor = new Proveedor(); // representa a un objeto proveedor
        BLLBase coreBase = new BLLBase(); // contiene la operaciones del namespace BASE
        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalle del documento
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos
        ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>(); //lista de proveedores de transportes
        Documento documento = new Documento(); //objeto documento usado en toda la clase
        Producto p_selec = new Producto(); // representa al producto seleccionado desde el selector de productos
        int _ultimoIddocumento = 0; // almacena el ultimo id de registro de la tabla documento
                                    // string _operacion = ""; //representa la operacion a realizar respecto del stock del producto ingresado
        List<Deposito> depositos = new List<Deposito>();

        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }
        #endregion

        public DIP()
        {
            InitializeComponent();
            txtTipoDocu.Text = "DIP";
            txtConcepto.Text = "Recepcion";
            dtpFechaRemito.SelectedDate = DateTime.Today;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
            tipoProductos = coreProducto.ListarTipos();
            cmbTipoProducto.ItemsSource = tipoProductos;

            transportes = bLLProveedor.ProveedorPorRubro(9); // proveedores de transportes
            cmbTransportes.ItemsSource = transportes;
            productos = coreProducto.ListarTodos();
            //lista_rem_proveedor = coreRemito.RemitosProductosTodos();
            depositos = coreBase.ListarDepositos();
            cmbDepositos.ItemsSource = depositos;
        }

        #region Filtros



        private bool filtroBuscarProducto(object obj)
        {
            Producto p = obj as Producto;
            return p.Nombre.Contains(txtBuscar.Text) || p.Modelo.Contains(txtBuscar.Text) || p.Marca.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        }
        private bool filtroCateProducto(object obj)
        {
            Producto p = obj as Producto;
            if (cmbCategoriaProducto.SelectedItem != null)
            {
                CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;

                return p.IdCateP == ctp.IdCateP;
            }
            else
            {
                return p.Categoria == null;
            }
        }

        private bool filtroTipoProducto(object obj)
        {
            Producto p = obj as Producto;
            if (cmbTipoProducto.SelectedItem != null)
            {
                TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;

                return p.IdTipoP == t.IdTipoP;
            }
            else
            {
                return p.Tipo == null;
            }

        }
        #endregion

        #region EventosBotones

        //seleccion del producto 
        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            //acciones del boton de seleccion del DrawBotton
            bool _existe_item_repetido = false;
            p_selec = dgSeleProductos.SelectedItem as Producto; // creamos un objeto producto desde la seleccion de la grid 
            if (p_selec != null)
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
                    dt.Descripcion = p_selec.Nombre;
                    dt.CodIventario = p_selec.CodInventario;
                    dt.Marca = p_selec.Marca;
                    dt.Modelo = p_selec.Modelo;
                    dt.CantidadItem = 0;
                    dt.TipoItem = 1;
                    dt.PrecioItem = p_selec.PrecioUnitario;

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
            else
            {
                MessageBox.Show("No hay seleccionado un producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }
        //seleccion del proveedor
        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto desde la seleccion en la lista 
            Proveedor p = lstResultadoBusquedaProveedor.SelectedItem as Proveedor;
            if (p != null)
            {

                //pasamos los datos dle objeto a los cuadros de texto determinados
                txtCodProveedor.Text = p.IdProve.ToString();
                txbNombreProveedor.Text = p.RazonSocial;
                //cerramos el drawer de seleccion(izquierdo)
                btnCerrarDrawLeft.Command.Execute(Dock.Left);

            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK);
                return;
            }

        }

        //borrar el item seleccionado del detalle
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
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
        }

        //registrar el ingreso a deposito del producto comprado
        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //en este lugar debemos hacer algunas cosas
            //1) validamos que el grid tenga items 
            if (documentoDetalles.Count == 0)
            {
                MessageBox.Show("No existe detalle del remito. No se puede procesar la operacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //2)//validamos primero que esten todos los datos
            bool _valido = ValidarDatosObligatarios();

            if (_valido)
            {
                //3) grabamos el encabezado si estan los datos correctos ingresados
                GrabarEncabezadoEnBD();
                //4) grabar el detalle
                GrabarDetalleEnBD();
                //5) damos de alta los productos en el deposito que recepciona
                IngresoProductoAlStockDeposito();

                //6) actualizamos la situcion fisica del producto si es que controla situacion fisica
                ActualizarSFProductos();

                //7 )imprimir el remito
                MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ImprimirDIP imprimir = new ImprimirDIP(_ultimoIddocumento);
                    imprimir.Show();

                }
                this.Close();

            }
        }


        //cancelar la operacion de ingreso
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        //activar el selector de productos
        private void btnSeleccion_Click(object sender, RoutedEventArgs e)
        {
            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;
        }
        #endregion

        #region EventosTextBox

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //contiene la representacion de busqued de un producto en el selector
            vistaProductos.Filter = filtroBuscarProducto;
        }

        private void txtBuscarProveedor_TextChanged(object sender, TextChangedEventArgs e)
        {
            //contiene la representacion de busqueda para un proveedor 
            proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProveedor.Text);

            lstResultadoBusquedaProveedor.ItemsSource = proveedors;
        }


        private void txtChofer_GotFocus(object sender, RoutedEventArgs e)
        {
            txtChofer.SelectAll();
        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {

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

                    // si las cantidades son correctas, debemos entonces actualizar la cantidad del item seleccionado

                    itemDetalle.CantidadItem = _cantidad;
                    //si todo esta correcto entonces habilitamos el boton de seleccionar un producto 
                    btnSeleccionarDrawBotton.IsEnabled = true;


                }
            }
        }


        private void txtNumeroDeposito_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumeroDeposito.SelectAll();
        }
        private void txtNumeroOC_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumeroOC.SelectAll();
        }

        private void txtNumeroDeposito_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (string.IsNullOrEmpty(txtNumeroDeposito.Text))
                {
                    MessageBox.Show("Debe ingresar un numero de deposito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    txbNombreDeposito.Focus();
                }
                else
                {

                    _iddeposito = Convert.ToInt16(txtNumeroDeposito.Text);
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
                            txbNombreDeposito.Text = deposito.NomDepo;
                            productos = coreProducto.ListarTodos();
                            dgSeleProductos.ItemsSource = vistaProductos;
                            dgSeleProductos.DataContext = vistaProductos;
                            //bordeEncabezado.IsEnabled = true;
                            btnSeleccion.IsEnabled = true;

                        }
                    }
                }
            }
        }




        #endregion

        #region EventosVentana

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region EventosComboBox
        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            if (tipo != null)
            {


                categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
                cmbCategoriaProducto.ItemsSource = categoriaPs;
                vistaProductos.Filter = filtroTipoProducto;
            }
        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaProductos.Filter = filtroCateProducto;
        }
        #endregion

        #region MetodosPrivados



        private void ActualizarSFProductos()
        {
            //para este caso que el producto ingresa a deposito , el codigo de situacion fisica es el numero 2
            foreach (var item in documentoDetalles)
            {
                //buscamos el producto
                Producto _idp = coreProducto.BuscarDatosUno(item.CodigoItem);
                if (_idp.ControlSf == 1) // indica que el producto controla la situacion fisica
                {
                    //actualizamos la situacion fisica si es que el producto asi lo requiere
                    coreProducto.ActualizarSituacionFisica(item.CodigoItem, 2);
                }
            }
        }

        private void IngresoProductoAlStockDeposito()
        {
            //debemos ingresar cada producto del detalle al stock 
            foreach (var item in documentoDetalles)
            {
                coreProducto.AltaEnStock(item.CodigoItem, item.CantidadItem, _iddeposito);

            }
            foreach (var item in documentoDetalles)
            {
                //recalcular el costo del stock del producto en deposito porque se ingresan nuevas cantidades
                Producto p = coreProducto.BuscarDatosUno(item.CodigoItem);
                coreProducto.RecalcularCostoStock(p.IdProducto, _iddeposito, p.PrecioUnitario);
            }
        }



        private void GrabarDetalleEnBD()
        {
            // obtenemos el ultimo iddocumento 
            _ultimoIddocumento = coreRemito.ObtenerUltimoIdDocumento();

            foreach (var item in documentoDetalles)
            {
                //primero verificamos que la cantidad ingresada sea mayor que 0 
                if (item.CantidadItem <= 0)
                {
                    MessageBox.Show("Uno de los items tiene cantidad Cero. Debe ingresar una cantidad distinta de cero", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                }
                item.IdDocumento = _ultimoIddocumento;

                coreRemito.GrabarDetalle(item);

            }
        }

        private void GrabarEncabezadoEnBD()
        {
            Proveedor p = cmbTransportes.SelectedItem as Proveedor;

            documento.Alta = DateTime.Today;
            documento.IdTipoRem = 5;
            documento.Concepto = txtConcepto.Text;

            documento.CostoDocu = CalcularCostoDocumento(); //calculamos el costo del remito
            documento.IdEstado = 1;
            documento.IdTransporte = p.IdProve;
            documento.Transporte = p.RazonSocial;
            documento.IdProve = Convert.ToInt32(txtCodProveedor.Text);
            documento.IdDepDestino = Convert.ToInt32(txtNumeroDeposito.Text);// deposito que recibe los materiales
            documento.NombreDepDestino = txbNombreDeposito.Text;
            documento.IdUsuario = Contexto.CodUser; // usuario que realiza la operacion
            documento.Chofer = txtChofer.Text;
            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = dtpFechaRemito.SelectedDate.Value;
            documento.FechaRemProveedor = dtpFechaDocumento.SelectedDate;
            documento.RemitoProveedor = txtRemitoProve.Text;
            documento.FechaFacProveedor = null;
            documento.NumFacturaProveedor = "";

            if (rdbSi.IsChecked == true) // si se trata de un envio entre depositos
            {
                Deposito deporigen = cmbDepositos.SelectedItem as Deposito;
                if (deporigen != null)
                {
                    documento.IdDeposito = deporigen.IdDeposito;
                    documento.NombreDepOrigen = deporigen.NomDepo;
                }
                documento.FechaRemDepOrigen = dtpFechaRemitoInterno.SelectedDate;
                documento.RemDepOrigen = txtNumRemitoInterno.Text;
            }
            else
            {

            }

            documento.NumeroOc = txtNumeroOC.Text;
            documento.Registrado = 0;

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
            if (string.IsNullOrEmpty(txtNumeroDeposito.Text))
            {
                MessageBox.Show("Debe indicar el numero del deposito receptor", "Aviso", MessageBoxButton.OK);
                return false;
            }
            if (dtpFechaRemito.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha para el remito", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                if (string.IsNullOrEmpty(txtCodProveedor.Text) || Convert.ToInt32(txtCodProveedor.Text) == 0)
            {
                MessageBox.Show("Debe elegir un codigo de proveedor ", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                    if (string.IsNullOrEmpty(txtRemitoProve.Text))
            {
                MessageBox.Show("El numero de remito del proveedor no es valido o falta", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                   if (string.IsNullOrEmpty(txtChofer.Text))
            {
                MessageBox.Show("El dato del chofer no es valido o falta", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
            {
                if (cmbTransportes.SelectedItem == null)
                {
                    MessageBox.Show("El dato del transporte no es valido o falta", "Aviso", MessageBoxButton.OK);
                    return false;
                }
                else
                {
                    if (dtpFechaDocumento.SelectedDate == null)
                    {
                        MessageBox.Show("Debe indicar la fecha del remito del proveedor ", "Aviso", MessageBoxButton.OK);
                        return false;
                    }
                    else
                    {
                        if (rdbSi.IsChecked == true)
                        {
                            if (cmbDepositos.SelectedItem == null)
                            {
                                MessageBox.Show("Debe indicar el deposito que envia el material ", "Aviso", MessageBoxButton.OK);
                                return false;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtNumRemitoInterno.Text))
                                {
                                    MessageBox.Show("Debe indicar el numero remito interno  ", "Aviso", MessageBoxButton.OK);
                                    return false;
                                }
                                else
                                {
                                    if (dtpFechaRemitoInterno.SelectedDate == null)
                                    {
                                        MessageBox.Show("Debe indicar la fecha del remito interno ", "Aviso", MessageBoxButton.OK);
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
                    return true;
                }
            }



        }



        #endregion

        private void rdbSi_Checked(object sender, RoutedEventArgs e)
        {

            cmbDepositos.IsEnabled = true;
            txtNumRemitoInterno.IsEnabled = true;
            dtpFechaRemitoInterno.IsEnabled = true;
            cmbDepositos.Focus();


        }

        private void rdbNo_Checked(object sender, RoutedEventArgs e)
        {

            cmbDepositos.IsEnabled = false;
            txtNumRemitoInterno.IsEnabled = false;
            dtpFechaRemitoInterno.IsEnabled = false;

        }

        private void txtRemitoProve_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtRemitoProve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                int _idp = Convert.ToInt32(txtCodProveedor.Text);
                //hay que validar si existe el remito ingresado
                // para eso comparamos el texto ingresado en el cuadro con el campo de los registro en la lista
                bool validacion = coreRemito.ValidarRemitoProveedor(_idp, txtRemitoProve.Text);
                if (validacion)
                {
                    MessageBox.Show("Ya existe ese numero de remito registrado para este proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
        }
    }
}
