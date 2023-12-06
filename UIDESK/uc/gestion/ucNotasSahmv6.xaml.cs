using BLL;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UIDESK.uc.gestion.Notas;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ucNotasSahmv6.xaml
    /// </summary>
    public partial class ucNotasSahmv6 : UserControl
    {

        BLLForo coreForo = new BLLForo();
        BLLBase coreBase = new BLLBase();

        ObservableCollection<NotaSahmV6> listaNotas = new ObservableCollection<NotaSahmV6>();
        NotaSahmV6 _notaSeleccionada = new NotaSahmV6();
        RespuestaNota _respuestaSeleccionada = new RespuestaNota();
        List<RespuestaNota> listaRespuestas = new List<RespuestaNota>();
        List<Usuario> listaUsuarios = new List<Usuario>();
        public string _tituloClase = "Notas SAHMV6";
        public ucNotasSahmv6()
        {
            InitializeComponent();
            CargarNotasActivas();
            listaUsuarios = coreBase.ListarUsuariosSistema();
            cmbFiltroUsuario.ItemsSource = listaUsuarios;
            cmbFiltroUsuario.DataContext = listaUsuarios;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Crear _crearNota = new Crear();
            if (_crearNota.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la nota", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarNotasActivas();
            }
        }

        private void btnLecturas_Click(object sender, RoutedEventArgs e)
        {
            LeerNota leerNota = new LeerNota(_notaSeleccionada);
            leerNota.ShowDialog();
            // cuando cierro el cuadro de dialogo se verifica si el usuario ya habia leido esta nota
            // si no es asi, se graba en la base de datos el registro correspondiente en la tabla NotasLeido
            coreForo.MarcarComoLeidaNota(_notaSeleccionada.IdNota, _notaSeleccionada.IdUsuario, DateTime.Today.Date);
            CargarNotasActivas();
        }

        private void dgNotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _notaSeleccionada = dgNotas.SelectedItem as NotaSahmV6;

        }

        private void btnAddRespuesta_Click(object sender, RoutedEventArgs e)
        {
            // primero validamos que la nota este abierta
            if (_notaSeleccionada.IdEstado == 1)
            {
                // llamamos al formulario de respuesta. Debemos pasar la nota seleccionada al constructor
                AgregarRespuesta _formRespuesta = new AgregarRespuesta(_notaSeleccionada);
                if (_formRespuesta.ShowDialog() == true)
                {
                    CargarNotasActivas();
                }

            }
        }

        private void btnRespuestas_Click(object sender, RoutedEventArgs e)
        {
            if (_notaSeleccionada.CantRespuesta > 0)
            {
                listaRespuestas = coreForo.ObtnerRespuestasUnaNota(_notaSeleccionada.IdNota);
                dgRespuestas.ItemsSource = listaRespuestas;
                dgRespuestas.DataContext = listaRespuestas;
                txbtitulo.Text = _notaSeleccionada.Titulo;
            }
        }

        private void btnCerrarNota_Click(object sender, RoutedEventArgs e)
        {
            //primero debemos confirmar que el usuario es el usuario creador
            if (_notaSeleccionada.IdUsuario != Contexto.CodUser)
            {
                MessageBox.Show("No puede realizar esa operacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // debemos pedir confirmacion al usuario si desea cerrar la nota
                MessageBoxResult result = MessageBox.Show("Cerrar la nota?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // cerramos la nota
                    coreForo.CerrarNota(_notaSeleccionada.IdNota);
                    CargarNotasActivas();
                }
            }


        }

        private void CargarNotasActivas()
        {
            listaNotas = coreForo.ObtenerNotasActivas();
            dgNotas.ItemsSource = listaNotas;
            dgNotas.DataContext = listaNotas;
        }

        private void btnDeleteNota_Click(object sender, RoutedEventArgs e)
        {
            //primero debemos confirmar que el usuario es el usuario creador
            if (_notaSeleccionada.IdUsuario != Contexto.CodUser)
            {
                MessageBox.Show("No puede realizar esa operacion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Borrar/Baja  la nota?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // PRIMERO DEBEMOS COMPROBAR QUE LA NOTA NO TENGA RESPUESTAS
                    if (_notaSeleccionada.CantRespuesta > 0)
                    {
                        //baja de nota
                        coreForo.BajaNota(_notaSeleccionada.IdNota);
                        MessageBox.Show("La nota se dio de baja", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarNotasActivas();
                    }
                    else
                    {
                        //borrar nota
                        coreForo.BorrarNota(_notaSeleccionada.IdNota);
                        MessageBox.Show("La nota fue borrada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarNotasActivas();
                    }
                }
            }




        }

        private void btnDeleteRespuesta_Click(object sender, RoutedEventArgs e)
        {
            // primero se verifica que el usuario que va a borrar la respuesta
            //sea el que la creo

            if (_respuestaSeleccionada.IdUsaurio != Contexto.CodUser)
            {
                MessageBox.Show("No puede borrar la respuesta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                coreForo.BorrarRespuesta(_respuestaSeleccionada.IdRespuesta, _respuestaSeleccionada.IdNota);
                listaRespuestas = coreForo.ObtnerRespuestasUnaNota(_notaSeleccionada.IdNota);
                dgRespuestas.ItemsSource = listaRespuestas;
                dgRespuestas.DataContext = listaRespuestas;
                txbtitulo.Text = _notaSeleccionada.Titulo;
            }
            CargarNotasActivas();



        }

        private void dgRespuestas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _respuestaSeleccionada = dgRespuestas.SelectedItem as RespuestaNota;
        }

        private void btnFiltroUsuario_Click(object sender, RoutedEventArgs e)
        {
            // en funcion de lo seleccionado en el combo box , se filtra el listado de notas
            Usuario usuario = cmbFiltroUsuario.SelectedItem as Usuario;

            listaNotas = coreForo.FiltrarNotasUnUsuario(usuario.IdUsuario);
            dgNotas.ItemsSource = listaNotas;
            dgNotas.DataContext = listaNotas;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            CargarNotasActivas();
        }
    }
}
