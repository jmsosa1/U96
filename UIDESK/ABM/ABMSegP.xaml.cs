using BLL;
using ENTIDADES;
using MaterialDesignExtensions.Controls;
using System;
using System.Windows;

namespace UIDESK.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMSegP.xaml
    /// </summary>
    public partial class ABMSegP : MaterialWindow
    {
        #region Declarativas
        BLLProducto coreProducto = new BLLProducto();
        int _operacion = 0;
        int _idcategoria = 0;
        SegmentoP _segmento = new SegmentoP();
        #endregion


        public ABMSegP(SegmentoP segmento, int ope)
        {
            InitializeComponent();
            _segmento = segmento;
           grd1.DataContext = _segmento;
            _idcategoria = _segmento.IdCateP;
            _operacion = ope;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreCate.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para la categoria de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (string.IsNullOrEmpty(txtPrecioSeg.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para la categoria de producto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ActualizarSegmento(_operacion);
                    DialogResult = true;
                }
               
            }
        }

        private void ActualizarSegmento(int _op)
        {
           
            string _valor = txtPrecioSeg.Text;
            decimal _precio = decimal.Parse(_valor.Replace("$", ""));
            if (_op == 1) // alta
            {
                coreProducto.SegProductoAlta(_idcategoria, txtNombreSeg.Text,_precio);
            }
            else
            {
                if (_op == 2) // modificacion
                {
                    coreProducto.SegProductoModi(_segmento);
                }
                else
                {
                    //baja
                }
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
