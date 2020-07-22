using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class PedidoLineaTiempo
    {
        public int PedidoLineaTiempoID { get; set; }
        public int PedidoID { get; set; }
        public string EstadoPedidoID { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaTexto { get; set; }
        public string Numero { get; set; }
        public int UsuarioClienteID { get; set; }

    }
}
