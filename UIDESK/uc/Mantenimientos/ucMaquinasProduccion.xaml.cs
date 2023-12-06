using BLL;
using ENTIDADES;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;
using System.Windows;

namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para ucMaquinasProduccion.xaml
    /// </summary>
    public partial class ucMaquinasProduccion : UserControl
    {
        #region Declarativas

        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Producto> lista_maquinas = new ObservableCollection<Producto>();
        BLLLaboratorio coreLab = new BLLLaboratorio();
        BLLMaquinas coreMaq = new BLLMaquinas();
        #endregion

        public ucMaquinasProduccion()
        {
            InitializeComponent();
            //cargamos los productos que son de la categoria maquinas
            lista_maquinas = coreProducto.ListarProductosDeUnTipo(9); // codigo de maquinas
            dgMaquinas.ItemsSource = lista_maquinas;
            dgMaquinas.DataContext = lista_maquinas;
            
        }

        private void btnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            Producto producto = dgMaquinas.SelectedItem as Producto;
            if (producto != null)
            {

                if (coreMaq.ValidarExistenciaPlanillaMPM(producto.IdProducto))
                {
                    MessageBox.Show("Ya existe una planilla activa para esta maquina", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {


                    PlanillaMantenimientoMaquina planilla = new PlanillaMantenimientoMaquina(producto);
                    if (planilla.ShowDialog() == true)
                    {
                        MessageBox.Show("Se Agrego el plan de mantenimiento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        lista_maquinas = coreProducto.ListarProductosDeUnTipo(9); // codigo de maquinas
                        dgMaquinas.ItemsSource = lista_maquinas;
                        dgMaquinas.DataContext = lista_maquinas;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una maquina", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

        }

       

        private void btnCerrarDetalle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dgMaquinas.SelectedIndex = -1;
        }

        private void dgMaquinas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto p = dgMaquinas.SelectedItem as Producto;
            if (p != null)
            {
               
                    ucDetalleMpm._idproducto = p.IdProducto;
                
            }
        }

       

       

        private void btnVerPLanilla_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dgMaquinas.SelectedItem as Producto;
            DetallePlanilla detalle = new DetallePlanilla(producto);
            detalle.ShowDialog();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dgMaquinas.SelectedItem as Producto;
            if (producto != null)
            {
                PlanillaMantenimientoMaquina planilla = new PlanillaMantenimientoMaquina(producto);
                if (planilla.ShowDialog() == true)
                {
                    MessageBox.Show("Se Actualizo el plan de mantenimiento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    lista_maquinas = coreProducto.ListarProductosDeUnTipo(9); // codigo de maquinas
                    dgMaquinas.ItemsSource = lista_maquinas;
                    dgMaquinas.DataContext = lista_maquinas;
                }
            }
        }
    }
}
