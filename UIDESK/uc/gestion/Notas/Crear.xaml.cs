using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.IO;
using System.Windows;

namespace UIDESK.uc.gestion.Notas
{
    /// <summary>
    /// Lógica de interacción para Crear.xaml
    /// </summary>
    public partial class Crear : MaterialWindow
    {
        BLLForo coreForo = new BLLForo();
        NotaSahmV6 _nota = new NotaSahmV6();


        public Crear()
        {
            InitializeComponent();
            _nota.FechaAlta = DateTime.Today.Date;
            _nota.IdEstado = 1;
            _nota.IdUsuario = Contexto.CodUser;
            _nota.CantRespuesta = 0;
            _nota.NombreUsuario = Contexto.Iniciales;
            byte[] img = File.ReadAllBytes(@"imagenes\nueva-tarea.png");
            _nota.ImagenEstado = img;
            _nota.CantLecturas = 0;
            _nota.Situacion = 18;
            //_nota.Titulo = "agrege un titulo";


            DataContext = _nota;
        }


        #region Ventana


        #endregion

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (coreForo.ValidarNota(_nota))
            {
                coreForo.AgregarNota(_nota);

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Existen errores en el formulario.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }
    }
}
