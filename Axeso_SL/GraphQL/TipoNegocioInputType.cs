using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class TipoNegocioInputType : InputObjectGraphType
    {
        public TipoNegocioInputType()
        {
            Name = "tipoNegocioInput";
            Field<NonNullGraphType<IntGraphType>>("tipoNegocioID");
            Field<NonNullGraphType<StringGraphType>>("nombre");
            Field<NonNullGraphType<StringGraphType>>("abreviatura");
            Field<NonNullGraphType<IntGraphType>>("orden");
            Field<NonNullGraphType<BooleanGraphType>>("activo");
        }
    }
}
