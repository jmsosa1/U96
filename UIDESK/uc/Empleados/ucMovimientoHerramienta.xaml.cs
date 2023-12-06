using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDESK.uc.Empleados
{
    /// <summary>
    /// Lógica de interacción para ucMovimientoHerramienta.xaml
    /// </summary>
    public partial class ucMovimientoHerramienta : UserControl
    {
       public static ObservableCollection<BalanceEmpleado> _balance { get;set; }
        public static int idherra { get; set; }
      
        public ucMovimientoHerramienta()
        {
            InitializeComponent();
            //_balance = balance;
            
            dgMoviHerramienta.ItemsSource = _balance.Where(z=> z.IdProducto == idherra);
            dgMoviHerramienta.DataContext = _balance;   


        }
    }
}
