using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class TipoUbigeo
    {
        public int ID { get; set; }
        public string PaisID { get; set; }
        public int TipoUbigeoID { get; set; }
        public int TipoUbigeoID_pad { get; set; }
        public string Nombre { get; set; }
        public List<UbigeoModel> ubigeo { get; set; }
    }
}
