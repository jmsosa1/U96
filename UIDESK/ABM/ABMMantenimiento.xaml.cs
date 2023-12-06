using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMMantenimiento.xaml
    /// </summary>
    public partial class ABMMantenimiento : MaterialWindow 
    {
        #region Declarativas


        BLLProveedor bLLProveedor = new BLLProveedor();
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        BLLObras bllObras = new BLLObras();
        BLLGestion gestion = new BLLGestion();
        List<Proveedor> proveedors = new List<Proveedor>();
        List<CategoriaManteVh> categoriaMantes = new List<CategoriaManteVh>();
        ObservableCollection<DetManteVh> detManteVhs = new ObservableCollection<DetManteVh>();
        Vehiculo vehiculo = new Vehiculo();
        Proveedor proveedor = new Proveedor();
        Obra obramante = new Obra();
        Mante_vh nuevoMante = new Mante_vh();
        // lista de items de una otm cumplidos pero no registrados
        ObservableCollection<OtmDetalle> otmDetallesNR = new ObservableCollection<OtmDetalle>();

        #endregion  


        public ABMMantenimiento() //constructor de clase
        {
            InitializeComponent();

            //carga de datos de las categorias de mantenimientos
            categoriaMantes = bLLVehiculos.ListarCateMante();
            cmbCateMante.ItemsSource = categoriaMantes;
            //setup de grid que muestra el detalle del mantenimiento
            dgDetalle.DataContext = detManteVhs;
            dgDetalle.ItemsSource = detManteVhs;

            DetManteVh detMante = new DetManteVh();
            grdItemDetalle.DataContext = detMante; // grid carga item del detalle drawercontent bottom
            grdPrincipal.DataContext = nuevoMante; // grid con datos del encabezado




        }

        #region TextBoxes



        //cuadros de texto de entrad de datos recibiendo el focus 
        private void txtDescriMante_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescriMante.SelectAll();
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCantidad.SelectAll();
        }

        private void txtImporteItem_GotFocus(object sender, RoutedEventArgs e)
        {
            txtImporteItem.SelectAll();
        }

        private void txtFactura_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFactura.SelectAll();
        }

        private void txtRemito_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRemito.SelectAll();
        }

        private void txtOC_GotFocus(object sender, RoutedEventArgs e)
        {
            txtOC.SelectAll();
        }

        private void txtImporte_GotFocus(object sender, RoutedEventArgs e)
        {
            txtImporte.SelectAll();
        }

        private void txtKm_GotFocus(object sender, RoutedEventArgs e)
        {
            txtKm.SelectAll();
        }

        private void txtHs_GotFocus(object sender, RoutedEventArgs e)
        {
            txtHs.SelectAll();
        }

        private void txtIdProve_GotFocus(object sender, RoutedEventArgs e)
        {
            txtIdProve.SelectAll();
        }

        private void txtImputacion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtImputacion.Text.Length != 0)
            {
                int _imp = Convert.ToInt32(txtImputacion.Text);
                obramante = bllObras.BuscarImputacion(_imp);
                txbNombreObra.Text = obramante.NombreObra;
                txbNombreCliente.Text = obramante.Cliente;
            }
            else
            {

            }
        }



        private void txtDominio_LostFocus(object sender, RoutedEventArgs e)
        {
            bool existeDomnio = false;
            if (txtDominio.Text.Length != 0)
            {
                //validamos primero el dominio
                existeDomnio = bLLVehiculos.ValidarDominio(txtDominio.Text);

                if (existeDomnio)
                {
                    vehiculo = bLLVehiculos.VehiculoBuscarUnDominio(txtDominio.Text);
                    //comprobamos que el vehiculo este activo
                    if (vehiculo.IdSf==8)
                    {
                        MessageBox.Show("El vehiculo esta inactivo.Debe indicar otro dominio", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        // si el domino es activo entonces proseguimos
                        txbMarca.Text = vehiculo.NomMarca;
                        txbModelo.Text = vehiculo.Modelo;

                        // una vez que el dominio existe , debemos verifica que exista algun mantenimiento
                        //programado que este cumplido para ese vehiculo
                        int _cantItems = gestion.OtmItemsCumplidosNoRegistrados(vehiculo.IdVh);

                        if (_cantItems != 0)
                        {
                            bdgCItemMante.Badge = _cantItems;
                            btnMantePlanificado.Background = Brushes.Red;
                            //ahora cargamos la colleccion de items cumplidos y no registrados
                            otmDetallesNR = gestion.DetalleOTM_Cumplido_NR(vehiculo.IdVh);
                            dgPlanificacion.ItemsSource = otmDetallesNR;
                            dgPlanificacion.DataContext = otmDetallesNR;
                        }
                    }
                   

                }
                else
                {
                    MessageBox.Show("El vehiculo no existe", "Aviso", MessageBoxButton.OK);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Debe ingresar un dominio", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void txtImputacion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtImputacion.SelectAll();
        }

        #endregion

        #region MetodosPrivados


        //metodo privado para armar el encabezado de los datos del mantenimiento
        //registrado
        private Mante_vh ArmarEncabezadoMante()
        {
            Mante_vh mante_Vh = new Mante_vh();
            mante_Vh.IdVh = vehiculo.IdVh;
            mante_Vh.Dominio = txtDominio.Text;
            mante_Vh.IdProve = Convert.ToInt32(txtIdProve.Text);
            mante_Vh.HorasMante = string.IsNullOrEmpty(txtHs.Text) ? 0 : Convert.ToInt32(txtHs.Text);
            mante_Vh.KmMante = string.IsNullOrEmpty(txtKm.Text) ? 0 : Convert.ToInt32(txtKm.Text);
            mante_Vh.AltaF = DateTime.Today.Date;
            mante_Vh.Imputacion = obramante.Imputacion;
            mante_Vh.NumFactura = string.IsNullOrEmpty(txtFactura.Text) ? "no indica" : txtFactura.Text;
            mante_Vh.NumRemito = string.IsNullOrEmpty(txtRemito.Text) ? "no indica" : txtRemito.Text;
            mante_Vh.OrdenCompra = string.IsNullOrEmpty(txtOC.Text) ? "no indica" : txtOC.Text;

            mante_Vh.TipoMante = ((ComboBoxItem)cmbTipoMante.SelectedItem).Content.ToString();
            if (dtpFechaFactura.SelectedDate != null)
            {
                mante_Vh.FechaFac = dtpFechaFactura.SelectedDate.Value.Date;
            }
            else
            {
                mante_Vh.FechaFac = null;
            }

            if (dtpFechaRemito.SelectedDate != null)
            {
                mante_Vh.FechaRem = dtpFechaRemito.SelectedDate.Value.Date;
            }
            else
            {
                mante_Vh.FechaRem = null;
            }
            mante_Vh.IdEmpleado = 0;
            string _importeMante = txtImporte.Text;
            mante_Vh.Importe = decimal.Parse(_importeMante.Replace("$", ""));

            return mante_Vh;
        }
        #endregion

        #region Botones
        // evento para guardar un registro de mantenimiento
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // para guardar primero debo guardar el encabezado del mantenimiento
            //en un objeto de la clase Mante_VH
            if (cmbTipoMante.SelectedItem == null)
            {
                MessageBox.Show("Debe Seleccionar un tipo de mantenimiento", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dgDetalle.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar un item al detalle", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpFechaFactura.SelectedDate == null)
            {
                MessageBox.Show("Debes indicar una fecha de factura", "Aviso", MessageBoxButton.OK);
                return;
            }

            Mante_vh mante_Vh = new Mante_vh();
            mante_Vh = ArmarEncabezadoMante();
            int fila = bLLVehiculos.AltaMantenimiento(mante_Vh);

            //obtenemos el ultimo id de mantenimiento para poder grabar el detalle
            Mante_vh ultimo = new Mante_vh();
            ultimo = bLLVehiculos.ObtenerUltimoIdMante();
            //grabamos el detalle
            foreach (var item in detManteVhs)
            {
                item.IdManteVh = ultimo.IdManteVh;

                bLLVehiculos.AltaDetaMante(item);


            }

            //aca debemos recorrer nuevamente la lista de items del mantenimiento para actualizar el estado en la programacion del mismo

            foreach (var item in detManteVhs)
            {
                OtmDetalle detalle = new OtmDetalle();
                detalle.IdProve = item.IdProve;
                detalle.OcProve = txtOC.Text;
                detalle.FacturaProve = txtFactura.Text;
                detalle.RemitoProve = txtRemito.Text;
                detalle.IdOtm = item.Otm;
                detalle.NumItem = item.ItemOtm;
                // aca viene la llamada al procedure que actualiza el estado del item de detalle a registrado y la informacion del proveedor
                gestion.OtmRegistrarUnItemDetalle(detalle);
            }
            if (fila != 0)
            {
                MessageBox.Show("Se dio de alta el mantenimiento", "Aviso", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("NO se pudo grabar el mantenimiento", "Aviso", MessageBoxButton.OK);
            }
            DialogResult = true;
            
        }

        //agregar el item del mantenimiento al detalle del formulario
        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            DetManteVh detMante = new DetManteVh();
            CategoriaManteVh categoriaManteVh = new CategoriaManteVh();
            categoriaManteVh = cmbCateMante.SelectedItem as CategoriaManteVh;

            detMante.IdCateMante = categoriaManteVh.IdCateMante;
            detMante.DescriMante = txtDescriMante.Text;
            detMante.Cantidad = Convert.ToInt16(txtCantidad.Text);
            detMante.NomCateMante = categoriaManteVh.NomCate;
            string _costoitem = txtImporteItem.Text;
            detMante.CostoItem = decimal.Parse(_costoitem.Replace("$", ""));
            detManteVhs.Add(detMante);

            btnCerrarDrawBotton.Command.Execute(Dock.Bottom);
        }


        //quitar un item seleccionado del detalle del formulario
        private void btnQuitarItem_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar; //item que queremos borrar de la lista
            indiceItemABorrar = dgDetalle.SelectedIndex; //tomamos su indice en la lista detalle
            detManteVhs.RemoveAt(indiceItemABorrar); // lo removemos de la misma
                                                     //como es una observable collection el cambio se refleja de manera inmediata


        }

        //boton para seleccionar del cuadro de seleccion del proveedor (drawer izquierdo)
        private void btnSeleccionarDrawLeft_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto desde la seleccion en la lista 
            Proveedor p = lstResultadoBusquedaProve.SelectedItem as Proveedor;
            if (p != null)
            {


                //pasamos los datos dle objeto a los cuadros de texto determinados
                txtIdProve.Text = p.IdProve.ToString();
                txbRazonSocial.Text = p.RazonSocial;
                //cerramos el drawer de seleccion(izquierdo)
                btnCerrarDrawLeft.Command.Execute(Dock.Left);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        // evento seleccionar un item del grid con items programados
        private void BtnSeleccionarDrawRight_Click(object sender, RoutedEventArgs e)
        {
            //creamos un objeto del item seleccionado.
            OtmDetalle itemplanificado = dgPlanificacion.SelectedItem as OtmDetalle;

            if (itemplanificado != null)
            {
                //aca lo pasamos a lista de detalle del mantenimientos
                DetManteVh detMante = new DetManteVh();
                detMante.DescriMante = itemplanificado.DescripcionItem;
                detMante.IdCateMante = itemplanificado.IdCateMante;
                detMante.NomCateMante = itemplanificado.NomCateMante;
                detMante.Otm = itemplanificado.IdOtm;
                detMante.ItemOtm = itemplanificado.NumItem;
                detMante.CostoItem = 0;
                detManteVhs.Add(detMante);
                btnCerrarDrawRight.Command.Execute(Dock.Right);

            }
            else
            {
                MessageBox.Show("Debe seleccionar un item", "Aviso", MessageBoxButton.OK);
                return;

            }

        }


        //evento buscar un proveedor en panel de busqueda del proveedor
        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            proveedors = bLLProveedor.ProveedorCombobox(txtBuscarProve.Text);

            lstResultadoBusquedaProve.ItemsSource = proveedors;
        }

        #endregion

        #region Ventana
        //mover la ventana
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //cerrar la ventana 
       

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
