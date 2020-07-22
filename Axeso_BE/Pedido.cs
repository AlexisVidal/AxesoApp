using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public string Numero { get; set; }
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
        public string Estado { get; set; }
        public string NombreEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public double LatitudEntrega { get; set; }
        public double LongitudEntrega { get; set; }
        public decimal TotalPagar { get; set; }
        public string TipoPagar { get; set; }
        public decimal MontoPagar { get; set; }
        public int UsuarioClienteID { get; set; }

    }
}
