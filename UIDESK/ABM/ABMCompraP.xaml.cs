using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCompraP.xaml
    /// </summary>
    public partial class ABMCompraP : MaterialWindow 
    {
        #region Declarativas
        BLLProveedor bLLProveedor = new BLLProveedor();
        List<Proveedor> proveedors = new List<Proveedor>();
        Proveedor proveedor = new Proveedor();
        BLLRemito coreRemito = new BLLRemito();
        ObservableCollection<Documento> encabezados = new ObservableCollection<Documento>();
        ObservableCollection<DocumentoDetalle> detalles = new ObservableCollection<DocumentoDetalle>();
        Documento Encabezado = new Documento();
        DocumentoDetalle Detalle = new DocumentoDetalle();
        int _idprove = 0;
        CompraP regCompra = new CompraP();
        CompraPDetalle detRegCompra = new CompraPDetalle();
        ObservableCollection<object> consulta_remitos = new ObservableCollection<object>();
        ObservableCollection<ConsultaRemitos> lista_rem_prove_pendientes = new ObservableCollection<ConsultaRemitos>();
        ObservableCollection<CompraPDetalle> compraPDetalles = new ObservableCollection<CompraPDetalle>();
        BLLProducto coreProducto = new BLLProducto();
        #endregion

        public ABMCompraP()
        {
            InitializeComponent();

            encabezados = coreRemito.RemitosProductosTodos();
            detalles = coreRemito.ListarDetallesDIP();
            dgDetalle.ItemsSource = compraPDetalles;
            dgDetalle.DataContext = compraPDetalles;
            DataContext = regCompra;

        }


        #region Botones

        private void btnQuitarItem_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
            indiceItemABorrar = dgDetalle.SelectedIndex; //tomamos su indice en la lista detalle
            CompraPDetalle _temp = dgDetalle.SelectedItem as CompraPDetalle; // este objeto lo usamos para actualizar los objetos de la lista de remitos
            compraPDetalles.RemoveAt(indiceItemABorrar); // lo removemos de la misma
            //como es una observable collection el cambio se refleja de manera inmediata
            //debemos actualizar el estado de la propiedad seleccionado del objeto ConsultaRemitos al valor seleccionado =0
            //lista_rem_prove_pendientes.Where(x => x.IdProducto == _temp.IdProducto && x.IdDocumento == _temp.IdRemito).FirstOrDefault().Seleccionado = 0;
            foreach (var item in lista_rem_prove_pendientes)
            {
                if (item.IdProducto == _temp.IdProducto && item.IdDocumento == _temp.IdRemito)
                {
                    item.Seleccionado = 0;
                }
            }

            bdgCItemMante.Badge = lista_rem_prove_pendientes.Count;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //en este lugar debemos hacer algunas cosas
            //1) validamos que el grid tenga items 
            if (compraPDetalles.Count == 0)
            {
                MessageBox.Show("No existe detalle del documento. No se puede procesar la operacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
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

                //5 actualizar el precio de los productos 
                ActualizarPreciosProductos();

                //7 )imprimir el remito
                MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //ImprimirRemitoProducto imprimir = new ImprimirRemitoProducto(_ultimoIddocumento);
                    //imprimir.Show();
                    this.Close();
                }
                this.Close();
            }
        }



        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            //cuando buscamos el proveedor seleccionado
            // proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProve.Text);

            //lstResultadoBusquedaProve.ItemsSource = proveedors;
        }

        private void btnSeleccionarProveedor_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto desde la seleccion en la lista 
            Proveedor p = lstResultadoBusquedaProve.SelectedItem as Proveedor;
            if (p != null)
            {

                //pasamos los datos del objeto a los cuadros de texto determinados
                txtIdProve.Text = p.IdProve.ToString();
                txbRazonSocial.Text = p.RazonSocial;
                //cerramos el drawer de seleccion(izquierdo)
                btnCerrarDrawLeft.Command.Execute(Dock.Left);
                //una vez seleccionado el proveedor devemos revisar si tiene remitos pendientes
                _idprove = p.IdProve;
                ListarRemitosPendientesProveedor(_idprove);



            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void ListarRemitosPendientesProveedor(int _id)
        {
            lista_rem_prove_pendientes.Clear(); // limpiamos la lista, ya que debemos actualizar con los registros pendientes
            var consulta_remitos = from Documento in encabezados
                                   join docdet in detalles on Documento.IdDocumento equals docdet.IdDocumento
                                   where Documento.IdProve == _id && docdet.EstadoItem == 1
                                   select new
                                   {
                                       Documento.IdDocumento,
                                       Documento.RemitoProveedor,
                                       Documento.FechaRemProveedor,
                                       Documento.NombreDepDestino,
                                       docdet.CodigoItem,
                                       docdet.Descripcion,
                                       docdet.Modelo,
                                       docdet.Marca,
                                       docdet.CantidadItem,
                                       docdet.PrecioItem
                                   };
            //esta consulta devuelve algunos datos del encabezado y del detalle de remitos donde el coincida el codigo del proveedor y el detalle del item del remito
            //no esta registrado, o sea sea igual a 1
            if (consulta_remitos != null) //  si la consulta no es nula entonces armamos lista de datos de remitos pendientes en vase a lista anterior
            {
                foreach (var item in consulta_remitos)
                {
                    ConsultaRemitos cr = new ConsultaRemitos();
                    cr.IdDocumento = item.IdDocumento;
                    cr.RemitoProveedor = item.RemitoProveedor;
                    cr.FechaRemProveedor = item.FechaRemProveedor;
                    cr.NomDepDestino = item.NombreDepDestino;
                    cr.IdProducto = item.CodigoItem;
                    cr.NombrePro = item.Descripcion;
                    cr.Marca = item.Marca;
                    cr.Modelo = item.Modelo;
                    cr.PrecioUnit = item.PrecioItem;
                    cr.Cantidad = item.CantidadItem;
                    cr.Seleccionado = 0;
                    lista_rem_prove_pendientes.Add(cr); // lista remitos pendientes
                }
                // cargamos esos datos al datagrid
                dgRemitosProveedor.ItemsSource = lista_rem_prove_pendientes;
                dgRemitosProveedor.DataContext = lista_rem_prove_pendientes;
                //contamos y mostramos la cantidad  de items 
                bdgCItemMante.Badge = lista_rem_prove_pendientes.Count;
            }
        }

        private void btnSelecRemProve_Click(object sender, RoutedEventArgs e)
        {
            //cuando seleccionamos un remito del proveedor 
            ConsultaRemitos r_selec = dgRemitosProveedor.SelectedItem as ConsultaRemitos;
            int _idprove = Convert.ToInt32(txtIdProve.Text);
            if (r_selec.Seleccionado == 1)
            {
                MessageBox.Show("El item ya fue seleccionado. No puede seleccionarse de nuevo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                CompraPDetalle cdp = new CompraPDetalle();
                cdp.IdProducto = r_selec.IdProducto;
                cdp.IdRemito = r_selec.IdDocumento;
                cdp.NomProducto = r_selec.NombrePro;
                cdp.Cantidad = r_selec.Cantidad;
                cdp.FechaRemito = r_selec.FechaRemProveedor;
                cdp.RemitoProveedor = r_selec.RemitoProveedor;
                cdp.PrecioItem = r_selec.PrecioUnit;
                cdp.TotalItem = cdp.Cantidad * cdp.PrecioItem;
                cdp.Deposito = r_selec.NomDepDestino;
                compraPDetalles.Add(cdp);

                lista_rem_prove_pendientes.Where(x => x.IdProducto == r_selec.IdProducto && x.IdDocumento == r_selec.IdDocumento).FirstOrDefault().Seleccionado = 1;

                btnCerrarDrawRight.Command.Execute(Dock.Right);// cerramos el draw
            }

        }
        #endregion

        #region TextBoxes
        private void txtFactura_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFactura.SelectAll();
        }


        private void txtOC_GotFocus(object sender, RoutedEventArgs e)
        {
            txtOC.SelectAll();
        }

        private void txtImporte_GotFocus(object sender, RoutedEventArgs e)
        {
            txtImporte.SelectAll();
        }

        private void txtPrecioUnit_GotFocus(object sender, RoutedEventArgs e)//enfoque de la celda "precio unitario"en la grid detalle
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }

        private void txtPrecioUnit_KeyDown(object sender, KeyEventArgs e)//key down de la celda "precio unitario"en la grid detalle
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab) // si se presiona enter o tab
            {
                int indiceItemABorrar = dgDetalle.SelectedIndex;
                CompraPDetalle cdp = dgDetalle.SelectedItem as CompraPDetalle;
                TextBox box = sender as TextBox; // el objeto sender lo tomamos como un textbox

                //para calular el total del item primero guardamos el valor del textbox de la celda precio unitario
                //en una variable del tipo strin para poder sacarle los simbolos de pesos
                string _pu = box.Text;
                // lueog usando Parse, convertimos al formato adecuado
                decimal _preciounit = decimal.Parse(_pu.Replace("$", ""));
                cdp.PrecioItem = _preciounit;
                //luego debemos multiplicar este valor por el valor del campo cantidad del obejeto seleccionado y asignarlo
                //al campo totalitem del mismos
                decimal _total = _preciounit * cdp.Cantidad;
                cdp.TotalItem = _total;
                compraPDetalles.RemoveAt(indiceItemABorrar); //borramos el item
                compraPDetalles.Insert(indiceItemABorrar, cdp); // lo volvemos a agregar modificado en el lugar original del item

                //como es una observable collection deberia reflejar el cambio en forma inmediata
            }
        }

        private void txtBuscarProve_TextChanged(object sender, TextChangedEventArgs e)
        {
            //contiene la representacion de busqueda para un proveedor 
            proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProve.Text);

            lstResultadoBusquedaProve.ItemsSource = proveedors;
        }
        #endregion

        #region Ventana

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel =false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion



        class ConsultaRemitos
        {
            private DateTime? fechaRemProveedor;

            public string RemitoProveedor { get; set; }
            public DateTime? FechaRemProveedor { get => fechaRemProveedor; set => fechaRemProveedor = value; }
            public string NomDepDestino { get; set; }
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnit { get; set; }
            public string NombrePro { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public int Seleccionado { get; set; }
            public int IdDocumento { get; set; }
        }


        #region MetodosPrivados


        private void GrabarDetalleEnBD()
        {
            // obtenemos el ultimo iddocumento 
            int _ultimoIddocumento = coreProducto.ObtenerUltimoIdCompra();

            foreach (var item in compraPDetalles)
            {
                item.IdCompra = _ultimoIddocumento;

                coreProducto.CompraDetalleAlta(item);

            }
            //ahora debemos actualizar el estado de los remitos no registrados a registrados
            //pero primero debemos verificar si se cumplieron todos los items del remito
            //como se pueden registrar varios remitos a la vez , debemos chekear para cada uno
            //para eso debemos cambiar el estado del item del detalle a valor 12 'Registrado'
            foreach (var item in compraPDetalles)
            {
                coreProducto.ActualizarDetDocuARegistrado(item.IdRemito, item.IdProducto);
            }


        }

        private void GrabarEncabezadoEnBD()
        {
            // grabamos el encabezado de la compra
            regCompra.IdProve = Convert.ToInt32(txtIdProve.Text);
            regCompra.IdUsuario = Contexto.CodUser;
            coreProducto.CompraEncabezadoAlta(regCompra);
        }

        private void ActualizarPreciosProductos()
        {
            MessageBoxResult respuesta = MessageBox.Show("Desea actualizar los precios de los productos?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (respuesta == MessageBoxResult.Yes)
            {
                foreach (var item in compraPDetalles)
                {
                    //actualizamos el precio unitario del producto
                    coreProducto.ActualizarPreciosProductos(item.IdProducto, item.PrecioItem);
                    //actualizamos el costo del stock en funcion del nuevo precio ingresado
                    coreProducto.ActualizarCostoStockUno(item.IdProducto, item.PrecioItem);
                    //grabar en la tabla historial de precios
                    coreProducto.ActualizarHistorialPrecios(item.IdProducto, item.PrecioItem);
                }
            }

        }

        private bool ValidarDatosObligatarios()
        {
            if (dtpFechaFactura.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha para la factura", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                if (string.IsNullOrEmpty(txtFactura.Text))
            {
                MessageBox.Show("Debe indicar un numero de factura ", "Aviso", MessageBoxButton.OK);
                return false;
            }
            else
                    if (string.IsNullOrEmpty(txtImporte.Text))
            {
                MessageBox.Show("Debe indicar un valor del importe", "Aviso", MessageBoxButton.OK);
                return false;
            }

            else
            {

                return true;
            }
        }
        #endregion

        private void txtFactura_LostFocus(object sender, RoutedEventArgs e)
        {
            //debemos verificar que ya existe o no la factura que se intenta ingresar.
            int _idp = Convert.ToInt32(txtIdProve.Text);

            bool validacion = coreRemito.ValidarFacturaProveedor(_idp, txtFactura.Text);
            if (validacion)
            {
                MessageBox.Show("El numero de factura ingresado ya existe como registrado para este proveedor", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

       
    }
}

