using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class PedidoProductoInput
    {
        public int PedidoID { get; set; }
        public int CotizacionProductoID { get; set; }
        public int CotizacionID { get; set; }
        public int SolicitudProductoID { get; set; }
        public int ProductoID { get; set; }
        public int TipoNegocioID { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int UnidadId { get; set; }
        public string UnidadNombre { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaAbreviatura { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool RequiereReceta { get; set; }
    }
}
