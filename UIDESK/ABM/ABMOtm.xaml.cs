using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMOtm.xaml
    /// </summary>
    public partial class ABMOtm : MaterialWindow 
    {
        BLLGestion gestion = new BLLGestion();
        BLLVehiculos bLL = new BLLVehiculos();
        Vehiculo v = new Vehiculo();
        ObservableCollection<OtmDetalle> otm_detalle = new ObservableCollection<OtmDetalle>();
        List<CategoriaManteVh> categoriaMantes = new List<CategoriaManteVh>();
        Otm _otm = new Otm(); // objeto otm que almacena el parametro del constructor
        OtmDetalle _detalle = new OtmDetalle(); // objeto otmdetalle que almacena el parametro del constructor

        string _operacion = "";
        public ABMOtm(Otm otm, OtmDetalle detalle)
        {
            InitializeComponent();
            categoriaMantes = bLL.ListarCateMante();
            cmbCateMante.ItemsSource = categoriaMantes;
            _otm = otm;

            if (_operacion == "M") // si es una modificacion
            {


                if (detalle != null) // si el objeto que pasamos como detalle no es null...
                {
                    otm_detalle.Add(detalle);
                }
            }

            DataContext = otm;
            dgDetalle.ItemsSource = otm_detalle;
            dgDetalle.DataContext = otm_detalle;

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtDominio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                v = bLL.VehiculoBuscarUnDominio(txtDominio.Text);
                if (v == null)
                {
                    MessageBox.Show("No existe el vehiculo", "Aviso", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    txtModelo.Text = v.Modelo;
                    txtDescripcion.Text = v.Descripcion;
                    txtMarca.Text = v.NomMarca;
                }
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtDominio.Text))
            {
                MessageBox.Show("Debe ingresar un dominio de vehiculo", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MessageBox.Show("Debe ingresar un titulo para la OTM", "Aviso", MessageBoxButton.OK);
                return;
            }
            if (dtpFnecesidad.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha de necesidad", "Aviso", MessageBoxButton.OK);
                return;
            }

            if (dgDetalle.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar un item al menos al detalle", "Aviso", MessageBoxButton.OK);
                return;

            }

            //damos de alta la nueva tarea


            _otm.Titulo = txtTitulo.Text;
            _otm.Nota = txtNota.Text;
            _otm.FNecesidad = dtpFnecesidad.SelectedDate;

            _otm.UsuarioCreador = Contexto.CodUser;
            byte[] img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\nueva-tarea.png");
            _otm.Img_Estado = img;
            _otm.CantItems = otm_detalle.Count;
            int fila = gestion.OTMAlta(_otm);
            // zona del detalle
            //obtenemos el ultimo id de otm
            Otm ultimaId = new Otm();
            ultimaId = gestion.ObtenerUltimoIdOtm();
            //grabamos el detalle
            foreach (var item in otm_detalle)
            {
                item.IdOtm = ultimaId.IdOtm;
                int detalle = gestion.DetalleOtmAlta(item);
            }

            DialogResult = true;
            this.Close();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtDescriActividad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescriActividad.SelectAll();
        }

        private void btnSeleccionarDrawBotton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCateMante.SelectedItem != null)
            {


                CategoriaManteVh c = cmbCateMante.SelectedItem as CategoriaManteVh;
                OtmDetalle d = new OtmDetalle();
                d.NumItem = otm_detalle.Count + 1;
                d.DescripcionItem = txtDescriActividad.Text;
                d.EstadoItem = "Pendiente";
                d.IdCateMante = c.IdCateMante;
                d.NomCateMante = c.NomCate;
                d.Idvh = v.IdVh;
                otm_detalle.Add(d);
                dgDetalle.DataContext = otm_detalle;
                dgDetalle.ItemsSource = otm_detalle;
                txtDescriActividad.Text = "";
                btnCerrarDrawBotton.Command.Execute(Dock.Bottom);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una categoria de mantenimiento", "Aviso", MessageBoxButton.OK);
                return;
            }
        }

        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            int indiceItemABorrar;
            indiceItemABorrar = dgDetalle.SelectedIndex;
            otm_detalle.RemoveAt(indiceItemABorrar);
            for (int i = 0; i < otm_detalle.Count; i++)
            {
                otm_detalle[i].NumItem = i + 1;
            }

            dgDetalle.ItemsSource = otm_detalle;
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
    }
}
