using System;

namespace ENTIDADES
{
    public class Asignacion_vh
    {

        int _idasig, _idvh, _imputacion, _diasacu, _idempleado;
        int  _idcatevh;
        DateTime _fechain;
        DateTime _altaf;
        decimal _costoasig;
        string _estadoasignacion, _situacionasignacion;
        string _dominio, _marca, _modelo, _nomobra, _nomempleado, _clienteobra, _categoria;
        private DateTime? _bajaf;

        public int IdAsig { get { return _idasig; } set { _idasig = value; } }
        public DateTime FechaIn { get { return _fechain; } set { _fechain = value; } }
        public DateTime? FechaFin { get; set; }
        public int IdVh { get { return _idvh; } set { _idvh = value; } }
        public int Imputacion { get { return _imputacion; } set { _imputacion = value; } }
        public int DiasAcumulados { get { return _diasacu; } set { _diasacu = value; } }
        public decimal CostoAsignacion { get { return _costoasig; } set { _costoasig = value; } }
        public string EstadoAsignacion { get { return _estadoasignacion; } set { _estadoasignacion = value; } }
        public string SituacionAsignacion { get { return _situacionasignacion; } set { _situacionasignacion = value; } }
        public int IdEmpleado { get { return _idempleado; } set { _idempleado = value; } }
        public string NombreEmpleado { get { return _nomempleado; } set { _nomempleado = value; } }
        public string Dominio { get { return _dominio; } set { _dominio = value; } }
        public string Marca { get { return _marca; } set { _marca = value; } }
        public string Modelo { get { return _modelo; } set { _modelo = value; } }
        public string NombreObra { get { return _nomobra; } set { _nomobra = value; } }
        public decimal HsAcuObra { get; set; } 

        public decimal KmAcuObra { get; set; }
        public string Cliente { get { return _clienteobra; } set { _clienteobra = value; } }
        public int IdCatevh { get { return _idcatevh; } set { _idcatevh = value; } }
        public string Categoria { get { return _categoria; } set { _categoria = value; } }
        
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public DateTime? BajaF { get => _bajaf; set => _bajaf = value; }

        public Asignacion_vh()
        { }
    }
}
