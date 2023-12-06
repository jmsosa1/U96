using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
   public class DetTareaSector :INotifyPropertyChanged
    {
        int _idseguidor, _iddet, _idtarea, _numitem;
        string _descritarea, _estadoitem, _nomseguidor, _observacion;
        private DateTime? _fcumplimiento;

        public event PropertyChangedEventHandler PropertyChanged;
        public void CambioPropiedad(string nombreprop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombreprop));
            }
        }

        public int IdDetTarea { get { return _iddet; } set { _iddet = value; } }
        public int IdTarea { get { return _idtarea; } set { _idtarea = value; } }
        public int IdSeguidor { get { return _idseguidor; } set { _idseguidor = value; } }
        public int NumItem { get { return _numitem; }
            set { _numitem = value;
                CambioPropiedad("NumItem");
            } }
        public DateTime? Fcumplimiento { get => _fcumplimiento; set => _fcumplimiento = value; }

        public string NombreSeguidor { get { return _nomseguidor; } set { _nomseguidor = value; } }
        public string DescriTarea { get { return _descritarea; } set { _descritarea = value; } }
        public string EstadoItem { get {return  _estadoitem; } set { _estadoitem = value; } }
        public string Observacion { get { return _observacion; } set { _observacion = value; } }

        public DetTareaSector()
        { }
        

        //NumItem numero de item de actividad de la tarea
        //EstadoItem estado de cumplimiento : Pendiente, EnCurso, Cumplido
        // Nombreseguidor : nombre del usuario que tiene a cargo la tarea
        //fcumplimiento : fecha de cumplimiento de la actividad. Cambio de estado del item
    }
}
