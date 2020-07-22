using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class SolicitudDataFarmacias
    {
        [Key]
        public int SolicitudDataFarmaciasID { get; set; }
        [ForeignKey("SolicitudID")]
        public int SolicitudID { get; set; }
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }
        public bool Activo { get; set; }
        [Column("Solicitud")]
        public virtual Solicitud Solicitud { get; set; }
        [Column("Usuarios")]
        public virtual Usuarios Usuarios { get; set; }
    }
}
