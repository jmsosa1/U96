using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.Laboratorio
{
    /// <summary>
    /// Lógica de interacción para InstrumentosVencidos.xaml
    /// </summary>
    public partial class InstrumentosVencidos : System.Windows.Window
    {
        BLLProducto coreProducto = new BLLProducto();
        ObservableCollection<Producto> lista_instrumentos = new ObservableCollection<Producto>();
       
        public InstrumentosVencidos( ObservableCollection<Producto> productos)
        {
            InitializeComponent();
            lista_instrumentos = productos;
            dgDocVencida.ItemsSource = lista_instrumentos;
            dgDocVencida.DataContext = lista_instrumentos;
          
            txbCantidad.Text = lista_instrumentos.Count.ToString();
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        private void GenerateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "Id";
            ws.Range["B1"].Value = "Nombre";
            ws.Range["C1"].Value = "Cod Inventario";
            ws.Range["D1"].Value = "Marca";
            ws.Range["E1"].Value = "Modelo";
            ws.Range["F1"].Value = "N Serie";
            ws.Range["G1"].Value = "Estado";
            ws.Range["H1"].Value = "Situacion";

            int i = 1;
            foreach (var item in lista_instrumentos)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.IdProducto;
                ws.Range["B" + i].Value = item.Nombre;
                ws.Range["C" + i].Value = item.CodInventario;
                ws.Range["D" + i].Value = item.Marca;
                ws.Range["E" + i].Value = item.Modelo;
                ws.Range["F" + i].Value = item.NumSerie;
                ws.Range["J" + i].Value = item.EstadoInstrumento;
                ws.Range["H" + i].Value = item.Situacion;

            }


        }
        private void dgDocVencida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
