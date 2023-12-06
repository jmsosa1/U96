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
using UIDESK.imprimir;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para DDI.xaml
    /// </summary>
    public partial class DDI : MaterialWindow
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto(); // acciones para productos
        BLLRemito coreRemito = new BLLRemito(); // acciones para remitos / documentos
        BLLBase coreBase = new BLLBase();
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de productos
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos
        BLLEmpleados BLLEmpleados = new BLLEmpleados(); // acciones para empleados
        ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>(); // lista de empleados
        ObservableCollection<StockProducto> productos = new ObservableCollection<StockProducto>(); // lista de stock de productos
        BLLObras BLLObras = new BLLObras(); // acciones para las obras
        Documento documento = new Documento(); //objeto documento usado en toda la clase

        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalles del documento
        BLLProveedor coreProveedor = new BLLProveedor();
        ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>();
        ObservableCollection<BalanceEmpleado> balance_empleado = new ObservableCollection<BalanceEmpleado>();// lista de herramientas que tiene a cargo el empleado
        BalanceEmpleado p_selec = new BalanceEmpleado();

        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones

        private int _iddeposito; //contiene el id del deposito que esta ejecutando el remito
        //private int _numeracion_remito;
        public string _operacion = ""; // indica el tipo de operacion que se esta haciendo con el remito
        int _ultimoIddocumento = 0;
        public ICollectionView vistaEmpleados // vista empleados 
        {
            get { return CollectionViewSource.GetDefaultView(empleados); }
        }
        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(balance_empleado); }
        }
        #endregion

        public DDI()
        {
            InitializeComponent();
            //llenamos las colecciones

            empleados = BLLEmpleados.EmpleadosActivos();
            tipoProductos = coreProducto.ListarTipos();
            //ajustamos propiedades de los controles de datos
            cmbTipoProducto.ItemsSource = tipoProductos;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
            lstResultadoBusquedaEmpleado.ItemsSource = vistaEmpleados; // a propiedad item source de la lista de empleados
            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;
            vistaEmpleados.Filter = new Predicate<object>(filtroBuscarEmpleado);
            vistaProductos.Filter = new Predicate<object>(filtroBuscarProducto);

            vistaProductos.Filter = new Predicate<object>(filtroCateProducto);

            transportes = coreProveedor.ProveedorPorRubro(9); // proveedores de transportes
            cmbTransportes.ItemsSource = transportes;
            dtpFechaRemito.SelectedDate = DateTime.Today.Date;
            txtNumeroDeposito.Text = Convert.ToString(1);
            // _numeracion_remito = coreRemito.ObtenerUltimaNumeracion() + 1 ;
            //  txtNumDocu.Text = Convert.ToString(_numeracion_remito);

            txtNumeroDeposito.Focus();
        }

        #region Filtros
        private bool filtroObraProductos(object obj)
        {
            //filtramos por imputacion 
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            return p.Imputacion == _imputacion && p.IdTipoP == 2;
        }




        private bool filtroCateProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;

            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            int _cateselec = ctp.IdCateP;
            return p.IdCateP == _cateselec;

        }



        private bool filtroBuscarProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.NombreProducto.Contains(txtBuscar.Text) || p.ModeloProducto.Contains(txtBuscar.Text) || p.MarcaProducto.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        }

        private bool filtroBuscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtBuscarEmpleado.Text);
        }


        #endregion

        #region Ventana
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion


        #region TextBoxes
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

        private void txtNumeroDeposito_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumeroDeposito.SelectAll();
        }

        private void txtChofer_GotFocus(object sender, RoutedEventArgs e)
        {
            txtChofer.SelectAll();
        }


        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //buscamos los datos de la obra en cuestion
                int _imputacion = Convert.ToInt32(txtImputacion.Text);
                Obra obra = new Obra();
                obra = BLLObras.BuscarImputacion(_imputacion);
                if (obra != null)
                {
                    txtCliente.Text = obra.Cliente;
                    txtLocalidad.Text = obra.Localidad;
                    txtProvincia.Text = obra.Provincia;
                }
                else
                {
                    MessageBox.Show("La obra no existe en el sistema", "Aviso", MessageBoxButton.OK);
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

        #region RadioButton






        #endregion

        #region Botones
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
                    //5) actualizamos la situcion fisica del producto si es que controla situacion fisica
                    ActualizarSFProductos();
                    //6 ) actualizar balance del empleado
                    ActualizarBalanceEmpleado();
                    //7 )imprimir el remito
                    MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        ImprimirDSI imprimir = new ImprimirDSI(_ultimoIddocumento);
                        imprimir.Show();

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
                this.Close();
            }
        }

        private void btnSeleccion_Click(object sender, RoutedEventArgs e)
        {
            //cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del balance 
            //del empleado para esa imputacion y en este caso los productos de indumentaria 
            //se hace la entrega de las herramienta      
            int _idempleado = Convert.ToInt32(txtCodEmpleado.Text);

            balance_empleado = BLLEmpleados.BalanceEmpleado(_idempleado);
            vistaProductos.Filter = filtroObraProductos;
            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;
        }
        #endregion

        #region DrafLeft
        private void TxtBuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            vistaEmpleados.Filter = filtroBuscarEmpleado; //cada vez que cambia el contenido de la propiedad text, se aplica el filtro a la vista
        }

        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = lstResultadoBusquedaEmpleado.SelectedItem as Empleado;
            txtCodEmpleado.Text = Convert.ToString(empleado.IdEmpleado);
            txbNombreEmpleado.Text = empleado.Nombre;
            txtDniEmpleado.Text = Convert.ToString(empleado.Dni);
            txtSector.Text = empleado.NomSector;

            btnCerrarDrawLeft.Command.Execute(Dock.Left);
            bordeDetalle.IsEnabled = true;
        }

        #endregion

        #region DrawBottom

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)//buscador del producto
        {
            vistaProductos.Filter = filtroBuscarProducto; // cada vez que cambia el contenido de la propiedad text , se aplica el filtro a la vista
        }

        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)//seleccionar un producto 
        {
            //acciones del boton de seleccion del DrawBotton
            bool _existe_item_repetido = false;
            p_selec = dgSeleProductos.SelectedItem as BalanceEmpleado; // creamos un objeto producto desde la seleccion de la grid 

            if (p_selec != null)
            {


                //chekeamos que el producto no este en mantenimiento , es decir si la situacion fisica es igual a 3
                //primero lo buscamos
                Producto p = coreProducto.BuscarDatosUno(p_selec.IdProducto);
                if (p.Idsf == 3)
                {
                    MessageBox.Show("El producto se encuentra en mantenimiento. No puede seleccionarlo", "Aviso", MessageBoxButton.OK);

                    return;

                }
                // chekeamos que el producto tiene stock
                if (p_selec.CantidadEntregada == 0)
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
                        dt.Marca = p_selec.MarcaProducto;
                        dt.Modelo = p_selec.ModeloProducto;
                        dt.CantidadItem = 0;
                        dt.TipoItem = p_selec.IdTipoP;

                        dt.StockDisponible = p_selec.DifCantidad;
                        //guardamos la cantidad disponible del item en la variable de comparacion
                        _StkDisponibleItem = p_selec.DifCantidad;
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
            else
            {
                MessageBox.Show("No hay seleccionado un producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


        }

        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)//cambiar tipo producto
        {
            /*
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
            cmbCategoriaProducto.ItemsSource = categoriaPs;
            vistaProductos.Filter = filtroTipoProducto;*/
        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)//cambiamos la categoria
        {
            vistaProductos.Filter = filtroCateProducto;
        }


        #endregion


        #region MetodosPrivados

        private void ActualizarBalanceEmpleado()
        {
            // para lograr esto, recorremos el detalle del remito y luego vamos pasando los datos al metodo que se encarga de ello
            int _idempleado = documento.IdEmpleado;
            int _imputacion = documento.Imputacion;

            foreach (var item in documentoDetalles)
            {
                //actualizamos el balance por devolucion
                BLLEmpleados.ActualizarBalancePorDevolucion(_idempleado, item.CodigoItem, item.CantidadItem, _imputacion);
            }
        }

        private void ActualizarSFProductos()
        {
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

        private void ActualizarStockProductos()
        {
            //como se trata de un remito de devolucion debemos sumar al stock
            _operacion = "Devolucion";
            int _iddeposito = Convert.ToInt32(txtNumeroDeposito.Text);
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
            Proveedor p = cmbTransportes.SelectedItem as Proveedor;


            documento.Alta = DateTime.Today;
            documento.IdTipoRem = 9;// salida de indumentaria
            documento.Concepto = txtConcepto.Text;
            documento.Imputacion = Convert.ToInt32(txtImputacion.Text);
            documento.CostoDocu = CalcularCostoDocumento(); //calculamos el costo del remito
            documento.IdEstado = 1;
            documento.IdTransporte = p.IdProve;
            documento.IdEmpleado = Convert.ToInt32(txtCodEmpleado.Text); // empleado que recibe los materiales
            documento.IdDeposito = Convert.ToInt32(txtNumeroDeposito.Text);// deposito que envia los materiales
            documento.IdUsuario = Contexto.CodUser; // usuario del sistema que hace el remito
            documento.Chofer = txtChofer.Text;
            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = dtpFechaRemito.SelectedDate.Value;
            documento.NombreDepOrigen = txbNombreDeposito.Text;
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
            if (dtpFechaRemito.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha para el remito", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                if (string.IsNullOrEmpty(txtImputacion.Text) || Convert.ToInt32(txtImputacion.Text) == 0)
            {
                MessageBox.Show("EL numero de imputacion no es valido o falta", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                    if (string.IsNullOrEmpty(txtCodEmpleado.Text))
            {
                MessageBox.Show("El codigo del destinatario no es valido o falta", "Aviso", MessageBoxButton.OK);
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
                    if (string.IsNullOrEmpty(txtNumeroDeposito.Text))
                    {
                        MessageBox.Show("EL Dato del deposito no es valido o falta", "Aviso", MessageBoxButton.OK);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
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

       
    }
}
