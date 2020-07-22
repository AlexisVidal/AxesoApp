using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Parametro
    {
        public int ParametroID { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public bool Activo { get; set; }
    }
}
