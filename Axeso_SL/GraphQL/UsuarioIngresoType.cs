using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class UsuarioIngresoType : ObjectGraphType<UsuarioIngreso>
    {
        public UsuarioIngresoType()
        {
            Field(a => a.UsuarioIngresoID);
            Field(a => a.UsuarioID);
            Field(a => a.FechaRegistro);
            Field(a => a.FechaUltimaActualizacion);
            Field(a => a.Disponible);
        }
    }
}
