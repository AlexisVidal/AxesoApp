using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class PedidoInputType : InputObjectGraphType
    {
        public PedidoInputType()
        {
            Name = "pedidoInput";
            Field<StringGraphType>("numero");
            Field<IntGraphType>("cotizacionID");
            Field<IntGraphType>("usuarioID");
            Field<IntGraphType>("solicitudID");
            Field<BooleanGraphType>("activo");
            Field<DateTimeGraphType>("fecha");
            Field<StringGraphType>("fechaGenerado");
            Field<StringGraphType>("nombre");
            Field<StringGraphType>("direccion");
            Field<FloatGraphType>("latitud");
            Field<FloatGraphType>("longitud");
            Field<StringGraphType>("estado");
            Field<StringGraphType>("nombreEntrega");
            Field<StringGraphType>("direccionEntrega");
            Field<FloatGraphType>("latitudEntrega");
            Field<FloatGraphType>("longitudEntrega");
            Field<DecimalGraphType>("totalPagar");
            Field<StringGraphType>("tipoPagar");
            Field<DecimalGraphType>("montoPagar");
            Field<IntGraphType>("usuarioClienteID");

        }
    }
}
