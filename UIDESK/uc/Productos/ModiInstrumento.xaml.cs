using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.uc.Productos
{
    /// <summary>
    /// Lógica de interacción para ModiInstrumento.xaml
    /// </summary>
    public partial class ModiInstrumento : MaterialWindow
    {
        BLLProducto coreProducto = new BLLProducto();
        Producto produ = new Producto();

        public ModiInstrumento(Producto producto)
        {
            InitializeComponent();
            produ = producto;

            DataContext = produ;



        }



        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            //primero actualizamos las propiedades que dependen de los radio button que son dos "contraste" y "patron"
            if (rdb1.IsChecked == true)
            {
                produ.Constrate = 1;
            }
            else
            {
                if (rb2.IsChecked == true)
                {
                    produ.Constrate = 2;
                }
                else
                {
                    produ.Constrate = 3;
                }
            }

            if (rb4.IsChecked == true)
            {
                produ.Patron = 1;
            }
            else
            {
                produ.Patron = 2;
            }
            // para actualizar pasamos el objeto modificado directamente
            coreProducto.InstrumentoModificar(produ.Escala, produ.RangoMedicion, produ.Exactitud, produ.Patron, produ.Constrate, produ.IdProducto);
            DialogResult = true;
            this.Close();

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
