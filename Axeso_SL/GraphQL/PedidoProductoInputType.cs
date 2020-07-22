using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class PedidoProductoInputType : InputObjectGraphType
    {
        public PedidoProductoInputType()
        {
            Name = "pedidoProductoInput";
            Field<IntGraphType>     ("pedidoID");
            Field<IntGraphType>     ("cotizacionProductoID");
            Field<IntGraphType>     ("cotizacionID");
            Field<IntGraphType> ("solicitudProductoID");
            Field<BooleanGraphType>("activo");
            Field<IntGraphType>  ("productoID");
            Field<IntGraphType>  ("tipoNegocioID");
            Field<StringGraphType>  ("nombre");
            Field<StringGraphType>   ("abreviatura");
            Field<IntGraphType>   ("unidadId");
            Field<StringGraphType>  ("unidadNombre");
            Field<IntGraphType>  ("categoriaID");
            Field<StringGraphType>  ("categoriaNombre");
            Field<StringGraphType>   ("categoriaAbreviatura");
            Field<IntGraphType>   ("cantidad");
            Field<StringGraphType> ("imagen");
            Field<BooleanGraphType>  ("requiereReceta");
            Field<DecimalGraphType> ("precioUnitario");
            Field<DecimalGraphType> ("precioTotal");

        }
    }
}
