using ENTIDADES;
using MaterialDesignExtensions.Controls;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using System.Windows;
using UIDESK.uc.Vehiculos.Notas;
using BLL;
using System.Collections.Generic;

namespace UIDESK.Documentos
{
    /// <summary>
    /// Lógica de interacción para VhDocVencida.xaml
    /// </summary>
    public partial class VhDocVencida : MaterialWindow
    {
        BLLVehiculos bLLVehiculos = new BLLVehiculos();
        List<VehiculoDocu> _lista = new List<VehiculoDocu>();
        List<NotaDocuVh> _listaNotas = new List<NotaDocuVh>();
        NotaDocuVh _nota = new NotaDocuVh();
        //VehiculoDocu vhdoc = new VehiculoDocu();
        int i;
        public VhDocVencida(List<VehiculoDocu> vehiculoDocus, string titulo)
        {
            InitializeComponent();
            _lista = vehiculoDocus;
            txbTitulo.Text = titulo;
            dgDocVencida.ItemsSource = _lista;
            dgDocVencida.DataContext = _lista;
            txbCantidad.Text = _lista.Count.ToString();
            foreach (var item in _lista)
            {
                _listaNotas = bLLVehiculos.VehiculoDocNotas(item.IdVhDoc, 1);
                if (_listaNotas.Count > 0)
                {
                    item.TieneNotas = true;
                }
            }

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintVhDocVencida printVhDocVencida = new PrintVhDocVencida(_lista, txbTitulo.Text);
            if (printVhDocVencida.ShowDialog() == true)
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
            ws.Range["B1"].Value = "Descripcion";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Documento";
            ws.Range["E1"].Value = "Numero";
            ws.Range["F1"].Value = "Fecha Vencimiento";
            i = 1;
            foreach (var item in _lista)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.DominioVh;
                ws.Range["B" + i].Value = item.DescriVh;
                ws.Range["C" + i].Value = item.ModeloVh;
                ws.Range["D" + i].Value = item.NombreDocu;
                ws.Range["E" + i].Value = item.NumeroDoc;
                ws.Range["F" + i].Value = item.FVencimiento;
            }


        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcelFile();
        }

        private void btnAddNota_Click(object sender, RoutedEventArgs e)
        {
            VehiculoDocu vhdoc = dgDocVencida.SelectedItem as VehiculoDocu;
            AddNotaDoc nota = new AddNotaDoc(Contexto.CodUser,vhdoc.IdVhDoc,1);
            if (nota.ShowDialog()== true)
            {
                MessageBox.Show("Se agrego la nota al documento", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
               
                _listaNotas = bLLVehiculos.VehiculoDocNotas(vhdoc.IdVhDoc, 1); // notas de plan de inspeccion
                dgNotadoc.ItemsSource = _listaNotas;
                dgNotadoc.DataContext = _listaNotas;
                foreach (var item in _lista)
                {
                    _listaNotas = bLLVehiculos.VehiculoDocNotas(item.IdVhDoc, 2);
                    if (_listaNotas.Count > 0)
                    {
                        item.TieneNotas = true;
                    }
                }
                return;
            }
        }

        private void dgDocVencida_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void btnVerNotas_Click(object sender, RoutedEventArgs e)
        {
            VehiculoDocu vhdoc = dgDocVencida.SelectedItem as VehiculoDocu;
            if (vhdoc != null)
            {
                _listaNotas = bLLVehiculos.VehiculoDocNotas(vhdoc.IdVhDoc, 1); // notas de plan de inspeccion
                dgNotadoc.ItemsSource = _listaNotas;
                dgNotadoc.DataContext = _listaNotas;
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
