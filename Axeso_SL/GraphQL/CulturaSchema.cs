using Axeso_ET;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_API.GraphQL
{
    public class CulturaSchema : Schema
    {
        public CulturaSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CulturaQuery>();
            Query = resolver.Resolve<AxesoQuery>();
        }
    }
}
