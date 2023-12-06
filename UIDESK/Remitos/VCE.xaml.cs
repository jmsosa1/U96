using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;

namespace UIDESK.Remitos
{
    /// <summary>
    /// Lógica de interacción para VCE.xaml
    /// </summary>
    public partial class VCE : MaterialWindow
    {
        #region Declarativas
        BLLBase coreBase = new BLLBase();
        BLLObras coreObra = new BLLObras(); // nucleo de obras
        BLLEmpleados coreEmpleado = new BLLEmpleados();
        BLLProducto coreProducto = new BLLProducto();
        Empleado _empleado = new Empleado(); // datos del empleado al que se le hace el vale
        ObservableCollection<BalanceEmpleado> balance_empleado = new ObservableCollection<BalanceEmpleado>();// lista de herramientas que tiene a cargo el empleado
        ObservableCollection<TipoProducto> tipoProductos = new ObservableCollection<TipoProducto>(); //lista de tipos de productos
        ObservableCollection<CategoriaP> categoriaPs = new ObservableCollection<CategoriaP>(); // lista de categoria de productos
        BalanceEmpleado p_selec = new BalanceEmpleado();
        Obra _obra = new Obra(); // obra resultado de la busqueda
        Documento documento = new Documento(); //objeto documento usado en toda la clase
        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>(); // lista de detalles del documento
        int _iduser = 0; // id usuario que hace el vale
        string _nombreuser; // nombre del usuario que hace el vale
        int _imputacion = 0; // imputacion de obra a buscar
        private decimal _StkDisponibleItem = 0; // varible que guarda el valor del stock disponible del item seleccionado para luego hacer comparaciones
        public ICollectionView vistaProductos // vista productos
        {
            get { return CollectionViewSource.GetDefaultView(balance_empleado); }
        }
        List<CausaBaja> _causasBajas = new List<CausaBaja>();
        #endregion

        public VCE(Empleado empleado,int iduser, string nombreuser )
        {
            InitializeComponent();
            _empleado = empleado;
            _iduser = iduser; 
            _nombreuser = nombreuser;
            txtIdUsuario.Text = _iduser.ToString();
            txtNombreUsuario.Text = _nombreuser;
            stpDatosEmpleado.DataContext = _empleado;
            _causasBajas = coreBase.ListaCausasBaja();
           

        }

        #region Filtros
       
        private bool filtroObraProductos(object obj)
        {
            //filtramos por imputacion 
            BalanceEmpleado p = obj as BalanceEmpleado;
            
            return p.Imputacion == _imputacion && p.DifCantidad > 0;
        }

        private bool filtroBuscarProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            return p.NombreProducto.Contains(txtBuscar.Text) || p.ModeloProducto.Contains(txtBuscar.Text) || p.MarcaProducto.Contains(txtBuscar.Text) || p.CodInventario.Contains(txtBuscar.Text);
        }

        private bool filtroTipoProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;

            TipoProducto t = cmbTipoProducto.SelectedItem as TipoProducto;
            int _tiposelec = t.IdTipoP;
            return p.IdTipoP == _tiposelec;


        }

        private bool filtroCateProducto(object obj)
        {
            BalanceEmpleado p = obj as BalanceEmpleado;
            CategoriaP ctp = cmbCategoriaProducto.SelectedItem as CategoriaP;
            int _cateselec = ctp.IdCateP;
            return p.IdCateP == _cateselec;


        }


        #endregion

        #region Botones
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("El producto no tiene cantidad disponible suficiente. No puede seleccionarlo", "Aviso", MessageBoxButton.OK);

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

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

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
            //cuando seleccionamos agregar un producto, lo que hacemos es configurar el grid con datos del balance del empleado  
            //se hace la devolucion de las herramienta       
            int _idempleado = _empleado.IdEmpleado;
            
            balance_empleado = coreEmpleado.BalanceEmpleado(_idempleado);
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
        #endregion

        #region TextBoxes
        private void txtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //chequeamos que no este vacio
                if (string.IsNullOrEmpty(txtImputacion.Text))
                {
                    MessageBox.Show("Debe ingresar un numero de imputacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    //si hay un numero de imputacion
                    // buscamos sus datos en la base de datos
                    _imputacion = Convert.ToInt32(txtImputacion.Text);
                    _obra = coreObra.BuscarImputacion(_imputacion);
                    stpDatosObra.DataContext = _obra;
                   
                }
            }
        }

      

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            vistaProductos.Filter = filtroBuscarProducto; // cada vez que cambia el contenido de la propiedad text , se aplica el filtro a la vista
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

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }
        #endregion

        #region Datagrid

        #endregion

        #region Combobox
        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CmbTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;
            categoriaPs = coreProducto.ListarCategoriasTipo(tipo.IdTipoP);
            cmbCategoriaProducto.ItemsSource = categoriaPs;
            vistaProductos.Filter = filtroTipoProducto;

        }

        private void cmbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vistaProductos.Filter = filtroCateProducto;
        }










        #endregion

        private void MaterialWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
