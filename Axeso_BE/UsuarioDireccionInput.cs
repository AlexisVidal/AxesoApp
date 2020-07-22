using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UsuarioDireccionInput
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Activo { get; set; }
    }
}
