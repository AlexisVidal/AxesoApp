using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class PedidoLineaTiempoList
    {
        public int PedidoLineaTiempoID { get; set; }
        public int PedidoID { get; set; }
        public string EstadoPedidoID { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaTexto { get; set; }
        public bool IsLast { get; set; } = false;
        public string Estado { get; set; }
        public string Numero { get; set; }
    }
}
