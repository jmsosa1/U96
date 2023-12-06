using BLL;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucBalanceObra.xaml
    /// </summary>
    public partial class ucBalanceObra : UserControl
    {
        BLLObras coreObras = new BLLObras();


        int i;
        int imputacion_obra;
        public ucBalanceObra()
        {
            InitializeComponent();
        }

        private void btnBuscarObra_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtImputacionObra.Text))
            {
                MessageBox.Show("Debe ingresar una imputacion de obra", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                imputacion_obra = Convert.ToInt32(txtImputacionObra.Text);
                //ucResumenPrincipal uc = new ucResumenPrincipal(imputacion_obra);
                ucResumenPrincipal uc = new ucResumenPrincipal(imputacion_obra);
                ccPrincipal.Content = uc;
                Obra obra = coreObras.BuscarImputacion(imputacion_obra);
                txtCliente.Text = obra.Cliente;
                txtNombreObra.Text = obra.NombreObra;
                btnDetProductos.IsEnabled = true;
                btnDetEmpleados.IsEnabled = true;
                btnDetGastos.IsEnabled = true;
                btnDetVehiculos.IsEnabled = true;
                //btnExportarExcel.IsEnabled = true;

            }

        }

        private void btnDetProductos_Click(object sender, RoutedEventArgs e)
        {
            ucResumenDetalleProductos uc = new ucResumenDetalleProductos(imputacion_obra);
            ccPrincipal.Content = uc;
        }

        private void btnDetEmpleados_Click(object sender, RoutedEventArgs e)
        {
            ucResumenDetalleEmpleados uc = new ucResumenDetalleEmpleados(imputacion_obra);
            ccPrincipal.Content = uc;
        }

        private void btnDetVehiculos_Click(object sender, RoutedEventArgs e)
        {
            ucResumenDetalleVehiculos uc = new ucResumenDetalleVehiculos(imputacion_obra);
            ccPrincipal.Content = uc;

        }

        private void btnDetGastos_Click(object sender, RoutedEventArgs e)
        {
            ucResumenDetalleGastos uc = new ucResumenDetalleGastos(imputacion_obra);
            ccPrincipal.Content = uc;

        }

   
        private void GenerateExcelFile()
        {
            //inicializar variables
            List<BalanceObraTiposVehiculo> lista_obra_tipoVh = new List<BalanceObraTiposVehiculo>();
            List<BalanceObraProductos> lista_obra_tipoP = new List<BalanceObraProductos>();
            List<BalanceObraEmpleados> lista_empleados = new List<BalanceObraEmpleados>();
            List<Mante_P> lista_mantep = new List<Mante_P>();
            List<Mante_vh> lista_mantevh = new List<Mante_vh>();
            List<TipoGastosObra> lista_gastos = new List<TipoGastosObra>();
            // conseguir los datos
            lista_obra_tipoVh = coreObras.BalanceTiposVehiculo(imputacion_obra);
            lista_obra_tipoP = coreObras.BalanceTipoProductos(imputacion_obra);
            lista_empleados = coreObras.ListarEmpleadosCostoUnaObra(imputacion_obra);
            lista_mantep = coreObras.ListarGastosMantenimientoProductos(imputacion_obra);
            lista_mantevh = coreObras.ListarGastosMantenimientoVehiculos(imputacion_obra);
            //ahora generemamos el excel
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1]; // hoja de herramientas
            Worksheet ws_2 = wb.Worksheets[2]; // hoja vehiculos
            Worksheet ws_3 = wb.Worksheets[3]; // hoja empleados
            //Worksheets ws_4 = wb.Worksheets[4];// hoja gastos
            //DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "Tipo";
            ws.Range["B1"].Value = "Costo";


            i = 1;
            foreach (var item in lista_obra_tipoP)
            {
                i = i + 1;
                ws.Range["A" + i].Value = item.NombreTipo;
                ws.Range["B" + i].Value = item.CostoEntregas;
            }

            ws_2.Range["A1"].Value = "Tipo";
            ws_2.Range["B1"].Value = "Asignaciones";
            i = 1;
            foreach (var item in lista_obra_tipoVh)
            {
                i = i + 1;
                ws_2.Range["A" + i].Value = item.NombreCateVh;
                ws_2.Range["B" + i].Value = item.CantidadAsignada;
            }
            i = 1;
            ws_3.Range["A1"].Value = "Nombre";
            ws_3.Range["B1"].Value = "Costo Herramientas";
            foreach (var item in lista_empleados)
            {
                i = i + 1;
                ws_3.Range["A" + i].Value = item.NombreEmpleado;
                ws_3.Range["B" + i].Value = item.CostoHerramientas;
            }

        }
    }
}
