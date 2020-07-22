using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class UnidadMedida
    {
        public UnidadMedida()
        {
        }
        public int ID { get; set; }

        [Key, Column("UnidadID")]
        public int UnidadID { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Abreviatura")]
        public string Abreviatura { get; set; }
        public bool EsUndiadComercial { get; set; }
        public bool EsUnidadGranel { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
