using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;
using System.Windows.Input;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMAjuste.xaml
    /// </summary>

    public partial class ABMAjuste : MaterialWindow
    {
        public string objetoABM = "";
        public int idreg = 0;
        public string _op = "";
        BLLVehiculos bLL = new BLLVehiculos();
        BLLBase bllbase = new BLLBase();

        public ABMAjuste()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (objetoABM)
            {
                case "CMB":
                    GestionCombustible(_op, idreg);
                    break;
                case "CTE":
                    GestionCategoria(_op, idreg);
                    break;
                case "LN":
                    GestionLinea(_op, idreg);
                    break;
                case "PV":
                    GestionProvincia(_op, idreg);
                    break;
                case "LC":
                    GestionLocalidad(_op, idreg);
                    break;
                default:
                    break;
            }
            DialogResult = true;
            this.Close();
            // al cerrar el formulario lo que sucede despues es que con los datos cargados en los textboxes
            //se llevan adelante las operaciones que correspondan
        }

        private void GestionCombustible(string op, int id)
        {
            Combustible c = new Combustible();
            int fila = 0;
            // bool resultado = false;

            if (op == "A")
            {
                c.Descripcion = txt1.Text;
                c.PrecioLitroActual = Convert.ToDecimal(txt2.Text);

                fila = bLL.CombustibleAlta(c);
            }
            if (op == "M")
            {
                c.Descripcion = txt1.Text;
                c.PrecioLitroActual = Convert.ToDecimal(txt2.Text);
                c.IdCombustible = idreg;
                fila = bLL.CombustibleModi(c);
            }

            if (op == "D")
            {

            }
        }

        private void GestionCategoria(string op, int id)
        { }
        private void GestionLinea(string op, int id)
        { }

        private void GestionProvincia(string op, int id)
        { }

        private void GestionLocalidad(string op, int id)
        { }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
