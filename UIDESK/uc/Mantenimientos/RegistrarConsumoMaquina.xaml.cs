using System.Windows;
using ENTIDADES;
using BLL;
using MaterialDesignExtensions.Controls;
using System;

namespace UIDESK.uc.Mantenimientos
{
    /// <summary>
    /// Lógica de interacción para RegistrarConsumoMaquina.xaml
    /// </summary>
    public partial class RegistrarConsumoMaquina : MaterialWindow 
    {
        Producto _producto = new Producto();
        BLLLaboratorio coreLab = new BLLLaboratorio();
        int _idmpm;

        public RegistrarConsumoMaquina( Producto producto, int idmpm)
        {
            InitializeComponent();
            _producto = producto;
            _idmpm = idmpm;
            DataContext = _producto;
           
            
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            //validamos que haya una cantidad indicada
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe Ingresar una cantidad", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                int _consumo = Convert.ToInt32(txtCantidad.Text);
                //llamamos al metodo que registra el consumo
                coreLab.RegistrarConsumoMaquina(_idmpm, _consumo);
                DialogResult = true;
            }
           

        }
    }
}
