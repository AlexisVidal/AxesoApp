using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class SolicitudProducto
    {
        public SolicitudProducto()
        {
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SolicitudProductoID { get; set; }
        [ForeignKey("SolicitudID")]
        public int SolicitudID { get; set; }
        public int ProductoLiteID { get; set; }
        public int ProductoID { get; set; }
        public int TipoNegocioID { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int UnidadID { get; set; }
        public string UnidadNombre { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaAbreviatura { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }

        public decimal PrecioUnitario { get; set; }
        public bool RequiereReceta { get; set; }
        public decimal PrecioTotal { get; set; }

        [Column("Solicitud")]
        public virtual Solicitud Solicitud { get; set; }

        public virtual ICollection<CotizacionProducto> CotizacionProducto { get; set; }
    }
}
