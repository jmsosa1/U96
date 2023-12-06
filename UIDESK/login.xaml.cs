using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using ENTIDADES;
using BLL;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;


namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        BLLEmpleados bllEmpleados = new BLLEmpleados();


        public login() //constructor
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
           // cerramos la ventana si se hace click en la imagen de cerrar
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /* codigo de login en la base de datos sql server*/
            try
            {
              
                  //invocamos al metodo LoginEmpleado en la clase BLLEmpleados
                  // si el resultado es exitoso damos la vienvenida
                if (bllEmpleados.LoginEmpleado(txt_usuario.Text, txt_password.Password) == true)
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
                foreach (SqlError  err in ex.Errors)
                {
                    MessageBox.Show(err.Message);
                }
                return;
            }
            //  MessageBox.Show("base de datos abierta");

            Usuario loginUsuario = bllEmpleados.DatosUsuario(txt_usuario.Text, txt_password.Password);
            //pasamos los datos del usuario logeado a la clase estatica que contiene sus datos
            Contexto.CodUser = loginUsuario.IdUsuario;
            Contexto.Nomuser = loginUsuario.NomUser;
            Contexto.Email = loginUsuario.Email;
            Contexto.CodigoDeposito = loginUsuario.Deposito;
            /* codigo para desplegar el form principal*/
            Principal principal = new Principal();
            //pasamos lo datos a la tarjeta del usuario 
            principal.chipUsuario.Content =loginUsuario.RolUsuario;
            principal.chipUsuario.Icon = loginUsuario.Iniciales;
            
            //principal.tbFechaActual.Text = DateTime.Today.ToString();
            if (loginUsuario.NomUser == "DSalazar" )
            {
                principal.btnVehiculos.IsEnabled = false;
            }
           
            principal.Show();
            this.Close();
           
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
    }
}
