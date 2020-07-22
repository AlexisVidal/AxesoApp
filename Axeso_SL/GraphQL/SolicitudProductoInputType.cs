using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudProductoInputType : InputObjectGraphType
    {
        public SolicitudProductoInputType()
        {
            Name = "solicitudProductoInput";
            //Field<NonNullGraphType<IntGraphType>>("iD");
            Field<IntGraphType>("solicitudID");
            Field<IntGraphType>("productoLiteID");
            Field<IntGraphType>("productoID");
            Field<IntGraphType>("tipoNegocioID");
            Field<StringGraphType>("nombre");
            Field<StringGraphType>("abreviatura");
            Field<IntGraphType>("unidadID");
            Field<StringGraphType>("unidadNombre");
            Field<IntGraphType>("categoriaID");
            Field<StringGraphType>("categoriaNombre");
            Field<StringGraphType>("categoriaAbreviatura");
            Field<IntGraphType>("cantidad");
            Field<BooleanGraphType>("activo");
            Field<StringGraphType>("imagen");
            Field<DecimalGraphType>("precioUnitario");
            Field<DecimalGraphType>("precioTotal");
            Field<BooleanGraphType>("requiereReceta");
        }
    }
}
