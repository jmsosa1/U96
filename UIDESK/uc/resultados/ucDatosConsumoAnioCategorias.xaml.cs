using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.TextFormatting;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosConsumoAnioCategorias.xaml
    /// </summary>
    public partial class ucDatosConsumoAnioCategorias : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        List<ACC_CategoriaAnio> resumen_categorias = new List<ACC_CategoriaAnio>();

        public Func<double, string> Formatter { get; set; }
        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }


        public ucDatosConsumoAnioCategorias()
        {
            InitializeComponent();

            Formatter = value => value.ToString("C");
            // DataContext = this;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscarAnio.Text))
            {
                MessageBox.Show("Debe ingresar el numero del año a buscar", "Aviso", MessageBoxButton.OK);
                return;
            }

            values_lineas.Clear();
            tags_lineas.Clear();
            lvcCartesiano.Series.Clear();


            int _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text);
            resumen_categorias = coreVh.ResumenConsumoCategoriaAnio(_anioBuscar);

            foreach (var item in resumen_categorias)
            {
                values_lineas.Add(item.CConsumoCategoria);
                tags_lineas.Add(item.NombreCategoria);
            }

            Etiquetas = tags_lineas.ToArray();
            lvcCartesiano.Series = new SeriesCollection
            {
                new RowSeries{ Title="Consumos por categoria de vehiculo",Values=values_lineas,DataLabels=true,Foreground= System.Windows.Media.Brushes.Orange}
            };
            lvcCartesiano.DataContext = this;
            
            

        }
    }
}
