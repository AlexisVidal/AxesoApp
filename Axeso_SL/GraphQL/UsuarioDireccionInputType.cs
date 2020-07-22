using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioDireccionInputType : InputObjectGraphType
    {
        public UsuarioDireccionInputType()
        {
            Name = "usuarioDireccionInput";
            Field<IntGraphType>("usuarioID");
            Field<StringGraphType>("nombre");
            Field<StringGraphType>("direccion");
            Field<FloatGraphType>("latitud");
            Field<FloatGraphType>("longitud");
            Field<BooleanGraphType>("activo");
        }
    }
}
