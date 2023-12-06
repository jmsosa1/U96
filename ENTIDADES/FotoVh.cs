using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ENTIDADES
{
    public class FotoVh 
    {
        int _idfotovh, _idvh;
        DateTime _altaf;
        string _descrifoto, _titulo;
        byte[] _foto;

       // public event PropertyChangedEventHandler PropertyChanged;


        public int IdFotoVh { get { return _idfotovh; } set { _idfotovh = value; } }
        public int Idvh { get { return _idvh; } set { _idvh = value; } }
        public DateTime AltaF { get { return _altaf; } set { _altaf = value; } }
        public string DescriFoto { get { return _descrifoto; } set { _descrifoto = value; } }
        public string Titulo { get { return _titulo; } set { _titulo = value; } }
        public byte[] Foto { get { return _foto; } set { _foto = value; } }

        public FotoVh()
        { }
    }
}
