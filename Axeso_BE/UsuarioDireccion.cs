using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UsuarioDireccion
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UsuarioDireccionID { get; set; }
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Activo { get; set; }

        [Column("Usuarios")]
        public virtual Usuarios Usuarios { get; set; }
    }
}
