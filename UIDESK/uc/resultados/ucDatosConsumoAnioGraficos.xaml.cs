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
    /// Lógica de interacción para ucDatosConsumoAnioGraficos.xaml
    /// </summary>
    public partial class ucDatosConsumoAnioGraficos : UserControl
    {
        BLLVehiculos coreVh = new BLLVehiculos();
        List<ConsumoAnios> lista_consumos_anios = new List<ConsumoAnios>();

        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public SeriesCollection series { get; set; }
        public string[] EtiquetasX { get; set; }
        public Func<double, string> Formatter { get; set; }

        public ucDatosConsumoAnioGraficos()
        {
            InitializeComponent();
            lista_consumos_anios = coreVh.ResumenConsumoAniovsAnio();
            foreach (var item in lista_consumos_anios)
            {
                values_lineas.Add(item.CostoAnio);
                tags_lineas.Add(item.Anio.ToString());
            }
            EtiquetasX = new[] { "2018", "2019", "2020", "2021", "2022" };

            series = new SeriesCollection
            {
                new ColumnSeries{Title="Consumo Año vs Año",Values = values_lineas,DataLabels=true}
            };
            Formatter = value => value.ToString("C");

            DataContext = this;
        }
    }
}
