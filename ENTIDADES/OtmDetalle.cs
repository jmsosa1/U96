using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
  public   class OtmDetalle
    {
        int id_det_otm;
        int idotm;
        int num_item;
        int idcatemante;
        string descripcion_item;
        int usuario_seguidor;
        string estado_item;
        int idprove;
        string obs_item;
        string remito_proveedor;
        string factura_proveedor;
        string oc_proveedor;
        string _razon_prove;
        string _nombre_seguidor;
        string _nomcatemante , _registrado;
        private DateTime? _fcumplimiento;
        int _idvh;
       // byte[] _img_observacion;

        public int IdDet { get { return id_det_otm; } set { id_det_otm = value; } }
        public int IdOtm { get { return idotm; } set { idotm = value; } }
        public int NumItem { get { return num_item; } set { num_item = value; } }
        public string DescripcionItem { get { return descripcion_item; } set { descripcion_item = value; } }
        public int UsuarioSeguidor { get { return usuario_seguidor; } set { usuario_seguidor = value; } }
        public string EstadoItem { get { return estado_item; }set { estado_item = value; } }
        public int IdProve { get { return idprove; } set { idprove = value; } }
        public string Observacion { get { return obs_item; } set { obs_item = value; } }
        public string RemitoProve { get { return remito_proveedor; } set { remito_proveedor = value; } }
        public string FacturaProve { get { return factura_proveedor; } set { factura_proveedor = value; } }
        public string OcProve { get { return oc_proveedor; } set { oc_proveedor = value; } }
        public string RazonProve { get { return _razon_prove; } set { _razon_prove = value; } }
        public string NombreSeguidor { get { return _nombre_seguidor; } set { _nombre_seguidor = value; } }
        public int IdCateMante { get { return idcatemante; } set { idcatemante = value; } }
        public string NomCateMante { get { return _nomcatemante; } set { _nomcatemante = value; } }
        public DateTime? FCumplimiento { get { return _fcumplimiento; } set { _fcumplimiento = value; } }
        public int Idvh { get { return _idvh; } set { _idvh = value; } }
        public string Registrado { get { return _registrado; } set { _registrado = value; } }
        public byte[] Img_Observacion { get; set; }

        public OtmDetalle()
        { }
    }
}
