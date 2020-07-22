using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class CotizacionList
    {
        public int CotizacionID { get; set; }
        public int UsuarioID { get; set; }
        public int SolicitudID { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaGenerado { get; set; }

        public string Titulo { get; set; }
        public string FarmaciaNombre { get; set; }
        public string Distancia { get; set; }
        public int CantProductos { get; set; }
        public int CantProductosTake { get; set; }
        public string CantProductosString { get; set; }
        public string Total { get; set; }
        public decimal DTotal { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
