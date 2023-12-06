using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DAL;
using ENTIDADES;
using System.Collections.ObjectModel;

namespace BLL
{
    public class BLLGestion
    {
        DALGestion dAL = new DALGestion();
       

        #region[tareas del sector]

        //alta de una tarea
        public int TareaNuevaAlta(TareaSector tareaSector)
        {
            int tarea = 0;
            tarea = dAL.TareaNuevaAlta(tareaSector);
            return tarea;
        }

        public int TareaModificar(TareaSector tareaSector)
        {
            int tarea = 0;
            tarea = dAL.TareaModificar(tareaSector);
            return tarea;
        }

        public int TareaBorrar(int _idtarea)
        {
            int tarea = 0;
            tarea = dAL.TareaBorrar(_idtarea);
            return tarea;
        }
        public int TareaCancelar(int idtarea, DateTime fecha)
        {
            int fila = 0;
            fila = dAL.TareaCancelar(idtarea, fecha);
            return fila;
        }

        public bool ConfirmarTareaBorrar(int _idtarea)
        {
            int filas = 0;
            filas = dAL.ConfirmarTareaBorrar(_idtarea);
            bool tareatienedetalle
;
            if (filas != 0)
            {
                tareatienedetalle = true;
            }
            else
            {
                tareatienedetalle = false;
            }
            return tareatienedetalle;
        }

        public int DetalleTareaAlta(DetTareaSector detTareaSector)
        {
            int tarea = 0;
            tarea = dAL.DetalleTareaAlta(detTareaSector);
            return tarea;

        }

        public int DetalleTareaBorrar(int _iddet, int _idt)
        {
            int tarea = 0;
            tarea = dAL.DetalleTareaBorrar(_iddet, _idt);
            return tarea;
        }

        public int DetalleTareaCumplimiento(DetTareaSector detalle)
        {
            int tarea = 0;
            tarea = dAL.DetalleTareaCumplimiento(detalle);
            return tarea;
        }

        public int DetalleTareaCancelar(DetTareaSector det)
        {
            int tarea = 0;
            tarea = dAL.DetalleTareaCancelar(det);
            return tarea;
        }

        public int DetalleTareaTomarSeguidor(DetTareaSector detTareaSector)
        {
            int det = 0;
            det = dAL.DetalleTareaTomarSeguidor(detTareaSector);
            return det;
        }

        public TareaSector ObetenerUltimoIdTarea()
        {
            TareaSector tareaSector = new TareaSector();
            tareaSector = dAL.ObetenerUltimoIdTarea();
            return tareaSector;
        }

        public ObservableCollection<TareaSector> TareasSectorTodas()
        {
            ObservableCollection<TareaSector> lista_tareas = new ObservableCollection<TareaSector>();

            lista_tareas = dAL.TareasSectorTodas();


            return lista_tareas;
        }

        public ObservableCollection<DetTareaSector> DetalleTareaSector(int idtarea)
        {
            ObservableCollection<DetTareaSector> detTareas = new ObservableCollection<DetTareaSector>();
            detTareas = dAL.DetalleTareaSector(idtarea);
            return detTareas;
        }

        public int TareaCalcularCumplimiento(int _idtarea)
        {
            int porcentaje = 0;
            porcentaje = dAL.TareaCalcularCumplimiento(_idtarea);
            return porcentaje;
        }

        public void TareaCumplirUna(int idtarea, DateTime fcumplido)
        {
            dAL.TareaCumplirUna(idtarea, fcumplido);
        }
        public void TareaCalcularDiasEnEjecucion()
        {

        }

        public ObservableCollection<TareaSector> TareasSectorSeguimientoUsuario(int idusuario)
        {
            ObservableCollection<TareaSector> lista_tareas = new ObservableCollection<TareaSector>();
            lista_tareas = dAL.TareasSectorSeguimientoUsuario(idusuario);

            return lista_tareas;
            
        }

        public void TareaModicarEstadoTemporal(DateTime fechaactual)
        {
            dAL.TareaModicarEstadoTemporal(fechaactual);
        }

        public void OTMModificarEstadoTemporal(DateTime fechaactual)
        {
            dAL.OTMModificarEstadoTemporal(fechaactual);

        }

        public int OtmItemsCumplidosNoRegistrados(int idvh)
        {
            int cantidadItem = dAL.OtmItemsCumplidosNoRegistrados(idvh);
            
            return cantidadItem;
        }

        public ObservableCollection<OtmDetalle> DetalleOTM_Cumplido_NR(int _idvh)
        {
            ObservableCollection<OtmDetalle> det_otm = new ObservableCollection<OtmDetalle>();
            det_otm = dAL.DetalleOTM_Cumplido_NR(_idvh);
            return det_otm;

        }
        #endregion

        #region[solicitudes]


        public int SolicitudAB_alta(Solicitud_Ab solicitud_)
        {
            int fila = dAL.SolicitudAB_alta(solicitud_);

            return fila;

        }

        public int SolicitudAB_alta_detalle(Solicitud_ab_detalle ab_Detalle)
        {
            int fila = dAL.SolicitudAB_alta_detalle(ab_Detalle);

            return fila;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Todas()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();

            lista_solicitud = dAL.SolicitudAb_Todas();
            return lista_solicitud;
        }
        public ObservableCollection<Solicitud_ab_detalle> SolicitudAb_Detalle_Una(int idsol)
        {
            ObservableCollection<Solicitud_ab_detalle> lista_detalle = new ObservableCollection<Solicitud_ab_detalle>();
            lista_detalle = dAL.SolicitudAb_Detalle_Una(idsol);

            return lista_detalle;
        }

        public Solicitud_Ab ObtenerUltimoIdSolicitud()
        {
            Solicitud_Ab ultimasol = new Solicitud_Ab();
            ultimasol = dAL.ObtenerUltimoIdSolicitud();
            return ultimasol;

        }

        public int SolicitudTomarUna(int idsol, int iduser)
        {
            int fila = dAL.SolicitudTomarUna(idsol,iduser);
            
            return fila;

        }

        public int SolicitudAB_Anular(int idsol)
        {
            int fila = dAL.SolicitudAB_Anular(idsol);
           
            return fila;
        }
        public int SolicitudAB_Cumplir_Un_Item(int idsol, int iddet, int iduser)
        {
            int fila =dAL.SolicitudAB_Cumplir_Un_Item(idsol,iddet,iduser);
          
            return fila;
        }

        public int SolicitudAB_Cancelar_Un_Item(int idsol, int iddet, int iduser)
        {
            int fila =dAL.SolicitudAB_Cancelar_Un_Item(idsol,iddet,iduser);
            
            return fila;
        }
        public int SolicitudAB_Calcular_Cumplimiento(int idsol)
        {
            int porcentaje = 0;
            porcentaje = dAL.SolicitudAB_Calcular_Cumplimiento(idsol);
            return porcentaje;
        }

        public void SolicitudAB_Cumplir_Una(int idsol, DateTime fcumplido)
        {
            dAL.SolicitudAB_Cumplir_Una(idsol, fcumplido);
        }

        public void SolicitudesModificarEstadoTemporal(DateTime fechaactual)
        {
            dAL.SolicitudesModificarEstadoTemporal(fechaactual);
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Pendiente()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();

            lista_solicitud = dAL.SolicitudAb_Pendiente();
            return lista_solicitud;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Cumplido()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            lista_solicitud = dAL.SolicitudAb_Cumplido();
            return lista_solicitud;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudAb_Anulado()
        {
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            lista_solicitud = dAL.SolicitudAb_Cumplido();

            return lista_solicitud;
        }

        public ObservableCollection<Solicitud_Ab> SolicitudesAB_JefeObra(string jefe)
        {
            
            ObservableCollection<Solicitud_Ab> lista_solicitud = new ObservableCollection<Solicitud_Ab>();
            lista_solicitud = dAL.SolicitudesAB_JefeObra(jefe);

            return lista_solicitud;

        }
        public DataSet SolicitudAb_Imprimir(int idsol)
        {
           
            DataSet ds1 = new DataSet(); // creamos el dataset que vamos a devolver
            ds1 = dAL.SolicitudAb_Imprimir(idsol);
            return ds1; //devolvemos el data set
        }

       

        #endregion

        #region[otm]

        public int OTMAlta(Otm otm)
        {
            int fila = 0;
            fila = dAL.OTMAlta(otm);
            return fila;
        }

        public int OTMBaja(int _idotm)
        {
            int fila = 0;
            fila = dAL.OTMBaja(_idotm);  
            return fila;

        }

        public ObservableCollection<Otm> OTM_Todas_VH()
        {
            ObservableCollection<Otm> otms = new ObservableCollection<Otm>();
            otms = dAL.OTM_Todas_VH();
            return otms;
        }

        public ObservableCollection<Otm> OTM_Todas_Producto()
        {
            ObservableCollection<Otm> otms = new ObservableCollection<Otm>();
            otms = dAL.OTM_Todas_Producto(); 
            return otms;
        }

        public Otm ObtenerUltimoIdOtm()
        {
            Otm otm_ultimo_id = new Otm();
            otm_ultimo_id = dAL.ObtenerUltimoIdOtm();
            return otm_ultimo_id;
        }

        public int DetalleOtmAlta(OtmDetalle otmDetalle)
        {
            
           int tarea = dAL.DetalleOtmAlta(otmDetalle);
            return tarea;

        }
        public ObservableCollection<OtmDetalle> DetalleOTM(int _idotm)
        {

            ObservableCollection<OtmDetalle> det_otm = new ObservableCollection<OtmDetalle>();
            det_otm = dAL.DetalleOTM(_idotm);
            return det_otm;

        }

        public int OtmDetalleTomarSeguidor(OtmDetalle otmDetalle)
        {
            
           int fila = dAL.OtmDetalleTomarSeguidor(otmDetalle);
            return fila;
        }

        public int OtmDetalleCumplimiento(OtmDetalle detalle)
        {
           
           int fila = dAL.OtmDetalleCumplimiento(detalle);
            return fila;
        }

        public int OtmDetalleCancelar(OtmDetalle detalle)
        {
            
            int fila = dAL.OtmDetalleCancelar(detalle);
            return fila;
        }

        public int OtmCalcularCumplimiento(int idotm)
        {
            int porcentaje = dAL.OtmCalcularCumplimiento(idotm);
             
            return porcentaje;

        }
        public void OtmCumplirUna(int idotm, DateTime fcumplido)
        {

            dAL.OtmCumplirUna(idotm, fcumplido);
        }

        public void OtmRegistrarUnItemDetalle(OtmDetalle otmDetalle)
        {
            dAL.OtmRegistrarUnItemDetalle(otmDetalle);
        }

        #endregion
    }
}
