using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class CotizacionUpInputType : InputObjectGraphType
    {
        public CotizacionUpInputType()
        {
            Name = "cotizacionUpInput";
            Field<IntGraphType>("cotizacionID");
            Field<StringGraphType>("estado");
        }
    }
}
