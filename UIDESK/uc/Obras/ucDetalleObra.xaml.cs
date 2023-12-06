using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Windows.Controls;

namespace UIDESK.uc.Obras
{
    /// <summary>
    /// Lógica de interacción para ucDetalleObra.xaml
    /// </summary>
    public partial class ucDetalleObra : UserControl
    {
        BLLObras coreObras = new BLLObras();
        Obra _obra = new Obra();
        List<Casa> lista_casas = new List<Casa>();
        public static int _imputacion = 0;

        public ucDetalleObra()
        {
            InitializeComponent();
            lista_casas = coreObras.ListarCasaUnaObra(_imputacion);
            dgCasas.ItemsSource = lista_casas;
            dgCasas.DataContext = lista_casas;
            _obra = coreObras.BuscarImputacion(_imputacion);
           grDatosObras.DataContext = _obra;

        }
    }
}
