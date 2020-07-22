using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Cotizacion
    {
        public int CotizacionID { get; set; }
        public int UsuarioID { get; set; }
        public int SolicitudID { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaGenerado { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Estado { get; set; }              //1 generada 2 aceptada 3 rechazada
        public virtual Solicitud Solicitud { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        //public virtual DataFarmacias DataFarmacias { get; set; }
        public virtual ICollection<CotizacionProducto> CotizacionProducto { get; set; }
    }
}
