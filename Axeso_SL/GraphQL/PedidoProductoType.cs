using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_BE;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class PedidoProductoType : ObjectGraphType<PedidoProducto>
    {
        public PedidoProductoType()
        {
            Field(a => a.PedidoProductoID);
            Field(a => a.PedidoID);
            Field(a => a.CotizacionProductoID);
            Field(a => a.CotizacionID);
            Field(a => a.SolicitudProductoID);
            Field(a => a.Activo);
            Field(a => a.ProductoID);
            Field(a => a.TipoNegocioID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.UnidadId);
            Field(a => a.UnidadNombre);
            Field(a => a.CategoriaID);
            Field(a => a.CategoriaNombre);
            Field(a => a.CategoriaAbreviatura);
            Field(a => a.Cantidad);
            Field(a => a.Imagen);
            Field(a => a.RequiereReceta);
            Field(a => a.PrecioUnitario);
            Field(a => a.PrecioTotal);
        }
    }
}
