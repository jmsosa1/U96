using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using System.Collections.Generic;
using System.Windows;


namespace UIDESK
{
    /// <summary>
    /// Lógica de interacción para PrincipalResultados.xaml
    /// </summary>
    public partial class PrincipalResultados : MaterialWindow
    {

        private List<INavigationItem> m_navigationItems;
        public List<INavigationItem> NavigationItems { get { return m_navigationItems; } }

        public PrincipalResultados()
        {
            /*
               m_navigationItems = new List<INavigationItem>(){
                   new FirstLevelNavigationItem(){Label="Home",Icon=PackIconKind.Home, NavigationItemSelectedCallback = item => new ucHomeRVh() },
                   new DividerNavigationItem(),
                   new SubheaderNavigationItem(){Subheader="Mantenimientos"},
                   new FirstLevelNavigationItem(){Label="Costo Horas ",  NavigationItemSelectedCallback = item => new ucCostoManteHs(), Icon=PackIconKind.CarCruiseControl},
                    new FirstLevelNavigationItem(){Label="Costo Kilometros ",  NavigationItemSelectedCallback = item => new ucCostoManteKm(), Icon=PackIconKind.CarCog},
                    new FirstLevelNavigationItem(){Label="Costos Anual ",  NavigationItemSelectedCallback = item => new ucDatosManteVhAnio(), Icon=PackIconKind.ChartBoxOutline},
                    new FirstLevelNavigationItem(){Label="Costo Anual Categorias ",  NavigationItemSelectedCallback = item => new ucDatosManteVhAnioCategorias(),Icon=PackIconKind.TableArrowRight },
                     new FirstLevelNavigationItem(){Label="Costo Anual Graficos ",  NavigationItemSelectedCallback = item => new ucDatosManteVhAnioGraficos(), Icon=PackIconKind.ChartBarStacked},
                      new FirstLevelNavigationItem(){Label="Costo Anual Individual ",  NavigationItemSelectedCallback = item => new ucDatosManteVhAnioIndividual(), Icon=PackIconKind.ChartBubble},
                   new DividerNavigationItem(),
                   new SubheaderNavigationItem(){Subheader="Costos Consumos"},
                   new FirstLevelNavigationItem(){Label="Total anual flota", NavigationItemSelectedCallback= item=> new ucDatosConsumoAnio(),Icon=PackIconKind.Kodi},
                   new FirstLevelNavigationItem(){Label="Total anual por categoria",NavigationItemSelectedCallback=item=> new ucDatosConsumoAnioCategorias(),Icon=PackIconKind.Laptop},
                   new FirstLevelNavigationItem(){Label="Total anual de un Vehiculo",NavigationItemSelectedCallback=item=> new ucDatosConsumoAnioIndividual(),Icon=PackIconKind.BookLocation},
                   new FirstLevelNavigationItem(){Label="Total mensual de flota", NavigationItemSelectedCallback= item=> new ucConsumoMensualFlota(), Icon=PackIconKind.LeadPencil},
                   new FirstLevelNavigationItem(){Label="Progresion mensual anual", NavigationItemSelectedCallback=item=> new ucProgresionConsumosMensual(),Icon=PackIconKind.CalendarMultipleCheck},
                   new FirstLevelNavigationItem(){Label="Progresion interanual",NavigationItemSelectedCallback=item=> new ucDatosConsumoAnioGraficos(),Icon=PackIconKind.ChartBarStacked},
                   new DividerNavigationItem(),
                   new SubheaderNavigationItem(){Subheader="Resultados"},
                   new DividerNavigationItem(),
                   new SubheaderNavigationItem(){Subheader="Situacion Operativa"}

               };*/
            InitializeComponent();
            //sideNav.DataContext = this;
            navigationDrawerNav.Items = m_navigationItems;
            navigationDrawerNav.DataContext = this;

            Loaded += LoadedHandler;
        }

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            navigationDrawerNav.SelectedItem = m_navigationItems[1];
            // sideNav.SelectedItem = m_navigationItems[1];
            m_navigationItems[1].IsSelected = true;
        }


        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            SelectNavigationItem(args.NavigationItem);
        }

        private void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                object newContent = navigationItem.NavigationItemSelectedCallback(navigationItem);

                if (contentControl.Content == null || contentControl.Content.GetType() != newContent.GetType())
                {
                    contentControl.Content = newContent;
                }
            }
            else
            {
                contentControl.Content = null;
            }

            if (appBar != null)
            {
                appBar.IsNavigationDrawerOpen = false;
            }
        }




    }
}
