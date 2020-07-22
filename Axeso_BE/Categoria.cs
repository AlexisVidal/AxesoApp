using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class Categoria
    {
        public int ID { get; set; }
        public int TipoNegocioID { get; set; }
        [Column("CategoriaID")]
        public int CategoriaID { get; set; }
        public Nullable<int> CategoriaID_pad { get; set; }
        public string Nombre { get; set; }
        [Column("Abreviatura")]
        public string Abreviatura { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }

        //[Column("Categorias")]
        //public virtual ICollection<Categoria> Categoria1 { get; set; }
        //[Column("Categoria")]
        //public virtual Categoria Categoria2 { get; set; }
        public virtual TipoNegocio TipoNegocio { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
