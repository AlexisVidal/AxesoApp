using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class ParametroType : ObjectGraphType<Parametro>
    {
        public ParametroType()
        {
            Field(a => a.ParametroID);
            Field(a => a.Nombre);
            Field(a => a.Valor);
            Field(a => a.Activo);
        }
    }
}
