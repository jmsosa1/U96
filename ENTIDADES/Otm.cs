using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ENTIDADES
{
    public class Otm : INotifyPropertyChanged
    {
        int _idotm, _idvh, _idproducto,  _usuario_creador, _tipo ;
        DateTime _altaf;
        private DateTime? _fnecesidad, _fcumplimiento;
        string _estado_otm, _descripcion, _nota, _dominio, _codinventario, _nomuser;
        string _descrivh, _modelovh, _nombre_producto;
        byte[] _img_estado;
        int _lectura_hs;
        decimal _lectura_km;
        int _cantitems, _pcumplido, _estado_tmp, _vencida;
        string _observacion;


        public event PropertyChangedEventHandler PropertyChanged;

        public void CambioPropiedad(string nombreprop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombreprop));
            }
        }

        public int IdOtm { get { return _idotm; } set { _idotm = value; } }
        public int Idvh { get { return _idvh; } set { _idvh = value; } }
        public int Idproducto { get { return _idproducto; } set { _idproducto = value; } }
        
        public int UsuarioCreador { get { return _usuario_creador; } set { _usuario_creador = value; } }
        
        public DateTime Alta { get { return _altaf; } set { _altaf = value; } }
        public DateTime? FNecesidad { get => _fnecesidad; set => _fnecesidad = value; }
        public DateTime? FCumplimiento { get { return _fcumplimiento; } set { _fcumplimiento = value; } }
        public string Estado_Otm { get { return _estado_otm; } set { _estado_otm = value; } }
        public string Titulo { get { return _descripcion; } set { _descripcion = value; } }
        public string Nota { get { return _nota; } set { _nota = value; } }
        public string Dominio { get { return _dominio; } set { _dominio = value; } }
        public string CodInventario { get { return _codinventario; } set { _codinventario = value; } }
        public byte[] Img_Estado { get { return _img_estado; } set { _img_estado = value; } }
        public int Tipo_Otm { get { return _tipo; } set { _tipo = value; } }
        
        public string NombreUsuario { get { return _nomuser; } set { _nomuser = value; } }
        public string DescriVh { get { return _descrivh; } set { _descrivh = value; } }
        public string ModeloVh { get { return _modelovh; } set { _modelovh = value; } }
        public string NombreProducto { get { return _nombre_producto; } set { _nombre_producto = value; } }
        public int LecturaHs { get { return _lectura_hs; } set { _lectura_hs = value; } }
        public decimal LecturaKm { get { return _lectura_km; } set { _lectura_km = value; } }

        public int CantItems { get { return _cantitems; } set { _cantitems = value; } }
        public int PCumplido { get { return _pcumplido; } set { _pcumplido = value; } } // % cumplido de la otm
        public int Est_Tmp { get { return _estado_tmp; } set { _estado_tmp = value; } }
        public int Vencida { get { return _vencida; } set { _vencida = value; } }
        public string MarcaVh { get; set; }
        public string Observacion { get { return _observacion; } set { _observacion = value; } }
        public int IdPlanInspeccion { get; set; }
        public Otm()
        { }


    }
}
