using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class TipoProducto
    {
        [Key]
        [Column("TipoProductoID")]
        public int TipoProductoID { get; set; }
        public string Nombre { get; set; }
        [Column("Abreviatura")]
        public string Abreviatura { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<TipoProductoTipoNegocio> TipoProductoTipoNegocio { get; set; }
    }
}
