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
using BLL;
using ENTIDADES;

namespace UIDESK.uc.Vehiculos.Notas
{
    /// <summary>
    /// Lógica de interacción para AddNotaDoc.xaml
    /// </summary>
    public partial class AddNotaDoc : Window
    {
        BLLVehiculos coreVehiculos = new BLLVehiculos();
        NotaDocuVh notaDocuVh = new NotaDocuVh();
        int _idusuario, _idregistro, _idtiponota;
        
        public AddNotaDoc(int idusuario , int idregistro, int idtiponota) // pasamos los parametros que identifican la nota
        {
            InitializeComponent();
            _idusuario = idusuario;
            _idregistro = idregistro;
            _idtiponota = idtiponota;
            notaDocuVh.IdUsuario = _idusuario;
            notaDocuVh.IdRegistro = _idregistro;
            notaDocuVh.FechaAlta = DateTime.Today.Date;
            notaDocuVh.IdTipoNota = _idtiponota;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContenido.Text))
            {
                MessageBox.Show("El contenido no puede estar en blanco", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                // si esta todo bien grabamos
                notaDocuVh.Contenido = txtContenido.Text;
                coreVehiculos.VehiculoDocAltaNota(notaDocuVh);
                DialogResult = true;
            }
        }
    }
}
