using BLL;
using ENTIDADES;
using System;
using System.Windows;
using System.Windows.Input;


namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para RechazoPlanificacion.xaml
    /// </summary>
    public partial class RechazoPlanificacion : Window
    {
        PlanificacionVH bajapl = new PlanificacionVH();
        BLLVehiculos core = new BLLVehiculos();
        public RechazoPlanificacion(PlanificacionVH paramVH)
        {
            InitializeComponent();

            txtDominio.Text = paramVH.Dominio;
            txtImputacion.Text = paramVH.Imputacion.ToString();
            txtMarca.Text = paramVH.Marca;
            txtModelo.Text = paramVH.Modelo;
            txtSolicitado.Text = paramVH.Solicitante;
            txtNotas.Text = paramVH.Notas;
            txbObra.Text = paramVH.NomObra;
            bajapl = paramVH;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int _idpl = bajapl.IdPl;
            string _notabaja = txtNotaBaja.Text;
            DateTime fbaja = DateTime.Today.Date;
            int filasAfectadas = core.VehiculoAnulaPLanificacion(_idpl, _notabaja, fbaja);

            DialogResult = true;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
