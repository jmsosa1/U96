using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucVehiculosGeneral.xaml
    /// </summary>
    public partial class ucVehiculosGeneral : UserControl
    {
        ObservableCollection<Vehiculo> colvehiculos = new ObservableCollection<Vehiculo>(); //coleccion para el datagrid
        List<CategoriaVh> categoriaVh = new List<CategoriaVh>(); //coleccion para las categorias
        BLLVehiculos bllVehiculos = new BLLVehiculos(); // clase con los metodos lectura /escritura de la capa de negocios
        CultureInfo ci = new CultureInfo("es-AR");
        ObservableCollection<plan_inspeccion> plan_Inspeccions = new ObservableCollection<plan_inspeccion>();

        public ICollectionView vistaVehiculos
        {
            get { return CollectionViewSource.GetDefaultView(colvehiculos); }
        }



        //constructor de clase 
        public ucVehiculosGeneral()
        {
            InitializeComponent();
            //rdbDominio.IsChecked = true;
            colvehiculos = bllVehiculos.VehiculosListarActivos(); //cargamos la coleccion del data grid
            categoriaVh = bllVehiculos.VehiculosListarCategoria(); //cargamos la coleccion para las categorias
            vistaVehiculos.Filter = new Predicate<object>(filtroBuscar);
            vistaVehiculos.Filter = new Predicate<object>(filtroCategoria);
            vistaVehiculos.Filter = new Predicate<object>(filtroDescripcion);
            
           
            cmbCategoria.ItemsSource = categoriaVh; // set items source del combobox que contiene la coleccion categoria
            vistaVehiculos.Filter = filtroActivos;
            dgVhGeneral.DataContext = vistaVehiculos; //  set datacontex del datagrid principal
            dgVhGeneral.ItemsSource = vistaVehiculos; // set itemsource del datagrid principal
            CalcularResultados();


        }
        #region Filtros
        private bool filtroDescripcion(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            return vehiculo.Descripcion.Contains(txtBuscar.Text);
        }

        private bool filtroCategoria(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            if (cmbCategoria.SelectedItem != null)
            {


                CategoriaVh categoria = cmbCategoria.SelectedItem as CategoriaVh;

                return vehiculo.NomCategoria.Contains(categoria.NomCate);
            }
            else
            {
                return false;
            }
        }

        private bool filtroBuscar(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            return vehiculo.Dominio.Contains(txtBuscar.Text) || vehiculo.Descripcion.Contains(txtBuscar.Text)
                || vehiculo.NomMarca.Contains(txtBuscar.Text) || vehiculo.Modelo.Contains(txtBuscar.Text);

        }

        private bool filtroBajas(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            return vehiculo.Estado == "Inactivo";
        }

        private bool filtroActivos(object obj)
        {
            Vehiculo vehiculo = obj as Vehiculo;
            return vehiculo.Estado == "Activo";
        }
        #endregion



        private void CalcularResultados()
        {
            Vehiculo v = new Vehiculo();

            decimal _akm = 0; // acumuludor de km
            decimal _ahs = 0; // acumulador de horas
            int _ct = 0; // cantidad de registros
            decimal _valordoll = 0; // valor de la flota en dolares

            foreach (var item in vistaVehiculos)
            {
                v = item as Vehiculo;
                _ahs = _ahs + v.HorasAcumuladas;
                _akm = _akm + (v.KmAcumulado - v.KmInicial);
                _valordoll = _valordoll + v.CostoReposicionDls;
                _ct = _ct + 1;

            }

            txtRegistros.Text = _ct.ToString();
            txtTotalHsAcu.Text = _ahs.ToString("N", ci);
            txtTotalKmAcu.Text = _akm.ToString("N", ci);
            txtTotalDolares.Text = _valordoll.ToString("N", ci);

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // codigo para agregar un nuevo vehiculo a la base de datos
            //
            Vehiculo vhNulo = new Vehiculo(); // nueva instacia de un objeto vehiculo

            ABMVehiculo aBMVehiculo = new ABMVehiculo(vhNulo); // nueva instancia de un formulario ABM
            //aBMVehiculo.txbOperacion.Text = "Alta de Vehiculo";
            aBMVehiculo._codigoABM = "A";

            if (aBMVehiculo.ShowDialog() == true) // mostramos el formulario y esperamos el valor del metodo Showdialog
            {
                // si el valor devuelto de metodo es true
                MessageBox.Show("EL registro se grabo con exito", "Aviso", MessageBoxButton.OK);
                colvehiculos = bllVehiculos.VehiculosListarActivos(); // cargamos nuevamente la coleccion para el datagrid
                dgVhGeneral.ItemsSource = colvehiculos; // refrescamos el datagrid set itemsource con los nuevos datos
            }


        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            // codigo para cerrar el detalle del registro seleccionado 
            //lo hacemos set la propiedad selectedindex a -1 (no hay objeto seleccionado)
            dgVhGeneral.SelectedIndex = -1;
        }

        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //codigo para mostrar los vehiculos perteneciente a una categoria seleccionada
            //CategoriaVh categoriaVh = new CategoriaVh(); //nueva instancia de un objeto categoria
            //categoriaVh = cmbCategoria.SelectedItem as CategoriaVh; // asociamos el elemento seleccionado del combobox a el 
            //objeto categoria creado anteriormente

            //colvehiculos = bllVehiculos.VehiculosListarPorCategorias(categoriaVh.IdCateVh);//cargamos la coleccion para el datagrid
            //con los resultados de la selecion de la categoria

            // dgVhGeneral.ItemsSource = colvehiculos;// refrescamos el grid

            vistaVehiculos.Filter = filtroCategoria;
            CalcularResultados();
        }

        private void chkFiltroCategoria_Checked(object sender, RoutedEventArgs e)
        {
            cmbCategoria.IsEnabled = true;  // habilitamos la seleccion de categoria de vehiculo
        }

        private void chkFiltroCategoria_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbCategoria.IsEnabled = false; // inabilitamos la seleccion de categoria de vehiculos
            colvehiculos = bllVehiculos.VehiculosListarActivos(); // seteamosy refrescamos el grid 
            dgVhGeneral.ItemsSource = colvehiculos;


        }

        private void btnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            //codigo para poder modificar los datos de un vehiculo seleccionado directamente en el grid
            //Vehiculo modiVehiculo = new Vehiculo();

            if (dgVhGeneral.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                Vehiculo seleVehiculo = dgVhGeneral.SelectedItem as Vehiculo;

                ABMVehiculo vhModi = new ABMVehiculo(seleVehiculo);
                //vhModi.txbOperacion.Text = "Modificar Datos del Vehiculo";
                //modiVehiculo = bllVehiculos.BuscarPorId(seleVehiculo.IdVh);
                vhModi._codigoABM = "M";


                if (vhModi.ShowDialog() == true)
                {
                    MessageBox.Show("EL registro se actualizo con exito", "Aviso", MessageBoxButton.OK);
                    colvehiculos = bllVehiculos.VehiculosListarActivos();
                    dgVhGeneral.ItemsSource = colvehiculos;
                }

            }


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //codigo para eliminar un vehiculo de la base de datos

            if (dgVhGeneral.SelectedItem == null) // si no se selecciona ningun vehiculo se lanza un aviso
            {
                MessageBox.Show("Debe seleccionar un registro", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                // 
                Vehiculo vhBorrar = dgVhGeneral.SelectedItem as Vehiculo;
                string _dominio = vhBorrar.Dominio;
                int _idvh = vhBorrar.IdVh;
                //antes de eliminar un vehiculo hay que comprobar que no exista en la tabla consumos de combustibles
                // y tambien que no exista en la tabla asignaciones y que tampoco exista en la tabla balance-obra-vehiculos
                //y que no tenga mantenimientos asociados
                // validamos tood eso entonces
                bool pase = bllVehiculos.ValidarBorrado(_idvh);
                if (pase == false)
                {
                    MessageBox.Show("No se puede borrar el vehiculo porque tiene otros registros asociados", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBoxResult resultado;
                    resultado = MessageBox.Show("Desea eliminar el dominio:" + _dominio + "", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                    if (resultado == MessageBoxResult.OK)
                    {
                        //aca llamamos al procedure que borra el registro
                        int filaDel = bllVehiculos.VehiculoBorrarUno(_idvh);
                        //refrescamos el listado
                        if (filaDel == 1)
                        {
                            MessageBox.Show("El registro fue eliminado", "Aviso", MessageBoxButton.OK);

                            colvehiculos = bllVehiculos.VehiculosListarActivos();
                            dgVhGeneral.ItemsSource = colvehiculos;
                        }

                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                vistaVehiculos.Filter = null;
                vistaVehiculos.Filter = filtroBuscar;
            }
            else
            {
                colvehiculos = bllVehiculos.VehiculosListarActivos(); // cargamos nuevamente la coleccion para el datagrid
                vistaVehiculos.Filter = filtroActivos;
                dgVhGeneral.ItemsSource = vistaVehiculos; // refrescamos el datagrid set itemsource con los nuevos datos
            }

            CalcularResultados();



        }


        private void btnConsumoHs_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo == null || vehiculo.IdSf==8)
            {
                MessageBox.Show("Debe seleccionar un registro / el vehiculo esta dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                RegistrarConsumo registrarConsumo = new RegistrarConsumo();
                registrarConsumo.txtDominio.Text = vehiculo.Dominio;
                registrarConsumo.txbMarca.Text = vehiculo.NomMarca;
                registrarConsumo.txbModelo.Text = vehiculo.Modelo;
                //registrarConsumo.txbOperacion.Text = registrarConsumo.txbOperacion.Text + " Horas";
                registrarConsumo._tipoConsumo = "HS";
                if (registrarConsumo.ShowDialog() == true)
                {
                    MessageBox.Show("Se registro con exito el consumo", "Aviso", MessageBoxButton.OK);
                    colvehiculos = bllVehiculos.VehiculosListarActivos();
                    dgVhGeneral.ItemsSource = colvehiculos;
                    //actualizamos el estado de las tareas del vehiculo
                    ActualizarPlanInspeccionDelVehiculo(vehiculo.IdVh);

                }
            }

           


        }

        private void btnConsumoKm_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo == null || vehiculo.IdSf==8)
            {
                MessageBox.Show("Debe seleccionar un vehiculo / vehiculo dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                RegistrarConsumo registrarConsumo = new RegistrarConsumo();
                registrarConsumo.txtDominio.Text = vehiculo.Dominio;
                //registrarConsumo.txbOperacion.Text = registrarConsumo.txbOperacion.Text + " Kilometros";
                registrarConsumo.txbMarca.Text = vehiculo.NomMarca;
                registrarConsumo.txbModelo.Text = vehiculo.Modelo;
                registrarConsumo._tipoConsumo = "KM";
                if (registrarConsumo.ShowDialog() == true)
                {
                    MessageBox.Show("Se registro con exito el consumo", "Aviso", MessageBoxButton.OK);
                    colvehiculos = bllVehiculos.VehiculosListarActivos();
                    dgVhGeneral.ItemsSource = colvehiculos;
                    // actualizamos el estado de las tareas del vehiculo
                    ActualizarPlanInspeccionDelVehiculo(vehiculo.IdVh);
                }
            }

           

        }

        private void ActualizarPlanInspeccionDelVehiculo(int _idvh)
        {
            //este metodo revisa las tareas activas y luego cambia los estado dependiendo del gap de la tarea
            //verificar si es necesario enviar una alerta, para eso volvemos a llenar la lista con el nuevo estado de las tareas
            // llamamos a los procedimientos adecuados
            byte[] img_porvencer = File.ReadAllBytes(@"C:\SAHmv6\imagenes\clock-alert.png"); // guardamos imagenes para actualizar el estado por vencer
            byte[] img_vencido = File.ReadAllBytes(@"C:\SAHmv6\imagenes\clock-check.png");// imagen para estado vencido
            bllVehiculos.PlansInspeccionCalcularAlarma(_idvh);// verificamos si la tarea esta en zona de alarma
            bllVehiculos.PlanInspeccionCalcularVencimiento(_idvh); // verificamos si la tarea esta vencida
            bllVehiculos.PlanInspeccionCambiarImagenPorVencer(_idvh, img_porvencer); // si la tarea esta en zona de alarma, cambiamos su imagen
            bllVehiculos.PlanInspeccionCambiarImagenVencido(_idvh, img_vencido); // si la tarea esta vencida cambiamos su imagen
        }

        private void btnAsignarObra_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo == null || vehiculo.IdSf==8)
            {
                MessageBox.Show("Debe seleccionar un vehiculo / vehiculo dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                //antes de generar una nueva asignacion , se debe verificar que no tenga una asignacion activa
                bool existeAsg = bllVehiculos.ExisteAsignacionActiva(vehiculo.IdVh);

                if (existeAsg == true)
                {
                    MessageBox.Show("El vehiculo ya tiene una asignacion activa. No se puede generar una nueva", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                ABMAsignacion aBMAsignacion = new ABMAsignacion(vehiculo.IdVh);
                //aBMAsignacion.txbOperacion.Text = "Alta de asignacion de vehiculo a obra";
                aBMAsignacion.txtDominio.Text = vehiculo.Dominio;
                aBMAsignacion.txbModelo.Text = vehiculo.Modelo;
                aBMAsignacion.txbMarca.Text = vehiculo.NomMarca;

                if (aBMAsignacion.ShowDialog() == true)
                {
                    MessageBox.Show("Se registro con exito la asignacion", "Aviso", MessageBoxButton.OK);

                }
            }
           
        }

        private void btnAutorizar_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo == null || vehiculo.IdSf==8)
            {
                MessageBox.Show("Debe seleccionar un vehiculo / vehiculo dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                ABMAutorizacion nuevaAutorizacion = new ABMAutorizacion();
                //nuevaAutorizacion.txbOperacion.Text = "Alta de Autorizacion de manejo de vehiculo";
                nuevaAutorizacion.txtDominio.Text = vehiculo.Dominio;
                nuevaAutorizacion.txbModelo.Text = vehiculo.Modelo;
                nuevaAutorizacion.txbMarca.Text = vehiculo.NomMarca;
                nuevaAutorizacion.txtIdVh.Text = Convert.ToString(vehiculo.IdVh);
                if (nuevaAutorizacion.ShowDialog() == true)
                {
                    MessageBox.Show("Se registro con exito la autorizacion", "Aviso", MessageBoxButton.OK);
                }
            }

          
        }

        private void dgVhGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo != null )
            {


                ucDetalleFilaVehiculoGral._idvh = vehiculo.IdVh;
            }
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            //codigo para dar de baja un vehiculo. Lo que se hace es cambiar el estado y la situacion fisica del mismo
            //antes de dar de baja, debe verificarse que no tenga una asignacion activa
            Vehiculo vehiculo = dgVhGeneral.SelectedItem as Vehiculo;

            if (vehiculo == null || vehiculo.IdSf == 8)
            {
                MessageBox.Show("Debe seleccionar un vehiculo / vehiculo dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                if (vehiculo != null)
                {
                    MessageBoxResult result = MessageBox.Show("Desea dar de baja el vehiculo? " + vehiculo.Dominio + " " + vehiculo.Descripcion + " "
                        + vehiculo.NomMarca + " " + vehiculo.Modelo + " ", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //si se decide por la afirmativa deberiamos preguntar por la causa de la baja
                    //venta, fin de vida util , sin reparacion posible ,destruccion total,
                    SelecCausaBaja selecCausa = new SelecCausaBaja(vehiculo);
                    if (selecCausa.ShowDialog() == true)
                    {
                        MessageBox.Show("Se dio de baja el vehiculo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        colvehiculos = bllVehiculos.VehiculosListarActivos();
                        dgVhGeneral.DataContext = colvehiculos; //  set datacontex del datagrid principal
                        dgVhGeneral.ItemsSource = colvehiculos;
                    }

                }
            }

           
           

        }

        private void btnAddDocumentacion_Click(object sender, RoutedEventArgs e)
        {


            ABMDocVh nuevaDocvh = new ABMDocVh();
            //nuevaDocvh.txbOperacion.Text = "Nueva Documentacion del Vehiculo";
            nuevaDocvh.btnOperacion.Content = "Guardar";
            if (nuevaDocvh.ShowDialog() == true)
            {
                MessageBox.Show("Se agego la nueva documentacion", "Aviso", MessageBoxButton.OK);
                //colvehiculos = bllVehiculos.VehiculosListarActivos();
                //dgVhGeneral.ItemsSource = colvehiculos;
            }
        }

        private void BtnOtmNueva_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo = dgVhGeneral.SelectedItem as Vehiculo;
            if (vehiculo == null || vehiculo.IdSf == 8)
            {
                MessageBox.Show("No hay vehiculo seleccionado o el  vehiculo esta dado de baja", "Aviso", MessageBoxButton.OK);
                return;
            }
            else
            {
                if (vehiculo != null)
                {


                    Otm otm = new Otm();
                    OtmDetalle otmDetalle = new OtmDetalle();
                    otm.Idvh = vehiculo.IdVh;
                    otm.Dominio = vehiculo.Dominio;
                    otm.LecturaHs = Convert.ToInt32(vehiculo.HorasAcumuladas);
                    otm.LecturaKm = vehiculo.KmAcumulado;
                    otm.MarcaVh = vehiculo.NomMarca;
                    otm.ModeloVh = vehiculo.Modelo;
                    otm.DescriVh = vehiculo.Descripcion;
                    otm.Alta = DateTime.Today;
                    otm.Tipo_Otm = 1;
                    otm.Idproducto = 0;
                    otm.IdPlanInspeccion = 0;
                    otm.Estado_Otm = "Pendiente";

                    ABMOtm aBMOtm = new ABMOtm(otm, otmDetalle);
                    aBMOtm.txtDominio.Text = vehiculo.Dominio;
                    //aBMOtm.txbOperacion.Text = "Agregar una nueva Orden de Trabajo para Mantenimiento";

                    if (aBMOtm.ShowDialog() == true)
                    {
                        MessageBox.Show("Se dio de alta la OTM de vehiculo", "Aviso", MessageBoxButton.OK);


                    }
                }
            }

          
        }



        /* private void RdbDescripcion_Checked(object sender, RoutedEventArgs e)
         {
             MaterialDesignThemes.Wpf.HintAssist.SetHint(txtBuscar, "Buscar por Descripcion");
         }

         private void RdbDominio_Checked(object sender, RoutedEventArgs e)
         {
             MaterialDesignThemes.Wpf.HintAssist.SetHint(txtBuscar, "Buscar por Dominio");
         }*/

        private void btnMostrarFicha_Click(object sender, RoutedEventArgs e)
        {
            // este evento habre una ventana con la ficha del veiculo seleccionado
            FichaVehiculo fichaVehiculo = new FichaVehiculo();
            fichaVehiculo.Show();
        }

        private void btnVerBajas_Click(object sender, RoutedEventArgs e)
        {
            vistaVehiculos.Filter = filtroBajas;
            CalcularResultados();
        }
    }
}
