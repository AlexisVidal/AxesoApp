using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class SolicitudDataFarmaciasInput
    {
        public int SolicitudID { get; set; }
        public int UsuarioID { get; set; }
        public bool Activo { get; set; }
    }
}
