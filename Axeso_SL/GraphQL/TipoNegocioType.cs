using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class TipoNegocioType : ObjectGraphType<TipoNegocio>
    {
        public TipoNegocioType()
        {
            Field(a => a.TipoNegocioID);
            Field(a => a.Nombre);
            Field(a => a.Abreviatura);
            Field(a => a.Orden);
            Field(a => a.Activo);
        }
    }
}
