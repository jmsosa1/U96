using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTIDADES;

namespace BLL
{
    public class BLLLaboratorio
    {
        DALLaboratorio coreLab = new DALLaboratorio();
        DALProveedor coreProve = new DALProveedor();
        DALConexion conn = new DALConexion();

        public void NuevaCalibracionInstrumento(CalibracionInstrumento calibracion)
        {
            coreLab.NuevaCalibracionInstrumento(calibracion);
        }

        public void ActualizarEstadoInstrumento(int idproducto, string _estado)
        {
            coreLab.ActualizarEstadoInstrumento(idproducto, _estado);
        }

        public List<CalibracionInstrumento> ListarTodasLasCalibracionesUnInstrumento(int idinstrumento)
        {
            List<CalibracionInstrumento> lista = coreLab.ListarTodasLasCalibracionesUnInstrumento(idinstrumento);
           
            return lista;
        }

        public CalibracionInstrumento BuscarCalibracionActivaUnInstrumento(int idproducto)
        {
            CalibracionInstrumento calibracion = coreLab.BuscarCalibracionActivaUnInstrumento(idproducto);
           
            return calibracion;
        }

        public void BorrarCalibracion(int idcalibracion)
        {
            coreLab.BorrarCalibracion(idcalibracion);
        }

        public ObservableCollection<CalibracionInstrumento> ListarCalibracionesUnInstrumento(int idproducto)
        {
            ObservableCollection<CalibracionInstrumento> lista = coreLab.ListarCalibracionesUnInstrumento(idproducto);


            return lista;
        }

        public ObservableCollection<Producto> ListarInstrumentos()
        {
            ObservableCollection<Producto> lista = coreLab.ListarInstrumentos();

            return lista;


        }
        public ObservableCollection<Producto> ListarInstrumentosVencidos()
        {
            ObservableCollection<Producto> lista = coreLab.ListarInstrumentosVencidos();

            return lista;
        }

        public ObservableCollection<Producto> ListarInstrumentosPorVencer()
        {
            ObservableCollection<Producto> lista = coreLab.ListarInstrumentosPorVencer();
         
            return lista;
        }


        public void CalibracionesActualizarVencimientos()
        {
            coreLab.CalibracionesActualizarVencimientos();
        }

        public void CalibracionesActualizarEstadoInstrumentos()
        {
            coreLab.CalibracionesActualizarEstadoInstrumentos();
        }



       
    }
}
