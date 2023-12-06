using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignExtensions.Controls;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para DescriRepuesto.xaml
    /// </summary>
    public partial class DescriRepuesto : MaterialWindow
    {

        BLLProducto coreProducto = new BLLProducto();
        BLLVehiculos coreVh = new BLLVehiculos();
        List<SegmentoP> cate_repuestos = new List<SegmentoP>();
        ObservableCollection<Producto> lista_productos = new ObservableCollection<Producto>();
        List<Producto> lista_seleccion = new List<Producto>();

        private int id_vehiculo;

        public DescriRepuesto(int _idvh)
        {
            InitializeComponent();

            id_vehiculo = _idvh;
            //creacion de la lista de categoria de productos que se utiliza en la solapa "Repuestos"
            // como parametro pasamos el tipo de categoria de producto
            cate_repuestos = coreProducto.RepuestosVhListarCategorias();
            cmbCateRep.ItemsSource = cate_repuestos;
        }



        private void BtAceptar_Click(object sender, RoutedEventArgs e)
        {
            //codigo para dar de alta un nuevo registro en la tabla repuestovh
            //chekeamos que este seleccionada una categoria
            if (cmbCateRep.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un segmento de Repuesto", "Aviso", MessageBoxButton.OK);
                return;
            }
            //a parti de aca , debemos recorrer la lista de seleccion y agregar uno a uno los items
            //al listado de repuestos de la base de datos
            foreach (var item in lista_seleccion)
            {
                coreVh.RepuestoAlta(id_vehiculo, item.IdProducto, 1);
            }



            //grabamos


            DialogResult = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea cancelar la operacion?", "Aviso", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void CmbCateRep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SegmentoP segmento = cmbCateRep.SelectedItem as SegmentoP;
            lista_productos = coreProducto.ListarTodosTipoCategoriaSegmento(5, 5, segmento.IdSegmentoP);
            dgConsultaRepuestos.ItemsSource = lista_productos;
            dgConsultaRepuestos.DataContext = lista_productos;
        }

        private void ChkSeleccionar_Checked(object sender, RoutedEventArgs e)
        {
            //si hacemos click en la casilla de verificacion, podemos seleccionar y agregar el producto a la lista de seleccion
            Producto pselec = dgConsultaRepuestos.SelectedItem as Producto;
            lista_seleccion.Add(pselec); // agregamos a la lista el producto seleccionado
        }

        private void ChkSeleccionar_Unchecked(object sender, RoutedEventArgs e)
        {
            Producto punselec = dgConsultaRepuestos.SelectedItem as Producto;
            lista_seleccion.Remove(punselec);
        }

        private void MaterialWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
