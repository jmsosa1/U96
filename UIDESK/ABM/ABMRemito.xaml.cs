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

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMRemito.xaml
    /// </summary>
    public partial class ABMRemito : MaterialWindow 
    {
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


        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones
        StockProducto p_selec;// objeto que contiene al producto seleccionado del selector
        private int _iddeposito; //contiene el id del deposito que esta ejecutando el remito
        public string _operacion = ""; // indica el tipo de operacion que se esta haciendo con el remito

        public ICollectionView vistaEmpleados // vista empleados 
        {
            get { return CollectionViewSource.GetDefaultView(empleados); }
        }
        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(productos); }
        }

        //constructor
        public ABMRemito()
        {
            InitializeComponent();
            //llenamos las colecciones
            productos = coreProducto.SelectorProducto();
            empleados = BLLEmpleados.EmpleadosActivos();
            tipoProductos = coreProducto.ListarTipos();
            //ajustamos propiedades de los controles de datos
            cmbTipoProducto.ItemsSource = tipoProductos;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
            lstResultadoBusquedaEmpleado.ItemsSource = vistaEmpleados; // a propiedad item source de la lista de empleados
            dgConsultaVh.ItemsSource = vistaProductos;
            dgConsultaVh.DataContext = vistaProductos;
            vistaEmpleados.Filter = new Predicate<object>(filtroBuscarEmpleado);
            vistaProductos.Filter = new Predicate<object>(filtroBuscarProducto);
            vistaProductos.Filter = new Predicate<object>(filtroTipoProducto);
            vistaProductos.Filter = new Predicate<object>(filtroCateProducto);
            vistaProductos.Filter = new Predicate<object>(productosPorDeposito);
            txtNumDocu.Focus();

        }

        // filtros 
        private bool productosPorDeposito(object obj)
        {
            StockProducto producto = obj as StockProducto;
            return producto.IdDeposito == 1; // aca usamos la constante 1 , pero en realidad debe ser desde el deposito elejido
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

        private bool filtroBuscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtBuscarEmpleado.Text);
        }



        private void BtnAceptar_Click(object sender, RoutedEventArgs e)//aceptar y generar el remito
        {
            //en este lugar debemos hacer algunas cosas

            //1 )
            //validamos primero que esten todos los datos
            if (dtpFechaRemito.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha para el remito", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtImputacion.Text) || Convert.ToInt32(txtImputacion.Text) == 0)
            {
                MessageBox.Show("EL numero de imputacion no es valido o falta", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtDestinatario.Text))
            {
                MessageBox.Show("El codigo del destinatario no es valido o falta", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtChofer.Text))
            {
                MessageBox.Show("El dato del chofer no es valido o falta", "Aviso", MessageBoxButton.OK);
                return;
            }


            //2) grabamos el encabezado
            documento.NumDocumento = txtNumDocu.Text;
            documento.TipoDocumento = txtTipoDocu.Text;
            documento.Alta = DateTime.Today;
            documento.Concepto = txtConcepto.Text;
            documento.Imputacion = Convert.ToInt32(txtImputacion.Text);
            documento.CostoDocu = 0;
            documento.Estado = "Activo";
            documento.RemitoProveedor = "no indica";
            //documento.FechaRemProveedor = DateTime.Today;
            documento.NumFacturaProveedor = "no indica";
            documento.ImporteFacturaProveedor = 0;
            //documento.FechaFacProveedor = DateTime.Today;
            documento.NumeroOc = "no indica";
            documento.Registrado = 0;
            documento.Transporte = txtTransporte.Text;
            documento.IdEmpleado = Convert.ToInt32(txtDestinatario.Text);
            documento.IdCasa = 0;
            documento.IdProve = 1453;
            documento.IdDeposito = 0;
            documento.IdUsuario = Contexto.CodUser;
            documento.Chofer = txtChofer.Text;
            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = dtpFechaRemito.SelectedDate.Value;
            coreRemito.GrabarEncabezado(documento);

            //3) grabar el detalle
            // obtenemos el ultimo iddocumento 
            int _ultimoIddocumento = coreRemito.ObtenerUltimoIdDocumento();

            foreach (var item in documentoDetalles)
            {
                item.IdDocumento = _ultimoIddocumento;

                coreRemito.GrabarDetalle(item);

            }


            //4)actualizamos el stock de los productos en el deposito correspondiente 
            //como se trata de un remito de entrega debemos restar al stock
            int _iddeposito = Convert.ToInt32(txtNumeroDeposito.Text);
            foreach (var item in documentoDetalles)
            {
                coreProducto.ActualizarStock(item.CodigoItem, item.CantidadItem, _iddeposito, _operacion, item.PrecioItem);
            }

            //5) actualizamos la situcion fisica del producto si es que controla situacion fisica
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
            //6 ) actualizar balance del empleado
            // para lograr esto, recorremos el detalle del remito y luego vamos pasando los datos al metodo que se encarga de ello
            int _idempleado = documento.IdEmpleado;
            int _imputacion = documento.Imputacion;

            foreach (var item in documentoDetalles)
            {
                BLLEmpleados.ActualizarBalance(_idempleado, item.CodigoItem, item.CantidadItem, _imputacion, item.PrecioItem);
            }


            //7 )imprimir el remito
            MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_ultimoIddocumento);
                imprimir.Show();

            }
            this.Close();



        }


        private void BtnCancelar_Click(object sender, RoutedEventArgs e)//cancelar el remito
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnAddDel_Click(object sender, RoutedEventArgs e) // borrar un item de la lista
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

        private void TxtImputacion_KeyDown(object sender, KeyEventArgs e) // indicar la obra
        {
            if (e.Key == Key.Enter)
            {
                //buscamos los datos de la obra en cuestion
                int _imputacion = Convert.ToInt32(txtImputacion.Text);
                Obra obra = new Obra();
                obra = BLLObras.BuscarImputacion(_imputacion);
                if (obra != null)
                {
                    txbCliente.Text = obra.Cliente;
                    txbCuit.Text = obra.Cuit;
                    txbDireccionObra.Text = obra.DireccionObra;
                    txbObra.Text = obra.NombreObra;
                    txbLocalidad.Text = obra.Localidad;
                    txbProvincia.Text = obra.Provincia;
                    txtTransporte.Text = "PROYECCION ELECTROLUZ SRL";

                }
                else
                {

                    MessageBox.Show("La obra no existe en el sistema", "Aviso", MessageBoxButton.OK);


                }

            }
        }

        #region DrawLeft

        private void TxtBuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e) //indicar el destinatario
        {
            vistaEmpleados.Filter = filtroBuscarEmpleado; //cada vez que cambia el contenido de la propiedad text, se aplica el filtro a la vista
        }

        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)// seleccionar el destinatario
        {
            Empleado empleado = lstResultadoBusquedaEmpleado.SelectedItem as Empleado;
            txtDestinatario.Text = Convert.ToString(empleado.IdEmpleado);
            txbNombreDestinatario.Text = empleado.Nombre;

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
            p_selec = dgConsultaVh.SelectedItem as StockProducto; // creamos un objeto producto desde la seleccion de la grid 

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


        private void txtNumeroDeposito_KeyDown(object sender, KeyEventArgs e)//indicar el deposito origen
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
                            txbNombreDeposito.Text = deposito.NomDepo;
                            bordeEncabezado.IsEnabled = true;
                        }
                    }
                }
            }
        }

        private void txtNumeroDeposito_GotFocus(object sender, RoutedEventArgs e)//foco sobre el numero de deposito
        {
            txtNumeroDeposito.SelectAll();
        }

        private void btnSeleccion_Click(object sender, RoutedEventArgs e)//abrimos el selector de productos
        {
            //cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del deposito desde donde 
            //se hace la entrega de las herramienta          
            vistaProductos.Filter = productosPorDeposito;

        }


        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)//enfoque de la celda "cantidad"en la grid detalle
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }


        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)//textboxt de celda "cantidad" del grid detalle
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab) // si se presiona enter o tab
            {
                TextBox box = sender as TextBox; // el objeto sender lo tomamos como un textbox
                int _cantidad = Convert.ToInt32(box.Text); // convetirmos la cantidad ingresada
                if (_cantidad == 0 || _cantidad < 0) // si la cantidad es igual a cero o es negativa
                {
                    MessageBox.Show("La cantidad no puede ser cero o negativa", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    // si la cantidad tiene valores de formato correcto, entonces debemos validar contra el stock disponible
                    if (_cantidad > _StkDisponibleItem) // si la cantidad ingresada es mayor que el stock disponible del producto
                    {
                        MessageBox.Show("La cantidad excede el stock disponible.Debe indicar una cantidad distinta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;

                    }
                    else
                    {
                        // si las cantidades son correctas, debemos entonces actualizar la cantidad del item seleccionado
                        DocumentoDetalle itemDetalle = dgDetalleDocu.SelectedItem as DocumentoDetalle;
                        itemDetalle.CantidadItem = _cantidad;
                    }
                    //si todo esta correcto entonces habilitamos el boton de seleccionar un producto 
                    btnSeleccionarDrawBotton.IsEnabled = true;
                }
            }
        }


        private void txtChofer_GotFocus(object sender, RoutedEventArgs e) //foco sobre el nombre del chofer
        {
            txtChofer.SelectAll();
        }


    }
}
