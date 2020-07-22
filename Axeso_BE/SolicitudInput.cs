using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class SolicitudInput
    {
        public string SolicitudCode { get; set; }
        public int UsuarioID { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Distancia { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public bool Cotizado { get; set; }
        public string FechaEnviado { get; set; }
    }
}
