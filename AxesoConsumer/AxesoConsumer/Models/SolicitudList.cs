using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Models
{
    public class SolicitudList
    {
        public int NroItem { get; set; }
        public int SolicitudID { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaEnviado { get; set; }
        public string TotalProductos { get; set; }
        public bool Cotizado { get; set; }
        public string Color { get; set; }
        public decimal PrecioTotal { get; set; }
        public string SCotizado { get; set; }

    }
}
