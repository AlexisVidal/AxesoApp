using Axeso_API.Interfaces;
using Axeso_ET;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_API.GraphQL
{
    public class CulturaQuery : ObjectGraphType
    {
        public CulturaQuery(IRepository culturaRepository)
        {
            Field<ListGraphType<CulturaType>>(
                "culturas",
                resolve: context=> culturaRepository.GetCulturas());

            Field<CulturaType>(
                "cultura",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return culturaRepository.GetCulturaById(id);
                }
            );
        }
    }
}
