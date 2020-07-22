using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class CotizacionProducto
    {
        public int CotizacionProductoID { get; set; }
        public int CotizacionID { get; set; }
        public int SolicitudProductoID { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool Activo{ get; set; }
        public int Cantidad { get; set; }

        public virtual Cotizacion Cotizacion { get; set; }
        public virtual SolicitudProducto SolicitudProducto { get; set; }
    }
}
