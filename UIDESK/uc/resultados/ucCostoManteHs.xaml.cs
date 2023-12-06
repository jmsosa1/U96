using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UIDESK.imprimir;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucCostoManteHs.xaml
    /// </summary>
    public partial class ucCostoManteHs : UserControl
    {
        BLLResultado bLL = new BLLResultado();
        BLLBase coreBase = new BLLBase();
        ObservableCollection<RelacionManteHs> rmh = new ObservableCollection<RelacionManteHs>();
        int i;

        public ICollectionView vistaDominio
        {
            get { return CollectionViewSource.GetDefaultView(rmh); }
        }
        Coeficiente _cof;

        public ucCostoManteHs()
        {
            InitializeComponent();
            rmh = bLL.RelManteHs();
            dgCostoManteHs.ItemsSource = rmh;
            dgCostoManteHs.DataContext = rmh;
            _cof = coreBase.BuscarUno(5); // antes de cargar la imagen del estado , debemos crear el objeto coeficiente
            // con el cual vamos a trabajar
            CargarImagenEstado();
        }


        #region Filtros

        private bool filtroDominio(object obj)
        {
            RelacionManteHs relacion = obj as RelacionManteHs;
            return relacion.Dominio.Contains(txtDominio.Text);
        }
        #endregion

        private void CargarImagenEstado()
        {
            foreach (var item in rmh)
            {
                if (item.Relacion > 0 && item.Relacion <= 1.75M)
                {

                    byte[] img = File.ReadAllBytes(@"imagenes\se-green.png");
                    item.ImageEstado = img;
                }
                else
                {
                    if (item.Relacion >= 1.75M && item.Relacion <= 3.5M)
                    {
                        //@"imagenes\nueva-tarea.png"
                        byte[] img = File.ReadAllBytes(@"imagenes\se-yelloy.png");
                        item.ImageEstado = img;
                    }
                    else
                    {

                        byte[] img = File.ReadAllBytes(@"imagenes\se-red.png");
                        item.ImageEstado = img;
                    }
                }
            }
        }

        private void BtnPrintReporte_Click(object sender, RoutedEventArgs e)
        {
            ImprimirManteVsHs imprimir = new ImprimirManteVsHs();
            imprimir.Show();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            vistaDominio.Filter = filtroDominio;
            dgCostoManteHs.ItemsSource = vistaDominio;
            dgCostoManteHs.DataContext = vistaDominio;
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
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

            ws.Range["A1"].Value = "Categoria";
            ws.Range["B1"].Value = "Dominio";
            ws.Range["C1"].Value = "Modelo";
            ws.Range["D1"].Value = "Anio modelo";
            ws.Range["E1"].Value = "Marca";
            ws.Range["F1"].Value = "CTM";
            ws.Range["G1"].Value = "Hs Acumulado";
            ws.Range["H1"].Value = "Relacion";
            ws.Range["I1"].Value = "Estado";
            i = 1;
            foreach (var item in rmh)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.Categoria;
                ws.Range["B" + i].Value = item.Dominio;
                ws.Range["C" + i].Value = item.Modelo;
                ws.Range["D" + i].Value = item.AnioModelo;
                ws.Range["E" + i].Value = item.Modelo;
                ws.Range["F" + i].Value = item.CostoManteAcu;
                ws.Range["G" + i].Value = item.HsAcumuladas;
                ws.Range["H" + i].Value = item.Relacion;
                //ws.Range["I" + i].Value = item.ImageEstado;
                // de acuerdo al valor de la relacion, cambiamos el color de fondo de la celda
                // ya que es mas facil que convertir la imagen asociada al objeto
                if (item.Relacion > 0 && item.Relacion <= 1.2M)
                {
                    ws.Range["I" + i].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                }
                else
                {
                    if (item.Relacion >= 1.2M && item.Relacion <= 2.4M)
                    {

                        ws.Range["I" + i].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    }
                    else
                    {

                        ws.Range["I" + i].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                }
            }


        }
    }
}
