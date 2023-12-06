using BLL;
using ENTIDADES;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para LoginApp.xaml
    /// </summary>
    public partial class LoginApp : Window
    {
        BLLEmpleados bllEmpleados = new BLLEmpleados();
        public LoginApp()
        {
            InitializeComponent();
        }

        //Theme Code ========================>
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //===================================>

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            //Theme Code ========================>
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
            //===================================>
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            /* codigo de login en la base de datos sql server*/
            try
            {

                //invocamos al metodo LoginEmpleado en la clase BLLEmpleados
                // si el resultado es exitoso damos la vienvenida
                if (bllEmpleados.LoginEmpleado(txtUsername.Text, txtPassword.Password) == true)
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


            CargarVentanaPrincipal();
        }

        private void CargarVentanaPrincipal()
        {
            Usuario loginUsuario = bllEmpleados.DatosUsuario(txtUsername.Text, txtPassword.Password);
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
            principal.chipUsuario.Content = loginUsuario.NomUser;
            principal.chipUsuario.Icon = loginUsuario.Iniciales;
            principal.txbfechaYhora.Text = DateTime.Today.Date.ToShortDateString();
            //principal.tbFechaActual.Text = DateTime.Today.ToString();
            if (loginUsuario.NomUser == "DSalazar")
            {
                principal.cardVehiculos.IsEnabled = false;
            }

            if (loginUsuario.NomUser == "ERossati")
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
    }
}
