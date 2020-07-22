using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public partial class Usuarios
    {
        [Key]
        public int UsuarioID { get; set; }
        public string TipoUsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public string Token { get; set; }

       // [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FchHraCreacion { get; set; }
        public DateTime FchHraActualizacion { get; set; }
        public DateTime FchHraBloqueo { get; set; }
        public DateTime FchHraDesbloqueo { get; set; }
        public int DataFarmaciasID { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<SolicitudDataFarmacias> SolicitudDataFarmacias { get; set; }
        public virtual ICollection<Solicitud> Solicitud { get; set; }
        public virtual ICollection<Cotizacion> Cotizacion { get; set; }
        public virtual ICollection<UsuarioDireccion> UsuarioDireccion { get; set; }

    }
}
