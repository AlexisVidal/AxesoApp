using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class TipoUsuarioModel
    {
        public string TipoUsuarioID { get; set; }
        public string Nombre { get; set; }
        public List<Usuarios> usuario { get; set; }
    }
}
