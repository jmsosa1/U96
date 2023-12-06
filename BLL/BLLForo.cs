using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTIDADES;

namespace BLL
{
    public class BLLForo
    {

        DALForo dalForo = new DALForo();

        bool _validarNota = false;
        bool _validarLectura = false;

        #region Notas

        public bool ValidarNota(NotaSahmV6 notaSahm)
        {
            if (string.IsNullOrEmpty( notaSahm.Titulo))
            {
                _validarNota = false;
                return _validarNota;
            }
            
            if (string.IsNullOrEmpty(notaSahm.Contenido))
             {
                _validarNota = false;
                return _validarNota;
             }
            _validarNota = true;
            return _validarNota;
        }

        public void AgregarNota(NotaSahmV6 nota)
        {
            dalForo.AgregarNota(nota);
        }

        public void BorrarNota(int idnota)
        {
            dalForo.BorrarNota(idnota);
        }

        public void BajaNota(int idnota)
        {
            dalForo.BajaNota(idnota);
        }

        public void ActualizarSituacionNota(DateTime _fecha)
        {

            dalForo.ActualizarSituacionNota(_fecha);

        }

        public bool ValidarLecturaNota(int idusuario, int idnota)
        {
          NotasLeidos leido =    dalForo.BuscarNotaLeida(idusuario, idnota);
            if (leido.IdUsuario == idnota && leido.IdUsuario == idusuario)
            {
                _validarLectura = true;
                return _validarLectura;
            }
            else
            {
                _validarLectura = false;
                return _validarLectura;
            }
            
        }

        public void MarcarComoLeidaNota(int idnota, int idusuario, DateTime fecha)
        {
            // este metodo refiere a confirmar la lectura de la nota por parte de alguien
            dalForo.MarcarComoLeidaNota(idnota, idusuario, fecha);
        }

        public void CerrarNota(int idnota)
        {
            dalForo.CerrarNota(idnota);
        }

        public ObservableCollection<NotaSahmV6> ObtenerNotasActivas()
        {
            ObservableCollection<NotaSahmV6> lista = dalForo.ObtenerNotasActivas();
            return lista;
        }


        public ObservableCollection<NotaSahmV6> FiltrarNotasUnUsuario(int idusuario)
        {
            ObservableCollection<NotaSahmV6> lista = dalForo.FiltrarNotasUnUsuario(idusuario);
            return lista;
        }

        #endregion

        #region Respuestas

        public void AgregarRespuesta(RespuestaNota respuesta)
        {
            dalForo.AgregarRespuesta(respuesta);
        }

        public void BorrarRespuesta(int idrespuesta,int idnota)
        {
            dalForo.BorrarRespuesta(idrespuesta,idnota);
        }

        public List<RespuestaNota> ObtnerRespuestasUnaNota(int idnota)
        {
            List<RespuestaNota> lista = dalForo.ObtnerRespuestasUnaNota(idnota);
           

            return lista;
        }
        #endregion

    }
}
