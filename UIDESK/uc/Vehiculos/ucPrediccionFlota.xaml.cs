using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using BLL;
using ENTIDADES;
using LiveCharts;
using UIDESK.Helpers;

namespace UIDESK.uc.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para ucPrediccionFlota.xaml
    /// </summary>
    public partial class ucPrediccionFlota : UserControl
    {
        CategoriaVh categoriaVh = new CategoriaVh();
        List<CategoriaVh> listaCate = new List<CategoriaVh>();
        List<CategoriaVh> listaCatVhFlota = new List<CategoriaVh>();
        List<Vehiculo> listaVh = new List<Vehiculo>();
        CalculosFlota calculos = new CalculosFlota();
        BLLVehiculos coreVh = new BLLVehiculos();
        BLLResultado coreResultado = new BLLResultado();
        CultureInfo ci = new CultureInfo("es-AR");
        public Func<double, string> Formatter { get; set; }
        public ChartValues<decimal> values_lineas = new ChartValues<decimal>();
        public List<string> tags_lineas = new List<string>();
        public string[] Etiquetas { get; set; }

        int _r050 = 0;
        int _r50100 = 0;
        int _r100200 = 0;
        int _r200300 = 0;
        int _r300400 = 0;
        int _r400500 = 0;
        int _r500 = 0;
        public ucPrediccionFlota()
        {
            InitializeComponent();
            txtAnioActual.Text = DateTime.Now.Year.ToString();
            listaCate = coreVh.VehiculosListarCategoria();
            foreach (var item in listaCate)
            {

                if (item.IdTipoVh == 1 || item.IdTipoVh == 2)
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
                
                List<VhKmRango> lista1 = coreResultado.VehiculosPorRangoKm(categoria.IdCateVh);
                dgVhRangoKm.ItemsSource = lista1;
                dgVhRangoKm.DataContext = lista1;
                dgPronosticos.ItemsSource = null;
                dgPronosticos.DataContext = null;
                dgVhRangoKmPrediccion.DataContext = null;
                dgVhRangoKmPrediccion.ItemsSource = null;
                txbCantPronosticoReemplazos.Text = string.Empty;
                txbCantPronosticoCostoReemplazos.Text = string.Empty;
                //vamos a llamar al metodo para calcular los posibles reemplazos en el año actual 
                //como cantidad de años pasamos el valor 0
                List<VhKmAvgCate> lista2 = calculos.PromedioKmCategoria(categoria.IdCateVh, 0);
                CalcularReemplazos(lista2, categoria.IdCateVh,0); // calculo de reemplazos actuales

            }
            
            
            //values_lineas.Clear();
            //tags_lineas.Clear();
            //gra1.Series.Clear();
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            
            CategoriaVh categoria = cmbCateVh.SelectedItem as CategoriaVh;
            if (categoria != null)
            {
                
                if (string.IsNullOrEmpty(txtAnios.Text))
                {
                    MessageBox.Show("Debe ingresar un numero de años para el calculo","Aviso",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                else {
                    int _anios = Convert.ToInt32(txtAnios.Text);
                   
                    if (_anios <0 || _anios >5)
                    {
                        MessageBox.Show("Debe ingresar un numeros de años entre 1 y 5 ", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        txbAniosPronostico.Text = "";
                        txbAniosPronostico.Text = _anios.ToString() + " " + " Años";
                        txbCantAnios.Text = txtAnios.Text + " Años";
                        // llamamos al procedimiento que hace los calculos
                        GenerarPronostico(categoria.IdCateVh,_anios);
                    }
                }

               
            }
        }

        private void GenerarPronostico(int idcate, int anios)
        {
            List<VhKmAvgCate> lista2 = calculos.PromedioKmCategoria(idcate, anios);
          
            CalcularReemplazos(lista2, idcate,1);
           
            // una vez que tenemos los contadores de los pronosticos definidos , armamos una nueva lista de objetos vhkmrango
            //que mostraremos en la grid de pronostico

            List<VhKmRango> lista_pro_km_rango = new List<VhKmRango>();
            VhKmRango vh1 = new VhKmRango() { IdRango = 1, NombreRango = "Entre 0y 50k", CantVh = _r050 };
            VhKmRango vh2 = new VhKmRango() { IdRango = 2, NombreRango = "Entre 50 y 100k", CantVh = _r50100 };
            VhKmRango vh3 = new VhKmRango() { IdRango = 3, NombreRango = "Entre 100 y 200k", CantVh = _r100200 };
            VhKmRango vh4 = new VhKmRango() { IdRango = 4, NombreRango = "Entre 200 y 300k", CantVh = _r200300 };
            VhKmRango vh5 = new VhKmRango() { IdRango = 5, NombreRango = "Entre 300 y 400k", CantVh = _r300400 };
            VhKmRango vh6 = new VhKmRango() { IdRango = 6, NombreRango = "Entre 400 y 500k", CantVh = _r400500 };
            VhKmRango vh7 = new VhKmRango() { IdRango = 7, NombreRango = "Mas 500k", CantVh = _r500 };
            lista_pro_km_rango.Add(vh1);
            lista_pro_km_rango.Add(vh2);
            lista_pro_km_rango.Add(vh3);
            lista_pro_km_rango.Add(vh4);
            lista_pro_km_rango.Add(vh5);
            lista_pro_km_rango.Add(vh6);
            lista_pro_km_rango.Add(vh7);
            //ahora que tenemos la lista armada, la asignamos al grid del pronostico
            dgVhRangoKmPrediccion.DataContext = lista_pro_km_rango;
            dgVhRangoKmPrediccion.ItemsSource = lista_pro_km_rango;
            dgPronosticos.DataContext = lista2;
            dgPronosticos.ItemsSource = lista2;
        }

        private void CalcularReemplazos(List<VhKmAvgCate> lista2 ,int idcate, int tipo_pronostico)
        {
            //a partir de esta lista debo crear la lista de pronosticos de VhKmRango
            //declaramos las variables
           
            foreach (var item in lista2)
            {
                //recorremos la lista y vamos asignando los contadores
                //de cada categoria
                if (item.PronosticoConsumo <= 50000)
                {
                    _r050 += 1;
                }
                if (item.PronosticoConsumo > 50000 && item.PronosticoConsumo <= 100000)
                {
                    _r50100 += 1;
                }
                else
                {
                    if (item.PronosticoConsumo > 100000 && item.PronosticoConsumo <= 200000)
                    {
                        _r100200 += 1;
                    }
                    else
                    {
                        if (item.PronosticoConsumo > 200000 && item.PronosticoConsumo <= 300000)
                        {
                            _r200300 += 1;
                        }
                        else
                        {
                            if (item.PronosticoConsumo > 300000 && item.PronosticoConsumo <= 400000)
                            {
                                _r300400 += 1;
                                if (idcate == 1) //  si la categoria del vehiculo es automovil
                                {
                                    item.Reemplazo = 1;
                                }
                            }
                            else
                            {
                                if (item.PronosticoConsumo > 400000 && item.PronosticoConsumo <= 500000)
                                {
                                    _r400500 += 1;
                                    if (idcate == 1) // si la categoria del vehiculo es automovil
                                    {


                                        item.Reemplazo = 1;
                                    }
                                }
                                else
                                {
                                    if (item.PronosticoConsumo > 500000)
                                    {
                                        _r500 += 1;

                                        item.Reemplazo = 1;

                                    }


                                }
                            }
                        }
                    }
                }

            }
            //calculamos los potenciales reemplazos
            int _cantidadReemplazos = 0;
            decimal _costoReemplazos = 0;
          
          
            foreach (var item in lista2)
            {
                if (item.Reemplazo == 1)
                {
                    _cantidadReemplazos += 1;
                    _costoReemplazos = _costoReemplazos + item.CostoRepoDls;
                }
            }

            if (tipo_pronostico == 0)
            {
                txbReemplazosActuales.Text = _cantidadReemplazos.ToString(); // remplazos actuales
                txbCostoReemplazosActuales.Text = _costoReemplazos.ToString("C", ci);
            }
            else
            {
                txbCantPronosticoReemplazos.Text= _cantidadReemplazos.ToString();
                txbCantPronosticoCostoReemplazos.Text =  _costoReemplazos.ToString("C", ci);
              

              
            }
           
           
        }


        //zona grafico sacado de ucDatosManteVhAnioCategorias
        /*
          //bucle que crea las series del grafico 
            foreach (var item in resumen_categorias)
            {
                values_lineas.Add(item.CostoTotalCategoria);
                tags_lineas.Add(item.NombreCategoria);
            }

            Etiquetas = tags_lineas.ToArray();
            lvcCartesiano.Series = new SeriesCollection
            {
                new RowSeries{ Title="Costos Mantenimiento por categoria de vehiculo",Values=values_lineas,DataLabels=true}
            };
            lvcCartesiano.DataContext = this;
         */

    }



}
        
    


