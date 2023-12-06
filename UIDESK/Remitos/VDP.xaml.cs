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
    /// Lógica de interacción para VDP.xaml
    /// </summary>
    public partial class VDP : MaterialWindow
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto(); // acciones para productos
        BLLRemito coreRemito = new BLLRemito(); // acciones para remitos / documentos
        BLLBase coreBase = new BLLBase();
        
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de productos
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos
        BLLEmpleados BLLEmpleados = new BLLEmpleados(); // acciones para empleados
        ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>(); // lista de empleados

        BLLObras BLLObras = new BLLObras(); // acciones para las obras
        Documento documento = new Documento(); //objeto documento usado en toda la clase
        List<CausaBaja> lista_causa = new List<CausaBaja>();
        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalles del documento
        //BLLProveedor coreProveedor = new BLLProveedor();
        //ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>();
        ObservableCollection<BalanceEmpleado> balance_empleado = new ObservableCollection<BalanceEmpleado>();// lista de herramientas que tiene a cargo el empleado
        BalanceEmpleado p_selec = new BalanceEmpleado();
        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones


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

        Empleado _empleado = new Empleado();
        public VDP(Empleado empleado, int iduser, string nombreuser) //vale de descuento al trabajador
        {
            InitializeComponent();
            _empleado = empleado;
            lista_causa = coreBase.ListaCausasBaja();
            txtIdUsuario.Text = Contexto.CodUser.ToString();
            txtNombreUsuario.Text = Contexto.Nomuser;
            stkDatosEmpleado.DataContext = _empleado;
            cmbCausaDescuento.ItemsSource = lista_causa;
            cmbCausaDescuento.DataContext = lista_causa;
          

        }

        #region filtros
        private bool filtroObraProductos(object obj)
        {
            //devuelve los productos solo para la obra ingresada
            BalanceEmpleado p = obj as BalanceEmpleado;
            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            return p.Imputacion == _imputacion && p.DifCantidad>0;
        }

        private bool filtroCateProducto(object obj)
        {
            //devuelve productos para una categoria seleccionada
            BalanceEmpleado p = obj as BalanceEmpleado;

            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            int _cateselec = ctp.IdCateP;
            return p.IdCateP == _cateselec;


        }

        private bool filtroTipoProducto(object obj)
        {
            //devuelve los productos para un tipo de producto seleccionado
            BalanceEmpleado p = obj as BalanceEmpleado;

            TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
            int _tiposelec = t.IdTipoP;
            return p.IdTipoP == _tiposelec;


        }

        private bool filtroBuscarProducto(object obj)
        {
            //busca un producto en el balande del empleado 
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.NombreProducto.Contains(txtBuscar.Text) || p.ModeloProducto.Contains(txtBuscar.Text) || p.MarcaProducto.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        }


        #endregion

        #region Ventana
      
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        #endregion


        #region Botones
        // boton que abre el selector de productos del balance del empleado
        private void btnSeleccion_Click(object sender, RoutedEventArgs e)
        {
            //cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del balance del empleado  
            //se hace la devolucion de las herramienta       
            int _idempleado = _empleado.IdEmpleado; ;
            int _imputacion = Convert.ToInt32(txtImputacion.Text);
            balance_empleado = BLLEmpleados.BalanceEmpleado(_idempleado);
            vistaProductos.Filter = filtroObraProductos;
            if (vistaProductos.IsEmpty)
            {
                MessageBox.Show("No hay productos disponibles para esa obra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            else
            {
                dgSeleProductos.ItemsSource = vistaProductos;
                dgSeleProductos.DataContext = vistaProductos;
            }
            
        }

        // boton del selector de productos del balance
        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            //acciones del boton de seleccion del DrawBotton
            bool _existe_item_repetido = false;
            p_selec = dgSeleProductos.SelectedItem as BalanceEmpleado; // creamos un objeto producto desde la seleccion de la grid 

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
                    // agregado el dia 28/12/21 por pedido de Gustavo.
                    //  se contabiliza el precio de reposicion del segmento para calcular el valor del consumo
                    //buscamos el producto para ver su datos del segmento al que pertenece
                    Producto pp = coreProducto.BuscarDatosUno(dt.CodigoItem);
                    //buscamos el segmento 
                    SegmentoP segmento=  coreProducto.BuscarUnSegmento(pp.IdSegP);

                    //pasamos el precio de reposicion del segmento del producto buscado al detalle del remito
                    dt.PrecioItem = segmento.CostoReposicion;
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
            // primero debemos validar los datos
            bool _valido = ValidarDatosObligatarios();
            if (_valido)
            {
                GrabarEncabezadoEnBD();
                GrabarDetalleEnBD();
                ActualizarBalanceEmpleado();
                //7 )imprimir el remito
                MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_ultimoIddocumento);
                    imprimir.Show();

                }
                this.Close();
            }

        }

        #endregion

        #region ComboBox


        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaProductos.Filter = filtroCateProducto;
        }

        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
            cmbCategoriaProducto.ItemsSource = categoriaPs;
            vistaProductos.Filter = filtroTipoProducto;
        }

        private void cmbCausaDescuento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // selector de la causa del descuento
        }

        #endregion

        #region TextBoxes
        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            vistaProductos.Filter = filtroBuscarProducto; // cada vez que cambia el contenido de la propiedad text , se aplica el filtro a la vista
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
                    txbClienteObra.Text = obra.Cliente;
                    txbNombreObra.Text = obra.NombreObra;

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


        #region MetodosPrivados

        private void ActualizarBalanceEmpleado()
        {
            // para lograr esto, recorremos el detalle del remito y luego vamos pasando los datos al metodo que se encarga de ello
            //en este caso el item en cuestion debe cambiar al estado "descuento"
            int _idempleado = documento.IdEmpleado;
            int _imputacion = documento.Imputacion;

            foreach (var item in documentoDetalles)
            {
                BLLEmpleados.ActualizarBalancePorDescuento(_idempleado, item.CodigoItem, item.CantidadItem, _imputacion, item.PrecioItem);
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

            CausaBaja causaBaja = cmbCausaDescuento.SelectedItem as CausaBaja;
            documento.Alta = DateTime.Today;
            documento.IdTipoRem = 11; // vale descuento de producto al empleado
            documento.Concepto = "Vale Descuento";
            documento.Imputacion = Convert.ToInt32(txtImputacion.Text);
            documento.CostoDocu = CalcularCostoDocumento(); //calculamos el costo del remito
            documento.IdEstado = 1;
            documento.IdTransporte = 0;
            documento.IdEmpleado = Convert.ToInt32(txtIdEmpleado.Text); // empleado al que se le hace el vale
            documento.IdDepDestino = 0;
            documento.IdUsuario = Convert.ToInt32(txtIdUsuario.Text);
            documento.Chofer = "";
            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = DateTime.Today.Date;
            documento.IdCausaDescuento = causaBaja.IdCausaBaja;

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

            if (string.IsNullOrEmpty(txtImputacion.Text) || Convert.ToInt32(txtImputacion.Text) == 0)
            {
                MessageBox.Show("EL numero de imputacion no es valido o falta", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
            {
                if (cmbCausaDescuento.SelectedItem == null) // si no se selecciona una causa de descuento
                {
                    MessageBox.Show("Debe seleccionar una causa de descuento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else
                {
                    if (documentoDetalles.Count == 0) //  si no se ingresa ningun detalle en el remito
                    {
                        MessageBox.Show("No existen item en el detalle.Debe ingresar al menos un item", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                    return true;
                }

            }

        }






        #endregion

        private void MaterialWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
