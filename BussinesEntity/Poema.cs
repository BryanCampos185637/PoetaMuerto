using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntity
{
    public class Poema
    {
        public Int64 Idpoema { get; set; }
        public string Titulo { get; set; }
        public string Verso { get; set; }
        public string Imagen { get; set; }
        public string Bhabilitado { get; set; }
        public Poema(){}
        //constructor parametrizado para hacer listados
        public Poema(Int64 id,string titulo, string verso, string imagen)
        {
            Idpoema = id;
            Titulo = titulo;
            Verso = verso;
            Imagen = imagen;
        }
    }
}
