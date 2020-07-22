using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Solicitud
    {
        public Solicitud()
        {
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SolicitudID { get; set; }

        public string SolicitudCode { get; set; }
        public int UsuarioID { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Distancia { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public string FechaEnviado { get; set; }
        public bool Cotizado { get; set; }

        public virtual ICollection<SolicitudProducto> SolicitudProducto { get; set; }
        
        public virtual ICollection<SolicitudDataFarmacias> SolicitudDataFarmacias { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
