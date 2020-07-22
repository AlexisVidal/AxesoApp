using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioInputType : InputObjectGraphType
    {
        public UsuarioInputType()
        {
            Name = "usuarioInput";
            Field<NonNullGraphType<IntGraphType>>("usuarioID");
            Field<StringGraphType>("tipoUsuarioID");
            Field<StringGraphType>("usuario");
            Field<StringGraphType>("descripcion");
            Field<StringGraphType>("email");
            Field<StringGraphType>("telefono");
            Field<BooleanGraphType>("activo");
            Field<BooleanGraphType>("bloqueado");
            Field<StringGraphType>("token");
            Field<DateTimeGraphType>("fchHraCreacion");
            Field<DateTimeGraphType>("fchHraActualizacion");
            Field<DateTimeGraphType>("fchHraBloqueo");
            Field<DateTimeGraphType>("fchHraDesbloqueo");
            Field<DateTimeGraphType>("fechaNacimiento");
            Field<IntGraphType>("dataFarmaciasID");
        }
    }
}
