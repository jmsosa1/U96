using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucResumenPrincipal.xaml
    /// </summary>
    public partial class ucResumenPrincipal : UserControl
    {
        #region Declarativas
        BLLObras coreObras = new BLLObras();
        List<BalanceObraTiposVehiculo> lista_obra_tipoVh = new List<BalanceObraTiposVehiculo>();
        List<BalanceObraProductos> lista_obra_tipoP = new List<BalanceObraProductos>();
        List<BalanceObraEmpleados> lista_empleados = new List<BalanceObraEmpleados>();
        List<Mante_P> lista_mantep = new List<Mante_P>();
        List<Mante_vh> lista_mantevh = new List<Mante_vh>();
        List<TipoGastosObra> lista_gastos = new List<TipoGastosObra>();

        int imputacionObra;
        CultureInfo ci = new CultureInfo("es-Ar");
        #endregion
        public ucResumenPrincipal(int _impu)
        {
            InitializeComponent();
            imputacionObra = _impu;
            //llenamos las listas con los objetos desde la base de datos
            lista_obra_tipoVh = coreObras.BalanceTiposVehiculo(imputacionObra);
            lista_obra_tipoP = coreObras.BalanceTipoProductos(imputacionObra);
            lista_empleados = coreObras.ListarEmpleadosCostoUnaObra(imputacionObra);
            lista_mantep = coreObras.ListarGastosMantenimientoProductos(imputacionObra);
            lista_mantevh = coreObras.ListarGastosMantenimientoVehiculos(imputacionObra);
            //seteamos las grid
            dgTipoProducto.ItemsSource = lista_obra_tipoP;
            dgTipoProducto.DataContext = lista_obra_tipoP;
            dgTipoVh.ItemsSource = lista_obra_tipoVh;
            dgTipoVh.DataContext = lista_obra_tipoVh;
            dgEmpleados.ItemsSource = lista_empleados;
            dgEmpleados.DataContext = lista_empleados;

            //calculamos los totales
            CalcularTotales();

        }

        private void CalcularTotales()
        {
            decimal _totalProductos = 0;
            decimal _totalCostoGastos = 0;
            decimal _totalgastosP = 0;
            decimal _totalgastoVh = 0;
            int _totalVh = 0;
            foreach (var item in lista_obra_tipoP)
            {
                _totalProductos = _totalProductos + item.CostoEntregas;
            }

            foreach (var item in lista_obra_tipoVh)
            {
                _totalVh = _totalVh + item.CantidadAsignada;
            }

            txbCTProductos.Text = _totalProductos.ToString("C", ci);
            txbCantidadVh.Text = _totalVh.ToString();
            foreach (var item in lista_mantep)
            {
                _totalgastosP = _totalgastosP + item.ImporteFactura;
            }

            foreach (var item in lista_mantevh)
            {
                _totalgastoVh = _totalgastoVh + item.Importe;
            }

            TipoGastosObra tipoGastosObra1 = new TipoGastosObra();
            tipoGastosObra1.NombreGasto = "Mante. Herramientas";
            tipoGastosObra1.CostoAcumulado = _totalgastosP;
            lista_gastos.Add(tipoGastosObra1);
            TipoGastosObra tipoGastoObras2 = new TipoGastosObra();
            tipoGastoObras2.NombreGasto = "Mante. Vehiculos";
            tipoGastoObras2.CostoAcumulado = _totalgastoVh;
            lista_gastos.Add(tipoGastoObras2);
            dgTipoGastos.ItemsSource = lista_gastos;
            dgTipoGastos.DataContext = lista_gastos;
            _totalCostoGastos = _totalgastosP + _totalgastoVh;
            txbCostoGastos.Text = _totalCostoGastos.ToString("C", ci);
            txbCantEmpleados.Text = lista_empleados.Count.ToString();

        }

        private void dgTipoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BalanceObraProductos bl = new BalanceObraProductos();
            if (bl != null)
            {

                //ucResumenCatPro uc = new ucResumenCatPro(bl.IdTipoPro, imputacionObra);

            }
        }


    }
}
