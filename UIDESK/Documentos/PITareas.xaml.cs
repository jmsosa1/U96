using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UIDESK.uc.Vehiculos.Notas;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para PITareas.xaml
    /// </summary>
    public partial class PITareas : MaterialWindow
    {
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        List<plan_inspeccion> _lista = new List<plan_inspeccion>();
        List<NotaDocuVh> _listaNotas= new List<NotaDocuVh>();
        int i;

        public PITareas(List<plan_inspeccion> plan_s, string _titulo)
        {
            InitializeComponent();
            _lista = plan_s;
            dgPITareas.ItemsSource = _lista;
            dgPITareas.DataContext = _lista;
            txbTitulo.Text = _titulo;
            txbCantidad.Text = _lista.Count.ToString();

            foreach (var item in _lista)
            {
                _listaNotas = bLLVehiculos.VehiculoDocNotas(item.Idplan, 2);
                if (_listaNotas.Count > 0)
                {
                    item.TieneNotas = true;
                }
            }
            
        }

     

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintPITareas printPITareas = new PrintPITareas(_lista, txbTitulo.Text);
            if (printPITareas.ShowDialog() == true)
            {

            }
        }

        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "Dominio";
            ws.Range["B1"].Value = "Marca";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Tare";
            ws.Range["E1"].Value = "GAP";
            ws.Range["F1"].Value = "Atributo Comparativo";
            i = 1;
            foreach (var item in _lista)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.Dominio;
                ws.Range["B" + i].Value = item.Marca;
                ws.Range["C" + i].Value = item.Modelo;
                ws.Range["D" + i].Value = item.Tarea;
                ws.Range["E" + i].Value = item.Gap;
                ws.Range["F" + i].Value = item.AtributoComparativo;
            }


        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        private void dgPITareas_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void btnAddNota_Click(object sender, RoutedEventArgs e)
        {
            plan_inspeccion plan = dgPITareas.SelectedItem as plan_inspeccion;
            AddNotaDoc nota = new AddNotaDoc(Contexto.CodUser, plan.Idplan, 2);
            if (nota.ShowDialog() == true)
            {
                MessageBox.Show("Se agrego la nota al documento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                
                _listaNotas = bLLVehiculos.VehiculoDocNotas(plan.Idplan, 2); // notas de plan de inspeccion
                dgNotadoc.ItemsSource = _listaNotas;
                dgNotadoc.DataContext = _listaNotas;
                foreach (var item in _lista)
                {
                    _listaNotas = bLLVehiculos.VehiculoDocNotas(item.Idplan, 2);
                    if (_listaNotas.Count > 0)
                    {
                        item.TieneNotas = true;
                    }
                }
                return;
            }
        }

        private void btnVerNotas_Click(object sender, RoutedEventArgs e)
        {
            plan_inspeccion plan = dgPITareas.SelectedItem as plan_inspeccion;
            if (plan != null)
            {
               _listaNotas = bLLVehiculos.VehiculoDocNotas(plan.Idplan,2); // notas de plan de inspeccion
                dgNotadoc.ItemsSource = _listaNotas;
                dgNotadoc.DataContext = _listaNotas;
            }
            else
            {
                MessageBox.Show("No se selecciono ningun documento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void btnDeleteNota_Click(object sender, RoutedEventArgs e)
        {
            NotaDocuVh _nota = dgNotadoc.SelectedItem as NotaDocuVh;
            bLLVehiculos.VehiculoDocDelete(_nota.IdNota);
            _listaNotas = bLLVehiculos.VehiculoDocNotas(_nota.IdRegistro, 1);

            dgNotadoc.ItemsSource = _listaNotas;
            dgNotadoc.DataContext = _listaNotas;
        }
    }
}
