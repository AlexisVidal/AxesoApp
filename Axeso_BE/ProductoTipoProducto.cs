using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class ProductoTipoProducto
    {
        [Key]
        public int ProductoID { get; set; }
        [Column("TipoProductoID")]
        public int TipoProductoID { get; set; }
        public int TipoNegocioID { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual TipoProductoTipoNegocio TipoProductoTipoNegocio { get; set; }
    }
}
