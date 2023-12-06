using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMTrabajador.xaml
    /// </summary>
    public partial class ABMTrabajador : MaterialWindow 
    {
        Empleado empleado = new Empleado();

        public ABMTrabajador()
        {
            InitializeComponent();

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se cancelo la operacion", "Aviso", MessageBoxButton.OK);
            DialogResult = false;
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

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

        //CODIGO QUE PUEDE SERVIR DE AYUDA PARA PROGRAMAR EL ABM DEL EMPLEADO

        /*
       //objeto de la clase Empleado para usar en el datacontex del grid de interfaz
       Empleado nuevoEmpleado = new Empleado();
       //objeto de la clase BLLEmpleados que usaremos para guardar los datos en la base de datos
       BLLEmpleados bLLEmpleados = new BLLEmpleados();
       // variable para indicar el tipo de operacion que se quiere hacer
       string operacion = "";

       public Trabajador()
       {
           InitializeComponent();
           //asignamos el datacontex a el objeto trabajador creado anteriormente
           //asi cada campo de texto se puede asociar a una propiedad del objeto empleado
           grdTrabajador.DataContext = nuevoEmpleado;
       }

       private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
       {
           this.Close();
       }

       private void btnGuardar_Click(object sender, RoutedEventArgs e)
       {
           //antes de guardar hay que verificar que no queden errores en la validacion de los campos
           if (Validation.GetHasError(txtNombre)|| Validation.GetHasError(txtDni)||Validation.GetHasError(txtDireccion)) 
           {
               MessageBox.Show("Hay campos con error de datos. Por favor corregir y volver a intentarlo");
               return;
           }
           else
           {
               DialogResult = true;
           }
           //si todos los datos son correctos
           //asignamos al objeto Trabajador creado anteriomente , los valores de la propiedad
           //datacontex del grid que contiene los campos de datos, pasando este valor
           //de datacontex como un objeto completo de la clase trabajador
           //hacemos esto para no tener que escribir el valor de cada propiedad
           nuevoEmpleado = grdTrabajador.DataContext as Empleado;

           //ahora validamos que el empleado exista o no en la base de datos

           bool existe_empleado = false;
           existe_empleado = bLLEmpleados.ValidarEmpleado(nuevoEmpleado.Dni);

           //luego preguntamos en funcion del resultado de la validacion del empleado
           if (existe_empleado == true || operacion=="A")
           {


                   MessageBox.Show("El dni de empleado ingresado ya existe. Por favor corregir", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                   return;

           }
           else
           {
               DialogResult = true;
           }
           //luego invocamos lo metodos de la clase de logica del negocio
           //creamos una variable entera que contendra el resultado de la operacion 
           // que devuelve el metodo de grabarempleado
           int resultadoGrabacion;
           resultadoGrabacion= bLLEmpleados.GrabarEmpleado(nuevoEmpleado,operacion);

           if (resultadoGrabacion==1)
           {
               MessageBox.Show("El registro se grabo con exito", "Aviso", MessageBoxButton.OK);
           }
           else
           {
               MessageBox.Show("No se pudo grabar el registro", "Aviso", MessageBoxButton.OK);
           }
       }*/
    }
}
