using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UIDESK.imprimir;
using System.Linq;
using System.Linq.Expressions;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMDocumento.xaml
    /// </summary>
    public partial class ABMDocumento : Window
    {
        Vehiculo VehiculoPase = new Vehiculo();
        List<Vehiculo> vehiculos = new List<Vehiculo>();
       
        BLLVehiculos BLL = new BLLVehiculos();
        Documento documento = new Documento();

        ObservableCollection<DocumentoDetalle> documentoDetalles = new ObservableCollection<DocumentoDetalle>();
        BLLEmpleados BLLEmpleados = new BLLEmpleados();
        ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();
        BLLObras BLLObras = new BLLObras();
        BLLRemito BLLRemito = new BLLRemito();
        BLLProveedor coreProveedor = new BLLProveedor();
        ObservableCollection<Proveedor> transportes = new ObservableCollection<Proveedor>();

        public ICollectionView vistaEmpleados
        {
            get { return CollectionViewSource.GetDefaultView(empleados); }
        }

        public ICollectionView vistaVehiculos //vista declarada sobre la coleccion de vehiculos
        {
            get { return CollectionViewSource.GetDefaultView(vehiculos); }
        }

        public ABMDocumento()
        {
            InitializeComponent();
            vehiculos = BLL.ListaParaRemision(); // llenamos la coleccion de vehiculo
            var _vhAct = from vh in vehiculos where vh.Estado == "Activo" select vh;
            empleados = BLLEmpleados.EmpleadosActivos();
            txtTipoDocu.Text = "DSV";
            txtConcepto.Text = "ENTREGA";
            dtpFechaRemito.SelectedDate = DateTime.Today;
            transportes = coreProveedor.ProveedorPorRubro(9); // proveedores de transportes
            cmbTransportes.ItemsSource = transportes;
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
            dgConsultaVh.ItemsSource = _vhAct; // la propiedad itemsource de la grilla de seleccion del vehiculo 
            dgConsultaVh.DataContext = _vhAct; // la propiedad datacontex de la grilla de seleccion del vehiculo
            lstResultadoBusquedaEmpleado.ItemsSource = vistaEmpleados; // a propiedad item source de la lista de empleados
            //vistaVehiculos.Filter = new System.Predicate<object>(filtroBuscar);// declaramos un filtro para la vista
            //vistaEmpleados.Filter = new Predicate<object>(filtroBuscarEmpleado);

        }

        //filtro de la vista que nos devuelve los objetos que coinciden con el parametro de busqueda
        private bool filtroBuscarEmpleado(object obj)
        {
            Empleado empleado = obj as Empleado;
            return empleado.Nombre.Contains(txtBuscarEmpleado.Text);
        }

        //filtro de la vista que nos devuelve los objetos que coinciden con los parametros de busqueda
        private bool filtroBuscar(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            return vehiculo.Descripcion.Contains(txtBuscar.Text) || vehiculo.Dominio.Contains(txtBuscar.Text) || vehiculo.Modelo.Contains(txtBuscar.Text);
        }

        #region DrawBotton


        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            //acciones del boton de seleccion del DrawBotton
            Vehiculo vh_selec = dgConsultaVh.SelectedItem as Vehiculo; // creamos un objeto vehiculo desde la seleccion de la grid 

            if (vh_selec != null)
            {


                //asignamos el valor de esas propiedades a un objeto del tipo documentodetalle
                DocumentoDetalle dt = new DocumentoDetalle();
                dt.CodigoItem = vh_selec.IdVh;
                dt.Descripcion = vh_selec.Descripcion;
                dt.CodIventario = vh_selec.Dominio;

                dt.Marca = vh_selec.NomMarca;
                dt.Modelo = vh_selec.Modelo;
                dt.CantidadItem = 1;
                dt.TipoItem = 2;

                //agregamos el objeto documentodetalle a la coleccion de esos objetos
                documentoDetalles.Add(dt);
                //refrescamos la lista de detalle  algo importante para el remito
                dgDetalleDocu.ItemsSource = documentoDetalles;
                dgDetalleDocu.DataContext = documentoDetalles;
                btnCerrarDrawBotton.Command.Execute(Dock.Bottom);// cerramos el draw
            }
        }

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            vistaVehiculos.Filter = filtroBuscar; // cada vez que cambia el contenido de la propiedad text , se aplica el filtro a la vista
        }

        private void BtnAddDel_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
            indiceItemABorrar = dgDetalleDocu.SelectedIndex; //tomamos su indice en la lista detalle
            documentoDetalles.RemoveAt(indiceItemABorrar); // lo removemos de la misma
            //como es una observable collection el cambio se refleja de manera inmediata
            dgDetalleDocu.ItemsSource = documentoDetalles;
            dgDetalleDocu.DataContext = documentoDetalles;
        }

        #endregion  

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Desea Cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //en este lugar debemos hacer algunas cosas

            //1 )grabar el encabezado
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
            if (cmbTransportes.SelectedItem == null)
            {
                MessageBox.Show("El dato del transporte no es valido o falta", "Aviso", MessageBoxButton.OK);
                return;
            }
            documento.NumDocumento = txtNumDocu.Text;

            documento.IdTipoRem = 2;
            documento.Alta = DateTime.Today;
            documento.Concepto = txtConcepto.Text;
            documento.Imputacion = Convert.ToInt32(txtImputacion.Text);
            documento.IdEstado = 1;
            Proveedor proveedortransporte = cmbTransportes.SelectedItem as Proveedor;
            documento.IdTransporte = proveedortransporte.IdProve; // proyeccion electroluz
            documento.IdEmpleado = Convert.ToInt32(txtDestinatario.Text);
            documento.IdUsuario = Contexto.CodUser;
            documento.Chofer = txtChofer.Text;
            documento.NotaRemito = txtNotaRemito.Text;
            documento.FechaRemito = dtpFechaRemito.SelectedDate.Value.Date;
            // llenado de campos no necesarios para este documento
            documento.RemitoProveedor = "";
            documento.FechaRemProveedor = null;
            documento.NumFacturaProveedor = "";
            documento.ImporteFacturaProveedor = 0;
            documento.FechaFacProveedor = null;
            documento.NumeroOc = "";
            documento.Registrado = 0;
            documento.IdCasa = 0;
            documento.IdDeposito = 0;
            documento.IdDepDestino = 0;
            documento.IdCausaDescuento = 0;
            documento.Aclaraciones = "";
            documento.AutorizacionVale = "";
            documento.RemDepOrigen = "";
            documento.RetiraPersonal = 0;
            //--grabar el encabezado
            BLLRemito.GrabarEncabezado(documento);

            //2) grabar el detalle
            // obtenemos el ultimo iddocumento 
            int _ultimoIddocumento = BLLRemito.ObtenerUltimoIdDocumento();

            foreach (var item in documentoDetalles)
            {
                item.IdDocumento = _ultimoIddocumento;

                BLLRemito.GrabarDetalle(item);

            }
            // 3) cambiar la asigancion del vehiculo para eso recorremos nuevamente la lista de documentoDetalles
            foreach (var item in documentoDetalles)
            {
                bool existeAsg = BLL.ExisteAsignacionActiva(item.CodigoItem);
                if (existeAsg == true)
                {
                    //buscamos la ultima asignacion
                    int _ultimaAsig = BLL.BuscarAsignacionActiva(item.CodigoItem);
                    //finalizamos la asignacion actual del vehiculo

                    BLL.BajaAsignacion(_ultimaAsig, dtpFechaRemito.SelectedDate.Value.Date);

                }
                //una vez finalizada la asignacion actual del vehiculo, creamos una nueva
                //buscamos los datos del vehiculo porque lo vamos a necesitar
                Vehiculo vehiculo = BLL.BuscarPorId(item.CodigoItem);
                Asignacion_vh asignacion_ = new Asignacion_vh();
                asignacion_.AltaF = DateTime.Today;
                asignacion_.Imputacion = Convert.ToInt32(txtImputacion.Text);
                asignacion_.IdVh = item.CodigoItem;
                asignacion_.FechaIn = dtpFechaRemito.SelectedDate.Value.Date;
                asignacion_.EstadoAsignacion = "Activa";
                asignacion_.SituacionAsignacion = "En Curso";
                asignacion_.DiasAcumulados = 0;
                asignacion_.CostoAsignacion = 0;
                asignacion_.KmAcuObra = 0;
                asignacion_.HsAcuObra = 0;
                asignacion_.IdEmpleado = Convert.ToInt32(txtDestinatario.Text);
                asignacion_.IdCatevh = vehiculo.IdCate;
                int numero = BLL.AltaAsignacion(asignacion_);
                //cambiamos la situacion fisica del vehiculo segun sea la imputacion
                if (asignacion_.Imputacion == 1)
                {
                    //si la imputcion es =1 , es decir deposito herramientas , la situacion cambia a 2, En Deposito

                    BLL.CambioSF(vehiculo.IdVh, 2); // 
                }
                else
                {
                    if (asignacion_.Imputacion == 13)
                    {
                        // si la imputacion es al deposito de mantenimiento
                        BLL.CambioSF(vehiculo.IdVh, 3);
                    }
                    else
                    {
                        if (asignacion_.Imputacion == 17)
                        { //si la imputacion es una venta
                            BLL.CambioSF(vehiculo.IdVh, 5);
                        }
                        else
                        {
                            //si la imputacion es otra , entonces la situacion cambia a 1, "en obra"
                            BLL.CambioSF(vehiculo.IdVh, 1);
                        }
                    }

                }
            }

            //4 )imprimir el remito
            MessageBoxResult result = MessageBox.Show("El registro se grabo con exito. Desea Imprimir el remito?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // llamamos a la ventana de imprimir documento
                PrintRemitoVH printRemitoObra = new PrintRemitoVH(_ultimoIddocumento);
                if (printRemitoObra.ShowDialog() == true)
                {
                    MessageBox.Show("Se imprimio el remito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //ImprimirRemitoVh imprimir = new ImprimirRemitoVh(_ultimoIddocumento);
                //imprimir.Show();

            }
            this.Close();

        }

        #region Drawleft


        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = lstResultadoBusquedaEmpleado.SelectedItem as Empleado;
            txtDestinatario.Text = Convert.ToString(empleado.IdEmpleado);
            txbNombreDestinatario.Text = empleado.Nombre;
            btnCerrarDrawBotton.Command.Execute(Dock.Left);
        }


        private void TxtBuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            vistaEmpleados.Filter = filtroBuscarEmpleado; //cada vez que cambia el contenido de la propiedad text, se aplica el filtro a la vista
        }
        #endregion

        private void TxtImputacion_KeyDown(object sender, KeyEventArgs e)
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
                    //txtTransporte.Text = "PROYECCION ELECTROLUZ SRL";
                }
                else
                {

                    MessageBox.Show("La obra no existe en el sistema", "Aviso", MessageBoxButton.OK);


                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscarVh_Click(object sender, RoutedEventArgs e)
        {
            vistaVehiculos.Filter = filtroBuscar;
            dgConsultaVh.ItemsSource = vistaVehiculos;
            dgConsultaVh.DataContext = vistaVehiculos;
        }
    }
}
