using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class PedidoList
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

        public string Titulo { get; set; }
        public string SEstado { get; set; }
        public int CantProductos { get; set; }
        public int CantProductosTake { get; set; }
        public string CantProductosString { get; set; }
        public string Total { get; set; }
        public string Distancia { get; set; }

        public string Color { get; set; }

    }
}
