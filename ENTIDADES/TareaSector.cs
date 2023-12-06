using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.ComponentModel;



namespace ENTIDADES
{
    public class TareaSector : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void CambioPropiedad(string nombreprop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombreprop));
            }
        }

        int _idtarea, _uscreador,  _pcumplimiento, _cantitems, _dias, _estadotemp, _vencida;
        string _titulo, _estadotarea, _importancia, _nomcreador;
        DateTime _altaf;
        private DateTime ? _fnecesidad, _fcierre, _ultimamodi;
        private byte[] _image_estado_temp;

        public int IdTarea { get { return _idtarea; } set { _idtarea = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public int UsuarioCreador { get { return _uscreador; } set { _uscreador = value; } }
        public string NombreCreador { get { return _nomcreador; } set { _nomcreador = value; } }
        public string TituloTarea { get { return _titulo; } set { _titulo = value; } }
        public string EstadoTarea { get { return _estadotarea; } set { _estadotarea = value; } }
        public string ImportanciaTarea { get { return _importancia; } set { _importancia = value; } }
        public DateTime? Fnecesidad { get => _fnecesidad; set => _fnecesidad = value; }
        public DateTime? Fcierre { get => _fcierre; set => _fcierre = value; }
        public int PorcentajeCumplimiento { get { return _pcumplimiento; } set { _pcumplimiento = value; } }
        public int CantidadItems { get { return _cantitems; } set { _cantitems = value; } }
        public int DiasEjecucion { get { return _dias; } set { _dias = value; } }
        public int EstadoTemporal { get { return _estadotemp; } set { _estadotemp = value; } }
        public DateTime? Ultimamodi { get => _ultimamodi; set => _ultimamodi = value; }
        public int Vencida { get { return _vencida; } set { _vencida = value; } }
        public byte[] ImageEstadoTemp { get { return _image_estado_temp; } set { _image_estado_temp = value; } }

        public TareaSector()
        { }
    }
}
