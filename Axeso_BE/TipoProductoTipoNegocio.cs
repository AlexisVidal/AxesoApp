using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class TipoProductoTipoNegocio
    {
        [Column("TipoProductoID",Order = 0), ForeignKey("TipoProductoID")]
        public int TipoProductoID { get; set; }
        [Column("TipoNegocioID", Order = 1), ForeignKey("TipoNegocioID")]
        public int TipoNegocioID { get; set; }
        public virtual ICollection<ProductoTipoProducto> ProductoTipoProducto { get; set; }
        public virtual TipoNegocio TipoNegocio { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
    }
}
