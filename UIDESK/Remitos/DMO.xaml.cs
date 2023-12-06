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
    /// Lógica de interacción para DMO.xaml
    /// </summary>
    public partial class DMO : MaterialWindow
    {
        #region Declarativas


        BLLProducto coreProducto = new BLLProducto(); // acciones para productos
        BLLRemito coreRemito = new BLLRemito(); // acciones para remitos / documentos
        BLLBase coreBase = new BLLBase(); //  acciones base
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de productos
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos
        BLLEmpleados BLLEmpleados = new BLLEmpleados(); // acciones para empleados
        ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>(); // lista de empleados

        BLLObras BLLObras = new BLLObras(); // acciones para las obras
        Documento documento = new Documento(); //objeto documento usado en toda la clase

        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalles del documento
        BLLProveedor coreProveedor = new BLLProveedor();
        ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>();
        ObservableCollection<BalanceEmpleado> balance_empleado = new ObservableCollection<BalanceEmpleado>();// lista de herramientas que tiene a cargo el empleado
        BalanceEmpleado p_selec = new BalanceEmpleado();
        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones

        //private int _iddeposito; //contiene el id del deposito que esta ejecutando el remito
        // private int _numeracion_remito;
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

        public DMO()
        {
            InitializeComponent();
            empleados = BLLEmpleados.EmpleadosActivos();
            tipoProductos = coreProducto.ListarTipos();
            //ajustamos propiedades de los controles de datos
            cmbTipoProducto.ItemsSource = tipoProductos;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;

            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;
            vistaEmpleados.Filter = new Predicate<object>(filtroBuscarEmpleado);
            vistaProductos.Filter = new Predicate<object>(filtroBuscarProducto);
            vistaProductos.Filter = new Predicate<object>(filtroTipoProducto);
            vistaProductos.Filter = new Predicate<object>(filtroCateProducto);

            transportes = coreProveedor.ProveedorPorRubro(9); // proveedores de transportes
            cmbTransportes.ItemsSource = transportes;
            //txtNumeroDeposito.Focus();
        }


        #region Filtros
        private bool filtroObraProductos(object obj) // filtro de obra que usa el empleado de origen
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _imputacion = Convert.ToInt32(txtImputacionOrigen.Text);
            return p.Imputacion == _imputacion && p.DifCantidad > 0 && p.EstadoItem == 1;
        }

        private bool filtroCateProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            int _cateselec = ctp.IdCateP;
            return p.IdCateP == _cateselec;
        } // filtro categoria de producto

        private bool filtroTipoProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;

            TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
            int _tiposelec = t.IdTipoP;
            return p.IdTipoP == _tiposelec;


        }  //  filtro de tipo de producto

        private bool filtroBuscarProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.NombreProducto.Contains(txtBuscar.Text) || p.ModeloProducto.Contains(txtBuscar.Text) || p.MarcaProducto.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        } // filtro buscar un producto

        private bool filtroBuscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtEmpleadoOrigen.Text);
        } //  filtro buscar un empleado


        #endregion


        #region Ventana


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region TextBoxes

        private void txtEmpleadoOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(txtEmpleadoOrigen.Text))
                {
                    MessageBox.Show("Debe ingresar un id de empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    int _empleado = Convert.ToInt16(txtEmpleadoOrigen.Text);
                    Empleado emp = BLLEmpleados.BuscarPorId(_empleado);
                    if (emp == null)
                    {
                        MessageBox.Show("El empleado no existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        txtNombreEmOrigen.Text = emp.Nombre;
                    }
                }
            }
        }

        private void txtEmpleadoDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(txtEmpleadoDestino.Text))
                {
                    MessageBox.Show("Debe ingresar un id de empleado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    int _empleado = Convert.ToInt16(txtEmpleadoDestino.Text);
                    Empleado emp = BLLEmpleados.BuscarPorId(_empleado);
                    if (emp == null)
                    {
                        MessageBox.Show("El empleado no existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        txtNombreEmDestino.Text = emp.Nombre;
                    }
                }
            }
        }

        private void txtImputacionDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(txtImputacionDestino.Text))
                {
                    MessageBox.Show("Debe ingresar una imputacion de destino", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    int _imp = Convert.ToInt16(txtImputacionDestino.Text);
                    bool resultado = BLLObras.ValidarNumeroImputacion(_imp);
                    if (resultado == false)
                    {
                        MessageBox.Show("La obra ingresada no existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else
                    {
                        Obra obra = BLLObras.BuscarImputacion(_imp);
                        txtNombreObraDestino.Text = obra.NombreObra;
                        txtClienteDestino.Text = obra.Cliente;
                    }
                }
            }
        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }

        private void txtImputacionOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(txtImputacionOrigen.Text))
                {
                    MessageBox.Show("Debe ingresar una imputacion de origen", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    int _imp = Convert.ToInt16(txtImputacionOrigen.Text);
                    //debemos validar la imputacion
                    bool resultado = BLLObras.ValidarNumeroImputacion(_imp);
                    if (resultado == false)
                    {
                        MessageBox.Show("La obra ingresada no existe", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else
                    {
                        Obra obra = BLLObras.BuscarImputacion(_imp);
                        txtNombreObra.Text = obra.NombreObra;
                        txtCliente.Text = obra.Cliente;
                    }
                }
            }



        }

        private void txtChofer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtChofer.SelectAll();
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
            else
            {
                MessageBox.Show("No hay seleccionado un producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
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



        #region Botones

        private void btnSeleccion_Click(object sender, RoutedEventArgs e)
        {
            //cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del balance del empleado  
            //se hace de origen del movimiento de las herramienta       
            int _idempleado = Convert.ToInt32(txtEmpleadoOrigen.Text);
            int _imputacion = Convert.ToInt32(txtImputacionOrigen.Text);
            balance_empleado = BLLEmpleados.BalanceEmpleado(_idempleado);
            vistaProductos.Filter = filtroObraProductos;
            dgSeleProductos.ItemsSource = vistaProductos;
            dgSeleProductos.DataContext = vistaProductos;
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
                //validamos el detalle del remito
                bool _validodet = ValidarDetalleRemito();
                if (_validodet)
                {


                    //2) grabamos el encabezado si estan los datos correctos ingresados
                    GrabarEncabezadoEnBD();
                    //3) grabar el detalle
                    GrabarDetalleEnBD();

                    //4 ) actualizar balance del empleado
                    ActualizarBalanceEmpleado();
                    //5 actualizamos la situcion fisica del producto
                    ActualizarSFProductos();
                    //7 )imprimir el remito
                    MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        //MessageBox.Show("Lo sentimos , por ahora ese servicio no esta disponible por el momento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        //ImprimirDMO imprimir = new ImprimirDMO(_ultimoIddocumento);
                        //imprimir.Show();
                        PrintRemitoDMO printRemitoObra = new PrintRemitoDMO(_ultimoIddocumento);

                        if (printRemitoObra.ShowDialog() == true)
                        {
                            MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    this.Close();
                }

            }
        }

        private void btnCancelarRemito_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }
        #endregion

        #region MetodosPrivados

        private void ActualizarBalanceEmpleado()
        {
            //hay que realizar dos tipos de actualizacion del balance
            // para el caso del origen , es un balance por devolucion
            // para lograr esto, recorremos el detalle del remito y luego vamos pasando los datos al metodo que se encarga de ello

            int _idempleadoOrigen = documento.IdEmpleado;
            int _imputacionOrigen = documento.Imputacion;
            foreach (var item in documentoDetalles)
            {
                BLLEmpleados.ActualizarBalancePorDevolucion(_idempleadoOrigen, item.CodigoItem, item.CantidadItem, _imputacionOrigen);
            }
            // para el caso del destino  , es un balance por ingreso
            // para lograr esto, recorremos el detalle del remito y luego vamos pasando los datos al metodo que se encarga de ello
            int _idempleadoDestino = documento.IdEmpleadoDestino;
            int _imputacionDestino = documento.ImputacionDestino;

            foreach (var item in documentoDetalles)
            {
                BLLEmpleados.ActualizarBalance(_idempleadoDestino, item.CodigoItem, item.CantidadItem, _imputacionDestino, item.PrecioItem);
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
                    coreProducto.ActualizarSituacionFisica(item.CodigoItem, 1);
                }
            }
        }

        /*  private void ActualizarStockProductos()
          {
              //como se trata de un remito de entrega debemos restar al stock
              _operacion = "Egreso";
              int _iddeposito = Convert.ToInt32(txtNumeroDeposito.Text);
              foreach (var item in documentoDetalles)
              {
                  coreProducto.ActualizarStock(item.CodigoItem, item.CantidadItem, _iddeposito, _operacion,item.PrecioItem);
              }

          }*/

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
            //si se selecciono la opcion de incluir muebles
            /* if (chkMuebles.IsChecked==true)
             {
                 Casa casa = cmbCasasObra.SelectedItem as Casa;
                 documento.IdCasa = casa.IdCasa;
             }*/

            documento.Alta = DateTime.Today;
            documento.IdTipoRem = 14;
            documento.Concepto = "Mov Obras";
            documento.Imputacion = Convert.ToInt32(txtImputacionOrigen.Text); // imputacion origen
            documento.ImputacionDestino = Convert.ToInt32(txtImputacionDestino.Text);// imputacion destino
            documento.CostoDocu = CalcularCostoDocumento(); //calculamos el costo del remito
            documento.IdEstado = 1;
            documento.IdTransporte = p.IdProve;
            documento.IdEmpleado = Convert.ToInt32(txtEmpleadoOrigen.Text); // empleado que recibe los materiales
            //documento.IdDeposito = Convert.ToInt32(txtNumeroDeposito.Text);// deposito que envia los materiales
            documento.IdEmpleadoDestino = Convert.ToInt32(txtEmpleadoDestino.Text);
            documento.IdUsuario = Contexto.CodUser;
            documento.Chofer = txtChofer.Text;
            documento.NotaRemito = txtNotaRemito.Text;
            //documento.FechaRemito = dtpFechaRemito.SelectedDate.Value;

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
            if (string.IsNullOrEmpty(txtImputacionOrigen.Text))
            {
                MessageBox.Show("Debe ingresar una imputacion de origen", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtImputacionDestino.Text))
                {
                    MessageBox.Show("Debe ingresar una imputacion de destino", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtEmpleadoOrigen.Text))
                    {
                        MessageBox.Show("Debe ingresar un empleado de origen", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtEmpleadoDestino.Text))
                        {
                            MessageBox.Show("Debe ingresar un empleado de destino", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
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
