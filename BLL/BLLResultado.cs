using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTIDADES;
using static DAL.DALResultados;

namespace BLL
{
   public  class BLLResultado
    {
        DALResultados dAL = new DALResultados();

        public ObservableCollection<RelacionManteKM> RelManteKM()
        {
            ObservableCollection<RelacionManteKM> relacions = new ObservableCollection<RelacionManteKM>();

            relacions = dAL.RelManteKM(); 
            return relacions;
        }

        public ObservableCollection<RelacionManteHs> RelManteHs()
        {
            ObservableCollection<RelacionManteHs> relacions = new ObservableCollection<RelacionManteHs>();
            relacions = dAL.RelManteHs();
           
            return relacions;
        }

        public ObservableCollection<SituacionOperativaVh> SituacionOpListarTodas()
        {
            ObservableCollection<SituacionOperativaVh> relacions = new ObservableCollection<SituacionOperativaVh>();
            relacions = dAL.SituacionOpListarTodas();
            
            return relacions;
        }

        public List<VhKmRango> VehiculosPorRangoKm(int idcatevh)
        {
            List<VhKmRango> lista = new List<VhKmRango>();
            lista = dAL.VehiculosPorRangoKm(idcatevh);
            return lista;
        }
        public VhKmAvg VehiculosKmPromedio(int idcatevh)
        {
            VhKmAvg kmAvg = dAL.VehiculosKmPromedio(idcatevh);
            return kmAvg;
        }

        public Vehiculo VehiculoMasnuevo(int idcatevh)
        {
            Vehiculo km = new Vehiculo();
             km = dAL.VehiculoMasnuevo(idcatevh);
            return km;
        }

        public Vehiculo VehiculoMasViejo(int idcatevh)
        {
            Vehiculo k = new Vehiculo();
            k = dAL.VehiculoMasViejo(idcatevh);
            return k;
        }

        public List<VhAnioModelo> VehiculosPorAnioModelo(int idcatevh)
        {
            List<VhAnioModelo> lista = new List<VhAnioModelo>();
            lista = dAL.VehiculosPorAnioModelo(idcatevh);
            return lista;
        }

        public List<Vehiculo> VehiculosInversiones(int idcatevh, int aniodesde, int aniohasta)
        {
            List<Vehiculo> lista = dAL.VehiculosInversiones(idcatevh, aniodesde, aniohasta);
            
            return lista;
        }
    }
}
