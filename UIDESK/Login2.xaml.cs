using BLL;
using ENTIDADES;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para Login2.xaml
    /// </summary>
    public partial class Login2 : Window
    {
        BLLEmpleados bllEmpleados = new BLLEmpleados();

        public Login2()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /* codigo de login en la base de datos sql server*/
            try
            {

                //invocamos al metodo LoginEmpleado en la clase BLLEmpleados
                // si el resultado es exitoso damos la vienvenida
                if (bllEmpleados.LoginEmpleado(txtUser.Text, txtPassword.Password) == true)
                {
                    MessageBox.Show("bienvenido", "aviso", MessageBoxButton.OK);




                }
                else
                {
                    MessageBox.Show("el usuario no es valido", "aviso", MessageBoxButton.OK);
                    return;
                }



            }
            catch (SqlException ex)
            {
                MessageBox.Show("Imposible conectar con la base de datos");
                foreach (SqlError err in ex.Errors)
                {
                    MessageBox.Show(err.Message);
                }
                return;
            }

            //  MessageBox.Show("base de datos abierta");

            Usuario loginUsuario = bllEmpleados.DatosUsuario(txtUser.Text, txtPassword.Password);
            //pasamos los datos del usuario logeado a la clase estatica que contiene sus datos
            Contexto.CodUser = loginUsuario.IdUsuario;
            Contexto.Nomuser = loginUsuario.NomUser;
            Contexto.Email = loginUsuario.Email;
            Contexto.CodigoDeposito = loginUsuario.Deposito;
            Contexto.Iniciales = loginUsuario.Iniciales;
            Contexto.RolUsuario = loginUsuario.RolUsuario;
            /* codigo para desplegar el form principal*/
            Principal principal = new Principal();
            //pasamos lo datos a la tarjeta del usuario 
            principal.chipUsuario.Content = loginUsuario.RolUsuario;
            principal.chipUsuario.Icon = loginUsuario.Iniciales;

            //principal.tbFechaActual.Text = DateTime.Today.ToString();
            if (loginUsuario.NomUser == "DSalazar")
            {
                principal.cardVehiculos.IsEnabled = false;
            }

            if (loginUsuario.NomUser== "ERossati")
            {
                principal.cardVehiculos.IsEnabled = false;
                principal.cardTrabajadores.IsEnabled = false;
                principal.cardAbastecimiento.IsEnabled = false;
                principal.cardObras.IsEnabled = false;
                principal.cardResultadoVehiculos.IsEnabled = false;
                principal.cardResultadoHerramienta.IsEnabled = false;
            }

            principal.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
