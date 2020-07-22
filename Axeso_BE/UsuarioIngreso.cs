using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UsuarioIngreso
    {
        [Key]
        public int UsuarioIngresoID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public Boolean Disponible { get; set; }
    }
}
