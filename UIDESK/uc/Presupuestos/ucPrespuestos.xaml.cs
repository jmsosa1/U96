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
using ENTIDADES;
using BLL;

namespace UIDESK.uc.Presupuestos
{
    /// <summary>
    /// Lógica de interacción para ucPrespuestos.xaml
    /// </summary>
    public partial class ucPrespuestos : UserControl
    {
        BLLPresupuesto corePresupuesto = new BLLPresupuesto();
        List<Presupuesto> presupuestos; 
        public ucPrespuestos()
        {
            InitializeComponent();
            presupuestos = new List<Presupuesto>();
            presupuestos = corePresupuesto.ListarTodosLosPresupuestos();
            dgVhGeneral.DataContext = presupuestos;
            dgVhGeneral.ItemsSource = presupuestos;
            if (presupuestos.Count == 0)
            {
                txtRegistros.Text = "No se encuentran registros";
            }
        }

        private void dgVhGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModicarDatos_Click(object sender, RoutedEventArgs e)
        {
            
          
           
            //antes de modificar un presupuesto debemos verificar que su estado sea el correcto , es dcir que no este vigente
           Presupuesto presupuesto = dgVhGeneral.SelectedItem as Presupuesto;
            if (presupuesto != null)
            {
                //primero debemos verificar que el usuario tiene el permiso adecuado
                if (Contexto.RolUsuario != "Admin")
                {
                    MessageBox.Show("no tiene los permisos para modificar el presupuesto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (presupuesto.IdEstado != 2)
                    {
                        if (presupuesto.IdSituacion != 18)
                        {
                            Presupuesto_Encabezado presupuesto_Encabezado = new Presupuesto_Encabezado(presupuesto);
                           presupuesto_Encabezado.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("El presupuesto esta cerrado.No puede modificarse", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El presupuesto esta dado de Baja.No puede modificarse", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un presupuesto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //debemos validar que el usuario tenga los permisos adecuados
            //primero debemos verificar que el usuario tiene el permiso adecuado
            if (Contexto.RolUsuario != "Admin")
            {
                MessageBox.Show("no tiene los permisos para agregar un presupuesto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //// antes de permitir agregar un nuevo prespuesto, debemos validar que no exista uno activo actual
                //int idpr = corePresupuesto.ValidarExistenciaPresupuestoActivo();
                //if (idpr>0)
                //{
                //    MessageBox.Show("Ya existe un presupuesto activo.No se puede agregar uno nuevo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //}
                //else
                //{

                //}

                //debemos buscar el ultimo numero de presupuesto y agregar el nuevo al presupuesto de alta
                
                PresupuestoVh presupuesto = corePresupuesto.UltimoIdPresupuesto();
               
                presupuesto.Numero = " " + presupuesto.IdPre.ToString() + "-2023";
                presupuesto.IdPre = 0;
                presupuesto.IdUsuario = Contexto.CodUser;
                presupuesto.NomUsuarioCreador = Contexto.Nomuser;
                presupuesto.F_UltimaModificacion = DateTime.Now.Date;
               
                Presupuesto_Encabezado presupuesto_Encabezado = new Presupuesto_Encabezado(presupuesto); //1 vehiculos
                if (presupuesto_Encabezado.ShowDialog() == true)
                {
                    MessageBox.Show("Presupuesto Grabado","Aviso",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }



        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            // debemos verificar si el usuario tiene los permisos para dar de baja un prespuesto

            Presupuesto presupuesto = dgVhGeneral.SelectedItem as Presupuesto;
            if (presupuesto != null)
            {
                //primero debemos verificar que el usuario tiene el permiso adecuado
                if (Contexto.RolUsuario != "Admin")
                {
                    MessageBox.Show("no tiene los permisos para modificar el presupuesto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (presupuesto.IdEstado == 2)
                    {
                        MessageBox.Show("El presupuesto ya esta dado de Baja", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {

                    }
                }
               
            }
            else
            {
                MessageBox.Show("Debe seleccionar un presupuesto", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            Presupuesto presupuesto = dgVhGeneral.SelectedItem as Presupuesto;
        }
    }
}
