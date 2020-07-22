using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class SolicitudInputType : InputObjectGraphType
    {
        public SolicitudInputType()
        {
            Name = "solicitudInput";
            Field<StringGraphType>("solicitudCode");
            Field<IntGraphType>("usuarioID");
            Field<StringGraphType>("direccion");
            Field<FloatGraphType>("latitud");
            Field<FloatGraphType>("longitud");
            Field<FloatGraphType>("distancia");
            Field<DateTimeGraphType>("fecha");
            Field<BooleanGraphType>("activo");
            Field<StringGraphType>("fechaEnviado");
            Field<BooleanGraphType>("cotizado");

        }
    }
}
