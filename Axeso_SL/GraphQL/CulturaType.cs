using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class CulturaType : ObjectGraphType<Cultura>
    {
        public CulturaType()
        {
            Field(a => a.ID);
            Field(a => a.CulturaID);
            Field(a => a.CulturaID_pad);
            Field(a => a.Nombre);
        }
    }
}
