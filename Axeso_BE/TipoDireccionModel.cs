using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asexo_ET
{
    public class TipoDireccion
    {
        public int TipoDireccionID { get; set; }
        public string Nombre { get; set; }
        public bool EsParaNegocio { get; set; }
        public bool EsParaConsumidor { get; set; }
    }
}
