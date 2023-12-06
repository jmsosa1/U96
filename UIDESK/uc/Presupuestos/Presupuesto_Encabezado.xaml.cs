using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDESK.uc.Presupuestos
{
    /// <summary>
    /// Lógica de interacción para Presupuesto_Encabezado.xaml
    /// </summary>
    public partial class Presupuesto_Encabezado : Window
    {
        // si  se trata de una alta del presupuesto no hay que ir contra la base de datos 
        // para grabar el detalle o el encabezado
        //sin embargo si se trata de una modificacion , ahi si debemos trabajar directamente contra la base de datos
        BLLPresupuesto corePresupuesto = new BLLPresupuesto(); // acceso a los metodos DAL 
        BLLVehiculos coreVh = new BLLVehiculos(); // acceso a los metodos DAL 
         ObservableCollection<Presupuesto_Item> _detallePre = new ObservableCollection<Presupuesto_Item>(); // lista que contendra el detalle del presupuesto
        Presupuesto _encabezado ; // objeto que contendra el parametro pasado al constructor
        ObservableCollection<CategoriaVh> categorias = new ObservableCollection<CategoriaVh>(); // lista de categorias de vehiculos
        List<TipoVh> tipoVhs = new List<TipoVh>(); // lista de tipos de vehiculos
        
        int _operacion = 1; // representa la operacion  1 alta, 2 modificacion
       
        int _itemdet = 1; // valor que se usa para indicar el numero de item del detalle
        TipoVh tipoVh_sel { get; set; } // objeto que se usa para contener la seleccion del combobox de los tipos de vehiculos
        CategoriaVh CateVh_sel { get; set; } //objeto que se usa para contener la seleccion del combobox de las categorias de vehiculos 
        CultureInfo ci_us = new CultureInfo("en-US");
        CultureInfo ci_ar = new CultureInfo("es-Ar");
     
        public Presupuesto_Encabezado(Presupuesto encabezado)
        {
            InitializeComponent();
            //antes que nada comprobamos si el presupuesto tiene un id asociado
            
            if (encabezado.IdPre !=0)
            {
                // si lo tiene, entonces se trata de una modificacion o baja
                //cargamos su detalle
                _detallePre = corePresupuesto.ListarDetalleUnPresupuesto(encabezado.IdPre, encabezado.IdTipoPresupuesto);
                
            }
            else
            {
                // se trata de una alta
            }
            tipoVhs = coreVh.ListarTiposVh();
          
            cmbTipos.ItemsSource = tipoVhs;
            cmbTipos.DataContext = tipoVhs;
            cmbTipos.SelectedIndex = 0;
            //cmbGTipos.ItemsSource = tipoVhs;
            
            //luego seteamos los data context y datagrids correspondientes
            _encabezado = encabezado;
            DataContext = _encabezado;
            //Presupuesto_Item_Vh _Item_Vh_Actualizar = new Presupuesto_Item_Vh();
            //_detallePre.Add(_Item_Vh_Actualizar);
            grD.ItemsSource = _detallePre; // datagrid detalle
            grD.DataContext = _detallePre;
          

          

        }

      
        private void btnCerrarPresupuesto_Click(object sender, RoutedEventArgs e)
        {
             // logica para cerrar el presupuesto, lo que implica guardar los cambios 
             // pero no implica activar el mismo 
        }


        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            // logica para imprimir o exportar el presupuesto de obra
        }

       

        private void cmbTipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipoVh_sel = cmbTipos.SelectedItem as TipoVh;
            if (tipoVh_sel != null)
            {
                //traemos la categorias del tipo seleccionado
                categorias = coreVh.VehiculosListarCategoriaPorTipo(tipoVh_sel.IdTipoVh);
                //creamos ahora la lista de categorias dto
                cmbCategorias.DataContext = categorias;
                cmbCategorias.ItemsSource = categorias;
                cmbCategorias.SelectedIndex = 0;
                
               

            }
        }

        private void cmbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CateVh_sel = cmbCategorias.SelectedItem as CategoriaVh;
            if (CateVh_sel != null)
            {

            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //aca debemos agregar el item a lista del detalle
            // para eso debemos verificar que tipo de operacion estamos haciendo, segun el valor del tipode ABM ,
            if (_operacion==1) // alta
            {
                Presupuesto_Item_Vh _Item_Vh1 = new Presupuesto_Item_Vh();
                _Item_Vh1.Monto_Presupuestado = decimal.Parse(txtPresupuesto.Text.Replace("$", ""));
                _Item_Vh1.Monto_Real_Ejecutado = decimal.Parse(txtEjecutado.Text.Replace("$", ""));
                _Item_Vh1.Monto_Aprobado = decimal.Parse(txtAprobado.Text.Replace("$", ""));
                _Item_Vh1.NomCate = CateVh_sel.NomCate;
                _Item_Vh1.IdCate = CateVh_sel.IdCateVh;
                _Item_Vh1.IdTipo = tipoVh_sel.IdTipoVh;
                _Item_Vh1.IdPre = 0;
                _Item_Vh1.NomTipo = tipoVh_sel.NomTipo;
                _Item_Vh1.NumItem = _detallePre.Count() + 1;
                _detallePre.Add(_Item_Vh1);
                CalcularImportes(_Item_Vh1);
            }
            else
            {
                // si es una modificacion lo hacemos con los siguientes pasos.
                // primero ubicamos el indice del objeto que estamos modificando

                var indice = _detallePre.IndexOf(grD.SelectedItem as Presupuesto_Item_Vh);
                //luego borramos este objeto seleccionado en esa posicion
                _detallePre.RemoveAt(indice);
                //luego creamos un objeto nuevo , le asignamos los valores y lo insertamos en la posicion que tenia el objeto anterior
                Presupuesto_Item_Vh _Item_Vh1 = new Presupuesto_Item_Vh();
                _Item_Vh1.Monto_Presupuestado = decimal.Parse(txtPresupuesto.Text.Replace("$", ""));
                _Item_Vh1.Monto_Real_Ejecutado = decimal.Parse(txtEjecutado.Text.Replace("$", ""));
                _Item_Vh1.Monto_Aprobado = decimal.Parse(txtAprobado.Text.Replace("$", ""));
                _Item_Vh1.NomCate = CateVh_sel.NomCate;
                _Item_Vh1.IdCate = CateVh_sel.IdCateVh;
                _Item_Vh1.IdTipo = tipoVh_sel.IdTipoVh;
                _Item_Vh1.IdPre = 0;
                _Item_Vh1.NomTipo = tipoVh_sel.NomTipo;
                _detallePre.Insert(indice,_Item_Vh1);
                CalcularImportes(_Item_Vh1);
            }
            
        }

        //metodo que actualiza los importes del presupuesto segun se agreguen , modifiquen o quiten item del detalle
        private void CalcularImportes(Presupuesto_Item_Vh item_Vh)
        {
            //metodo que actualiza los montos del encabezado del presupuesto
            // para esto ponemos a cero el montototalmonedappal y recalculamos el mismo sumando los montos de cada items
            // recorriendo la lista de objetos del item
            _encabezado.MontoTotalMonedaPpal = 0;
            foreach (var item in _detallePre)
            {
                _encabezado.MontoTotalMonedaPpal = _encabezado.MontoTotalMonedaPpal + item.Monto_Presupuestado;
            }
            //con el monto total recalculado, recalculamos los otros
            _encabezado.MontoTotalMonedaSec = (_encabezado.MontoTotalMonedaPpal / _encabezado.DolarPresupuesto);
            //mostramos el resultado 
            txtMontoTotalMppal.Text = _encabezado.MontoTotalMonedaPpal.ToString("C", ci_ar);
            txtMontoTotalMSec.Text = _encabezado.MontoTotalMonedaSec.ToString("C", ci_us);
        }

        //editar un item del detalle
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            // primero verificamos que se haya seleccionado un item
            Presupuesto_Item_Vh _item_presupuesto = grD.SelectedItem as Presupuesto_Item_Vh;
            if (_item_presupuesto != null)
            {
                //actualizamos la bandera del item seleccionado para indicar que es una modificacion
                _operacion = 2;
                _item_presupuesto.TipoABM = 2;
                // colocamos correctamente los elementos seleccionados de el tipo y la categoria que estamos modificando
                cmbTipos.SelectedIndex = tipoVhs.IndexOf(tipoVhs.FirstOrDefault(x => x.IdTipoVh == _item_presupuesto.IdTipo));
                cmbCategorias.SelectedIndex = categorias.IndexOf(categorias.FirstOrDefault(x => x.IdCateVh == _item_presupuesto.IdCate));
                grCalculadora.DataContext = _item_presupuesto;
              
                txtPresupuesto.Text = _item_presupuesto.Monto_Presupuestado.ToString("C", ci_ar);
                txtAprobado.Text = _item_presupuesto.Monto_Aprobado.ToString("C", ci_ar);
                txtEjecutado.Text = _item_presupuesto.Monto_Real_Ejecutado.ToString("C", ci_ar);
                grCalculadora.Visibility = Visibility.Visible;
                

            }

        }

        private void btnDelItem_Click(object sender, RoutedEventArgs e)
        {
            // logica para ver la eleminacion del item
            Presupuesto_Item_Vh item_Vh = grD.SelectedItem as Presupuesto_Item_Vh;
            if (item_Vh != null)
            {
                MessageBoxResult result = MessageBox.Show("Desea eliminar este item?", "Atencion!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // aca se borra el item.
                    //debemos evaluar si es una alta del presupuesto o si es una modificacion del mismo
                    if (_operacion == 1) // alta, borramos de la lista 
                    {
                        _detallePre.Remove(item_Vh);
                    }
                    else
                    {
                        //aca ya debemos ir a la base de datos y borrar el item del detalle del presupuesto y luego refrescar el grid
                    }
                }
            }
        }

        private void btnCalculadora_Click(object sender, RoutedEventArgs e)
        {
            if (grCalculadora.Visibility == Visibility.Collapsed)
            {
                grCalculadora.Visibility = Visibility.Visible;
            }
            else
            {
                grCalculadora.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAddCalculadora_Click(object sender, RoutedEventArgs e)
        {
            _operacion = 1; // alta de un item
            Presupuesto_Item_Vh item_Vh = new Presupuesto_Item_Vh();
            grCalculadora.DataContext = item_Vh;
            btnAceptar.IsEnabled = true;
        }

        private void btnCancelCal_Click(object sender, RoutedEventArgs e)
        {
            grCalculadora.Visibility= Visibility.Collapsed;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //aca va la logica para guardar un presupuesto
            // primero debemos validar los datos ingresados al encabezado
            if (!corePresupuesto.ValidarEncabezadoPresupuesto(_encabezado) )
            {
                //si es true, entonces avanzamos y registramos o actualizamos el encabezado
                
                corePresupuesto.AltaEncabezado(_encabezado);
                // para grabar el detalle del presupuesto 
                // debemos consultar los datos del encabezado , de la siguiente manera
                // si es una alta, tomamos el ultimo ingresado ejecutando la consulta a la base de datos
                //pero si es una modificacion, directamente usamos los datos del objeto _encabezado pasado como parametro al metodo anterior
                if (_encabezado.IdPre==0) //alta
                {
                    //buscamos el ultimo presupuesto
                    PresupuestoVh presupuestoVh = corePresupuesto.UltimoIdPresupuesto();
                    // ahora con este objeto grabamos el detalle
                    foreach (var item in _detallePre)
                    {
                        item.IdPre = presupuestoVh.IdPre;
                        corePresupuesto.AltaDetalle(item, _encabezado.IdUsuario);
                    }
                    MessageBox.Show("Se dio de alta el presupuesto","Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // usamos el objeto _encabezado
                    foreach (var item in _detallePre)
                    {
                        corePresupuesto.AltaDetalle(item, _encabezado.IdUsuario);
                    }
                    MessageBox.Show("Se actualizo el presupuesto","Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            else
            {
                MessageBox.Show("Faltan algunos datos, como el titulo o el precio del dolar. Por favor, verifique", 
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
            }
        }

        private void btnCancelarEdicion_Click(object sender, RoutedEventArgs e)
        {
            //preguntamos si queremos cancelar la edicion
            MessageBoxResult result = MessageBox.Show("Desea cancelar la edicion del presupuesto?","Aviso",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void txtPresupuesto_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPresupuesto.SelectAll();
        }

        private void txtAprobado_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAprobado.SelectAll();
        }
    }
}
