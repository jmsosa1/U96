using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;

namespace UIDESK.uc.gestion.Notas
{
    /// <summary>
    /// Lógica de interacción para AgregarRespuesta.xaml
    /// </summary>
    public partial class AgregarRespuesta : MaterialWindow
    {
        BLLForo coreForo = new BLLForo();

        NotaSahmV6 _nota = new NotaSahmV6();

        public AgregarRespuesta(NotaSahmV6 notaSahmV6)
        {
            InitializeComponent();
            _nota = notaSahmV6;
            DataContext = _nota;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnAddRespuesta_Click(object sender, RoutedEventArgs e)
        {
            //primero validamos que se haya agregado contenido a la respuesta
            if (string.IsNullOrEmpty(txtRespuesta.Text))
            {
                MessageBox.Show("Debe ingresar un respuesta ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // si existe una respuesta llamamos al metodo que lo escribe en la BD
                //creeamos el objeto respuestaNota
                RespuestaNota respuestaNota = new RespuestaNota();
                respuestaNota.IdNota = _nota.IdNota;
                respuestaNota.IdUsaurio = _nota.IdUsuario;
                respuestaNota.Fecha = DateTime.Today.Date;
                respuestaNota.Contenido = txtRespuesta.Text;
                coreForo.AgregarRespuesta(respuestaNota);
                DialogResult = true;
            }

        }
    }
}
