using BLL;
using ENTIDADES;
using System;
using System.IO;
using System.Windows;

namespace UIDESK.uc.gestion
{
    /// <summary>
    /// Lógica de interacción para ObservacionOTM.xaml
    /// </summary>
    public partial class ObservacionOTM : Window
    {
        public string operacion = "";
        BLLGestion gestion = new BLLGestion();
        OtmDetalle otd = new OtmDetalle();
        byte[] _img;

        public ObservacionOTM(OtmDetalle detalle)
        {
            InitializeComponent();
            otd = detalle;
            _img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\file-document-box.png");
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //seteo de campos del detallet
            otd.Observacion = txtObservacion.Text;
            otd.FCumplimiento = DateTime.Today.Date;
            otd.Img_Observacion = _img;

            //verificacmos que tipo de operacion se trata
            if (operacion == "C")
            {
                //operacion de cumplimiento

                //llamada a procedure
                int fila = gestion.OtmDetalleCumplimiento(otd);
            }
            else
            {
                if (operacion == "X")
                {
                    // operacion de cancelacion

                    // llamada a procedure 
                    int fila = gestion.OtmDetalleCancelar(otd);
                }
            }

            DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        // byte[] img = File.ReadAllBytes(@"C:\SAHMV6\imagenes\nueva-tarea.png");
    }
}
