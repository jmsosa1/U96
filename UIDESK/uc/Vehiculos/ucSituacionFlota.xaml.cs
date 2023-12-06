using ENTIDADES;
using System;
using System.Collections.Generic;
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
using DAL;
using BLL;
using UIDESK.Helpers;
using OfficeOpenXml.Interfaces.Drawing.Text;
using System.Globalization;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucSituacionFlota.xaml
    /// </summary>
    /// 
  
    public partial class ucSituacionFlota : UserControl
    {
        #region Declarativas
        CategoriaVh categoriaVh= new CategoriaVh();
        List<CategoriaVh> listaCate = new List<CategoriaVh>();
        List<CategoriaVh> listaCatVhFlota = new List<CategoriaVh>();
        ObservableCollection<Vehiculo> listaVh = new ObservableCollection<Vehiculo>();
        CalculosFlota calculos = new CalculosFlota();
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLResultado coreResultado = new BLLResultado();
      CultureInfo ci = new CultureInfo("es-AR");
        public ICollectionView vistaAniosModelo // collecionviewsource para el group del los años modelos
        {
            get { return CollectionViewSource.GetDefaultView(listaVh); }
        }

        #endregion
        public ucSituacionFlota()
        {
            InitializeComponent();
            listaCate = coreVh.VehiculosListarCategoria();
            foreach (var item in listaCate)
            {
               
                if (item.IdTipoVh == 1 || item.IdTipoVh==2 )
                {
                    CategoriaVh cate = new CategoriaVh();
                    cate = item;
                    listaCatVhFlota.Add(cate);
                }

            }
            cmbCateVh.ItemsSource = listaCatVhFlota;
           
        }

        private void cmbCateVh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaVh categoria = cmbCateVh.SelectedItem as CategoriaVh;
            if (categoria != null)
            {
               // txbNombreCategoria.Text = categoria.NomCate;
                //aca se disparan todos los metodos relacionados con la categoria de vehiculo seleccionada
                //vehiculo mas viejo
                Vehiculo vhMasviejo = coreResultado.VehiculoMasViejo(categoria.IdCateVh);
                cardVhMasViejo.DataContext = vhMasviejo;
                txbDescViejo.Text = vhMasviejo.Modelo;
                txbModeloViejo.Text = "Anio:" + " " + vhMasviejo.AnioModelo + " - " + vhMasviejo.Dominio;
                //vehiculo mas nuevo
                Vehiculo vhMasnuevo = coreResultado.VehiculoMasnuevo(categoria.IdCateVh);
                cardVhMasNuevo.DataContext = vhMasnuevo;
                txbDescNuevo.Text =  vhMasnuevo.Modelo;
                txbModeloNuevo.Text = "Anio:" + " " + vhMasnuevo.AnioModelo + "- " + vhMasnuevo.Dominio; ;
                //promedio de km en la categoria
                VhKmAvg lista2 = coreResultado.VehiculosKmPromedio(categoria.IdCateVh);
                cardVhPromedioKM.DataContext = lista2;
                // lista de cantidad de vehiculos por rango de km acumulado
                List<VhKmRango> lista1 = coreResultado.VehiculosPorRangoKm(categoria.IdCateVh);
                dgVhRangoKm.ItemsSource = lista1;
                dgVhRangoKm.DataContext = lista1;
                // lista de vehiculos segun su año de modelo
                List<VhAnioModelo> lista3 = coreResultado.VehiculosPorAnioModelo(categoria.IdCateVh);
                dgAnioModelo.DataContext = lista3;
                dgAnioModelo.ItemsSource = lista3;
                int _adesde = 2018;
                int _hasta = DateTime.Today.Year;
                // lista de inversiones en la categoria seleccionada 
                List<Vehiculo> lista_inversiones = coreResultado.VehiculosInversiones(categoria.IdCateVh, _adesde, _hasta);
                dgVhInversiones.DataContext = lista_inversiones;
                dgVhInversiones.ItemsSource = lista_inversiones;
                txbCantInversiones.Text = lista_inversiones.Count.ToString();
                decimal _montoTotalInversiones = 0;
                foreach (var item in lista_inversiones)
                {
                    _montoTotalInversiones += item.ValorCompra;
                }
                txbMontoTotal.Text= _montoTotalInversiones.ToString("C",ci);
                txbSituacion.Text= calculos.SituacionFlotaKilometraje(categoria.IdCateVh,lista1);
                //listaVh = coreVh.VehiculosListarPorCategorias(categoria.IdCateVh);
                //dgAnioModelo.ItemsSource = listaVh;
                //dgAnioModelo.DataContext = listaVh;
            }
        }

       
    }
}
