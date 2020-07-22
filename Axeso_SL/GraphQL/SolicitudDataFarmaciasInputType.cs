using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudDataFarmaciasInputType : InputObjectGraphType
    {
        public SolicitudDataFarmaciasInputType()
        {
            Name = "solicitudDataFarmaciasInputType";
            Field<IntGraphType>("solicitudID");
            Field<IntGraphType>("usuarioID");
            Field<BooleanGraphType>("activo");
        }
    }
}
