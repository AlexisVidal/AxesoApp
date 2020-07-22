using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioIngresoUpInputType : InputObjectGraphType
    {
        public UsuarioIngresoUpInputType()
        {
            Name = "usuarioIngresoUpInput";
            Field<IntGraphType>("usuarioIngresoID");
            Field<IntGraphType>("usuarioID");
            Field<DateTimeGraphType>("fechaRegistro");
            Field<DateTimeGraphType>("fechaUltimaActualizacion");
            Field<BooleanGraphType>("disponible");
        }
    }
}
