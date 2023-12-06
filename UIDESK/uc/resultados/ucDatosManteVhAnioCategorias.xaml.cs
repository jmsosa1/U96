using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosManteVhAnioCategorias.xaml
    /// </summary>
    public partial class ucDatosManteVhAnioCategorias : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        List<ACMVH_CategoriaAnio> resumen_categorias = new List<ACMVH_CategoriaAnio>();
        List<ACMVH_CategoriaAnio> resumen_incidencia_categoria = new List<ACMVH_CategoriaAnio>();
        int _anioBuscar;
        public Func<double, string> Formatter { get; set; }
        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }
        public ucDatosManteVhAnioCategorias()
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


            _anioBuscar = Convert.ToInt32(txtBuscarAnio.Text);
            resumen_categorias = coreVh.ResumenMantevhCategoriasAnio(_anioBuscar);

            //bucle que crea las series del grafico 
            foreach (var item in resumen_categorias)
            {
                values_lineas.Add(item.CostoTotalCategoria);
                tags_lineas.Add(item.NombreCategoria);
            }

            Etiquetas = tags_lineas.ToArray();
            lvcCartesiano.Series = new SeriesCollection
            {
                new RowSeries{ Title="Costos Mantenimiento por categoria de vehiculo",Values=values_lineas,DataLabels=true}
            };
            lvcCartesiano.DataContext = this;
            //pasamos el items source al datagrid planilla
            foreach (var item in resumen_categorias)
            {
                item.CostoPromedioCategoria = item.CostoTotalCategoria / item.CantidadIncidencias;
            }
            dgPlanillaCostos.ItemsSource = resumen_categorias;
            dgPlanillaCostos.DataContext = resumen_categorias;
        }



        private void dgPlanillaCostos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ACMVH_CategoriaAnio aCMVH = dgPlanillaCostos.SelectedItem as ACMVH_CategoriaAnio;
            resumen_incidencia_categoria = coreVh.ResumenIncidenciasUnaCategoria(_anioBuscar, aCMVH.IdCateManteVh);
            dgIncidendiasCategoriaManteVh.ItemsSource = resumen_incidencia_categoria;
            dgIncidendiasCategoriaManteVh.DataContext = resumen_incidencia_categoria;

        }

    }
}
