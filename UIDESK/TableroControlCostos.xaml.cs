using MaterialDesignExtensions.Controls;
using System.Windows;
using System.Windows.Controls;
using UIDESK.uc.tablerocostos;

namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para TableroControlCostos.xaml
    /// </summary>
    public partial class TableroControlCostos : MaterialWindow 
    {
        public TableroControlCostos()
        {
            InitializeComponent();
            ucResultadoGeneralCostos uc = new ucResultadoGeneralCostos();
            cc.Content = uc;
            txbTitulo.Text = "Inicio";
        }

        private void lsvNavMante_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "IMHome":
                    ucResultadoGeneralCostos uc = new ucResultadoGeneralCostos();
                    cc.Content = uc;
                    txbTitulo.Text = "Inicio";
                    break;
                case "IMComparativaAnios":
                    ucProgresionCostosInteranual ucProgresion = new ucProgresionCostosInteranual();
                    cc.Content = ucProgresion;
                    txbTitulo.Text = "Progresion Interanual";
                    break;
                case "IMProgresionMensual":
                    ucProgresionCostosMensual ucProgresionMes = new ucProgresionCostosMensual();
                    cc.Content = ucProgresionMes;
                    txbTitulo.Text = "Progresion Mensual";
                    break;
                case "IMCostoInversiones":
                    ucCostoInversiones ucCostoInversiones = new ucCostoInversiones();
                    cc.Content = ucCostoInversiones;
                    txbTitulo.Text = "Costo Inversiones ";
                    
                    break;
               
                case "IMCostoMantenimientos":
                    ucCostoMantenimientos ucCostoMantenimientos = new ucCostoMantenimientos();
                    cc.Content = ucCostoMantenimientos;
                    txbTitulo.Text = "Costo Mantenimientos";
                    break;
                default:
                    break;
            }
        }
    }
}
