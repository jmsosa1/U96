using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.Drawing;
using System.ComponentModel;
using UIDESK.ABM;
using UIDESK.Documentos;

namespace UIDESK.uc.Laboratorio
{
    /// <summary>
    /// Lógica de interacción para FichaControlCalibraciones.xaml
    /// </summary>
    public partial class FichaControlCalibraciones : MaterialWindow
    {
        BLLLaboratorio coreLab = new BLLLaboratorio();
        Producto _instrumento = new Producto();
        CalibracionInstrumento _calibracion = new CalibracionInstrumento();
        CalibracionInstrumento _caliSelecDg = new CalibracionInstrumento();
        List<CalibracionInstrumento> _listaCalibraciones = new List<CalibracionInstrumento>();
        CultureInfo ci = new CultureInfo("es-AR");

        public FichaControlCalibraciones(Producto producto)
        {
            InitializeComponent();
            _instrumento = producto;
            
            DataContext = _instrumento;
            _listaCalibraciones = coreLab.ListarTodasLasCalibracionesUnInstrumento(_instrumento.IdProducto);
            //chekeamos que la lista no sea null
            if (_listaCalibraciones.Count > 0)
            {
                dgDetalle.ItemsSource = _listaCalibraciones;
                dgDetalle.DataContext = _listaCalibraciones;
                //buscamos la calibracion que no este vencida y que este activa
               _calibracion = _listaCalibraciones.FirstOrDefault(x => x.EstadoVencimiento == 13 && x.EstadoCalibracion==1);
                if (_calibracion != null)
                {
                    txtValidez.Text = _calibracion.ValidezDias.ToString();
                    txtVencimiento.Text = _calibracion.VencimientoActual.Value.ToShortDateString();
                }
                else
                {// si la calibracion esta vencida, entonces buscamos la misma pero que sea la activa o ultima registrada
                    _calibracion = _listaCalibraciones.FirstOrDefault(x => x.EstadoVencimiento == 14 && x.EstadoCalibracion==1);
                    txtValidez.Text = _calibracion.ValidezDias.ToString();
                    txtVencimiento.Text = _calibracion.VencimientoActual.Value.ToShortDateString();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("No existen calibraciones para el instrumento elegido.Desea agregar una?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ABMCalibracion calibracion = new ABMCalibracion(_instrumento);
                    calibracion.ShowDialog();
                }
                else
                {
                    return;
                }
            }


        }

        private void dgDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _caliSelecDg = dgDetalle.SelectedItem as CalibracionInstrumento;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintFichaControlCalibraciones printFicha = new PrintFichaControlCalibraciones(_instrumento,_calibracion,  _listaCalibraciones);
            if (printFicha.ShowDialog() == true)
            {

            }
        }

        private void btnDeleteCalibracion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea Eliminar el item?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result== MessageBoxResult.Yes)
            {
                //borrar calibracion
                coreLab.BorrarCalibracion(_caliSelecDg.IdCalibracion);
                _listaCalibraciones = coreLab.ListarTodasLasCalibracionesUnInstrumento(_instrumento.IdProducto);
                dgDetalle.ItemsSource = _listaCalibraciones;
                dgDetalle.DataContext = _listaCalibraciones;

            }
        }
    }
}
