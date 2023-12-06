using BLL;
using ENTIDADES;
using System;
using System.Windows;


namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ObservacionActividad.xaml
    /// </summary>
    public partial class ObservacionActividad : Window
    {
        public string operacion = "";
        BLLGestion gestion = new BLLGestion();
        DetTareaSector d = new DetTareaSector();
        public ObservacionActividad(DetTareaSector det)
        {
            InitializeComponent();
            d = det;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            if (operacion == "C")
            {
                //es una operacion de cumplimiento
                d.Observacion = txtObservacion.Text;
                d.Fcumplimiento = DateTime.Today.Date;
                int fila = gestion.DetalleTareaCumplimiento(d);
            }
            else
            {
                if (operacion == "X")
                {
                    //es una operacion de cancelacion
                    d.Observacion = txtObservacion.Text;
                    d.Fcumplimiento = DateTime.Today.Date;
                    int fila = gestion.DetalleTareaCancelar(d);
                }

            }
            DialogResult = true;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
