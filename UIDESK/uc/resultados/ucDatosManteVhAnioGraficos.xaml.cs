using BLL;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace UIDESK.uc.resultados
{
    /// <summary>
    /// Lógica de interacción para ucDatosManteVhAnioGraficos.xaml
    /// </summary>
    public partial class ucDatosManteVhAnioGraficos : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        List<ConsumoAnios> lista_consumos_anios = new List<ConsumoAnios>();

        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public SeriesCollection series { get; set; }
        public string[] EtiquetasX { get; set; }
        public Func<double, string> Formatter
        {
            get; set;
        }
        public ucDatosManteVhAnioGraficos()
        {
            InitializeComponent();
            lista_consumos_anios = coreVh.ResumenManteAniovsAnio();
            foreach (var item in lista_consumos_anios)
            {
                if (item.Anio > 2011)
                {


                    values_lineas.Add(item.CostoAnio);
                    tags_lineas.Add(item.Anio.ToString());
                }
            }
            EtiquetasX = new[] { "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021","2022","2023","2024" };

            series = new SeriesCollection
            {
                new ColumnSeries{Title="Costo Año vs Año",Values = values_lineas,DataLabels=true}
            };
            Formatter = value => value.ToString("C");

            DataContext = this;
        }
    }
}
