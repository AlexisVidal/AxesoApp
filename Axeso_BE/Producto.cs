using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Producto
    {
        public Producto()
        {
        }
        public int ID { get; set; }
        [Key]
        public int ProductoID { get; set; }
        public int TipoNegocioID { get; set; }
        public string Nombre { get; set; }
        [Column("Abreviatura")]
        public string Abreviatura { get; set; }
        
        [ForeignKey("UnidadID_com")]
        public int UnidadID_com { get; set; }
        [ForeignKey("UnidadID_gra")]
        public Nullable<int> UnidadID_gra { get; set; }
        public string Imagen { get; set; }
        public string Busqueda { get; set; }
        public bool RequiereReceta { get; set; }

        public int ProductoMarcaID { get; set; }

        public decimal PrecioRef_com { get; set; }
        public decimal PrecioRef_gra { get; set; }


        //[ForeignKey("CategoriaID")]
        [Column("Categoria")]
        public virtual Categoria Categoria { get; set; }
        [Column("CategoriaID")]
        public int CategoriaID { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        //[Column("UnidadMedida")]
        //public virtual UnidadMedida UnidadMedida1 { get; set; }
        public virtual ICollection<ProductoTipoProducto> ProductoTipoProducto { get; set; }
        
    }
}
