using SQLite;
using System;

namespace AxesoConsumer.Models
{
    public class SolicitudLite
    {
        [PrimaryKey]
        public string IdSolicitud { get; set; }
        public string Usuario { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitud { get; set; }
        public double Distancia { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public bool Enviado { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}",
                IdSolicitud, Usuario, Fecha, Activo);
        }
    }
}
