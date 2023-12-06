using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Linq;
using System.Linq.Expressions;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucVehiculoActualidad.xaml
    /// </summary>
    public partial class ucVehiculoActualidad : UserControl
    {
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        ObservableCollection<Asignacion_vh> asignacion_Vhs = new ObservableCollection<Asignacion_vh>();
        ObservableCollection<Asignacion_vh>asignacionObra = new ObservableCollection<Asignacion_vh>();
        List<CategoriaVh> categoriaVhs = new List<CategoriaVh>();
        string _dominioFinalizar;
        Obra _obra = new Obra();
        BLLObras coreObra = new BLLObras();
        CultureInfo ci = new CultureInfo("es-AR");

        public ICollectionView vistaAsignaciones
        {
            get { return CollectionViewSource.GetDefaultView(asignacion_Vhs); }
        }

        public ucVehiculoActualidad()
        {
            InitializeComponent();

            vistaAsignaciones.Filter = new Predicate<object>(filtroImputacion);
            vistaAsignaciones.Filter = new Predicate<object>(filtroDominio);
            vistaAsignaciones.Filter = new Predicate<object>(filtroResponsable);
            asignacion_Vhs = bLLVehiculos.ListarAsignaciones();
            categoriaVhs = bLLVehiculos.VehiculosListarCategoria();
            cmbCategoria.ItemsSource = categoriaVhs;
            dgActualidadVh.DataContext = asignacion_Vhs;
            dgActualidadVh.ItemsSource = asignacion_Vhs;
        }

        private bool filtroResponsable(object obj)
        {
            Asignacion_vh asignacion = obj as Asignacion_vh;
            return asignacion.NombreEmpleado.Contains("");
        }

        private bool filtroDominio(object obj)
        {
            Asignacion_vh asignacion = obj as Asignacion_vh;
            return asignacion.Dominio.Contains(txtBuscar.Text);
        }

        private bool filtroImputacion(object obj)
        {
            Asignacion_vh asignacion = obj as Asignacion_vh;
            if (string.IsNullOrEmpty(txtImputacion.Text))
            {
                return false;
            }
            else
            {
                int _imputacion = Convert.ToInt32(txtImputacion.Text);
                return asignacion.Imputacion == _imputacion && asignacion.SituacionAsignacion == "En Curso";
            }
        }

        private void CalcularResultadosVista()
        {
            int _ct = 0; // contador de registros
            foreach (var item in vistaAsignaciones)
            {
                _ct = _ct + 1;
            }
            txtRegistros.Text = _ct.ToString();
        }

        private void btnCerrarDetalle_Click(object sender, RoutedEventArgs e)
        {
            dgActualidadVh.SelectedIndex = -1;

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            asignacion_Vhs = bLLVehiculos.ListarTodasAsignaciones();
            vistaAsignaciones.Filter = filtroDominio;
            dgActualidadVh.DataContext = vistaAsignaciones;
            dgActualidadVh.ItemsSource = vistaAsignaciones;

        }

        private void dgActualidadVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Asignacion_vh asignacion = new Asignacion_vh();
            asignacion = dgActualidadVh.SelectedItem as Asignacion_vh;
            if (asignacion != null)
            {
                ucDetalleFilaAsignacion._idasig = asignacion.IdAsig;
            }
        }

        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaVh categoriaVh = new CategoriaVh();
            categoriaVh = cmbCategoria.SelectedItem as CategoriaVh;
            if (categoriaVh != null)
            {
                asignacion_Vhs = bLLVehiculos.ListarAsignacionesEnCursoPorCategoria(categoriaVh.IdCateVh);
                dgActualidadVh.ItemsSource = asignacion_Vhs;
                txtRegistros.Text = asignacion_Vhs.Count.ToString();
            }
            else
            {
                return;
            }
        }

        private void chkFiltroCategoria_Checked(object sender, RoutedEventArgs e)
        {
            cmbCategoria.IsEnabled = true;
            cmbCategoria.SelectedItem = null;
        }

        private void chkFiltroCategoria_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbCategoria.IsEnabled = false;

        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {



        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Asignacion_vh asignacion = new Asignacion_vh();
            asignacion = dgActualidadVh.SelectedItem as Asignacion_vh;
            _dominioFinalizar = asignacion.Dominio;

            MessageBoxResult messageBoxResult;
            messageBoxResult = MessageBox.Show("Desea Eliminar la asignacion del vehiculo: " + _dominioFinalizar + " ", "Aviso", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                int filaborrada = bLLVehiculos.BorrarAsignacion(asignacion.IdAsig);
            }
            asignacion_Vhs = bLLVehiculos.ListarAsignaciones();

            dgActualidadVh.ItemsSource = asignacion_Vhs;
        }


        private void ChkImputacion_Checked(object sender, RoutedEventArgs e)
        {
            txtImputacion.Text = "";
            txtImputacion.IsEnabled = true;
            txtImputacion.Focus();
        }

        private void ChkImputacion_Unchecked(object sender, RoutedEventArgs e)
        {
            // si el check de imputacion no esta clickeado entonces volvemos a la vista general
            txtImputacion.Text = "";
            txtImputacion.IsEnabled = false;
            asignacion_Vhs = bLLVehiculos.ListarAsignaciones();
            dgActualidadVh.DataContext = asignacion_Vhs;
            dgActualidadVh.ItemsSource = asignacion_Vhs;

        }



        private void TxtImputacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // traigo todas las asignaciones y le aplico el filtro de dominio

                vistaAsignaciones.Filter = filtroImputacion;
                dgActualidadVh.DataContext = vistaAsignaciones;
                dgActualidadVh.ItemsSource = vistaAsignaciones;
                if (vistaAsignaciones != null && vistaAsignaciones.CanGroup == true)
                {
                    vistaAsignaciones.GroupDescriptions.Clear();
                    vistaAsignaciones.GroupDescriptions.Add(new PropertyGroupDescription("Categoria"));
                }
                int _imputacion = Convert.ToInt32(txtImputacion.Text);
                _obra = coreObra.BuscarImputacion(_imputacion);
                foreach (var item in asignacion_Vhs)
                {
                    if (item.Imputacion == _obra.Imputacion)
                    {
                        asignacionObra.Add(item);
                    }
                }
                CalcularResultadosVista();
            }
        }





        private void BtnInformeObra_Click(object sender, RoutedEventArgs e)
        {
            //este es el procedimiento para imprimir el informe de vehiculos actuales en la obra
            //int _imputacion = Convert.ToInt32(txtImputacion.Text);
            //string _url = "http://pc-128/reports/report/ResultadosVehiculos/AsignacionesActivasUnaObra?imputacion= " + _imputacion;
            //System.Diagnostics.Process.Start(_url);
            GenerarExcelFileObra();
        }

        private void GenerarExcelFileObra()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //titulo del resumen en excel
            ws.Range["A1"].Value = "Informe de obra - Vehiculos  ";
            ws.Range["A1"].Font.Size = 12;
            ws.Range["A1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
            ws.Range["A2"].Value = "Obra :";
            ws.Range["B2"].Value = _obra.Imputacion;
            ws.Range["C2"].Value = "Nombre:";
            ws.Range["D2"].Value = _obra.NombreObra;
            ws.Range["C2"].Value = "Cliente:";
            ws.Range["D2"].Value = _obra.Cliente;

           // ws.Range["A2", "B2"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.OrangeRed);
           
           // ws.Range["A4", "H4"].Font.Bold = true;
            // pasamos los valores para este rango iterando la lista de totales
            int j = 5; //  indicador de posicion de celda
                       //para hacer un corte y control en excel vamos a hacer lo siguiente:
                       //el corte sera por categoria y se indicara dominio, y demas campos
                       //usamos linq y otros artilugios


            int _categoriaCorte = 0; //  variable de control de corte
            foreach (var itemObra in asignacionObra) // recorremos los vehiculos en la lista de asignaciones de obra
            {
                if (_categoriaCorte != itemObra.IdCatevh) // si la variable de corte no es igual al valor de corte
                {
                    _categoriaCorte = itemObra.IdCatevh; //se le asigna el nuevo valor

                    // armamos una lista con los elementos del corte 
                    var lista = from a in asignacionObra
                                where a.IdCatevh == _categoriaCorte
                                select a;
                    //seteamos el valor del nombre de la categoria (en este caso es el campo del corte
                    ws.Range["A" + j].Value = itemObra.Categoria;

                    j++; // aumentamos una posicion el indice de celda
                         // pasamos los campos de la categoria
                         //recorremos la lista conseguida para ese valor de corte 
                    foreach (var item in lista)
                    {
                        ws.Range["A" + j].Value = item.Dominio;
                        ws.Range["B" + j].Value = item.Modelo;
                        ws.Range["C" + j].Value = item.SituacionAsignacion;
                        ws.Range["D" + j].Value = item.FechaIn;
                        ws.Range["E" + j].Value = item.FechaFin;

                        j++;
                    }
                    j++;
                }
               
                
            }
        }

        private void BtnInformeEncargado_Click(object sender, RoutedEventArgs e)
        {
            //este es el procedimiento para imprimir el informe de vehiculos actuales para un encargado

        }


    }
}
