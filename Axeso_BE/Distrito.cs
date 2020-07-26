using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Distrito
    {
        public int DistritoID { get; set; }
        public string CodigoPostal { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public virtual ICollection<UsuarioDireccion> UsuarioDireccion { get; set; }
    }
}
