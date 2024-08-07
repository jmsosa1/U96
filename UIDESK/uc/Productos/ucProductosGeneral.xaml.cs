using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.ABM;
using UIDESK.Remitos;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ucProductosGeneral.xaml
    /// </summary>
    public partial class ucProductosGeneral : UserControl
    {
        #region Declarativas


        BLLProducto coreproducto = new BLLProducto();
        ObservableCollection<Producto> lista_productos = new ObservableCollection<Producto>();

        int _tipoProducto = 1;

        public ICollectionView vistaProductos
        {
            get { return CollectionViewSource.GetDefaultView(lista_productos); }
        }
        #endregion  

        public ucProductosGeneral()
        {
            InitializeComponent();
            lista_productos = coreproducto.ListarTodos();

            dgProductos.ItemsSource = lista_productos.Where(x => x.EstadoItem == 1 && x.AltaF > DateTime.Now.AddDays(-120)).ToList();
            dgProductos.DataContext = lista_productos.Where(x => x.EstadoItem == 1 && x.AltaF > DateTime.Now.AddDays(-120)).ToList();
          
            //CalcularRegistrosVista(lista_productos);

            // vistaProductos.Filter = new System.Predicate<object>(filtroTipo);
            // vistaProductos.Filter = new Predicate<object>(filtroCategoria);
        }

        private void CalcularRegistrosVista(List<Producto> lista_productos)
        {
            txtRegistros.Text = lista_productos.Count.ToString();
            var _activos = lista_productos.Where(x => x.EstadoItem == 1).ToList();
            var _bajas = lista_productos.Where(x => x.EstadoItem == 5).ToList();
            txtBajas.Text = _bajas.Count.ToString();
            txtActivos.Text = _activos.Count.ToString();
        }



        #region FiltrosVistas

        /*
        private bool filtroCategoria(object obj)
        {/*
            Producto p = obj as Producto;
            //CategoriaP ct = cmbCategoriaProducto.SelectedItem as CategoriaP;
            if (ct!= null)
            {
                return p.IdCateP == ct.IdCateP;
            }
            return p.IdCateP == 0;
        }*/

        private bool filtroTipo(object obj)
        {

            Producto p = obj as Producto;
            //TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;

            return p.IdTipoP == _tipoProducto;
        }

        private bool filtroNombre(object obj)
        {
            Producto p = obj as Producto;
            string _texto = txtBuscar.Text;
            //TipoProducto tipo = cmbTipoProducto.SelectedItem as TipoProducto;

            return p.Nombre.Contains(_texto) || p.Modelo.Contains(_texto)
                || p.CodInventario.Contains(_texto)
                || p.Descripcion.Contains(_texto) || p.NumSerie.Contains(_texto);
        }

        private bool filtroCodigoProducto(object obj)
        {
            Producto p = obj as Producto;
            int cod = Convert.ToInt32(txtBuscar.Text);
            return p.IdProducto == cod;
        }
        private void chkInventario_Click(object sender, RoutedEventArgs e)
        {
            //ok aca algo interesante
            // cuando ponemos a true este chekbox modificamos su hint, para que indique porque criterio
            //realizamos la busqueda
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txtBuscar, "Buscar por Cod Inventario");

            //aca viene el codigo de la busqueda por inventario
        }


        #endregion

        #region EventosGrid


        private void DgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto p = dgProductos.SelectedItem as Producto;
            if (p != null)
            {
                ucDetalleFilaProducto._idproducto = p.IdProducto;
                ucDetalleFilaProducto._vistalab = false;
            }
        }

        /* private void DataGridRow_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
         {
             //este manejador de eventos permite habrir y cerrar el detalle con el click izquierdo del mouse
             DataGridRow row = sender as DataGridRow;
             if (row != null)
             {
                 row.DetailsVisibility = row.IsSelected ? Visibility.Collapsed : Visibility.Visible;
             }
         }*/
        #endregion



        #region EventosBotones
        private void BtnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            Producto p = dgProductos.SelectedItem as Producto;

            ABMProducto modificarProducto = new ABMProducto(p.IdProducto);

            if (modificarProducto.ShowDialog() == true)
            {
                MessageBox.Show("Se modifico el producto", "Aviso", MessageBoxButton.OK);
                lista_productos = coreproducto.ListarTodos();
                dgProductos.ItemsSource = lista_productos;
                dgProductos.DataContext = lista_productos;
            }
        }



        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ABMProducto nuevoProducto = new ABMProducto(0);
            if (nuevoProducto.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego el producto", "Aviso", MessageBoxButton.OK);
                lista_productos = coreproducto.ListarTodos();
                dgProductos.ItemsSource = lista_productos;
                dgProductos.DataContext = lista_productos;
            }

        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult boxResult = MessageBox.Show("Dar de baja el producto?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.Yes)
            {
                // dar de baja el producto

                Producto p = dgProductos.SelectedItem as Producto;
                coreproducto.ProductoCambiarEstado(p.IdProducto, 5); // idestado=5 es baja
                MessageBox.Show("Se dio de baja el producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                lista_productos = coreproducto.ListarTodos();
                dgProductos.ItemsSource = lista_productos;
                dgProductos.DataContext = lista_productos;
            }
        }


        private void btnRepuestos_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 5;
            vistaProductos.Filter = filtroTipo;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //este bloque de codigo nos permite realizar las busquedas por numeros o por caracteres
            //evaluando el texto ingresado usando TryParse y su parametro de salida
            //int temp;
            //if (int.TryParse(txtBuscar.Text, out temp)) // si es numeros
            //{


            //    vistaProductos.Filter = filtroCodigoProducto;
            //}
            //else
            //{
            //    vistaProductos.Filter = filtroNombre; // si es caracter
            //}
            
            vistaProductos.Filter = filtroNombre;
            
            dgProductos.ItemsSource = lista_productos; 
            dgProductos.DataContext = lista_productos;
             List<Producto> productos = new List<Producto>();
            productos = dgProductos.Items.Cast<Producto>().ToList();
            CalcularRegistrosVista(productos);
        }

        //selectores de categoria de productos
        private void btnMuebles_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 3;
            vistaProductos.Filter = filtroTipo;
        }

        private void btnInstrumentos_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 4;
            vistaProductos.Filter = filtroTipo;
        }

        private void btnContenedores_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 6;
            vistaProductos.Filter = filtroTipo;
        }

        private void btnIndumentaria_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 2;
            vistaProductos.Filter = filtroTipo;
        }

        private void btnHerramientas_Click(object sender, RoutedEventArgs e)
        {
            _tipoProducto = 1;
            vistaProductos.Filter = filtroTipo;
        }


        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgProductos.SelectedIndex = -1;
        }

        private void btnAdmMarcas_Click(object sender, RoutedEventArgs e)
        {
            AdmMarcaProductos adm = new AdmMarcaProductos();
            adm.ShowDialog();

        }

        private void btnAdmTipos_Click(object sender, RoutedEventArgs e)
        {
            AdmTiposProductos adm = new AdmTiposProductos();
            adm.ShowDialog();
        }






        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mniCambiarSituacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar.SelectAll();
        }

        private void btnBajaProducto_Click(object sender, RoutedEventArgs e)
        {
            bool validar;
            Producto p = dgProductos.SelectedItem as Producto;

        
            if (p != null)
            {

                //primero verificamos si el producto ya esta dado de baja

                if (p.EstadoItem == 5)
                {
                    MessageBox.Show("el producto no se puede dar de baja", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {


                    MessageBoxResult boxResult = MessageBox.Show("Dar de baja el producto?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (boxResult == MessageBoxResult.Yes)
                    {
                        // dar de baja el producto
                        //para dar de baja un producto , primero debemos verifica que no tenga cantidades en stock asociadas
                        validar = coreproducto.ValidarExistenciaDeStockUnProducto(p.IdProducto);
                        if (validar)
                        {
                            //si el producto tiene stock, avisamos que se dara la baja del stock a esas unidades
                            MessageBoxResult confirmar = MessageBox.Show("El producto tiene stock asociado. Se dara consumo al mismo.Continuar?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (confirmar == MessageBoxResult.Yes)
                            {
                                BajaProducto bajaProducto = new BajaProducto(p.IdProducto);
                                if (bajaProducto.ShowDialog() == true)
                                {
                                    MessageBox.Show("Se dio de baja el producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                                    //refrescamos la lista
                                    lista_productos = coreproducto.ListarTodos();
                                    dgProductos.ItemsSource = lista_productos;
                                    dgProductos.DataContext = lista_productos;
                                }
                            }



                        }
                        else
                        {
                            BajaProducto bajaProducto = new BajaProducto(p.IdProducto);
                            if (bajaProducto.ShowDialog() == true)
                            {
                                MessageBox.Show("Se dio de baja el producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                                //refrescamos la lista
                                lista_productos = coreproducto.ListarTodos();
                                dgProductos.ItemsSource = lista_productos;
                                dgProductos.DataContext = lista_productos;
                            }
                        };
                    }
                }
            }

        }

        private void btnEditProducto_Click(object sender, RoutedEventArgs e)
        {
            Producto p = dgProductos.SelectedItem as Producto;
            if (p != null)
            {
                ABMProducto modificarProducto = new ABMProducto(p.IdProducto);

                if (modificarProducto.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el producto", "Aviso", MessageBoxButton.OK);
                    lista_productos = coreproducto.ListarTodos();
                    dgProductos.ItemsSource = lista_productos;
                    dgProductos.DataContext = lista_productos;
                }
            }

        }

        private void btnNuevoMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dgProductos.SelectedItem as Producto;
            if (producto == null)
            {
                MessageBox.Show("Debe seleccionar un producto antes", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                RegistrarMantenimiento registrarMantenimiento = new RegistrarMantenimiento(producto);
                if (registrarMantenimiento.ShowDialog() == true)
                {
                    MessageBox.Show("Se registro el mantenimiento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_productos = coreproducto.ListarTodos();
                    dgProductos.ItemsSource = lista_productos;
                    dgProductos.DataContext = lista_productos;
                }
            }
        }

        private void btnNuevoRMA_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dgProductos.SelectedItem as Producto;
            if (producto != null && producto.EstadoItem == 1) // mientras el producto no sea null y ademas tenga estado activo
            {
                RMA rMA = new RMA(producto);
                if (rMA.ShowDialog() == true)
                {
                    MessageBox.Show("Se genero el RMA del producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_productos = coreproducto.ListarTodos();
                    dgProductos.ItemsSource = lista_productos;
                    dgProductos.DataContext = lista_productos;
                }
            }
        }

        private void btnModiInstrumento_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dgProductos.SelectedItem as Producto;
            if (producto != null && producto.IdTipoP == 4)
            {
                ModiInstrumento modi = new ModiInstrumento(producto);
                if (modi.ShowDialog() == true)
                {
                    MessageBox.Show("Se modifico el producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_productos = coreproducto.ListarTodos();
                    dgProductos.ItemsSource = lista_productos;
                    dgProductos.DataContext = lista_productos;
                }
            }
            else
            {
                MessageBox.Show("El producto no es un instrumento. No se puede modificar", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
        }
    }
}
