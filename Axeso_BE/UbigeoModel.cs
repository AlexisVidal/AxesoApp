using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UbigeoModel
    {
        public int ID { get; set; }
        public string PaisID { get; set; }
        public int TipoUbigeoID { get; set; }
        public int UbigeoID { get; set; }
        public int TipoUbigeoID_pad { get; set; }
        public int UbigeoID_pad { get; set; }
        public string Nombre { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }

    }
}
