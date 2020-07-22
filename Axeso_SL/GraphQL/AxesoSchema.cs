using Axeso_BE;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class AxesoSchema : Schema
    {
        public AxesoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AxesoQuery>();
            Mutation = resolver.Resolve<AxesoMutation>();
        }
    }
}
