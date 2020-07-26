using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axeso_BE;
using GraphQL.Types;

namespace Axeso_SL.GraphQL
{
    public class DistritoType : ObjectGraphType<Distrito>
    {
        public DistritoType()
        {

            Field(a => a.DistritoID);
            Field(a => a.CodigoPostal);
            Field(a => a.Nombre);
            Field(a => a.Estado);
        }
    }
}
